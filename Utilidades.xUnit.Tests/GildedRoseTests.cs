using Xunit;
using GildedRoseKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Assert = Xunit.Assert;

namespace GildedRoseKata.Tests {
    public class GildedRoseTests {
        [Theory()]
        [InlineData(11, 10, 10, 9)]
        [InlineData(7, 1, 6, 0)]
        //[InlineData(5, -5, 4, 0)]
        [InlineData(0, 3, -1, 1)]
        public void Product_Common_Test(int sellIn, int quality, int sellInResult, int qualityResult) {
            String name = "Common Product";
            Item product = new Item() { Name = name, SellIn = sellIn, Quality = quality };
            GildedRose app = new GildedRose(new Item[] { product });
            app.UpdateQuality();
            Assert.Multiple(
                () => Assert.Equal(name, product.Name),
                () => Assert.Equal(sellInResult, product.SellIn),
                () => Assert.Equal(qualityResult, product.Quality)
                );
        }

        [Theory()]
        [InlineData(2, 0, 1, 1)]
        [InlineData(-1, 48, -2, 50)]
        [InlineData(2, 50, 1, 50)]
        [InlineData(-2, 49, -3, 50)]
        [InlineData(1, 1, 0, 2)]
        public void Product_Aged_Brie_Test(int sellIn, int quality, int sellInResult, int qualityResult) {
            String name = "Aged Brie";
            Item product = new Item() { Name = name, SellIn = sellIn, Quality = quality };
            GildedRose app = new GildedRose(new Item[] { product });
            app.UpdateQuality();
            Assert.Multiple(
                () => Assert.Equal(name, product.Name),
                () => Assert.Equal(sellInResult, product.SellIn),
                () => Assert.Equal(qualityResult, product.Quality)
                );
            product.Should().NotBeNull()
                .And.BeEquivalentTo(new { Name = name, SellIn = sellInResult, Quality = qualityResult }
                    //, options => options.ExcludingMissingMembers()
                    //, options => options.Excluding(o => o.Name)
                    //, options => options.Including(o => o.SellIn).Including(o => o.Quality)
                    );
        }
        [Theory()]
        [InlineData(1, 0, 1, 0)]
        [InlineData(0, 1, 0, 1)]
        [InlineData(-1, 1, -1, 1)]
        public void Product_Sulfuras_Test(int sellIn, int quality, int sellInResult, int qualityResult) {
            String name = "Sulfuras, Hand of Ragnaros";
            Item product = new Item() { Name = name, SellIn = sellIn, Quality = quality };
            GildedRose app = new GildedRose(new Item[] { product });
            app.UpdateQuality();
            Assert.Multiple(
                () => Assert.Equal(name, product.Name),
                () => Assert.Equal(sellInResult, product.SellIn),
                () => Assert.Equal(qualityResult, product.Quality)
                );
        }

        [Theory()]
        [InlineData(11, 0, 10, 1)]
        [InlineData(7, 1, 6, 3)]
        [InlineData(7, 49, 6, 50)]
        [InlineData(5, 3, 4, 6)]
        [InlineData(0, 3, -1, 0)]
        [InlineData(-1, 3, -2, 0)]
        public void Product_Passes_Test(int sellIn, int quality, int sellInResult, int qualityResult) {
            String name = "Backstage passes to a TAFKAL80ETC concert";
            Item product = new Item() { Name = name, SellIn = sellIn, Quality = quality };
            GildedRose app = new GildedRose(new Item[] { product });
            app.UpdateQuality();
            Assert.Multiple(
                () => Assert.Equal(name, product.Name),
                () => Assert.Equal(sellInResult, product.SellIn),
                () => Assert.Equal(qualityResult, product.Quality)
                );
        }

        //[Theory(Skip = "Pendiente de implementar")]
        //[InlineData(11, 10, 10, 8)]
        //[InlineData(7, 1, 6, 0)]
        //[InlineData(-5, 10, -6, 6)]
        //[InlineData(0, 3, -1, 0)]
        //public void Product_Conjured_Test(int sellIn, int quality, int sellInResult, int qualityResult) {
        //    String name = "Conjured Mana Cake";
        //    Item product = new Item() { Name = name, SellIn = sellIn, Quality = quality };
        //    GildedRose app = new GildedRose(new Item[] { product });
        //    app.UpdateQuality();
        //    Assert.Multiple(
        //        () => Assert.Equal(name, product.Name),
        //        () => Assert.Equal(sellInResult, product.SellIn),
        //        () => Assert.Equal(qualityResult, product.Quality)
        //        );
        //}
    }
}