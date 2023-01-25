﻿using System.Collections.Generic;

namespace IniMapper.Elements {
	public class Section : IniElement<string> {

		public string Name { get; set; }

		public IEnumerable<string> Keys => _keyToElement.Keys;

		/// <summary>
		/// key of property.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string this[string key] {
			get => _keyToElement[key];
			set => _keyToElement[key] = value;
		}

		/// <summary>
		/// Initialize a new instance of <see cref="Section"/> class
		/// </summary>
		public Section(string name) {
			Name = name;
		}

	}
}
