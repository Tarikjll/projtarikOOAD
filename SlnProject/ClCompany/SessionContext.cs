using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClCompany
{
    public static class SessionContext
    {
        public static bool IsAdmin { get; set; }
        public static Company? CurrentCompany { get; set; }

    }
}
