import React, { useState } from "react";
import type { IProducto } from "../App";

interface ModalEdicionProps {
    producto: IProducto;
    Cerrar: () => void;
    Recargar: () => void;
}

const ModalEdicion: React.FC<ModalEdicionProps> = ({ producto, Cerrar, Recargar }) => {
    const [form, setForm] = useState<IProducto>(producto);

    const handleChange = (field: keyof IProducto) => (
        e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
    ) => {
        const { value } = e.target;
        setForm(prev => ({
            ...prev,
            [field]: field === "precio_producto"
                ? parseFloat(value) || 0
                : field === "estado"
                    ? value === "true"
                    : value
        }));
    };

    const handleSubmit = async () => {
        try {
            const myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");

            const raw = JSON.stringify({
                "id_producto": form.id_producto,
                "nombre_producto": form.nombre_producto,
                "descripcion_producto": form.descripcion_producto,
                "precio_producto": form.precio_producto,
                "fecha_creacion": form.fecha_creacion,
                "estado": form.estado,
                "identificador_Imagenes_producto": form.identificador_Imagenes_producto,
                "url_Imagen": form.url_Imagen
            });

            console.log(raw);
            const requestOptions = {
                method: "POST",
                headers: myHeaders,
                body: raw,
                redirect: "follow" as RequestRedirect
            };

            const consulta = await fetch(`${import.meta.env.VITE_URL_API}/Modificar_Producto`, requestOptions)
            const cuerpo = await consulta.json();
            if (cuerpo["codigo"] === 200) {

                alert("Producto modificado correctamente");
                Recargar();
                Cerrar();
            } else {
                alert(`Error al modificar el producto: ${cuerpo["mensaje"]}`);
            }
        } catch {
            alert("Error al conectar con el servidor. Por favor, inténtelo más tarde.");
        }
    };

    return (
        <div className="fixed inset-0 flex items-center justify-center bg-gray-100 bg-opacity-40 z-50">
            <form

                className="bg-white w-full max-w-4xl max-h-[90vh] overflow-y-auto rounded-xl shadow-xl p-6"
            >
                <h2 className="text-center text-2xl font-bold text-gray-800 mb-6">
                    Modificar producto
                </h2>

                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    {/* Formulario */}
                    <div className="flex flex-col gap-4">
                        <label className="flex flex-col">
                            <span className="mb-1 font-medium text-gray-700">Nombre</span>
                            <input
                                type="text"
                                value={form.nombre_producto}
                                onChange={handleChange("nombre_producto")}
                                className="border rounded p-3 w-full"
                                required
                            />
                        </label>

                        <label className="flex flex-col">
                            <span className="mb-1 font-medium text-gray-700">Descripción</span>
                            <input
                                type="text"
                                value={form.descripcion_producto}
                                onChange={handleChange("descripcion_producto")}
                                className="border rounded p-3 w-full"
                                required
                            />
                        </label>

                        <label className="flex flex-col">
                            <span className="mb-1 font-medium text-gray-700">Precio (COP)</span>
                            <input
                                type="number"
                                min={0}
                                value={form.precio_producto}
                                onChange={handleChange("precio_producto")}
                                className="border rounded p-3 w-full"
                                required
                            />
                        </label>

                        <label className="flex flex-col">
                            <span className="mb-1 font-medium text-gray-700">Estado</span>
                            <select
                                value={form.estado ? "true" : "false"}
                                onChange={handleChange("estado")}
                                className="border rounded p-3 w-full"
                            >
                                <option value="true">Activo</option>
                                <option value="false">Inactivo</option>
                            </select>
                        </label>

                        <label className="flex flex-col">
                            <span className="mb-1 font-medium text-gray-700">URL de la imagen</span>
                            <input
                                type="url"
                                value={form.url_Imagen}
                                onChange={handleChange("url_Imagen")}
                                className="border rounded p-3 w-full"
                                required
                            />
                        </label>
                    </div>

                    {/* Imagen */}
                    <div className="flex flex-col items-center justify-center gap-4">
                        <img
                            src={form.url_Imagen}
                            alt="Vista previa"
                            className="max-h-72 w-auto rounded border object-contain"
                        />
                    </div>
                </div>

                <div className="mt-8 flex flex-col md:flex-row gap-4">
                    <button
                        type="submit"
                        className="bg-orange-500 text-white px-6 py-3 rounded-lg hover:bg-orange-600 w-full md:w-1/2"
                        onClick={async () => { await handleSubmit() }}
                    >
                        Guardar cambios
                    </button>
                    <button
                        type="button"
                        onClick={Cerrar}
                        className="border border-gray-500 text-gray-800 px-6 py-3 rounded-lg hover:bg-gray-100 w-full md:w-1/2"
                    >
                        Cancelar
                    </button>
                </div>
            </form>
        </div>
    );
};

export default ModalEdicion;
