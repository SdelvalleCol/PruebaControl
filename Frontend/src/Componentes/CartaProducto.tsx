import React from 'react';
import type { IProducto } from '../App';
import ModalEdicion from './ModalEdicion';

interface Props {
    producto: IProducto;
    Recargar: () => void;
}

const CartaProducto: React.FC<Props> = ({ producto, Recargar }) => {

    const [producto_seleccionado, set_producto_seleccionado] = React.useState<IProducto | null>(null);
    const [recarga, set_recarga] = React.useState<boolean>(false);

    React.useEffect(() => {
        if (recarga == true) {
            Recargar();
        }
    }, [recarga]);

    return (
        <>
            <div className="max-w-xs bg-white rounded-2xl shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-300">
                <img
                    src={producto.url_Imagen}
                    alt={producto.nombre_producto}
                    className="w-full h-48 object-cover"
                />
                <div className="p-4">
                    <h2 className="text-xl font-semibold text-gray-800">{producto.nombre_producto}</h2>
                    <p className="text-sm text-gray-500 mb-2">{producto.descripcion_producto}</p>
                    <p className="text-lg font-bold text-green-600">${producto.precio_producto.toLocaleString()}</p>

                    <div className="text-xs text-gray-400 mt-2">
                        <p>Creado: {new Date(producto.fecha_creacion).toLocaleDateString()}</p>
                        <p className={producto.estado ? 'text-green-500' : 'text-red-500'}>
                            {producto.estado ? 'Activo' : 'Inactivo'}
                        </p>
                    </div>
                    <button onClick={() => { set_producto_seleccionado(producto) }} className='p-2 bg-orange-300 text-white rounded-lg shadow-md mt-5 w-full'>Editar</button>
                </div>
            </div>

            {
                producto_seleccionado != null ? (<ModalEdicion producto={producto_seleccionado} Recargar={() => { set_recarga(true) }} Cerrar={() => { set_producto_seleccionado(null) }}></ModalEdicion>) : (null)
            }
        </>

    );
};

export default CartaProducto;
