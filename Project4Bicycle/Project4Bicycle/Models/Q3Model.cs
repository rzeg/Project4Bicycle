using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Models
{
    class Q3Model : GraphModel<StackedData>
    {
        public Q3Model()
        {
            model = new ObservableCollection<StackedData>();
        }

        internal void Clear()
        {
            model.Clear();
        }
    }
}
