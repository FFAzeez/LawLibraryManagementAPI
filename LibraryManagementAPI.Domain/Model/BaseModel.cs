using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Domain.Model
{
    public class BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
   



