using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlGrafico
{
    public class Producto
    {
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }
        public String Imagen { get; set; }
    }

    public class GestorProductos
    {
        public static ObservableCollection<Producto> getProductos()
        {
            return new ObservableCollection<Producto> {
                new Producto(){ Nombre = "Procesador", Stock = 5, PrecioCompra = 45.00, PrecioVenta = 299.99, Imagen = "Assets/procesador.jpg" },
                new Producto(){ Nombre = "RAM", Stock = 2, PrecioCompra = 27.25, PrecioVenta = 129.99, Imagen = "Assets/ram.jpg" },
                new Producto(){ Nombre = "Tarjeta gráfica", Stock = 9, PrecioCompra = 120.00, PrecioVenta = 449.99, Imagen = "Assets/grafica.jpg" },
                new Producto(){ Nombre = "Disco duro", Stock = 3, PrecioCompra = 20.40, PrecioVenta = 69.99, Imagen = "Assets/discoduro.jpg" },
                new Producto(){ Nombre = "Monitor", Stock = 7, PrecioCompra = 65.00, PrecioVenta = 199.99, Imagen = "Assets/monitor.jpg" }
            };
        }
    }
}
