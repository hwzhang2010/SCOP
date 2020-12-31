using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using GalaSoft.MvvmLight;


namespace TSFCS.SCOP.Model
{
    public class DigitItemModel : ObservableObject
    {
        #region Field
        private bool isCheck;
        private string name;
        #endregion

        #region Property
        public bool IsCheck
        {
            get { return isCheck; }
            set
            {
                isCheck = value;
                RaisePropertyChanged("IsCheck");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        #endregion

        #region Method
        public static ObservableCollection<DigitItemModel> GetDigitSelect()
        {
            ObservableCollection<DigitItemModel> models = new ObservableCollection<DigitItemModel>();
            models.Add(new DigitItemModel() { IsCheck = true, Name = "1" });
            models.Add(new DigitItemModel() { IsCheck = false, Name = "2" });

            return models;
        }
        public static ObservableCollection<DigitItemModel> GetDigitTransmit()
        {
            ObservableCollection<DigitItemModel> models = new ObservableCollection<DigitItemModel>();
            models.Add(new DigitItemModel() { IsCheck = true, Name = "开机" });
            models.Add(new DigitItemModel() { IsCheck = false, Name = "关机" });

            return models;
        }
        public static ObservableCollection<DigitItemModel> GetDigitMode()
        {
            ObservableCollection<DigitItemModel> models = new ObservableCollection<DigitItemModel>();
            models.Add(new DigitItemModel() { IsCheck = true, Name = "遥测" });
            models.Add(new DigitItemModel() { IsCheck = false, Name = "数传" });

            return models;
        }
        public static ObservableCollection<DigitItemModel> GetDigitRefresh()
        {
            ObservableCollection<DigitItemModel> models = new ObservableCollection<DigitItemModel>();
            models.Add(new DigitItemModel() { IsCheck = true, Name = "使能" });
            models.Add(new DigitItemModel() { IsCheck = false, Name = "禁止" });

            return models;
        }
        #endregion
    }
}
