using System;
using System.Collections.Generic;

namespace FontViewer.Model
{
	/// <summary>
	/// Provides a <see cref="SelectionList{ColorSortOrders}"/>.
	/// </summary>
	internal class ColorSortOrderList : SelectionList<ColorSortOrder>
	{
		static readonly List<ColorSortOrder> _sortOrders = new List<ColorSortOrder>();

		static ColorSortOrderList()
		{
			foreach (ColorSortOrder sortOrder in Enum.GetValues(typeof(ColorSortOrder)))
			{
				_sortOrders.Add(sortOrder);
			}
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public ColorSortOrderList()
			: base(_sortOrders)
		{
		}
	}
}
