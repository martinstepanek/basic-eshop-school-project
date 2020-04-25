using System;
using System.Collections.Generic;
using BasicEshop.Models.Helpers;

namespace BasicEshop.Models.Forms
{
    public class ProductFilterForm : ICastToStringDictionary
    {
        public string? CategoryId { get; set; }
        public string? SellerIds { get; set; }

        public static implicit operator Dictionary<string, string>(ProductFilterForm form)
        {
            return new Dictionary<string, string>()
            {
                {"CategoryId", form.CategoryId},
                {"SellerIds", form.SellerIds}
            };
        }

        public Dictionary<string, string> ToStringDictionary()
        {
            return this;
        }
    }
}