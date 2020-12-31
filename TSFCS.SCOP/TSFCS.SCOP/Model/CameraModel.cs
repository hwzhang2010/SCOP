using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TSFCS.SCOP.Model
{
    public class CameraParamModel : ObservableObject
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
        public static ObservableCollection<CameraParamModel> GetCameraParam()
        {
            ObservableCollection<CameraParamModel> models = new ObservableCollection<CameraParamModel>();
            models.Add(new CameraParamModel() { Num = 0, Name = "发送时间", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 1, Name = "原始图像硬盘状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 2, Name = "成像模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 3, Name = "数传模式", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 4, Name = "相机上电加载SPI状态位", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 5, Name = "相机LVDS通道Training状态位", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 6, Name = "压缩通道1", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 7, Name = "压缩通道2", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 8, Name = "DDR2初始化状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 9, Name = "工作状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 10, Name = "相机图像读出方向", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 11, Name = "硬盘读写状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 12, Name = "传感器Training成功计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 13, Name = "压缩因子", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 14, Name = "原始图像存储盘可用存储容量", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 15, Name = "原始图像写文件号", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 16, Name = "原始图像读文件号", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 17, Name = "原始图像读文件偏移", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 18, Name = "行周期", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 19, Name = "当前所使用的总线", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 20, Name = "指令计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 21, Name = "错误指令计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 22, Name = "最后一条指令帧编号（填写指令帧仲裁域ID20~ID13）", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 23, Name = "指令接收状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 24, Name = "辅助数据帧计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 25, Name = "相机模拟增益", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 26, Name = "相机全色数字增益", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 27, Name = "相机多光谱数字增益", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 28, Name = "全色谱段积分级数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 29, Name = "多光谱段积分级数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraParamModel() { Num = 30, Name = "真正帧周期", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class CameraResponseModel : ObservableObject
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
        public static ObservableCollection<CameraResponseModel> GetCameraResponse()
        {
            ObservableCollection<CameraResponseModel> models = new ObservableCollection<CameraResponseModel>();
            models.Add(new CameraResponseModel() { Num = 0, Name = "当前所使用的总线", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 1, Name = "指令计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 2, Name = "最后一条指令帧编号", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 3, Name = "指令接收状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 4, Name = "接收错误指令计数", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 5, Name = "自检状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 6, Name = "自检状态", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 7, Name = "自检状态", Hex = string.Empty, Cal = string.Empty });

            return models;
        }

        public static ObservableCollection<CameraResponseModel> GetCameraFileResponse()
        {
            ObservableCollection<CameraResponseModel> models = new ObservableCollection<CameraResponseModel>();
            models.Add(new CameraResponseModel() { Num = 0, Name = "原始图像文件号", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 1, Name = "存储星上时", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 2, Name = "原始图像文件大小", Hex = string.Empty, Cal = string.Empty });
            models.Add(new CameraResponseModel() { Num = 3, Name = "当前原始图像文件总数目", Hex = string.Empty, Cal = string.Empty });

            return models;
        }
        #endregion
    }

    public class CameraItemModel : ObservableObject
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
        public static ObservableCollection<CameraItemModel> GetCameraPictMode()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "推扫相机TDI成像" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "推扫相机前端自校" });

            return models;
        }

        public static ObservableCollection<CameraItemModel> GetCameraDigitMode()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "STD有损压缩图像" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "STD无损压缩图像" });

            return models;
        }

        public static ObservableCollection<CameraItemModel> GetCameraPict()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "开始拍照" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "停止拍照" });

            return models;
        }

        public static ObservableCollection<CameraItemModel> GetCameraDigit()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "开始数传" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "停止数传" });

            return models;
        }

        public static ObservableCollection<CameraItemModel> GetCameraIntegralDirection()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "由小到大" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "由大到小" });

            return models;
        }

        public static ObservableCollection<CameraItemModel> GetCameraBus()
        {
            ObservableCollection<CameraItemModel> models = new ObservableCollection<CameraItemModel>();
            models.Add(new CameraItemModel() { IsCheck = true, Name = "自主" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "A" });
            models.Add(new CameraItemModel() { IsCheck = false, Name = "B" });

            return models;
        }
        #endregion
    }

   



}
