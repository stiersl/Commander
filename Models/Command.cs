namespace Commander.Models
{
  public class Command
  {
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string Line { get; set; }
    public string Platform { get; set; }

    public override string ToString()  
      => $"Id: {Id} \tHowTo: {HowTo} \tLine: {Line} \tPlatform: {Platform}";
  
  }
}