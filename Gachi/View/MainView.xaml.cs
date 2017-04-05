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
        private MainViewModel viewModel = null;
        public MainView()
        {
            this.InitializeComponent();

            var viewModel = new MainViewModel(new NavigationService());
            this.viewModel = viewModel;
            this.DataContext = viewModel;
        }

        /// <summary>
        /// 설명을 보니 엔터가 눌린 경우를 판단할 때는
        /// MVVM 방식으로 Event를 구독하는 방법보다는 이와같이,
        /// 내장함수 OnKeyDown 메서드를 오버라이딩하는게
        /// 더 효율적이라는 문서를 읽어서 이렇게 구성하였다
        /// 주소: http://stackoverflow.com/questions/15434494/winrt-application-login-by-pressing-enter-key-with-mvvm-light
        /// </summary>
        /// <param name="e">이벤트</param>
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);

            // 만약에 엔터키가 눌렸다면,
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                this.viewModel.DoSendMessage.Execute(null);
            }
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

                        viewModel.Messages.Add(new Message()
                        {
                            Contents = "비오는날엔... 파전에 막걸린데 말이죠",
                            CountUnReader = 1,
                            Date = "2017-04-05",
                            Time = "19:05",
                            ProjectId = "unknown",
                            Sender = new User()
                            {
                                Name = "란또",
                                Email = "jae@naver.com",
                                NickName = "coworker",
                                ProfileUrl = "ms-appx:///Resource/profile01.png",
                                Pw = "1234"
                            }
                        });
                        viewModel.Messages.Add(new Message()
                        {
                            Contents = "그르게요",
                            CountUnReader = 1,
                            Date = "2017-04-05",
                            Time = "19:07",
                            ProjectId = "unknown",
                            Sender = new User()
                            {
                                Name = "안덕환",
                                Email = "ranran@naver.com",
                                NickName = "GoodGirl",
                                ProfileUrl = "ms-appx:///Resource/profile02.png",
                                Pw = "1234"
                            },
                            IsMine = true
                        });
                        viewModel.Messages.Add(new Message()
                        {
                            Contents = "파이팅합시다",
                            CountUnReader = 1,
                            Date = "2017-04-05",
                            Time = "19:10",
                            ProjectId = "unknown",
                            Sender = new User()
                            {
                                Name = "란또",
                                Email = "jae@naver.com",
                                NickName = "coworker",
                                ProfileUrl = "ms-appx:///Resource/profile01.png",
                                Pw = "1234"
                            }
                        });
                    }
                }
            }
        }
    }
}
