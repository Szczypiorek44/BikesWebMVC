﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2_podejscie_2.ViewModels
{
    public class RandomBikeViewModel
    {
        public Bike Bike { get; set; }
        public List<Customer> Customers { get; set; }
    }
}