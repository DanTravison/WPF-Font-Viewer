using System.Collections.Generic;
using System.Reflection;
using System.Windows;
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

			List<NamedColor> systemColors = new List<NamedColor>();
            foreach (PropertyInfo info in typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static))
			{
				if (info.PropertyType == typeof(SolidColorBrush))
				{
					object? infoValue = info.GetValue(null);
					if (infoValue is SolidColorBrush brush)
					{
                        NamedColor color = new NamedColor(info.Name, brush);
                        systemColors.Add(color);
                    }
				}
			}

			systemColors.Sort(NamedColorComparer.Comparers[ColorSortOrder.Name]);
			list.AddRange(systemColors);

			All = list;
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="brush">The <see cref="SystemColors"/> property.</param>
		private NamedColor(string name, SolidColorBrush brush)
			: this(name, brush.Color)
		{
			_brush = brush;
			IsSystemColor = true;
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
		public string Name { get; }

		/// <summary>
		/// Gets the color's value.
		/// </summary>
		public Color Color { get; }

		/// <summary>
		/// Gets the ARGB string.
		/// </summary>
		public string ARGB { get; }

		/// <summary>
		/// Indicates this instance was created from <see cref="SystemColors"/>.
		/// </summary>
		public bool	IsSystemColor { get; }

		/// <summary>
		/// Gets the <see cref="NamedColor"/> instances.
		/// </summary>
		public static IList<NamedColor> All
		{
			get;
		}
	}
}
