using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.ProductCountryServices
{
    public interface IProductCountryService
    {
        void Link(int productId, int countryId);
        void UnLink(int productId, int countryId);
        ProductCountry? Get(int productId, int countryId);
        void UnLinkAllForCountry(int countryId);

    }
}
