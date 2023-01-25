using System.Collections.Generic;
using IniMapper.Elements;

namespace IniMapper.Reader {
	public interface IReader {

		/// <summary>
		/// Read all sections.
		/// </summary>
		/// <returns></returns>
		List<Section> ReadSections();


		/// <summary>
		/// Read as <see cref="Ini"/>.
		/// </summary>
		/// <returns></returns>
		Ini ReadIni();


		/// <summary>
		/// Read as <see cref="T"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T ReadValue<T>() where T : new();

	}
}
