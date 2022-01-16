namespace Commander.Dtos
{
  /// <summary>
  /// This class contains what data were going to return to the users
  /// of the WebAPI
  /// </summary>
  public class CommandReadDto
  {
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string Line { get; set; }

    public override string ToString()
      => $"Id: {Id} \tHowTo: {HowTo} \tLine: {Line}";
  }
}