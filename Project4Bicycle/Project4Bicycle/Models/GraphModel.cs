using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Models
{
    abstract class GraphModel<T>
    {
        public ObservableCollection<T> model { get; set; }
        public void AddData(T entry)
        {
            model.Add(entry);
        }

        public void RemoveData(T entry)
        {
            model.Remove(entry);
        }
    }
}
