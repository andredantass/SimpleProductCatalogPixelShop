using SimpleProductCatalog.Abstraction.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Application.Util
{
    public static class ValidationSummary
    {
        public static bool ValidateProductForUpdate(ProductDTO productDto, out string validationMessage)
        {
            if (string.IsNullOrWhiteSpace(productDto.Id.ToString()))
            {
                validationMessage = "Product ID is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                validationMessage = "Product name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productDto.Description))
            {
                validationMessage = "Product description is required.";
                return false;
            }

            if (productDto.Price <= 0)
            {
                validationMessage = "Product price must be greater than zero.";
                return false;
            }

            if (productDto.Category == null || string.IsNullOrWhiteSpace(productDto.Category.Id))
            {
                validationMessage = "Category ID is required.";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }
    }
}
