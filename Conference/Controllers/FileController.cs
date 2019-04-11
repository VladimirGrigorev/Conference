using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ConfService.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Conference.Controllers
{
    [Route("api")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        //private IHostingEnvironment _hostingEnvironment;

        public FileController(IFileService fileService/*IHostingEnvironment hostingEnvironment*/)
        {
            _fileService = fileService;
            //_hostingEnvironment = hostingEnvironment;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("lectures/{lectureId}/files")]
        public ActionResult Upload(int lectureId)
        {
            //try
            //{
            //    var file = Request.Form.Files[0];
            //    string folderName = "Upload";
            //    //string webRootPath = _hostingEnvironment.WebRootPath;

            //    //Directory..GetCurrentDirectory();//Path.Combine(webRootPath, folderName);

            //    //var file = Request.Form.Files[0];
            //    //var folderName = Path.Combine("Resources", "Images");
            //    //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            //    string newPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //    if (!Directory.Exists(newPath))
            //    {
            //        Directory.CreateDirectory(newPath);
            //    }
            //    if (file.Length > 0)
            //    {
            //        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        string fullPath = Path.Combine(newPath, fileName);
            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            file.CopyTo(stream);
            //        }
            //    }
            //    return Ok("Upload Successful.");
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //    //return ("Upload Failed: " + ex.Message);
            //}

            return Ok(_fileService.Upload(Request?.Form?.Files[0], lectureId));
        }
        [Route("files/{id}")]
        public IActionResult Download(int id)
        {
            //if (filename == null)
            //    throw new Exception("filename not present");

            //string folderName = "Upload";

            
            //return Ok(new {mimetype = mimeType, file = File(memory, mimeType, Path.GetFileName(path))});


            var col = _fileService.Download(id);
            return File(col.fileStream, col.contentType,col.fileDownloadName);
            // memory, mimeType, Path.GetFileName(path));
        }



        [HttpGet("lectures/{id}/files")]
        public IActionResult GetAllByLectureId(int id)
        {
            return Ok(_fileService.GetAllByLectureId(id));
        }
    }
}