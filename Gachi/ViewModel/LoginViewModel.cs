using Gachi.Util;
using Gachi.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Gachi.ViewModel
{
    class LoginViewModel : ObservableObject
    {
        private INavigationService navService = null;
        public LoginViewModel(INavigationService navService)
        {
            this.navService = navService;
        }

        #region UserInfo
        private string userEmail = "";
        public string UserEmail
        {
            get { return this.userEmail; }
            set
            {
                if (this.userEmail != value)
                {
                    this.userEmail = value;
                    this.RaisePropertyChanged("UserEmail");
                }
            }
        }

        private string userPw = "";
        public string UserPw
        {
            get { return this.userPw; }
            set
            {
                if (this.userPw != value)
                {
                    this.userPw = value;
                    this.RaisePropertyChanged("UserPw");
                }
            }
        }
        #endregion

        #region Commands
        private ICommand doLogin = null;
        public ICommand DoLogin
        {
            get
            {
                if (this.doLogin == null)
                {
                    this.doLogin = new RelayCommand(async () =>
                    {
                        if ("".Equals(this.userEmail.Trim()))
                        {
                            var pop = new MessageDialog("아이디를 입력해 주세요");
                            await pop.ShowAsync();
                            return;
                        }

                        if ("".Equals(this.UserPw.Trim()))
                        {
                            var pop = new MessageDialog("비밀번호를 입력해 주세요");
                            await pop.ShowAsync();
                            return;
                        }

                        this.navService.Navigate(typeof(MyProjectView), this.UserEmail);
                    });
                }
                return this.doLogin;
            }
        }
        #endregion
    }
}
