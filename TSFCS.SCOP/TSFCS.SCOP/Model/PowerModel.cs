using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TSFCS.SCOP.Model
{
    public class PowerSwitchModel : ObservableObject
    {
        #region Field
        private string num;
        private string name;
        private string status;
        private string content;
        #endregion

        #region Property
        public string Num
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
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                RaisePropertyChanged("Content");
            }
        }
        #endregion

        #region Method
        public static ObservableCollection<PowerSwitchModel> GetPowerSwitchs()
        {
            ObservableCollection<PowerSwitchModel> models = new ObservableCollection<PowerSwitchModel>();
            models.Add(new PowerSwitchModel() { Num = "K1", Name = "CMD0", Status = "0/关", Content = "GPS电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K2", Name = "CMD0", Status = "1/开", Content = "GPS电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K3", Name = "CMD1", Status = "0/关", Content = "相机电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K4", Name = "CMD1", Status = "1/开", Content = "相机电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K5", Name = "CMD2", Status = "0/关", Content = "数传1电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K6", Name = "CMD2", Status = "1/开", Content = "数传1电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K7", Name = "CMD3", Status = "0/关", Content = "数传2电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K8", Name = "CMD3", Status = "1/开", Content = "数传2电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K9", Name = "CMD4", Status = "0/关", Content = "反作用X电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K10", Name = "CMD4", Status = "1/开", Content = "反作用X电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K11", Name = "CMD5", Status = "0/关", Content = "反作用Y电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K12", Name = "CMD5", Status = "1/开", Content = "反作用Y电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K13", Name = "CMD6", Status = "0/关", Content = "反作用Z电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K14", Name = "CMD6", Status = "1/开", Content = "反作用Z电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K15", Name = "CMD7", Status = "0/关", Content = "反作用S电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K16", Name = "CMD7", Status = "1/开", Content = "反作用S电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K17", Name = "CMD8", Status = "0/关", Content = "磁力矩器X电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K18", Name = "CMD8", Status = "1/开", Content = "磁力矩器X电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K19", Name = "CMD9", Status = "0/关", Content = "磁力矩器Y电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K20", Name = "CMD9", Status = "1/开", Content = "磁力矩器Y电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K21", Name = "CMD10", Status = "0/关", Content = "磁力矩器Z电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K22", Name = "CMD10", Status = "1/开", Content = "磁力矩器Z电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K23", Name = "CMD11", Status = "0/关", Content = "磁强计1电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K24", Name = "CMD11", Status = "1/开", Content = "磁强计1电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K25", Name = "CMD12", Status = "0/关", Content = "磁强计2电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K26", Name = "CMD12", Status = "1/开", Content = "磁强计2电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K27", Name = "CMD13", Status = "0/关", Content = "实验磁强计电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K28", Name = "CMD13", Status = "1/开", Content = "实验磁强计电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K29", Name = "CMD14", Status = "0/关", Content = "星敏1电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K30", Name = "CMD14", Status = "1/开", Content = "星敏1电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K31", Name = "CMD15", Status = "0/关", Content = "星敏2电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K32", Name = "CMD15", Status = "1/开", Content = "星敏2电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K33", Name = "CMD16", Status = "0/关", Content = "星敏3电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K34", Name = "CMD16", Status = "1/开", Content = "星敏3电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K35", Name = "CMD17", Status = "0/关", Content = "太阳敏电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K36", Name = "CMD17", Status = "1/开", Content = "太阳敏电源ON" });
            models.Add(new PowerSwitchModel() { Num = "K37", Name = "CMD18", Status = "0/关", Content = "陀螺电源OFF" });
            models.Add(new PowerSwitchModel() { Num = "K38", Name = "CMD18", Status = "1/开", Content = "陀螺电源ON" });

            return models;
        }
        #endregion
    }


    public class PowerDataModel : ObservableObject
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
        public static ObservableCollection<PowerDataModel> GetPowerDatas()
        {
            ObservableCollection<PowerDataModel> models = new ObservableCollection<PowerDataModel>();
            models.Add(new PowerDataModel() { Num = 0, Name = "母线电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 1, Name = "母线电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 2, Name = "帆板电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 3, Name = "帆板电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 4, Name = "TTC电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 5, Name = "TTC电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 6, Name = "OBC电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 7, Name = "OBC电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 8, Name = "GPS电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 9, Name = "GPS电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 10, Name = "相机电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 11, Name = "相机电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 12, Name = "数传1电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 13, Name = "数传1电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 14, Name = "数传2电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 15, Name = "数传2电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 16, Name = "反作用轮X电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 17, Name = "反作用轮X电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 18, Name = "反作用轮Y电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 19, Name = "反作用轮Y电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 20, Name = "反作用轮Z电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 21, Name = "反作用轮Z电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 22, Name = "反作用轮S电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 23, Name = "反作用轮S电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 24, Name = "磁力矩器X电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 25, Name = "磁力矩器X电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 26, Name = "磁力矩器X电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 27, Name = "磁力矩器X电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 28, Name = "磁力矩器X电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 29, Name = "磁力矩器X电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 30, Name = "磁强计1电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 31, Name = "磁强计1电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 32, Name = "磁强计2电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 33, Name = "磁强计2电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 34, Name = "实验磁强计电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 35, Name = "实验磁强计电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 36, Name = "星敏1电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 37, Name = "星敏1电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 38, Name = "星敏2电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 39, Name = "星敏2电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 40, Name = "星敏3电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 41, Name = "星敏3电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 42, Name = "太阳敏电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 43, Name = "太阳敏电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 44, Name = "陀螺电压", Hex = string.Empty, Cal = string.Empty });
            models.Add(new PowerDataModel() { Num = 45, Name = "陀螺电流", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }






}
