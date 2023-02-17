using System.ComponentModel.DataAnnotations;

namespace Weather.Api;

public class ExampleOptions
{
    public const string SectionName = "Example";

    [EnumDataType(typeof(LogLevel))]
	public LogLevel LogLevel { get; init; }

    [Range(1,9)]
    public int Retries { get; init; }

    [Required]
    public string RequiredString { get; set; }
}