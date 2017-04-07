using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Gachi.Converter
{
    class CollectionToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var collection = value as IEnumerable<object>;
            if (collection != null)
            {
                return collection.Count();
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
