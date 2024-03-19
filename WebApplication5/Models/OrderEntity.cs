using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseDuration { get; set; }
        //public string CourseFees { get; set; }
        public double CourseFees { get; set; }
        [NotMapped]
        public string TransactionId { get; set; }
        [NotMapped]

        public string OrderId { get; set; }
    }
}
