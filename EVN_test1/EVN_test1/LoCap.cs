using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    class LoCap
    {
        private string name;
        private List<int> location;
        public LoCap(string name)
        {
            this.name = name;
            this.location = new List<int>();
        }
        public string getName()
        {
            return this.name;
        }
        public List<int> getList()
        {
            return this.location;
        }
        public void add(int position)
        {
            this.location.Add(position);
        }
        public void delete(int item_pos)
        {
            this.location.RemoveAt(item_pos);
        }
    }
}
