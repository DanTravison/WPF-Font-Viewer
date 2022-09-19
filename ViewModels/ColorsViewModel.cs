using FontViewer.Model;
using System.ComponentModel;

namespace FontViewer.ViewModels
{
	internal class ColorsViewModel : ObservableObject
	{
		NamedColor? _background;
		NamedColor? _foreground;
		bool _setBackground;

		public ColorsViewModel()
		{
			Colors = new SelectionList<NamedColor>(NamedColor.All);

			_foreground = NamedColor.Black;
			_background = NamedColor.White;
			Colors.SelectedItem = NamedColor.Black;
			Colors.PropertyChanged += OnColorChanged;

            SortNames = new ColorSortOrderList();
            SortNames.SelectedItem = ColorSortOrder.Name;
            SortNames.PropertyChanged += OnSortOrderChanged;
			
		}

		#region Properties

		/// <summary>
		/// Gets the <see cref="NamedColor"/> to use for the example text's background color.
		/// </summary>
		public NamedColor? Background
		{
			get => _background;
			set
			{
				if (!object.ReferenceEquals(value, _background))
				{
					_background = value;
					OnPropertyChanged(BackgroundChangedEventArgs);
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="NamedColor"/> to use for the example text's background color.
		/// </summary>
		public NamedColor? Foreground
		{
			get => _foreground;
			set
			{
				if (!object.ReferenceEquals(value, _foreground))
				{
					_foreground = value;
					OnPropertyChanged(ForegroundChangedEventArgs);
				}
			}
		}

		/// <summary>
		/// Gets the value indicating of the background color should be set.
		/// </summary>
		/// <value>
		/// true if the background color should be set; otherwise, false if the foreground
		/// color should be set.
		/// </value>
		/// <remarks>
		/// This property is used by <see cref="OnColorChanged"/> to update either
		/// the <see cref="Background"/> or <see cref="Foreground"/> properties.
		/// </remarks>
		public bool SetBackground
		{
			get => _setBackground;
			set
			{
				if (value != _setBackground)
				{
					_setBackground = value;
					OnPropertyChanged(SetBackgroundChangedEventArgs);
				}
			}
		}

		/// <summary>
		/// Gets the list of <see cref="NamedColor"/> values.
		/// </summary>
		public SelectionList<NamedColor> Colors
		{
			get;
		}

		/// <summary>
		/// Gets the list of color sort orders
		/// </summary>
		public ColorSortOrderList SortNames
		{
			get;
		}

		#endregion Properties

		#region Event handlers

		private void OnColorChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (_setBackground)
			{
				Background = Colors.SelectedItem;
			}
			else
			{
				Foreground = Colors.SelectedItem;
			}
		}

		private void OnSortOrderChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (object.ReferenceEquals(e, SelectionList<ColorSortOrder>.SelectedValueChangedEventArgs))
			{
				ColorSortOrder value = SortNames.SelectedItem;
				NamedColorComparer comparer = NamedColorComparer.Comparers[value];
				Colors.Sort(comparer);
			}
		}

		#endregion Event handlers

		#region Cached PropertyChangedEventArgs

		static readonly PropertyChangedEventArgs BackgroundChangedEventArgs = new PropertyChangedEventArgs(nameof(Background));
		static readonly PropertyChangedEventArgs SetBackgroundChangedEventArgs = new PropertyChangedEventArgs(nameof(SetBackground));
		static readonly PropertyChangedEventArgs ForegroundChangedEventArgs = new PropertyChangedEventArgs(nameof(Foreground));

		#endregion Cached PropertyChangedEventArgs
	}
}
