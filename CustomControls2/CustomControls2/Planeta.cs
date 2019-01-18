using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CustomControls2
{
    public class Planeta
    {
        public string Nombre { get; set; }
        public double Diametro { get; set; }
        public double DistanciaSol { get; set; }
        public SolidColorBrush Color { get; set; }
        public string Imagen { get; set; }
    }

    public class GestorPlanetas
    {
        public static List<Planeta> ObtenerPlanetas()
        {
            return new List<Planeta>
            {
                //new Planeta() {Nombre = "Mercurio", Diametro = 5, DistanciaSol = 100, Color = new SolidColorBrush(Colors.Gray)},
                //new Planeta() {Nombre = "Venus", Diametro = 20, DistanciaSol = 150, Color = new SolidColorBrush(Colors.Yellow)},
                //new Planeta() {Nombre = "Tierra", Diametro = 15, DistanciaSol = 200, Color = new SolidColorBrush(Colors.Blue)},
                //new Planeta() {Nombre = "Marte", Diametro = 10, DistanciaSol = 250, Color = new SolidColorBrush(Colors.Red)},
                new Planeta() {Nombre = "Jupiter", Diametro = 90, DistanciaSol = 350, Imagen =  "Assets/jupiter.jpg", Color = new SolidColorBrush(Colors.Brown)},
                new Planeta() {Nombre = "Saturno", Diametro = 80, DistanciaSol = 450, Color = new SolidColorBrush(Colors.Brown), Imagen =  "Assets/saturno.jpg"},
                //new Planeta() {Nombre = "Urano", Diametro = 45, DistanciaSol = 550, Color = new SolidColorBrush(Colors.Cyan)},
                //new Planeta() {Nombre = "Neptuno", Diametro = 45, DistanciaSol = 650, Color = new SolidColorBrush(Colors.Blue)}
                
            };
        }
    }
}
