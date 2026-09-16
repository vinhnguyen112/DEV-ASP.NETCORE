namespace Lap04.Models
{
    public class DataLocal
    {
        public static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện tử" },
            new Category { Id = 2, Name = "Thời trang" },
            new Category { Id = 3, Name = "Thực phẩm" },
            new Category { Id = 4, Name = "Đồ gia dụng" },
            new Category { Id = 5, Name = "Sách & Văn phòng phẩm" },
        };

        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(int id) =>
            _categories.FirstOrDefault(c => c.Id == id);

        public static List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Price = 33990000,
                SalePrice = 29990000,
                Status = 1,
                CreatedDate = new DateTime(2024, 1, 15),
                Image = "/images/products/iphone15.png",
                CategoryId = 1,
                Description = "Điện thoại iPhone 15 Pro Max 256GB, chip A17 Pro."
            },
            new Product
            {
                Id = 2,
                Name = "Samsung Galaxy S24 Ultra",
                Price = 31990000,
                SalePrice = 27990000,
                Status = 1,
                CreatedDate = new DateTime(2024, 2, 10),
                Image = "/images/products/samsung_s24.png",
                CategoryId = 1,
                Description = "Samsung Galaxy S24 Ultra 512GB, bút S-Pen tích hợp."
            },
            new Product
            {
                Id = 3,
                Name = "Áo Polo Nam Cao Cấp",
                Price = 450000,
                SalePrice = 350000,
                Status = 1,
                CreatedDate = new DateTime(2024, 3, 5),
                Image = "/images/products/polo_shirt.png",
                CategoryId = 2,
                Description = "Áo Polo nam chất liệu cotton cao cấp, nhiều màu sắc."
            },
            new Product
            {
                Id = 4,
                Name = "Cà Phê Arabica Đà Lạt",
                Price = 120000,
                SalePrice = 99000,
                Status = 1,
                CreatedDate = new DateTime(2024, 3, 20),
                Image = "/images/products/coffee.png",
                CategoryId = 3,
                Description = "Cà phê Arabica nguyên chất từ Đà Lạt, rang xay tươi 500g."
            },
            new Product
            {
                Id = 5,
                Name = "Nồi Cơm Điện Toshiba",
                Price = 1290000,
                SalePrice = 990000,
                Status = 0,
                CreatedDate = new DateTime(2024, 4, 1),
                Image = "/images/products/rice_cooker.png",
                CategoryId = 4,
                Description = "Nồi cơm điện Toshiba 1.8L, lòng nồi phủ chống dính."
            },
        };

        public static List<Product> GetProducts() => _products;

        public static Product? GetProductById(int id) =>
            _products.FirstOrDefault(p => p.Id == id);

        public static List<People> _people = new List<People>
        {
            new People
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                Phone = "123-456-7890",
                Address = "123 Main St, Anytown, USA",
                Avatar = "/images/avatar/06.png",
                Brithday = new DateTime(1990, 1, 1),
                Bio = "Software developer with a passion for open source.",
                Gender = 1
            },

            new People
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Phone = "987-654-3210",
                Address = "456 Elm St, Othertown, USA",
                Avatar = "/images/avatar/07.png",
                Brithday = new DateTime(1992, 2, 2),
                Bio = "Graphic designer with a love for typography.",
                Gender = 2
            },

            new People
            {
                Id = 3,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Phone = "987-654-3210",
                Address = "456 Elm St, Othertown, USA",
                Avatar = "/images/avatar/08.png",
                Brithday = new DateTime(1992, 2, 2),
                Bio = "Graphic designer with a love for typography.",
                Gender = 2
            },

            new People
            {
                Id = 4,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Phone = "987-654-3210",
                Address = "456 Elm St, Othertown, USA",
                Avatar = "/images/avatar/09.png",
                Brithday = new DateTime(1992, 2, 2),
                Bio = "Graphic designer with a love for typography.",
                Gender = 2
            },

            new People
            {
                Id = 5,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Phone = "987-654-3210",
                Address = "456 Elm St, Othertown, USA",
                Avatar = "/images/avatar/11.png",
                Brithday = new DateTime(1992, 2, 2),
                Bio = "Graphic designer with a love for typography.",
                Gender = 2
            },


            new People
            {
                Id = 6,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Phone = "987-654-3210",
                Address = "456 Elm St, Othertown, USA",
                Avatar = "/images/avatar/12.png",
                Brithday = new DateTime(1992, 2, 2),
                Bio = "Graphic designer with a love for typography.",
                Gender = 2
            },
        };

        public static List<People> GetPeoples()
        {
            return _people;
        }

        public static People? GetPeopleById(int Id)
        {
            var people = _people.FirstOrDefault(x => x.Id == Id);
            return people;
        }


    }

}
