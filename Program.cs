
using PanchitoProyectApi.Configuration;
using PanchitoProyectApi.Services;

namespace PanchitoProyectApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<InformacionServices>();

            // Configura CORS
            builder.Services.AddCors(options => options.AddPolicy("AngularClient", policy => {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            var app = builder.Build(); // Mueve la declaración de 'app' aquí arriba

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AngularClient"); // Mueve esto antes de app.MapControllers()
            app.MapControllers();
            app.Run();
        }
    }
}