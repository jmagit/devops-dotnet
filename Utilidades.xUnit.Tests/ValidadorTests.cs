using Xunit;
using Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;

namespace Utilidades.Tests {
    public class ValidadorTests {
        [Theory()]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void IsBlankTrue(string? caso) {
            Assert.True(caso!.IsBlank());
        }

        [Theory()]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void IsNotBlankTrue(string? caso) {
            Assert.False(caso!.IsNotBlank());
        }

        [Fact()]
        public void MaxLenghtTrue() {
            Assert.True("1234".MaxLenght(4));
        }
        [Fact()]
        public void MaxLenghtFalse() {
            Assert.False("1234".MaxLenght(3));
        }

        [Fact()]
        public void PosiveTrue() {
            Assert.True(1.Posive());
        }
        [Fact()]
        public void PosiveFalse() {
            Assert.False(0.Posive());
        }

        [Theory()]
        [InlineData("12345678z")]
        [InlineData("4G")]
        public void IsNIFTrue(string caso) {
            Assert.True(caso.IsNIF());
        }

        [Theory()]
        [InlineData("12345678")]
        [InlineData("G4")]
        [InlineData("")]
        [InlineData(null)]
        public void IsNIFFalse(string? caso) {
            Assert.False(caso!.IsNIF());
        }
    }
}