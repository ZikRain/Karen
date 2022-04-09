using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test.Data.Repositories
{
    public class BaseRepositoryTest
    {
        public readonly IConfiguration _configuration;

        public BaseRepositoryTest()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"D:\Karen\KarenNew\Karen\Karen\Test\testconfig.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
