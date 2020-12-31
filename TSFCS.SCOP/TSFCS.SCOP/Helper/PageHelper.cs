using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TSFCS.SCOP.Helper
{
    public enum EJumpOperation
    {
        GoHome = 0,
        GoPrevious = 1,
        GoNext = 2,
        GoEnd = 3
    }

    public class PageHelper<T>
    {
        private ObservableCollection<T> dataSource;
        private int pageSize;
        public int PageCount { get; private set; }
        public int CurrentIndex { get; private set; }

        public PageHelper()
        {
            CurrentIndex = 0;
        }

        public PageHelper(ObservableCollection<T> dataSource, int pageSize) : this()
        {
            this.dataSource = dataSource;
            this.pageSize = pageSize;
            this.PageCount = dataSource.Count / pageSize;
            this.PageCount += (dataSource.Count % pageSize) != 0 ? 1 : 0;
        }

        public ObservableCollection<T> GetPageData(EJumpOperation jo)
        {
            switch (jo)
            {
                case EJumpOperation.GoPrevious:
                    if (CurrentIndex > 0)
                        CurrentIndex--;
                    break;
                case EJumpOperation.GoNext:
                    if (CurrentIndex < PageCount - 1)
                        CurrentIndex++;
                    break;
                case EJumpOperation.GoHome:
                    CurrentIndex = 0;
                    break;
                case EJumpOperation.GoEnd:
                    CurrentIndex = PageCount - 1;
                    break;
            }
            ObservableCollection<T> pageData = new ObservableCollection<T>();
            int pageRecord = pageSize;  //每页记录数目
            if (CurrentIndex == (PageCount - 1) && dataSource.Count % pageSize > 0)  //最后1页记录数目
            {
                pageRecord = dataSource.Count % pageSize;
            }
            if (dataSource != null)
            {
                for (int i = 0; i < pageRecord; i++)
                {
                    if (CurrentIndex * pageSize + i < dataSource.Count)
                        pageData.Add(dataSource[CurrentIndex * pageSize + i]);
                    else
                        break;
                }
            }
            return pageData;
        }
    }
}
