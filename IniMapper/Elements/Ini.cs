using System.Collections.Generic;

namespace IniMapper.Elements {
	public class Ini {

		private readonly Dictionary<string, Section> _keyToSection;
		public IReadOnlyDictionary<string, Section> KeyToSection => _keyToSection;

		/// <summary>
		/// name of section.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Section this[string key] {
			get {
				if (_keyToSection.TryGetValue(key, out var section)) {
					return section;
				}
				section = new Section(key);
				_keyToSection.Add(key, section);
				return section;
			}
			set {
				_keyToSection[key] = value;
			}
		}

		/// <summary>
		/// Initialize a new instance of <see cref="Ini"/> class
		/// </summary>
		public Ini() {
			_keyToSection = new Dictionary<string, Section>();
		}

	}
}
