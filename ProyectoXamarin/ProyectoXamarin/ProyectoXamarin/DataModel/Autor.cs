using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoXamarin.DataModel
{
    public class Autor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Atributos
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

        private string _apellidos;
        public string Apellidos
        {
            get
            {
                return this._apellidos;
            }
            set
            {
                if (!value.Equals(this._apellidos))
                {
                    this._apellidos = value;
                    OnPropertyChanged("Apellidos");
                }
            }
        }

        private string _nacimiento;
        public string Nacimiento
        {
            get
            {
                return this._nacimiento;
            }
            set
            {
                if (!value.Equals(this._nacimiento))
                {
                    this._nacimiento = value;
                    OnPropertyChanged("Nacimiento");
                }
            }
        }

        private string _telefono;
        public string Telefono
        {
            get
            {
                return this._telefono;
            }
            set
            {
                if (!value.Equals(this._telefono))
                {
                    this._telefono = value;
                    OnPropertyChanged("Telefono");
                }
            }
        }

        private string _sexo;
        public string Sexo
        {
            get
            {
                return this._sexo;
            }
            set
            {
                if (!value.Equals(this._sexo))
                {
                    this._sexo = value;
                    OnPropertyChanged("Sexo");
                }
            }
        }

        // Constructores
        public Autor(){}
        public Autor(string nombre, string apellidos, string nacimiento, string telefono, string sexo)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Nacimiento = nacimiento;
            this.Telefono = telefono;
            this.Sexo = sexo;
        }
    }
}
