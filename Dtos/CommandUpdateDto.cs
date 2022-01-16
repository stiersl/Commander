using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
  public class CommandUpdateDto
  {
    // note removed the ID as it is being created by DB
    // Make sure to inlude the data annotations here to force return code 400
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
      => $"HowTo: {HowTo} \tLine: {Line} \tPlatform: {Platform}";
  }
}