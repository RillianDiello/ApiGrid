using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Wrappers;

namespace WebApplication1.Services
{
    public class FileServices
    {
        private readonly SampleServices sampleServices;
        private readonly _DBContext _context;

        public FileServices(SampleServices sampleService, _DBContext context)
        {
            sampleServices = sampleService;
            _context = context;
        }
       
        public async Task<PagedResponse<IEnumerable<FileUp>>> GetFilesUp(int page = 1, int per_page = 1)
        {
            var validFilter = new PaginationFilter(page, per_page);
            var pagedData = await _context.FilesUp.Skip((page - 1) * per_page).Take(per_page).ToListAsync();
            var totalPages = await _context.FilesUp.CountAsync() / validFilter.PageSize;
            return new PagedResponse<IEnumerable<FileUp>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalPages);
        }

        //public async Task<bool> SaveFile()

        public async Task<bool> CreateFile(string fileName, string infoName)
        {

            FileUp file = new FileUp
            {
                DateUp = DateTime.Now,
                Filename = infoName,
            };
            _context.FilesUp.Add(file);
            _context.SaveChanges();

            List<Sample> samples = new List<Sample>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    bool primeiraLinha = true;
                    do
                    {
                        string s = reader.ReadLine();
                        if (primeiraLinha)
                        {
                            primeiraLinha = false;
                            continue;
                        }
                        else
                        {
                            string[] arrData = s.Split(',');
                            samples.Add(new Sample()
                            {
                                Date = Convert.ToDateTime(arrData[0].ToString()),
                                Value = Convert.ToDouble(arrData[1], System.Globalization.CultureInfo.GetCultureInfo("en-US")),
                                File = file,
                                FileId = file.Id
                            });
                        }
                    } while (!reader.EndOfStream);
                }
            }

            sampleServices.SaveSamplesList(samples);
            return true;
        }

         public async Task<FileUp> GetFileUpId(int id)
         {
             var fileUp = await _context.FilesUp.FindAsync(id);

             if (fileUp == null)
             {
                 return null;
             }

             return fileUp;
         }
    }
}
