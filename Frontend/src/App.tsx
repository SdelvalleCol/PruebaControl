import React from "react"
import CartaProducto from "./Componentes/CartaProducto";

export interface IProducto {
  id_producto: string;
  nombre_producto: string;
  descripcion_producto: string;
  precio_producto: number;
  fecha_creacion: string;
  estado: boolean;
  identificador_Imagenes_producto: string;
  url_Imagen: string;
}

const App: React.FC = () => {

  const [productos, set_productos] = React.useState<IProducto[]>([]);
  const [recarga, set_recarga] = React.useState<boolean>(false);

  React.useEffect(() => {
    const CargaInfo = async () => {
      console.log("Cargando productos...");
      const requestOptions = {
        method: "GET",
        redirect: "follow" as RequestRedirect
      };
      const consulta = await fetch(`${import.meta.env.VITE_URL_API}/Obtener_Productos`, requestOptions);
      const respuesta = await consulta.json();
      if (respuesta["codigo"] === 200) {
        set_productos(respuesta["productos"]);
      }
    };
    CargaInfo();
  }, [recarga]);

  return (
    <div className="bg-gray-100 min-h-screen text-black">
      <div className="p-5 text-lg w-full bg-white rounded-lg shadow-md">
        Productos disponibles
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4 mt-5">
        {Array.isArray(productos) && productos.length > 0 ? (
          productos.map((producto: IProducto) => (
            <CartaProducto key={producto.id_producto} producto={producto} Recargar={() => { set_recarga(!recarga) }} />
          ))
        ) : (
          <p className="col-span-full text-center text-gray-500">No hay productos</p>
        )}
      </div>

    </div>);
}
export default App
