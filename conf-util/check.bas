Const DocumentFileName = ".\document.doc"
Const TemplateFileName = ".\all-styles.doc"

Const ParagraphFirstLen = 50

Const adLF = 10
Const adWriteLine = 1
Const adSaveCreateOverWrite = 2



Function Contains(Col As Collection, Key As Object
                 ) As Boolean
    Dim obj As Object
    On Error GoTo Err
    Contains = True
    obj = Col(Key)
    Exit Function
Err:
    Contains = False
End Function


Function CompareParagraphFormatting(ByRef Paragraph As Paragraph, ByRef RightParagraph As Paragraph) As String
    If StrComp(Paragraph.Style, "@_Ручное_форматирование") = 0 Or _
            StrComp(Paragraph.Style, "$_Таблица_содержимое") = 0 Then
        CompareParagraphFormatting = "Ok"
        Exit Function
    End If

    Dim Diffs As String
    Dim Result As Boolean

    Dim Range As Range
    Dim RightRange As Range

    Range = Paragraph.Range
    RightRange = RightParagraph.Range

    Dim Font As Font
    Dim RightFont As Font

    Font = Range.Font
    RightFont = RightRange.Font

    Dim Format As ParagraphFormat
    Dim RightFormat As ParagraphFormat

    Format = Paragraph.Format
    RightFormat = RightParagraph.Format

    On Error Resume Next

    ' Будем считать, что шрифт совпадает, если есть хотя бы одно слово, где шрифт совпадает
    Dim _
        FontNameEquals, FontSizeEquals, FontScalingEquals, FontColorEquals, _
        FontBoldEquals, FontItalicEquals, FontUnderlineEquals, FontStrikeThroughEquals, _
        FontCapsEquals
    FontNameEquals = False
    FontSizeEquals = False
    FontScalingEquals = False
    FontColorEquals = False
    FontBoldEquals = False
    FontItalicEquals = False
    FontUnderlineEquals = False
    FontStrikeThroughEquals = False
    FontCapsEqual = False

    Dim Word As Range
    For Each Word In Range.Words
        Font = Word.Font

        If StrComp(Font.Name, RightFont.Name) = 0 Then
            FontNameEquals = True
        End If
        If Font.Size = RightFont.Size Then
            FontSizeEquals = True
        End If
        If Font.Scaling = RightFont.Scaling Then
            FontScalingEquals = True
        End If
        If Font.Color = RightFont.Color Then
            FontColorEquals = True
        End If
        'If Word.Bold = RightRange.Bold Then
        '    FontBoldEquals = True
        'End If
        If Font.Bold = RightFont.Bold Then
            FontBoldEquals = True
        End If
        'If Word.Italic = RightRange.Italic Then
        '    FontItalicEquals = True
        'End If
        If Font.Italic = RightFont.Italic Then
            FontItalicEquals = True
        End If
        'If Word.Underline = RightRange.Underline Then
        '    FontUnderlineEquals = True
        'End If
        If Font.Underline = RightFont.Underline And _
                Font.UnderlineColor = RightFont.UnderlineColor Then
            FontUnderlineEquals = True
        End If
        If Font.StrikeThrough = RightFont.StrikeThrough And _
                Font.DoubleStrikeThrough = RightFont.DoubleStrikeThrough Then
            FontStrikeThroughEquals = True
        End If
        If Font.AllCaps = RightFont.AllCaps And _
                Font.SmallCaps = RightFont.SmallCaps Then
            FontCapsEqual = True
        End If
        'Остальные параметры шрифта пока пропустим:
        '    .Outline
        '    .Emboss
        '    .Shadow
        '    .Hidden
        '    .Engrave
        '    .Superscript
        '    .Subscript
        '    .Kerning
        '    .Animation
        'А также:
        '    .Ligatures
        '    .NumberSpacing
        '    .NumberForm
        '    .StylisticSet
        '    .ContextualAlternates
    Next Word

    Diffs = ""

    If Not FontNameEquals Then
        Diffs = Diffs & ", название шрифта"
    End If
    If Not FontSizeEquals Then
        Diffs = Diffs & ", размер шрифта"
    End If
    If Not FontScalingEquals Then
        Diffs = Diffs & ", масштаб шрифта"
    End If
    If Not FontColorEquals Then
        Diffs = Diffs & ", цвет"
    End If
    If Not FontBoldEquals Then
        Diffs = Diffs & ", жирность"
    End If
    If Not FontItalicEquals Then
        Diffs = Diffs & ", курсив"
    End If
    If Not FontUnderlineEquals Then
        Diffs = Diffs & ", подчеркивание"
    End If
    If Not FontStrikeThroughEquals Then
        Diffs = Diffs & ", перечеркивание"
    End If
    If Not FontCapsEqual Then
        Diffs = Diffs & ", строчные/прописные"
    End If
    'Остальные параметры шрифта пока пропустим:
    '    .Outline
    '    .Emboss
    '    .Shadow
    '    .Hidden
    '    .Engrave
    '    .Superscript
    '    .Subscript
    '    .Kerning
    '    .Animation
    'А также:
    '    .Ligatures
    '    .NumberSpacing
    '    .NumberForm
    '    .StylisticSet
    '    .ContextualAlternates

    If Format.Alignment <> RightFormat.Alignment Then
        Diffs = Diffs & ", выравнивание"
    End If
    If Format.LeftIndent <> RightFormat.LeftIndent Then
        Diffs = Diffs & ", отступ слева"
    End If
    If Format.RightIndent <> RightFormat.RightIndent Then
        Diffs = Diffs & ", отступ справа"
    End If
    If Format.FirstLineIndent <> RightFormat.FirstLineIndent Then
        If StrComp(Paragraph.Style, "$_Абзац_(обычный)") <> 0 Then
            Diffs = Diffs & ", первая строка отступ/выступ"
        End If
    End If
    If Format.SpaceBefore <> RightFormat.SpaceBefore Or _
            Format.SpaceBeforeAuto <> RightFormat.SpaceBeforeAuto Then
        Diffs = Diffs & ", интервал перед"
    End If
    If Format.SpaceAfter <> RightFormat.SpaceAfter Or _
            Format.SpaceAfterAuto <> RightFormat.SpaceAfterAuto Then
        Diffs = Diffs & ", интервал после"
    End If
    If Format.LineSpacingRule <> RightFormat.LineSpacingRule Then
        Diffs = Diffs & ", междустрочный интервал"
    End If
    If Format.KeepWithNext <> RightFormat.KeepWithNext Then
        Diffs = Diffs & ", не отрывать от следующего"
    End If
    If Format.KeepTogether <> RightFormat.KeepTogether Then
        Diffs = Diffs & ", не разрывать абзац"
    End If
    If Format.PageBreakBefore <> RightFormat.PageBreakBefore Then
        Diffs = Diffs & ", с новой страницы"
    End If
    If Format.OutlineLevel <> RightFormat.OutlineLevel Then
        Diffs = Diffs & ", уровень"
    End If

    'Остальные параметры абзаца пока пропустим:
    '    .WidowControl
    '    .NoLineNumber
    '    .Hyphenation
    '    .CharacterUnitLeftIndent
    '    .CharacterUnitRightIndent
    '    .CharacterUnitFirstLineIndent
    '    .LineUnitBefore
    '    .LineUnitAfter
    'А также:
    '    .MirrorIndents
    '    .TextboxTightWrap

    On Error GoTo 0

    If Len(Diffs) > 0 Then
        CompareParagraphFormatting = _
            "Форматирование абзаца отличается шаблонного для стиля """ & _
                Paragraph.Style & """ (различия: " & Right(Diffs, Len(Diffs) - 2) & ")"
    Else
        CompareParagraphFormatting = "Ok"
    End If
End Function


Function CheckSameFormatting(ByRef Paragraph As Paragraph) As String
    If StrComp(Paragraph.Style, "@_Ручное_форматирование") = 0 Or _
            StrComp(Paragraph.Style, "$_Таблица_содержимое") = 0 Then
        CheckSameFormatting = "Ok"
        Exit Function
    End If

    Dim Font As Font
    Font = Paragraph.Range.Font

    On Error Resume Next

    Diffs = ""

    Dim Result As Boolean
    Result = False
    Result = Font.Name = wdUndefined
    Result = Result Or Len(Font.Name) = 0
    If Result Then
        Diffs = Diffs & ", название шрифта"
    End If
    If Font.Size = wdUndefined Then
        Diffs = Diffs & ", размер шрифта"
    End If
    If Font.Scaling = wdUndefined Then
        Diffs = Diffs & ", масштаб шрифта"
    End If
    If Font.Color = wdUndefined Then
        Diffs = Diffs & ", цвет"
    End If
    If Font.Bold = wdUndefined Then
        Diffs = Diffs & ", жирность"
    End If
    If Font.Italic = wdUndefined Then
        Diffs = Diffs & ", курсив"
    End If
    If Font.Underline = wdUndefined Or _
            Font.UnderlineColor = wdUndefined Then
        Diffs = Diffs & ", подчеркивание"
    End If
    If Font.StrikeThrough = wdUndefined Or _
            Font.DoubleStrikeThrough = wdUndefined Then
        Diffs = Diffs & ", перечеркивание"
    End If
    If Font.AllCaps = wdUndefined Or _
            Font.SmallCaps = wdUndefined Then
        Diffs = Diffs & ", строчные/прописные"
    End If
    'Остальные параметры шрифта пока пропустим:
    '    .Outline
    '    .Emboss
    '    .Shadow
    '    .Hidden
    '    .Engrave
    '    .Superscript
    '    .Subscript
    '    .Kerning
    '    .Animation
    'А также:
    '    .Ligatures
    '    .NumberSpacing
    '    .NumberForm
    '    .StylisticSet
    '    .ContextualAlternates

    On Error GoTo 0

    If Len(Diffs) > 0 Then
        CheckSameFormatting = _
            "Абзац состоит из фрагментов с разным форматированием" & _
            " (различия: " & Right(Diffs, Len(Diffs) - 2) & ")"
    Else
        CheckSameFormatting = "Ok"
    End If
End Function


Sub Check()
    Dim TemplateDoc As Document
    TemplateDoc = Application.Documents.Open(
        FileName:=TemplateFileName,
        ReadOnly:=True,
        AddToRecentFiles:=False,
        Visible:=False
    )

    Dim Styles As Collection
    Styles = New Collection

    Dim Paragraph As Paragraph
    Dim Range As Range

    For Each Paragraph In TemplateDoc.Paragraphs
        If InStr(1, Paragraph.Style, "$_") = 1 Or InStr(1, Paragraph.Style, "@_") = 1 Then
            If Not Contains(Styles, Paragraph.Style) Then
                Styles.Add(Paragraph, Paragraph.Style)
            End If
        End If
    Next Paragraph

    Dim ErrorsAndWarnings As Collection
    ErrorsAndWarnings = New Collection

    Dim Result As String
    Result = "Ok"
    Dim StylesFound As Boolean
    StylesFound = False
    For Each Paragraph In ActiveDocument.Paragraphs
        Dim ParagraphText As String
        ParagraphText = Paragraph.Range.Text
        If Len(ParagraphText) > ParagraphFirstLen Then
            ParagraphText = Left(ParagraphText, ParagraphFirstLen) & "..."
            ParagraphText = Replace(ParagraphText, "<", " ")
            ParagraphText = Replace(ParagraphText, ">", " ")
            ParagraphText = Replace(ParagraphText, Chr(1), " ")
            ParagraphText = Replace(ParagraphText, Chr(10), " ")
            ParagraphText = Replace(ParagraphText, Chr(13), " ")
            ParagraphText = Replace(ParagraphText, Chr(09), " ")
        End If

        If Contains(Styles, Paragraph.Style) Then
            Dim CheckResult As String
            CheckResult = CompareParagraphFormatting(Paragraph, Styles(Paragraph.Style))
            If StrComp(CheckResult, "Ok") <> 0 Then
                ErrorsAndWarnings.Add(Array(
                    "Error", ParagraphText,
                    CheckResult
                ))
                Result = "Error"
            Else
                CheckResult = CheckSameFormatting(Paragraph)
                If StrComp(CheckResult, "Ok") <> 0 Then
                    ErrorsAndWarnings.Add(Array(
                        "Warning", ParagraphText,
                        CheckResult
                    ))
                    If StrComp(Result, "Ok") = 0 Then
                        Result = "Warning"
                    End If
                End If
            End If
            StylesFound = True
        Else
            If InStr(ParagraphText, Chr(7)) <> Len(ParagraphText) Then
                ErrorsAndWarnings.Add(Array(
                    "Error", ParagraphText,
                    "Стиль """ & Paragraph.Style & """ для абзаца недопустим (не шаблонный)"
                ))
                Result = "Error"
            End If
        End If
    Next Paragraph

    If Not StylesFound Then
        ErrorsAndWarnings = New Collection
        ErrorsAndWarnings.Add(Array(
            "Error", "Весь документ",
            "Шаблон оформления не был использован."
        ))
        Result = "Error"
    End If

    Dim objStream As Object
    objStream = CreateObject("ADODB.Stream")
    objStream.Charset = "utf-8"
    objStream.LineSeparator = adLF
    objStream.Open

    objStream.WriteText("<Check Result=""" & Result & """>", adWriteLine)
    Dim ErrWarn
    For Each ErrWarn In ErrorsAndWarnings
        objStream.WriteText("  <" & ErrWarn(0) & ">", adWriteLine)
        objStream.WriteText("    <Paragraph>" & ErrWarn(1) & "</Paragraph>", adWriteLine)
        objStream.WriteText("    <Comment>" & ErrWarn(2) & "</Comment>", adWriteLine)
        objStream.WriteText("  </" & ErrWarn(0) & ">", adWriteLine)
    Next ErrWarn
    objStream.WriteText("</Check>", adWriteLine)

    objStream.SaveToFile(DocumentFileName & ".check", adSaveCreateOverWrite)
    objStream.Close

    TemplateDoc.Close(wdDoNotSaveChanges)
End Sub
