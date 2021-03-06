﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace CustomControlGrafico
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Producto> productos = GestorProductos.getProductos();
        /// <summary>
        /// Inicializa una <see cref="MainPage"/>.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.productos.Add(new Producto() {
                Nombre = nombre.Text.ToString(),
                Stock = int.Parse(stockActual.Text.ToString()),
                StockMinimo = int.Parse(stockMinimo.Text.ToString()),
                PrecioCompra = int.Parse(precioCompra.Text.ToString()),
                PrecioVenta = int.Parse(precioVenta.Text.ToString()),
                Imagen = imagen.Text.ToString()
            });
        }
    }
}
