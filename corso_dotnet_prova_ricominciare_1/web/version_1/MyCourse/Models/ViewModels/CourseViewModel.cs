using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public string ImagePath { set; get; }
        public string Author { set; get; }
        public double Rating { set; get; }
        public Money FullPrice{ set; get; }
        public Money CurrentPrice { set; get; }
    }
}