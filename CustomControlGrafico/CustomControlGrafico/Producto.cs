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
    }

    public class GestorProductos
    {
        public static ObservableCollection<Producto> getProductos()
        {
            return new ObservableCollection<Producto> {
                new Producto(){ Nombre = "Procesador", Stock = 5, PrecioCompra = 0.25, PrecioVenta = 1.99 },
                new Producto(){ Nombre = "RAM", Stock = 2, PrecioCompra = 0.25, PrecioVenta = 1.99 },
                new Producto(){ Nombre = "Tarjeta gráfica", Stock = 9, PrecioCompra = 0.25, PrecioVenta = 1.99 },
                new Producto(){ Nombre = "Disco duro", Stock = 3, PrecioCompra = 0.25, PrecioVenta = 1.99 },
                new Producto(){ Nombre = "Monitor", Stock = 10, PrecioCompra = 0.25, PrecioVenta = 1.99 }
            };
        }
    }
}
