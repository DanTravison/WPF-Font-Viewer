using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

namespace FontViewer.Model
{
	public class NamedColor
	{
		/// <summary>
		/// Provides a <see cref="NamedColor"/> for <see cref="Colors.Black"/>.
		/// </summary>
		static public readonly NamedColor Black = new NamedColor("Black", Colors.Black);

		/// <summary>
		/// Provides a <see cref="NamedColor"/> for <see cref="Colors.White"/>.
		/// </summary>
		static public readonly NamedColor White = new NamedColor("White", Colors.White);

		static NamedColor()
		{
			List<NamedColor> list = new List<NamedColor>();

			foreach (PropertyInfo info in typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static))
			{
				if (info.PropertyType == typeof(Color))
				{
					object? infoValue = info.GetValue(null);
					if (infoValue == null)
					{
						continue;
					}
					Color value = (Color)infoValue;
					NamedColor color = new NamedColor(info.Name, value);
					
					list.Add(color);
				}
			}
			list.Sort(NamedColorComparer.Comparers[ColorSortOrder.Name]);
			All = list;
		}

		private NamedColor(string name, Color color)
		{
			Name = name;
			Color = color;
			ARGB = string.Format
			(
				"#{0:X2}{1:X2}{2:X2}{3:X2}",
				(int)(color.A),
				(int)(color.R),
				(int)(color.G),
				(int)(color.B)
			);
		}

		SolidColorBrush? _brush;
		
		/// <summary>
		/// Gets the <see cref="SolidColorBrush"/> for the <see cref="Color"/>.
		/// </summary>
		public SolidColorBrush Brush
		{
			get
			{
				if (_brush == null)
				{
					_brush = new SolidColorBrush(Color);
				}
				return _brush;
			}
		}

		/// <summary>
		/// Gets the color's Name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the color's value.
		/// </summary>
		public Color Color { get; private set; }

		/// <summary>
		/// Gets the ARGB string.
		/// </summary>
		public string ARGB { get; private set; }

		/// <summary>
		/// Gets the <see cref="NamedColor"/> instances.
		/// </summary>
		public static IList<NamedColor> All
		{
			get;
			private set;
		}
	}
}
