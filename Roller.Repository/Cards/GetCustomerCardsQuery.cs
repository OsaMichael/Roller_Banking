using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Cards
{
   public class GetCustomerCardsQuery
    {
        public List<CardDto> cardDtos { get; set; }
        public int CustomerId { get; set; }
    }
}
