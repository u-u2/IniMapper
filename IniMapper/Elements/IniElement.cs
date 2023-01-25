using System.Collections.Generic;

namespace IniMapper.Elements {
	public abstract class IniElement<T> {

		protected readonly Dictionary<string, T> _keyToElement = new();

		public abstract T this[string key] { get; set; }

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">the key of the value to get</param>
		/// <param name="value">contains the value associated with specified key or default value.</param>
		/// <returns>true if the <see cref="Dictionary{string, T}"/> contains with the specified key. otherwise false.</returns>
		public virtual bool TryGetElement(string key, out T value) {
			if (_keyToElement.ContainsKey(key)) {
				value = _keyToElement[key];
				return true;
			}
			value = default;
			return false;
		}

	}
}
