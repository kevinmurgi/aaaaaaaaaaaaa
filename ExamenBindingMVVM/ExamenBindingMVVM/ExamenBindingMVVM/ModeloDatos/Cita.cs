using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ExamenBindingMVVM.ModeloDatos
{
    public class Cita : INotifyPropertyChanged
    {
        // Evento PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Atirbutos
        private string _nombre;
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                if (this._nombre != value)
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
                if (this._apellidos != value)
                {
                    this._apellidos = value;
                    OnPropertyChanged("Apellidos");
                }
            }
        }

        private string _fechaCita;
        [PrimaryKey]
        public string FechaCita
        {
            get
            {
                return this._fechaCita;
            }
            set
            {
                if (this._fechaCita != value)
                {
                    this._fechaCita = value;
                    OnPropertyChanged("FechaCita");
                }
            }
        }

        private string _horaCita;
        [PrimaryKey]
        public string HoraCita
        {
            get
            {
                return this._horaCita;
            }
            set
            {
                if (this._horaCita != value)
                {
                    this._horaCita = value;
                    OnPropertyChanged("HoraCita");
                }
            }
        }

        private string _motivoCita;
        public string MotivoCita
        {
            get
            {
                return this._motivoCita;
            }
            set
            {
                if (this._motivoCita != value)
                {
                    this._motivoCita = value;
                    OnPropertyChanged("MotivoCita");
                }
            }
        }

        // Consturctores
        public Cita() { }
        public Cita(string nombre, string apellidos, string fecha, string hora, string motivo)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.FechaCita = fecha;
            this.HoraCita = hora;
            this.MotivoCita = motivo;
        }
    }
}
