using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace FontViewer.Model
{
	/// <summary>
	/// Provides a <see cref="FontWeight"/> list for a given <see cref="FontFamily"/>.
	/// </summary>
	internal class FontFamilyWeights : SelectionList<FontWeight>
	{
		/// <summary>
		/// Provides an empty instance of this class.
		/// </summary>
		static public readonly FontFamilyWeights Empty = new FontFamilyWeights(new List<FontWeight>());

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public FontFamilyWeights(IEnumerable<FontWeight> weights)
			: base(weights)
		{
		}

		/// <summary>
		/// Creates a new instance of this class.
		/// </summary>
		/// <param name="family">The <see cref="FontFamily"/> to query.</param>
		/// <returns>A <see cref="FontFamilyWeights"/> containing the <see cref="FontWeight"/>
		/// instances defined for the specified <paramref name="family"/>.</returns>
		public static FontFamilyWeights CreateInstance(FontFamily family)
		{
			List<FontWeight> list = new List<FontWeight>();
			Dictionary<int, FontWeight> weights = new Dictionary<int, FontWeight>();
			foreach (FamilyTypeface typeface in family.FamilyTypefaces)
			{
				FontWeight weight = typeface.Weight;
				if (!weights.TryGetValue(typeface.Weight.ToOpenTypeWeight(), out FontWeight notused))
				{
					weights.Add(weight.ToOpenTypeWeight(), weight);
					list.Add(weight);
				}
			}
			list.Sort(FontWeightComparer.Comparer);
			return new FontFamilyWeights(list);
		}
	}
}
