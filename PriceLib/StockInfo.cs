using System;
using System.Collections.Generic;
using System.Text;

namespace PriceLib
{
    public class StockInfo
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public bool Suspended { get; set; }

        public StockInfo() { }

        public StockInfo(int code, string name, bool suspended)
        {
            this.Code = code;
            this.Name = name;
            this.Suspended = suspended;
        }

        public static StockInfo Parse(string csvStr)
        {
            if (string.IsNullOrEmpty(csvStr)) throw new ArgumentException("Empty argument: " + csvStr);

            var arr = csvStr.Split(',');
            if (arr.Length != 3) throw new ArgumentException("Invalid argument (split result length is not 3): " + csvStr);

            StockInfo si = new StockInfo();
            si.Code = int.Parse(arr[0]);
            si.Name = arr[1];
            si.Suspended = arr[2].Equals("Suspended", StringComparison.OrdinalIgnoreCase);
            return si;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Code, Name, Suspended ? "Suspended" : "");
        }
    }
}
