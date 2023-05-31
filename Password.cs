using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPad
{
    public class Password
    {
        public string DecodePassword { get; set; }

        public Password()
        {

        }

        public Password(string password)
        {
            DecodePassword = password; 
        }
    }
}
