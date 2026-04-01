\# E-Commerce Product Catalog (ASP.NET Core MVC + ADO.NET)



This project is a simple product catalog built with:



✔ ASP.NET Core MVC 8  

✔ ADO.NET (NO EF CORE)  

✔ SQL Server  

✔ Stored Procedures  

✔ AJAX + jQuery  

✔ Partial Views  

✔ Custom Exception Middleware  



\---



\## 📌 FEATURES



\### LIST VIEW

\- Filters by Name, Price, Category  

\- Pagination  

\- Loads via AJAX  

\- Uses Partial View  



\### VIEW POPUP

\- Large product image  

\- Thumbnail slider  

\- Product details  



\### EDIT POPUP

\- Updates Name, Price, Stock, Category, Description  

\- SKU is readonly  

\- AJAX submit + refresh  



\### DELETE POPUP

\- Confirmation  

\- AJAX delete  



\---



\## 📌 DATABASE SETUP



Run the following SQL:



\- Create tables:

&#x20; - Products  

&#x20; - Categories  

&#x20; - ProductImages  



\- Create stored procedures:

&#x20; - sp\_GetProducts  

&#x20; - sp\_GetProductById  

&#x20; - sp\_UpdateProduct  

&#x20; - sp\_DeleteProduct  

&#x20; - sp\_AddProductImage

&#x20; - sp\_DeleteProductImage

\---



\## 📌 ADD CONNECTION STRING



In `appsettings.json`:
"ConnectionStrings": {

"DefaultConnection": "Server=YOUR-PC;Database=ProductCatalog;Trusted\_Connection=True;MultipleActiveResultSets=true"

}



\---



\## 📌 RUN THE PROJECT



1\. Restore NuGet packages  

2\. Run SQL scripts  

3\. Press \*\*F5\*\*  

4\. Browse `/Product`  



