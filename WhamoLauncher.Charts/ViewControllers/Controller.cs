using System;
using System.Windows.Forms;
using WhamoLauncher.Charts.Views;

namespace WhamoLauncher.Charts.ViewControllers
{
    internal static class Controller
    {
        public static Controller<ViewBase> GetDefaultController() => Controller<ViewBase>.GetController();
        public static Controller<T> GetController<T>() where T : ViewBase => Controller<T>.GetController();
    }

    internal class Controller<T> : IViewController where T : ViewBase
    {
        internal static Controller<T> GetController() => new Controller<T>();

        protected Controller()
        {
            View = Activator.CreateInstance(typeof(T), this) as T;
        }

        private bool disposed;
        protected T View { get; }
        ViewBase IViewController.View => View;
        public virtual object ShowViewDialog() => View.ShowDialog();
        public virtual object ShowViewDialog(IViewController owner) => View.ShowDialog(owner.View);
        public virtual void ShowView() => View.Show();

        public virtual void CloseView()
        {
            if (View.InvokeRequired)
            {
                View.BeginInvoke(new Action(View.Close));
            }
            else
            {
                View.Close();
            }
        }

        public void SetViewAsApplicationMainWindow(ApplicationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.MainForm = View;
        }

        public void ShowErrorMessage(string message, string caption) => View.ShowErrorMessage(message, caption);
        public virtual void ProcessCommand(Control sender, Command command) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isSafe)
        {
            if (disposed)
            {
                return;
            }

            if (isSafe)
            {
                View.Dispose();
            }

            disposed = true;
        }

        ~Controller()
        {
            Dispose(false);
        }
    }
}
