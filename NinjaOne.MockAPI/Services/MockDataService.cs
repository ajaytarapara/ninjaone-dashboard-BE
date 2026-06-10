using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaOne.MockAPI.Services
{
    public class MockDataService
    {
        private readonly IWebHostEnvironment _environment;

        public MockDataService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetJson(string fileName)
        {
            var path = Path.Combine(
                _environment.ContentRootPath,
                "MockData",
                fileName);

            return File.ReadAllText(path);
        }
    }
}