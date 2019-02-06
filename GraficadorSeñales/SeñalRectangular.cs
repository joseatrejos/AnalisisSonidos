using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalRectangular : Señal
    {

        public SeñalRectangular()
        {
            TiempoInicial = 0.0;
            TiempoFinal = 1.0;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
            

        }

        public SeñalRectangular(double tiempoInicial, double tiempoFinal, double umbral)
        {
            TiempoInicial = tiempoInicial;
            TiempoFinal = tiempoFinal;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
            

        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            if (Math.Abs(tiempo) > 0.5)
            {
                resultado = 0;
            }
            else if (Math.Abs(tiempo) < 0.5)
            {
                resultado = 1;
            }
            else
            {
                resultado = 0.5;
            }

            return resultado;
        }

    }
}
