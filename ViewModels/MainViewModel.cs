using FontViewer.Model;
using System.ComponentModel;

namespace FontViewer.ViewModels
{
	internal class MainViewModel : ObservableObject
	{
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public MainViewModel()
		{
		}

		#region Properties

		/// <summary>
		/// Gets the <see cref="ColorsViewModel"/>.
		/// </summary>
		public ColorsViewModel Colors
		{
			get => Models.Colors;
		}

		/// <summary>
		/// Gets the <see cref="FontsViewModel"/>.
		/// </summary>
		public FontsViewModel Fonts
		{
			get => Models.Fonts;
		}

		#endregion Properties
	}
}
