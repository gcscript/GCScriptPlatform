using GCScript.Shared.Enums;

namespace GCScript.Shared.Models;

public class MXPathSelectorBuilderFind
{
    public EHtmlTag Tag { get; set; } = EHtmlTag.any;
    public List<MXPathSelectorBuilderItem> Items { get; set; } = [new()];
}
