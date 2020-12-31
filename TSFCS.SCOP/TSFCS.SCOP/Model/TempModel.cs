using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
namespace TSFCS.SCOP.Model
{
    public class TempModel : ObservableObject
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
        public TempModel()
        {
        }

        public TempModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        #endregion

        #region Method
        public static ObservableCollection<TempModel> GetTemps()
        {
            ObservableCollection<TempModel> models = new ObservableCollection<TempModel>();
            models.Add(new TempModel() { Id = 0, Name = "TTC温度" });
            models.Add(new TempModel() { Id = 1, Name = "OBC1温度" });
            models.Add(new TempModel() { Id = 2, Name = "OBC2温度" });
            models.Add(new TempModel() { Id = 3, Name = "GPS温度" });
            models.Add(new TempModel() { Id = 4, Name = "帆板1温度" });
            models.Add(new TempModel() { Id = 5, Name = "帆板2温度" });
            models.Add(new TempModel() { Id = 6, Name = "帆板3温度" });
            models.Add(new TempModel() { Id = 7, Name = "帆板4温度" });
            models.Add(new TempModel() { Id = 8, Name = "帆板5温度" });
            models.Add(new TempModel() { Id = 9, Name = "帆板6温度" });
            models.Add(new TempModel() { Id = 10, Name = "帆板7温度" });
            models.Add(new TempModel() { Id = 11, Name = "结构面1温度" });
            models.Add(new TempModel() { Id = 12, Name = "结构面2温度" });
            models.Add(new TempModel() { Id = 13, Name = "结构面3温度" });
            models.Add(new TempModel() { Id = 14, Name = "结构面4温度" });
            models.Add(new TempModel() { Id = 15, Name = "结构面5温度" });
            models.Add(new TempModel() { Id = 16, Name = "电源温度" });
            models.Add(new TempModel() { Id = 17, Name = "蓄电池1温度" });
            models.Add(new TempModel() { Id = 18, Name = "蓄电池2温度" });
            models.Add(new TempModel() { Id = 19, Name = "磁强计1温度" });
            models.Add(new TempModel() { Id = 20, Name = "磁强计2温度" });
            models.Add(new TempModel() { Id = 21, Name = "实验磁强计温度" });

            return models;
        }
        #endregion
    }
}