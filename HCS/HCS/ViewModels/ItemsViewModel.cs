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
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableCollection<Item> Items { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Total = "0";
			Title = "Платежка ЖКХ";
			Items = new ObservableCollection<Item>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, detailsItem) =>
			{
				var xx = Items.IndexOf(Items.FirstOrDefault(x => x.Id == detailsItem.Id));
				Items[xx] = detailsItem;
				Total = await DataStore.UpdateItemAsync(detailsItem);
			});

			MessagingCenter.Subscribe<SettingsViewModel, string>(this, "UpdateTotal", (obj, newTotal) =>
			{
				Total = newTotal;
			});
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items.Values)
				{
					Items.Add(item);
					await DataStore.CalculateTariffAsync(item);
				}
				Total = items.Total();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

	}
}