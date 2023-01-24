using IniMapper.Attributes;

namespace IniMapperTests.Entity {
	public class Example {

		[Ini("owner", "name")]
		public string? Name { get; set; }

		[Ini("owner", "organization")]
		public string? Organization { get; set; }

		[Ini("database", "server")]
		public string? Server { get; set; }

		[Ini("database", "port")]
		public int Port { get; set; }

		[Ini("database", "file")]
		public string? File { get; set; }

	}
}
