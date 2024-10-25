using Xunit;
using Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Assert = Xunit.Assert;
//using Microsoft.QualityTools.Testing.Fakes;

namespace Utilidades.Tests {
    public class PersonasServiceTests {
        [Fact()]
        public void AddTest() {
            var persona = new Persona { Id = 1, Nombre = "Pepito", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns((Persona?)null);
            mock.Setup(o => o.Add(It.IsAny<Persona>())).Returns(persona);
            var service = new PersonasService(mock.Object);
            var p = service.Add(persona);

            Assert.NotNull(p);
            Assert.Multiple(
                () => Assert.Equal(1, p.Id),
                () => Assert.Equal("Pepito", p.Nombre),
                () => Assert.Equal("grillo", p.Apellidos, ignoreCase: true)
            );
        }
        [Fact()]
        public void AddYaExisteKO() {
            var persona = new Persona { Id = 1, Nombre = "Pepito", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns(persona);
            mock.Setup(o => o.Add(It.IsAny<Persona>())).Returns(persona);
            var service = new PersonasService(mock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.Add(persona));
            Assert.Equal("Ya existe", ex.Message);
        }
        [Fact()]
        public void AddInvalidosKO() {
            var persona = new Persona { Id = 1, Nombre = "    ", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns(persona);
            mock.Setup(o => o.Add(It.IsAny<Persona>())).Returns(persona);
            var service = new PersonasService(mock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.Add(persona));
            Assert.Equal("Datos inválidos", ex.Message);
        }
        [Fact()]
        public void AddSinDatosKO() {
            var persona = new Persona { Id = 1, Nombre = "    ", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.Add(It.IsAny<Persona>())).Returns(persona);
            var service = new PersonasService(mock.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => service.Add(null!));
            //Assert.Equal("item", ex.Message);
        }
        [Fact()]
        public void AddFallaRepositorio() {
            var persona = new Persona { Id = 1, Nombre = "kkk", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns((Persona?)null);
            mock.Setup(o => o.Add(It.IsAny<Persona>())).Throws(() => new Exception("UNIQUE CONTRAINS ..."));
            var service = new PersonasService(mock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.Add(persona));
            Assert.Equal("Datos inválidos", ex.Message);
            mock.VerifyAll();
        }
        [Fact()]
        public void ModifyTest() {
            var personaOld = new Persona { Id = 1, Nombre = "Pepito", Apellidos = "Grillo" };
            var personaNew = new Persona { Id = 1, Nombre = "GRILLO", Apellidos = "PEPITO" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns(personaOld);
            mock.Setup(o => o.Modify(It.IsAny<Persona>())).Returns(personaNew);
            var service = new PersonasService(mock.Object);
            var p = service.Modify(personaNew);

            Assert.NotNull(p);
            Assert.Multiple(
                () => Assert.Equal(1, p.Id),
                () => Assert.Equal("GRILLO", p.Nombre),
                () => Assert.Equal("PEPITO", p.Apellidos)
            );
        }
        [Fact()]
        public void ModifyFallaRepositorio() {
            var persona = new Persona { Id = 1, Nombre = "kkk", Apellidos = "Grillo" };
            var mock = new Mock<IPersonasRepository>();
            mock.Setup(o => o.GetById(1)).Returns(persona);
            mock.Setup(o => o.Modify(It.IsAny<Persona>())).Throws(() => new Exception("UNIQUE CONTRAINS ..."));
            var service = new PersonasService(mock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.Modify(persona));
            Assert.Equal("Datos inválidos", ex.Message);
            mock.VerifyAll();
        }

        //[Fact]
        //public void EdadOK() {
        //    using(ShimsContext.Create()) {
        //        System.Fakes.ShimDateTime.TodayGet = () => new DateTime(2020, 1, 1);
        //        var persona = new Persona { FechaNacimiento = new DateTime(2000, 1, 2) };
        //        Assert.Equal(19, persona.Edad);
        //    }
        //}
    }
}