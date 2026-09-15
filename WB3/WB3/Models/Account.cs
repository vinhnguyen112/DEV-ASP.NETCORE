namespace WB3.Models
{
    public class Account
    {
        public int id { get; set; }
        public float saleprice { get; set; }
        public string img { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string categoryId { get; set; }
        public string description { get; set; }
        public string status { get; set; }

        public DateTime created { get; set; } = DateTime.Now;

    }
}
