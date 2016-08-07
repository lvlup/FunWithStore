General requirements:

1. It should be made as simplified ASP.NET Forms(or ASP.NET MVC)-based application with using of C#, ADO.NET (Entity Framework or something else) and Microsoft SQL Server.

2. The application should allow customers to place and manage theirs orders.

3. It should be possible to create a new customer and create a new order for it. Update/delete operations should be implemented for both customers and Orders.

 

Entity details:

1. Customer has:

Name (string),

Address (string).

2. Order has:

Number (int),

Date (DateTime),

Amount (int),

Description (string).

 

UI notes:

1. The application must contain at least one form with 2 related grids (Master-detail).

2. First grid should display list of customers. Customer should have a Name and address.

3. Second grid should display orders which are related to a customer selected in the first grid. Order should have Number, Date, Amount and Description.

 

Nice to have features:

1. It will be nice to have the validation implemented.

2. It will be nice to design the solution as 2-tier one by implementing DAL (Data Access Layer).
