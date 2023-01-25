using System;
using System.IO;
using IniMapper.Elements;

namespace IniMapper.Writer {
	public class IniWriter : IWriter, IDisposable {

		private readonly TextWriter _writer;

		private int _currentRow;

		/// <summary>
		/// Initialize a new instance of <see cref="IniWriter"/> class
		/// </summary>
		/// <param name="writer"></param>
		public IniWriter(TextWriter writer) {
			_writer = writer;
			_currentRow = 0;
		}

		/// <inheritdoc/>
		public void WriteSection(string name) {
			if (_currentRow > 0) {
				_writer.WriteLine();
			}
			_writer.Write($"[{name}]");
			_currentRow++;
		}

		/// <inheritdoc/>
		public void WriteProperty(string key, string value) {
			if (_currentRow > 0) {
				_writer.WriteLine();
			}
			_writer.Write($"{key}={value}");
			_currentRow++;
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public void Dispose() {
			_writer.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
