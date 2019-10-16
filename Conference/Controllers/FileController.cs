using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using ConfService.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("lectures/{lectureId}/files"), DisableRequestSizeLimit]
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
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_fileService.Upload(userId, Request?.Form?.Files[0], lectureId));
        }

        [HttpGet("files/{id}")]
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

        [Authorize]
        [HttpDelete("files/{id}")]
        public IActionResult Delete(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _fileService.Delete(userId, id);
            return Ok();
        }

        [HttpGet("lectures/{id}/files")]
        public IActionResult GetAllByLectureId(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_fileService.GetAllByApplicationId(userId, id));
        }
        
        [Authorize]
        [HttpDelete("applications/{id}/files/notifications")]
        public IActionResult DeleteNotifications(int id)
        {
            _fileService.DeleteNotifications(id);
            return Ok();
        }
    }
}