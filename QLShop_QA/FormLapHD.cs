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
    public partial class FormLapHD : System.Windows.Forms.Form
    {
        public FormLapHD()
        {
            InitializeComponent();
        }
        private void clear()
        {
            txtTenKH.Clear();
            txtDC.Clear();
            txtDT.Clear();
            cboMaKH.Text = " ";
            txtMaHD.Clear();
            txtMaHD.ReadOnly = false;
            cboMaNV.Text = " ";
            txtTenNV.Clear();
            cboMaHang.Text = " ";
            txtTenHang.Clear();
            txtDonGia.Text = "";
            txtGiamGia.Text = "";
            txtSoluong.Text = "";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";
            dgvChiTietHoaDon.Rows.Clear();
            dgvHoaDon.Rows.Clear();
        }
        private void FromQLHoaDon_Load(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var dataNV = db.nhanViens.Where(x=>x.idQuyen==2).Select(k => k.maNhanVien);
                cboMaNV.DataSource = dataNV;
                var dataKH = db.khaches.Select(k => k.makhach);
                cboMaKH.DataSource = dataKH;

                var dataHH = db.hangs.Select(k => k.maHang);
                cboMaHang.DataSource = dataHH;
            }
            clear();
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    khach makh = db.khaches.FirstOrDefault(p => p.makhach.Equals(cboMaKH.SelectedValue));
                    txtTenKH.Text = makh.tenkhach;
                    txtDC.Text = makh.diachi;
                    txtDT.Text = makh.dienthoai;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void cboMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    hang mahh = db.hangs.FirstOrDefault(p => p.maHang.Equals(cboMaHang.SelectedValue));
                    txtTenHang.Text = mahh.tenHang;
                    txtDonGia.Text = mahh.donGiaBan.ToString();
                  
                }
                catch (Exception)
                {
                    throw;
                }
                txtThanhTien.Clear();
                txtSoluong.Clear();
                txtGiamGia.Clear();
                cboMaHang.Enabled = true;
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    nhanVien manv = db.nhanViens.FirstOrDefault(p => p.maNhanVien.Equals(cboMaNV.SelectedValue));
                    txtTenNV.Text = manv.tenNhanVien;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvChiTietHoaDon.DataSource = db.HDBans.Where(p => p.maHDBan.Equals(txtTimKiem.Text));
            }
            txtTimKiem.Clear();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    string mahd = dgvChiTietHoaDon.SelectedCells[0].OwningRow.Cells["maHang"].Value.ToString();
                    chiTietHDBan delete = db.chiTietHDBans.Where(p => p.maHang.Equals(mahd)).First();
                    
                    //update lai so luong kho hang khi xoa
                    hang suaSL = db.hangs.Where(p => p.maHang.Equals(cboMaHang.Text)).SingleOrDefault();
                    suaSL.soLuong = suaSL.soLuong + double.Parse(txtSoluong.Text);

                    db.chiTietHDBans.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                    MessageBox.Show("Xoá sản phẩm thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                LoadDGV_CTHD();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLShop_QADataContext db = new QLShop_QADataContext())
                {
                    chiTietHDBan sua = db.chiTietHDBans.Where(p => p.maHang.Equals(cboMaHang.Text)).Single();
                    sua.maHDBan = txtMaHD.Text;
                    sua.giamGia = double.Parse(txtGiamGia.Text);
                    sua.soLuong = double.Parse(txtSoluong.Text);
                    sua.donGia = double.Parse(txtDonGia.Text);
                    sua.thanhTien = double.Parse(txtThanhTien.Text);

                    //update lai so luong kho hang khi sua
                    double soluong = double.Parse(dgvChiTietHoaDon.SelectedCells[2].OwningRow.Cells["soLuong"].Value.ToString());
                    hang suaSL = db.hangs.Where(p => p.maHang.Equals(cboMaHang.Text)).SingleOrDefault();
                    if ((soluong - double.Parse(txtSoluong.Text)) > 0)
                    {
                        suaSL.soLuong = suaSL.soLuong + (soluong - double.Parse(txtSoluong.Text));
                    }
                    else
                    {
                        suaSL.soLuong = suaSL.soLuong + (soluong - double.Parse(txtSoluong.Text));
                    }

                    db.SubmitChanges();
                    LoadDGV_CTHD();
                    MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sửa không thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
            }

        }
        public void LoadDGV_CTHD()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvChiTietHoaDon.DataSource = from a in db.chiTietHDBans
                                              from b in db.hangs
                                              where a.maHang == b.maHang && a.maHDBan == txtMaHD.Text
                                              select new
                                              {
                                                  maHang = a.maHang,
                                                  tenHang = b.tenHang,
                                                  soLuong = a.soLuong,
                                                  donGia = b.donGiaBan,
                                                  giamGia = a.giamGia,
                                                  thanhTien = a.thanhTien,
                                              };

            }
        }
        public void loadDGV_HDB()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvHoaDon.DataSource = from a in db.HDBans
                                       where a.maHDBan == txtMaHD.Text
                                       select new
                                       {
                                           maHDBan = a.maHDBan,
                                           maNhanVien = a.maNhanVien,
                                           ngayBan = a.ngayBan,
                                           maKhach = a.maKhach,
                                           tongTien = a.tongTien,
                                       };
            }
        }
        public void tinhTongTien()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var sum = db.chiTietHDBans.Where(c => c.maHDBan.Equals(txtMaHD.Text)).Select(c => c.thanhTien).Sum();
                txtTongTien.Text = sum.ToString();
                HDBan sua = db.HDBans.Where(p => p.maHDBan.Equals(txtMaHD.Text)).SingleOrDefault();
                sua.tongTien = double.Parse(txtTongTien.Text);
                db.SubmitChanges();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    if (txtSoluong.Text != "" && txtGiamGia.Text != "")
                    //Thêm CTHD khi có hóa đơn
                    {
                        chiTietHDBan themCTHD = new chiTietHDBan();
                        themCTHD.maHang = cboMaHang.Text;
                        themCTHD.maHDBan = txtMaHD.Text;
                        themCTHD.soLuong = double.Parse(txtSoluong.Text);
                        themCTHD.donGia = double.Parse(txtDonGia.Text);
                        themCTHD.giamGia = double.Parse(txtGiamGia.Text);
                        themCTHD.thanhTien = double.Parse(txtThanhTien.Text);
                        
                        //Cap nhat lai so luong trong kho hang
                        hang suaSL = db.hangs.Where(p => p.maHang.Equals(cboMaHang.Text)).SingleOrDefault();
                        suaSL.soLuong = suaSL.soLuong - double.Parse(txtSoluong.Text);
                        db.chiTietHDBans.InsertOnSubmit(themCTHD);
                        db.SubmitChanges();
                        //tinh tong tien
                        //var sum = db.chiTietHDBans.Where(c => c.maHDBan.Equals(txtMaHD.Text)).Select(c => c.thanhTien).Sum();
                        //txtTongTien.Text = sum.ToString();
                        tinhTongTien();
                        loadDGV_HDB();
                        LoadDGV_CTHD();
                        MessageBox.Show("Đã thêm sản phẩm", "Thông báo!", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập số lượng hoặc giảm giá!!", "Thông báo!", MessageBoxButtons.OK);
                    }

                }
                    catch (Exception)
                {
                    MessageBox.Show("Sản phẩm đã tồn tại!!", "Thông báo!", MessageBoxButtons.OK);
                }
        }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    if (txtGiamGia.Text != "")
                    {
                        txtThanhTien.Text = ((double.Parse(txtSoluong.Text) * double.Parse(txtDonGia.Text)) - double.Parse(txtSoluong.Text)*(double.Parse(txtGiamGia.Text)* double.Parse(txtDonGia.Text))).ToString();
                        db.SubmitChanges();
                    }
                    else
                    {
                        txtGiamGia.Text = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
            }
        }

        private void FromLapHD_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var hoaDonBan = db.chiTietHDBans.Where(c => c.maHDBan == txtMaHD.Text).Select
                                (ct => new
                                {
                                    tenHang = ct.hang.tenHang,
                                    soLuong = Convert.ToDouble(ct.soLuong),
                                    donGia = Convert.ToDouble(ct.hang.donGiaBan),
                                    giamGia = Convert.ToDouble(ct.giamGia),
                                    thanhTien = Convert.ToDouble(ct.thanhTien),
                                });

                CRHoaDonSanPham r = new CRHoaDonSanPham();
                r.SetDataSource(hoaDonBan);
                InHoaDonBan f = new InHoaDonBan();
                f.cRVHoaDonBan.ReportSource = r;
                f.ShowDialog();
            }
        }

        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    if (cboMaNV.Text != "" && cboMaNV.Text != "" && txtMaHD.Text !=  "")
                    {
                        HDBan themHDB = new HDBan();
                        themHDB.maHDBan = txtMaHD.Text;
                        themHDB.maNhanVien = cboMaNV.Text;
                        themHDB.ngayBan = DateTime.Parse(dtpNgayBan.Text);
                        themHDB.maKhach = cboMaKH.Text;
                        themHDB.tongTien = double.Parse(txtTongTien.Text);
                        db.HDBans.InsertOnSubmit(themHDB);
                        db.SubmitChanges();
                        MessageBox.Show("Thêm hóa đơn thành công", "Thông báo!", MessageBoxButtons.OK);
                        loadDGV_HDB();
                        txtMaHD.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Hãy điền đầy đủ thông tin!!!", "Thông báo!", MessageBoxButtons.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Thiếu thông tin hoặc mã hóa đơn đã tồn tại!!!", "Thông báo!", MessageBoxButtons.OK);
                }
            }
        }
    
        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    HDBan sua = db.HDBans.Where(p => p.maHDBan.Equals(txtMaHD.Text)).SingleOrDefault();
                    sua.maNhanVien = cboMaNV.Text;
                    sua.maKhach = cboMaKH.Text;
                    sua.ngayBan = DateTime.Parse(dtpNgayBan.Text);
                    db.SubmitChanges();
                    MessageBox.Show("Sửa hóa đơn thành công", "Thông báo!", MessageBoxButtons.OK);
                    loadDGV_HDB();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sửa hóa đơn thất bại!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
              
            }
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    if (txtSoluong.Text != "" && txtGiamGia.Text == "")
                    {
                        txtThanhTien.Text = (double.Parse(txtSoluong.Text) * double.Parse(txtDonGia.Text)).ToString(); 
                        db.SubmitChanges();
                    }
                    else
                    if(txtSoluong.Text !="" && txtGiamGia.Text != "")
                    {
                        txtThanhTien.Text = ((double.Parse(txtSoluong.Text) * double.Parse(txtDonGia.Text)) - double.Parse(txtSoluong.Text) * (double.Parse(txtGiamGia.Text) * double.Parse(txtDonGia.Text))).ToString();
                        db.SubmitChanges();
                    }
                    if(txtSoluong.Text == "" && txtGiamGia.Text !="")
                    {
                        txtSoluong.Text = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Chỉ được nhập số!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            new FormNhanVien().Show();
            this.Hide();
        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cboMaHang.Text = dgvChiTietHoaDon.Rows[numrow].Cells[0].Value.ToString();
                txtTenHang.Text = dgvChiTietHoaDon.Rows[numrow].Cells[1].Value.ToString();
                txtSoluong.Text = dgvChiTietHoaDon.Rows[numrow].Cells[2].Value.ToString();
                txtDonGia.Text = dgvChiTietHoaDon.Rows[numrow].Cells[3].Value.ToString();
                txtGiamGia.Text = dgvChiTietHoaDon.Rows[numrow].Cells[4].Value.ToString();
                txtThanhTien.Text = dgvChiTietHoaDon.Rows[numrow].Cells[5].Value.ToString();
            }
        }

        private void cboMaKH_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboMaNV_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboMaHang_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

    }
}
