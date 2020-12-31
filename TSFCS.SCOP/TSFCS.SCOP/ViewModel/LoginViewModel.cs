using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using CommonServiceLocator;

using Dapper;

using TSFCS.SCOP.Model;

namespace TSFCS.SCOP.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Field
        private readonly string strConnection = "Data Source=db_scop.db3";
        private ObservableCollection<UserModel> users;
        private UserModel user;
        private string password;
        private bool isRemained;
        #endregion

        #region Property
        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }
        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public bool IsRemained
        {
            get { return isRemained; }
            set
            {
                isRemained = value;
                RaisePropertyChanged("IsRemained");
            }
        }
        #endregion

        #region Command
        private bool CanLoadedExecute()
        {
            return true;
        }
        private void LoadedExecute()
        {
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                List<UserModel> list = connection.Query<UserModel>("select * from t_user").AsList<UserModel>();  //获取所有用户信息
                Users = new ObservableCollection<UserModel>(list);
                User = Users[0];  //默认选中第1个用户
                if (IsRemained = User.IsRemained == 1)  //记住密码
                {
                    Password = users[0].Password;
                }
            }
            //Messenger.Default.Send<string>("Loaded", "Login");
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }

        private bool CanDragMoveExecute()
        {
            return true;
        }
        private void DragMoveExecute()
        {
            Messenger.Default.Send<string>("DragMove", "Login");
        }
        public ICommand DragMoveCommand { get { return new RelayCommand(DragMoveExecute, CanDragMoveExecute); } }

        private bool CanClosedExecute()
        {
            return true;
        }
        private void ClosedExecute()
        {
            Messenger.Default.Send<string>("Closed", "Login");
        }
        public ICommand ClosedCommand { get { return new RelayCommand(ClosedExecute, CanClosedExecute); } }

        private bool CanMinExecute()
        {
            return true;
        }
        private void MinExecute()
        {
            Messenger.Default.Send<string>("Min", "Login");
        }
        public ICommand MinCommand { get { return new RelayCommand(MinExecute, CanMinExecute); } }

        private bool CanSetExecute()
        {
            return true;
        }
        private void SetExecute()
        {
            Messenger.Default.Send<string>("Set", "Login");
        }
        public ICommand SetCommand { get { return new RelayCommand(SetExecute, CanSetExecute); } }

        private bool CanUserChangedExecute(int id)
        {
            return true;
        }
        private void UserChangedExecute(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                List<UserModel> list = connection.Query<UserModel>("select * from t_user where id = @id", new { id = id }).AsList<UserModel>();  //获取所有用户信息
                User = list[0];  //选中用户
                Password = string.Empty;
                if (IsRemained = User.IsRemained == 1)  //记住密码
                {
                    Password = users[0].Password;
                }
            }
        }
        public ICommand UserChangedCommand { get { return new RelayCommand<int>((id) => UserChangedExecute(id), CanUserChangedExecute); } }

        private bool CanLoginExecute()
        {
            return true;
        }
        private void LoginExecute()
        {
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                string strSql = @"update t_user set password = @password, isremained = @isremained WHERE username = @username";  //数据库更新语句
                connection.Execute(strSql, new { password = this.Password, isremained = Convert.ToInt32(this.IsRemained), username = this.User.Username });  //更新数据库记录
            }

            Messenger.Default.Send<string>("Login", "Login");
        }
        public ICommand LoginCommand { get { return new RelayCommand(LoginExecute, CanLoginExecute); } }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
        }
        #endregion
        
         #region Override Method
        public override void Cleanup()
        {
            base.Cleanup();
        }
        #endregion
    }
}
