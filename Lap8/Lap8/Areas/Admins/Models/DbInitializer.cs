using Lesson08.Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap8.Areas.Admins.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // 1. Chuẩn hóa lại 20 danh mục tiếng Việt chuẩn
            var standardCategories = new (string Name, string Description)[]
            {
                ("Điện thoại thông minh", "Các dòng smartphone chính hãng từ Apple, Samsung, Xiaomi..."),
                ("Laptop & Máy tính xách tay", "Laptop học tập, văn phòng, gaming và đồ họa cao cấp."),
                ("Máy tính bảng", "iPad, máy tính bảng Android phục vụ công việc và giải trí."),
                ("Tai nghe & Loa", "Tai nghe true wireless, chụp tai và hệ thống loa bluetooth."),
                ("Đồng hồ thông minh", "Smartwatch theo dõi sức khỏe, thể thao và thời trang."),
                ("Máy ảnh & Phụ kiện", "Máy ảnh Mirrorless, DSLR và ống kính, chân máy chuyên nghiệp."),
                ("Màn hình máy tính", "Màn hình 4K, 2K, màn hình gaming tần số quét cao."),
                ("Bàn phím & Chuột", "Bàn phím cơ, chuột không dây công thái học cao cấp."),
                ("Ổ cứng & Lưu trữ", "SSD NVMe tốc độ cao, ổ cứng di động, thẻ nhớ lưu trữ."),
                ("Nguồn & Pin dự phòng", "Pin sạc dự phòng dung lượng lớn, sạc nhanh GaN."),
                ("Thiết bị mạng", "Router Wi-Fi 6, Mesh Wi-Fi và thiết bị mạng doanh nghiệp."),
                ("Smart TV & Màn hình lớn", "Tivi OLED, QLED kích thước lớn với độ phân giải siêu nét."),
                ("Máy in & Thiết bị văn phòng", "Máy in laser, in màu đa chức năng cho văn phòng."),
                ("Phụ kiện điện thoại", "Ốp lưng, kính cường lực, cáp sạc nhanh cao cấp."),
                ("Gaming & Thiết bị game", "Tay cầm chơi game, ghế gaming, thiết bị stream chuyên nghiệp."),
                ("Đèn thông minh & Chiếu sáng", "Hệ thống đèn LED RGB thông minh điều khiển qua app."),
                ("Loa thông minh & Smart Home", "Trợ lý ảo, chuông cửa thông minh và cảm biến an ninh."),
                ("Phần mềm & Bản quyền", "Bản quyền Windows, Office 365 và phần mềm đồ họa."),
                ("Linh kiện & Bo mạch máy tính", "CPU, Card đồ họa VGA, Mainboard, RAM PC chính hãng."),
                ("Thiết bị đeo & Sức khỏe", "Vòng đeo tay thông minh theo dõi nhịp tim và giấc ngủ.")
            };

            var existingCategories = context.Categories.ToList();
            if (!existingCategories.Any())
            {
                foreach (var item in standardCategories)
                {
                    context.Categories.Add(new Category
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Status = 1,
                        CreatedDate = DateTime.Now
                    });
                }
                context.SaveChanges();
                existingCategories = context.Categories.ToList();
            }
            else
            {
                // Cập nhật lại tên tiếng Việt có dấu chuẩn cho các danh mục hiện tại nếu bị lỗi font
                for (int i = 0; i < existingCategories.Count && i < standardCategories.Length; i++)
                {
                    existingCategories[i].Name = standardCategories[i].Name;
                    existingCategories[i].Description = standardCategories[i].Description;
                }
                context.SaveChanges();
            }

            var catIds = existingCategories.Select(c => c.Id).ToList();

            // 2. Seed 20 Products
            if (!context.Products.Any())
            {
                var sampleProducts = new[]
                {
                    new Product { Name = "iPhone 15 Pro Max 256GB", Price = 34990000, SalePrice = 29990000, Status = 1, Description = "Khung titan bền nhẹ, chip Apple A17 Pro mạnh mẽ, camera tiềm vọng 5x.", CategoryId = catIds[0], CreatedDate = DateTime.Now.AddDays(-19) },
                    new Product { Name = "Samsung Galaxy S24 Ultra 512GB", Price = 33990000, SalePrice = 28490000, Status = 1, Description = "Trang bị bút S-Pen, vi xử lý Snapdragon 8 Gen 3 và bộ tính năng Galaxy AI thông minh.", CategoryId = catIds[0], CreatedDate = DateTime.Now.AddDays(-18) },
                    new Product { Name = "MacBook Pro 14 M3 Pro (18GB/512GB)", Price = 49990000, SalePrice = 45990000, Status = 1, Description = "Màn hình Liquid Retina XDR sắc nét, chip M3 Pro xử lý đồ họa và render đỉnh cao.", CategoryId = catIds[1], CreatedDate = DateTime.Now.AddDays(-17) },
                    new Product { Name = "Laptop ASUS ROG Zephyrus G14 OLED", Price = 42990000, SalePrice = 39990000, Status = 1, Description = "Màn hình ROG Nebula OLED 3K 120Hz, Ryzen 9 và RTX 4070 mỏng nhẹ sang trọng.", CategoryId = catIds[1], CreatedDate = DateTime.Now.AddDays(-16) },
                    new Product { Name = "iPad Pro 11 inch M4 Wi-Fi 256GB", Price = 28990000, SalePrice = 26490000, Status = 1, Description = "Siêu mỏng nhẹ kỷ lục, màn hình Ultra Retina XDR Tandem OLED tiên tiến.", CategoryId = catIds[2], CreatedDate = DateTime.Now.AddDays(-15) },
                    new Product { Name = "Tai nghe chống ồn Sony WH-1000XM5", Price = 8490000, SalePrice = 6990000, Status = 1, Description = "Công nghệ chống ồn chủ động đỉnh cao, thời lượng pin ấn tượng lên tới 30 giờ.", CategoryId = catIds[3], CreatedDate = DateTime.Now.AddDays(-14) },
                    new Product { Name = "Loa Bluetooth Marshall Stanmore III", Price = 9990000, SalePrice = 8790000, Status = 1, Description = "Âm thanh trường rộng sống động, phong cách thiết kế vintage cổ điển đậm chất rock.", CategoryId = catIds[3], CreatedDate = DateTime.Now.AddDays(-13) },
                    new Product { Name = "Đồng hồ Apple Watch Ultra 2 GPS + Cellular", Price = 21990000, SalePrice = 19490000, Status = 1, Description = "Mặt kính Sapphire, vỏ titan siêu bền bỉ, độ sáng 3000 nits dành cho thể thao mạo hiểm.", CategoryId = catIds[4], CreatedDate = DateTime.Now.AddDays(-12) },
                    new Product { Name = "Máy ảnh Mirrorless Sony Alpha A7 IV", Price = 59990000, SalePrice = 52990000, Status = 1, Description = "Cảm biến Full-frame 33MP, hệ thống lấy nét AI bắt nét mắt người và động vật siêu nhanh.", CategoryId = catIds[5], CreatedDate = DateTime.Now.AddDays(-11) },
                    new Product { Name = "Màn hình đồ họa Dell UltraSharp 27 4K U2723QE", Price = 14500000, SalePrice = 12900000, Status = 1, Description = "Tấm nền IPS Black độ tương phản 2000:1, chuẩn màu 98% DCI-P3 chuyên nghiệp.", CategoryId = catIds[6], CreatedDate = DateTime.Now.AddDays(-10) },
                    new Product { Name = "Bàn phím cơ không dây Keychron Q1 Pro", Price = 4850000, SalePrice = 4250000, Status = 1, Description = "Khung nhôm nguyên khối CNC, kết nối không dây đa thiết bị, gõ êm mượt mà.", CategoryId = catIds[7], CreatedDate = DateTime.Now.AddDays(-9) },
                    new Product { Name = "Chuột công thái học Logitech MX Master 3S", Price = 2490000, SalePrice = 1990000, Status = 1, Description = "Cảm biến 8000 DPI hoạt động trên mọi bề mặt, nút cuộn siêu tốc MagSpeed cực êm.", CategoryId = catIds[7], CreatedDate = DateTime.Now.AddDays(-8) },
                    new Product { Name = "Ổ cứng SSD Samsung 990 Pro 2TB PCIe 4.0", Price = 5200000, SalePrice = 4590000, Status = 1, Description = "Tốc độ đọc tuần tự lên đến 7450 MB/s, độ bền TBW cao tối ưu cho game thủ.", CategoryId = catIds[8], CreatedDate = DateTime.Now.AddDays(-7) },
                    new Product { Name = "Pin sạc dự phòng Anker 737 PowerCore 24000mAh", Price = 2990000, SalePrice = 2490000, Status = 1, Description = "Công suất sạc 140W chuẩn PD 3.1, có màn hình hiển thị thông minh thông số sạc.", CategoryId = catIds[9], CreatedDate = DateTime.Now.AddDays(-6) },
                    new Product { Name = "Router Wi-Fi 6 ASUS RT-AX88U Pro Dual Band", Price = 6990000, SalePrice = 5990000, Status = 1, Description = "Tốc độ mạng cực nhanh 6000 Mbps, hỗ trợ cổng kép 2.5G cho kết nối ổn định.", CategoryId = catIds[10], CreatedDate = DateTime.Now.AddDays(-5) },
                    new Product { Name = "Smart TV LG OLED 65 inch evo 4K OLED65C3PSA", Price = 42900000, SalePrice = 36900000, Status = 1, Description = "Bộ xử lý AI α9 Gen6 4K, công nghệ Brightness Booster gia tăng độ sáng rực rỡ.", CategoryId = catIds[11], CreatedDate = DateTime.Now.AddDays(-4) },
                    new Product { Name = "Máy in đa năng HP LaserJet Pro MFP M227fdw", Price = 7890000, SalePrice = 6990000, Status = 1, Description = "In hai mặt tự động, kết nối Wi-Fi, fax và photocopy tốc độ cao cho văn phòng.", CategoryId = catIds[12], CreatedDate = DateTime.Now.AddDays(-3) },
                    new Product { Name = "Ốp lưng chống sốc UAG Monarch Pro iPhone 15", Price = 1750000, SalePrice = 1450000, Status = 1, Description = "Tiêu chuẩn quân đội 5 lớp bảo vệ, hỗ trợ sạc hít nam châm MagSafe siêu bền bỉ.", CategoryId = catIds[13], CreatedDate = DateTime.Now.AddDays(-2) },
                    new Product { Name = "Ghế công thái học Ergonomic Sihoo Doro C300", Price = 7500000, SalePrice = 6200000, Status = 1, Description = "Đệm lưng tự thích ứng, tựa đầu 3D linh hoạt, đệm lưới thoáng khí chống mỏi lưng.", CategoryId = catIds[14], CreatedDate = DateTime.Now.AddDays(-1) },
                    new Product { Name = "Vòng đeo tay thông minh Xiaomi Smart Band 8 Pro", Price = 1790000, SalePrice = 1490000, Status = 1, Description = "Màn hình AMOLED lớn 1.74 inch, tích hợp GPS độc lập và pin dùng lên tới 14 ngày.", CategoryId = catIds[19], CreatedDate = DateTime.Now }
                };

                context.Products.AddRange(sampleProducts);
                context.SaveChanges();
            }

            // 3. Seed 20 Banners
            if (!context.Banners.Any())
            {
                var sampleBanners = new[]
                {
                    new Banner { Name = "Siêu sale công nghệ - Giảm tới 50%", Prioty = 1, Status = 1, Description = "Khuyến mãi lớn nhất tháng áp dụng cho các dòng điện thoại và phụ kiện chính hãng.", CreatedDate = DateTime.Now.AddDays(-19) },
                    new Banner { Name = "Mừng năm mới - Ưu đãi ngập tràn", Prioty = 2, Status = 1, Description = "Hàng ngàn voucher lì xì đầu năm trị giá lên tới 2 triệu đồng.", CreatedDate = DateTime.Now.AddDays(-18) },
                    new Banner { Name = "Ra mắt flagship iPhone 15 Series", Prioty = 3, Status = 1, Description = "Đặt trước ngay để nhận bộ quà tặng trị giá 3 triệu và trả góp 0% lãi suất.", CreatedDate = DateTime.Now.AddDays(-17) },
                    new Banner { Name = "Đại tiệc âm thanh Sony & Marshall", Prioty = 4, Status = 1, Description = "Ưu đãi 25% cho tất cả các dòng loa bluetooth và tai nghe chống ồn đỉnh cao.", CreatedDate = DateTime.Now.AddDays(-16) },
                    new Banner { Name = "Back to School - Giảm 20% Laptop sinh viên", Prioty = 5, Status = 1, Description = "Tặng kèm balo chống sốc, chuột không dây cho học sinh sinh viên.", CreatedDate = DateTime.Now.AddDays(-15) },
                    new Banner { Name = "Flash Sale hàng ngày khung giờ vàng 12H - 14H", Prioty = 6, Status = 1, Description = "Săn deal chớp nhoáng với mức giá giảm sập sàn mỗi ngày.", CreatedDate = DateTime.Now.AddDays(-14) },
                    new Banner { Name = "Miễn phí vận chuyển toàn quốc đơn từ 500k", Prioty = 7, Status = 1, Description = "Giao hàng hỏa tốc trong 2 giờ tại nội thành Hà Nội và TP.HCM.", CreatedDate = DateTime.Now.AddDays(-13) },
                    new Banner { Name = "Rạp chiếu tại gia cùng Smart TV LG OLED", Prioty = 8, Status = 1, Description = "Tặng gói xem phim bản quyền 1 năm khi mua tivi từ 55 inch trở lên.", CreatedDate = DateTime.Now.AddDays(-12) },
                    new Banner { Name = "Gaming Week - Đỉnh cao chiến game", Prioty = 9, Status = 1, Description = "Combo máy tính chơi game và bàn phím cơ giảm ngay 15% kèm quà tặng.", CreatedDate = DateTime.Now.AddDays(-11) },
                    new Banner { Name = "Tuần lễ phụ kiện Anker chính hãng", Prioty = 10, Status = 1, Description = "Củ sạc nhanh GaN, cáp bọc dù siêu bền đồng giá chỉ từ 199.000đ.", CreatedDate = DateTime.Now.AddDays(-10) },
                    new Banner { Name = "Ưu đãi quét mã VNPay giảm thêm 200k", Prioty = 11, Status = 1, Description = "Nhập mã VNPAYTECH khi thanh toán để nhận thêm ưu đãi hấp dẫn.", CreatedDate = DateTime.Now.AddDays(-9) },
                    new Banner { Name = "Chương trình thu cũ đổi mới trợ giá 3 triệu", Prioty = 12, Status = 1, Description = "Định giá máy cũ nhanh chóng, hỗ trợ lên đời điện thoại mới tiết kiệm.", CreatedDate = DateTime.Now.AddDays(-8) },
                    new Banner { Name = "Bộ sưu tập Apple Watch chính hãng", Prioty = 13, Status = 1, Description = "Theo dõi sức khỏe toàn diện cùng nhiều lựa chọn dây đeo thời trang.", CreatedDate = DateTime.Now.AddDays(-7) },
                    new Banner { Name = "Nhiếp ảnh chuyên nghiệp cùng Sony Alpha", Prioty = 14, Status = 1, Description = "Trải nghiệm chụp ảnh sắc nét, tặng thẻ nhớ tốc độ cao 128GB.", CreatedDate = DateTime.Now.AddDays(-6) },
                    new Banner { Name = "Bảo hành 24 tháng mọi linh kiện PC", Prioty = 15, Status = 1, Description = "Cam kết 1 đổi 1 trong vòng 30 ngày đầu tiên nếu phát sinh lỗi phần cứng.", CreatedDate = DateTime.Now.AddDays(-5) },
                    new Banner { Name = "Góc làm việc công thái học chuẩn Ergonomic", Prioty = 16, Status = 1, Description = "Bàn nâng hạ thông minh và ghế công thái học bảo vệ cột sống tối đa.", CreatedDate = DateTime.Now.AddDays(-4) },
                    new Banner { Name = "Lễ hội âm nhạc mùa hè cùng loa Bluetooth", Prioty = 17, Status = 1, Description = "Loa chống nước chuẩn IP67 sẵn sàng cho mọi bữa tiệc hồ bơi và dã ngoại.", CreatedDate = DateTime.Now.AddDays(-3) },
                    new Banner { Name = "Giải pháp nhà thông minh Smart Home an toàn", Prioty = 18, Status = 1, Description = "Khóa cửa vân tay, camera an ninh AI giám sát 24/7 từ xa tiện lợi.", CreatedDate = DateTime.Now.AddDays(-2) },
                    new Banner { Name = "Xả kho đón Tết - Giá chạm đáy", Prioty = 19, Status = 1, Description = "Thanh lý hàng trưng bày chính hãng giá hời bảo hành đầy đủ.", CreatedDate = DateTime.Now.AddDays(-1) },
                    new Banner { Name = "Tri ân khách hàng thân thiết - Quà tặng VIP", Prioty = 20, Status = 1, Description = "Tích điểm đổi quà tặng công nghệ cao cấp cho hội viên Kim Cương.", CreatedDate = DateTime.Now }
                };

                context.Banners.AddRange(sampleBanners);
                context.SaveChanges();
            }

            // 4. Seed 20 Blogs
            if (!context.Blogs.Any())
            {
                var sampleBlogs = new[]
                {
                    new Blog { Name = "Đánh giá chi tiết iPhone 15 Pro Max sau 6 tháng sử dụng", ViewCount = 15420, Status = 1, Description = "Chia sẻ thực tế về thời lượng pin, độ bền khung titan và chất lượng camera tiềm vọng 5x.", CreatedDate = DateTime.Now.AddDays(-19) },
                    new Blog { Name = "Top 5 laptop đồ họa và lập trình đáng mua nhất năm nay", ViewCount = 12300, Status = 1, Description = "Tổng hợp những mẫu máy tính xách tay cấu hình mạnh mẽ, màn hình chuẩn màu và tản nhiệt tốt.", CreatedDate = DateTime.Now.AddDays(-18) },
                    new Blog { Name = "Hướng dẫn chọn bàn phím cơ phù hợp cho người mới bắt đầu", ViewCount = 9850, Status = 1, Description = "Tìm hiểu về các loại switch linear, tactile, clicky và layout bàn phím thông dụng nhất hiện nay.", CreatedDate = DateTime.Now.AddDays(-17) },
                    new Blog { Name = "So sánh tai nghe Sony WH-1000XM5 và Bose QuietComfort Ultra", ViewCount = 8900, Status = 1, Description = "Đâu là chiếc tai nghe chống ồn chủ động tốt nhất hiện nay cho người thường xuyên di chuyển?", CreatedDate = DateTime.Now.AddDays(-16) },
                    new Blog { Name = "Cách tối ưu hóa hiệu năng máy tính chơi game mượt mà không bị giật lag", ViewCount = 11200, Status = 1, Description = "Các bước tinh chỉnh Windows, cập nhật driver card đồ họa và dọn dẹp file rác hiệu quả.", CreatedDate = DateTime.Now.AddDays(-15) },
                    new Blog { Name = "Tổng hợp mẹo bảo quản pin điện thoại kéo dài tuổi thọ gấp đôi", ViewCount = 14500, Status = 1, Description = "Quy tắc sạc 20-80%, tránh nhiệt độ cao và những thói quen sai lầm cần từ bỏ ngay.", CreatedDate = DateTime.Now.AddDays(-14) },
                    new Blog { Name = "Xu hướng nhà thông minh Smart Home có gì mới?", ViewCount = 6700, Status = 1, Description = "Chuẩn kết nối Matter lên ngôi, mang lại khả năng tương thích tuyệt vời giữa các hệ sinh thái.", CreatedDate = DateTime.Now.AddDays(-13) },
                    new Blog { Name = "Cách thiết lập góc làm việc công thái học chống đau lưng và mỏi cổ", ViewCount = 13200, Status = 1, Description = "Chiều cao bàn ghế chuẩn, khoảng cách màn hình và tầm quan trọng của giá đỡ nâng hạ.", CreatedDate = DateTime.Now.AddDays(-12) },
                    new Blog { Name = "Phân biệt SSD NVMe Gen 4 và Gen 5: Có thực sự đáng để nâng cấp?", ViewCount = 7600, Status = 1, Description = "Đánh giá sự khác biệt về tốc độ truyền tải thực tế và vấn đề nhiệt độ hoạt động của ổ cứng.", CreatedDate = DateTime.Now.AddDays(-11) },
                    new Blog { Name = "Đánh giá màn hình Dell UltraSharp 4K cho dân thiết kế đồ họa", ViewCount = 10400, Status = 1, Description = "Màu sắc trung thực, khả năng kết nối cổng Type-C tiện lợi và chế độ bảo vệ mắt hiệu quả.", CreatedDate = DateTime.Now.AddDays(-10) },
                    new Blog { Name = "Top 10 ứng dụng không thể thiếu trên máy tính bảng iPad cho học tập", ViewCount = 15800, Status = 1, Description = "Khám phá các app ghi chú GoodNotes, vẽ Procreate và quản lý thời gian hiệu quả hàng đầu.", CreatedDate = DateTime.Now.AddDays(-9) },
                    new Blog { Name = "Tại sao router Wi-Fi 6 lại cần thiết cho hộ gia đình hiện đại?", ViewCount = 5900, Status = 1, Description = "Khả năng chịu tải nhiều thiết bị cùng lúc, độ trễ thấp khi xem video 4K và chơi game online.", CreatedDate = DateTime.Now.AddDays(-8) },
                    new Blog { Name = "Hướng dẫn chụp ảnh phong cảnh đẹp bằng máy ảnh Mirrorless", ViewCount = 8300, Status = 1, Description = "Bí quyết canh góc, kỹ thuật phơi sáng và cách tận dụng ánh sáng giờ vàng trong nhiếp ảnh.", CreatedDate = DateTime.Now.AddDays(-7) },
                    new Blog { Name = "Những phụ kiện du lịch công nghệ không thể thiếu trong balo", ViewCount = 9200, Status = 1, Description = "Pin sạc dự phòng, củ sạc đa cổng quốc tế, túi chống nước và tai nghe nhỏ gọn.", CreatedDate = DateTime.Now.AddDays(-6) },
                    new Blog { Name = "Bảo mật tài khoản trực tuyến: Những quy tắc vàng cần ghi nhớ", ViewCount = 11900, Status = 1, Description = "Kích hoạt xác thực hai yếu tố (2FA), dùng trình quản lý mật khẩu và cảnh giác lừa đảo.", CreatedDate = DateTime.Now.AddDays(-5) },
                    new Blog { Name = "Tìm hiểu công nghệ màn hình OLED trên tivi thế hệ mới", ViewCount = 6400, Status = 1, Description = "Điểm ảnh tự phát sáng, độ tương phản vô cực và màu đen sâu thẳm mang lại trải nghiệm điện ảnh.", CreatedDate = DateTime.Now.AddDays(-4) },
                    new Blog { Name = "So sánh chuột Logitech MX Master 3S và chuột công thái học dọc", ViewCount = 7800, Status = 1, Description = "Trải nghiệm cổ tay sau nhiều giờ làm việc văn phòng, ưu và nhược điểm của từng dòng chuột.", CreatedDate = DateTime.Now.AddDays(-3) },
                    new Blog { Name = "Kinh nghiệm chọn máy in gia đình tiết kiệm mực và bền bỉ", ViewCount = 5100, Status = 1, Description = "So sánh máy in phun liên tục và máy in laser trắng đen về chi phí in ấn trên từng trang.", CreatedDate = DateTime.Now.AddDays(-2) },
                    new Blog { Name = "Đánh giá thời lượng pin trên các dòng đồng hồ thông minh hiện nay", ViewCount = 8700, Status = 1, Description = "Từ Apple Watch sạc mỗi ngày đến Garmin dùng 2 tuần: đâu là lựa chọn phù hợp với bạn?", CreatedDate = DateTime.Now.AddDays(-1) },
                    new Blog { Name = "Cách chọn nguồn máy tính PSU chuẩn 80 Plus an toàn cho PC", ViewCount = 9400, Status = 1, Description = "Cách tính công suất nguồn cần thiết cho CPU, card đồ họa và phân biệt các chuẩn Bronze, Gold.", CreatedDate = DateTime.Now }
                };

                context.Blogs.AddRange(sampleBlogs);
                context.SaveChanges();
            }

            // 5. Seed 20 Accounts (Quản trị viên)
            if (!context.Accounts.Any())
            {
                var sampleAccounts = new[]
                {
                    new Account { Name = "Nguyễn Văn An", Email = "an.nguyen@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Trần Thị Bích", Email = "bich.tran@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Lê Hoàng Cường", Email = "cuong.le@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Phạm Minh Đức", Email = "duc.pham@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Vũ Thanh Hằng", Email = "hang.vu@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Đặng Quốc Huy", Email = "huy.dang@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Bùi Thu Hà", Email = "ha.bui@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Hoàng Đình Khôi", Email = "khoi.hoang@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Ngô Bảo Long", Email = "long.ngo@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Dương Mỹ Linh", Email = "linh.duong@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Đỗ Tuấn Minh", Email = "minh.do@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Trịnh Kim Ngân", Email = "ngan.trinh@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Phan Hữu Nghĩa", Email = "nghia.phan@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Võ Thảo Phương", Email = "phuong.vo@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Lý Nhật Quang", Email = "quang.ly@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Trương Tấn Sang", Email = "sang.truong@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Đinh Ngọc Sơn", Email = "son.dinh@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Hồ Khánh Vy", Email = "vy.ho@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Cao Văn Vũ", Email = "vu.cao@admin.com", Password = "adminPassword123" },
                    new Account { Name = "Tô Hồng Yến", Email = "yen.to@admin.com", Password = "adminPassword123" }
                };

                context.Accounts.AddRange(sampleAccounts);
                context.SaveChanges();
            }

            // 6. Seed 20 Customers
            if (!context.Customers.Any())
            {
                var sampleCustomers = new[]
                {
                    new Customer { FullName = "Nguyễn Tuấn Kiệt", Email = "kiet.nguyen@gmail.com", Phone = "0901234567", Address = "123 Lê Lợi, Quận 1, TP.HCM", Gender = "Nam", Birthday = new DateTime(1995, 5, 12), Password = "customerPassword123" },
                    new Customer { FullName = "Trần Mai Anh", Email = "maianh.tran@gmail.com", Phone = "0912345678", Address = "456 Trần Hưng Đạo, Q.5, TP.HCM", Gender = "Nữ", Birthday = new DateTime(1998, 8, 20), Password = "customerPassword123" },
                    new Customer { FullName = "Lê Quốc Bảo", Email = "bao.le@gmail.com", Phone = "0923456789", Address = "789 Nguyễn Huệ, Quận 1, TP.HCM", Gender = "Nam", Birthday = new DateTime(1992, 3, 15), Password = "customerPassword123" },
                    new Customer { FullName = "Phạm Diệu Linh", Email = "linh.pham@gmail.com", Phone = "0934567890", Address = "12 Hai Bà Trưng, Hoàn Kiếm, HN", Gender = "Nữ", Birthday = new DateTime(2000, 11, 25), Password = "customerPassword123" },
                    new Customer { FullName = "Hoàng Trung Dũng", Email = "dung.hoang@gmail.com", Phone = "0945678901", Address = "34 Cầu Giấy, Cầu Giấy, Hà Nội", Gender = "Nam", Birthday = new DateTime(1996, 7, 8), Password = "customerPassword123" },
                    new Customer { FullName = "Vũ Ngọc Ánh", Email = "anh.vu@gmail.com", Phone = "0956789012", Address = "56 Nguyễn Trãi, Thanh Xuân, HN", Gender = "Nữ", Birthday = new DateTime(1999, 1, 18), Password = "customerPassword123" },
                    new Customer { FullName = "Đỗ Minh Quân", Email = "quan.do@gmail.com", Phone = "0967890123", Address = "78 Quang Trung, Gò Vấp, TP.HCM", Gender = "Nam", Birthday = new DateTime(1994, 9, 30), Password = "customerPassword123" },
                    new Customer { FullName = "Bùi Quỳnh Nga", Email = "nga.bui@gmail.com", Phone = "0978901234", Address = "90 Phan Đăng Lưu, Phú Nhuận", Gender = "Nữ", Birthday = new DateTime(1997, 4, 5), Password = "customerPassword123" },
                    new Customer { FullName = "Đặng Gia Huy", Email = "huy.dang99@gmail.com", Phone = "0989012345", Address = "15 Lê Duẩn, Hải Châu, Đà Nẵng", Gender = "Nam", Birthday = new DateTime(1993, 12, 14), Password = "customerPassword123" },
                    new Customer { FullName = "Trương Mỹ Duyên", Email = "duyen.truong@gmail.com", Phone = "0990123456", Address = "28 Nguyễn Văn Linh, Đà Nẵng", Gender = "Nữ", Birthday = new DateTime(2001, 6, 22), Password = "customerPassword123" },
                    new Customer { FullName = "Phan Trọng Nhân", Email = "nhan.phan@gmail.com", Phone = "0909876543", Address = "42 Hùng Vương, TP. Huế", Gender = "Nam", Birthday = new DateTime(1991, 2, 28), Password = "customerPassword123" },
                    new Customer { FullName = "Võ Thanh Thảo", Email = "thao.vo@gmail.com", Phone = "0918765432", Address = "63 Trần Phú, TP. Nha Trang", Gender = "Nữ", Birthday = new DateTime(1998, 10, 10), Password = "customerPassword123" },
                    new Customer { FullName = "Lâm Hải Đăng", Email = "dang.lam@gmail.com", Phone = "0927654321", Address = "85 Nguyễn Thị Minh Khai, Cần Thơ", Gender = "Nam", Birthday = new DateTime(1995, 4, 16), Password = "customerPassword123" },
                    new Customer { FullName = "Cao Phương Thùy", Email = "thuy.cao@gmail.com", Phone = "0936543210", Address = "19 CMT8, TP. Biên Hòa", Gender = "Nữ", Birthday = new DateTime(1999, 7, 27), Password = "customerPassword123" },
                    new Customer { FullName = "Dương Khắc Tiệp", Email = "tiep.duong@gmail.com", Phone = "0945432109", Address = "72 Bạch Đằng, TP. Thủ Dầu Một", Gender = "Nam", Birthday = new DateTime(1990, 11, 3), Password = "customerPassword123" },
                    new Customer { FullName = "Tô Bích Trâm", Email = "tram.to@gmail.com", Phone = "0954321098", Address = "31 Lê Thánh Tôn, TP. Vũng Tàu", Gender = "Nữ", Birthday = new DateTime(1996, 3, 19), Password = "customerPassword123" },
                    new Customer { FullName = "Hồ Nhật Nam", Email = "nam.ho@gmail.com", Phone = "0963210987", Address = "54 Trần Phú, TP. Đà Lạt", Gender = "Nam", Birthday = new DateTime(1997, 8, 9), Password = "customerPassword123" },
                    new Customer { FullName = "Đoàn Khánh Chi", Email = "chi.doan@gmail.com", Phone = "0972109876", Address = "98 Hoàng Diệu, Buôn Ma Thuột", Gender = "Nữ", Birthday = new DateTime(2002, 5, 1), Password = "customerPassword123" },
                    new Customer { FullName = "Ngô Tiến Đạt", Email = "dat.ngo@gmail.com", Phone = "0981098765", Address = "101 Lý Thường Kiệt, Quy Nhơn", Gender = "Nam", Birthday = new DateTime(1994, 12, 7), Password = "customerPassword123" },
                    new Customer { FullName = "Mai Cẩm Tú", Email = "tu.mai@gmail.com", Phone = "0990987654", Address = "23 Nguyễn Tất Thành, Phan Thiết", Gender = "Nữ", Birthday = new DateTime(1998, 9, 15), Password = "customerPassword123" }
                };

                context.Customers.AddRange(sampleCustomers);
                context.SaveChanges();
            }

            var customerList = context.Customers.ToList();

            // 7. Seed 20 Orders
            if (!context.Orders.Any() && customerList.Any())
            {
                var sampleOrders = new List<Orders>();
                for (int i = 0; i < 20; i++)
                {
                    var customer = customerList[i % customerList.Count];
                    byte status = (byte)(i % 4); // 0, 1, 2, 3 luân phiên
                    sampleOrders.Add(new Orders
                    {
                        CustomerId = customer.Id,
                        Name = customer.FullName,
                        Email = customer.Email,
                        Address = customer.Address,
                        Status = status,
                        CreatedDate = DateTime.Now.AddDays(-20 + i)
                    });
                }

                context.Orders.AddRange(sampleOrders);
                context.SaveChanges();
            }

            // 8. Seed OrderDetail nếu có đơn hàng và sản phẩm
            var orderList = context.Orders.ToList();
            var productList = context.Products.ToList();
            if (!context.OrderDetails.Any() && orderList.Any() && productList.Any())
            {
                var sampleDetails = new List<OrderDetail>();
                for (int i = 0; i < orderList.Count; i++)
                {
                    var order = orderList[i];
                    var prod1 = productList[i % productList.Count];
                    var prod2 = productList[(i + 3) % productList.Count];

                    sampleDetails.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = prod1.Id,
                        Quantity = (i % 3) + 1,
                        Price = prod1.SalePrice > 0 ? prod1.SalePrice : prod1.Price
                    });

                    if (prod1.Id != prod2.Id)
                    {
                        sampleDetails.Add(new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = prod2.Id,
                            Quantity = 1,
                            Price = prod2.SalePrice > 0 ? prod2.SalePrice : prod2.Price
                        });
                    }
                }

                context.OrderDetails.AddRange(sampleDetails);
                context.SaveChanges();
            }
        }
    }
}
