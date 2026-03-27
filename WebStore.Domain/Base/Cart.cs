using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public int ItemCount(int ItemID) => Items.Sum(item => item.Quantity);
        public void Add(int ProductID)
        {
            var item = Items.FirstOrDefault(item => item.ItemID == ProductID);
            if (item is null) Items.Add(new CartItem { ItemID = ProductID});
            else item.Quantity++;
        }
        public void Decriment(int ProductID)
        {
            var item = Items.FirstOrDefault(item => item.ItemID == ProductID);
            if (item is null) return;
            if (item.Quantity == 1) Items.Remove(item);
            else item.Quantity--;
        }
        public void Remove(int ProductID)
        {
            var item = Items.FirstOrDefault(item => item.ItemID == ProductID);
            if (!(item is null))
                Items.Remove(item);
        }
        public void Clear() => Items.Clear();
    }
    public class CartItem
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
