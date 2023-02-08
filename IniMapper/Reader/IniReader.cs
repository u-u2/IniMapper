using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IniMapper.Attributes;
using IniMapper.Elements;

namespace IniMapper.Reader {
	public class IniReader : IReader, IDisposable {

		private readonly TextReader _reader;

		/// <summary>
		/// Initialize a new instance of <see cref="IniReader"/> class
		/// </summary>
		/// <param name="reader"></param>
		public IniReader(TextReader reader) {
			_reader = reader;
		}

		/// <inheritdoc/>
		public List<Section> ReadSections() {
			string line;
			var sections = new Stack<Section>();
			while ((line = _reader.ReadLine()) != null) {
				var found = false;
				for (int i = 0; i < line.Length; i++) {
					if (found) {
						break;
					}
					var ch = line[i];
					switch (ch) {
						case '[':
							var end = line.IndexOf(']');
							var name = line.Substring(i + 1, end - 1);
							sections.Push(new Section(name.Trim()));
							found = true;
							break;
						case '=':
							if (i > 0 && line[i - 1] != '\\') {
								var key = line.Substring(0, i);
								var value = line.Substring(i + 1, line.Length - (i + 1));
								sections.Peek()[key.Trim()] = value.Trim();
								found = true;
							}
							break;
					}
				}
			}
			return sections.Reverse().ToList();
		}

		/// <inheritdoc/>
		public Ini ReadIni() {
			var ini = new Ini();
			foreach (var section in ReadSections()) {
				ini[section.Name] = section;
			}
			return ini;
		}

		/// <inheritdoc/>
		public T ReadValue<T>() where T : new() {
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(p => p.GetCustomAttribute<IniAttribute>(false) != null)
				.Select(p => {
					var attribute = p.GetCustomAttribute<IniAttribute>(false);
					return new {
						Ref = p,
						attribute.SectionName,
						attribute.Key,
					};
				});
			var ini = ReadIni();
			var obj = new T();
			foreach (var property in properties) {
				var value = Convert.ChangeType(
					ini[property.SectionName][property.Key],
					property.Ref.PropertyType);
				property.Ref.SetValue(obj, value);
			}
			return obj;
		}

		/// <inheritdoc/>
		public void Dispose() {
			_reader.Dispose();
			GC.SuppressFinalize(this);
		}

	}
}
