using MauiAutoSuggestBox.Controls;
using Microsoft.Maui.Handlers;

namespace MauiAutoSuggestBox.Platforms.Windows.Handlers;

public partial class AutoSuggestHandler : IViewHandler
{
    public static IPropertyMapper<AutoSuggestEntry, AutoSuggestHandler> PropertyMapper = new PropertyMapper<AutoSuggestEntry, AutoSuggestHandler>(ViewHandler.ViewMapper)
    {
    };

    public static CommandMapper<AutoSuggestEntry, AutoSuggestHandler> CommandMapper = new(ViewCommandMapper)
    {
    };

    public AutoSuggestHandler() : base(PropertyMapper, CommandMapper)
    {
    }
}