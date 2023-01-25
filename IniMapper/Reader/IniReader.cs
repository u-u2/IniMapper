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
				if (line.StartsWith('[') && line.EndsWith(']')) {
					sections.Push(new Section(line.Trim()[1..^1]));
				}
				if (line.Contains('=')) {
					var section = sections.Peek();
					var keyValue = line.Split('=')
						.Select(x => x.Trim())
						.ToArray();
					section[keyValue[0]] = keyValue[1];
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
