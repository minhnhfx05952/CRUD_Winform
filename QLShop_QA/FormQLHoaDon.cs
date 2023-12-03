using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace QLShop_QA
{
    public partial class FormQLHoaDon : System.Windows.Forms.Form
    {
        public FormQLHoaDon()
        {
            InitializeComponent();
        }

        private void FromQLHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát hay không ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        public void loadDB()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvHoaDon.DataSource = from a in db.HDBans
                                       select new
                                       {
                                           MaHDBan = a.maHDBan,
                                           maNhanVien = a.maNhanVien,
                                           ngayBan = a.ngayBan,
                                           maKhach = a.maKhach,
                                           tongTien = a.tongTien,
                                       };
            }
        }
        private void FromQLHoaDon_Load(object sender, EventArgs e)
        {
            loadDB();
            loadDGV_CTHD();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    string mahd = dgvHoaDon.SelectedCells[0].OwningRow.Cells["maHDBan"].Value.ToString();
                    HDBan delete = db.HDBans.Where(p => p.maHDBan.Equals(mahd)).SingleOrDefault();
                    db.HDBans.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                    MessageBox.Show("Xoá thành công hóa đơn!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("Bạn phải xóa thông tin sản phẩm của hóa đơn!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                
            }
            loadDB();
        }

        public void loadDGV_CTHD()
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                string mahd = dgvHoaDon.SelectedCells[0].OwningRow.Cells["maHDBan"].Value.ToString();
                dtgCTHD.DataSource = from a in db.chiTietHDBans
                                     from b in db.hangs
                                     where a.maHang == b.maHang && a.maHDBan == mahd
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
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            loadDGV_CTHD();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnXoaCTHD_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                try
                {
                    string mahd = dtgCTHD.SelectedCells[0].OwningRow.Cells["maHang"].Value.ToString();
                    chiTietHDBan delete = db.chiTietHDBans.Where(p => p.maHang.Equals(mahd)).First();
                    db.chiTietHDBans.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                    MessageBox.Show("Xoá sản phẩm thành công!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra!!!!", "Thông báo!", MessageBoxButtons.OK);
                }
                
            }
            loadDGV_CTHD();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                dgvHoaDon.DataSource = db.HDBans.Where(p => p.maHDBan.Equals(txtTimKiem.Text));
            }
            txtTimKiem.Clear();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            using (QLShop_QADataContext db = new QLShop_QADataContext())
            {
                var hoaDonBan = db.HDBans.Select(c => new
                {
                    c.maHDBan,
                    c.maNhanVien,
                    ngayBan = Convert.ToDateTime(c.ngayBan),
                    c.maKhach,
                    tongTien = Convert.ToDouble(c.tongTien),
                }).ToList();

                // Sử dụng iTextSharp để tạo file PDF
                string pdfPath = "E://hoadon.pdf"; // Đặt đường dẫn tới nơi bạn muốn lưu file PDF
                try
                {
                    // ...
                    using (PdfWriter writer = new PdfWriter(pdfPath))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document document = new Document(pdf);

                            // Thêm dữ liệu từ hoaDonBan vào tài liệu PDF
                            foreach (var hoaDon in hoaDonBan)
                            {
                                document.Add(new Paragraph($"Mã Hóa Đơn: {hoaDon.maHDBan}"));
                                document.Add(new Paragraph($"Mã Nhân Viên: {hoaDon.maNhanVien}"));
                                document.Add(new Paragraph($"Ngày Bán: {hoaDon.ngayBan.ToShortDateString()}"));
                                document.Add(new Paragraph($"Mã Khách: {hoaDon.maKhach}"));
                                document.Add(new Paragraph($"Tổng Tiền: {hoaDon.tongTien}"));
                                document.Add(new AreaBreak());
                            }
                        }
                    }
                    // ...
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Hiển thị thông báo sau khi tạo file PDF
                MessageBox.Show($"File PDF đã được lưu tại:\n{pdfPath}", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
