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
                        this.RightSideOpened = !(this.RightSideOpened);
                    });
                }
                return this.doPaneRightSide;
            }
        }
        #endregion
    }
}
