using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiProject.Helpers
{
    public class QueryObject
    {
        public string? Service { get; set; } = null;
        public string? Username {get; set; } = null;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}