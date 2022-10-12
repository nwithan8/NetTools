using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NetTools;

public static class Validation
{
    public static bool Exists(object? value)
    {
        return value != null;
    }

    public static bool Exists(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static bool IsCorrectLength(string value, int minLength = 1, int maxLength = 1000000)
    {
        return (value.Length >= minLength && value.Length <= maxLength);
    }

    public static bool ItemsMatch(string value1, string value2)
    {
        if (!Exists(value1) || !Exists(value2))
        {
            return false;
        }

        return value1.Equals(value2);
    }
    
    public static bool IsValidUnitedStatesPhoneNumber(string phoneNumber)
    { 
        const string pattern = @"^\d{3}-\d{3}-\d{4}$";
        return RegularExpressions.Matches(phoneNumber, pattern, true);
    }

    public static bool IsValidSocialSecurityNumber(string ssn)
    {
        const string pattern = @"^(?!(000|666|9))\d{3}-(?!00)\d{2}-(?!0000)\d{4}$";
        return RegularExpressions.Matches(ssn, pattern, true);
    }

    public static bool IsValidPassportNumber(string passportNumber)
    {
        const string pattern = @"^[A-Z|\d]{6,9}$";
        return RegularExpressions.Matches(passportNumber, pattern, true);
    }

    public static bool IsValidEmail(string email)
    {
        if (!Exists(email))
        {
            return false;
        }

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        // source: VB.NET here: http://emailregex.com/
        const string emailAddressPattern = @"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

        return RegularExpressions.Matches(email, emailAddressPattern, true);
    }

    public static bool IsValidPassword(string password, int minLength = 1, int maxLength = 100)
    {
        var passwordPattern = @"(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*¿?\\-\\(\\)&]{" +
                              minLength + "," + maxLength + "}";

        return RegularExpressions.Matches(password, passwordPattern, true);
    }
}
