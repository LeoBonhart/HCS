using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HCS.Models
{
	public class Tariff : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ItemsType Id { get; set; }
		public string Name { get; set; }
		public double Value { get; set; }

		public Tariff()
		{
		}
		public Tariff(Tariff extentedItem) {
			Id = extentedItem.Id;
			Name = extentedItem.Name;
			Value = extentedItem.Value;
		}
	}
	public class Tariffs : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Tariff Gas { get; set; }
		public Tariff Water { get; set; }
		public Tariff HotWater { get; set; }
		public TariffsElectricPower ElectricPower { get; set; }
		public Tariffs() {
			Gas = new Tariff()
			{
				Id = ItemsType.NaturalGas,
				Name = "Газ",
				Value = CrossSettings.Current.GetValueOrDefault("Gas", 2.367)
			};
			Water = new Tariff()
			{
				Id = ItemsType.Water,
				Name = "Вода",
				Value = CrossSettings.Current.GetValueOrDefault("Water", 21.55)
			};
			HotWater = new Tariff()
			{
				Id = ItemsType.HotWater,
				Name = "Горячая вода",
				Value = CrossSettings.Current.GetValueOrDefault("HotWater", 35.16)
			};
			ElectricPower = new TariffsElectricPower();
		}
		public int CalculateGas(ref Item value) => value.Summ = (int)Math.Ceiling(value.GetDifference * Gas.Value);
		public int CalculateWater(ref Item value) => value.Summ = (int)Math.Ceiling(value.GetDifference * Water.Value);
		public int CalculateHotWater(ref Item value) => value.Summ = (int)Math.Ceiling(value.GetDifference * HotWater.Value);
		public int CalculateElectricPower(ref Item value) => value.Summ = (int)Math.Ceiling(ElectricPower.Calculate(value.GetDifference));
		public void ChangeGas(double value) {
			Gas.Value = value;
			CrossSettings.Current.AddOrUpdateValue("Gas", value);
		}
		public void ChangeWater(double value) {
			Water.Value = value;
			CrossSettings.Current.AddOrUpdateValue("Water", value);
		}
		public void ChangeHotWater(double value)
		{
			HotWater.Value = value;
			CrossSettings.Current.AddOrUpdateValue("HotWater", value);
		}
		public void ChangeElectricPower(TariffsElectricPower value) {
			ElectricPower.Update(value);
		}
	}

	public class TariffsElectricPower : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ItemsType Id { get; set; }
		public string Name { get; set; }
		public double Less150kW { get; set; }
		public double Ower150kW { get; set; }
		public double Ower800kW { get; set; }
		public TariffsElectricPower() {
			Id = ItemsType.Water;
			Name = "Электроэнергия";
			Less150kW = CrossSettings.Current.GetValueOrDefault("Less150kW", 0.8018);
			Ower150kW = CrossSettings.Current.GetValueOrDefault("Ower150kW", 1.0904);
			Ower800kW = CrossSettings.Current.GetValueOrDefault("Ower800kW", 2.6808);
		}
		public TariffsElectricPower(TariffsElectricPower extentedItem)
		{
			Id = extentedItem.Id;
			Name = extentedItem.Name;
			Less150kW = extentedItem.Less150kW;
			Ower150kW = extentedItem.Ower150kW;
			Ower800kW = extentedItem.Ower800kW;
		}
		public double Calculate(int value) {
			double result;
			if (value < 150)
			{
				result = value * Less150kW;
			}
			else
			{
				result = 150 * Less150kW;
				if (value < 800)
				{
					result += (value - 150) * Ower150kW;
				}
				else
				{
					result += 650 * Ower150kW;
					result += (value - 800) * Ower800kW;
				}
			}
			return result;
		}
		public void Update(TariffsElectricPower value) {
			Less150kW = value.Less150kW;
			Ower150kW = value.Ower150kW;
			Ower800kW = value.Ower800kW;
			CrossSettings.Current.AddOrUpdateValue("Less150kW", value.Less150kW);
			CrossSettings.Current.AddOrUpdateValue("Ower150kW", value.Ower150kW);
			CrossSettings.Current.AddOrUpdateValue("Ower800kW", value.Ower800kW);
		}
	}
}
