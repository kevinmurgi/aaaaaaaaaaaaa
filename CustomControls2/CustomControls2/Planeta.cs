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
        public string Imagen { get; set; }
    }

    public class GestorPlanetas
    {
        public static List<Planeta> ObtenerPlanetas()
        {
            return new List<Planeta>
            {
                new Planeta() {Nombre = "Mercurio", Diametro = 5, DistanciaSol = 100, Imagen =  "Assets/mercurio.jpg"},
                new Planeta() {Nombre = "Venus", Diametro = 30, DistanciaSol = 150, Imagen =  "Assets/venus.jpg"},
                new Planeta() {Nombre = "Tierra", Diametro = 36, DistanciaSol = 200, Imagen =  "Assets/tierra.png"},
                new Planeta() {Nombre = "Marte", Diametro = 20, DistanciaSol = 250, Imagen =  "Assets/marte.jpg"},
                new Planeta() {Nombre = "Jupiter", Diametro = 90, DistanciaSol = 400, Imagen =  "Assets/jupiter.jpg"},
                new Planeta() {Nombre = "Saturno", Diametro = 80, DistanciaSol = 480, Imagen =  "Assets/saturno.jpg"},
                new Planeta() {Nombre = "Urano", Diametro = 46, DistanciaSol = 540, Imagen =  "Assets/urano.jpg"},
                new Planeta() {Nombre = "Neptuno", Diametro = 46, DistanciaSol = 600, Imagen =  "Assets/neptuno.jpg"}
                
            };
        }
    }
}
