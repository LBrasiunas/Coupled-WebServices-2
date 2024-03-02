using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Models;

public class ServiceEntry
{
    [Key]
    public required int Id { get; set; }

    public required int CarId { get; set; }

    public required int ServiceId { get; set; }

    public required int ServiceLeaflet { get; set; } // Same as book

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime InsertedOn { get; set; }
}
