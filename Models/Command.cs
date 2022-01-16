using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
  public class Command
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; }
    [Required]
    [MaxLength(250)]
    public string Line { get; set; }
    [Required]
    [MaxLength(250)]
    public string Platform { get; set; }

    public override string ToString()
      => $"Id: {Id} \tHowTo: {HowTo} \tLine: {Line} \tPlatform: {Platform}";

  }
}