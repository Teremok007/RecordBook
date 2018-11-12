﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordBook.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category{ get; set; }
    }
}