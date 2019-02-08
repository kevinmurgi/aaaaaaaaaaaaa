using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlGrafico
{
    /// <summary>
    /// Clase Producto. Establece la estructura de los objetos que se mostrarán en el gráfico.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Atributo Nombre.
        /// Hace referencia al nombre del producto.
        /// </summary>
        /// <value>string</value>
        public string Nombre { get; set; }

        /// <summary>
        /// Atributo Stock.
        /// Hace referencia a la cantidad de stock actual que tiene el producto.
        /// </summary>
        /// <value>int</value>
        public int Stock { get; set; }

        /// <summary>
        /// Atributo StockMinimo.
        /// Hace referencia al Stock mínimo de este producto que debe haber en almacén.
        /// </summary>
        /// <value>int</value>
        public int StockMinimo { get; set; }

        /// <summary>
        /// Atributo PrecioCompra.
        /// Hace referencia al precio de compra del producto (a proveedores).
        /// </summary>
        /// <value>double</value>
        public double PrecioCompra { get; set; }

        /// <summary>
        /// Atributo PrecioVenta.
        /// Hace referencia al precio de venta del producto (a clientes).
        /// </summary>
        /// <value>double</value>
        public double PrecioVenta { get; set; }

        /// <summary>
        /// Atributo Imagen.
        /// Es la ruta de la imagen del producto que se mostrará a través de UI.
        /// </summary>
        /// <value>String</value>
        public String Imagen { get; set; }
    }

    /// <summary>
    /// Clase GestorProductos.
    /// </summary>
    public class GestorProductos
    {
        /// <summary>
        /// Método estático getProductos().
        /// Permite obtener una lista de productos.
        /// </summary>
        /// <returns>ObservableCollection<see cref="Producto"/></returns>
        public static ObservableCollection<Producto> getProductos()
        {
            return new ObservableCollection<Producto> {
                new Producto(){ Nombre = "Procesador", Stock = 5, StockMinimo = 5, PrecioCompra = 45.00, PrecioVenta = 299.99, Imagen = "Assets/procesador.jpg" },
                new Producto(){ Nombre = "RAM", Stock = 2, StockMinimo = 5, PrecioCompra = 27.25, PrecioVenta = 129.99, Imagen = "Assets/ram.jpg" },
                new Producto(){ Nombre = "Tarjeta gráfica", Stock = 9, StockMinimo = 3, PrecioCompra = 120.00, PrecioVenta = 449.99, Imagen = "Assets/grafica.jpg" },
                new Producto(){ Nombre = "Disco duro", Stock = 3, StockMinimo = 5, PrecioCompra = 20.40, PrecioVenta = 69.99, Imagen = "Assets/discoduro.jpg" },
                new Producto(){ Nombre = "Monitor", Stock = 7, StockMinimo = 9, PrecioCompra = 65.00, PrecioVenta = 199.99, Imagen = "Assets/monitor.jpg" }
            };
        }
    }
}
