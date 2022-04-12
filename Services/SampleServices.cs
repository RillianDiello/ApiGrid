using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Wrappers;

namespace WebApplication1.Services
{
    public class SampleServices
    {
        private readonly _DBContext _context;

        public SampleServices(_DBContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Sample>>> GetSamplesByFilesUp(int fileId, int page = 1, int per_page = 1)
        {

            var validFilter = new PaginationFilter(page, per_page);
            var pagedData = await _context.Samples.Where(x => x.FileId == fileId).Skip((page - 1) * per_page).Take(per_page).ToListAsync();

            var totalPages = await _context.Samples.Where(x => x.FileId == fileId).CountAsync() / validFilter.PageSize;
            return new PagedResponse<IEnumerable<Sample>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalPages);

            
        }

        public async Task<bool> SaveSamples(Sample sample)
        {

            _context.Samples.Add(sample);
            if (await _context.SaveChangesAsync() != 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Problems to save Sample");
            }

        }

        public async Task<bool> SaveSamplesList(List<Sample> samples)
        {
            if (samples == null)
            {
                throw new ArgumentNullException(nameof(samples));
            }


            _context.AddRange(samples);
            if (await _context.SaveChangesAsync() != 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Problems to save Samplse");
            }
        }

        public async Task<Sample> GetSampleId(int id)
        {
            var fileUp = await _context.Samples.FindAsync(id);

            if (fileUp == null)
            {
                return null;
            }

            return fileUp;
        }

        public async Task<float> MaxValue(int fileId)
        {
            return (float)await _context.Samples.Where(x => x.FileId == fileId).MaxAsync(x => x.Value);

        }

        public async Task<double> MinValue(int fileId)
        {
            return await _context.Samples.Where(x => x.FileId == fileId).MinAsync(x => x.Value);
        }

        
        public async Task<List<Sample>> MostExpensive(int fileId)
        {
            var data= await _context.Samples.Where(x => x.FileId == fileId).ToListAsync();

            double sum = 0;
            var sample = new List<Sample>();
            for (int i = 0; i < data.Count - 1; i++)
            {
                double tempSum = data[i].Value + data[i + 1].Value;
                if(tempSum > sum)
                {
                    sample.Clear();
                    sample.Add(data[i]);
                    sample.Add(data[i+1]);
                    sum = tempSum;
                }
            }

            return sample;
        }


    }
}
