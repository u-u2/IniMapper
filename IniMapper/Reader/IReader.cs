using IniMapper.Elements;

namespace IniMapper.Reader {
	public interface IReader {

		List<Section> ReadSections();

		Ini ReadIni();

		T ReadValue<T>() where T : new();

	}
}
