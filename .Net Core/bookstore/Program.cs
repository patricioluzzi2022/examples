using Library.Actions;
using Library.Context;
using Library.Models;
using Library.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<AuthQuery>();
builder.Services.AddScoped<AuthorsQuery>();
builder.Services.AddScoped<AuthorsAction>();
builder.Services.AddScoped<BooksQuery>();
builder.Services.AddScoped<BooksAction>();
builder.Services.AddScoped<NotesQuery>();
builder.Services.AddScoped<NoteAction>();
builder.Services.AddScoped<PublishersQuery>();
builder.Services.AddScoped<PublisherAction>();
builder.Services.AddScoped<InventoryLocationQuery>();
builder.Services.AddScoped<InventoryLocationAction>();
builder.Services.AddScoped<ShipmentsQuery>();
builder.Services.AddScoped<ShipmentAction>();
builder.Services.AddScoped<TrackMapQuery>();
builder.Services.AddScoped<TrackMapAction>();

// Registrar el esquema de GraphQL
builder.Services
    .AddDbContext<LibraryDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDatabase")))
    .AddGraphQLServer()
    //.AddGlobalObjectIdentification();
    //.AddGlobalObjectIdentification()
    .AddQueryType<RootQuery>()
    .AddMutationType<RootAction>();

var app = builder.Build();

app.MapGraphQL(builder.Configuration.GetConnectionString("Enviroment"));

app.Run();