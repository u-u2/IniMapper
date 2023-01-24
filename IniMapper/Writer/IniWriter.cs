using IniMapper.Elements;

namespace IniMapper.Writer {
	public class IniWriter : IWriter, IDisposable {

		private readonly TextWriter _writer;

		private int _currentRow;

		public IniWriter(TextWriter writer) {
			_writer = writer;
			_currentRow = 0;
		}

		public void WriteSection(string name) {
			if (_currentRow > 0) {
				_writer.WriteLine();
			}
			_writer.Write($"[{name}]");
			_currentRow++;
		}

		public void WriteProperty(string key, string value) {
			if (_currentRow > 0) {
				_writer.WriteLine();
			}
			_writer.Write($"{key}={value}");
			_currentRow++;
		}

		public void Write(Ini ini) {
			var separateCount = ini.SectionCount - 1;
			var separated = 0;
			foreach (var section in ini.Sections) {
				WriteSection(section.Name);
				foreach (var key in section.Keys) {
					WriteProperty(key, ini[section.Name][key]);
				}
				if (separated < separateCount) {
					_writer.WriteLine();
					separated++;
				}
			}
		}

		public void Dispose() {
			_writer.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
