namespace IniMapper.Attributes {

	[AttributeUsage(AttributeTargets.Property,
		AllowMultiple = false,
		Inherited = false)]
	public class IniAttribute : Attribute {

		public string SectionName { get; private set; }

		public string Key { get; private set; }

		/// <summary>
		/// Initialize a new instance of <see cref="IniAttribute"/> class
		/// </summary>
		public IniAttribute(string sectionName, string key) {
			SectionName = sectionName;
			Key = key;
		}

	}
}
