using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base
{
    public class ProductFilter
    {
        public int? BrandID { get; set; }
        public int? SectionID { get; set; }
        public int[] IDs { get; set; } = [];
    }
}
