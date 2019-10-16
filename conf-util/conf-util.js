/*
  Для программного доступа к VBAProject у Word надо разрешить
  "Доверять доступ к объектной модели проектов VBA"
  ( Параметры Word -> Центр Управления безопасностью -> Параметры макросов )
*/
var wdSectionBreakNextPage = 2;
var wdCollapseStart = 1;
var wdPageView = 3;
var wdDoNotSaveChanges = 0;
var wdSaveChanges = -1;
var wdFormatDocument = 0;
var wdRefTypeBookmark = 2;
var wdPageNumber = 7;
var wdKeyCategoryCommand = 1;
var wdKeyCategoryMacro = 2;

var wdKeyShift = 256;
var wdKeyControl = 512;
var wdKeyAlt = 1024;
var wdKeyE = 69;
var wdKeyF = 70;
var wdKey1 = 49;
var wdKeyN = 78;
var wdKeyU = 85;
var wdKeyS = 83;
var wdKeyT = 84;
var wdKeyB = 66;

var vbext_ct_StdModule = 1;

// кол-во первых абзацев для просмотра, чтобы определить название и авторов
var firstParagraphsCountForInfo = 10;


// %HOME% заменяется на директорию, в которой лежит данный скрипт
var templateFilename = "%HOME%/template.doc";
var headerFilename = "%HOME%/header.doc";
var footerFilename = "%HOME%/footer.doc";

var global = this;
var ws = WScript;
var wssh = new ActiveXObject("WScript.Shell");



var scriptFilename = getFullPath(ws.ScriptFullName);
var scriptDir = scriptFilename.replace(/\\[^\\]*$/, "");

var templateFilename = templateFilename.replace("%HOME%", scriptDir);
var headerFilename = headerFilename.replace("%HOME%", scriptDir);
var footerFilename = footerFilename.replace("%HOME%", scriptDir);



function nvl(a, b) {
  return a ? a : b;
}



String.prototype.trim = function () {
  return this.replace(/^(\s|\n|\r|\u00A0)+|(\s|\n|\r|\u00A0)+$/g, "");
};
String.prototype.firstToLowerCase = function () {
  return !this || this.length == 0 ? this : this.substr(0, 1).toLowerCase() + this.substr(1);
};
String.prototype.firstToUpperCase = function () {
  return !this || this.length == 0 ? this : this.substr(0, 1).toUpperCase() + this.substr(1);
};

String.prototype.iconv = function (from, to) {
  if (!this) {
    return this;
  }

  var getCharsetName = function (charset) {
    charset = /^\s*(866|cp866|dos)\s*$/i.test(charset) ? "ibm866" : charset;
    charset = /^\s*(1251|cp1251|win|windows)\s*$/i.test(charset) ? "windows-1251" : charset;

    return charset;
  };

  var stream = new ActiveXObject("ADODB.Stream");
  stream.Type = 2;
  stream.Mode = 3;
  stream.Open();
  stream.Charset = getCharsetName(to);
  stream.WriteText(this);
  stream.Position = 0;
  stream.Charset = getCharsetName(from);
  var result = stream.ReadText();
  stream.Close();

  return result;
};



function getParam(index) {
  if (ws.Arguments.Count() > index)
    return ws.Arguments(index);
}


function exit(status) {
  ws.Quit(status);
}


function print(value) {
  ws.Echo(value);
}


function printObj(obj, objName) {
  if (JSON)
    print((objName ? objName + " = " : "") + JSON.stringify(obj, null, "  "));
}


function error(value) {
  ws.StdErr.WriteLine(value);
}


function loadJson(json) {
  eval("var data = " + json + ";");
  return data;
}


function getFullPath(filename) {
  var fso = new ActiveXObject("Scripting.FileSystemObject");
  return fso.GetAbsolutePathName(filename);
}


function fileExists(filename) {
  var fso = new ActiveXObject("Scripting.FileSystemObject");
  return fso.FileExists(filename);
}

function dirExists(dirname) {
  var fso = new ActiveXObject("Scripting.FileSystemObject");
  return fso.FolderExists(dirname);
}


function readFile(filename) {
  var fso = new ActiveXObject("Scripting.FileSystemObject");
  var file = fso.GetFile(filename);
  var ts = file.OpenAsTextStream(1);
  var content = ts.ReadAll();
  ts.Close();
  
  return content;
}


function writeFile(filename, content) {
  var fso = new ActiveXObject("Scripting.FileSystemObject");
  var ts = fso.CreateTextFile(filename);
  ts.Write(content);
  ts.Close();
}


function include(filename) {
  if (! /^[a-z]:\\/i.test(filename)) {
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var file = fso.GetFile(WScript.ScriptFullName);
    filename = fso.BuildPath(file.ParentFolder.Path, filename);
  }

  var content = readFile(filename);
  eval(content);
}


function getAllFileNames(dir, includeRegexp, recursive, excludeRegexp, fso) {
  if (includeRegexp && !(includeRegexp instanceof RegExp))
    includeRegexp = new RegExp(includeRegexp);
  if (excludeRegexp && !(excludeRegexp instanceof RegExp))
    excludeRegexp = new RegExp(excludeRegexp);
  if (!fso)
    fso = new ActiveXObject("Scripting.FileSystemObject");

  var result = [];
  
  var folder = fso.GetFolder(dir);

  var en = new Enumerator(folder.Files);
  for (en.moveFirst(); !en.atEnd(); en.moveNext()) {  
    var it = en.item();
    var filename = fso.GetFileName(it.Path);
    if ((!includeRegexp || includeRegexp && includeRegexp.test(filename)) &&
        (!excludeRegexp || excludeRegexp && !excludeRegexp.test(filename)))
      result.push(fso.BuildPath(dir, it.Name));
  }

  if (recursive) {
    var en = new Enumerator(folder.SubFolders);  
    for (en.moveFirst(); !en.atEnd(); en.moveNext()) {  
      var it = en.item();
      result = result.concat(getAllFileNames(
        fso.BuildPath(dir, it.Name), includeRegexp, recursive, excludeRegexp, fso
      ));
    }
  }

  return result;
}



function preprocValue(value) {
  if (value) {
    value = "" + value;
    value = value.replace(/\s+/g, " ");
    value = value.trim();
    if (value == "")
      value = null;
  }
  return value;
}





function parseDocument(doc, firstParagraphsCount) {
  if (!firstParagraphsCount)
    firstParagraphsCount = 1E6;

  var result = {
    paragraphsByStyle: {}
  };

  for (var i = 1; i <= Math.min(doc.Paragraphs.Count, firstParagraphsCount); i++) {
    var p = doc.Paragraphs(i)
    if (!(p.Style.NameLocal in result.paragraphsByStyle))
      result.paragraphsByStyle[p.Style.NameLocal] = [];
    result.paragraphsByStyle[p.Style.NameLocal].push({
      paragraph: p,
      index: i
    });
  }

  return result;
}


function newPage(doc) {
  var lastPos = doc.Content.End;
  if (lastPos <= 2)
    return;
  var lastRange = doc.Range(lastPos - 1, lastPos);
  lastRange.Collapse(wdCollapseStart);
  // вместо новой страницы всегда добавляется раздел с новой страницы,
  // т.к. это избавляет от ряда проблем
  // (например, со страницами, которые начинаются сразу с таблицы)
  lastRange.InsertBreak(wdSectionBreakNextPage);
}


function applyMainStyles(doc) {
  var processStyles = [
    "$_Статья_название",
    "$_Автор",
    "$_Организация",
    "$_Аннотация",
    "$_Ключевые_слова",
    "$_Копирайт",
    //"$_Формула_номер", // в некоторых документах еще и номера формул сбрасываются
    //"$_Абзац_с_маркером",
    //"$_Абзац_с_номером",
    //"$_Список_с_маркером",
    //"$_Список_с_номером",
    "$_Раздел_название_с_номером",
    "$_Раздел_название_без_номера",
    "$_Рисунок_название_с_номером",
    "$_Рисунок_название_без_номера",
    "$_Таблица_заголовок_с_номером",
    "$_Таблица_заголовок_без_номера",
    "$_Таблица_название",
    "$_Листинг_заголовок_с_номером",
    "$_Листинг_заголовок_без_номера",
    "$_Листинг_название",
    "$_Список_литературы"
  ];
  var temp = {};
  for (var s in processStyles)
    temp[processStyles[s]] = true;
  processStyles = temp;

  for (var i = 1; i <= doc.Paragraphs.Count; i++) {
    var p = doc.Paragraphs(i)
    if (p.Style.NameLocal in processStyles) {
      if (p.Style.NameLocal == "$_Список_литературы")
        var fontSize = p.Range.Font.Size;
      p.Style = doc.Styles(p.Style.NameLocal);
      if (p.Style.NameLocal == "$_Список_литературы")
        p.Range.Font.Size = fontSize;
    }
  }
  for (var i = 1; i <= doc.Footnotes.Count; i++) {
    var f = doc.Footnotes(i);
    for (var j = 1; j <= f.Range.Paragraphs.Count; j++) {
      var p = f.Range.Paragraphs(j);
      if (p.Style.NameLocal in processStyles) {
        if (p.Style.NameLocal == "$_Копирайт")
          var fontSize = p.Range.Font.Size;
        p.Style = doc.Styles(p.Style.NameLocal);
        if (p.Style.NameLocal == "$_Копирайт")
          p.Range.Font.Size = fontSize;
      }
    }
  }
}


function checkContentEqualsAfterPaste(app, filename) {
  var addDoc = app.Documents.Add(getFullPath(templateFilename));
  var tempDoc = app.Documents.Add(getFullPath(filename));
  addDoc.Content.FormattedText = tempDoc.Content.FormattedText;
  applyMainStyles(addDoc);  

  tempDoc.Content.ListFormat.ConvertNumbersToText();
  addDoc.Content.ListFormat.ConvertNumbersToText();

  var changes = [];
  for (var i = 1; i <= Math.min(addDoc.Paragraphs.Count, tempDoc.Paragraphs.Count); i++) {
    var tempP = tempDoc.Paragraphs(i);
    var addP = addDoc.Paragraphs(i);
    if (addP.Range.Text != tempP.Range.Text) {
      changes.push({
        before: tempP.Range.Text.substr(1, 40).trim(),
        after: addP.Range.Text.substr(1, 40).trim(),
        style: addP.Style.NameLocal
      });
    }
  }

  tempDoc.Close(wdDoNotSaveChanges);
  addDoc.Close(wdDoNotSaveChanges);

  return changes;
}


function addDocument(doc, filename, startNewPage, bookmark) {
  var addDoc = doc.Application.Documents.Add(getFullPath(templateFilename));
  var tempDoc = doc.Application.Documents.Add(getFullPath(filename));
  addDoc.Content.FormattedText = tempDoc.Content.FormattedText;
  tempDoc.Close(wdDoNotSaveChanges);

  applyMainStyles(addDoc);

  if (bookmark) {
    var parsed = parseDocument(addDoc, firstParagraphsCountForInfo);

    var articleName = null;
    var title = parsed.paragraphsByStyle["$_Статья_название"];
    if (title)
      bookmark = addDoc.Bookmarks.Add(bookmark, title[0].paragraph.Range);
  }

  if (startNewPage)
    newPage(doc);

  var lastPos = doc.Content.End;
  var lastRange = doc.Content;
  if (lastPos > 2)
    var lastRange = doc.Range(lastPos - 1, lastPos);
  lastRange.FormattedText = addDoc.Content.FormattedText;

  doc.UndoClear();

  addDoc.UndoClear();
  addDoc.Close(wdDoNotSaveChanges);
}


function deleteLinkStyles(doc) {
  try {
    doc.Styles("Гиперссылка").Delete();
  } catch (ignored) { }
  for (var i = 1; i < 10; i++) {
    try {
      doc.Styles("Гиперссылка" + i).Delete();
    } catch (ignored) { }
    try {
      doc.Styles("Гиперссылка " + i).Delete();
    } catch (ignored) { }
  }
  try {
    doc.Styles("Internet Link").Delete();
  } catch (ignored) { }
  for (var i = 1; i < 10; i++) {
    try {
      doc.Styles("Internet Link" + i).Delete();
    } catch (ignored) { }
    try {
      doc.Styles("Internet Link " + i).Delete();
    } catch (ignored) { }
  }
}



function appError(msg) {
  error("Error: " + msg);
}



function checkCommand(filename, print) {
  if (!fileExists(filename)) {
    appError("File " + filename + " not found");
    exit(1);
  }
  error("file exists. horaay");
  error(scriptDir);
  var docFilename = getFullPath(filename);
  var docDir = docFilename.replace(/\\[^\\]*$/, "");

  var vba = readFile(scriptDir + "\\check.bas");
  vba = vba.replace(/".\\(document.doc)"/, "\"" + docFilename + "\"");
  vba = vba.replace(/".\\(all-styles.doc)"/, "\"" + scriptDir + "\\$1\"");

  var word = new ActiveXObject("Word.Application");
  var doc = word.Documents.Add(docFilename);

  var moduleName = "CheckModule";
  try {
    doc.VBProject.VBComponents.Remove(doc.VBProject.VBComponents(moduleName));
  }
  catch (ignored) {
    error("Ignored Error: " + JSON.stringify(ignored));
  }
  var newModule = doc.VBProject.VBComponents.Add(vbext_ct_StdModule);
  newModule.Name = moduleName;
  newModule.CodeModule.AddFromString(vba);

  try {
    word.Run("CheckModule.Check");
  }
  catch (e) {
    error("Error: " + JSON.stringify(e));
  }

  doc.Close(wdDoNotSaveChanges);
  word.Quit();
}



function updateMacroCommand(filename) {
  if (!fileExists(filename)) {
    appError("File " + filename + " not found");
    exit(1);
  }

  var moduleName = "NewMacros";

  var docFilename = getFullPath(filename);

  var word = new ActiveXObject("Word.Application");
  var doc = word.Documents.Add(scriptDir + "\\all-styles.doc");

  var vba = "";
  var vbComponents = doc.VBProject.VBComponents;
  for (var i = 1; i <= vbComponents.Count; i++) {
    var module = vbComponents(i);
    if (module.Name == moduleName)
      vba = module.CodeModule.Lines(1, module.CodeModule.CountOfLines);
  }

  doc.Close(wdDoNotSaveChanges);

  var doc = word.Documents.Open(docFilename);

  var vbComponents = doc.VBProject.VBComponents;
  for (var i = vbComponents.Count; i > 0; i--)
    try {
      vbComponents.Remove(vbComponents(i));
    }
    catch (ignored) { }
  for (var i = vbComponents.Count; i > 0; i--)
    try {
      vbComponents(i).CodeModule.DeleteLines(1, vbComponents(i).CodeModule.CountOfLines);
    }
    catch (ignored) { }

  var newModule = doc.VBProject.VBComponents.Add(vbext_ct_StdModule);
      newModule.Name = moduleName;
      newModule.CodeModule.AddFromString(vba);

  word.CustomizationContext = doc;
  word.KeyBindings.Add(wdKeyCategoryMacro, "CreateFormula", word.BuildKeyCode(wdKeyAlt, wdKeyF));
  word.KeyBindings.Add(wdKeyCategoryMacro, "CreateE", word.BuildKeyCode(wdKeyAlt, wdKeyE));
  word.KeyBindings.Add(wdKeyCategoryMacro, "Size1", word.BuildKeyCode(wdKeyAlt, wdKey1));
  word.KeyBindings.Add(wdKeyCategoryMacro, "ApplyStylesToContent", word.BuildKeyCode(wdKeyAlt, wdKeyN));
  word.KeyBindings.Add(wdKeyCategoryMacro, "UpdateAll", word.BuildKeyCode(wdKeyAlt, wdKeyU));
  word.KeyBindings.Add(wdKeyCategoryCommand, "FormattingPane", word.BuildKeyCode(wdKeyAlt, wdKeyS));
  word.KeyBindings.Add(wdKeyCategoryCommand, "Superscript", word.BuildKeyCode(wdKeyAlt, wdKeyT));
  word.KeyBindings.Add(wdKeyCategoryCommand, "Subscript", word.BuildKeyCode(wdKeyAlt, wdKeyB));

  doc.Close(wdSaveChanges);
  word.Quit();
}



function getName(str) {
  var letterOrDigit = "[А-ЯЁа-яёA-Za-z0-9]";
  var regExp = new RegExp(letterOrDigit + ".*" + letterOrDigit);
  return str.match(regExp)[0];
}

function getFios(str) {
  var secondName = "[А-ЯЁA-Z][-А-ЯЁа-яёA-Za-z]+";
  var initial = "[А-ЯЁA-Z]\\.";
  var ws = "[\\s|\\u00A0]";
  var fio1 = "(" + secondName + ")" + ws + "+(" + initial + ")" + ws + "*(" + initial + ")";
  var fio2 = "(" + initial + ")" + ws + "*(" + initial + ")" + ws + "+(" + secondName + ")";
  var regExp = new RegExp(fio1 + "|" + fio2, "g");
  var regExp1 = new RegExp(fio1);
  var regExp2 = new RegExp(fio2);

  var authors = [];
  var temp = str.match(regExp);    
  if (temp)
    for (var i = 0; i < temp.length; i++) {
      var author = temp[i];
      var match = author.match(regExp1);
      if (match)
        author = match[1] + " " + match[2] + "\u00A0" + match[3];
      var match = author.match(regExp2);
      if (match)
        author = match[3] + " " + match[1] + "\u00A0" + match[2];
      authors.push(author);
    }

  return authors;
}

function infoCommand(filename) {
  var checkOut = [];

  var word = new ActiveXObject("Word.Application");
  var doc = word.Documents.Open(getFullPath(filename));

  var parsed = parseDocument(doc, firstParagraphsCountForInfo);

  var articleName = null;
  if (parsed.paragraphsByStyle["$_Статья_название"])
    articleName = getName(parsed.paragraphsByStyle["$_Статья_название"][0].paragraph.Range.Text);
  var authors = [];
  if (parsed.paragraphsByStyle["$_Автор"])
    for (var i in parsed.paragraphsByStyle["$_Автор"])
      authors = authors.concat(getFios(parsed.paragraphsByStyle["$_Автор"][i].paragraph.Range.Text));

  print("Authors: " + authors.join(", "));
  print("Name: " + articleName);

  doc.Close(wdDoNotSaveChanges);
  word.Quit();
}



function groupCommand(filenames, outFilename) {
  for (var i in filenames) {
    if (!fileExists(filenames[i])) {
      appError("File " + filenames[i] + " not found");
      exit(1);
    }
  }

  var word = new ActiveXObject("Word.Application");


  print("Чтение документов:");

  var allFilesByFullArticleName = {};
  var allBookmarksByFullArticleName = {};
  var allFullArticleNames = [];
  for (var i in filenames) {
    print("  " + filenames[i]);
    var doc = word.Documents.Add(getFullPath(filenames[i]));

    var parsed = parseDocument(doc, firstParagraphsCountForInfo);

    var articleName = null;
    if (parsed.paragraphsByStyle["$_Статья_название"])
      articleName = getName(parsed.paragraphsByStyle["$_Статья_название"][0].paragraph.Range.Text);
    var authors = [];
    if (parsed.paragraphsByStyle["$_Автор"])
      for (var j in parsed.paragraphsByStyle["$_Автор"])
        authors = authors.concat(getFios(parsed.paragraphsByStyle["$_Автор"][j].paragraph.Range.Text));
    var fullArticleName = authors.join(", ") + " " + articleName;
    var bookmark = (authors[0] + " " + articleName).substr(0, 40).replace(/[^А-ЯЁа-яёA-Za-z0-9]/g, "_");

    doc.Close(wdDoNotSaveChanges);

    allFullArticleNames.push(fullArticleName);
    allFilesByFullArticleName[fullArticleName] = filenames[i];
    allBookmarksByFullArticleName[fullArticleName] = bookmark;
  }
  allFullArticleNames = allFullArticleNames.sort();
  

  print("Объединение документов:");

  var doc = word.Documents.Add(getFullPath(templateFilename));
  try {
    addDocument(doc, getFullPath(headerFilename), true);
  } catch (ignored) { }

  for (var i in allFullArticleNames) {
    var filename = allFilesByFullArticleName[allFullArticleNames[i]];
    var bookmark = allBookmarksByFullArticleName[allFullArticleNames[i]];
    
    print("  " + filename);
    var changes = checkContentEqualsAfterPaste(word, filename);
    if (changes && changes.length > 0) {
      print("    !!! Форматирование поменялось");
      for (var i in changes) {
        print("      - " + changes[i].before + " / " + changes[i].style);
        print("      + " + changes[i].after + " / " + changes[i].style);
      }
    }
    addDocument(doc, filename, true, bookmark);
  }


  print("Создание оглавления ...");

  newPage(doc);

  var range = doc.Range(doc.Content.End - 1);
  range.Style = "$_Статья_название";
  range.InsertAfter("Содержание\r\n");

  var range = doc.Range(doc.Content.End - 1);
  range.Style = "@_Оглавление_секция";
  range.InsertAfter("Название секции\r\n");

  var range = doc.Range(doc.Content.End - 1);
  range.Style = "@_Оглавление_номер";

  for (var i in allFullArticleNames) {
    var title = allFullArticleNames[i];
    var bookmark = allBookmarksByFullArticleName[allFullArticleNames[i]];

    var end = doc.Content.End - 1;
    range.InsertAfter(title + "\t");
    doc.Range(doc.Content.End - 1).InsertCrossReference(wdRefTypeBookmark, wdPageNumber, bookmark);
    end = doc.Range(end, doc.Content.End);
    doc.Hyperlinks.Add(end, "", bookmark); 
    if (i < allFullArticleNames.length - 1)
      range.InsertAfter("\r\n");
  }

  range.Fields.Update();  // почему не работает (но это не проблема)

  deleteLinkStyles(doc);

  doc.UndoClear();

  try {
    addDocument(doc, getFullPath(footerFilename), true);
  } catch (ignored) { }
  
  doc.SaveAs(getFullPath(outFilename), wdFormatDocument);

  doc.Close(wdDoNotSaveChanges);
  word.Quit();
}



include("./lib/JSON-js/json2.js");



var command = getParam(0),
    param = getParam(1);
var superString = "noooo";
error(superString);
error(command);
error(param);
if (command == "info" && param) {
  infoCommand(param);
}
else if (command == "check" && param) {
  checkCommand(param);
}
else if (command == "upd-macro" && param) {
  updateMacroCommand(param);
}
else if (command == "group" && param) {
  var filenames = [];
  for (var i = 2; param; i++) {
    if (param == "-o") {
      var outFilename = getParam(i++);
      param = getParam(i);
      continue;
    }
    if (dirExists(param))
      filenames = filenames.concat(getAllFileNames(param, /.*\.(doc|docx)$/i, true, /^~/));
    else if (fileExists(param))
      filenames.push(param);
    param = getParam(i);
  }
  groupCommand(filenames, outFilename ? outFilename : "./result.doc");
}
else {
  error("Usage:");
  error("  article-util info <filename>");
  error("  article-util check <filename>");
  error("  article-util upd-macro <filename>");
  error("  article-util group [-o <filename>] <(dirname|filename)> ...");
}
