using System.Collections.Generic;

namespace IniMapper.Elements {
	public class Section {

		private readonly Dictionary<string, string> _keyToValue;
		public string Name { get; private set; }
		public IReadOnlyDictionary<string, string> KeyToValue => _keyToValue;

		/// <summary>
		/// key of property.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string this[string key] {
			get => _keyToValue[key];
			set => _keyToValue[key] = value;
		}

		/// <summary>
		/// Initialize a new instance of <see cref="Section"/> class
		/// </summary>
		public Section(string name) {
			_keyToValue = new Dictionary<string, string>();
			Name = name;
		}

	}
}
