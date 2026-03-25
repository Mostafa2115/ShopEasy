using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopEasy.Data;
using ShopEasy.Models;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);   //.UseLazyLoadingProxies();
        using var context = new AppDbContext(optionsBuilder.Options);
        await new DataSeeder(context).SeedAsync();
        Console.WriteLine("AppDbContext initialized successfully!");


        #region Register a new customer
        //using var transaction = await context.Database.BeginTransactionAsync();
        //try
        //{
        //    var customer = new Customer
        //    {
        //        FullName = "Mostafa Mahmoud",
        //        Email = "mostafa@gmail.com",
        //        PhoneNumber = "01012345678"
        //    };

        //    await context.Customers.AddAsync(customer);
        //    await context.SaveChangesAsync();

        //    var profile = new CustomerProfile
        //    {
        //        CustomerId = customer.CustomerId,
        //        Address = "farouq Street",
        //        City = "zagazig",
        //        PostalCode = "12345",
        //        NationalId = "29801011234567"
        //    };

        //    await context.CustomerProfiles.AddAsync(profile);
        //    await context.SaveChangesAsync();

        //    await transaction.CommitAsync();

        //    Console.WriteLine("Customer registered successfully!");
        //}
        //catch (Exception ex)
        //{
        //    await transaction.RollbackAsync();
        //    Console.WriteLine($"Error: {ex.Message}");
        //}
        #endregion

        #region View customer profile
        //int customerId = 1; 
        //var customer = await context.Customers
        //    .Include(c => c.CustomerProfile)  
        //    .Include(c => c.Orders)            
        //    .SingleOrDefaultAsync(c => c.CustomerId == customerId);

        //if (customer == null)
        //    Console.WriteLine("Customer not found ❌");
        //else
        //{
        //    Console.WriteLine($"Name: {customer.FullName}");
        //    Console.WriteLine($"Email: {customer.Email}");
        //    Console.WriteLine($"Phone: {customer.PhoneNumber}");
        //    if (customer.CustomerProfile != null)
        //    {
        //        Console.WriteLine($"Address: {customer.CustomerProfile.Address}");
        //        Console.WriteLine($"City: {customer.CustomerProfile.City}");
        //        Console.WriteLine($"Postal Code: {customer.CustomerProfile.PostalCode}");
        //    }
        //    Console.WriteLine($"Orders Count: {customer.Orders.Count}");
        //    foreach (var order in customer.Orders)
        //        Console.WriteLine($"- Order #{order.OrderId} | Total: {order.TotalAmount} | Status: {order.Status}");
        //}
        #endregion

        #region  Update customer address
        //int customerId = 1; 
        //var customer = await context.Customers
        //    .SingleOrDefaultAsync(c => c.CustomerId == customerId);
        //if (customer == null)
        //{
        //    Console.WriteLine("Customer not found");
        //    return;
        //}
        //await context.Entry(customer)
        //             .Reference(c => c.CustomerProfile)
        //             .LoadAsync();

        //if (customer.CustomerProfile == null)
        //{
        //    customer.CustomerProfile = new CustomerProfile
        //    {
        //        CustomerId = customer.CustomerId
        //    };
        //    context.CustomerProfiles.Add(customer.CustomerProfile);
        //}

        //customer.CustomerProfile.Address = "mahata street";
        //customer.CustomerProfile.City = "zagazig";
        //customer.CustomerProfile.PostalCode = "12345";
        //await context.SaveChangesAsync();

        //Console.WriteLine("Customer address updated successfully");
        #endregion

        #region  Browse all active products
        //var products = await context.Products.Include(p => p.Category).OrderBy(p => p.Price).Select(p => new                           
        //               {
        //                      Name = p.Name,
        //                      Price = p.Price,
        //                      CategoryName = p.Category.Name
        //               }).AsNoTracking().ToListAsync();
        //foreach (var p in products)
        //    Console.WriteLine($"{p.Name} | {p.Price} | {p.CategoryName}");
        #endregion

        #region Search products by name or category
        //string keyword = "phone";     
        //string categoryName = "Electronics"; 
        //var products = await context.Products.Include(p => p.Category).Where(p =>
        //                     p.Name.Contains(keyword) ||p.Category.Name.Contains(categoryName)).Select(p => new
        //                     {
        //                              Name = p.Name,
        //                              Price = p.Price,
        //                              CategoryName = p.Category.Name
        //                     }).AsNoTracking().ToListAsync();
        //if (!products.Any())
        //    Console.WriteLine("No products found");
        //else
        //{
        //    Console.WriteLine("Search Results");
        //    foreach (var p in products)
        //        Console.WriteLine($"{p.Name} | {p.Price} | {p.CategoryName}");
        //}
        #endregion

        #region View product details with tags and reviews
        //int productId = 1; 
        //var product = await context.Products.Include(p => p.ProductTags).ThenInclude(pt => pt.Tag).Include(p => p.Reviews).SingleOrDefaultAsync(p => p.ProductId == productId);
        //if (product == null)
        //    Console.WriteLine("Product not found");
        //else
        //{
        //    Console.WriteLine($"Product: {product.Name}");
        //    Console.WriteLine($"SKU: {product.SKU}");
        //    Console.WriteLine($"Price: {product.Price:C}");
        //    Console.WriteLine($"Stock: {product.StockQuantity}");
        //    Console.WriteLine($"Category: {product.CategoryId}"); 

        //    var tags = product.ProductTags.Select(pt => pt.Tag.Name).ToList();
        //    Console.WriteLine("Tags: " + (tags.Any() ? string.Join(", ", tags) : "No tags"));

        //    if (product.Reviews.Any())
        //    {
        //        var avgRating = product.Reviews.Average(r => r.Rating);
        //        var totalReviews = product.Reviews.Count();
        //        Console.WriteLine($"Average Rating: {avgRating:F1} ⭐ ({totalReviews} reviews)");
        //        Console.WriteLine("Reviews:");
        //        foreach (var review in product.Reviews)
        //            Console.WriteLine($"- {review.Rating}⭐ by Customer {review.CustomerId}: {review.Comment}");
        //    }
        //    else
        //        Console.WriteLine("No reviews yet.");
        //}
        #endregion

        #region  Get top 5 highest-rated products
        //var topProducts = await context.Reviews.GroupBy(r => r.ProductId).Select(g => new
        //                  {
        //                        ProductId = g.Key,
        //                        AverageRating = g.Average(r => r.Rating)
        //                  }).OrderByDescending(x => x.AverageRating).Take(5).ToListAsync();

        //var topProductsWithNames = await context.Products.Where(p => topProducts.Select(t => t.ProductId).Contains(p.ProductId)).Select(p => new
        //                           {
        //                                  Name = p.Name,
        //                                  AverageRating = topProducts.First(t => t.ProductId == p.ProductId).AverageRating
        //                           }).ToListAsync();

        //Console.WriteLine("Top 5 Highest-Rated Products:");
        //foreach (var p in topProductsWithNames)
        //    Console.WriteLine($"{p.Name} | Average Rating: {p.AverageRating:F2}");
        #endregion

        #region Deactivate all out-of-stock products
        //int updatedCount = await context.Products
        //    .Where(p => p.StockQuantity == 0)
        //    .ExecuteUpdateAsync(p => p.SetProperty(prod => prod.IsActive, false));

        //Console.WriteLine($"{updatedCount} out-of-stock products deactivated.");
        //int inactiveCount = await context.Products.CountAsync(p => !p.IsActive);
        //Console.WriteLine($"Total inactive products: {inactiveCount}");
        #endregion

        #region  Place a new order
        //using var transaction = await context.Database.BeginTransactionAsync();
        //try
        //{
        //    int customerId = 1;
        //    var orderItemsInput = new List<(int ProductId, int Quantity)>
        //    {
        //        (1, 2),
        //        (2, 1)
        //    };
        //    var productIds = orderItemsInput.Select(x => x.ProductId).ToList();
        //    var products = await context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();

        //    if (products.Count != orderItemsInput.Count)
        //    {
        //        Console.WriteLine("Some products not found");
        //        return;
        //    }
        //    var order = new Order
        //    {
        //        CustomerId = customerId,
        //        Status = OrderStatus.Pending.ToString(),
        //        PlacedAt = DateTime.UtcNow,
        //        TotalAmount = 0
        //    };

        //    await context.Orders.AddAsync(order);
        //    await context.SaveChangesAsync(); 
        //    decimal totalAmount = 0;
        //    foreach (var item in orderItemsInput)
        //    {
        //        var product = products.First(p => p.ProductId == item.ProductId);

        //        if (product.StockQuantity < item.Quantity)
        //            throw new Exception($"Not enough stock for product {product.Name}");

        //        product.StockQuantity -= item.Quantity;

        //        var orderItem = new OrderItem
        //        {
        //            OrderId = order.OrderId,
        //            ProductId = product.ProductId,
        //            Quantity = item.Quantity,
        //            UnitPrice = product.Price
        //        };

        //        totalAmount += product.Price * item.Quantity;

        //        await context.OrderItems.AddAsync(orderItem);
        //    }

        //    order.TotalAmount = totalAmount;

        //    var payment = new Payment
        //    {
        //        OrderId = order.OrderId,
        //        Method = "Cash",
        //        Status = "Pending",
        //        Amount = totalAmount
        //    };

        //    await context.Payments.AddAsync(payment);
        //    await context.SaveChangesAsync();
        //    await transaction.CommitAsync();

        //    Console.WriteLine("Order placed successfully");
        //}
        //catch (Exception ex)
        //{
        //    await transaction.RollbackAsync();
        //    Console.WriteLine($"Error: {ex.Message}");
        //}
        #endregion

        #region View order history
        //int customerId = 1; 

        //var orders = await context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.OrderItems).Include(o => o.Payment).OrderByDescending(o => o.PlacedAt).ToListAsync();

        //Console.WriteLine("Order History:");
        //foreach (var order in orders)
        //{
        //    Console.WriteLine($"Order #{order.OrderId} | Status: {order.Status} | Total: {order.TotalAmount} | Date: {order.PlacedAt}");

        //    foreach (var item in order.OrderItems)
        //        Console.WriteLine($"   - ProductId: {item.ProductId}, Qty: {item.Quantity}, Price: {item.UnitPrice}");

        //    if (order.Payment != null)
        //        Console.WriteLine($"   Payment: {order.Payment.Status} | Amount: {order.Payment.Amount}");
        //}

        //var latestOrder = await context.Orders.Where(o => o.CustomerId == customerId).OrderByDescending(o => o.PlacedAt).FirstOrDefaultAsync();                          

        //if (latestOrder != null)
        //    Console.WriteLine($"\nLatest Order ID: {latestOrder.OrderId}");
        //else
        //    Console.WriteLine("No orders found");
        #endregion

        #region Cancel a pending order
        //int orderId = 1; 

        //using var transaction = await context.Database.BeginTransactionAsync();

        //try
        //{
        //    var order = await context.Orders.Include(o => o.OrderItems).Include(o => o.Payment).SingleOrDefaultAsync(o => o.OrderId == orderId);

        //    if (order == null)
        //    {
        //        Console.WriteLine("Order not found");
        //        return;
        //    }

        //    if (order.Status != OrderStatus.Pending.ToString())
        //    {
        //        Console.WriteLine("Only pending orders can be cancelled");
        //        return;
        //    }

        //    foreach (var item in order.OrderItems)
        //    {
        //        var product = await context.Products.SingleAsync(p => p.ProductId == item.ProductId);
        //        product.StockQuantity += item.Quantity;
        //    }

        //    order.Status = OrderStatus.Cancelled.ToString();
        //    context.Orders.Update(order);

        //    if (order.Payment != null)
        //    {
        //        order.Payment.Status = "Refunded";
        //        context.Payments.Update(order.Payment);
        //    }

        //    await context.SaveChangesAsync();
        //    await transaction.CommitAsync();

        //    Console.WriteLine("Order cancelled successfully");
        //}
        //catch (Exception ex)
        //{
        //    await transaction.RollbackAsync();
        //    Console.WriteLine($"Error: {ex.Message}");
        //}
        #endregion

        #region  Monthly revenue report
        //var currentYear = DateTime.UtcNow.Year;

        //var monthlyRevenue = await context.Orders.Where(o => o.PlacedAt.Year == currentYear && o.Status == OrderStatus.Completed.ToString()).GroupBy(o => o.PlacedAt.Month).Select(g => new
        //                     {
        //                           Month = g.Key,
        //                           TotalRevenue = g.Sum(o => o.TotalAmount)                                     
        //                     }).OrderBy(g => g.Month).ToListAsync();

        //Console.WriteLine("Monthly Revenue Report:");

        //foreach (var item in monthlyRevenue)
        //    Console.WriteLine($"Month: {item.Month} | Revenue: {item.TotalRevenue:C}");
        #endregion

        #region  View pending orders using raw SQL
        // sql raw query
        //var pendingOrders = await context.Orders.FromSqlRaw("SELECT * FROM shop.Orders WHERE Status = 'Pending'").AsNoTracking().ToListAsync();
        //foreach (var order in pendingOrders)
        //    Console.WriteLine($"Order #{order.OrderId} | {order.Status} | {order.TotalAmount}");

        // stored procedure
        //var pendingOrders = await context.Orders.FromSqlRaw("EXEC GetPendingOrders").AsNoTracking().ToListAsync();
        //foreach (var order in pendingOrders)
        //    Console.WriteLine($"Order #{order.OrderId} | {order.Status} | {order.TotalAmount}");
        #endregion

        #region Apply a discount code at checkout
        //int customerId = 1;
        //string discountCode = "YALABEEEENA"; 
        //using var transaction = await context.Database.BeginTransactionAsync();

        //try
        //{
        //    var order = await context.Orders.Where(o => o.CustomerId == customerId && o.Status == OrderStatus.Pending.ToString()).Include(o => o.Payment).OrderByDescending(o => o.PlacedAt).FirstOrDefaultAsync();

        //    if (order == null)
        //    {
        //        Console.WriteLine("No pending order found");
        //        return;
        //    }

        //    var discount = await context.Discounts.SingleOrDefaultAsync(d => d.Code == discountCode);

        //    if (discount == null)
        //    {
        //        Console.WriteLine("Discount code not found");
        //        return;
        //    }

        //    if (!discount.IsActive || discount.ExpiresAt < DateTime.UtcNow)
        //    {
        //        Console.WriteLine("Discount code is expired or inactive");
        //        return;
        //    }

        //    if (discount.CurrentUses >= discount.MaxUses)
        //    {
        //        Console.WriteLine("Discount code usage limit reached");
        //        return;
        //    }

        //    decimal discountAmount = order.TotalAmount * (discount.Percentage / 100);
        //    order.TotalAmount -= discountAmount;
        //    discount.CurrentUses += 1;
        //    context.Orders.Update(order);
        //    context.Discounts.Update(discount);
        //    await context.SaveChangesAsync();
        //    await transaction.CommitAsync();

        //    Console.WriteLine($"Discount applied successfully New Total: {order.TotalAmount:C}");
        //}
        //catch (Exception ex)
        //{
        //    await transaction.RollbackAsync();
        //    Console.WriteLine($"Error applying discount: {ex.Message}");
        //}
        #endregion

        #region  Bulk delete expired discounts
        //using var transaction = await context.Database.BeginTransactionAsync();

        //try
        //{
        //    var deletedCount = await context.Discounts.Where(d => d.ExpiresAt < DateTime.UtcNow || !d.IsActive).ExecuteDeleteAsync();  
        //    await transaction.CommitAsync();
        //    Console.WriteLine($"Expired/inactive discounts deleted: {deletedCount}");
        //}
        //catch (Exception ex)
        //{
        //    await transaction.RollbackAsync();
        //    Console.WriteLine($"Error deleting discounts: {ex.Message}");
        //}
        #endregion

        #region Lazy-load product reviews on demand
        //var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == 1);

        //Console.WriteLine($"Product: {product.Name}");

        //Console.WriteLine("Accessing Reviews...");
        //foreach (var review in product.Reviews) 
        //    Console.WriteLine($"Review #{review.ReviewId}: {review.Comment}");
        #endregion

        #region  Load customer data using split queries
        //int customerId = 1;

        //var customer = await context.Customers.Where(c => c.CustomerId == customerId).Include(c => c.Orders).ThenInclude(o => o.OrderItems).Include(c => c.Orders).ThenInclude(o => o.Payment).Include(c => c.Reviews).AsSplitQuery().AsNoTracking().SingleOrDefaultAsync();

        //if (customer == null)
        //    Console.WriteLine("Customer not found");
        //else
        //{
        //    Console.WriteLine($"Customer: {customer.FullName}");
        //    Console.WriteLine($"Orders ({customer.Orders.Count}):");

        //    foreach (var order in customer.Orders)
        //    {
        //        Console.WriteLine($"- Order #{order.OrderId} | Status: {order.Status} | Total: {order.TotalAmount:C}");
        //        foreach (var item in order.OrderItems)
        //            Console.WriteLine($"  - Item: {item.ProductId} | Quantity: {item.Quantity} | Price: {item.UnitPrice:C}");
        //    }

        //    Console.WriteLine($"Reviews ({customer.Reviews.Count}):");
        //    foreach (var review in customer.Reviews)
        //        Console.WriteLine($"- Product #{review.ProductId} | Rating: {review.Rating} | Comment: {review.Comment}");
        //}
        #endregion

        #region Find customers with no orders
        //var customers = await context.Customers.Where(c => !c.Orders.Any()).Select(c => new
        //                {
        //                      c.FullName,
        //                      c.Email
        //                }).AsNoTracking().ToListAsync();

        //foreach (var c in customers)
        //    Console.WriteLine($"{c.FullName} | {c.Email}");

        //var customers = await context.Customers.GroupJoin(context.Orders,c => c.CustomerId,o => o.CustomerId,(c, orders) => new
        //                {
        //                      Customer = c,
        //                      Orders = orders
        //                }).Where(x => !x.Orders.Any()).Select(x => new
        //                {
        //                      x.Customer.FullName,
        //                      x.Customer.Email
        //                }).AsNoTracking().ToListAsync();

        //foreach (var c in customers)
        //    Console.WriteLine($"{c.FullName} | {c.Email}");
        #endregion

        #region  Products ranked by total quantity sold
        //var productsSold = await context.Products.Join(context.OrderItems,p => p.ProductId,oi => oi.ProductId,(p, oi) => new
        //                   {
        //                         p.ProductId,
        //                         p.Name,
        //                         oi.Quantity
        //                   }).GroupBy(x => new { x.ProductId, x.Name }).Select(g => new
        //                   {
        //                         ProductName = g.Key.Name,
        //                         TotalSold = g.Sum(x => x.Quantity)
        //                   }).OrderByDescending(x => x.TotalSold).AsNoTracking().ToListAsync();

        //Console.WriteLine("Top Selling Products:");
        //foreach (var p in productsSold)
        //    Console.WriteLine($"{p.ProductName} | Sold: {p.TotalSold}");
        #endregion


    }
}
