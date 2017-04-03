using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gachi.Model
{
    public enum ProjectItemType { Directory, File, Image };
    class ProjectItem : ObservableObject
    {
        private string title = "";
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

        private ProjectItemType itemType = ProjectItemType.File;
        /// <summary>
        /// 프로젝트 하위 파일의 아이템 타입
        /// </summary>
        public ProjectItemType ItemType
        {
            get { return this.itemType; }
            set
            {
                if (this.itemType != value)
                {
                    this.itemType = value;
                    this.RaisePropertyChanged("ItemType");
                }
            }
        }

        private ObservableCollection<ProjectItem> projectItems = null;
        /// <summary>
        /// 이 프로젝트가 가지고 있는 하위 아이템들
        /// </summary>
        public ObservableCollection<ProjectItem> ProjectItems
        {
            get
            {
                if (this.projectItems == null)
                {
                    this.projectItems = new ObservableCollection<ProjectItem>();
                }
                return this.projectItems;
            }
            set
            {
                if (this.projectItems != value)
                {
                    this.projectItems = value;
                    this.RaisePropertyChanged("ProjectItems");
                }
            }
        }
    }
}
