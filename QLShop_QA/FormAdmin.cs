using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShop_QA
{
    public partial class FormAdmin : System.Windows.Forms.Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            FormQLKhachHang formQLkh = new FormQLKhachHang();
            formQLkh.ShowDialog();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            FormQLNhanVien formQLnv = new FormQLNhanVien();
            formQLnv.ShowDialog();
        }

        private void btnQLSP_Click(object sender, EventArgs e)
        {
            FormQLHang formQLHang = new FormQLHang();
            formQLHang.ShowDialog();
        }

        private void btnQLHD_Click(object sender, EventArgs e)
        {
            FormQLHoaDon formQLHoaDon = new FormQLHoaDon();
            formQLHoaDon.ShowDialog();
        }

        private void FormAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            new FormDangNhap().Show();
            this.Hide();
        }
    }
}
