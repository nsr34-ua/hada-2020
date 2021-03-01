using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        private int amonestacion;
        private int falta;
        private int e;
        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        public string nombre { get; private set; }
        public int puntos { get; set; }

        private int amonestaciones
        {
            get { return amonestacion; }
            set
            {
                if (value > maxAmonestaciones)
                {

                }
                else
                {
                    if (value < 0)
                    {
                        amonestacion = 0;
                    }
                    else
                    {
                        amonestacion = value;
                    }
                }
            }

        }
        private int faltas
        {
            get { return falta; }
            set
            {
                if (value > maxFaltas)
                {

                }
                else
                {
                    falta = value;
                }
            }
        }
        private int energia
        {
            get { return e; }
            set
            {
                if (value < minEnergia)
                {

                }
                else
                {
                    if (value < 0)
                    {
                        e = 0;
                    }
                    else
                    {
                        if (value > 100)
                        {
                            e = 100;
                        }
                        else
                        {
                            e = value;
                        }
                    }
                }

            }
        }
        public Jugador(string nombre,int amonestaciones,int faltas,int energia,int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;

        }

    }
}
