using Microsoft.Build.Tasks.Deployment.Bootstrapper;
//using System.Drawing.Drawing2D;
using WebStore.Models;
//using static System.Collections.Specialized.BitVector32;
using WebStore.Domain.Base;
using Product = WebStore.Domain.Base.Product;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> _employees = new List<Employee>
        {
            new() { ID = 1, Name = "Alice", Position = "Developer" , DateOfBirth=new DateTime(2002,12,12) },
            new() { ID = 2, Name = "Bob", Position = "Designer" , DateOfBirth=new DateTime(2001,11,11) },
            new() { ID = 3, Name = "Charlie", Position = "Manager", DateOfBirth=new DateTime(2000,10,10) }
        };

        public static IEnumerable<Section> Sections { get; } = new Section[]
    {
          new() { ID = 01, Name = "Спорт", Order = 0 },
          new() { ID = 02, Name = "Nike", Order = 0, ParentID = 1 },
          new() { ID = 03, Name = "Under Armour", Order = 1, ParentID = 1 },
          new() { ID = 04, Name = "Adidas", Order = 2, ParentID = 1 },
          new() { ID = 05, Name = "Puma", Order = 3, ParentID = 1 },
          new() { ID = 06, Name = "ASICS", Order = 4, ParentID = 1 },
          new() { ID = 07, Name = "Для мужчин", Order = 1 },
          new() { ID = 08, Name = "Fendi", Order = 0, ParentID = 7 },
          new() { ID = 09, Name = "Guess", Order = 1, ParentID = 7 },
          new() { ID = 10, Name = "Valentino", Order = 2, ParentID = 7 },
          new() { ID = 11, Name = "Диор", Order = 3, ParentID = 7 },
          new() { ID = 12, Name = "Версачи", Order = 4, ParentID = 7 },
          new() { ID = 13, Name = "Армани", Order = 5, ParentID = 7 },
          new() { ID = 14, Name = "Prada", Order = 6, ParentID = 7 },
          new() { ID = 15, Name = "Дольче и Габбана", Order = 7, ParentID = 7 },
          new() { ID = 16, Name = "Шанель", Order = 8, ParentID = 7 },
          new() { ID = 17, Name = "Гуччи", Order = 9, ParentID = 7 },
          new() { ID = 18, Name = "Для женщин", Order = 2 },
          new() { ID = 19, Name = "Fendi", Order = 0, ParentID = 18 },
          new() { ID = 20, Name = "Guess", Order = 1, ParentID = 18 },
          new() { ID = 21, Name = "Valentino", Order = 2, ParentID = 18 },
          new() { ID = 22, Name = "Dior", Order = 3, ParentID = 18 },
          new() { ID = 23, Name = "Versace", Order = 4, ParentID = 18 },
          new() { ID = 24, Name = "Для детей", Order = 3 },
          new() { ID = 25, Name = "Мода", Order = 4 },
          new() { ID = 26, Name = "Для дома", Order = 5 },
          new() { ID = 27, Name = "Интерьер", Order = 6 },
          new() { ID = 28, Name = "Одежда", Order = 7 },
          new() { ID = 29, Name = "Сумки", Order = 8 },
          new() { ID = 30, Name = "Обувь", Order = 9 },
    };

        /// <summary>Бренды</summary>
        public static IEnumerable<Brand> Brands { get; } = new Brand[]
        {
        new() { ID = 1, Name = "Acne", Order = 0 },
        new() { ID = 2, Name = "Grune Erde", Order = 1 },
        new() { ID = 3, Name = "Albiro", Order = 2 },
        new() { ID = 4, Name = "Ronhill", Order = 3 },
        new() { ID = 5, Name = "Oddmolly", Order = 4 },
        new() { ID = 6, Name = "Boudestijn", Order = 5 },
        new() { ID = 7, Name = "Rosch creative culture", Order = 6 },
        };

        public static IEnumerable<Product> Products { get; } = new Product[]
        {
        new() { ID = 1, Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
        new() { ID = 2, Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
        new() { ID = 3, Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
        new() { ID = 4, Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
        new() { ID = 5, Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
        new() { ID = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
        new() { ID = 7, Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
        new() { ID = 8, Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
        new() { ID = 9, Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
        new() { ID = 10, Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
        new() { ID = 11, Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
        new() { ID = 12, Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
        };
    
        public static IEnumerable<Blog> Blogs { get; } = new Blog[]
    {
        new() {ID=1, Name = "Girls Pink T Shirt arrived in store",
                PictureUrl="blog-one.jpg",
                Author = "Piter Pen",
                BrandID = 4,
                Block = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                publicDate = new DateTime(2013, 10, 23, 15, 29, 51),
        },
        new() {ID=2, Name = "New brands are outsiders in quality",
                Author = "Janna Sid",
                PictureUrl="blog-three.jpg",
                Block = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                publicDate = new DateTime(2013, 10, 24, 15, 29, 51),
        },
        new() {ID=3, Name = "Whild style coming to the release",
                Author = "Piter Pen",
                PictureUrl="blog-two.jpg",
                BrandID=4,
                SectionID=16,
                Block = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                publicDate = new DateTime(2013, 10, 25, 15, 29, 51),
        },


    };
    };

}
