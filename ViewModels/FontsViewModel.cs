using FontViewer.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace FontViewer.ViewModels
{
	internal class FontsViewModel : ObservableObject
	{
		Dictionary<string, FontFamily> _fonts = new Dictionary<string, FontFamily>();
		FontFamilyWeights? _fontWeights = FontFamilyWeights.Empty;

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public FontsViewModel()
		{
			List<FontFamily> fonts = new List<FontFamily>();
			foreach (FontFamily family in Fonts.SystemFontFamilies)
			{
				fonts.Add(family);
				_fonts.Add(family.Source, family);
			}
			fonts.Sort(FontFamilyComparer.Comparer);

			Families = new SelectionList<FontFamily>(fonts);
			Families.PropertyChanged += OnFontFamilyChanged;
			Families.SelectedItem = this["Segoe UI"];
			
			Sizes = new SelectionList<double>(
				new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96 }
			);
			Sizes.SelectedItem = 24;

			Decorations = new SelectionList<NamedTextDecoration>
			(
				NamedTextDecoration.All
			);
			Decorations.SelectedItem = NamedTextDecoration.Default;

			Alignment = new SelectionList<TextAlignment>
			(
				new TextAlignment[] {TextAlignment.Left, TextAlignment.Right, TextAlignment.Center, TextAlignment.Justify}
			);
			Alignment.SelectedItem = TextAlignment.Left;
		}

		#region Properties

		/// <summary>
		/// Gets the <see cref="NamedTextDecoration"/> to apply to text.
		/// </summary>
		public SelectionList<NamedTextDecoration> Decorations
		{
			get;
		}

		static readonly FontStyle[] _fontStyles = new FontStyle[]
		{
			FontStyles.Normal,
			FontStyles.Italic,
			FontStyles.Oblique
		};

		/// <summary>
		/// Gets the <see cref="FontStyle"/> list.
		/// </summary>
		public SelectionList<FontStyle> Styles
		{
			get;
		} = new SelectionList<FontStyle>(_fontStyles);

		/// <summary>
		/// Gets the list of sizes to apply to a font.
		/// </summary>
		public SelectionList<double> Sizes
		{
			get;
		}

		/// <summary>
		/// Gets the list of system font families.
		/// </summary>
		public SelectionList<FontFamily> Families
		{
			get;
		}


		/// <summary>
		/// Gets the <see cref="FontFamilyWeights"/> for the selected <see cref="FontFamily"/>.
		/// </summary>
		public FontFamilyWeights? Weights
		{
			get => _fontWeights;
			private set
			{
				_fontWeights = value;
				OnPropertyChanged(WeightsChangedEventArgs);
			}
		}

		/// <summary>
		/// Gets the named <see cref="FontFamily"/>.
		/// </summary>
		/// <param name="name">The value of the <see cref="FontFamily.Source"/> to get.</param>
		/// <returns>A <see cref="FontFamily"/> where the <see cref="FontFamily.Source"/>
		/// matches the specified <paramref name="name"/>; otherwise, a null reference
		/// if a matching <see cref="FontFamily"/> was not found.
		/// </returns>
		public FontFamily? this[string name]
		{
			get
			{
				_fonts.TryGetValue(name, out FontFamily? family);
				return family;
			}
		}

		/// <summary>
		/// Gets the <see cref="Alignment"/> for displaying text.
		/// </summary>
		public SelectionList<TextAlignment> Alignment
		{
			get;
		}

		#endregion Properties

		#region Event handlers

		private void OnFontFamilyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if
			(
				object.ReferenceEquals(e, SelectionList<FontFamily>.SelectedValueChangedEventArgs)
			)
			{
				if (Families.SelectedItem != null)
				{
					Weights = FontFamilyWeights.CreateInstance(Families.SelectedItem);
				}
				else
				{
					Weights = null;
				}
			}
		}

		#endregion Event handlers

		#region Cached PropertyChangedEventArgs

		static readonly PropertyChangedEventArgs StylesChangedEventArgs = new PropertyChangedEventArgs(nameof(Styles));
		static readonly PropertyChangedEventArgs WeightsChangedEventArgs = new PropertyChangedEventArgs(nameof(Weights));

		#endregion Cached PropertyChangedEventArgs
	}
}
