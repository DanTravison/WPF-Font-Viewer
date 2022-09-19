using System;
using System.Collections.Generic;

namespace FontViewer.Model
{

	/// <summary>
	/// Provides an <see cref="IComparable{NamedColor}"/> for comparing two <see cref="NamedColor"/>
	/// instances by <see cref="NamedColor.Name"/>.
	/// </summary>
	public class NamedColorComparer : IComparer<NamedColor>
	{
		public static readonly Dictionary<ColorSortOrder, NamedColorComparer> Comparers = new Dictionary<ColorSortOrder, NamedColorComparer>()
		{
			{ColorSortOrder.Hue, new NamedColorComparer(ColorSortOrder.Hue)},
			{ColorSortOrder.Brightness, new NamedColorComparer(ColorSortOrder.Brightness)},
			{ColorSortOrder.Saturation, new NamedColorComparer(ColorSortOrder.Saturation)},
			{ColorSortOrder.ARGB, new NamedColorComparer(ColorSortOrder.ARGB)},
			{ColorSortOrder.Name,  new NamedColorComparer()}
		};

		ColorSortOrder _comparisonType = ColorSortOrder.Name; 
		StringComparer _comparer = StringComparer.InvariantCulture;

		private NamedColorComparer(ColorSortOrder type)
		{
			_comparisonType = type;
		}

		private NamedColorComparer()
			: this (ColorSortOrder.Name)
		{
			_comparer = StringComparer.OrdinalIgnoreCase;
		}

		/// <summary>
		/// Compares two <see cref="NamedColor"/> instances.
		/// </summary>
		/// <param name="x">The first <see cref="NamedColor"/> to compare.</param>
		/// <param name="y">The second <see cref="NamedColor"/> to compare.</param>
        /// <returns>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Value</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Less than zero</term>
        ///         <description><paramref name="x"/> is less than <paramref name="y"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>Zero</term>
        ///         <description>This <paramref name="x"/> is equal to <paramref name="y"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>Greater than zero.</term>
        ///         <description><paramref name="x"/> is greater than <paramref name="y"/>.</description>
        ///     </item>
        /// </list>
        /// </returns>		
		public int Compare(NamedColor? x, NamedColor? y)
		{
			int result;
			if (x == null && y == null)
			{
				result = 0;
			}
			else if (x == null)
			{
				result = -1;
			}
			else if (y == null)
			{
				result = 1;
			}
			else
			{
				switch (_comparisonType)
				{
					case ColorSortOrder.Brightness:
						result = (int)(x.Color.GetBrightness() - y.Color.GetBrightness());
						break;

					case ColorSortOrder.Saturation:
						result = (int)(x.Color.GetSaturation() - y.Color.GetSaturation());
						break;

					case ColorSortOrder.Hue:
						result = (int)(x.Color.GetHue() - y.Color.GetHue());
						break;

					case ColorSortOrder.ARGB:
						result = StringComparer.OrdinalIgnoreCase.Compare(x.ARGB, y.ARGB);
						break;

					default:
						int left = x.IsSystemColor ? 1 : 0;
						int right = y.IsSystemColor ? 1 : 0;
						result = left - right;
						if (result == 0)
						{
                            result = _comparer.Compare(x.Name, y.Name);
                        }
						break;
				}
			}
			return result;

		}
	}
}
