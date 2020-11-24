using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HCS.Models;
using HCS.ViewModels;
using System.Globalization;

namespace HCS.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class ItemDetailPage : ContentPage
	{

		ItemDetailViewModel viewModel;
		public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = this.viewModel = viewModel;
		}

		private void Entry_Completed_Value(object sender, EventArgs e)
		{
			Help(sender, (x,y) => { x.Summ = y; return x; });
		}

		private void Entry_Completed_First(object sender, EventArgs e)
		{
			Help(sender, (x, y) => { x.First = y; return x; });
		}

		private void Entry_Completed_Second(object sender, EventArgs e)
		{
			Help(sender, (x, y) => { x.Second = y; return x; });
		}

		private void Help(object sender, Func<Item, int, Item> lambda) {
			if (Int32.TryParse(((Entry)sender).Text, out int value))
			{
				viewModel.Item = lambda(viewModel.Item, value);
			}
			else
			{
			}
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
			await Navigation.PopAsync();
		}

		private void Button_Clicked_Transfer_Value(object sender, EventArgs e)
		{
			viewModel.Item.First = viewModel.Item.Second;
			First.Text = viewModel.Item.First.ToString();
		}
	}

	public class BooleanConverter : IValueConverter, IMarkupExtension
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}