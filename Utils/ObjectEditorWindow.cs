using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using ReactiveUI;

namespace LIcensesPO.Utils;

public class ObjectEditorWindow<T> : Window
{
    private readonly Dictionary<string, TemplatedControl>
        propertyTextBoxes = new Dictionary<string, TemplatedControl>();

    private T editingObject;
    private bool isInputValid = false;
    private Button okButton;

    public ObjectEditorWindow(T obj = default)
    {
        editingObject = obj;
        CreateUI();
        InitializeValues();
    }

    private void CreateUI()
    {
        Width = 350;
        SizeToContent = SizeToContent.Height;
        RequestedThemeVariant = ThemeVariant.Light;
        var stackPanel = new StackPanel
        {
            Spacing = 10,
            Margin = new Thickness(20)
        };

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name == "Id") continue;
            if (IsPrimitive(property.PropertyType))
            {
                var textBox = property.PropertyType == typeof(DateTime)
                    ? (TemplatedControl)new DatePicker()
                    {
                        Margin = new Thickness(0, 0, 0, 10),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        DataContext = this
                    }
                    : new TextBox
                    {
                        Watermark = property.Name,
                        Margin = new Thickness(0, 0, 0, 10),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        DataContext = this
                    };
                
                textBox.KeyDown += TextBox_AttachedToLogicalTree;

                propertyTextBoxes[property.Name] = textBox;

                stackPanel.Children.Add(textBox);
            }
        }

        var buttonPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Spacing = 10,
            Margin = new Thickness(0, 20, 0, 0)
        };

        var okButton = new Button
        {
            Content = "OK",
            Command = ReactiveCommand.Create(OkClick),
            Background = Brushes.Green,
            Foreground = Brushes.White,
            Width = 80,
            Height = 30,
            IsEnabled = false
        };
        this.okButton = okButton;

        var cancelButton = new Button
        {
            Content = "Cancel",
            Command = ReactiveCommand.Create(CancelClick),
            Background = Brushes.Gray,
            Foreground = Brushes.White,
            Width = 80,
            Height = 30,
            HorizontalAlignment = HorizontalAlignment.Right
        };

        buttonPanel.Children.Add(okButton);
        buttonPanel.Children.Add(cancelButton);

        stackPanel.Children.Add(buttonPanel);

        this.Content = stackPanel;
    }

    private void InitializeValues()
    {
        if (editingObject != null)
        {
            foreach (var propertyTextBox in propertyTextBoxes)
            {
                var propertyName = propertyTextBox.Key;
                var propertyValue = typeof(T).GetProperty(propertyName)?.GetValue(editingObject)?.ToString();
                if (propertyTextBox is TextBox)
                    ((TextBox)propertyTextBox.Value).Text = propertyValue;
                else ((DatePicker)propertyTextBox.Value).SelectedDate = DateTime.Parse(propertyValue);
            }
        }
    }

    private void OkClick()
    {
        var obj = editingObject ?? Activator.CreateInstance<T>();

        foreach (var propertyTextBox in propertyTextBoxes)
        {
            var propertyName = propertyTextBox.Key;
            Object propertyValue;
            if (propertyTextBox.Value is TextBox)
                propertyValue = ((TextBox)propertyTextBox.Value).Text;
            else propertyValue = ((DatePicker)propertyTextBox.Value).SelectedDate.Value.DateTime;
            if (propertyName == "Id") break;

            var propertyInfo = typeof(T).GetProperty(propertyName);
            var convertedValue = Convert.ChangeType(propertyValue, propertyInfo?.PropertyType);

            propertyInfo.SetValue(obj, convertedValue);
        }

        Close(obj);
    }

    private void CancelClick()
    {
        Close(default);
    }

    private bool IsPrimitive(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }

    private void TextBox_AttachedToLogicalTree(object? sender, KeyEventArgs e)
    {
        int count = 0;
        foreach (var propertyTextBox in propertyTextBoxes)
        {
            if (!ValidateInput(propertyTextBox.Value)) count++;
        }

        if (count == 0) okButton.IsEnabled = true;
        else okButton.IsEnabled = false;
    }
    private bool ValidateInput(TemplatedControl control)
    {
        if (control.DataContext is ObjectEditorWindow<T> editorWindow)
        {
            var propertyName = editorWindow.propertyTextBoxes
                .FirstOrDefault(pair => pair.Value == control).Key;

            if (string.IsNullOrWhiteSpace(control is TextBox textBox
                    ? textBox.Text
                    : control is DatePicker datePicker
                        ? datePicker.SelectedDate?.ToString()
                        : null))
                return false;
            return true;
        }

        return false;
    }
}