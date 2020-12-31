using System;

namespace TSFCS.SCOP
{
    public delegate void Eventhandler(object sender, EventArgs e);

    public class TimerWait
    {
        public event EventHandler Elapsed;

        private void EveryInterval(object obj, EventArgs e)
        {
            if (Elapsed != null)
                Elapsed(obj, e);
        }

        public System.Timers.Timer MyTimer;

        public TimerWait()
        {
            MyTimer = new System.Timers.Timer();
            MyTimer.Elapsed += EveryInterval;
            MyTimer.Interval = 1000;  //应答计时器，默认为1000ms
            MyTimer.Enabled = false;
        }

        public TimerWait(int interval)
        {
            MyTimer = new System.Timers.Timer();
            MyTimer.Elapsed += EveryInterval;
            MyTimer.Interval = interval;  //应答计时器，根据参数设定
            MyTimer.Enabled = false;
        }

    }
}
