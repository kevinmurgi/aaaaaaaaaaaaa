using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/*
 * Clase Libro que solamente define la estructura de los objetos de tipo Libro
 * Implementa INotifyPropertyChanged
 */
namespace ProyectoXamarin.DataModel
{
    public class Libro : INotifyPropertyChanged
    {
        // Evento y metodo para el PropertyChaged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Atributos de la clase
        private string _nombre;
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                if (!value.Equals(this._nombre))
                {
                    this._nombre = value;
                    OnPropertyChanged("Nombre");
                }
            }
        }

        private string _autorLibro;
        public string AutorLibro
        {
            get
            {
                return this._autorLibro;
            }
            set
            {
                if (!value.Equals(this._autorLibro))
                {
                    this._autorLibro = value;
                    OnPropertyChanged("AutorLibro");
                }
            }
        }

        private string _lanzamiento;
        public string Lanzamiento
        {
            get
            {
                {

                }
                return this._lanzamiento;
            }
            set
            {
                if (!value.Equals(this._lanzamiento))
                {
                    this._lanzamiento = value;
                    OnPropertyChanged("Lanzamiento");
                }
            }
        }

        private int _paginas;
        public int Paginas
        {
            get
            {
                return this._paginas;
            }
            set
            {
                if (value != this._paginas)
                {
                    this._paginas = value;
                    OnPropertyChanged("Paginas");
                }
            }
        }

        private string _genero;
        public string Genero
        {
            get
            {
                return this._genero;
            }
            set
            {
                if (!value.Equals(this._genero))
                {
                    this._genero = value;
                    OnPropertyChanged("Genero");
                }
            }
        }

        // Constructores
        public Libro() { }
        public Libro(string nombre, string autor, string lanzamiento, int paginas, string genero)
        {
            this.Nombre = nombre;
            this.AutorLibro = autor;
            this.Lanzamiento = lanzamiento;
            this.Paginas = paginas;
            this.Genero = genero;
        }
    }
}
