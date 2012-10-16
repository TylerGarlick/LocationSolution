using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.ZipCodes
{
    public class ZipCodeDeleteModel
    {
        public ZipCode ZipCode { get; set; }

        public ZipCodeDeleteModel()
        {

        }
        public ZipCodeDeleteModel(ZipCode zipCode)
        {
            ZipCode = zipCode;
        }
    }
}