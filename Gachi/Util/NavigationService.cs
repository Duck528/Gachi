using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Gachi.Util
{
    interface INavigationService
    {
        void GoBack();
        void Navigate(Type page, object param);
        void Navigate(Type page);
        void OnNavigatedTo(NavigationEventArgs e);
    }
    class NavigationService : INavigationService
    {
        public void GoBack()
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.GoBack();
            }
        }

        public void Navigate(Type page, object param)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(page, param);
            }
        }

        public void Navigate(Type page)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(page);
            }
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
