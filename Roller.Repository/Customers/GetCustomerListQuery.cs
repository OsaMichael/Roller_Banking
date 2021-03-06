﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class GetCustomerListQuery
    {
        public List<CustomerListViewModel> customerLists { get; set; }

        private int _limit = 50;
        public string Name { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }

        public int Limit
        {
            get => _limit;
            set => _limit = value > 50 ? _limit : value;
        }

        public int Offset { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;

    }
}
