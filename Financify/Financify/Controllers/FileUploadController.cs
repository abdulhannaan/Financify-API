using BLL.Dtos;
using Utility.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/fileUpload")]
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private string[] allowedProfilePicExtentions = { ".jpg", ".png", ".jpeg" };

        [HttpPost, DisableRequestSizeLimit]
        [AllowAnonymous]
        [Route("save")]
        public async Task<IActionResult> Save(int userId, int fileType, int documentType)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {

                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string ext = Path.GetExtension(file.FileName).ToLower();

                    if (!string.IsNullOrEmpty(ext) && fileType.Equals(Convert.ToInt32(FileType.ProfilePicture)))
                    {
                        if (allowedProfilePicExtentions.Contains(ext))
                            fileName = "profileImage" + ext;
                        else
                            return BadRequest();
                    }
                    //Getting Document Folder from enum
                    string documentFolder = Enums.GetFolderNameByModule(documentType);
                    //Create Folder path
                    var folderName = Path.Combine("Uploads", documentFolder, userId.ToString());
                    var pathToSave = Path.Combine(contentRootPath, folderName);

                    if (!Directory.Exists(pathToSave))//check if the folder exists;
                    {
                        Directory.CreateDirectory(pathToSave);
                    }

                    DirectoryInfo di = new DirectoryInfo(pathToSave);
                    var existedFileInfo = di.GetFiles().FirstOrDefault(o=>o.Name == fileName);
                    if (existedFileInfo != null && !string.IsNullOrEmpty(existedFileInfo.Name))
                        existedFileInfo.Delete();

                    var fullPath = Path.Combine(pathToSave, fileName);
                    fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        //[Authorize]
        [Route("get")]
        public ActionResult Get(int userId, int documentType)
       {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string result = "";
            FileInfoDto fileInfo = new FileInfoDto();
            try
            {
                string fileName = "profileImage";
                //Getting Document Folder from enum
                string documentFolder = Enums.GetFolderNameByModule(documentType);
                //Create Folder path
                var folderName = Path.Combine("Uploads", documentFolder, userId.ToString());
                string imagePath = Path.Combine(contentRootPath, folderName);
                //check if the folder exists;
                if (!Directory.Exists(imagePath))
                {
                    return BadRequest();
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(imagePath);
                    var file = dir.GetFiles().FirstOrDefault(o => o.Name.Contains(fileName));
                    if (file == null)
                    {
                        return BadRequest();

                    }
                    else
                    {
                        string path = folderName + "\\" + file.Name;
                    path = path.Replace("\\", "/");


                    fileInfo.Name = file.Name;
                    fileInfo.Url = path;

                }
                }
                return Ok(fileInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        //[Authorize]
        [Route("get-all")]
        public ActionResult GetAll(int userId, int documentType)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
           List<FileInfoDto> fileInfo = new List<FileInfoDto>();
            try
            {
                //Getting Document Folder from enum
                string documentFolder = Enums.GetFolderNameByModule(documentType);
                //Create Folder path
                var folderName = Path.Combine("Uploads", documentFolder, userId.ToString());
                string imagePath = Path.Combine(contentRootPath, folderName);
                //check if the folder exists;
                if (!Directory.Exists(imagePath))
                {
                    return BadRequest();
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(imagePath);
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        var files = new FileInfoDto();
                        string path = folderName + "\\" + file.Name;
                        path = path.Replace("\\", "/");
                        files.Name = file.Name;
                        files.Url = path;
                        fileInfo.Add( files);
                    }
                    return Ok(fileInfo);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        //[Authorize]
        [Route("delete")]
        public ActionResult Delete(int userId, int documentType, string fileName)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {
                //Getting Document Folder from enum
                string documentFolder = Enums.GetFolderNameByModule(documentType);
                //Create Folder path
                var folderName = Path.Combine("Uploads", documentFolder, userId.ToString());
                string imagePath = Path.Combine(contentRootPath, folderName);
                //check if the folder exists;
                if (!Directory.Exists(imagePath))
                {
                    return BadRequest();
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(imagePath);
                    var file = dir.GetFiles().FirstOrDefault(o => o.Name.Contains(fileName));
                    if (file!=null) 
                    {
                        file.Delete();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
