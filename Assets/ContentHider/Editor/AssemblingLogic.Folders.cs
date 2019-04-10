namespace Assets.ContentHider.Editor{
		public class AssemblerFolder
		{
			public string Source { get; private set; }
			public string Target { get; private set; }

			public AssemblerFolder(string source, string target)
			{
				Source = source;
				Target = target;
			}
	}
}