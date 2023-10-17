using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;

namespace MauiAutoSuggestBox.Platforms.Windows.Handlers;

public partial class AutoSuggestHandler : ViewHandler<IView, AutoSuggestBox>
{
    /// <inheritdoc/>
    protected override AutoSuggestBox CreatePlatformView()
    {
        AutoSuggestBox autoSuggestBox = new();
        autoSuggestBox.Background = new SolidColorBrush(global::Windows.UI.Color.FromArgb(0xff, 0xff, 0x90, 0x50));
        autoSuggestBox.MaxSuggestionListHeight = 100;
        autoSuggestBox.Width = 300;
        autoSuggestBox.Items.Add("Item A");
        autoSuggestBox.Items.Add("Item B");
        return autoSuggestBox;
    }

    public override void SetVirtualView(IView view)
    {
        base.SetVirtualView(view);
    }

    protected override void ConnectHandler(AutoSuggestBox platformView)
    {
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(AutoSuggestBox platformView)
    {
        base.DisconnectHandler(platformView);
    }
}