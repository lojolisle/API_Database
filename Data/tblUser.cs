using System.ComponentModel.DataAnnotations;

namespace ApiDatabaseProject2.Data
{
    public class tblUser
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Phonenumber { get; set; }

        public string? City { get; set; }
    }
}
