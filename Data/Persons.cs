using System.ComponentModel.DataAnnotations;

namespace Lesson35.WebAPI.Data
{
    public class Persons
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }        
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
