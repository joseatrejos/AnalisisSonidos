﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Muestra
    {
        //El instante del tiempo en que fue tomada la muestra
        public double X { get; set; }
        //El valor de esa muestra en ese instante
        public double Y { get; set; }

        //constructor que inicializa valores
        public Muestra(double x, double y)
        {
            X = x;
            Y = y;

        }

        //Constructor si parametros
        public Muestra()
        {
            X = 0.0;
            Y = 0.0;
        }

    }

}
