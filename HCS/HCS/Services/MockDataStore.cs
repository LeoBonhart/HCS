using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCS.Models;
using Newtonsoft.Json;
using Plugin.Settings;

namespace HCS.Services
{
	public class MockDataStore : IDataStore<ItemsType, Item, Items, Tariffs>
	{
		readonly Items items;
		readonly Tariffs tariffs;

		public MockDataStore()
		{
			tariffs = new Tariffs();
			items = new Items
			{
				Values = new List<Item>()
					{
						new Item { Id = ItemsType.Rent, Text = "Квартплата", Summ = 283, ShowDifference = false, ShowSumm = true },
						new Item { Id = ItemsType.ElectricPower, Text = "Электроэнергия", First = 4812, Second = 5123, ShowDifference = true, ShowSumm = true },
						new Item { Id = ItemsType.NaturalGas, Text = "Природный газ", First = 2540, Second = 2578, ShowDifference = true, ShowSumm = true },
						new Item { Id = ItemsType.Water, Text = "Вода/Стоки", First = 1779, Second = 1788, ShowDifference = true, ShowSumm = true },
						new Item { Id = ItemsType.HotWater, Text = "Горячая вода", First = 1779, Second = 1788, ShowDifference = true, ShowSumm = true },
						new Item { Id = ItemsType.Heating, Text = "Отопление", Summ = 794, ShowDifference = false, ShowSumm = true },
						new Item { Id = ItemsType.GarbageRemoval, Text = "Вывоз мусора", Summ = 49, ShowDifference = false, ShowSumm = true }
					}
			};
			string history = CrossSettings.Current.GetValueOrDefault("history", null);
			if (history != null)
			{
				Items tmp = JsonConvert.DeserializeObject<Items>(history);
				for (int i = 0; i < tmp.Values.Count; i++)
				{
					items.Values[i].Summ = tmp.Values[i].Summ;
					items.Values[i].First = tmp.Values[i].First;
					items.Values[i].Second = tmp.Values[i].Second;
				}
			}
		}

		public async Task<string> UpdateItemAsync(Item item)
		{
			items.UpdateValue(item);
			await CalculateTariffAsync(item);
			CrossSettings.Current.AddOrUpdateValue("history", JsonConvert.SerializeObject(items));
			return await Task.FromResult(items.Total());
		}

		public async Task<string> UpdateTariffsAsync()
		{
			await UpdateItemAsync(items.Values.FirstOrDefault(x => x.Id == ItemsType.ElectricPower));
			await UpdateItemAsync(items.Values.FirstOrDefault(x => x.Id == ItemsType.Water));
			await UpdateItemAsync(items.Values.FirstOrDefault(x => x.Id == ItemsType.HotWater));
			await UpdateItemAsync(items.Values.FirstOrDefault(x => x.Id == ItemsType.NaturalGas));
			return await Task.FromResult(items.Total());
		}

		public async Task<bool> CalculateTariffAsync(Item item)
		{
			switch (item.Id)
			{
				case ItemsType.ElectricPower:
					tariffs.CalculateElectricPower(ref item);
					break;
				case ItemsType.NaturalGas:
					tariffs.CalculateGas(ref item);
					break;
				case ItemsType.Water:
					tariffs.CalculateWater(ref item);
					break;
				case ItemsType.HotWater:
					tariffs.CalculateHotWater(ref item);
					break;
				default:
					break;
			}
			return await Task.FromResult(true);
		}

		public async Task<Item> GetItemAsync(ItemsType id)
		{
			return await Task.FromResult(items.GetValue(id));
		}

		public async Task<Items> GetItemsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(items);
		}

		public async Task<Tariffs> GetTariffsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(tariffs);
		}
	}
}