using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Services
{
   public class CyberpayResponse
    {
        public string code { get; set; }
        public bool Succeeded { get; set; }
        public Data Data { get; set; }
    }
}
