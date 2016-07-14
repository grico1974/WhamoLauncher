using System;
using System.Windows.Forms;

namespace WhamoLauncher.Charts.Views
{
    internal class AnimatedLabel: Label
    {
        private readonly Timer timer;
        private int tickCount;
        public event EventHandler AnimationTick;

        public AnimatedLabel()
        {
            timer = new Timer();
            timer.Tick += timer_Tick;
        }

        private string waitText;
        public string WaitText 
        {
            get { return waitText; }
            set
            {
                waitText = value;
                Text = waitText;
            }
        }

        public int TimerInterval 
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        public int AnimatedTicksBeforeTextReset { get; set; }
        public void StartAnimation() => timer.Start();
        public void StopAnimation() => timer.Stop();


        protected override void OnHandleDestroyed(EventArgs e)
        {
            timer.Dispose();
            base.OnHandleDestroyed(e);
        }

        protected virtual void OnTimerTick(EventArgs e)
        {
            if (tickCount == AnimatedTicksBeforeTextReset)
            {
                Text = waitText;
                tickCount = 0;
            }
            else
            {
                var newText = string.Concat(Text, '.');
                Invoke(new Action(() => Text = newText));
                tickCount++;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (AnimationTick != null)
            {
                AnimationTick(this, e);
            }

            OnTimerTick(e);
        }
    }
}
