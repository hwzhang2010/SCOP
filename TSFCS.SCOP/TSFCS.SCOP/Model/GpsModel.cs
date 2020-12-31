using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TSFCS.SCOP.Model
{

    public class GpsItemModel : ObservableObject
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
        public static ObservableCollection<GpsItemModel> GetGpsComm()
        {
            ObservableCollection<GpsItemModel> models = new ObservableCollection<GpsItemModel>();
            models.Add(new GpsItemModel() { IsCheck = true, Name = "CAN" });
            models.Add(new GpsItemModel() { IsCheck = false, Name = "422" });

            return models;
        }
        #endregion
    }


    public class GpsModel : ObservableObject
    {
        #region Field
        private int num;
        private string name;
        private string hex;
        private string cal;
        #endregion

        #region Property
        public int Num
        {
            get { return num; }
            set
            {
                num = value;
                RaisePropertyChanged("Num");
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
        public string Hex
        {
            get { return hex; }
            set
            {
                hex = value;
                RaisePropertyChanged("Hex");
            }
        }
        public string Cal
        {
            get { return cal; }
            set
            {
                cal = value;
                RaisePropertyChanged("Cal");
            }
        }
        #endregion

        #region Method
        public static ObservableCollection<GpsModel> GetGps()
        { 
            ObservableCollection<GpsModel> models = new ObservableCollection<GpsModel>();
            models.Add(new GpsModel() { Num = 0, Name = "工作状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 1, Name = "定位模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 2, Name = "定位星数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 3, Name = "历元时间", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 4, Name = "卫星位置X方向(km)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 5, Name = "卫星位置Y方向(km)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 6, Name = "卫星位置Z方向(km)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 7, Name = "卫星速度X方向(km/s)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 8, Name = "卫星速度Y方向(km/s)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new GpsModel() { Num = 9, Name = "卫星速度Z方向(km/s)", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class GpsShowModel
    {
        #region Field
        public int Num { get; set; }
        public string Name { get; set; }
        public string Hex { get; set; }
        public string Cal { get; set; }
        #endregion

        #region Constructor
        public GpsShowModel() 
        {
        }

        public GpsShowModel(int num, string name, string hex, string cal)
        {
            this.Num = num;
            this.Name = name;
            this.Hex = hex;
            this.Cal = cal;
        }
        #endregion
    }


}
