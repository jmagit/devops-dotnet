using Xunit;
using Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xunit.Abstractions;
using Moq;
//using System.Runtime.Remoting.Contexts.Fakes;
using Assert = Xunit.Assert;

namespace Utilidades.Tests {
    public class CalculadoraTests {
        private Calculadora sut;
        private readonly ITestOutputHelper output;

        public CalculadoraTests(ITestOutputHelper output) {
            this.output = output;
            sut = new Calculadora();
        }

        [Fact(DisplayName = "Sumar dos valores enteros"), Trait("Category", "smoke")]
        public void SumaTest() {
            //var sut = new Calculadora();

            var actual = sut.Suma(1, 2);

            Assert.Equal(3, actual);
        }

        [Fact(DisplayName = "Sumar dos valores decimales")]
        public void SumaDecimalTest() {
            //var sut = new Calculadora();

            var actual = sut.Suma(0.1m, (decimal)0.2);

            Assert.Equal(0.3m, actual);
        }

        [Fact(DisplayName = "Sumar múltiples valores double")]
        public void SumaDoubleTest() {
            //var sut = new Calculadora();

            var actual = sut.Suma(0.1, 0.2, 1);

            Assert.Equal(1.3, actual);
        }


        [Fact(DisplayName = "Sumar múltiples valores double error")]
        public void SumaDoubleTestKO() {
            Assert.Throws<ArgumentNullException>(() => sut.Suma(0.1, 0.2, null!));
        }

        [Fact(DisplayName = "Validar que no tiene problemas de IEEE 754")]
        public void Suma_IEEE_Test() {
            //var sut = new Calculadora();

            var actual = sut.Suma(0.1, 0.2);

            Assert.Equal(0.3, actual);
        }

        [Theory(DisplayName = "Sumar dos valores enteros")]
        [InlineData(2, 2, 4)]
        [InlineData(2, 3, 5)]
        [InlineData(1, -1, 0)]
        [InlineData(-2, 1, -1)]
        [InlineData(3, 0, 3)]
        //[InlineData(int.MaxValue, 1, int.MinValue)]
        public void SumasIntTest(int operando1, int operando2, int expect) {
            // Arrange
            // var sut = new Calculadora();

            // Act
            try {
                var actual = sut.Suma(operando1, operando2);
                // Assert
                Assert.Equal(expect, actual);
            } catch(Exception ex) {
                Assert.Fail($"Excepcion no controlada: {ex.ToString()}");
            }

        }

        //public static IEnumerable<object[]> SumasDoubleData => new object[][] {
        //    new object[] {0.1, 0.2, 0.3 },
        //    new object[] {2, 3, 5 },
        //    new object[] {3, 0.5, 3.5 },
        //    new object[] {1, -0.9, 0.1 },
        //};
        public static TheoryData<double, double, double> SumasDoubleData => new TheoryData<double, double, double> {
            {0.1, 0.2, 0.3 },
            {2, 3, 5 },
            {3, 0.5, 3.5 },
            {1, -0.9, 0.1 },
        };

        //class SumasDoubleData : TheoryData<double, double, double> {
        //    public SumasDoubleData()
        //    {
        //        Add(0.1, 0.2, 0.3);
        //        Add(2, 3, 5);
        //        Add(3, 0.5, 3.5);
        //        Add(1, -0.9, 0.1);

        //    }
        //}

        [Theory(DisplayName = "Sumar dos valores double")]
        [MemberData(nameof(SumasDoubleData))]
        //[ClassData(typeof(SumasDoubleData))]
        public void SumasDoubleTest(double operando1, double operando2, double expect) {
            // Arrange
            // var sut = new Calculadora();

            // Act
            var actual = sut.Suma(operando1, operando2);

            // Assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void DivideInt() {
            Assert.Equal(0, sut.Divide(1, 2));
        }

        [Fact]
        public void DivideDouble() {
            Assert.Equal(0.5, sut.Divide(1, 2.0));
        }
        [Fact]
        public void DivideIntKO() {
            Assert.Throws<DivideByZeroException>(() => sut.Divide(1, 0));
        }

        [Fact]
        public void DivideDoubleKO() {
            //var ex = Assert.ThrowsAny<ArithmeticException>(() => sut.Divide(1.0, 0));

            //Assert.Contains("cero", ex.Message);

            var ex = Record.Exception(() => {
                sut.Divide(1.0, 0);
            });

            Assert.IsType<DivideByZeroException>(ex);
            Assert.Contains("zero", ex.Message);
        }

        [Theory(DisplayName = "Sumar dos valores enteros")]
        [InlineData(2, 3, -1)]
        [InlineData(5, 2, 3)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 0.9, 0.1)]
        public void RestaTest(double operando1, double operando2, double expect) {
            // Act
            var actual = sut.Resta(operando1, operando2);
            // Assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Privado() {
            var arrange = sut;
            MethodInfo? privado = arrange.GetType().GetMethod("RoundIEEE754", BindingFlags.NonPublic | BindingFlags.Instance);
            if(privado == null ) 
                Assert.Fail("No tiene el método privado");
            double actual = (double)privado.Invoke(arrange, new object[] { (0.1d + 0.2d) })!;
            Assert.Equal(0.3, actual);
        }

        [Fact]
        public void Moqueada() {
            var mock = new Mock<Calculadora>();
            //mock.Setup(o => o.Suma(It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            var esperado = 2;
            mock.Setup(o => o.Suma(2, 2)).Returns(() => esperado + 1);
            mock.SetupSequence(o => o.Suma(1, 1)).Returns(1).Returns(2);
            mock.Setup(o => o.Suma(0, 0))
                   .Callback(() => output.WriteLine("Esto falla"))
                   .Throws(() => new ArithmeticException());
            var sut = mock.Object;
            Assert.Equal(3, sut.Suma(2,2));
            Assert.Equal(1, sut.Suma(1,1));
            Assert.Equal(2, sut.Suma(1,1));
            Assert.ThrowsAny<ArithmeticException>(() => sut.Suma(0,0));
            mock.Verify(o => o.Suma(1, 1), Times.Exactly(2));

        }

        [Fact]
        public void Flujo() {
            var actual = sut.Resta(1, 0);
            Assert.Equal(1, actual);
            actual = sut.Suma(actual, 1);
            actual = sut.Divide(actual, 2);
            Assert.Equal(1, actual);
        }
        //[Fact]
        //public void StubTest() {
        //    var mock = new Utilidades.Fakes.StubCalculadora() {
        //        SumaInt32Int32 = (a, b) => 3
        //    };
        //    Assert.Equal(3, mock.Suma(2, 2));
        //}

    }
}