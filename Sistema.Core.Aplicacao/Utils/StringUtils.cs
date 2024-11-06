using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.Utils
{
    static class StringUtils
    {
        public static string NormalizeName(string name)
        {
            return name.ToUpperInvariant()
                        .Normalize(NormalizationForm.FormD)
                        .Replace('\u0300', '\0')
                        .Replace('\u0301', '\0')
                        .Replace('\u0323', '\0');
        }
    }
}
