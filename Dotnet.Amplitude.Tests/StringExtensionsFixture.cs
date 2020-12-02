using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dotnet.Amplitude.Tests
{
    [TestClass]
    public class StringExtensionsFixtures
    {
        [TestMethod]
        public void ToSnakeCase_CorrectlyConverts()
        {
            Assert.AreEqual("foo_bar", "fooBar".ToSnakeCase());
            Assert.AreEqual("foo_bar", "FooBar".ToSnakeCase());
            Assert.AreEqual("f_o_o_b_a_r", "FOOBAR".ToSnakeCase());
            Assert.AreEqual("foo__bar", "foo_Bar".ToSnakeCase());
        }
    }
}
