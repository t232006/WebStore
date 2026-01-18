using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }


}
