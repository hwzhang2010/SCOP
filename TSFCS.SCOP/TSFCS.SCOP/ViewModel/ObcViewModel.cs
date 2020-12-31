using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;

using TSFCS.SCOP.Helper;
using TSFCS.SCOP.Model;
using TSFCS.SCOP.Udp;

namespace TSFCS.SCOP.ViewModel
{
    public class ObcViewModel : ViewModelBase
    {
        #region Field
        
        #endregion

        #region Property
        #endregion

        #region Command
        #endregion

        #region Constructor
        public ObcViewModel()
        {
        }
        #endregion

        #region Override Method
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion

        #region Messenger Handler
        #endregion
    }
}
