using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progi.BidCalculator.Core.Entities;

public class Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}