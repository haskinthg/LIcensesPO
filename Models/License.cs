using System;

namespace LIcensesPO.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class License
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int ComputerId { get; set; }

    [ForeignKey("ComputerId")]
    public Computer Computer { get; set; }

    [Required]
    public int ProgramId { get; set; }

    [ForeignKey("ProgramId")]
    public Prog Prog { get; set; }

    [Required]
    public int LicensorId { get; set; }

    [ForeignKey("LicensorId")]
    public Licensor Licensor { get; set; }
}