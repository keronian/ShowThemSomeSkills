CREATE TABLE "Invoice"
(
	"InvoiceNo" varchar(100) PRIMARY KEY,
	"CompanyName" varchar(255),

--	 These billing entries could easily be split into a separate table, but aren't here, which is fine, as there's no re-use
	"BillingContact" varchar(255),
	"BillingStreet" varchar(255),
	"BillingCity" varchar(255),
	"BillingState" varchar(2),
	"BillingZip" varchar(10),

	"PostedDate" datetime,
	"ShippingDate" datetime,
	"RequisitionDate" datetime,
	"TaxRate" decimal(2,2),
	"SubTotal" decimal(18,2),
	"Shipping" decimal(18,2),
	"Commission" decimal(18,2),
	"Total" decimal(18,2)
);

CREATE TABLE "InvoiceItem"
(
	"InvoiceNo" varchar(100) FOREIGN KEY REFERENCES Invoice(InvoiceNo) ON DELETE CASCADE,
	"LineText" varchar(255),
	"Taxable" bit,
	"Quantity" int,
	"UnitPrice" decimal(18,2),
	"Discount" tinyint,
	"SubTotal" decimal(18,2),
	"Total" decimal(18,2)
);