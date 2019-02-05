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
    /// <summary>
    /// Clase GraficaBarras
    /// Componente gráfico que muestra un gráfico de barras con una serie de productos, 
    /// de lo cuales se puede ver su cantidad de stock y si hay poca/mucha cantidad en almacén.
    /// </summary>
    /// <see cref="Producto"/>
    public sealed class GraficaBarras : Control
    {
        // Atributos
        /// <summary>
        /// Atributo longitudMaxima
        /// Define la longitud máxima que puede adquirir el componente dependiendo del espacio que tenga disponible.
        /// </summary>
        /// <value>double</value>
        private double longitudMaxima;

        /// <summary>
        /// Atributo alturaMaxima
        /// Define la altura máxima que puede adquirir el componente dependiendo del espacio que tenga disponible.
        /// </summary>
        /// <value>double</value>
        private double alturaMaxima;

        /// <summary>
        /// Atributo ejeX
        /// Hace referencia a la línea que representa el eje X en el gráfico.
        /// </summary>
        /// <value>Line</value>
        private Line ejeX;

        /// <summary>
        /// Atributo ejeY
        /// Hace referencia a la línea que representa el eje Y en el gráfico.
        /// </summary>
        /// <value>Line</value>
        private Line ejeY;

        /// <summary>
        /// Atributo miCanvas
        /// Hace referencia al canvas que sobre el que se dibujará el gráfico.
        /// </summary>
        /// <value>Canvas</value>
        Canvas miCanvas;

        /// <summary>
        /// Atributo maxStock
        /// Contiene el valor de stock actual más alto de entre todos los productos, 
        /// para establecer esa cantidad más grande el punto límite en el gráfico y
        /// el resto dividir en base a ella.
        /// </summary>
        /// <value>int</value>
        private int maxStock = 0;

        /// <summary>
        /// Atributo tamañoPorStock
        /// Utilizando las variables <see cref="maxStock"/> y <seealso cref="alturaMaxima"/>, 
        /// se establece por cada unidad de stock
        /// cuanta altura debe tener una barra dentro del gráfico.
        /// </summary>
        /// <value>double</value>
        private double tamañoPorStock;

        /// <summary>
        /// Atributo anchuraPorProducto
        /// Utilizando la variable <see cref="longitudMaxima"/> y la cantidad de poductos que forma el gráfico, 
        /// se establece por cada producto cuanta anchura debe tener dentro del gráfico.
        /// </summary>
        /// <value>double</value>
        private double anchuraPorProducto;

        /// <summary>
        /// Propiedad dependiente ColorEjes
        /// Define el color que van a tomar los ejes del gráfico.
        /// Por defecto en negro.
        /// </summary>
        /// <value>SolidColorBrush</value>
        public SolidColorBrush ColorEjes
        {
            get { return (SolidColorBrush)GetValue(ColorEjesProperty); }
            set { SetValue(ColorEjesProperty, value); }
        }
        public static readonly DependencyProperty ColorEjesProperty =
            DependencyProperty.Register("ColorEjes", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        /// <summary>
        /// Propiedad dependiente GrosorEjes
        /// Define el grosor que van a tomar los ejes del gráfico.
        /// Por defecto en 2.
        /// </summary>
        /// <value>int</value>
        public int GrosorEjes
        {
            get { return (int)GetValue(GrosorEjesProperty); }
            set { SetValue(GrosorEjesProperty, value); }
        }
        public static readonly DependencyProperty GrosorEjesProperty =
            DependencyProperty.Register("GrosorEjes", typeof(int), typeof(GraficaBarras), new PropertyMetadata(null));

        /// <summary>
        /// Propiedad dependiente ColorHayStock
        /// Define el color de los productos que tengan el suficiente stock en almacen.
        /// Por defecto en verde.
        /// </summary>
        /// <value>SolidColorBrush</value>
        public SolidColorBrush ColorHayStock
        {
            get { return (SolidColorBrush)GetValue(ColorHayStockProperty); }
            set { SetValue(ColorHayStockProperty, value); }
        }
        public static readonly DependencyProperty ColorHayStockProperty =
            DependencyProperty.Register("ColorHayStock", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        /// <summary>
        /// Propiedad dependiente ColorFaltaStock
        /// Define el color de los productos que no tengan el stock requerido en almacen.
        /// Por defecto en rojo.
        /// </summary>
        /// <value>SolidColorBrush</value>
        public SolidColorBrush ColorFaltaStock
        {
            get { return (SolidColorBrush)GetValue(ColorFaltaStockProperty); }
            set { SetValue(ColorFaltaStockProperty, value); }
        }
        public static readonly DependencyProperty ColorFaltaStockProperty =
            DependencyProperty.Register("ColorFaltaStock", typeof(SolidColorBrush), typeof(GraficaBarras), new PropertyMetadata(null));

        /// <summary>
        /// Propiedad dependiente ItemsSource
        /// Contiene una lista con los productos que se van a mostrar en el gráfico.
        /// </summary>
        /// <value>ObservableCollection<see cref="Producto"/></value>
        public ObservableCollection<Producto> ItemsSource
        {
            get { return (ObservableCollection<Producto>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<Producto>), typeof(GraficaBarras), new PropertyMetadata(null));



        /// <summary>
        /// Constructor por defecto (y único) de la clase <see cref="GraficaBarras"/>.
        /// Establece todos los valores por defecto del componente (gráfico).
        /// </summary>
        public GraficaBarras()
        {
            // Se establecen los valores por defecto
            this.DefaultStyleKey = typeof(GraficaBarras);
            this.GrosorEjes = 2;
            this.ColorEjes = new SolidColorBrush(Colors.Black);
            this.ColorHayStock = new SolidColorBrush(Colors.LawnGreen);
            this.ColorFaltaStock = new SolidColorBrush(Colors.Red);
        }
        
        /// <summary>
        /// Evento CollectionChanged asociado a la propiedad dependiente <see cref="ItemsSource"/>
        /// que, al añadirse o cambiarse algun objeto de la lista, se repinta el gráfico 
        /// recalculando las dimensiones del mismo.
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void ItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.generarLetreros();
            this.establecerDimensiones();
            this.crearBarras();
        }

        // Metodo OnApplyTemplate
        /// <summary>
        /// Método OnApplyTemplate
        /// Es llamado cuando se aplica/crea la plantilla gráfica del componente, aquí es donde
        /// se establecen las dimensiones y se pinta el gráfico.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Le asignamos el evento al ItemsSource
            this.ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
            // Establecemos como longitud y altura maximas el tamaño del grid
            // Si la altura es menor de 300, se pone en 300, menos no se veria nada
            if (this.Height >= 300) {
                this.alturaMaxima = this.Height - 115;
            } else {
                this.Height = 300;
                this.alturaMaxima = this.Height - 115;
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

        /// <summary>
        /// Método generarEjes
        /// Genera los ejes X e Y dl gráfico y los posiciona dentro del Canvas <see cref="miCanvas"/>.
        /// </summary>
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

        /// <summary>
        /// Método generarLetreros
        /// Genera 2 letreros de tipo <see cref="TextBlock"/> con el contenido Productos y Stock,
        /// y se posiciona al lado de los ejes X e Y respectivamente
        /// </summary>
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

        /// <summary>
        /// Método establecerDimensiones
        /// Calcula todas las dimensiones máximas dentro del gráfico para poder adaptar
        /// todos los productos a ellas.
        /// </summary>
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

        /// <summary>
        /// Evento Tapped asociado a todos los Rectángulos (barras del gráfico).
        /// Rellena los campos con los datos del producto seleccionado.
        /// </summary>
        /// <param name="sender">Objeto con el que se ha lanzado el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void MyRectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rectangulo = sender as Rectangle;
            TextBlock nombre = (TextBlock)GetTemplateChild("nombre");
            TextBlock stock = (TextBlock)GetTemplateChild("stock");
            TextBlock stockMin = (TextBlock)GetTemplateChild("stockminimo");
            TextBlock pcompra = (TextBlock)GetTemplateChild("pcompra");
            TextBlock pventa = (TextBlock)GetTemplateChild("pventa");
            Image imagen = (Image)GetTemplateChild("imagen");
            

            foreach (Producto p in this.ItemsSource) {
                if (p.Nombre.Equals(rectangulo.Name)) {
                    imagen.Source = new BitmapImage(new Uri("ms-appx:///" + p.Imagen));
                    nombre.Text = p.Nombre;
                    stock.Text = p.Stock.ToString() + " unidades restantes";
                    stockMin.Text = p.StockMinimo.ToString() + " unidades requeridas";
                    pcompra.Text = "Compra: " + p.PrecioCompra.ToString() + "€";
                    pventa.Text = "Venta: " + p.PrecioVenta.ToString() + "€";
                }
            }
        }

        /// <summary>
        /// Método crearBarras
        /// Genera tantos rectángulos como productos haya en el <see cref="ItemsSource"/>, y los dimensiona
        /// y posiciona según los datos obtenidos en <seealso cref="establecerDimensiones"/>.
        /// Además, les establece una animación que los hace "pintarse" de izquierda a derecha.
        /// </summary>
        private void crearBarras()
        {
            if (this.ItemsSource != null) {
                // Recorre todos los productos recibidos por el ItemsSources
                int cantProductos = 0;
                Storyboard storyboard = new Storyboard();
                foreach (Producto p in this.ItemsSource) {
                    // Por cada uno de ellos crea un rectangulo
                    var rectangulo = new Rectangle();

                    // Dependiendo del stock minimo y el actual se pinta de un color u otro
                    if (p.Stock >= p.StockMinimo) {
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
