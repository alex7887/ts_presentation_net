using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public class WaterProducer
    {

        private Dictionary<String, WaterStore> stories = new Dictionary<String, WaterStore>();

        public Dictionary<String, WaterStore> Stories
        {
            get { return stories; }
            set { stories = value; }
        }
        

        public void createStore(String name)
        {
            WaterStore waterStore = null;
            if (name.Equals("fanta"))
            {
                waterStore = new FantaStore();
                

            }else if(name.Equals("whisky")){

                waterStore = new WhiskyStore();
                
            }
            else if (name.Equals("jin"))
            {

                waterStore = new JinStore();
                
            }else
            {
                throw new Exception();
            }

            Stories.Add(name, waterStore);
            
        }

        public Water order(String name)
        {
            if (Stories.ContainsKey(name))
            {
                WaterStore waterStore = Stories[name];
                return waterStore.create(name);
            }

            return null;

        }
    }
}
