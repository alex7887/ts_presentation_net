using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public class FantaStore : WaterStore
    {
        public Water create(string name)
        {
            SoftDrink softDrink = new SoftDrink();

            softDrink.Name = name;
            softDrink.Price = 80;
            softDrink.Color = "orange";

            return softDrink;
        }
    }
}
