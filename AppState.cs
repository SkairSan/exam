using exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    public static class AppState
    {
        public static User? CurrentUser { get; set; }

    }
}
