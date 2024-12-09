using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.Auth
{
    public class Auth
    {
        public bool? status { get; set; }
        public string? token { get; set; }
        public string? msg { get; set; }
    }
}
