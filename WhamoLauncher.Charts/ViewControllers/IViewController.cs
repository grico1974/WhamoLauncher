using System;
using System.Windows.Forms;
using WhamoLauncher.Charts.Views;

namespace WhamoLauncher.Charts.ViewControllers
{
    internal interface IViewController: IDisposable
    {
        void ProcessCommand(Control sender, Command command);
        object ShowViewDialog();
        object ShowViewDialog(IViewController owner);
        void ShowView();
        void CloseView();
        void SetViewAsApplicationMainWindow(ApplicationContext context);
        void ShowErrorMessage(string message, string caption);
        ViewBase View { get; }
    }
}
