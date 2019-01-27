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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
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

        // Punto de stock en elq ue cambia de color
        public int NivelStock
        {
            get { return (int)GetValue(NivelStockProperty); }
            set { SetValue(NivelStockProperty, value); }
        }
        public static readonly DependencyProperty NivelStockProperty =
            DependencyProperty.Register("NivelStock", typeof(int), typeof(GraficaBarras), new PropertyMetadata(null));

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
            // Se establecen los valores por defecto
            this.DefaultStyleKey = typeof(GraficaBarras);
            this.GrosorEjes = 2;
            this.ColorEjes = new SolidColorBrush(Colors.Black);
            this.ColorHayStock = new SolidColorBrush(Colors.LawnGreen);
            this.ColorFaltaStock = new SolidColorBrush(Colors.Red);
            this.NivelStock = 5;
        }

        // Evento CollectionChanged para el ItemsSource que hace repintar los graficos
        private void ItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.generarLetreros();
            this.establecerDimensiones();
            this.crearBarras();
        }

        // Metodo OnApplyTemplate
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Le asignamos el evento al ItemsSource
            this.ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
            // Establecemos como longitud y altura maximas el tamaño del grid
            // Si la altura es menor de 300, se pone en 300, menos no se veria nada
            if (this.Height >= 300) {
                this.alturaMaxima = this.Height - 100;
            } else {
                this.Height = 300;
                this.alturaMaxima = this.Height - 100;
            }
            // Si la anchura es menor de 300, se pone en 300, menos no se veria nada
            if (this.Width >= 300) {
                this.longitudMaxima = this.Width;
            } else {
                this.Width = 300;
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
            miCanvas.Children.Clear();

            TextBlock textoEjeY = new TextBlock();
            textoEjeY.Text = "Stock";
            miCanvas.Children.Add(textoEjeY);
            Canvas.SetLeft(textoEjeY, - (40 + this.GrosorEjes));

            TextBlock textoEjeX = new TextBlock();
            textoEjeX.Text = "Productos";
            miCanvas.Children.Add(textoEjeX);
            Canvas.SetLeft(textoEjeX, this.longitudMaxima - 170);
            Canvas.SetTop(textoEjeX, this.alturaMaxima - 100);
        }

        private void establecerDimensiones()
        {
            if (this.ItemsSource != null) {
                // Recorre todos los productos recibidos por ItemsSource
                int cantProductos = 0;
                foreach (Producto p in this.ItemsSource) {
                    // Almacena el stock maximo
                    if (p.Stock > this.maxStock) {
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

        // Evento de los ractangulos de la grafica
        private void MyRectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rectangulo = sender as Rectangle;
            TextBlock nombre = (TextBlock)GetTemplateChild("nombre");
            TextBlock stock = (TextBlock)GetTemplateChild("stock");
            TextBlock pcompra = (TextBlock)GetTemplateChild("pcompra");
            TextBlock pventa = (TextBlock)GetTemplateChild("pventa");
            Image imagen = (Image)GetTemplateChild("imagen");
            

            foreach (Producto p in this.ItemsSource) {
                if (p.Nombre.Equals(rectangulo.Name)) {
                    imagen.Source = new BitmapImage(new Uri("ms-appx:///" + p.Imagen));
                    nombre.Text = p.Nombre;
                    stock.Text = p.Stock.ToString() + " unidades restantes";
                    pcompra.Text = "Compra: " + p.PrecioCompra.ToString() + "€";
                    pventa.Text = "Venta: " + p.PrecioVenta.ToString() + "€";
                }
            }
        }

        private void crearBarras()
        {
            if (this.ItemsSource != null) {
                // Recorre todos los productos recibidos por el ItemsSources
                int cantProductos = 0;
                Storyboard storyboard = new Storyboard();
                foreach (Producto p in this.ItemsSource) {
                    // Por cada uno de ellos crea un rectangulo
                    var rectangulo = new Rectangle();

                    // Dependiendo del stock se pinta de un color y otro
                    if (p.Stock >= this.NivelStock) {
                        rectangulo.Fill = this.ColorHayStock;
                    } else {
                        rectangulo.Fill = this.ColorFaltaStock;
                    }

                    // Le establecemos sus dimensiones
                    double ancho;
                    if (this.anchuraPorProducto > 20) {
                        ancho = this.anchuraPorProducto - 20;
                    } else {
                        ancho = this.anchuraPorProducto - 1;
                    }
                    rectangulo.Height = this.tamañoPorStock * p.Stock;
                    if (rectangulo.Height == 0) rectangulo.Height = 5;

                    // Le adjuntamos su evento
                    rectangulo.Name = p.Nombre;
                    rectangulo.IsTapEnabled = true;
                    rectangulo.Tapped += MyRectangle_Tapped;

                    // Creamos su animacion
                    DoubleAnimation animacion = new DoubleAnimation()
                    {
                        From = 0,
                        To = ancho,
                        Duration = new Duration(TimeSpan.FromSeconds(1.5)),
                        EnableDependentAnimation = true
                    };
                    Storyboard.SetTarget(animacion, rectangulo);
                    Storyboard.SetTargetProperty(animacion, "Rectangle.Width");
                    storyboard.Children.Add(animacion);

                    // Lo añadimos al Canvas y lo posicionamos
                    miCanvas.Children.Add(rectangulo);
                    Canvas.SetLeft(rectangulo, 20 + (cantProductos * this.anchuraPorProducto));
                    Canvas.SetTop(rectangulo, this.alturaMaxima - (100 + rectangulo.Height + this.GrosorEjes));

                    // Ponemos el nombre del producto encima de la barra
                    TextBlock nombre = new TextBlock();
                    int count = 1;
                    foreach (char c in p.Nombre) {
                        if (c == ' ') count++;
                    }
                    nombre.Text = p.Nombre.Replace(" ", "\n");

                    miCanvas.Children.Add(nombre);
                    Canvas.SetLeft(nombre, Canvas.GetLeft(rectangulo));
                    Canvas.SetTop(nombre, Canvas.GetTop(rectangulo) - (count * 20));

                    cantProductos++;
                }
                storyboard.Begin();
            }
        }
    }
}
