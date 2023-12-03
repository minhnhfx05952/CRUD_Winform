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
    public partial class FormQLHang : System.Windows.Forms.Form
    {
        public FormQLHang()
        {
            InitializeComponent();
        }
        public void loadDB()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvSanPham.DataSource = from sp in db.hangs
                                        from cl in db.chatLieus
                                        where sp.maChatLieu == cl.maChatLieu
                                        select new
                                        {
                                            MaSP = sp.maHang,
                                            tenSP = sp.tenHang,
                                            maCL = cl.maChatLieu,
                                            soLuong = sp.soLuong,
                                            donGiaNhap = sp.donGiaNhap,
                                            donGiaBan = sp.donGiaBan,
                                            anh = sp.anh,
                                            GhiChu = sp.ghiChu
                                        };
            }
        }

        private void FormQLHang_Load(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var data = db.chatLieus.Select(k => k);
                cboMaChatLieu.DataSource = data;
                cboMaChatLieu.DisplayMember = "maChatLieu";
                cboMaChatLieu.ValueMember = "maChatLieu";
            }
            txtMaSP.ReadOnly = true;
            loadDB();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvSanPham.DataSource = db.hangs.Where(p => p.maHang.Equals(txtTimKiem.Text));
            }
            txtTimKiem.Clear();
        }

        
         private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtMaSP.Text = dgvSanPham.Rows[numrow].Cells[0].Value.ToString();
                txtTenSP.Text = dgvSanPham.Rows[numrow].Cells[1].Value.ToString();
                cboMaChatLieu.Text = dgvSanPham.Rows[numrow].Cells[2].Value.ToString();
                txtSoLuong.Text = dgvSanPham.Rows[numrow].Cells[3].Value.ToString();
                txtDonGiaNhap.Text = dgvSanPham.Rows[numrow].Cells[4].Value.ToString();
                txtDonGiaBan.Text = dgvSanPham.Rows[numrow].Cells[5].Value.ToString();
                txtAnh.Text = dgvSanPham.Rows[numrow].Cells[6].Value.ToString();
                txtGhiChu.Text = dgvSanPham.Rows[numrow].Cells[7].Value.ToString();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    string masp = dgvSanPham.SelectedCells[0].OwningRow.Cells["MaSP"].Value.ToString();
                    hang delete = db.hangs.Where(p => p.maHang.Equals(masp)).SingleOrDefault();
                    db.hangs.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                }
                MessageBox.Show("Xóa thành công", "Thông báo!", MessageBoxButtons.OK);
                loadDB();
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo!", MessageBoxButtons.OK);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    hang sua = db.hangs.Where(p => p.maHang.Equals(txtMaSP.Text)).SingleOrDefault();
                    sua.maHang = txtMaSP.Text;
                    sua.tenHang = txtTenSP.Text;
                    sua.maChatLieu = cboMaChatLieu.Text;
                    sua.soLuong = double.Parse(txtSoLuong.Text);
                    sua.donGiaNhap = double.Parse(txtDonGiaNhap.Text);
                    sua.donGiaBan = double.Parse(txtDonGiaBan.Text);
                    sua.anh = txtAnh.Text;
                    sua.ghiChu = txtGhiChu.Text;

                    db.SubmitChanges();
                }
                MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK);
                loadDB();
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo!", MessageBoxButtons.OK);
            }
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    hang them = new hang();
                    them.maHang = txtMaSP.Text;
                    them.tenHang = txtTenSP.Text;
                    them.maChatLieu = cboMaChatLieu.Text;
                    them.soLuong = double.Parse(txtSoLuong.Text);
                    them.donGiaNhap = double.Parse(txtDonGiaNhap.Text);
                    them.donGiaBan = double.Parse(txtDonGiaBan.Text);
                    them.anh = txtAnh.Text;
                    them.ghiChu = txtGhiChu.Text;

                    db.hangs.InsertOnSubmit(them);
                    db.SubmitChanges();
                }
                MessageBox.Show("Lưu thành công!", "Thông báo!", MessageBoxButtons.OK);
                loadDB();
            }
            catch (Exception)
            {
                MessageBox.Show("Sản phẩm đã tồn tại!", "Thông báo!", MessageBoxButtons.OK);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtAnh.Clear();
            txtDonGiaBan.Clear();
            txtDonGiaNhap.Clear();
            txtGhiChu.Clear();
            txtMaSP.Clear();
            txtSoLuong.Clear();
            txtTenSP.Clear();
            txtMaSP.ReadOnly = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormQLHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
