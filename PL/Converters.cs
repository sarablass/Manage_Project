using System.Globalization;
using System.Windows.Data;
using System;

namespace PL;

// Class to convert an ID value to corresponding content (string)
class ConvertIdToContent : IValueConverter 
    {
    // Method to convert the input value to content based on its type and other parameters
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
            return (int)value == 0 ? "Add" : "Update"; // Convert the input value to an integer and check if it equals 0, If it equals 0, return "Add" and it will write Add on the button, otherwise return "Update" and it will write Update on the button.
    }

    // Method to convert back the content to the original value 
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

// Class to convert an ID value to a boolean value indicating if a control should be enabled
class ConvertIdToIsEnable : IValueConverter
{
    // Method to convert the input value to a boolean value based on its type and other parameters
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? true : false;  // Convert the input value to an integer and check if it equals 0, If it equals 0, return true (enabled in Add), otherwise return false (disabled) in Update.
    }

    // Method to convert back the boolean value to the original value 
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

