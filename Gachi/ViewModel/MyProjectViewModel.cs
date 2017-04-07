using Gachi.Model;
using Gachi.Util;
using Gachi.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Gachi.ViewModel
{
    class MyProjectViewModel : ObservableObject
    {
        /// <summary>
        /// View 로부터 Page Navigation 정보를 의존성 주입을 받아서
        /// MVVM 방식으로 페이지 네비게이션을 처리한다
        /// </summary>
        private INavigationService navService = null;
        public MyProjectViewModel(INavigationService navService)
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

        private Project selectedProject = null;
        /// <summary>
        /// 프로젝트 리스트 중에서 사용자가 선택한 프로젝트가 저장된다
        /// </summary>
        public Project SelectedProject
        {
            get { return this.selectedProject; }
            set
            {
                if (this.selectedProject != value)
                {
                    this.selectedProject = value;
                    this.RaisePropertyChanged("SelectedProject");
                }
            }
        }
        private ObservableCollection<Project> projects = null;
        /// <summary>
        /// 서버로부터 전송받은 사용자의 프로젝트 내역
        /// </summary>
        public ObservableCollection<Project> Projects
        {
            get
            {
                if (this.projects == null)
                {
                    var newProjects = new ObservableCollection<Project>();
                    newProjects.Add(new Project()
                    {
                        Title = "TCP Hidden Project",
                        CreatedAt = "2017-03-25",
                        LastModifiedAt = "2017-03-26",
                        Users = new ObservableCollection<User>()
                        {
                            new User()
                            {
                                Name = "란또",
                                Email = "jae@naver.com",
                                NickName = "coworker",
                                ProfileUrl = "ms-appx:///Resource/profile01.png",
                                Pw = "1234"
                            }
                        },
                        Master = new User()
                        {
                            Name = "안덕환",
                            Email = "sdzaq@naver.com",
                            NickName = "thekan",
                            ProfileUrl = "ms-appx:///Resource/profile02.png",
                            Pw = "1234"
                        }
                    });
                    newProjects.Add(new Project()
                    {
                        Title = "Mogwa",
                        CreatedAt = "2017-02-02",
                        LastModifiedAt = "2017-03-15",
                        Master = new User()
                        {
                            Name = "안덕환",
                            Email = "sdzaq@naver.com",
                            NickName = "thekan",
                            ProfileUrl = "ms-appx:///Resource/profile02.png",
                            Pw = "1234"
                        },
                        Users = new ObservableCollection<User>()
                        {
                            new User()
                            {
                                Name = "란또",
                                Email = "jae@naver.com",
                                NickName = "coworker",
                                ProfileUrl = "ms-appx:///Resource/profile01.png",
                                Pw = "1234"
                            },
                            new User()
                            {
                                Name = "김철수",
                                Email = "abcd@naver.com",
                                NickName = "Minie",
                                ProfileUrl = "ms-appx:///Resource/profile01.png",
                                Pw = "5678"
                            }
                        }
                    });
                    this.projects = newProjects;
                }
                return this.projects;
            }
            set
            {
                if (this.projects != value)
                {
                    this.projects = value;
                    this.RaisePropertyChanged("Projects");
                }
            }
        }

        private string statusText = "프로젝트를 선택해 주세요";
        /// <summary>
        /// 상태
        ///     - 서버로부터 프로젝트를 불러오는 중입니다
        ///     - 프로젝트를 선택해 주세요
        ///     - 네트워크 오류입니다
        /// </summary>
        public string StatusText
        {
            get { return this.statusText; }
            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.RaisePropertyChanged("StatusText");
                }
            }
        }
        #endregion

        #region Commands
        private ICommand doRename = null;
        /// <summary>
        /// 선택한 프로젝트 이름 변경 커맨드
        /// </summary>
        public ICommand DoRename
        {
            get
            {
                if (this.doRename == null)
                {
                    this.doRename = new RelayCommand(async () =>
                    {
                        if (this.SelectedProject == null)
                        {
                            var pop = new MessageDialog("프로젝트를 선택해 주세요");
                            await pop.ShowAsync();
                            return;
                        }

                        string newName = await this.InTextDialog("변경할 프로젝트 이름");
                        if (newName != "")
                        {
                            var targetProject = this.Projects.Where(p => p.Title.Equals(this.SelectedProject.Title)).FirstOrDefault();
                            targetProject.Title = newName;
                        }
                    });
                }
                return this.doRename;
            }
        }

        private ICommand doDelete = null;
        /// <summary>
        /// 선택한 프로젝트 삭제 커맨드
        /// </summary>
        public ICommand DoDelete
        {
            get
            {
                if (this.doDelete == null)
                {
                    this.doDelete = new RelayCommand(async () =>
                    {
                        if (this.SelectedProject == null)
                        {
                            var pop = new MessageDialog("삭제할 프로젝트를 선택해 주세요");
                            await pop.ShowAsync();
                            return;
                        }

                        string newName = await this.InTextDialog("삭제하려면 프로젝트 이름을 입력해 주세요");
                        if (newName != "")
                        {
                            var targetProject = this.Projects.Where(p => p.Title.Equals(this.SelectedProject.Title)).FirstOrDefault();
                            if (newName.Equals(targetProject.Title))
                            {
                                this.Projects.Remove(targetProject);
                            }
                            else
                            {
                                var pop = new MessageDialog("프로젝트 이름이 다릅니다");
                                await pop.ShowAsync();
                                return;
                            }
                        }
                    });
                }
                return this.doDelete;
            }
        }

        private ICommand doNavMain = null;
        public ICommand DoNavMain
        {
            get
            {
                if (this.doNavMain == null)
                {
                    this.doNavMain = new RelayCommand(async () =>
                    {
                        if (this.SelectedProject == null)
                        {
                            var pop = new MessageDialog("프로젝트를 선택해 주세요");
                            await pop.ShowAsync();
                            return;
                        }
                        var dict = new Dictionary<string, object>();
                        dict["userInfo"] = this.UserInfo;
                        dict["projectInfo"] = this.SelectedProject;

                        this.navService.Navigate(typeof(MainView), dict);
                    });
                }
                return this.doNavMain;
            }
        }

        #endregion

        #region Utils
        private async Task<string> InTextDialog(string title)
        {
            var textBox = new TextBox();
            textBox.AcceptsReturn = false;
            textBox.Height = 32;

            var contentDlg = new ContentDialog();
            contentDlg.Content = textBox;
            contentDlg.Title = title;
            contentDlg.IsSecondaryButtonEnabled = true;
            contentDlg.PrimaryButtonText = "확인";
            contentDlg.SecondaryButtonText = "취소";

            if (await contentDlg.ShowAsync() == ContentDialogResult.Primary)
            {
                return textBox.Text;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
