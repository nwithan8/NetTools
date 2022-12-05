using System.Globalization;
using Nager.Country;
using PhoneNumbers;

namespace NetTools.Common.Localization
{
    public static class PhoneNumbers
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        public static List<Country> CountryCodes => GetCountryCodes();

        public static List<Country> GetCountryCodes()
        {
            var codes = new List<Country>();
            var currentCulture = CultureInfo.CurrentCulture;
            foreach (var abbreviation in PhoneNumberUtil.GetSupportedRegions())
            {
                var countryName = new CountryProvider().GetCountry(abbreviation).OfficialName;
                var countryDigits = PhoneNumberUtil.GetCountryCodeForRegion(abbreviation).ToString();

                if (!string.IsNullOrWhiteSpace(countryName)) // some country names can't be found, so skip them
                {
                    codes.Add(new Country(name: countryName, abbreviation: abbreviation, phoneCode: countryDigits));
                }
            }

            var sortedCodes = codes.OrderBy(o => o.Name).ToList();
            return sortedCodes;
        }

        public static bool IsValidPhoneNumber(string phoneNumber, string countryCode = "US")
        {
            try
            {
                var parsedPhoneNumber = PhoneNumberUtil.Parse(phoneNumber, countryCode);
                return parsedPhoneNumber != null && PhoneNumberUtil.IsPossibleNumber(parsedPhoneNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        public static string? FormatPhoneNumberInInternationalFormat(string validPhoneNumber, string countryCode = "US")
        {
            try
            {
                var parsedPhoneNumber = PhoneNumberUtil.Parse(validPhoneNumber, countryCode);
                return "+" + parsedPhoneNumber.CountryCode + parsedPhoneNumber.NationalNumber;
            }
            catch (NumberParseException)
            {
                return string.Empty;
            }
        }

        public static string? StylizePhoneNumber(string phoneNumber, string countryCode = "US")
        {
            var formatter = new LiveFormatter(countryCode);

            var formattedNumber = "";
            foreach (var digit in phoneNumber.ToCharArray())
            {
                if (digit is '(' or ')' or '-' or ' ')
                {
                }
                else
                {
                    formattedNumber = formatter.AddDigit(digit);
                }
            }

            return formattedNumber;
        }

        public class LiveFormatter
        {
            private readonly AsYouTypeFormatter _formatter;

            public LiveFormatter(string countryCode = "US")
            {
                _formatter = new AsYouTypeFormatter(countryCode, PhoneNumberUtil);
                _formatter.Clear();
            }

            public string? AddDigit(char digit)
            {
                return _formatter.InputDigit(digit);
            }

            public string? AddDigit(int digit)
            {
                return AddDigit((char)digit);
            }

            public string? AddDigit(string digit)
            {
                return digit.Length <= 0 ? null : AddDigit(digit.ToCharArray()[0]);
            }
        }
    }
}
