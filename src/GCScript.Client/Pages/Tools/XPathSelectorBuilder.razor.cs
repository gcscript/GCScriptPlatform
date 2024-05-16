using System.Text;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using GCScript.Shared.Enums;
using GCScript.Shared.Models;

namespace GCScript.Client.Pages.Tools;

public partial class XPathSelectorBuilder : ComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }

    private XPathSelectorBuilderFindItem xpathSelectorBuilderFindItem = new();
    private XPathSelectorBuilderThenFindItem xpathSelectorBuilderThenFindItem = new();
    private string xpath = "";

    protected override void OnInitialized()
    {
        xpathSelectorBuilderFindItem.Items.Add(new XPathSelectorBuilderItem());
    }

    private void UpdateXPath()
    {
        var sb = new StringBuilder();
        sb.Append(xpathSelectorBuilderFindItem.Tag == EHtmlTag.any ? "//*" : $"//{xpathSelectorBuilderFindItem.Tag}");
        sb.Append(XPathBuilder(xpathSelectorBuilderFindItem.Items));

        foreach (var xpathSelectorBuilderFindItem in xpathSelectorBuilderThenFindItem.xpathSelectorBuilderFindItems)
        {
            sb.Append($"/{xpathSelectorBuilderFindItem.Find.ToString().ToLower()}::");
            sb.Append(xpathSelectorBuilderFindItem.Tag == EHtmlTag.any ? "*" : $"{xpathSelectorBuilderFindItem.Tag}");
            sb.Append(XPathBuilder(xpathSelectorBuilderFindItem.Items));
        }


        xpath = sb.ToString();
        InvokeAsync(StateHasChanged);
    }

    private static string XPathBuilder(List<XPathSelectorBuilderItem> xpathSelectorBuilderItemList)
    {
        foreach (var item in xpathSelectorBuilderItemList)
        {
            if (item.Mode == EXPathSelectorBuilderMode.Text)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    return item.ComparisonType switch
                    {
                        EComparisonType.Equals => $"[text()='{item.Value}']",
                        EComparisonType.Contains => $"[contains(text(), '{item.Value}')]",
                        EComparisonType.StartsWith => $"[starts-with(text(), '{item.Value}')]",
                        EComparisonType.EndsWith => $"[ends-with(text(), '{item.Value}')]",
                        _ => throw new NotImplementedException(),
                    };
                }
            }
            else if (item.Mode == EXPathSelectorBuilderMode.Attribute)
            {
                if (!string.IsNullOrWhiteSpace(item.Attribute) && !string.IsNullOrWhiteSpace(item.Value))
                {
                    return item.ComparisonType switch
                    {
                        EComparisonType.Equals => $"[@{item.Attribute}='{item.Value}']",
                        EComparisonType.Contains => $"[contains(@{item.Attribute}, '{item.Value}')]",
                        EComparisonType.StartsWith => $"[starts-with(@{item.Attribute}, '{item.Value}')]",
                        EComparisonType.EndsWith => $"[ends-with(@{item.Attribute}, '{item.Value}')]",
                        _ => throw new NotImplementedException(),
                    };
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        return "";
    }

    private async Task CopyToClipboard()
    {
        await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", xpath);
        Snackbar.Add("XPath copied to clipboard", Severity.Success);
    }
}