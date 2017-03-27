using Gachi.Model;
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

                        // 서버로부터 로그인이 승인되면 사용자 정보를 서버에 요청하여 받아온다 (지금은 하드코딩)
                        var user = new User();
                        user.Email = this.UserEmail;
                        user.NickName = "thekan";
                        user.ProfileUrl = "";

                        // 다음 페이지에 사용자 정보를 넘겨준다
                        this.navService.Navigate(typeof(MyProjectView), user);
                    });
                }
                return this.doLogin;
            }
        }
        #endregion
    }
}
