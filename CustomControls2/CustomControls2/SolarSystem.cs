using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace CustomControls2
{
    public sealed class SolarSystem : Control
    {
        // Atributos
        Canvas _canvas;
        private int radioSol = 75;

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

            _canvas = (Canvas)GetTemplateChild("canvasTemplate");
            if (_canvas != null)
            {
                crearPlanetas();
            }
        }

        private void crearPlanetas()
        {
            if (ItemsSource != null)
            {
                _canvas.Children.Clear();
                foreach (var item in ItemsSource)
                {
                    var element = new Ellipse();
                    //element.Fill = item.Color;
                    element.Fill = new ImageBrush() { ImageSource = new BitmapImage() { UriSource = new Uri("ms-appx:///"+item.Imagen) } };
                    element.Width = element.Height = item.Diametro;
                    _canvas.Children.Add(element);
                    Canvas.SetLeft(element, (radioSol * -1) + (item.DistanciaSol * -1));
                    Canvas.SetTop(element, (item.Diametro/2) * -1);
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
