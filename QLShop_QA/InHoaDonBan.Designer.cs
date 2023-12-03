
namespace QLShop_QA
{
    partial class InHoaDonBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cRVHoaDonBan = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cRVHoaDonBan
            // 
            this.cRVHoaDonBan.ActiveViewIndex = -1;
            this.cRVHoaDonBan.AllowDrop = true;
            this.cRVHoaDonBan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cRVHoaDonBan.Cursor = System.Windows.Forms.Cursors.Default;
            this.cRVHoaDonBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cRVHoaDonBan.Location = new System.Drawing.Point(0, 0);
            this.cRVHoaDonBan.Name = "cRVHoaDonBan";
            this.cRVHoaDonBan.Size = new System.Drawing.Size(800, 450);
            this.cRVHoaDonBan.TabIndex = 0;
            // 
            // InHoaDonBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cRVHoaDonBan);
            this.Name = "InHoaDonBan";
            this.Text = "In hóa đơn bán";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer cRVHoaDonBan;
    }
}