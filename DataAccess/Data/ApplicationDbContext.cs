using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        //public Dbset<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<InvoiceDetails> InvoicesDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CompanySupplier> CompanySuppliers { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<SaleInvoice> saleInvoices { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasData(
                new Invoice { Id = 1, UserId = 2, Costs = 233000, SupplierId = 1, CreationDate = DateTime.Now, PaymentId = 2, InvoiceNumber = "c433g" },
                new Invoice { Id = 2, UserId = 2, Costs = 233000, SupplierId = 2, CreationDate = DateTime.Now, PaymentId = 2, InvoiceNumber = "c433g" },
                new Invoice { Id = 3, UserId = 2, Costs = 233000, SupplierId = 3, CreationDate = DateTime.Now, PaymentId = 2, InvoiceNumber = "c433g" }
                );

                entity.HasMany(a => a.InvoiceDetails).WithOne(x => x.Invoice).HasForeignKey(c => c.InvoiceId);
                //entity.HasOne(b=>b.CompanySupplier).WithOne(o=>o.Invoice).HasForeignKey(p=>p.SupplierId);

                entity.HasOne(a => a.CompanySupplier).WithMany(o => o.Invoice).HasForeignKey(c => c.SupplierId).OnDelete(DeleteBehavior.Restrict);

			});


			modelBuilder.Entity<SaleInvoice>(entity =>
            {
                entity.HasData(new SaleInvoice {Id=1,costs=200000,CustomerId=1 },new SaleInvoice {Id=2,CustomerId=2,costs=222 });
				entity.HasOne(a => a.Customer).WithMany(p => p.saleinvoice).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Restrict);
			}
			);
			modelBuilder.Entity<Customer>(entity =>
			{
                entity.HasData(new Customer { Id = 1, Name = "dariush" }
                ,new Customer {Id=2,Name="Abbas" }
                
                );
				

			});
			modelBuilder.Entity<CompanySupplier>(entity =>
            {
                entity.HasData(new CompanySupplier { CSId = 1, SupplierName = "masood", CompanyName = "Chitoz" },
                    new CompanySupplier { CSId = 2, SupplierName = "Arsalan", CompanyName = "PEPSI" },
                    new CompanySupplier { CSId = 3, SupplierName = "Kambiz", CompanyName = "Maz Maz" }
                    );
				/*		entity.HasOne(a => a.Invoice)
                        .WithOne(u => u.CompanySupplier)
                        .HasForeignKey<CompanySupplier>(p => p.Id);*/
				//	entity.HasOne(a => a.Invoice).WithOne(b => b.CompanySupplier).HasForeignKey(c=>).OnDelete(DeleteBehavior.Restrict);

				//entity.HasOne(a => a.Invoice).WithMany(b => b.CompanySupplier).HasForeignKey(c => c.);
             //   entity.HasOne(a=>a.Invoice).WithMany(b=>b.CompanySupplier).HasForeignKey(c=>c.CSId);

			});


            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasData(

                                new Category { Id = 1, Name = "drinks" },
                                new Category { Id = 2, Name = "junks" },
                                new Category { Id = 3, Name = "Sauce" });
                entity.HasMany(a => a.Product).WithOne(s => s.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasData(new Product { Id = 1, CategoryId = 2, Name = "Chips", Quantity = 100, Color = "blue", Price = 23,ListPrice =25,Price50=22,Price100=20,Description="fasf",ImageUrl="" },
                                new Product { Id = 2, CategoryId = 1, Name = "Soda",  Quantity = 100, Color = "blue", Price = 30, ListPrice = 32, Price50 = 28, Price100 = 26 ,Description="sfas", ImageUrl = "" },
                                new Product { Id = 3, CategoryId = 3, Name = "sauce", Quantity = 100, Color = "blue", Price = 30, ListPrice = 32, Price50 = 28, Price100 = 26 ,Description="fa", ImageUrl = "" }
                                );

                entity.HasMany(a => a.sale).WithOne(p => p.product).HasForeignKey(o =>o.ProductId).OnDelete(DeleteBehavior.Restrict);
                
                /*				entity.HasOne(a => a.Category)
								.WithMany(u => u.Product)
								.HasForeignKey(p => p.CategoryId);*/


                /*entity.HasOne(a => a.companySupplier)
				.WithMany(x => x.Products)
				.HasForeignKey(p => p.CompanyId);*/
                //entity.HasOne<Category>(a => a.Category)
                //  .WithOne(u => u.product)
                //  .HasForeignKey<Category>(p => p.ProductId);

            });
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasData(new Sale { Id = 1, ProductId = 1, sellQ = 23, TheDate = DateTime.Now, SaleInvoiceId = 1 ,Price=23000,SalePrice=30000},
                    new Sale { Id=2,ProductId=1,sellQ=34,TheDate=DateTime.Now,SaleInvoiceId=1,Price=20000,SalePrice=400000}
                    );;
                entity.HasOne(a => a.saleinvoice).WithMany(o => o.sale).HasForeignKey(p => p.SaleInvoiceId).OnDelete(DeleteBehavior.Restrict);

            }

            );



            modelBuilder.Entity<InvoiceDetails>(entity =>
            {
                entity.Property(e => e.ExpiryDate).HasColumnType("date");
                entity.HasData(
                    new InvoiceDetails { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 4, Price = 290000, SalePrice = 3600000, ExpiryDate = DateTime.Now }

                    );
                //entity.HasOne(a => a.Invoice).WithMany(b => b.InvoiceDetails).HasForeignKey(d => d.InvoiceId);
                entity.HasOne(a => a.Product).WithMany(b => b.InvoiceDetails).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Restrict);
                /*entity.HasOne(a => a.Product)
				.WithMany(u => u.InvoiceDetails)
				.HasForeignKey(p => p.ProductId);*/
                //entity.HasOne(a=>a.Invoice).WithOne(c =>c.InvoiceDetails).HasForeignKey(x => x.Id);
            }
            );


            //modelBuilder.Entity<Invoice>().HasOne.withMan






        }
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
		  {
			  modelBuilder.Entity<Invoice>().HasData(
				  new Invoice { InvoiceId = 1, UserId = 2,SupplierId=1,Costs=233000,CreationDate=DateTime.Now.ToString() ,PaymentId=2 }

				  );
			  modelBuilder.Entity<Invoice>().HasOne(I=>I.InvoiceId).

		  }
		  protected override void OnModelCreating(ModelBuilder modelBuilder)
		  {
			  modelBuilder.Entity<InvoiceDetails>().HasData(
				  new InvoiceDetails {Id=1,InvoiceId=2,ProductId=1,Quantity=20,Price=129000,SalePrice=150000,CompanyName="Chitoz",ExpiryDate="1404/4/24" }

				  );
		  }*/

    }
}
