using DO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BO;

public static class Tools
{
    public static string ToStringProperty(this object p)
    {
        var prop = p.GetType().GetProperties();
        string str = "";
        foreach (var property in prop)
        {
            str += property.Name + ":" + property.GetValue(p);
        }
        return str;
    }

    public static void ValidationId(int id)
    {
        if (id < 0)
            throw new BlInvalidInputException($"The id ${id} is not correct");
    }

    public static void ValidationString(string str)
    {
        if (str is null)
            throw new BlInvalidInputException($"The input ${str} is not correct");
    }

    public static void ValidationCost(double cost)
    {
        if (cost < 0)
            throw new BlInvalidInputException($"The cost ${cost} is not correct");
    }

    public static void ValidationEmail(string email)
    {
        // Define a regular expression for basic email validation
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);

        // Use the regex to match the email address
        Match match = regex.Match(email);

        // Return true if the email matches the pattern, false otherwise
        if (match.Success == false)
            throw new BlInvalidInputException($"The email ${email} is not correct");

    }
}