using System.Collections.Generic;
using System.Windows;

namespace FontViewer.Model
{
	internal class NamedTextDecoration
	{
		public static NamedTextDecoration Default = new NamedTextDecoration();
		static NamedTextDecoration()
		{
			List<NamedTextDecoration> list = new List<NamedTextDecoration>()
			{
				Default,
				new NamedTextDecoration(nameof(TextDecorations.Baseline), TextDecorations.Baseline),
				new NamedTextDecoration(nameof(TextDecorations.Underline), TextDecorations.Underline),
				new NamedTextDecoration(nameof(TextDecorations.OverLine), TextDecorations.OverLine),
				new NamedTextDecoration(nameof(TextDecorations.Strikethrough), TextDecorations.Strikethrough)
			};
			All = list;
		}

		private NamedTextDecoration()
		{
			Name = "None";
			Value = null;
		}

		private NamedTextDecoration(string name, TextDecorationCollection value)
		{
			Name = name;
			Value = value;
		}

		public TextDecorationCollection? Value
		{
			get;
		}

		public string Name
		{
			get;
		}

		public static IEnumerable<NamedTextDecoration> All
		{
			get;
		}
	}
}
