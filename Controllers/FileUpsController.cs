#nullable disable
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Wrappers;

namespace WebApplication1.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]   
    public class FileUpsController : ControllerBase
    {

        private readonly FileServices fileServices;

        public FileUpsController(FileServices fileService)
        {
            fileServices = fileService;
        }


        // GET: api/FileUps
        [HttpGet]
        // [Route("")]
        public async Task<PagedResponse<IEnumerable<FileUp>>> GetFilesUp( int page = 1, int per_page = 1)
        {
            var data = await fileServices.GetFilesUp(page, per_page);
            return data;
        }

        // GET: api/FileUps/5
        [HttpGet("{id}")]
        //// [Route("")]
        public async Task<ActionResult<FileUp>> GetFileUp(int id, [FromServices] _DBContext context)
        {
            var fileUp = await context.FilesUp.FindAsync(id);

            if (fileUp == null)
            {
                return NotFound();
            }
            return fileUp;
        }

        // DELETE: api/FileUps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileUp(int id, [FromServices] _DBContext context)
        {
            var fileUp = await context.FilesUp.FindAsync(id);
            if (fileUp == null)
            {
                return NotFound();
            }

            context.FilesUp.Remove(fileUp);
            await context.SaveChangesAsync();

            return Ok();
        }
             

        //Upload file
        [HttpPost("")]
        public async Task<IActionResult> Upload(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment, [FromServices] _DBContext context)
        {
            string fileName = $"{hostingEnvironment.ContentRootPath}\\temp\\{file.FileName}"; 
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            await fileServices.CreateFile(fileName, file.FileName);

            return Ok();
        }
    }
}
