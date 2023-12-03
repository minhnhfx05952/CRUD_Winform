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
    public partial class FormQLKhachHang : System.Windows.Forms.Form
    {
        public FormQLKhachHang()
        {
            InitializeComponent();
        }

        public void loadDB()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvKhachHang.DataSource = from kh in db.khaches
                                     select new
                                     {
                                         maKhachHang = kh.makhach,
                                         tenKhachHang = kh.tenkhach,
                                         gioiTinh = kh.gioiTinh,
                                         diaChi = kh.diachi,
                                         dienThoai = kh.dienthoai,
                                         ngaySinh = kh.ngaySinh
                                     };
            }
            txtMaKH.ReadOnly = true;
        }

        private void FormQLKhachHang_Load(object sender, EventArgs e)
        {
            loadDB();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvKhachHang.DataSource = db.khaches.Where(p => p.makhach.Equals(txtTimKiem.Text));
            }
            txtTimKiem.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtDC.Clear();
            txtDT.Clear();
            txtMaKH.Clear();
            txtTenKH.Clear();
            cboGioiTinh.Text = null;
            txtMaKH.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    string makh = dgvKhachHang.SelectedCells[0].OwningRow.Cells["maKhachHang"].Value.ToString();
                    khach delete = db.khaches.Where(p => p.makhach.Equals(makh)).Single();
                    db.khaches.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa thành công", "Thông báo!", MessageBoxButtons.OK);
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
                    khach sua = db.khaches.Where(p => p.makhach.Equals(txtMaKH.Text)).Single();
                    sua.makhach = txtMaKH.Text;
                    sua.tenkhach = txtTenKH.Text;
                    sua.gioiTinh = cboGioiTinh.Text;
                    sua.diachi = txtDC.Text;
                    sua.dienthoai = txtDT.Text;
                    sua.ngaySinh = DateTime.Parse(dtpNgaySinh.Text);

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
                    khach them = new khach();
                    them.makhach = txtMaKH.Text;
                    them.tenkhach = txtTenKH.Text;
                    them.gioiTinh = cboGioiTinh.Text;
                    them.diachi = txtDC.Text;
                    them.dienthoai = txtDT.Text;
                    them.ngaySinh = DateTime.Parse(dtpNgaySinh.Text);

                    db.khaches.InsertOnSubmit(them);
                    db.SubmitChanges();
                    MessageBox.Show("Lưu thành công", "Thông báo!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!!!! Hãy tạo mới", "Thông báo!", MessageBoxButtons.OK);
            }

            loadDB();
        }

        private void cboGioiTinh_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            new FormNhanVien().Show();
            this.Hide();
        }

        private void FormQLKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtMaKH.Text = dgvKhachHang.Rows[numrow].Cells[0].Value.ToString();
                txtTenKH.Text = dgvKhachHang.Rows[numrow].Cells[1].Value.ToString();
                cboGioiTinh.Text = dgvKhachHang.Rows[numrow].Cells[2].Value.ToString();
                txtDC.Text = dgvKhachHang.Rows[numrow].Cells[3].Value.ToString();
                txtDT.Text = dgvKhachHang.Rows[numrow].Cells[4].Value.ToString();
                dtpNgaySinh.Text = dgvKhachHang.Rows[numrow].Cells[5].Value.ToString();
            }
            txtMaKH.ReadOnly = true;
        }
    }
}
