namespace Fizzler.Tests
{
    using NUnit.Framework;

    [TestFixture]
	public class Eq : SelectorBaseTest
	{

		[Test]
		public void No_Prefix_With_Digit()
		{
			var result = SelectList(":eq(5)");

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("p", result[0].Name);
		}
	
		[Test]
		public void Star_Prefix_With_Digit()
		{
			var result = SelectList("*:eq(1)");

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("head", result[0].Name);
		}

		[Test]
		public void Element_Prefix_With_Digit()
		{
			var result = SelectList("div:eq(1)");

			Assert.AreEqual(1, result.Count);
            Assert.AreEqual("someOtherDiv", result[0].Id);
		}
	}
}