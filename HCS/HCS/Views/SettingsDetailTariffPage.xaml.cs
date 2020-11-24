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
	public partial class SettingsDetailTariffPage : ContentPage
	{

		SettingsDetailTariffViewModel viewModel;
		public SettingsDetailTariffPage(SettingsDetailTariffViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = this.viewModel = viewModel;
		}

		private void Entry_Completed(object sender, EventArgs e)
		{
			Help(sender, (x,y) => { x.Value = y; return x; });
		}


		private void Help(object sender, Func<Tariff, double, Tariff> lambda) {
			if (Double.TryParse(((Entry)sender).Text, out double value))
			{
				viewModel.Item = lambda(viewModel.Item, value);
			}
			else
			{
			}
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "UpdateTariff", viewModel.Item);
			await Navigation.PopAsync();
		}

	}
}