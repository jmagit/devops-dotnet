using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilidades {
    public static class Validador {
        public static bool IsBlank(this string cad) {
            return string.IsNullOrEmpty((cad ?? "").Trim());
        }
        public static bool IsNotBlank(this string cad) {
            return !IsBlank(cad);
        }

        public static bool MaxLenght(this string cad, int len) {
            return (cad ?? "").Length <= len;
        }
        public static bool Posive(this int valor) {
            return valor > 0;
        }
        public static bool IsNIF(this string cad) {
            if(cad.IsBlank()) return false;
            var nif = cad.ToUpper();
            if(!Regex.IsMatch(nif, @"^\d{1,8}[TRWAGMYFPDXBNJZSQVHLCKE]$", RegexOptions.None, TimeSpan.FromMilliseconds(100))) return false;
            var numberValue = long.Parse(nif.Substring(0, nif.Length - 1));
            return nif[nif.Length - 1] == "TRWAGMYFPDXBNJZSQVHLCKE"[(int)(numberValue % 23)];
        }
    }
}
