using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
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

namespace CustomControls2
{
    public sealed class SolarSystem : Control
    {
        // Atributos
        Canvas _canvas;
        private double distanciaMaxItems = 0;
        private double distanciaPorPixel = 0;
        private double diametroMaxItems = 0;
        private double diametroMaxPorPixel = 0;

        // Constructor
        public SolarSystem()
        {
            this.DefaultStyleKey = typeof(SolarSystem);
        }

        /*
         * Metodo OnApplyTemplate, para pintar dentro del Canvas
         */
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.MaxItemSize == 0)
            {
                this.MaxItemSize = 100;
            }
            if (this.MinItemSize == 0)
            {
                this.MinItemSize = 10;
            }

            _canvas = (Canvas)GetTemplateChild("canvasTemplate");
            Ellipse sol = (Ellipse)GetTemplateChild("sol");
            sol.Width = sol.Height = this.MaxItemSize;

            if (_canvas != null)
            {
                getTamaños();
                crearPlanetas();
            }
        }

        private void getTamaños()
        {
            // Recorre todos los planetas
            if (ItemsSource != null)
            {
                foreach (var item in ItemsSource)
                {
                    // Guarda siempre la DistanciaSol mas grande
                    if (item.DistanciaSol > this.distanciaMaxItems)
                    {
                        this.distanciaMaxItems = item.DistanciaSol;
                    }
                    // Guarda siempre el Diametro mas grande
                    if (item.Diametro > this.diametroMaxItems)
                    {
                        this.diametroMaxItems = item.Diametro;
                    }
                }
                // Por ultimo realiza la relacion entre la distancia mas grande y el tamaño del Layout donde se encuentra el objeto
                this.distanciaPorPixel = (this.Width / 2) / this.distanciaMaxItems;
                // Por ultimo realiza la relacion entre el diametro mas grande y el tamaño maximo del diametro
                this.diametroMaxPorPixel = this.MaxItemSize / this.diametroMaxItems;
            }
        }

        private void crearPlanetas()
        {
            if (ItemsSource != null)
            {
                _canvas.Children.Clear();
                // Por cada planeta
                foreach (var item in ItemsSource)
                {
                    // Crea una eclipse
                    var element = new Ellipse();
                    // Le pone como fondo su imagen
                    element.Fill = new ImageBrush() { ImageSource = new BitmapImage() { UriSource = new Uri("ms-appx:///"+item.Imagen) } };
                    // Le establece su tamaño
                    element.Width = element.Height = (item.Diametro * this.diametroMaxPorPixel);
                    if (element.Width < this.MinItemSize)
                    {
                        element.Width = element.Height = this.MinItemSize;
                    }
                    // Lo añade al canvas y establece su posicion
                    _canvas.Children.Add(element);
                    // Posicion = DistanciaSol * la relacion de pixel con la distancia - el radio del planeta
                    // (por defecto el canvas esta en el centro)
                    Canvas.SetLeft(element, -1*((item.DistanciaSol * this.distanciaPorPixel) - (item.Diametro/2)));
                    Canvas.SetTop(element, (element.Width/2) * -1);

                    /*
                    var title = new TextBlock();
                    title.Text = item.Nombre;
                    title.HorizontalAlignment = HorizontalAlignment.Center;
                    _canvas.Children.Add(title);
                    Canvas.SetLeft(title, (radioSol * -1) + (item.DistanciaSol * -1));
                    Canvas.SetTop(title, ((item.Diametro/2) + 20) * -1);
                    */
                }
            }
        }

        /*
         * ItemsSource
         */
        public List<Planeta> ItemsSource
        {
            get { return (List<Planeta>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<Planeta>), typeof(SolarSystem), new PropertyMetadata(null));

        /*
         * Max Item Size
         */
        public double MaxItemSize
        {
            get { return (double)GetValue(MaxItemSizeProperty); }
            set { SetValue(MaxItemSizeProperty, value); }
        }
        public static readonly DependencyProperty MaxItemSizeProperty =
            DependencyProperty.Register("MaxItemSize", typeof(double), typeof(SolarSystem), new PropertyMetadata(null));

        /*
         * Min Item Size
         */
        public double MinItemSize
        {
            get { return (double)GetValue(MinItemSizeProperty); }
            set { SetValue(MinItemSizeProperty, value); }
        }
        public static readonly DependencyProperty MinItemSizeProperty =
            DependencyProperty.Register("MinItemSize", typeof(double), typeof(SolarSystem), new PropertyMetadata(null));
    }
}
