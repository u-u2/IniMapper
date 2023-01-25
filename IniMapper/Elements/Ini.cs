namespace IniMapper.Elements {
	public class Ini : IniElement<Section> {

		public IEnumerable<Section> Sections => _keyToElement.Values;

		public int SectionCount => _keyToElement.Count;

		/// <summary>
		/// name of section.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override Section this[string key] {
			get {
				if (TryGetElement(key, out Section section)) {
					return section;
				}
				section = new Section(key);
				_keyToElement.Add(key, section);
				return section;
			}
			set {
				_keyToElement[key] = value;
			}
		}

		public Ini() {
		}

	}
}
