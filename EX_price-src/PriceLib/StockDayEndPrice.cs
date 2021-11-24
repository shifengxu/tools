using System;

namespace PriceLib
{
    public class StockDayEndPrice : StockInfo
    {
        public string ClosePrice { get; set; }
        public string OpenPrice { get; set; }
        public string HighPrice { get; set; }
        public string LowPrice { get; set; }
        public string Volume { get; set; }

        public StockDayEndPrice()
        {
        }

        public StockDayEndPrice(StockInfo si)
        {
            this.Code = si.Code;
            this.Name = si.Name;
            this.Suspended = si.Suspended;
        }

        public bool MissingData()
        {
            return string.IsNullOrEmpty(OpenPrice)
                || string.IsNullOrEmpty(HighPrice)
                || string.IsNullOrEmpty(LowPrice)
                || string.IsNullOrEmpty(ClosePrice)
                || string.IsNullOrEmpty(Volume);
        }

        public static new StockDayEndPrice Parse(string csvStr)
        {
            if (string.IsNullOrEmpty(csvStr)) throw new ArgumentException("Empty argument: " + csvStr);

            var arr = csvStr.Split(';');
            if (arr.Length != 8) throw new ArgumentException("Invalid argument (split result length is not 8): " + csvStr);

            StockDayEndPrice sp = new StockDayEndPrice();
            sp.Code = int.Parse(arr[0]);
            sp.Name = arr[1];
            sp.Suspended = arr[2].Equals("Suspended", StringComparison.OrdinalIgnoreCase);
            sp.OpenPrice = arr[3];
            sp.HighPrice = arr[4];
            sp.LowPrice  = arr[5];
            sp.ClosePrice= arr[6];
            sp.Volume    = arr[7];
            return sp;
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                Code, Name, Suspended ? "Suspended" : "",
                OpenPrice, HighPrice, LowPrice, ClosePrice, Volume);
        }
    } // class
}
