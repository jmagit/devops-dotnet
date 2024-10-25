using Xunit;
using Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Text.RegularExpressions;
using AutoFixture;
using FluentAssertions;
using Assert = Xunit.Assert;

namespace Utilidades.Tests {
    public class ListaDePersonas {
        public List<Persona> lista { get; set; }

        public ListaDePersonas() {
            lista = new List<Persona>();
            lista.Add(new Persona { Id = 1, Nombre = "Pepito", Apellidos = "Grillo" });
            lista.Add(new Persona { Id = 2, Nombre = "Carmelo", Apellidos = "Coton" });
        }
    }

    public class PersonaTests: IClassFixture<ListaDePersonas> {
        ListaDePersonas fixture;
        private readonly ITestOutputHelper output;

        public PersonaTests(ListaDePersonas fixture, ITestOutputHelper output) {
            this.output = output;
            this.fixture = fixture;
        }

        [Fact(), Trait("Category", "smoke")]
        public void ToStringTest() {
            Assert.Equal(2, fixture.lista.Count);
            output.WriteLine(fixture.lista[1].ToString());
            var auto = new Fixture();
            output.WriteLine(auto.Create<int>().ToString());
            output.WriteLine(auto.Create<Persona>().ToString());
        }

        [Fact()]
        public void GetTest() {
            var p = fixture.lista.FirstOrDefault(item => item.Id == 1);
            Assert.NotNull(p);
            Assert.Multiple(
                () => Assert.Equal(1, p.Id),
                () => Assert.Equal("Pepito", p.Nombre),
                () => Assert.Equal("grillo", p.Apellidos, ignoreCase: true)
            );
            //p.Should().Be()
        }

        //[Fact()]
        //public void xMal() {
        //    fixture.lista.Add(new Persona());
        //    Assert.Equal(3, fixture.lista.Count);
        //}
    }


    [CollectionDefinition(nameof(PersonasCollection))]
    public class PersonasCollection : ICollectionFixture<ListaDePersonas> { }

    [Collection(nameof(PersonasCollection))]
    public class PersonasListTests {
        ListaDePersonas fixture;

        public PersonasListTests(ListaDePersonas fixture) {
            this.fixture = fixture;
        }

        [Fact()]
        public void ElementosTest() {
            Assert.Equal(2, fixture.lista.Count);
        }
    }

    [Collection(nameof(PersonasCollection))]
    public class PersonasTests {
        ListaDePersonas fixture;

        public PersonasTests(ListaDePersonas fixture) {
            this.fixture = fixture;
        }

        [Fact()]
        public void OtraPruebaTest() {
            Assert.Equal(2, fixture.lista.Count);
        }
    }

}