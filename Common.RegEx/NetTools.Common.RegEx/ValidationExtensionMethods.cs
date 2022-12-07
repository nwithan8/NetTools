namespace NetTools.Common.RegEx;

public static class ValidationExtensionMethods
{
    public static bool IsValidEmail(this string email)
    {
        return Validation.IsValidEmail(email);
    }

    public static bool IsValidPassportNumber(this string passportNumber)
    {
        return Validation.IsValidPassportNumber(passportNumber);
    }

    public static bool IsValidPassword(this string password, int minLength = 1, int maxLength = 100)
    {
        return Validation.IsValidPassword(password, minLength, maxLength);
    }

    public static bool IsValidSocialSecurityNumber(this string ssn)
    {
        return Validation.IsValidSocialSecurityNumber(ssn);
    }

    public static bool IsValidUnitedStatesPhoneNumber(this string phoneNumber)
    {
        return Validation.IsValidUnitedStatesPhoneNumber(phoneNumber);
    }
}
