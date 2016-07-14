using System.ComponentModel;
using System.Windows.Forms;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts.Views
{
    internal partial class ViewBase : Form
    {
        public ViewBase()
        {
            InitializeComponent();
        }

        public ViewBase(IViewController viewController)
            :this()
        {
            if (!DesignMode)
            {
                ViewController = viewController;
            }
        }

        public void ShowErrorMessage(string message, string caption) => MessageBox.Show(this, message,
                                                                                        $"{Program.ProductName}: {caption}", 
                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (DesignMode)
                {
                    base.Text = value;
                }
                else
                {
                    base.Text = $"{Program.ProductName}: {value}";
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IViewController ViewController { get; }
        protected virtual void SetControlTexts() { }
    }
}
