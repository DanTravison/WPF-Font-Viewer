using System.Collections.Generic;
using System.Windows;

namespace FontViewer.Model
{
	internal class FontWeightComparer : IComparer<FontWeight>
	{
		/// <summary>
		/// Provides an instance of a <see cref="FontWeightComparer"/>.
		/// </summary>
		public static readonly FontWeightComparer Comparer = new FontWeightComparer();

		private FontWeightComparer()
		{
		}

		/// <summary>
		/// Compares two <see cref="FontWeight"/> instances.
		/// </summary>
		/// <param name="x">The first <see cref="FontWeight"/> to compare.</param>
		/// <param name="y">The second <see cref="FontWeight"/> to compare.</param>
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
		public int Compare(FontWeight x, FontWeight y)
		{
			return x.ToOpenTypeWeight() - y.ToOpenTypeWeight();
		}
	}
}
