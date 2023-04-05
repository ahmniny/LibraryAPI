using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LibraryAPI;
using LibraryAPI.dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(connectionString));
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(MappingConfig));
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/api/books", (AppDbContext _context) =>
        {
            return _context.Books.ToListAsync();
        });
        app.MapGet("/api/authors", (AppDbContext _context) =>
        {
            return _context.Authors.ToListAsync();
        });
        app.MapGet("/api/categories", (AppDbContext _context) =>
        {
            return _context.Categories.ToListAsync();
        });
        app.MapGet("/api/main-categories", (AppDbContext _context) =>
        {
            return _context.MainCategories.ToListAsync();
        });
        // get by Id
        app.MapGet("/api/book/{id}", (int id, AppDbContext _context) =>
        {
            return _context.Books.Where(m => m.Id == id).FirstOrDefaultAsync();
        });
        app.MapGet("/api/author/{id}", (int id, AppDbContext _context) =>
        {
            return _context.Authors.Where(m => m.Id == id).FirstOrDefaultAsync();
        });
        app.MapGet("/api/category/{id}", (int id, AppDbContext _context) =>
        {
            return _context.Categories.Where(m => m.Id == id).FirstOrDefaultAsync();
        });
        app.MapGet("/api/main-category/{id}", (int id, AppDbContext _context) =>
        {
            return _context.MainCategories.Where(m => m.Id == id).FirstOrDefaultAsync();
        });
        // add model
        app.MapPost("/api/book", ([FromBody] Book book, AppDbContext _context) =>
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        });
        app.MapPost("/api/author", ([FromBody] Author author, AppDbContext _context) =>
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        });
        app.MapPost("/api/category", ([FromBody] Category category, AppDbContext _context) =>
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        });
        app.MapPost("/api/main-category", ([FromBody] MainCategory mainCategory, AppDbContext _context) =>
        {
            _context.MainCategories.Add(mainCategory);
            _context.SaveChanges();
        });
        // update model
        app.MapPut("/api/book", (
            [FromBody] BookDTO dto,
            AppDbContext _context,
            IMapper _mapper,
            IValidator<BookDTO> validator) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (validationResult.IsValid)
            {
                Book book = _context.Books.Find(dto.Id);
                //book = _mapper.Map<Book>(dto);
                book.AuthorId = dto.AuthorId;
                book.CategoryId = dto.CategoryId;
                book.Title = dto.Title;
                book.Year = dto.Year;
                book.Description = dto.Description;
                _context.SaveChanges();
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest(validationResult.Errors);
            }
        });
        app.MapPut("/api/author", async ([FromBody] AuthorDTO dto, AppDbContext _context, IMapper _mapper) =>
        {
            try
            {
                Author author = await _context.Authors.FindAsync(dto.Id);
                author.Name = dto.Name;
                author.Bio = dto.Bio;
                //author = _mapper.Map<Author>(dto);
                _context.SaveChangesAsync();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        });
        app.MapPut("/api/category", ([FromBody] Category category, AppDbContext _context, IMapper _mapper) =>
        {
            Category c = _context.Categories.Find(category.Id);
            if (c != null)
            {
                //c = _mapper.Map<Category>(category);
                c.Name = category.Name;
                c.MainCategoryId = category.MainCategoryId;
                _context.SaveChanges();
            }
        });
        app.MapPut("/api/main-category", ([FromBody] MainCategory _mainCategory, AppDbContext _context, IMapper _mapper) =>
        {
            MainCategory m = _context.MainCategories.Find(_mainCategory.Id);
            //m = _mapper.Map<MainCategory>(_mainCategory);
            m.Name = _mainCategory.Name;
            _context.SaveChanges();
        });
        app.Run();
    }
}
