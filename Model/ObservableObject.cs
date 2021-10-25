using System.ComponentModel;

namespace FontViewer.Model
{
	internal abstract class ObservableObject : INotifyPropertyChanged
	{
		protected ObservableObject()
		{
		}

		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}

		/// <summary>
		/// Occurs when a property changes.
		/// </summary>
		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
