using System.Collections.Generic;

namespace BasicEshop.Models.Helpers
{
    public interface ICastToStringDictionary
    {
        Dictionary<string, string> ToStringDictionary();
    }
}