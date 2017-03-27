using Gachi.Model;
using Gachi.Util;
using Gachi.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace Gachi.View
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();

            var viewModel = new MainViewModel(new NavigationService());
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var dict = e.Parameter as Dictionary<string, object>;
            if (dict != null)
            {
                var viewModel = this.DataContext as MainViewModel;
                if (viewModel != null)
                {
                    object userInfo = null;
                    object projectInfo = null;

                    bool hasUser = dict.TryGetValue("userInfo", out userInfo);
                    bool hasProject = dict.TryGetValue("projectInfo", out projectInfo);

                    if (hasUser == true && hasProject == true)
                    {
                        var user = userInfo as User;
                        if (user != null)
                        {
                            viewModel.UserInfo = user;
                        }
                        var project = projectInfo as Project;
                        if (project != null)
                        {
                            viewModel.CurProject = project;
                        }
                    }
                }
            }
        }
    }
}
