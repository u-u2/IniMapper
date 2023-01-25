using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using IniMapper.Attributes;
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
		public void Write<T>(T t) {
			var sectionGroup = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(p => p.GetCustomAttribute<IniAttribute>(false) != null)
				.GroupBy(p => p.GetCustomAttribute<IniAttribute>(false).SectionName,
				p => p,
				(k, e) => new {
					Ref = e,
					Name = k,
				}).ToArray();
			var separateCount = sectionGroup.Length - 1;
			var separated = 0;
			foreach (var section in sectionGroup) {
				WriteSection(section.Name);
				foreach (var property in section.Ref) {
					var key = property.GetCustomAttribute<IniAttribute>(false).Key;
					var value = property.GetValue(t);
					WriteProperty(key, value != null ? value.ToString() : string.Empty);
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
