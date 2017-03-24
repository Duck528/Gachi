using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gachi.Model
{
    public class Project : ObservableObject
    {
        private string title = "";
        /// <summary>
        /// 프로젝트 타이틀
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }

        private string createdAt = "";
        /// <summary>
        /// 프로젝트 생성 날짜
        /// </summary>
        public string CreatedAt
        {
            get { return this.createdAt; }
            set
            {
                if (this.createdAt != value)
                {
                    this.createdAt = value;
                    this.RaisePropertyChanged("CreatedAt");
                }
            }
        }

        private string lastModifiedAt = "";
        /// <summary>
        /// 마지막 수정 날짜
        /// </summary>
        public string LastModifiedAt
        {
            get { return this.lastModifiedAt; }
            set
            {
                if (this.lastModifiedAt != value)
                {
                    this.lastModifiedAt = value;
                    this.RaisePropertyChanged("LastModifiedAt");
                }
            }
        }

        private ObservableCollection<User> users = null;
        /// <summary>
        /// 이 프로젝트가 공유된 사용자 목록
        /// </summary>
        public ObservableCollection<User> Users
        {
            get { return this.users; }
            set
            {
                if (this.users != value)
                {
                    this.users = value;
                    this.RaisePropertyChanged("Users");
                }
            }
        }
    }
}
