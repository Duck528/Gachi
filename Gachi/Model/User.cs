using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gachi.Model
{
    public enum LinkStatus { Connected, DisConnected, Busy };
    public class User : ObservableObject
    {
        private string name = "";
        /// <summary>
        /// 사용자 이름
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        private string profileUrl = "";
        /// <summary>
        /// 사용자 프로필 이미지 경로
        /// </summary>
        public string ProfileUrl
        {
            get { return this.profileUrl; }
            set
            {
                if (this.profileUrl != value)
                {
                    this.profileUrl = value;
                    this.RaisePropertyChanged("ProfileUrl");
                }
            }
        }

        private string email = "";
        /// <summary>
        /// 사용자의 아이디로 사용되는 이메일 주소 (PK)
        /// 이 값은 변경 불가능하다
        /// </summary>
        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }

        private string nickName = "";
        /// <summary>
        /// 다른 사용자에게 보여지는 별명으로 식별키가 아니며,
        /// 중복이 가능하고 변경이 가능하다
        /// </summary>
        public string NickName
        {
            get { return this.nickName; }
            set
            {
                if (this.nickName != value)
                {
                    this.nickName = value;
                    this.RaisePropertyChanged("NickName");
                }
            }
        }

        private string pw = "";
        /// <summary>
        /// 로그인에 사용되는 패스워드
        /// </summary>
        public string Pw
        {
            get { return this.pw; }
            set
            {
                if (this.pw != value)
                {
                    this.pw = value;
                    this.RaisePropertyChanged("Pw");
                }
            }
        }

        private LinkStatus userLinkStatus = LinkStatus.DisConnected;
        /// <summary>
        /// 사용자 접속 정보
        /// </summary>
        public LinkStatus UserLinkStatus
        {
            get { return this.userLinkStatus; }
            set
            {
                if (this.userLinkStatus != value)
                {
                    this.userLinkStatus = value;
                    this.RaisePropertyChanged("UserLinkStatus");
                }
            }
        }
    }
}
