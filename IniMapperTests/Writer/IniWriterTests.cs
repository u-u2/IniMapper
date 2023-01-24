using IniMapper.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IniMapper.Writer.Tests {
	[TestClass()]
	public class IniWriterTests {

		private static readonly string s_fileName = "test.ini";

		[TestMethod()]
		public void WriteTest() {
			var ini = new Ini();
			ini["Section1"]["Key1"] = "Value1";
			ini["Section2"]["Key2"] = "Value2";
			using (var writer = new IniWriter(new StreamWriter(s_fileName, false))) {
				writer.Write(ini);
			}
			var result = File.ReadAllText(s_fileName);
			var expect = """
				[Section1]
				Key1=Value1

				[Section2]
				Key2=Value2
				""";
			Assert.AreEqual(expect, result);
		}

	}
}