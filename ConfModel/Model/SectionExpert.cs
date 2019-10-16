using ConfModel.Interface;

namespace ConfModel.Model
{
    public class SectionExpert : IId
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}