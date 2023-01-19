using System;

namespace Taxually.TechnicalTest.DataTransfer
{
    public class VatRegistrationRequest
    {
        public string CompanyName { get; set; } = String.Empty;
        
        public string CompanyId { get; set; } = String.Empty;

        public string Country { get; set; } = String.Empty;
    }
}
