using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace CustomControlGrafico
{
    public sealed class GraficaBarras : Control
    {
        // Atributos
        private double longitudMaxima;
        private double alturaMaxima;
        private Line ejeX;
        private Line ejeY;
        Canvas miCanvas;
        private int maxStock = 0;
        private double tamañoPorStock;
        private double anchuraPorProducto;

        // Color de los ejes X e Y
        public SolidColorBrush ColorEjes
        {
            get { return (SolidColorBrush)GetValue(ColorEjesProperty); }
            set { SetValue(ColorEjesProperty, value); }
        }
        public static readonly DependencyProperty ColorEjesProperty =
            DependencyProperty.Register("ColorEjes", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        // Grosor de los ejes X e Y
        public int GrosorEjes
        {
            get { return (int)GetValue(GrosorEjesProperty); }
            set { SetValue(GrosorEjesProperty, value); }
        }
        public static readonly DependencyProperty GrosorEjesProperty =
            DependencyProperty.Register("GrosorEjes", typeof(int), typeof(GraficaBarras), new PropertyMetadata(null));

        // Color de las barras si tienen 5+ stock *Por defecto en verde*
        public SolidColorBrush ColorHayStock
        {
            get { return (SolidColorBrush)GetValue(ColorHayStockProperty); }
            set { SetValue(ColorHayStockProperty, value); }
        }
        public static readonly DependencyProperty ColorHayStockProperty =
            DependencyProperty.Register("ColorHayStock", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        // Color de las barras si tienen -5 stock *Por defecto en rojo*
        public SolidColorBrush ColorFaltaStock
        {
            get { return (SolidColorBrush)GetValue(ColorFaltaStockProperty); }
            set { SetValue(ColorFaltaStockProperty, value); }
        }
        public static readonly DependencyProperty ColorFaltaStockProperty =
            DependencyProperty.Register("ColorFaltaStock", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        // ItemsSource
        public ObservableCollection<Producto> ItemsSource
        {
            get { return (ObservableCollection<Producto>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<Producto>), typeof(GraficaBarras), new PropertyMetadata(null));



        // Constructor
        public GraficaBarras()
        {
            this.DefaultStyleKey = typeof(GraficaBarras);
            this.ColorHayStock = new SolidColorBrush(Colors.LawnGreen);
            this.ColorFaltaStock = new SolidColorBrush(Colors.Red);
        }

        // Metodo OnApplyTemplate
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Establecemos como longitud y altura maximas el tamaño del grid
            // Si la altura es menor de 200, se pone en 200, menos no se veria nada
            if (this.Height < 200){
                this.alturaMaxima = 200;
            }else{
                this.alturaMaxima = this.Height;
            }
            // Si la anchura es menor de 200, se pone en 200, menos no se veria nada
            if (this.Width < 200){
                this.longitudMaxima = 200;
            }else{
                this.longitudMaxima = this.Width;
            }            

            // Referenciamos las lineas de Generic.xaml para trabajar con ellas
            this.ejeX = (Line) GetTemplateChild("ejeX");
            this.ejeY = (Line) GetTemplateChild("ejeY");
            generarEjes();

            // Referenciamos el canvas y establecemos su tamaño
            miCanvas = (Canvas) GetTemplateChild("canvas");
            miCanvas.Width = this.longitudMaxima - (100 + this.GrosorEjes);
            miCanvas.Height = this.alturaMaxima - (100 + this.GrosorEjes);

            generarLetreros();
            establecerDimensiones();
            crearBarras();
        }

        private void generarEjes()
        {
            // Le ponemos el color que nos indica el usuario (por defecto negro)
            this.ejeX.Stroke = this.ColorEjes;
            this.ejeY.Stroke = this.ColorEjes;

            // Establecemos la posicion y tamaño de los ejes en funcion del tamaño del Grid que los contiene
            this.ejeY.X1 = this.ejeY.X2 = this.ejeY.Y1 = this.ejeX.X1 = 50;
            this.ejeX.Y1 = this.ejeX.Y2 = this.ejeY.Y2 = this.alturaMaxima - 50;
            this.ejeX.X2 = this.longitudMaxima - 50;
            this.ejeY.StrokeThickness = this.ejeX.StrokeThickness = this.GrosorEjes;
        }

        private void generarLetreros()
        {
            TextBlock textoEjeY = new TextBlock();
            textoEjeY.Text = "Stock";
            miCanvas.Children.Add(textoEjeY);
            Canvas.SetLeft(textoEjeY, -40);

            TextBlock textoEjeX = new TextBlock();
            textoEjeX.Text = "Productos";
            miCanvas.Children.Add(textoEjeX);
            Canvas.SetLeft(textoEjeX, this.longitudMaxima - 170);
            Canvas.SetTop(textoEjeX, this.alturaMaxima - 100);
        }

        private void establecerDimensiones()
        {
            if (this.ItemsSource != null)
            {
                // Recorre todos los productos recibidos por ItemsSource
                int cantProductos = 0;
                foreach (Producto p in this.ItemsSource){
                    // Almacena el stock maximo
                    if (p.Stock > this.maxStock){
                        this.maxStock = p.Stock;
                    }
                    cantProductos++;
                }
                // Establece la altura que sube la barra por cada unidad de stock del producto
                this.tamañoPorStock = (this.alturaMaxima - (100 + this.GrosorEjes)) / this.maxStock;
                // Establece la anchura que tiene un producto en funcion de la cantidad que hay
                this.anchuraPorProducto = ((this.longitudMaxima - (100 + this.GrosorEjes)) / cantProductos);
            }
        }

        private void crearBarras()
        {
            if (this.ItemsSource != null)
            {
                // Recorre todos los productos recibidos por el ItemsSources
                int cantProductos = 0;
                foreach (Producto p in this.ItemsSource)
                {
                    // Por cada uno de ellos crea un rectangulo
                    var rectangulo = new Rectangle();

                    // Dependiendo del stock se pinta de un color y otro
                    if (p.Stock >= 5){
                        rectangulo.Fill = this.ColorHayStock;
                    }else{
                        rectangulo.Fill = this.ColorFaltaStock;
                    }

                    // Le establecemos sus dimensiones
                    if (this.anchuraPorProducto > 20){
                        rectangulo.Width = this.anchuraPorProducto - 20;
                    }else{
                        rectangulo.Width = this.anchuraPorProducto - 1;
                    }
                    rectangulo.Height = this.tamañoPorStock * p.Stock;

                    // Lo añadimos al Canvas y lo posicionamos
                    miCanvas.Children.Add(rectangulo);
                    Canvas.SetLeft(rectangulo, 20 + (cantProductos * this.anchuraPorProducto));
                    Canvas.SetTop(rectangulo, this.alturaMaxima - (100 + rectangulo.Height + this.GrosorEjes));

                    // Ponemos el nombre del producto encima de la barra
                    TextBlock nombre = new TextBlock();
                    int count = 1;
                    foreach (char c in p.Nombre){
                        if (c == ' ') count++;
                    }
                    nombre.Text = p.Nombre.Replace(" ", "\n");

                    miCanvas.Children.Add(nombre);
                    Canvas.SetLeft(nombre, Canvas.GetLeft(rectangulo));
                    Canvas.SetTop(nombre, Canvas.GetTop(rectangulo) - (count*20));

                    cantProductos++;
                }
            }
        }

    }
}
