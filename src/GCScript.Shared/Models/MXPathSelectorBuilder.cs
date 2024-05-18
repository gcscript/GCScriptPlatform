namespace GCScript.Shared.Models;

public class MXPathSelectorBuilder
{
    public MXPathSelectorBuilderFind FindElementWith { get; set; } = new();
    public List<MXPathSelectorBuilderThen> ThenList { get; set; } = [];
}
