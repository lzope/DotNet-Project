//namespace WebApplication5.Models
//{
//	public class SalesLeadEntity
//	{
//		public int Id { get; set; }	
//		public string CourseName { get; set; }
//		public int CourseDuration { get; set; }
//		public double CourseFees { get; set;}

//	}
//}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class SalesLeadEntity
    {
        public int Id { get; set; }

        public string CourseName { get; set; }
        [DisplayName("Course Duration (in Months)")]
        [Range(0, int.MaxValue, ErrorMessage = "Course duration cannot be negative")]

        public int CourseDuration { get; set; }
        [Range(501, 99999, ErrorMessage = "Course fees must be less than 100000 and greater than 500")]

        public double CourseFees { get; set; }

    }
}