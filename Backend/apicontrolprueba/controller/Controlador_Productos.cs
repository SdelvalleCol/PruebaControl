using System;
using System.Collections.Generic;
using System.Data;

public class Controlador_Productos
{
    /// <summary>
    /// Obtiene una lista de todos los productos    
    /// </summary>
    public (bool, string, List<ModelProduct>) ObtenerProductos()
    {
        List<ModelProduct> productos = new List<ModelProduct>();
        try
        {
            string query = $@"
            SELECT p.id_producto,p.nombre_producto,p.descripcion_producto,p.precio_producto,p.fecha_creacion_producto,p.estado ,im.identificador_imagenes_productos,im.url_imagen 
            FROM productos as p
            inner join imagenes_productos as im on im.identificador_producto = p.id_producto
            ";
            DataTable data = Controlador_BD.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Guid id_producto = Guid.Parse(row["Id_producto"].ToString());
                string nombre_producto = row["Nombre_producto"].ToString();
                string descripcion_producto = row["Descripcion_producto"].ToString();
                decimal precio_producto = Convert.ToDecimal(row["Precio_producto"]);
                DateTime fecha_creacion = Convert.ToDateTime(row["fecha_creacion_producto"]);
                bool estado = Convert.ToBoolean(row["Estado"]);
                Guid id_producto_imagen = Guid.Parse(row["identificador_imagenes_productos"].ToString());
                string url_imagen = row["url_imagen"].ToString();
                ModelProduct producto = new ModelProduct(id_producto, nombre_producto, descripcion_producto, precio_producto, fecha_creacion, estado, id_producto_imagen, url_imagen);
                productos.Add(producto);
            }
            return (true, "Productos obtenidos correctamente", productos);

        }
        catch (Exception ex)
        {
            return (false, "Error al obtener los productos: " + ex.Message, null);
        }
    }

    /// <summary>
    /// Modifica un producto existente  
    /// </summary>

    public (bool success, string message) ModificarProducto(ModelProduct p)
    {
        const string sql = @"
            UPDATE productos as p , imagenes_productos
            SET
                p.nombre_producto                 = @nombre,
                p.descripcion_producto            = @descripcion,
                p.precio_producto                 = @precio,
                p.estado                          = @estado,
                url_Imagen                      = @url
            WHERE p.id_producto = @id and identificador_imagenes_productos = @identificador";

        var parametros = new Dictionary<string, object>
        {
            ["@id"] = p.Id_producto,
            ["@identificador"] = p.Identificador_Imagenes_producto,
            ["@nombre"] = p.Nombre_producto,
            ["@descripcion"] = p.Descripcion_producto,
            ["@precio"] = p.Precio_producto,
            ["@estado"] = p.Estado,
            ["@url"] = p.Url_Imagen
        };

        try
        {
            int filas = Controlador_BD.ExecuteNonQuery(sql, parametros);

            return filas > 0
                ? (true, "Producto modificado correctamente")
                : (false, "No se encontró el producto o no hubo cambios");
        }
        catch (Exception ex)
        {
            return (false, "Error al modificar el producto: " + ex.Message);
        }
    }
}