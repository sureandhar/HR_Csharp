using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;
using Dhrms.DataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dhrms.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        Timer aTimer;
        static int lastHour = 0;
        DhrmsRepository _repository;
        public UploadController()
        {
            //timer interval for a day
            var totalMilliSecondsPerDay = TimeSpan.FromDays(1).TotalMilliseconds;
            aTimer = new System.Timers.Timer(totalMilliSecondsPerDay);
            //this line of code will repeteadly call method with specified interval
            aTimer.Elapsed += new ElapsedEventHandler(OnTimer_Elapsed);
            //to start the timer
            aTimer.Start();
            _repository = new DhrmsRepository();
        }


        private static void OnTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            if ( DateTime.Now.Hour ==0)
            {
                lastHour = DateTime.Now.Hour;
                //seperating delete task form the main threa
                Task.Run(() =>
                {
                    DeleteTempFiles();
                });
            }

        }

        /// <summary>
        /// To delete all the file inside temp folder for every day
        /// </summary>
        private static void DeleteTempFiles()
        {
            try
            {
                var folderName = Path.Combine("Resources", "Temp");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                System.IO.DirectoryInfo di = new DirectoryInfo("YourPath");

                //to detele file
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                //to detele directory

                //foreach (DirectoryInfo dir in di.EnumerateDirectories())
                //{
                //    dir.Delete(true);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        //once received the request it'll store file in the specified folder and return the path
        [HttpPost,DisableRequestSizeLimit]
        public IActionResult Upload(string username)
        {
            try
            {
                //string username = _repository.GetUsername(emailID);
                
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Temp",username.Replace(' ','_'));
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if (file.Length>0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    //create directory for user if not exists
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    System.IO.DirectoryInfo di = new DirectoryInfo(pathToSave);

                    //delete file if already exists with the same name
                    foreach (FileInfo _file in di.EnumerateFiles())
                    {
                        var _filename = _file.FullName;
                        if(_filename.Contains(fileName))
                        {
                            _file.Delete();
                            break;
                        }
                    }

                    //copy file to user specific folders
                    using (var stream=new FileStream(fullPath,FileMode.Create))
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

    }


}
