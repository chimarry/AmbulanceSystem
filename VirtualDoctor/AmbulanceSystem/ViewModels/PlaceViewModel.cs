﻿using System.Collections.Generic;

namespace AmbulanceSystem.ViewModels
{

    public class PlaceViewModel
    {
        public static readonly int EndScale = 100;
        public int IdPlace { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public int DrinkingWaterQuality { get; set; }
        public int FoodQuality { get; set; }
        public int RecreationalWaterQuality { get; set; }
        public int AirQuality { get; set; }
        public int InlandWaterQuality { get; set; }
        public int TerrainQuality { get; set; }
        public int MedicalVasteInformation { get; set; }
        public int NoiseInformation { get; set; }
        public int Radiation { get; set; }

        public double AverageQuality { get; set; }

        public double CalculateAverageQuality()
            => (FoodQuality + DrinkingWaterQuality + RecreationalWaterQuality + AirQuality + TerrainQuality + InlandWaterQuality +
                EndScale - MedicalVasteInformation + EndScale - Radiation + EndScale - NoiseInformation) / 9.0;

        public override bool Equals(object obj) => obj.GetHashCode() == GetHashCode();

        public override int GetHashCode()
        {
            var hashCode = 1906746711;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CountryName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PostalCode);
            return hashCode;
        }

        public override string ToString() => $"{Name}:{CountryName}";
    }
}
