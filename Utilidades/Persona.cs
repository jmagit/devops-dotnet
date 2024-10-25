using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades {
    public class Persona {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public bool Jubilado { get; set; } = false;
        public DateTime? FechaNacimiento { get; set; }

        public int? Edad {
            get {
                if(!FechaNacimiento.HasValue) return null;
                var edad = DateTime.Today.Year - FechaNacimiento.Value.Year;
                return edad - (DateTime.Today < FechaNacimiento.Value.AddYears(edad) ? 1 : 0);
            }
        }

        public bool IsValid() {
            return Nombre.IsNotBlank();
        }

        public override string ToString() {
            return $"Id: {Id}, Nombre: {Nombre}, Apellidos: {Apellidos}, Jubilado: {(Jubilado ? "si" : "no")}";
        }

    }
}
