using System;
using System.ComponentModel;
using HCS.Models;
using PropertyChanged;

namespace HCS.ViewModels
{
	public class SettingsDetailTariffViewModel : BaseViewModel
	{
		public Tariff Item { get; set; }

		public SettingsDetailTariffViewModel(Tariff item)
		{
			Title = item.Name;
			Item = new Tariff(item);
		}
	}
}
