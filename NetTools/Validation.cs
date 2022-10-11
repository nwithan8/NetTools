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

    public static bool IsValidPhoneNumber(string phoneNumber, string countryCode = "US")
    {
        return Exists(phoneNumber) && PhoneNumbers.IsValidPhoneNumber(phoneNumber, countryCode);
    }

    public static bool IsValidSocialSecurityNumber(string ssn)
    {
        var r = new Regex(@"^(?!(000|666|9))\d{3}-(?!00)\d{2}-(?!0000)\d{4}$");
        return r.IsMatch(ssn);
    }

    public static bool IsValidPassportNumber(string passportNumber)
    {
        var r = new Regex(@"^[A-Z|\d]{6,9}$");
        return r.IsMatch(passportNumber);
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
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        // source: VB.NET here: http://emailregex.com/
        const string emailAddressPattern = @"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

        return RegularExpressions.Matches(input: email,
            pattern: emailAddressPattern,
            ignoreCase: true);
    }

    public static bool IsValidPassword(string password, int minLength = 1, int maxLength = 100)
    {
        if (!Exists(password))
        {
            return false;
        }

        var passwordPattern = @"(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*¿?\\-\\(\\)&]{" +
                              minLength + "," + maxLength + "}";

        return RegularExpressions.Matches(input: password,
            pattern: passwordPattern,
            ignoreCase: true);
    }
}
