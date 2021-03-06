﻿using Gachi.Model;
using Gachi.Util;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private bool isVisibleProjectView = false;
        /// <summary>
        /// true - 우측 사이드 바에서 탐색 버튼 클릭이 된 경우이며 프로젝트 탐색기 컨트롤이 화면에 나타난다
        /// false - 우측 사이드 바가 꺼지거나, 탐색 버튼이 다시 눌린 경우에 설정된다
        /// </summary>
        public bool IsVisibleProjectView
        {
            get { return this.isVisibleProjectView; }
            set
            {
                if (this.isVisibleProjectView != value)
                {
                    this.isVisibleProjectView = value;
                    this.RaisePropertyChanged("IsVisibleProjectView");
                }
            }
        }

        private bool isVisibleChatView = false;
        /// <summary>
        /// true - 우측 사이드 바에서 채팅 버튼 클릭이 된 경우이며 채팅 컨트롤이 화면에 나타난다
        /// false - 우측 사이드바가 꺼지거나, 채팅 버튼이 다시 눌린 경우에 설정된다
        /// </summary>
        public bool IsVisibleChatView
        {
            get { return this.isVisibleChatView; }
            set
            {
                if (this.isVisibleChatView != value)
                {
                    this.isVisibleChatView = value;
                    this.RaisePropertyChanged("IsVisibleChatView");
                }
            }
        }

        private User mainChatUser = null;
        /// <summary>
        /// 채팅할 수 있는 프로젝트가 공유된 유저 목록에서 
        /// 사용자가 1명을 선택된 경우 이 변수에 바인딩된다
        /// </summary>
        public User MainChatUser
        {
            get { return this.mainChatUser; }
            set
            {
                // 다른 사용자가 클릭된 경우라면 교체한다
                if (this.mainChatUser != value)
                {
                    this.mainChatUser = value;
                    this.RaisePropertyChanged("MainChatUser");
                }
                // 만약, 같은 사용자가 두 번 이상 클릭된 경우엔 비운다
                else
                {
                    this.mainChatUser = null;
                    this.RaisePropertyChanged("MainChatUser");
                }
            }
        }

        private ObservableCollection<Message> messages = null;
        /// <summary>
        /// 프로젝트 채팅에서 수신된 메시지 목록들
        /// </summary>
        public ObservableCollection<Message> Messages
        {
            get
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<Message>();
                }
                return this.messages;
            }
            set
            {
                if (this.messages != value)
                {
                    this.messages = value;
                    this.RaisePropertyChanged("Messages");
                }
            }
        }

        private string chatText = "";
        public string ChatText
        {
            get { return this.chatText; }
            set
            {
                if (this.chatText != value)
                {
                    this.chatText = value;
                    this.RaisePropertyChanged("ChatText");
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
                            // 만약, 우측 사이드 바가 닫힌 경우라면
                            if (this.RightSideOpened == false)
                            {
                                // 프로젝트 뷰가 Visibile한 경우라면 Collapsed로 바꾼다
                                if (this.IsVisibleProjectView == true)
                                {
                                    this.IsVisibleProjectView = false;
                                }
                                // 채팅뷰가 Visibile한 경우라면 Collapsed로 바꾼다
                                if (this.IsVisibleChatView == true)
                                {
                                    this.IsVisibleChatView = false;
                                }
                            }
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

        private ICommand doToggleVisibilityProjectView = null;
        /// <summary>
        /// IsVisibleProject를 토글링한다
        /// </summary>
        public ICommand DoToggleVisibilityProjectView
        {
            get
            {
                if (this.doToggleVisibilityProjectView == null)
                {
                    this.doToggleVisibilityProjectView = new RelayCommand(() =>
                    {
                        this.IsVisibleProjectView = !(this.IsVisibleProjectView);
                        // 만약 프로젝트 뷰가 켜있는 상태에서 채팅바가 켜져있다면
                        // 채팅바의 비져빌러티를 false로 수정하여 보이지 않게 한다
                        if (this.IsVisibleProjectView == true)
                        {
                            if (this.IsVisibleChatView == true)
                            {
                                this.IsVisibleChatView = false;
                            }
                        }
                    });
                }
                return this.doToggleVisibilityProjectView;
            }
        }

        private ICommand doToggleVisibilityChatView = null;
        public ICommand DoToggleVisibilityChatView
        {
            get
            {
                if (this.doToggleVisibilityChatView == null)
                {
                    this.doToggleVisibilityChatView = new RelayCommand(() =>
                    {
                        this.IsVisibleChatView = !(this.IsVisibleChatView);
                        // 만약 채팅뷰가 켜있는 상태에서 프로젝트바가 켜져있다면
                        // 프로젝트 뷰의 비져빌러티를 false로 수정하여 보이지 않게 한다
                        if (this.IsVisibleChatView == true)
                        {
                            if (this.IsVisibleProjectView == true)
                            {
                                this.IsVisibleProjectView = false;
                            }
                        }
                    });
                }
                return this.doToggleVisibilityChatView;
            }
        }

        private ICommand doTapMainChatUserInUsers = null;
        /// <summary>
        /// 사용자가 채팅할 1명의 사용자를 선택한 경우
        /// </summary>
        public ICommand DoTapMainChatUserInUsers
        {
            get
            {
                if (this.doTapMainChatUserInUsers == null)
                {
                    this.doTapMainChatUserInUsers = new RelayCommand<object>((object sender) =>
                    {
                        // 메인으로 채팅할 유저를 선택한다
                        var chatUser = sender as User;
                        if (chatUser != null)
                        {
                            this.MainChatUser = chatUser;
                        }

                        // 메인으로 선택된 사용자를 채팅 목록에서 가장 상단으로 올린다
                        // 사용자 목록에서 선택된 사용자를 제거한 뒤 가장 상단에 삽입한다
                        bool isRemoved = this.CurProject.Users.Remove(this.MainChatUser);
                        if (isRemoved == true)
                        {
                            this.CurProject.Users.Insert(0, this.MainChatUser);
                        }
                    });
                }
                return this.doTapMainChatUserInUsers;
            }
        }

        private ICommand doSendMessage = null;
        public ICommand DoSendMessage
        {
            get
            {
                if (this.doSendMessage == null)
                {
                    this.doSendMessage = new RelayCommand(() =>
                    {
                        if ("".Equals(this.ChatText.Trim()) == false)
                        {
                            // 메시지를 채팅창에 추가한다
                            this.Messages.Add(new Message()
                            {
                                Contents = this.chatText,
                                CountUnReader = this.CurProject.Users.Count(),
                                Date = "2017-04-06",
                                IsMine = true,
                                ProjectId = "unknown",
                                Sender = this.UserInfo,
                                Time = "03:04"
                            });
                            // 텍스트 박스에 입력된 텍스트를 지운다
                            this.ChatText = "";
                        }
                    });
                }
                return this.doSendMessage;
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
