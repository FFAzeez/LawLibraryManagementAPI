using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Domain.Model;

public class AuditLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string TableName { get; set; }
    public string RecordId { get; set; }
    public string Action { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string AffectedColumns { get; set; }
    public DateTime InitiatedDate { get; set; }
}