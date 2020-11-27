using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace YoiBlazor.Test
{
    public class CssClassCollectionTest
    {
        [Fact]
        public void TestCssCollection()
        {
            CssClassCollection css = "a b c d";
            Assert.Equal("a b c d", css.ToString());

            CssClassCollection css1 = new[] { "a", "b", "c", "d" };
            Assert.Equal("a b c d", css1.ToString());

            CssClassCollection css2 = Css.Create.Add("a").Add("b").Add("c");
            Assert.Equal("a b c", css2.ToString());
        }

        [Fact]
        public void TestStyleCollection()
        {
            var indexGrama = new StyleCollection
            {
                ["a"] = "b",
                ["c"] = "d",
            };
            Assert.Equal("a:b;c:d", indexGrama.ToString());

            StyleCollection array = new[] { ("a", "b"), ("c", "d") };
            Assert.Equal("a:b;c:d", array.ToString());

            StyleCollection str = "a:b;c:d";
            Assert.Equal("a:b;c:d", str.ToString());

            StyleCollection s1 = Style.Create.Add("a", "b").Add("c", "d");
            Assert.Equal("a:b;c:d", s1.ToString());

            StyleCollection s2 = Style.Create.Add("q", "w").Add("a", "s");
            Assert.Equal("q:w;a:s", s2.ToString());
        }
    }
}
