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
    public partial class FormQLNhanVien : System.Windows.Forms.Form
    {
        public FormQLNhanVien()
        {
            InitializeComponent();
        }
        private void FormQLNhanVien_Load(object sender, EventArgs e)
        {
            loadDB();
        }
        public void loadDB()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                gtvQLNV.DataSource = from nv in db.nhanViens
                                     select new
                                     {
                                         maNhanVien = nv.maNhanVien,
                                         tenNhanVien = nv.tenNhanVien,
                                         gioiTinh = nv.gioiTinh,
                                         diaChi = nv.diaChi,
                                         dienThoai = nv.dienThoai,
                                         ngaySinh = nv.ngaySinh,
                                         taiKhoan = nv.taikhoan,
                                         matKhau = nv.matkhau,
                                         idQuyen = nv.idQuyen
                                        };
            }
            txtMaNV.ReadOnly = true;
        }
        private void gtvQLNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtMaNV.Text = gtvQLNV.Rows[numrow].Cells[0].Value.ToString();
                txtTenNV.Text = gtvQLNV.Rows[numrow].Cells[1].Value.ToString();
                cboGioiTinh.Text = gtvQLNV.Rows[numrow].Cells[2].Value.ToString();
                txtDC.Text = gtvQLNV.Rows[numrow].Cells[3].Value.ToString();
                txtDT.Text = gtvQLNV.Rows[numrow].Cells[4].Value.ToString();
                dtpNgaySinh.Text = gtvQLNV.Rows[numrow].Cells[5].Value.ToString();
                txtTaiKhoan.Text = gtvQLNV.Rows[numrow].Cells[6].Value.ToString();
                txtMatKhau.Text = gtvQLNV.Rows[numrow].Cells[7].Value.ToString();
                cboQuyen.Text = gtvQLNV.Rows[numrow].Cells[8].Value.ToString();

            }
            txtMaNV.ReadOnly = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtDC.Clear();
            txtDT.Clear();
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            cboQuyen.Text = "";
            cboGioiTinh.Text = null;
            txtMaNV.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    string manv = gtvQLNV.SelectedCells[0].OwningRow.Cells["maNhanVien"].Value.ToString();
                    nhanVien delete = db.nhanViens.Where(p => p.maNhanVien.Equals(manv)).SingleOrDefault();
                    db.nhanViens.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa không thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
            }
            loadDB();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    nhanVien sua = db.nhanViens.Where(p => p.maNhanVien.Equals(txtMaNV.Text)).Single();
                    sua.maNhanVien = txtMaNV.Text;
                    sua.tenNhanVien = txtTenNV.Text;
                    sua.gioiTinh = cboGioiTinh.Text;
                    sua.diaChi = txtDC.Text;
                    sua.dienThoai = txtDT.Text;
                    sua.ngaySinh = DateTime.Parse(dtpNgaySinh.Text);
                    sua.taikhoan = txtTaiKhoan.Text;
                    sua.matkhau = txtMatKhau.Text;
                    sua.idQuyen = int.Parse(cboQuyen.Text);

                    db.SubmitChanges();
                    MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi rồi!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
               
            }
            loadDB();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    nhanVien them = new nhanVien();
                    them.maNhanVien = txtMaNV.Text;
                    them.tenNhanVien = txtTenNV.Text;
                    them.gioiTinh = cboGioiTinh.Text;
                    them.diaChi = txtDC.Text;
                    them.dienThoai = txtDT.Text;
                    them.ngaySinh = DateTime.Parse(dtpNgaySinh.Text);
                    them.taikhoan = txtTaiKhoan.Text;
                    them.matkhau = txtMatKhau.Text;
                    them.idQuyen = int.Parse(cboQuyen.Text);


                    db.nhanViens.InsertOnSubmit(them);
                    db.SubmitChanges();
                    MessageBox.Show("Lưu thành công!!!!", "Thông báo!", MessageBoxButtons.OK);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu không thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
            }
            loadDB();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                gtvQLNV.DataSource = db.nhanViens.Where(p => p.maNhanVien.Equals(txtTimKiem.Text));
            }
            txtTimKiem.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
       

        private void cboGioiTinh_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void FormQLNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void gtvQLNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
