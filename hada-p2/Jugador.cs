﻿using System;
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
                if (value > maxAmonestaciones && amonestacionesMaximoExcedido!= null)
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(value));
                }
     
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
        private int faltas
        {
            get { return falta; }
            set
            {
                if (value > maxFaltas && faltasMaximoExcedido!= null)
                {
                    faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(value));
                }
                 falta = value;
                
            }
        }
        private int energia
        {
            get { return e; }
            set
            {
                if (value < minEnergia && energiaMinimaExcedida != null)
                {
                    energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(value));
                }
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
        public Jugador(string nombre,int amonestaciones,int faltas,int energia,int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;

        }
        public void incAmonestaciones()
        {
            amonestaciones = amonestaciones + rand.Next(0, 2 + 1);
        }
        public void incFaltas()
        {
            faltas = faltas + rand.Next(0, 3 + 1);

        }
        public void decEnergia()
        {
            energia = energia - rand.Next(1, 7 + 1);
        }
        public void incPuntos()
        {
            puntos = puntos + rand.Next(0, 3 + 1);
        }
        public bool todoOk()
        {
            bool ok = true;
            if(amonestaciones> maxAmonestaciones || energia<minEnergia || faltas > maxFaltas)
            {
                ok =false;
            }
            return ok;
        }
        public void mover()
        {
            if (todoOk())
            {
                incAmonestaciones();
                incFaltas();
                incPuntos();
                decEnergia();
            }
        }
      
        public override string ToString()
        {
            string str;
            str = "["+nombre +"]"+ " Puntos: " + puntos + "; Amonestaciones: " + amonestaciones + "; Faltas: " + faltas + "; Energia: " + energia + "%; Ok";
            if (todoOk())
            {
                str = str + ":True";
            }
            else
            {
                str = str + ":False";
            }
            return str;
        }
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;

        public class AmonestacionesMaximoExcedidoArgs: EventArgs
        {
             public int amonestaciones { get; set; }
            public AmonestacionesMaximoExcedidoArgs(int amonestacion)
            {
                this.amonestaciones = amonestacion;
            }
        }
        public class FaltasMaximoExcedidoArgs : EventArgs
        {
            public  int faltas { get; set; }
            public FaltasMaximoExcedidoArgs(int falta)
            {
                this.faltas = falta;
            }
        }
        public class EnergiaMinimaExcedidaArgs : EventArgs
        {
           public int energia{ get; set; } 
            public EnergiaMinimaExcedidaArgs(int e)
            {
                this.energia=e;
            }
        }
    }
}
