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
                new Planeta() {Nombre = "Mercurio", Diametro = 16, DistanciaSol = 140, Imagen =  "Assets/mercurio.jpg"},
                new Planeta() {Nombre = "Venus", Diametro = 30, DistanciaSol = 190, Imagen =  "Assets/venus.jpg"},
                new Planeta() {Nombre = "Tierra", Diametro = 36, DistanciaSol = 240, Imagen =  "Assets/tierra.png"},
                new Planeta() {Nombre = "Marte", Diametro = 20, DistanciaSol = 290, Imagen =  "Assets/marte.jpg"},
                new Planeta() {Nombre = "Jupiter", Diametro = 90, DistanciaSol = 390, Imagen =  "Assets/jupiter.jpg"},
                new Planeta() {Nombre = "Saturno", Diametro = 80, DistanciaSol = 490, Imagen =  "Assets/saturno.jpg"},
                new Planeta() {Nombre = "Urano", Diametro = 46, DistanciaSol = 570, Imagen =  "Assets/urano.jpg"},
                new Planeta() {Nombre = "Neptuno", Diametro = 46, DistanciaSol = 660, Imagen =  "Assets/neptuno.jpg"}
                
            };
        }
    }
}
