using System;

using System.Windows;
using Microsoft.Win32;

using System.Linq;

using NAudio.Wave;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double amplitudMaxima = 1;
        Señal señal;
        Señal señalResultado;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {

            AudioFileReader reader =
                new AudioFileReader(txtRutaArchivo.Text);

            double tiempoInicial = 0;
            double tiempoFinal =
                reader.TotalTime.TotalSeconds;
            double frecuenciaMuestreo =
                reader.WaveFormat.SampleRate;

            txtFrecuenciaMuestreo.Text =
                frecuenciaMuestreo.ToString();
            txtTiempoInicial.Text = "0";
            txtTiempoFinal.Text =
                tiempoFinal.ToString();

            señal = new SeñalPersonalizada();
  
            //---------------------------------PRIMERA SEÑAL------------------------------------------------------//
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;


            //Construir nuestra señal a traves del
            //archivo de audio

            var bufferLectura =
                new float[reader.WaveFormat.Channels];
            int muestrasLeidas = 1;
            double instanteActual = 0;
            double intervaloMuestra = 1.0 / frecuenciaMuestreo; 
            do
            {
                muestrasLeidas =
                    reader.Read(bufferLectura,
                    0, reader.WaveFormat.Channels);
                if (muestrasLeidas > 0)
                {
                    double max =
                        bufferLectura.
                        Take(muestrasLeidas).Max();
                    señal.Muestras.Add(
                        new Muestra(instanteActual, max));
                }
                instanteActual +=
                    intervaloMuestra;
            } while (muestrasLeidas > 0);

            
            
          

            
            
            señal.actualizarAmplitudMaxima();
            
            amplitudMaxima = señal.AmplitudMaxima;
           
            plnGrafica.Points.Clear();
            
            lblAmplitudMaximaY.Text = amplitudMaxima.ToString("F");
            lblAmplitudMaximaNegativaY.Text = "-" + amplitudMaxima.ToString("F");

            //PRIMERA SEÑAL
            if (señal != null)
            {
                //Recorre todos los elementos de una coleccion o arreglo
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, (muestra.Y /
                        amplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + 
                        (scrContenedor.Height / 2)));

                }
                
            }
            
            plnEjeX.Points.Clear();
            //Punto del principio
            plnEjeX.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del final
            plnEjeX.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, 
                (scrContenedor.Height / 2)));

            plnEjeY.Points.Clear();
            //Punto del principio
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width , (señal.AmplitudMaxima * 
                ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //Punto del final
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width, (-señal.AmplitudMaxima * 
                ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
                        
        }
        
       

        
        private void btnTransformadaFourier_Click(object sender, RoutedEventArgs e)
        {
           
            Señal transformada = Señal.transformar(señal);
            transformada.actualizarAmplitudMaxima();

            plnGraficaResultado.Points.Clear();

            lblAmplitudMaximaY_Resultado.Text = transformada.AmplitudMaxima.ToString("F");
            lblAmplitudMaximaNegativaY_Resultado.Text = "-" + transformada.AmplitudMaxima.ToString("F");

            //PRIMERA SEÑAL
            if (transformada != null)
            {
                //Recorre todos los elementos de una coleccion o arreglo
                foreach (Muestra muestra in transformada.Muestras)
                {
                    plnGraficaResultado.Points.Add(new Point((muestra.X - transformada.TiempoInicial) * scrContenedor_Resultado.Width, (muestra.Y /
                        transformada.AmplitudMaxima * ((scrContenedor_Resultado.Height / 2.0) - 30) * -1) +
                        (scrContenedor_Resultado.Height / 2)));

                }
                double valorMaximo = 0;
                int indiceMaximo = 0;
                int indiceActual = 0;

                foreach(Muestra muestra in transformada.Muestras)
                {
                    if (muestra.Y > valorMaximo)
                        {
                        valorMaximo = muestra.Y;
                        indiceMaximo = indiceActual;
                    }
                    indiceActual++;
                    if (indiceActual > (double)transformada.Muestras.Count / 2.0)
                    {
                        break;
                    }
                }
                double frecuenciaFundamental =
                    (double)indiceMaximo * señal.FrecuenciaMuestreo
                        / (double)transformada.Muestras.Count;

                lblFrecuenciaFundamental.Text =
                    frecuenciaFundamental.ToString("f") + " Hz";

            }

            

            plnEjeXResultado.Points.Clear();
            //Punto del principio
            plnEjeXResultado.Points.Add(new Point(0, (scrContenedor_Resultado.Height / 2)));
            //Punto del final
            plnEjeXResultado.Points.Add(new Point((transformada.TiempoFinal - transformada.TiempoInicial) * scrContenedor_Resultado.Width,
                (scrContenedor_Resultado.Height / 2)));

            plnEjeYResultado.Points.Clear();
            //Punto del principio
            plnEjeYResultado.Points.Add(new Point((0 - transformada.TiempoInicial) * scrContenedor_Resultado.Width, (transformada.AmplitudMaxima *
                ((scrContenedor_Resultado.Height / 2.0) - 30) * -1) + (scrContenedor_Resultado.Height / 2)));
            //Punto del final
            plnEjeYResultado.Points.Add(new Point((0 - transformada.TiempoInicial) * scrContenedor_Resultado.Width, (-transformada.AmplitudMaxima *
                ((scrContenedor_Resultado.Height / 2.0) - 30) * -1) + (scrContenedor_Resultado.Height / 2)));

       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog =
                new OpenFileDialog();

            if((bool)fileDialog.ShowDialog())
            {
                txtRutaArchivo.Text =
                    fileDialog.FileName;
            }

        }
    }

}
