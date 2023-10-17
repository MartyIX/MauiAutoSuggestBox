using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;

namespace MauiAutoSuggestBox.Controls;

/// <summary>
/// Auto-suggest control.
/// </summary>
public class AutoSuggestEntry : View, IElementConfiguration<AutoSuggestEntry>, IView
{
    /// <summary>Bindable property for <see cref="Text"/>.</summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AutoSuggestEntry), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) => { });

    /// <summary>Bindable property for <see cref="IsReadOnly"/>.</summary>
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(InputView), false);

    /// <summary>Bindable property for <see cref="CursorPosition"/>.</summary>
    public static readonly BindableProperty CursorPositionProperty = BindableProperty.Create(nameof(CursorPosition), typeof(int), typeof(InputView), 0, validateValue: (b, v) => (int)v >= 0);

    /// <summary>Bindable property for <see cref="SelectionLength"/>.</summary>
    public static readonly BindableProperty SelectionLengthProperty = BindableProperty.Create(nameof(SelectionLength), typeof(int), typeof(InputView), 0, validateValue: (b, v) => (int)v >= 0);

    /// <summary>
    /// Backing store for the <see cref="ReturnType"/> property.
    /// </summary>
    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(Entry), ReturnType.Default);

    /// <summary>
    /// Backing store for the <see cref="ReturnCommand"/> property.
    /// </summary>
    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(Entry), default(ICommand));

    /// <summary>
    /// Backing store for the <see cref="ReturnCommandParameter"/> property.
    /// </summary>
    public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(Entry), default(object));

    /// <inheritdoc cref="InputView.PlaceholderProperty"/>
    public static new readonly BindableProperty PlaceholderProperty = InputView.PlaceholderProperty;

    /// <inheritdoc cref="InputView.TextColorProperty"/>
    public static new readonly BindableProperty TextColorProperty = InputView.TextColorProperty;

    /// <summary>
    /// Backing store for the <see cref="ClearButtonVisibility"/> property.
    /// </summary>
    public static readonly BindableProperty ClearButtonVisibilityProperty = BindableProperty.Create(nameof(ClearButtonVisibility), typeof(ClearButtonVisibility), typeof(Entry), ClearButtonVisibility.Never);

    /// <inheritdoc/>
    private readonly Lazy<PlatformConfigurationRegistry<AutoSuggestEntry>> platformConfigurationRegistry;

    /// <summary>
    /// Occurs when the user finalizes the text in an entry with the return key.
    /// </summary>
    public event EventHandler Completed;

    /// <summary>
    /// Creates a new <see cref="AutoSuggestEntry"/> object with default values.
    /// </summary>
    public AutoSuggestEntry()
    {
        this.Completed = null!;
        this.platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<AutoSuggestEntry>>(() => new PlatformConfigurationRegistry<AutoSuggestEntry>(this));
    }

    /// <inheritdoc/>
    public string Text
    {
        get => (string)this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    /// <inheritdoc/>
    public bool IsReadOnly
    {
        get => (bool)this.GetValue(IsReadOnlyProperty);
        set => this.SetValue(IsReadOnlyProperty, value);
    }

    /// <inheritdoc/>
    public string Placeholder
    {
        get => (string)this.GetValue(PlaceholderProperty);
        set => this.SetValue(PlaceholderProperty, value);
    }

    /// <inheritdoc/>
    public Color TextColor
    {
        get => (Color)this.GetValue(TextColorProperty);
        set => this.SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Determines what the return key on the on-screen keyboard should look like. This is a bindable property.
    /// </summary>
    public ReturnType ReturnType
    {
        get => (ReturnType)this.GetValue(ReturnTypeProperty);
        set => this.SetValue(ReturnTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to run when the user presses the return key, either physically or on the on-screen keyboard.
    /// This is a bindable property.
    /// </summary>
    public ICommand ReturnCommand
    {
        get => (ICommand)this.GetValue(ReturnCommandProperty);
        set => this.SetValue(ReturnCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter object for the <see cref="ReturnCommand" /> that can be used to provide extra information.
    /// This is a bindable property.
    /// </summary>
    public object ReturnCommandParameter
    {
        get => this.GetValue(ReturnCommandParameterProperty);
        set => this.SetValue(ReturnCommandParameterProperty, value);
    }

    /// <summary>
    /// Determines the behavior of the clear text button on this entry. This is a bindable property.
    /// </summary>
    public ClearButtonVisibility ClearButtonVisibility
    {
        get => (ClearButtonVisibility)this.GetValue(ClearButtonVisibilityProperty);
        set => this.SetValue(ClearButtonVisibilityProperty, value);
    }

    /// <inheritdoc/>
    public bool IsPassword => false;

    /// <inheritdoc/>
    public bool IsTextPredictionEnabled => false;

    /// <inheritdoc/>
    public int CursorPosition
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public int SelectionLength
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public TextAlignment HorizontalTextAlignment => throw new NotImplementedException();

    /// <inheritdoc/>
    public TextAlignment VerticalTextAlignment => throw new NotImplementedException();

    /// <summary>
    /// Internal method to trigger <see cref="Completed"/> and <see cref="ReturnCommand"/>.
    /// Should not be called manually outside of .NET MAUI.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SendCompleted()
    {
        if (this.IsEnabled)
        {
            this.Completed?.Invoke(this, EventArgs.Empty);

            if ((this.ReturnCommand != null) && this.ReturnCommand.CanExecute(this.ReturnCommandParameter))
            {
                this.ReturnCommand.Execute(this.ReturnCommandParameter);
            }
        }
    }

    /// <inheritdoc/>
    public IPlatformElementConfiguration<T, AutoSuggestEntry> On<T>() where T : IConfigPlatform
    {
        return this.platformConfigurationRegistry.Value.On<T>();
    }
}