using System;
using System.Collections.Generic;
using System.Text;
using WebStore.ViewModels;

namespace WebStore.Domain.Base.Interfaces
{
    public interface ICart
    {
        void Add(int ProductID);
        void Decrement(int ProductID);
        void Remove(int ProductID);
        void Clear();
        CartViewModel GetViewModel();
    }
}
