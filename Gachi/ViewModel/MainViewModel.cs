using Gachi.Model;
using Gachi.Util;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Gachi.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private INavigationService navService = null;
        public MainViewModel(INavigationService navService)
        {
            this.navService = navService;
        }

        #region PageStatus
        private User userInfo = null;
        /// <summary>
        /// 로그인을 통해 인증된 사용자의 정보
        /// </summary>
        public User UserInfo
        {
            get { return this.userInfo; }
            set
            {
                if (this.userInfo != value)
                {
                    this.userInfo = value;
                    this.RaisePropertyChanged("UserInfo");
                }
            }
        }
        private Project curProject = null;
        /// <summary>
        /// 프로젝트 선택 윈도우에서 사용자가 선택한 프로젝트이다
        /// </summary>
        public Project CurProject
        {
            get { return this.curProject; }
            set
            {
                if (this.curProject != value)
                {
                    this.curProject = value;
                    this.RaisePropertyChanged("CurProject");
                }
            }
        }
        private bool leftSideOpened = false;
        /// <summary>
        /// Left SideBar가 열려있는지 상태인지, 닫혀있는 상태인지 표시한다
        /// </summary>
        public bool LeftSideOpened
        {
            get { return this.leftSideOpened; }
            set
            {
                if (this.leftSideOpened != value)
                {
                    this.leftSideOpened = value;
                    this.RaisePropertyChanged("LeftSideOpened");
                }
            }
        }

        private bool rightSideOpened = false;
        /// <summary>
        /// Right SideBar가 열려있는 상태인지, 닫혀있는 상태인지 표시한다
        /// </summary>
        public bool RightSideOpened
        {
            get { return this.rightSideOpened; }
            set
            {
                if (this.rightSideOpened != value)
                {
                    this.rightSideOpened = value;
                    this.RaisePropertyChanged("RightSideOpened");
                }
            }
        }

        private User rightTappedUser = null;
        /// <summary>
        /// Right SideBar에서 우클릭된 사용자 정보가 저장된다
        /// 우 클릭이 되면 Flyout 메뉴가 팝되며 여기선 다음과 같은 기능이 있다
        /// - 화상 채팅
        /// - 쪽지 보내기
        /// - 차단 등
        /// </summary>
        public User RightTappedUser
        {
            get { return this.rightTappedUser; }
            set
            {
                if (this.rightTappedUser != value)
                {
                    this.rightTappedUser = value;
                    this.RaisePropertyChanged("RightTappedUser");
                }
            }
        }

        private bool isPinRightSideBar = false;
        /// <summary>
        /// true - 우측 사이드 바 고정
        /// false - 우측 사이드 바 고정되지 않음
        /// </summary>
        public bool IsPinRightSideBar
        {
            get { return this.isPinRightSideBar; }
            set
            {
                if (this.isPinRightSideBar != value)
                {
                    this.isPinRightSideBar = value;
                    this.RaisePropertyChanged("IsPinRightSideBar");
                }
            }
        }
        #endregion

        #region Commands
        private ICommand doPaneLeftSide = null;
        /// <summary>
        /// LeftSideOpened의 값을 토글링한다
        /// true -> false
        /// false -> true
        /// </summary>
        public ICommand DoPaneLeftSide
        {
            get
            {
                if (this.doPaneLeftSide == null)
                {
                    this.doPaneLeftSide = new RelayCommand(() =>
                    {
                        this.LeftSideOpened = !(this.LeftSideOpened);
                    });
                }
                return this.doPaneLeftSide;
            }
        }

        private ICommand doPaneRightSide = null;
        public ICommand DoPaneRightSide
        {
            get
            {
                if (this.doPaneRightSide == null)
                {
                    this.doPaneRightSide = new RelayCommand(() =>
                    {
                        if (this.IsPinRightSideBar != true)
                        {
                            this.RightSideOpened = !(this.RightSideOpened);
                        }
                    });
                }
                return this.doPaneRightSide;
            }
        }

        private ICommand doRightTapChatBar = null;
        /// <summary>
        /// 채팅 바에서 우클릭 되면 실행되며, 
        /// 우클릭된 사용자 정보를 RightTappedUser 프로퍼티에 저장한다
        /// </summary>
        public ICommand DoRightTapChatBar
        {
            get
            {
                if (this.doRightTapChatBar == null)
                {
                    this.doRightTapChatBar = new RelayCommand<object>((object sender) =>
                    {
                        var user = sender as User;
                        if (user != null)
                        {
                            this.RightTappedUser = user;
                        }
                    });
                }
                return this.doRightTapChatBar;
            }
        }
        #endregion

        #region Events
        public void RightBar_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var wrapper = sender as ListView;
            if (wrapper != null)
            {
                var flyout = wrapper.FindName("rightMenuFlyout") as MenuFlyout;
                if (flyout != null)
                {
                    // Flyout 메뉴를 우클릭된 위치에서 띄운다
                    flyout.ShowAt(wrapper, e.GetPosition(wrapper));
                }
            }
        }

        public void PinRightBarButton_Tapped(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                this.IsPinRightSideBar = !(this.IsPinRightSideBar);
                if (this.IsPinRightSideBar == true)
                {
                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("ms-appx:///Resource/closedpin.png"))
                    };
                }
                else
                {
                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("ms-appx:///Resource/openpin.png"))
                    };
                }
            }
        }
        #endregion

    }
}
