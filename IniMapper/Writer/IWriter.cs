using IniMapper.Elements;

namespace IniMapper.Writer {
	internal interface IWriter {

		/// <summary>
		/// Write section. insert brackets automatically.
		/// </summary>
		/// <param name="name"></param>
		public void WriteSection(string name);

		/// <summary>
		/// Write key value pair. insert equal sign automatically.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void WriteProperty(string key, string value);

		/// <summary>
		/// Write ini.
		/// </summary>
		/// <param name="ini"></param>
		public void Write(Ini ini);


		/// <summary>
		/// Write <see cref="T"/> class to ini.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t">Ini entity</param>
		public void Write<T>(T t);

	}
}
