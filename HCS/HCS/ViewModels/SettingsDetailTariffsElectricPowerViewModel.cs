using System;
using System.ComponentModel;
using HCS.Models;
using PropertyChanged;

namespace HCS.ViewModels
{
	public class SettingsDetailTariffsElectricPowerViewModel : BaseViewModel
	{
		public TariffsElectricPower Item { get; set; }

		public SettingsDetailTariffsElectricPowerViewModel(TariffsElectricPower item)
		{
			Title = item.Name;
			Item = new TariffsElectricPower(item);
		}
	}
}
