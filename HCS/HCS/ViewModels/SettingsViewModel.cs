using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HCS.Models;
using HCS.Views;
using System.Collections.Generic;
using System.Linq;

namespace HCS.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public Tariffs Items { get; set; }
		public Command LoadItemsCommand { get; set; }
		public SettingsViewModel()
		{
			Title = "Тарифы";
			Items = Task.Run(async () => await DataStore.GetTariffsAsync(true)).Result;

			MessagingCenter.Subscribe<SettingsDetailTariffsElectricPowerPage, TariffsElectricPower>(this, "UpdateTariff", async (obj, detailsItem) =>
			{
				Items.ChangeElectricPower(detailsItem);
				MessagingCenter.Send(this, "UpdateTotal", await DataStore.UpdateTariffsAsync());
			});

			MessagingCenter.Subscribe<SettingsDetailTariffPage, Tariff>(this, "UpdateTariff", async (obj, detailsItem) =>
			{
				switch (detailsItem.Id)
				{
					case ItemsType.NaturalGas:
						Items.ChangeGas(detailsItem.Value);
						break;
					case ItemsType.Water:
						Items.ChangeWater(detailsItem.Value);
						break;
					case ItemsType.HotWater:
						Items.ChangeHotWater(detailsItem.Value);
						break;
					default:
						break;
				}
				MessagingCenter.Send(this, "UpdateTotal", await DataStore.UpdateTariffsAsync());
			});
		}

	}
}