using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Recipe.Payloads
{
    public class Amount
    {
        public int? Quantity { get; set; }
        public string Unit { get; set; }
        public Amount()
        {

        }
        public Amount(int? quantity, string unit)
        {
            this.Quantity = quantity;
            this.Unit = unit;
        }
    }
}
