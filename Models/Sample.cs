using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Sample")]
    public class Sample
    {
        [Column("Id")]
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Column("Date")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
               

        [Column("Value")]
        [Display(Name = "Value")]
        public double Value { get; set; }

        public int FileId { get; set; }
        public FileUp File { get; set; }


        public Sample() { }
        public Sample(int id, DateTime date, double value, int fileId, FileUp file)
        {
            Id = id;
            Date = date;
            Value = value;
            FileId = fileId;
            File = file;
        }
    }
}

