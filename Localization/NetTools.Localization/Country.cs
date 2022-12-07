using Nager.Country;

namespace NetTools.Localization;

public class Country
{
    public readonly string Abbreviation;
    public readonly string Name;

    public readonly string PhoneCode;

    private ICountryInfo _countryInfo;

    public string PhoneCodeDisplay => $"{Name} (+{PhoneCode})";

    /// <summary>
    ///     Constructor for a country.
    /// </summary>
    /// <param name="name">Name of the country.</param>
    /// <param name="abbreviation">Abbreviation of the country.</param>
    /// <param name="phoneCode">Phone prefix code of the country.</param>
    public Country(string name, string abbreviation, string phoneCode)
    {
        Name = name;
        Abbreviation = abbreviation;
        PhoneCode = phoneCode;
        _countryInfo = new CountryProvider().GetCountry(abbreviation);
    }
}
