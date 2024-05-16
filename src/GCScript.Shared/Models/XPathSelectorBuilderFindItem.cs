using GCScript.Shared.Enums;

namespace GCScript.Shared.Models;

public class XPathSelectorBuilderFindItem
{
    public EXPathSelectorBuilderFind Find { get; set; } = EXPathSelectorBuilderFind.Ancestor;
    public EHtmlTag Tag { get; set; } = EHtmlTag.any;
    public List<XPathSelectorBuilderItem> Items { get; set; } = [];
}
