use master
go
--drop database qlpk
create database qlpk
go

use qlpk

--drop table NGUOIDUNG
create table NGUOIDUNG (
IDTK int identity(1,1) not null primary key clustered (IDTK asc),
TenUser char(20) not null,
MatKhauUser char(20) not null,
HoTen nvarchar (50) not null,
Email varchar(30) not null,
NgaySinhKH date null,
GioiTinhKH nvarchar(5),
DiaChiKH nvarchar (50) null,
SDTKH char(10) null,
)
-- drop table DONHANG
Create table DONHANG
(
	DonHangID int identity(1,1) not null primary key clustered (DonHangID asc),
	TenKH nvarchar(50),
	SDT int,
	DiaChiNhan nvarchar(250),
)
Create table CHITIETDONHANG
(
	CTDHID int identity(1,1) not null primary key clustered (CTDHID asc),
	DonHangID int,
	GoiKhamID int,
	SoLuongSP int,
	TongTien int,
)

--drop table DATLICH
create table DATLICH (
DatLichID int identity(1,1) primary key clustered(DatLichID asc),
TenKH nvarchar (30) not null,
NgaySinh date not null,
GioiTinh nvarchar(5),
DiaChi nvarchar (50) not null,
SDT char(10) not null,
ChuyenKhoaID int not null,
BacSiID int not null,
NgayKham date null,
GioKham time null,
XetNghiemID int not null,
)
--drop table XETNGHIEM
create table XETNGHIEM (
 XetNghiemID int identity(1,1) not null primary key clustered (XetNghiemID asc),
 TenXetNghiem nvarchar (20) not null,
 )

create table CHUYENKHOA (
ChuyenKhoaID int identity(1,1) not null primary key clustered (ChuyenKhoaID asc),
TenCK nvarchar (30) not null,
)

--drop table BACSI
create table BACSI (
BacSiID int identity(1,1) not null primary key clustered (BacSiID asc),
TenBS nvarchar (30) not null,
ChuyenKhoaID int not null,
NgaySinhBS date not null,
GioiTinhBS nvarchar(5),
SDTBS char(10) not null,
)

--drop table LOAIGOIKHAM
create table LOAIGOIKHAM(
LGKID int identity(1,1) NOT NULL PRIMARY KEY clustered(LGKID asc),
TenLoaiGoiKham nvarchar(50) not null,
)

--drop table GOIKHAM
create table GOIKHAM (
GoiKhamID int identity(1,1) not null primary key clustered(GoiKhamID asc),
TenGoiKham nvarchar(30) not null,
ChiTietGK text,
ChiPhi int,
LGKID int not null,
Anh nvarchar(50),
)

--drop table KETQUA
create table KETQUA (
KetQuaID int identity(1,1) not null primary key clustered(KetquaID asc),
IDTK int not null,
MaDon char (5) null,
HinhAnh varchar(50),
ChiTiet text null,
KetLuan text null,
)
--drop table KEDONTHUOC
create table KEDONTHUOC (
KeDonThuocID int identity(1,1) not null primary key clustered(KeDonThuocID asc),
TenKH nvarchar (30) not null,
TenBenh nvarchar (30) not null,
THUOCID int not null,
SoLuong int not null,
)

create table THUOC (
THUOCID int identity(1,1) not null primary key clustered(ThuocID asc),
TENTHUOC nvarchar (30) not null,
)

ALTER TABLE [dbo].[BACSI]  WITH CHECK ADD  CONSTRAINT [FK_MaCKBS] FOREIGN KEY([ChuyenKhoaID]) REFERENCES [dbo].[CHUYENKHOA] ([ChuyenKhoaID])

ALTER TABLE [dbo].[DATLICH]  WITH CHECK ADD  CONSTRAINT [FK_MaBSDL] FOREIGN KEY(BacSiID) REFERENCES [dbo].[BACSI] ([BacSiID])
ALTER TABLE [DBO].[DATLICH] WITH CHECK ADD CONSTRAINT [FK_XetNghiemID] FOREIGN KEY([XetNghiemID]) REFERENCES [dbo].[XETNGHIEM] ([XetNghiemID])

ALTER TABLE [DBO].[GOIKHAM]  WITH CHECK ADD  CONSTRAINT [FK_MaLGKGK] FOREIGN KEY ([LGKID]) REFERENCES [dbo].[LOAIGOIKHAM] ([LGKID])

ALTER TABLE [DBO].[KETQUA]  WITH CHECK ADD  CONSTRAINT [FK_IDTKKQ] FOREIGN KEY ([IDTK]) REFERENCES [dbo].[NGUOIDUNG] ([IDTK])

ALTER TABLE [dbo].[KEDONTHUOC]  WITH CHECK ADD  CONSTRAINT [FK_THUOCID] FOREIGN KEY(THUOCID) REFERENCES [dbo].[THUOC] ([THUOCID])

ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [FK_DonHangID] FOREIGN KEY(DonHangID) REFERENCES [dbo].[DONHANG] ([DonHangID])
ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [FK_GoiKhamID] FOREIGN KEY(GoiKhamID) REFERENCES [dbo].[GOIKHAM] ([GoiKhamID])



Set identity_insert LOAIGOIKHAM On 
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (1, N'Dịch vụ Y tế Covid - 19');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (2, N'Khám Tư Vấn Từ xa');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (3, N'Khám Sức Khỏe Dành Cho Nam');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (4, N'Khám Sức Khỏe Dành Cho Nữ');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (5, N'Khám Sức Khỏe Tổng Quát Dánh Cho Trẻ Em');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (6, N'Tầm Soát Ung Thư');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (7, N'Tẩy Trắng Răng');
insert into LOAIGOIKHAM (LGKID, TenLoaiGoiKham) values (8, N'Xét Nghiệm Gen');
Set identity_insert LOAIGOIKHAM OFF

Set identity_insert CHUYENKHOA On 
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (1, N'Khoa Nội Tổng Quát');
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (2, N'Khoa Nhi');
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (3, N'Khoa Răng-Hàm-Mặt');
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (4, N'Khoa Tim Mạch');
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (5, N'Khoa Hô Hấp');
insert into CHUYENKHOA (ChuyenKhoaID, TenCK) values (6, N'Khoa Gan - Tiêu hóa');
Set identity_insert CHUYENKHOA OFF

set dateformat dmy
Set identity_insert BACSI On 
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (1, N'Bs.Nguyễn Hà Xuân Tài', 1, '16/1/1995', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (2, N'Bs.Phan Trí Nghĩa', 1, '30/5/1995', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (3, N'Bs.Đoàn Thanh Thiên Nga', 1, '27/12/1998', N'Nữ', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (4, N'Bs.Phạm Tấn Hưng', 2, '1/11/1996', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (5, N'Bs.Phan Lại Hào', 3, '17/11/1996', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (6, N'Bs.Lê Quốc Bằng', 4, '23/6/1996', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (7, N'Bs.Lê Phong', 5, '24/12/1996', N'Nam', '0926247716');
insert into BACSI (BacSiID, TenBS, ChuyenKhoaID, NgaySinhBS, GioiTinhBS, SDTBS) values (8, N'Bs.Hồ Hoàng Nam', 6, '9/10/1996', N'Nam', '0926247716');
Set identity_insert BACSI OFF

Set identity_insert THUOC On 
insert into THUOC (THUOCID, TenTHUOC) values (1, N'Panadon');
insert into THUOC (THUOCID, TenTHUOC) values (2, N'Loperamide');
insert into THUOC (THUOCID, TenTHUOC) values (3, N'Paracetamol');
insert into THUOC (THUOCID, TenTHUOC) values (4, N'Tiffy');
Set identity_insert THUOC OFF

Set identity_insert XETNGHIEM On
Insert into XETNGHIEM (XetNghiemID, TenXetNghiem) values (1,N'Không xét nghiệm ');
Insert into XETNGHIEM (XetNghiemID, TenXetNghiem) values (2,N'Xét nghiệm máu ');
Set identity_insert XETNGHIEM OFF

