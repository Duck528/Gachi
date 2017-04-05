using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gachi.Model
{
    /// <summary>
    /// 유저가 전송한 채팅 메시지
    /// </summary>
    class Message : ObservableObject
    {
        private string contents = "";
        /// <summary>
        /// 메시지 내용
        /// </summary>
        public string Contents
        {
            get { return this.contents; }
            set
            {
                if (this.contents != value)
                {
                    this.contents = value;
                    this.RaisePropertyChanged("Contents");
                }
            }
        }

        private string date = "";
        /// <summary>
        /// 메시지 발신 날짜
        /// </summary>
        public string Date
        {
            get { return this.date; }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }

        private string time = "";
        /// <summary>
        /// 메시지 발신 시간
        /// : 분리한 이유는 날짜별로 그룹핑하기 위해서이다
        /// </summary>
        public string Time
        {
            get { return this.time; }
            set
            {
                if (this.time != value)
                {
                    this.time = value;
                    this.RaisePropertyChanged("Time");
                }
            }
        }

        private User sender = null;
        /// <summary>
        /// 메시지를 발신한 사람
        /// </summary>
        public User Sender
        {
            get { return this.sender; }
            set
            {
                if (this.sender != value)
                {
                    this.sender = value;
                    this.RaisePropertyChanged("Sender");
                }
            }
        }

        private string projectId = "";
        /// <summary>
        /// 이 메시지가 전송될 프로젝트 아이디
        /// </summary>
        public string ProjectId
        {
            get { return this.projectId; }
            set
            {
                if (this.projectId != value)
                {
                    this.projectId = value;
                    this.RaisePropertyChanged("ProjectId");
                }
            }
        }

        private int countUnReader = -1;
        /// <summary>
        /// 이 메시지를 읽지 않은 사용자의 수
        /// </summary>
        public int CountUnReader
        {
            get { return this.countUnReader; }
            set
            {
                if (this.countUnReader != value)
                {
                    this.countUnReader = value;
                    this.RaisePropertyChanged("CountUnReader");
                }
            }
        }

        private bool isMine = false;
        /// <summary>
        /// 내가 보낸 메시지인지 검사한다
        /// (일단 모든 메시지는 서버로 보내고 다시 서버에서 받는다)
        /// (내가 보낸 메시지라고 채팅창에 보내진것처럼 바로 더하지 않는다)
        /// </summary>
        public bool IsMine
        {
            get { return this.isMine; }
            set
            {
                if (this.isMine != value)
                {
                    this.isMine = value;
                    this.RaisePropertyChanged("IsMine");
                }
            }
        }
    }
}
