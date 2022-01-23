using System;
using System.Drawing;
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    public partial class FrmStartSickness : Form
    {
        public DateTime SicknessStart { get; set; } = DateTime.MinValue;
        public DateTime SicknessEnd { get; set; } = DateTime.MinValue;
        public bool HasSicknessRange => radSickTimeSpan.Checked;

        private DateTime ActualDate => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));


        public FrmStartSickness()
        {
            InitializeComponent();
            radSickTimeSpan.Checked = true;
        }

        private void vActivateSicknessRange(bool bIsActive)
        {
            if (bIsActive)
            {
                dateSicknessStart.Enabled = true;
                dateSicknessEnd.Enabled = true;
                dateSicknessStart.Value = ActualDate;
                dateSicknessStart.MinDate = DateTime.Today;
                dateSicknessStart.MaxDate = DateTime.Today;
                dateSicknessEnd.Value = DateTime.Today.AddDays(7);
                dateSicknessEnd.MaxDate = DateTime.Today.AddDays(14);
            }
            else
            {
                dateSicknessStart.Enabled = false;
                dateSicknessEnd.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                if (Wisej.Web.MessageBox.Show("Ungültige Datumsangaben!", "Fehler") == DialogResult.OK)
                {
                    dateSicknessStart.Value = ActualDate;
                    dateSicknessStart.SelectOnEnter = true;
                    dateSicknessStart.Focus();
                }

            }
            else
            {
                SicknessStart = dateSicknessStart.Value;
                SicknessEnd= dateSicknessEnd.Value;
                Close();
                DialogResult = DialogResult.OK;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void radSickTimeSpan_CheckedChanged(object sender, EventArgs e)
        {
            vActivateSicknessRange(radSickTimeSpan.Checked);
        }

        private void radSickStartToday_CheckedChanged(object sender, EventArgs e)
        {
            //vActivateSicknessRange(radSickTimeSpan.Checked);
        }

        private void dateSicknessStart_ValueChanged(object sender, EventArgs e)
        {
            var dt= sender as DateTimePicker;
            if (dt.Value >= dt.MinDate && dt.Value <= dt.MaxDate)
                SicknessStart = dt.Value;
            else
            {
                if (Wisej.Web.MessageBox.Show("Ungültiges Startdatum!", "Fehler") == DialogResult.OK)
                {
                    dt.SelectOnEnter = true;
                    dt.Focus();
                }
            }
        }

        private void dateSicknessEnd_ValueChanged(object sender, EventArgs e)
        {
            var dt = sender as DateTimePicker;
            if (dt.Value >= dt.MinDate && dt.Value <= dt.MaxDate)
                SicknessEnd = dt.Value;
            else
            {
                if (Wisej.Web.MessageBox.Show("Ungültiges Enddatum!", "Fehler") == DialogResult.OK)
                {
                    dt.SelectOnEnter = true;
                    dt.Focus();
                }
            }
        }

        public bool ValidateInput()
        {
            if (dateSicknessStart.Value != DateTime.MinValue && dateSicknessEnd.Value!= DateTime.MinValue)
            {
                if (dateSicknessStart.Value < DateTime.Today || dateSicknessEnd.Value < dateSicknessStart.Value ||
                    (dateSicknessEnd.Value.DayOfYear - dateSicknessStart.Value.DayOfYear) > 14)
                    return false;
                return true;
            }
            return false;
        }
    }
}
