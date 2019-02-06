using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalSenoidal : Señal
    {
        //Exclusivas de la señal senoidal
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }

        public SeñalSenoidal()
        {
            Amplitud = 1.0;
            Fase = 0.0;
            Frecuencia = 1.0;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;

            Umbral = 0.0;

        }

        public SeñalSenoidal(double amplitud, double fase, double frecuencia, double umbral)
        {
            Amplitud = amplitud;
            Fase = fase;
            Frecuencia = frecuencia;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;

            Umbral = Umbral;

        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Amplitud * Math.Sin((2 * Math.PI * Frecuencia * tiempo) + Fase);

            return resultado;
        }

    }
}
