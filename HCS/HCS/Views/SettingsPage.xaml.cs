using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HCS.ViewModels;
using HCS.Models;

namespace HCS.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class SettingsPage : ContentPage
	{
		SettingsViewModel viewModel;
		public SettingsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new SettingsViewModel();
		}

		async void TapElectricPower(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new SettingsDetailTariffsElectricPowerPage(new SettingsDetailTariffsElectricPowerViewModel(viewModel.Items.ElectricPower)));
		}

		async void TapGas(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new SettingsDetailTariffPage(new SettingsDetailTariffViewModel(viewModel.Items.Gas)));
		}

		async void TapWater(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new SettingsDetailTariffPage(new SettingsDetailTariffViewModel(viewModel.Items.Water)));
		}

		async void TapHotWater(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new SettingsDetailTariffPage(new SettingsDetailTariffViewModel(viewModel.Items.HotWater)));
		}
	}
}