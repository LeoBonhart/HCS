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
	public partial class SettingsDetailTariffsElectricPowerPage : ContentPage
	{

		SettingsDetailTariffsElectricPowerViewModel viewModel;
		public SettingsDetailTariffsElectricPowerPage(SettingsDetailTariffsElectricPowerViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = this.viewModel = viewModel;
		}

		private void Entry_Completed_Less150kW(object sender, EventArgs e)
		{
			Help(sender, (x,y) => { x.Less150kW = y; return x; });
		}

		private void Entry_Completed_Ower150kW(object sender, EventArgs e)
		{
			Help(sender, (x, y) => { x.Ower150kW = y; return x; });
		}

		private void Entry_Completed_Ower800kW(object sender, EventArgs e)
		{
			Help(sender, (x, y) => { x.Ower800kW = y; return x; });
		}


		private void Help(object sender, Func<TariffsElectricPower, double, TariffsElectricPower> lambda) {
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