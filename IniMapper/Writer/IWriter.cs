using IniMapper.Elements;

namespace IniMapper.Writer {
	internal interface IWriter {

		/// <summary>
		/// write section. insert brackets automatically.
		/// </summary>
		/// <param name="name"></param>
		public void WriteSection(string name);

		/// <summary>
		/// write key value pair. insert equal sign automatically.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void WriteProperty(string key, string value);

		/// <summary>
		/// write ini data.
		/// </summary>
		/// <param name="ini"></param>
		public void Write(Ini ini);

	}
}
