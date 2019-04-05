insert into Employees (Family, Name, MiddleName, Birthday) VALUES 
('Иванов', 'Иван', 'Иванович', '01.01.1980'),
('Петров', 'Петр', 'Петрович', '01.01.1980'),
('Сидоров', 'Сидор', 'Сидорович', '01.01.1980'),
('Антонов', 'Антон', 'Антонович', '01.01.1980'),
('Ильин', 'Илья', 'Ильич', '01.01.1980'),
('Васильев', 'Василий', 'Васильевич', '01.01.1980');

insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Иванов' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Петров' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Сидоров' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Антонов' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Ильин' )
insert into Contacts (EmployeeID) (select emp.id from Employees as emp where emp.Family like 'Васильев' );

update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Иванов' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Петров' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Сидоров' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Антонов' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Ильин' ORDER BY emp.id DESC );
update Contacts  set Phone = '12345678901', Email = 'test@test.test' where EmployeeID like
(select top 1 emp.id from Employees as emp where emp.Family like 'Васильев' ORDER BY emp.id DESC );

