// Program.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ────────────────────────────────────────────────────────────
// 1.  Swagger / OpenAPI
// ────────────────────────────────────────────────────────────
builder.Services.AddOpenApi();

// ────────────────────────────────────────────────────────────
// 2.  Kestrel: escucha en 5000 (HTTP) y 5001 (HTTPS)
// ────────────────────────────────────────────────────────────
builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(5000);                          // 0.0.0.0:5000  → HTTP
    o.ListenAnyIP(5001, opts => opts.UseHttps()); // 0.0.0.0:5001  → HTTPS
});

// ────────────────────────────────────────────────────────────
// 3.  CORS: política abierta SOLO para desarrollo
// ────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("Libre", policy =>
    {
        policy
            .AllowAnyOrigin()   // ⚠️  No uses esto en producción
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// ────────────────────────────────────────────────────────────
// 4.  Middlewares
// ────────────────────────────────────────────────────────────
app.UseCors("Libre");          //  ⬅️   ¡IMPORTANTE!
// app.UseHttpsRedirection();  //  ⬅️   Descomenta si SÍ usarás HTTPS en el fetch

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ────────────────────────────────────────────────────────────
// 5.  Endpoints
// ────────────────────────────────────────────────────────────

// Estado del servicio
app.MapGet("/online", () => new
{
    Status = "Online",
    Timestamp = DateTime.UtcNow
})
.WithName("getOnline");


// Obtener productos
app.MapGet("/Obtener_Productos", () =>
{
    try
    {
        var controlador = new Controlador_Productos();
        var (success, message, products) = controlador.ObtenerProductos();

        if (success && products is { Count: > 0 })
        {
            return Results.Ok(new
            {
                codigo = 200,
                mensaje = message,
                productos = products.Select(p => new
                {
                    p.Id_producto,
                    p.Nombre_producto,
                    p.Descripcion_producto,
                    p.Precio_producto,
                    p.Fecha_creacion,
                    p.Estado,
                    p.Identificador_Imagenes_producto,
                    p.Url_Imagen
                })
            });
        }

        // No hay productos o error de negocio
        return Results.Json(new
        {
            codigo = 500,
            mensaje = message
        }, statusCode: 500);
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Error en el servidor al obtener los productos",
            detail: ex.Message,
            statusCode: 500);
    }
})
.WithName("GetProducts")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError);


// Modificar producto
app.MapPost("/Modificar_Producto", (ModelProduct producto) =>
{
    try
    {
        var controlador = new Controlador_Productos();
        var (success, message) = controlador.ModificarProducto(producto);

        if (success)
        {
            return Results.Ok(new
            {
                codigo = 200,
                mensaje = message
            });
        }

        return Results.Json(new
        {
            codigo = 500,
            mensaje = message
        }, statusCode: 500);
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Error en el servidor al modificar el producto",
            detail: ex.Message,
            statusCode: 500);
    }
})
.WithName("ModifyProduct")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError);

// ────────────────────────────────────────────────────────────
// 6.  Run
// ────────────────────────────────────────────────────────────
app.Run();
