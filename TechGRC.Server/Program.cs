using Microsoft.EntityFrameworkCore;

using TechGRC.Server.Data;
using TechGRC.Server.Services;
using TechGRC.Server.Models;

using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext with an in-memory database for testing purposes
// Requires: Microsoft.EntityFrameworkCore.InMemory package
// In terminal, run: dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.3
builder.Services.AddDbContext<TechGrcDbContext>(options =>
    options.UseInMemoryDatabase("TechGrcDb"));

// Register services with dependency injection
builder.Services.AddScoped<IComplianceService, ComplianceService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IChatService, ChatService>();

// Register the OpenAI ChatClient with dependency injection
// Requires: OpenAI package
// In terminal, run: dotnet add package OpenAI
builder.Services.AddSingleton<ChatClient>(provider =>
{
    // Retrieve the OpenAI API key from configuration
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    var model = builder.Configuration["OpenAI:Model"] ?? "gpt-3.5-turbo"; // Default to gpt-3.5-turbo if not specified

    // Ensure the API key is provided
    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(model))
    {
        throw new InvalidOperationException("OpenAI API key not found in configuration.");
    }

    // Create and return a new ChatClient instance
    return new ChatClient(model, apiKey);
});

var app = builder.Build();

// Seed the database with mock data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TechGrcDbContext>();
    SeedDatabase(context);
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

// Seed method
static void SeedDatabase(TechGrcDbContext context)
{
   // Seed SupplierPartners (not Suppliers)
    if (!context.SupplierPartners.Any())
    {
        var suppliers = new[]
        {
            new SupplierPartner 
            { 
                Id = Guid.NewGuid(), 
                Name = "Microsoft Corporation", 
                Category = "Software & Technology", 
                ContactInfo = "enterprise@microsoft.com | +1-800-642-7676"
            },
            new SupplierPartner 
            { 
                Id = Guid.NewGuid(), 
                Name = "Amazon Web Services", 
                Category = "Cloud Services", 
                ContactInfo = "enterprise@aws.amazon.com | +1-206-266-4064"
            },
            new SupplierPartner 
            { 
                Id = Guid.NewGuid(), 
                Name = "Salesforce Inc.", 
                Category = "CRM Software", 
                ContactInfo = "enterprise@salesforce.com | +1-415-901-7000"
            },
            new SupplierPartner 
            { 
                Id = Guid.NewGuid(), 
                Name = "CrowdStrike Holdings", 
                Category = "Cybersecurity", 
                ContactInfo = "enterprise@crowdstrike.com | +1-888-512-8906"
            },
            new SupplierPartner 
            { 
                Id = Guid.NewGuid(), 
                Name = "Tableau Software", 
                Category = "Business Intelligence", 
                ContactInfo = "enterprise@tableau.com | +1-206-633-3400"
            }
        };
        
        context.SupplierPartners.AddRange(suppliers);
        context.SaveChanges(); // Save suppliers first to get their IDs
    }

    // Seed SupplyContracts (not Contracts)
    if (!context.SupplyContracts.Any())
    {
        var suppliers = context.SupplierPartners.ToList();
        var contracts = new[]
        {
            new SupplyContract 
            { 
                Id = Guid.NewGuid(), 
                ContractTitle = "Office 365 Enterprise License Agreement", // Note: ContractTitle, not Title
                Description = "Annual enterprise license for Microsoft Office 365 suite including Word, Excel, PowerPoint, Teams, and SharePoint services.",
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow.AddMonths(6),
                SupplierPartnerId = suppliers.First(s => s.Name.Contains("Microsoft")).Id
            },
            new SupplyContract 
            { 
                Id = Guid.NewGuid(), 
                ContractTitle = "AWS Cloud Infrastructure Services",
                Description = "Cloud hosting and computing services including EC2, S3, RDS, and Lambda functions for enterprise applications.",
                StartDate = DateTime.UtcNow.AddYears(-1),
                EndDate = DateTime.UtcNow.AddYears(1),
                SupplierPartnerId = suppliers.First(s => s.Name.Contains("Amazon")).Id
            },
            new SupplyContract 
            { 
                Id = Guid.NewGuid(), 
                ContractTitle = "Salesforce CRM Platform License",
                Description = "Customer relationship management platform with sales automation, marketing tools, and analytics dashboard.",
                StartDate = DateTime.UtcNow.AddMonths(-8),
                EndDate = DateTime.UtcNow.AddMonths(4),
                SupplierPartnerId = suppliers.First(s => s.Name.Contains("Salesforce")).Id
            },
            new SupplyContract 
            { 
                Id = Guid.NewGuid(), 
                ContractTitle = "Cybersecurity Monitoring Services",
                Description = "24/7 cybersecurity monitoring, threat detection, and incident response services for enterprise network protection.",
                StartDate = DateTime.UtcNow.AddMonths(-10),
                EndDate = DateTime.UtcNow.AddMonths(2),
                SupplierPartnerId = suppliers.First(s => s.Name.Contains("CrowdStrike")).Id
            },
            new SupplyContract 
            { 
                Id = Guid.NewGuid(), 
                ContractTitle = "Data Analytics Platform License",
                Description = "Advanced data analytics and business intelligence platform with real-time reporting and machine learning capabilities.",
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(21),
                SupplierPartnerId = suppliers.First(s => s.Name.Contains("Tableau")).Id
            }
        };
        
        context.SupplyContracts.AddRange(contracts);
        context.SaveChanges();
    }

    Console.WriteLine("Database seeded successfully with SupplierPartners and SupplyContracts!");
}
