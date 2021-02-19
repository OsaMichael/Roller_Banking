using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roller.Repository.Cards
{
  public  class CardDto
    {
        private string _ccNumber;
        public string Type { get; set; }
        public DateTime Issued { get; set; }
        public string CcType { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }
        public int CardId { get; set; }
        public int CustomerId { get; set; }

        public string CcNumber
        {
            get => _ccNumber.Substring(0, 4) + " **** **** ***" + _ccNumber.Last();
            set => _ccNumber = value;
        }

    }
}
