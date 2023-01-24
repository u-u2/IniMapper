using IniMapper.Elements;
using IniMapperTests.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IniMapper.Reader.Tests {
	[TestClass()]
	public class IniReaderTests {

		private static readonly string s_fileName = "example.ini";

		[TestMethod()]
		public void ReadSectionsTest() {
			List<Section> sections;
			using (var reader = new IniReader(new StreamReader(s_fileName))) {
				sections = reader.ReadSections();
			}
			var sectionCount = 2;
			Assert.AreEqual(sectionCount, sections.Count);
		}

		[TestMethod]
		public void ReadIniTest() {
			Ini ini;
			using (var reader = new IniReader(new StreamReader(s_fileName))) {
				ini = reader.ReadIni();
			}
			Assert.IsTrue(ini.TryGetElement("owner", out Section owner));
			Assert.IsTrue(owner.TryGetElement("name", out string name));
			Assert.IsTrue(owner.TryGetElement("organization", out string organization));
			Assert.IsTrue(ini.TryGetElement("database", out Section database));
			Assert.IsTrue(database.TryGetElement("server", out string server));
			Assert.IsTrue(database.TryGetElement("port", out string port));
			Assert.IsTrue(database.TryGetElement("file", out string file));
		}

		[TestMethod()]
		public void ReadValueTest() {
			Example example;
			using (var reader = new IniReader(new StreamReader(s_fileName))) {
				example = reader.ReadValue<Example>();
			}
			Assert.AreEqual(example.Name, "John Doe");
			Assert.AreEqual(example.Organization, "Acme Widgets Inc.");
			Assert.AreEqual(example.Server, "localhost");
			Assert.AreEqual(example.Port, 143);
			Assert.AreEqual(example.File, "\"payroll.dat\"");
		}
	}
}