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
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            new FormQLKhachHang().Show();
            this.Hide();
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            new FormLapHD().Show();
            this.Hide();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            new FormDangNhap().Show();
            this.Hide();
        }
    }
}
