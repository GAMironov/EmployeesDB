insert into Employees (Family, Name, MiddleName, Birthday) VALUES 
('������', '����', '��������', '01.01.1980'),
('������', '����', '��������', '01.01.1980'),
('�������', '�����', '���������', '01.01.1980'),
('�������', '�����', '���������', '01.01.1980'),
('�����', '����', '�����', '01.01.1980'),
('��������', '�������', '����������', '01.01.1980');

insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '������' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '������' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '�������' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '�������' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '�����' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like '��������' );

update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '������' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '������' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '�������' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '�������' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '�����' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like '��������' ORDER BY emp.id DESC );

