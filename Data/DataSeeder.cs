using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopEasy.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext Context;
        public DataSeeder(AppDbContext context) { Context = context; }
        public async Task SeedAsync()
        {
            await SeedEntityAsync<Category>(Context.Categories, "JsonData/categories.json");
            await SeedEntityAsync<Tag>(Context.Tags, "JsonData/tags.json");
            await SeedEntityAsync<Customer>(Context.Customers, "JsonData/customers.json");
            await SeedEntityAsync<CustomerProfile>(Context.CustomerProfiles, "JsonData/customerProfiles.json");
            await SeedEntityAsync<Product>(Context.Products, "JsonData/products.json");
            await SeedEntityAsync<Order>(Context.Orders, "JsonData/orders.json");
            await SeedEntityAsync<OrderItem>(Context.OrderItems, "JsonData/orderItems.json");
            await SeedEntityAsync<Payment>(Context.Payments, "JsonData/payments.json");
            await SeedEntityAsync<Review>(Context.Reviews, "JsonData/reviews.json");

            if (!Context.ProductTags.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/productTags.json");
                if (!File.Exists(path))
                    throw new FileNotFoundException(path);
                var data = await File.ReadAllTextAsync(path);
                var entities = JsonSerializer.Deserialize<IEnumerable<ProductTag>>(data, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                if (entities is not null)
                {
                    var entityType = Context.Model.FindEntityType(typeof(ProductTag))!;
                    var tableName = entityType.GetTableName();
                    var schema = entityType.GetSchema() ?? "dbo";

                    var fullName = $"[{schema}].[{tableName}]";

                    using var transaction = await Context.Database.BeginTransactionAsync();
                    await Context.ProductTags.AddRangeAsync(entities);
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            if (!Context.Discounts.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/discounts.json");

                if (!File.Exists(path))
                    throw new FileNotFoundException(path);
                var data = await File.ReadAllTextAsync(path);
                var entities = JsonSerializer.Deserialize<IEnumerable<Discount>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (entities is not null)
                {
                    var entityType = Context.Model.FindEntityType(typeof(Discount))!;
                    var tableName = entityType.GetTableName();
                    var schema = entityType.GetSchema() ?? "dbo";
                    var fullName = $"[{schema}].[{tableName}]";
                    using var transaction = await Context.Database.BeginTransactionAsync();
                    await Context.Discounts.AddRangeAsync(entities);
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
        }
        private async Task SeedEntityAsync<T>(DbSet<T> dbSet, string filePath) where T : class
        {
            if (dbSet.Any()) return;
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path))
                throw new FileNotFoundException(path);
            var data = await File.ReadAllTextAsync(path);

            var entities = JsonSerializer.Deserialize<IEnumerable<T>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
            if (entities is not null)
            {
                var entityType = Context.Model.FindEntityType(typeof(T))!;
                var tableName = entityType.GetTableName();
                var schema = entityType.GetSchema() ?? "dbo";

                var fullName = $"[{schema}].[{tableName}]";

                using var transaction = await Context.Database.BeginTransactionAsync();

                await Context.Database.ExecuteSqlRawAsync($"Set Identity_Insert {fullName} on");
                await dbSet.AddRangeAsync(entities);
                await Context.SaveChangesAsync();
                await Context.Database.ExecuteSqlRawAsync($"Set Identity_Insert {fullName} off");
                await transaction.CommitAsync();

            }
        }
    }
}