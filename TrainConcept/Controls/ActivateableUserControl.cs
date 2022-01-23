using System.Windows.Forms;

namespace SoftObject.TrainConcept.Controls
{
    public partial class ActivateableUserControl : UserControl
    {
        private bool isActive = false;

        public ActivateableUserControl()
        {
            InitializeComponent();
        }

        public virtual void SetActive(bool bIsActive)
        {
            isActive = bIsActive;
        }

    }
}
