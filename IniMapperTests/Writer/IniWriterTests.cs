using IniMapper.Elements;
using IniMapperTests.Entity;
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

		[TestMethod()]
		public void WriteEntityTest() {
			var fileName = "example_entity.ini";
			var example = new Example() {
				Name = "Marry",
				Organization = "Test",
				Server = "test.tet",
				Port = 9000,
				File = "file.dat",
			};
			using (var writer = new IniWriter(new StreamWriter(fileName, false))) {
				writer.Write(example);
			}
			var result = File.ReadAllText(fileName);
			var expect = """
				[owner]
				na\=me=Marry
				organization=Test

				[database]
				server=test.tet
				port=9000
				file=file.dat
				""";
			Assert.AreEqual(expect, result);
		}

	}
}