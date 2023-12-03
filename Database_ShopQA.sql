use master
go

create database QlShop_QA
go
use QlShop_QA
go

create table chatLieu
(
	maChatLieu nvarchar(10) not null primary key,
	tenChatLieu nvarchar(30)
)
create table quyen
(
	id int not null primary key,
	name varchar(50)
)
create table khach
(
	makhach nvarchar(10) not null primary key,
	tenkhach nvarchar(30),
	gioiTinh nvarchar(10),
	diachi nvarchar(30),
	dienthoai nvarchar(15),
	ngaySinh datetime
)

create table hang
(
	maHang nvarchar(10) not null primary key,
	tenHang nvarchar(30),
	maChatLieu nvarchar(10),
	soLuong float,
	donGiaNhap float,
	donGiaBan float,
	anh nvarchar(200),
	ghiChu nvarchar(200),
	constraint fk_maCL foreign key(maChatLieu) references chatLieu(maChatLieu)
)


create table nhanVien
(
	maNhanVien nvarchar(10) not null primary key,
	tenNhanVien nvarchar(30),
	gioiTinh nvarchar(10),
	diaChi nvarchar(30),
	dienThoai nvarchar(15),
	ngaySinh datetime,
	taikhoan varchar(100),
	matkhau varchar(100),
	idQuyen int,
	constraint fk_idQuyen foreign key(idQuyen) references quyen(id)
)


create table HDBan
(
	maHDBan nvarchar(10) not null primary key,
	maNhanVien nvarchar(10),
	ngayBan datetime,
	maKhach nvarchar(10),
	tongTien float,
	constraint fk_maKhach foreign key(maKhach) references khach(maKhach),
	constraint fk_maNV foreign key(maNhanVien) references nhanVien(maNhanVien)
)

create table chiTietHDBan
(
	maHDBan nvarchar(10) not null,
	maHang nvarchar(10) not null,
	soLuong float,
	donGia float,
	giamGia float,
	thanhTien float,
	primary key(maHDBan,maHang),
	constraint fk_maHDB foreign key(maHDBan) references HDBan(maHDBan),
	constraint fk_maH foreign key(maHang) references hang(maHang)
)
------------------------------------- NHẬP LIỆU ----------------------------------------

------ QUYỀN -------
INSERT INTO quyen VALUES
(1,N'Admin'),
(2,N'NhanVien')
------ CHẤT LIÊU -------
INSERT INTO chatLieu VALUES
('CL01',N'COTTON'),
('CL02',N'KAKI'),
('CL03',N'LỤA'),
('CL04',N'JEANS')

SET DATEFORMAT dmy
---- NHÂN VIÊN -------
INSERT INTO nhanVien VALUES
('NV01',N'Nguyễn Thị Thảo Vy',N'Nữ',N'Tiền Giang',0353676483,'03/02/2001','thaovy','123',2),
('NV02',N'Phan Văn Tuấn',N'Nam',N'Đắk Lắk',0523612459,'25/12/2001','vantuan','123',1),
('NV03',N'Huỳnh Phát',N'Nam',N'Đắk Lắk',0625132485,'24/12/2001','huynhphat','123',2)

SET DATEFORMAT dmy
------ KHÁCH HÀNG ----------
INSERT INTO khach VALUES
('KH01',N'Nguyễn Tấn Phát','Nam',N'Tân Phú','02316548265','09/02/2001'),
('KH02',N'Nguyễn Tấn Hưng',N'Nữ',N'Quận 8','0214535126','03/08/2001'),
('KH03',N'Trần Khang','Nam',N'Quận 5','0356201489','05/03/2001'),
('KH04',N'Nguyễn Thái Khương ','Nam',N'Tân Phú','0945632157','04/02/2001'),
('KH05',N'Phan Tuấn Long','Nam',N'Quận 1','0865236214','09/11/2001'),
('KH06',N'Trương Trọng Nghĩa','Nam',N'Quận 6','0845213654','04/10/2001'),
('KH07',N'Bùi Huy Hoàng','Nam',N'Tân Bình','0845215426','15/06/2001'),
('KH08',N'Trần Đoàn Bảo Ngọc',N'Nữ',N'Phú Nhuận','0215436584','01/11/2001'),
('KH09',N'Phạm Thanh Long','Nam',N'Tân Phú','0845216324','06/05/2001'),
('KH10',N'Phạm Hồng Quân','Nam',N'Bình Thạnh','02154876942','23/12/2001'),
('KH11',N'Huỳnh Ngọc Thạch',N'Nữ',N'Gò Vấp','0215496231','13/07/2001'),
('KH12',N'Nguyễn Thông Thái',N'Nữ',N'Quận 11','02316548265','01/02/2001'),
('KH13',N'Huỳnh Công Tiến','Nam',N'Quận 3','0215436251','03/04/2001')

-------- HÀNG HÓA ----------
INSERT INTO hang VALUES
('H01',N'ÁO THUN 01','CL01',80,30000,150000,'',N'Màu sắc và họa tiết đơn giản'),
('H02',N'ÁO THUN 02','CL01',80,30000,150000,'',N'Màu sắc và họa tiết đơn giản'),
('H03',N'ÁO THUN 03','CL01',80,30000,150000,'',N'Màu sắc và họa tiết đơn giản'),
('H04',N'ÁO THUN 04','CL01',80,30000,150000,'',N'Màu sắc và họa tiết đơn giản'),
('H05',N'ÁO THUN 05','CL01',80,30000,150000,'',N'Màu sắc và họa tiết đơn giản'),

('H06',N'SƠ MI 01','CL02',80,100000,200000,'',N'Màu sắc và họa tiết thanh lịch'),
('H07',N'SƠ MI 02','CL02',80,100000,200000,'',N'Màu sắc và họa tiết thanh lịch'),
('H08',N'SƠ MI 03','CL02',80,100000,200000,'',N'Màu sắc và họa tiết thanh lịch'),
('H09',N'SƠ MI 04','CL02',80,100000,200000,'',N'Màu sắc và họa tiết thanh lịch'),
('H10',N'SƠ MI 05','CL02',80,100000,200000,'',N'Màu sắc và họa tiết thanh lịch'),

('H11',N'LỤA 01','CL03',80,100000,220000,'',N'Màu sắc và họa tiết nhẹ nhàng sang trọng'),
('H12',N'LỤA 02','CL03',80,100000,220000,'',N'Màu sắc và họa tiết nhẹ nhàng sang trọng'),
('H13',N'LỤA 03','CL03',80,100000,220000,'',N'Màu sắc và họa tiết nhẹ nhàng sang trọng'),
('H14',N'LỤA 04','CL03',80,100000,220000,'',N'Màu sắc và họa tiết nhẹ nhàng sang trọng'),
('H15',N'LỤA 05','CL03',80,100000,220000,'',N'Màu sắc và họa tiết nhẹ nhàng sang trọng'),

('H16',N'JEANS 01','CL04',80,90000,175000,'',N'Màu sắc và họa tiết năng động'),
('H17',N'JEANS 02','CL04',80,90000,175000,'',N'Màu sắc và họa tiết năng động'),
('H18',N'JEANS 03','CL04',80,90000,175000,'',N'Màu sắc và họa tiết năng động'),
('H19',N'JEANS 04','CL04',80,90000,175000,'',N'Màu sắc và họa tiết năng động'),
('H20',N'JEANS 05','CL04',80,90000,175000,'',N'Màu sắc và họa tiết năng động')

--SET DATEFORMAT dmy
------ HÓA ĐƠN------
INSERT INTO HDBan VALUES
('HD01','NV01','09/06/2022','KH01',0),
('HD02','NV01','15/06/2022','KH02',0),
('HD03','NV01','09/08/2022','KH03',0),
('HD04','NV01','23/06/2022','KH04',0),
('HD05','NV01','15/06/2022','KH05',0)

------ CHI TIẾT HÓA ĐƠN -------------
INSERT INTO chiTietHDBan VALUES
('HD01','H01',1,150000,0.1,0),
('HD01','H02',1,150000,0.2,0),
('HD02','H05',3,150000,0.3,0),
('HD03','H08',1,200000,0.1,0),
('HD04','H09',1,200000,0.2,0),
('HD05','H12',1,220000,0.1,0)

	select * from nhanVien
	select * from HDBan
	select * from chiTietHDBan
	select * from hang
	select * from quyen
