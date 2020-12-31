using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TSFCS.SCOP.Model
{
    public class UserModel : ObservableObject
    {
        #region Field
        private int id;
        private string username;
        private string password;
        private int isRemained;
        #endregion

        #region Property
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("Username");
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
        public int IsRemained
        {
            get { return isRemained; }
            set
            {
                isRemained = value;
                RaisePropertyChanged("IsRemained");
            }
        }
        #endregion

        #region Constructor
        public UserModel()
        {
        }

        public UserModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        #endregion

    }
}
