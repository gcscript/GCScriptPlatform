using GCScript.Shared.Enums;

namespace GCScript.Shared.Models;

public class MXPathSelectorBuilderThen : MXPathSelectorBuilderFind
{
    public EXPathSelectorBuilderFind Find { get; set; } = EXPathSelectorBuilderFind.Ancestor;
}
