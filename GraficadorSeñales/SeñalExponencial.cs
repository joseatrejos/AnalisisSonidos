using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalExponencial : Señal
    {
        public SeñalExponencial()
        {
            Alpha = 1.0;
            Umbral = 0.0;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        public SeñalExponencial(double alpha, double umbral)
        {
            Alpha = alpha;
            Umbral = umbral;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Math.Exp(Alpha * tiempo);

            return resultado;
        }
    }

}
