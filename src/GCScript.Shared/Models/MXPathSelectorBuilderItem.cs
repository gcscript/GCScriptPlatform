using GCScript.Shared.Enums;

namespace GCScript.Shared.Models;

public class MXPathSelectorBuilderItem
{
    public EXPathSelectorBuilderMode Mode { get; set; } = EXPathSelectorBuilderMode.Text;
    public EComparisonType ComparisonType { get; set; } = EComparisonType.Equals;
    public string Attribute { get; set; } = "id";
    public string Value { get; set; } = "example";
}
