using System.Text.Json.Serialization;

public class ModelProduct
{
    [JsonPropertyName("id_producto")]
    public Guid Id_producto { get; set; }

    [JsonPropertyName("nombre_producto")]
    public string Nombre_producto { get; set; }

    [JsonPropertyName("descripcion_producto")]
    public string Descripcion_producto { get; set; }

    [JsonPropertyName("precio_producto")]
    public decimal Precio_producto { get; set; }

    [JsonPropertyName("fecha_creacion")]
    public DateTime Fecha_creacion { get; set; }

    [JsonPropertyName("estado")]
    public bool Estado { get; set; }

    [JsonPropertyName("identificador_Imagenes_producto")]
    public Guid Identificador_Imagenes_producto { get; set; }

    [JsonPropertyName("url_Imagen")]
    public string Url_Imagen { get; set; }

    [JsonConstructor]
    public ModelProduct(
        Guid id_producto,
        string nombre_producto,
        string descripcion_producto,
        decimal precio_producto,
        DateTime fecha_creacion,
        bool estado,
        Guid identificador_Imagenes_producto,
        string url_Imagen)
    {
        Id_producto = id_producto;
        Nombre_producto = nombre_producto;
        Descripcion_producto = descripcion_producto;
        Precio_producto = precio_producto;
        Fecha_creacion = fecha_creacion;
        Estado = estado;
        Identificador_Imagenes_producto = identificador_Imagenes_producto;
        Url_Imagen = url_Imagen;
    }

    public ModelProduct() { }
}