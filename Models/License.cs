using System;

namespace LIcensesPO.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class License: BaseEntity
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public long ComputerId { get; set; }

    [ForeignKey("ComputerId")]
    public Computer Computer { get; set; }

    [Required]
    public long ProgramId { get; set; }

    [ForeignKey("ProgramId")]
    public Prog Prog { get; set; }

    [Required]
    public long LicensorId { get; set; }

    [ForeignKey("LicensorId")]
    public Licensor Licensor { get; set; }
}