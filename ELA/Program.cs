using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ELAContext>(opt => opt.UseMySQL("Server=localhost;Database=ElaDb;user=root"));

// Camada Validacao

builder.Services.AddScoped<IUsuarioValidacao, UsuarioValidacao>();
builder.Services.AddScoped<IArtigoValidacao, ArtigoValidacao>();
builder.Services.AddScoped<IAssuntoValidacao, AssuntoValidacao>();
builder.Services.AddScoped<IPerguntaValidacao, PerguntaValidacao>();
builder.Services.AddScoped<IFiqueAtentoValidacao, FiqueAtentoValidacao>();

// Mapeamento

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<ArtigoRequest, Artigo>();
    cfg.CreateMap<ArtigoPutRequest, Artigo>();
    cfg.CreateMap<FiqueAtentoRequest, FiqueAtento>();
    cfg.CreateMap<FiqueAtentoPutRequest, FiqueAtento>();
    cfg.CreateMap<PerguntaRequest, Pergunta>();
    cfg.CreateMap<PerguntaPutRequest, Pergunta>();
    cfg.CreateMap<UsuarioRequest, Usuario>();
    cfg.CreateMap<AssuntoRequest, Assunto>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
