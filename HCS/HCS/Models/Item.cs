using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms.Internals;

namespace HCS.Models
{
	public enum ItemsType
	{
		Rent,
		ElectricPower,
		NaturalGas,
		Water,
		Heating,
		HotWater,
		GarbageRemoval
	}

	public class Items : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public List<Item> Values { get; set; }
		public string Total() {
			int result = 0;
			foreach (var item in this.Values)
			{
				result += item.Summ;
			}
			return result.ToString();
		}

		public void UpdateValue(Item item) {
			var xx = Values.IndexOf(Values.FirstOrDefault(x => x.Id == item.Id));
			Values[xx] = item;
		}

		public Item GetValue(ItemsType id)
		{
			return Values.FirstOrDefault(x => x.Id == id);
		}
	}
	public class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ItemsType Id { get; set; }
		public string Text { get; set; }
		public int First { get; set; }
		public int Second { get; set; }
		public int Summ { get; set; }
		public bool ShowDifference { get; set; }
		public bool ShowSumm { get; set; }
		public string Difference => First != 0 && Second != 0 ? GetDifference.ToString() : "";
		public int GetDifference => Second - First;

		public Item()
		{ 
		}

		public Item(Item extentedItem)
		{
			Id = extentedItem.Id;
			Text = extentedItem.Text;
			First = extentedItem.First;
			Second = extentedItem.Second;
			Summ = extentedItem.Summ;
			ShowDifference = extentedItem.ShowDifference;
			ShowSumm = extentedItem.ShowSumm;
		}
	}

}