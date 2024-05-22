
create database dbSTUDENTMANAGEMENT;
go
use dbSTUDENTMANAGEMENT;
go

create table FACULTY
(
	FacultyID varchar(20) primary key,
	Name nvarchar(50)
);

create table COURSE
(
	CourseID varchar(20) primary key,
	Name nvarchar(50),
	NoCredits int,
)

create table LECTURER
(
	LecturerID varchar(20) primary key,
	Username varchar(50),
	Password varchar(50),
	Fullname nvarchar(50),
	Email varchar(50),
	Phone_number char(10),
	DoB date,
	Gender bit,
	Address nvarchar(50),
	NoIdentity varchar(11),
	AcademicDegree nvarchar(50),
	Role int not null,
	isExist bit not null,
	RandomKey varchar(50),
	FacultyID varchar(20),
)

create table CLASS
(
	ClassID varchar(20) primary key,
	Name nvarchar(50),
	LecturerID varchar(20)
)

create table STUDENT 
(
	StudentID varchar(20) primary key,
	Username varchar(50),
	Password varchar(50),
	Fullname nvarchar(50),
	Email varchar(50),
	Phone_number char(10),
	DoB date,
	Gender bit,
	Address nvarchar(50),
	NoIdentity varchar(11),
	Role int not null,
	isExist bit not null,
	RandomKey varchar(50),
	ClassID varchar(20)
)

create table COURSEDETAILS
(
	CourseDetailID int IDENTITY(1,1) primary key,
	LecturerID varchar(20),
	CourseID varchar(20)
)

create table COURSE_STUDENT
(
	CourseDetailID int,
	StudentID varchar(20),
	Grade int,
	primary key (CourseDetailID,StudentID)
);


create table ACCOUNT_ADMIN
(
	AccountID int IDENTITY(1,1) primary key,
	Username varchar(50),
	Password varchar(50),
)

create table KINDOFIMAGE
(
	ImageID int IDENTITY(1,1) primary key,
	Name nvarchar(50)
)

create table STUDENT_IMAGE
(
	StudentID varchar(20) not null,
	ImageID int not null,
	Source varchar(50)
	primary key (StudentID, ImageID)
)
ALTER TABLE LECTURER
ADD CONSTRAINT FK_tbLECTURER_tbFACULTY
FOREIGN KEY (FacultyID) REFERENCES FACULTY(FacultyID);

ALTER TABLE CLASS
ADD CONSTRAINT FK_tbLECTURER_tbCLASS
FOREIGN KEY (LecturerID) REFERENCES LECTURER(LecturerID);

ALTER TABLE STUDENT
ADD CONSTRAINT FK_tbSTUDENT_tbCLASS
FOREIGN KEY (ClassID) REFERENCES CLASS(ClassID);

ALTER TABLE COURSEDETAILS
ADD CONSTRAINT FK_tbCOURSE_tbCOURSEDETAILS
FOREIGN KEY (CourseID) REFERENCES COURSE(CourseID);

ALTER TABLE COURSEDETAILS
ADD CONSTRAINT FK_tbLECTURER_tbCOURSEDETAILS
FOREIGN KEY (LecturerID) REFERENCES LECTURER(LecturerID);

ALTER TABLE COURSE_STUDENT
ADD CONSTRAINT FK_tbCOURSE_STUDENT_tbCOURSEDETAILS
FOREIGN KEY (CourseDetailID) REFERENCES COURSEDETAILS(CourseDetailID);

ALTER TABLE COURSE_STUDENT
ADD CONSTRAINT FK_tbCOURSE_STUDENT_tbSTUDENT
FOREIGN KEY (StudentID) REFERENCES STUDENT(StudentID);

ALTER TABLE STUDENT_IMAGE
ADD CONSTRAINT FK_tbSTUDENT_IMAGE_tbSTUDENT
FOREIGN KEY (StudentID) REFERENCES STUDENT(StudentID);

ALTER TABLE STUDENT_IMAGE
ADD CONSTRAINT FK_tbSTUDENT_IMAGE_tbKINDOFIMAGE
FOREIGN KEY (ImageID) REFERENCES KINDOFIMAGE(ImageID);

-- Thêm dữ liệu vào bảng FACULTY
INSERT INTO FACULTY (FacultyID, Name)
VALUES 
('KH00001', N'Khoa Kỹ thuật'),
('KH00002', N'Khoa Kinh doanh'),
('KH00003', N'Khoa Nghệ thuật'),
('KH00004', N'Khoa Y Dược'),
('KH00005', N'Khoa Điện tử');

-- Thêm dữ liệu vào bảng COURSE
INSERT INTO COURSE (CourseID, Name, NoCredits)
VALUES 
('MH00001', N'Toán cao cấp', 3),
('MH00002', N'Lập trình C', 4),
('MH00003', N'Marketing cơ bản', 3),
('MH00004', N'Vẽ tranh cảnh đẹp', 2),
('MH00005', N'Nội soi y học', 4);


-- Thêm dữ liệu vào bảng LECTURER
INSERT INTO LECTURER (LecturerID, Username, Password, Fullname, Email, Phone_number, DoB,  Gender, Address, NoIdentity, AcademicDegree, Role, isExist,RandomKey, FacultyID)
VALUES 
('GV00001', 'gv_nguyenvana', '123456', N'Nguyễn Văn A', 'nguyenvana@gmail.com', '0123456789', '1980-01-01',  1, N'Hà Nội', '01234567891', N'Tiến sĩ',1,1,'abc', 'KH00001'),
('GV00002', 'gv_tranthib', 'abcde', N'Trần Thị B', 'tranthib@yahoo.com', '0987654321', '1975-05-10',  0, N'Hồ Chí Minh', '01234567892', N'Thạc sĩ',1,0,'123', 'KH00002'),
('GV00003', 'gv_phamvanc', 'password', N'Phạm Văn C', 'phamvc@outlook.com', '0369852147', '1988-12-20',  1, N'Đà Nẵng', '01234567893', N'Tiến sĩ',1,1,'0ad', 'KH00003');

-- Thêm dữ liệu vào bảng CLASS
INSERT INTO CLASS (ClassID, Name, LecturerID)
VALUES 
('CL00001', N'Lớp Toán cao cấp A', 'GV00001'),
('CL00002', N'Lớp Marketing cơ bản B', 'GV00002'),
('CL00003', N'Lớp Vẽ tranh cảnh đẹp C', 'GV00003');

-- Thêm dữ liệu vào bảng STUDENT
INSERT INTO STUDENT (StudentID, Username, Password, Fullname, Email, Phone_number, DoB, Gender, Address, NoIdentity, Role, isExist, RandomKey, ClassID)
VALUES 
('SV00001', 'sv_nguyenthilan', '123456', N'Nguyễn Thị Lan', 'nguyenlan@gmail.com', '0123456789', '2000-03-15', 0, N'Hà Nội', '01234567894',0,1,'ab1c', 'CL00001'),
('SV00002', 'sv_tranvanbinh', 'abcde', N'Trần Văn Bình', 'tranbinh@yahoo.com', '0987654321', '1999-06-20', 1, N'Hồ Chí Minh', '01234567895',0,0,'1abc', 'CL00002'),
('SV00003', 'sv_phamthihuong', 'password', N'Phạm Thị Hương', 'phamhuong@outlook.com', '0369852147', '2001-12-10', 0, N'Đà Nẵng', '01234567896',0,1,'a0bc', 'CL00003');

-- Thêm dữ liệu vào bảng COURSEDETAILS
INSERT INTO COURSEDETAILS (LecturerID, CourseID)
VALUES 
('GV00001', 'MH00001'),
('GV00002', 'MH00003'),
('GV00003', 'MH00004');




--Thêm dữ liệu vào bảng KINDOFIMAGE
INSERT INTO KINDOFIMAGE (Name)
VALUES
(N'Ảnh đại diện'),
(N'Ảnh thẻ sinh viên'),
(N'Ảnh CMND');

--Thêm dữ liệu vào bảng STUDENT_IMAGE
INSERT INTO STUDENT_IMAGE (StudentID,ImageID,Source)
VALUES
('SV00001',1,'abc.jpg'),
('SV00001',2,'adfk.jpg'),
('SV00001',3,'abcsdfjsd.jpg');




-- Thêm dữ liệu vào bảng COURSE_STUDENT
-- Vì không có dữ liệu cụ thể cho bảng này, ta để trống

-- Thêm dữ liệu vào bảng ACCOUNT_ADMIN
INSERT INTO ACCOUNT_ADMIN (Username, Password)
VALUES 
('admin', 'admin123');
INSERT INTO COURSE_STUDENT (CourseDetailID,StudentID,Grade)
VALUES 
(1,'SV004',8),
(2,'SV004',9),
(3,'SV004',5),
(1,'SV005',4),
(2,'SV005',5),
(3,'SV005',7),
(1,'SV006',5),
(2,'SV006',9),
(3,'SV006',6);

--Thêm dữ liệu vào bảng STUDENT_IMAGE
INSERT INTO STUDENT_IMAGE (StudentID,ImageID,Source)
VALUES
('SV004',1,'anhthe4.jpg'),
('SV004',2,'thesv4.jpg'),
('SV004',3,'cmnd4.jpg'),
('SV005',1,'anhthe5.jpg'),
('SV005',2,'thesv5.jpg'),
('SV005',3,'cmnd5.jpg'),
('SV006',1,'anhthe6.jpg'),
('SV006',2,'thesv6.jpg'),
('SV006',3,'cmnd6.jpg');


