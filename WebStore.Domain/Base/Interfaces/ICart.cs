using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base.Interfaces
{
    public interface ICart
    {
        public void Add(int ProductID);
        public void Decriment(int ProductID);
        public void Remove(int ProductID);
        public void Clear(); 
        public
    }
}
