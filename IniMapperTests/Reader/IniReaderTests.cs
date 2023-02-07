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
			Assert.IsTrue(ini.KeyToSection.TryGetValue("owner", out Section owner));
			Assert.IsTrue(owner.KeyToValue.TryGetValue("na\\=me", out string name));
			Assert.AreEqual("John Doe", name);
			Assert.IsTrue(owner.KeyToValue.TryGetValue("organization", out string organization));
			Assert.AreEqual("Acme Widgets Inc=.", organization);
			Assert.IsTrue(ini.KeyToSection.TryGetValue("database", out Section database));
			Assert.IsTrue(database.KeyToValue.TryGetValue("server", out string server));
			Assert.AreEqual("localhost", server);
			Assert.IsTrue(database.KeyToValue.TryGetValue("port", out string port));
			Assert.AreEqual("143", port);
			Assert.IsTrue(database.KeyToValue.TryGetValue("file", out string file));
			Assert.AreEqual("\"pay=roll.dat\"", file);
		}

		[TestMethod()]
		public void ReadValueTest() {
			Example example;
			using (var reader = new IniReader(new StreamReader(s_fileName))) {
				example = reader.ReadValue<Example>();
			}
			Assert.AreEqual("John Doe", example.Name);
			Assert.AreEqual("Acme Widgets Inc=.", example.Organization);
			Assert.AreEqual("localhost", example.Server);
			Assert.AreEqual(143, example.Port);
			Assert.AreEqual("\"pay=roll.dat\"", example.File);
		}
	}
}