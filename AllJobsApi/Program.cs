using AllJobsApi.Models;
using AllJobsApi.Models.Interface;
using AllJobsApi.Models.Repository;
using System.Data.OleDb;
using System.Reflection;
#if DEBUG
string path = @"C:\Ebares\BD.accdb";
#else
string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BD.accdb");
#endif
if (File.Exists(path))
{
    Console.WriteLine("Base de dados carregada!");
    IniciarApi(path);
}
else
{
    Console.Write("Base de dados não encontrado!");
    Console.ReadKey();
}


static void IniciarApi(string p)
{
    Console.WriteLine("Iniciando API...");
    var builder = WebApplication.CreateBuilder();
    var fileContent = string.Empty;
    var filePath = string.Empty;
    builder.WebHost.UseUrls($"https://*:*");
    builder.Services.AddSingleton<IDAO, DAO>(serviceProvider=>
    {
        return new DAO(p);
    });
    builder.Services.AddControllers();
    var app = builder.Build();

    app.MapControllers();
    app.Run();
}