#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Wrappers;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly SampleServices sampleServices;        

        public SamplesController(SampleServices sampleService)
        {
            sampleServices = sampleService;
        }

        // GET: api/Samples/5        
        [HttpGet("{fileUp}")]
        public async Task<PagedResponse<IEnumerable<Sample>>> GetSamples(int fileUp, int page = 1, int per_page = 1)
        {
            var data = await sampleServices.GetSamplesByFilesUp(fileUp, page, per_page);
            return data;           
        }

        [HttpGet("minSample/{fileUp}")]
        public async Task<double> GetMinSamples(int fileUp)
        {
            return await sampleServices.MinValue(fileUp);
        }

        [HttpGet("maxSample/{fileUp}")]
        public async Task<double> GetMaxSamples(int fileUp)
        {
            return await sampleServices.MaxValue(fileUp);
        }


        [HttpGet("range/{fileUp}")]
        public async Task<List<Sample>> GetRange(int fileUp)
        {
            return await sampleServices.MostExpensive(fileUp);
        }

    }
}
