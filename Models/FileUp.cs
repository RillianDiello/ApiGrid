using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class FileUp
    {
        [Column("Id")]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Column("Date")]
        [Display(Name = "Date Upload")]
        public DateTime DateUp { get; set; }

        [Column("Filename")]
        [Display(Name = "Filename")]
        public String Filename { get; set; }
               

     public FileUp() { }

        public FileUp(int id, string name, DateTime date)
        {
            Id = id;
            DateUp = date;
            Filename = name;

        }
    }
}