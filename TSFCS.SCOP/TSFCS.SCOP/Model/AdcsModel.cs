using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TSFCS.SCOP.Model
{
    public class AdcsItemModel : ObservableObject
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
        public static ObservableCollection<AdcsItemModel> GetAdcsSoftOnOff()
        {
            ObservableCollection<AdcsItemModel> models = new ObservableCollection<AdcsItemModel>();
            models.Add(new AdcsItemModel() { IsCheck = true, Name = "关" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "开" });

            return models;
        }
        public static ObservableCollection<AdcsItemModel> GetAdcsWorkMode()
        {
            ObservableCollection<AdcsItemModel> models = new ObservableCollection<AdcsItemModel>();
            models.Add(new AdcsItemModel() { IsCheck = true, Name = "1" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "2" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "3" });

            return models;
        }
        public static ObservableCollection<AdcsItemModel> GetAdcsMwSelect()
        {
            ObservableCollection<AdcsItemModel> models = new ObservableCollection<AdcsItemModel>();
            models.Add(new AdcsItemModel() { IsCheck = true, Name = "X" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "Y" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "Z" });
            models.Add(new AdcsItemModel() { IsCheck = false, Name = "S" });

            return models;
        }
        #endregion
    }

    public class AdcsCtrlModeModel : ObservableObject
    {
        #region Field
        private int id;
        private string name;
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

        #region Constructor
        public AdcsCtrlModeModel()
        {
        }

        public AdcsCtrlModeModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        #endregion

        #region Method
        public static ObservableCollection<AdcsCtrlModeModel> GetAdcsCtrlMode()
        {
            ObservableCollection<AdcsCtrlModeModel> models = new ObservableCollection<AdcsCtrlModeModel>();
            models.Add(new AdcsCtrlModeModel() { Id = 0, Name = "1" });
            models.Add(new AdcsCtrlModeModel() { Id = 1, Name = "2" });
            models.Add(new AdcsCtrlModeModel() { Id = 2, Name = "3" });
            models.Add(new AdcsCtrlModeModel() { Id = 3, Name = "31" });
            models.Add(new AdcsCtrlModeModel() { Id = 4, Name = "32" });
            models.Add(new AdcsCtrlModeModel() { Id = 5, Name = "33" });

            return models;
        }
        #endregion
    }

    public class AdcsDataModel : ObservableObject
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
        public static ObservableCollection<AdcsDataModel> GetAdcsData()
        {
            ObservableCollection<AdcsDataModel> models = new ObservableCollection<AdcsDataModel>();
            models.Add(new AdcsDataModel() { Num = 0, Name = "星上软件工作模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 1, Name = "滤波器模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 2, Name = "控制模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 3, Name = "控制周期", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 4, Name = "X轴角速率(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 5, Name = "Y轴角速率(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 6, Name = "Z轴角速率(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 7, Name = "姿态四元数q0(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 8, Name = "姿态四元数q1(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 9, Name = "姿态四元数q2(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 10, Name = "姿态四元数q3(bi)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 11, Name = "GPS周", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 12, Name = "GPS秒", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 13, Name = "SGP4_卫星位置R[0]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 14, Name = "SGP4_卫星位置R[1]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 15, Name = "SGP4_卫星位置R[2]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 16, Name = "SGP4_卫星角速度V[0]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 17, Name = "SGP4_卫星角速度V[1]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 18, Name = "SGP4_卫星角速度V[2]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 19, Name = "解算磁场值BfLO[0]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 19, Name = "解算磁场值BfLO[1]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 20, Name = "解算磁场值BfLO[2]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 21, Name = "解算太阳矢量SUN_V[0]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 22, Name = "解算太阳矢量SUN_V[1]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 23, Name = "解算太阳矢量SUN_V[2]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 24, Name = "轨道角速度Wo", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 25, Name = "磁强计1X轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 26, Name = "磁强计1Y轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 27, Name = "磁强计1Z轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 28, Name = "磁强计2X轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 29, Name = "磁强计2Y轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 30, Name = "磁强计2Z轴测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 31, Name = "磁力矩器输出值[0]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 32, Name = "磁力矩器输出值[1]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 33, Name = "磁力矩器输出值[2]", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 34, Name = "两轴太阳角矢量α角度", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 35, Name = "两轴太阳角矢量β角度", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 36, Name = "反作用轮X转速测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 37, Name = "反作用轮Y转速测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 38, Name = "反作用轮Z转速测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 39, Name = "反作用轮S转速测量值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 40, Name = "陀螺X轴角速率", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 41, Name = "陀螺Y轴角速率", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AdcsDataModel() { Num = 42, Name = "陀螺Z轴角速率", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }


    public class MwItemModel : ObservableObject
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
        public static ObservableCollection<MwItemModel> GetMwSelect()
        {
            ObservableCollection<MwItemModel> models = new ObservableCollection<MwItemModel>();
            models.Add(new MwItemModel() { IsCheck = true, Name = "X" });
            models.Add(new MwItemModel() { IsCheck = false, Name = "Y" });
            models.Add(new MwItemModel() { IsCheck = false, Name = "Z" });
            models.Add(new MwItemModel() { IsCheck = false, Name = "S" });

            return models;
        }

        public static ObservableCollection<MwItemModel> GetMwMode()
        {
            ObservableCollection<MwItemModel> models = new ObservableCollection<MwItemModel>();
            models.Add(new MwItemModel() { IsCheck = true, Name = "速度" });
            models.Add(new MwItemModel() { IsCheck = false, Name = "力矩" });

            return models;
        }
        #endregion
    }

    public class MwDataModel : ObservableObject
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
        public static ObservableCollection<MwDataModel> GetMwData()
        {
            ObservableCollection<MwDataModel> models = new ObservableCollection<MwDataModel>();
            models.Add(new MwDataModel() { Num = 0, Name = "转速", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MwDataModel() { Num = 1, Name = "电流", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MwDataModel() { Num = 2, Name = "状态", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class MmItemModel : ObservableObject
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
        public static ObservableCollection<MmItemModel> GetMmSelect()
        {
            ObservableCollection<MmItemModel> models = new ObservableCollection<MmItemModel>();
            models.Add(new MmItemModel() { IsCheck = true, Name = "磁强计1" });
            models.Add(new MmItemModel() { IsCheck = false, Name = "磁强计2" });

            return models;
        }
        #endregion
    }

    public class MmDataModel : ObservableObject
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
        public static ObservableCollection<MmDataModel> GetMmData()
        {
            ObservableCollection<MmDataModel> models = new ObservableCollection<MmDataModel>();
            models.Add(new MmDataModel() { Num = 0, Name = "X方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MmDataModel() { Num = 1, Name = "Y方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MmDataModel() { Num = 2, Name = "Z方向", Hex = string.Empty, Cal = string.Empty });

            return models;
        }

        public static ObservableCollection<MmDataModel> GetTmrData()
        {
            ObservableCollection<MmDataModel> models = new ObservableCollection<MmDataModel>();
            models.Add(new MmDataModel() { Num = 0, Name = "X方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MmDataModel() { Num = 1, Name = "Y方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new MmDataModel() { Num = 2, Name = "Z方向", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class MtItemModel : ObservableObject
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
        public static ObservableCollection<MtItemModel> GetMtAxis()
        {
            ObservableCollection<MtItemModel> models = new ObservableCollection<MtItemModel>();
            models.Add(new MtItemModel() { IsCheck = true, Name = "X轴" });
            models.Add(new MtItemModel() { IsCheck = false, Name = "Y轴" });
            models.Add(new MtItemModel() { IsCheck = false, Name = "Z轴" });

            return models;
        }
        #endregion
    }


    public class StarItemModel : ObservableObject
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
        public static ObservableCollection<StarItemModel> GetStarSelect()
        {
            ObservableCollection<StarItemModel> models = new ObservableCollection<StarItemModel>();
            models.Add(new StarItemModel() { IsCheck = true, Name = "1" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "2" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "3" });

            return models;
        }

        public static ObservableCollection<StarItemModel> GetStarWorkMode()
        {
            ObservableCollection<StarItemModel> models = new ObservableCollection<StarItemModel>();
            models.Add(new StarItemModel() { IsCheck = true, Name = "自检" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "正常" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "自适应" });

            return models;
        }

        public static ObservableCollection<StarItemModel> GetStarFilter()
        {
            ObservableCollection<StarItemModel> models = new ObservableCollection<StarItemModel>();
            models.Add(new StarItemModel() { IsCheck = true, Name = "关闭" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "打开" });

            return models;
        }

        public static ObservableCollection<StarItemModel> GetStarPict()
        {
            ObservableCollection<StarItemModel> models = new ObservableCollection<StarItemModel>();
            models.Add(new StarItemModel() { IsCheck = true, Name = "关闭" });
            models.Add(new StarItemModel() { IsCheck = false, Name = "打开" });

            return models;
        }
        #endregion
    }

    public class StarDataModel : ObservableObject
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
        public static ObservableCollection<StarDataModel> GetStarData()
        {
            ObservableCollection<StarDataModel> models = new ObservableCollection<StarDataModel>();
            models.Add(new StarDataModel() { Num = 0, Name = "姿态四元数Q0", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 1, Name = "姿态四元数Q1", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 2, Name = "姿态四元数Q2", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 3, Name = "姿态四元数Q3", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 4, Name = "内部时间(整数s)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 5, Name = "内部时间(小数40.96us)", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 6, Name = "温度", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 7, Name = "图像曝光值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 8, Name = "图像阈值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 9, Name = "图像背景值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 10, Name = "自检结果", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 11, Name = "内部参数监测", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 12, Name = "成像模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 13, Name = "图像传输开关", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 14, Name = "导航星数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 15, Name = "数据有效标志", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 16, Name = "图像帧号", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 17, Name = "滤波开关", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 18, Name = "四星寻找阈值", Hex = string.Empty, Cal = string.Empty });
            models.Add(new StarDataModel() { Num = 19, Name = "跟踪阈值", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class AssDataModel : ObservableObject
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
        public static ObservableCollection<AssDataModel> GetAssData()
        {
            ObservableCollection<AssDataModel> models = new ObservableCollection<AssDataModel>();
            models.Add(new AssDataModel() { Num = 0, Name = "+X方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AssDataModel() { Num = 1, Name = "-X方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AssDataModel() { Num = 2, Name = "+Y方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new AssDataModel() { Num = 3, Name = "-Y方向", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class GyroModel : ObservableObject
    {
        #region Field
        private int num;
        private string name;
        private string hex;
        private string cal;
        private string unit;
        private string range;
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
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }
        public string Range
        {
            get { return range; }
            set 
            { 
                range = value;
                RaisePropertyChanged("Range");
            }
        }
        #endregion

        #region Method
        public static ObservableCollection<GyroModel> GetGyroData()
        {
            ObservableCollection<GyroModel> models = new ObservableCollection<GyroModel>();
            models.Add(new GyroModel() { Num = 0, Name = "X轴角速度", Hex = string.Empty, Cal = string.Empty, Unit = "º", Range = string.Empty });
            models.Add(new GyroModel() { Num = 1, Name = "Y轴角速度", Hex = string.Empty, Cal = string.Empty, Unit = "º", Range = string.Empty });
            models.Add(new GyroModel() { Num = 2, Name = "Z轴角速度", Hex = string.Empty, Cal = string.Empty, Unit = "º", Range = string.Empty });
            models.Add(new GyroModel() { Num = 3, Name = "X轴温度", Hex = string.Empty, Cal = string.Empty, Unit = "℃", Range = string.Empty });
            models.Add(new GyroModel() { Num = 4, Name = "Y轴温度", Hex = string.Empty, Cal = string.Empty, Unit = "℃", Range = string.Empty });
            models.Add(new GyroModel() { Num = 5, Name = "Z轴温度", Hex = string.Empty, Cal = string.Empty, Unit = "℃", Range = string.Empty });

            return models;
        }
        #endregion
    }

    

    

}
