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
using Utility.Commons;
using BLL.Services;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/fileUpload")]
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private UserService userService;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        [Authorize]
        [Route("delete")]
        public ActionResult Delete(int id, string moduleName, string fileName)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {
                //Getting Document Folder from enum
                string documentFolder = moduleName;
                //Create Folder path
                var folderName = Path.Combine("Uploads", documentFolder);
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
                    if (file != null)
                    {
                        file.Delete();
                        if (moduleName == "User")
                        {
                            ////userService.RemoveProfilePic(id);
                        }
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

        [HttpPost, DisableRequestSizeLimit]
        //[Authorize]
        [AllowAnonymous]
        [Route("save")]
        public async Task<IActionResult> Save(string moduleName)
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

                    if (!string.IsNullOrEmpty(ext))
                    {
                        fileName = Helper.GetTimestamp(DateTime.Now) + ext;
                    }
                    //Getting Document Folder from enum
                    string documentFolder = moduleName;
                    //Create Folder path
                    var folderName = Path.Combine("Uploads", documentFolder);
                    //var folderName = Path.Combine("Uploads", documentFolder, userId.ToString());
                    var pathToSave = Path.Combine(contentRootPath, folderName);

                    if (!Directory.Exists(pathToSave))//check if the folder exists;
                    {
                        Directory.CreateDirectory(pathToSave);
                    }

                    DirectoryInfo di = new DirectoryInfo(pathToSave);
                    var existedFileInfo = di.GetFiles().FirstOrDefault(o => o.Name == fileName);
                    if (existedFileInfo != null && !string.IsNullOrEmpty(existedFileInfo.Name))
                        existedFileInfo.Delete();

                    var fullPath = Path.Combine(pathToSave, fileName);
                    fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbPath = dbPath.Replace("\\", "/");

                    return Ok(new { dbPath, fileName });
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
    }
}
