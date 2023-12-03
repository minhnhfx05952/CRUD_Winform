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
    public partial class FormDangNhap : System.Windows.Forms.Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
            btnDN.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void txtTDN_Leave(object sender, EventArgs e)
        {
            if(txtTDN.Text.Trim()=="")
            {
                this.errorTenDangNhap.SetError(txtTDN,"Bạn chưa nhập tên đăng nhập");
            }else
            {
                this.errorTenDangNhap.Clear();
            }    
        }

        private void txtMK_Leave(object sender, EventArgs e)
        {
            if (txtMK.Text.Trim() == "")
            {
                this.errorMatKhau.SetError(txtMK, "Bạn chưa nhập mật khẩu");
            }
            else
            {
                this.errorMatKhau.Clear();
            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var nv = db.nhanViens.FirstOrDefault(x => x.taikhoan.Equals(txtTDN.Text) && x.matkhau.Equals(txtMK.Text));
                
               if (nv!=null)
                {
                    if(nv.idQuyen==1)//admin
                    {
                        new FormAdmin().Show();
                    }else
                        if (nv.idQuyen == 2)
                    {
                        new FormNhanVien().Show();

                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tai khoan hoac mat khau", "Thong bao!", MessageBoxButtons.OK);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
