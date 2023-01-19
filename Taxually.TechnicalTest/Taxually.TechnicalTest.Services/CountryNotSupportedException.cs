using System;

namespace Taxually.TechnicalTest.Services
{
    public class CountryNotSupportedException : Exception
    {
        public CountryNotSupportedException(string countryCode)
            : base($"Country not supported: {countryCode}")
        {
            CountryCode = countryCode;
        }

        public string CountryCode { get; }
    }
}
