using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada

{
    class Equipo
    {
        private List<Jugador> jugadores { get; set; }
        private List<Jugador> jugadoresLesionados;
        private List<Jugador> jugadoresExpulsados;
        private List<Jugador> jugadoresCansados;
        public static int minJugadores { get; set; }
        public static int maxNumeroMovimientos { get; set; }
        public int movimientos { get; private set; }
        public string nombreEquipo { get; private set; }


        public Equipo(int nj, string nom)
        {
            jugadores = new List<Jugador>();
            jugadoresExpulsados = new List<Jugador>();
            jugadoresCansados = new List<Jugador>();
            jugadoresLesionados = new List<Jugador>();
            this.nombreEquipo = nom;
            for (int i =0 ; i < nj; i++)
            {
                Jugador new_j = (new Jugador("Jugador_" +(i+1), 0, 0, 50, 0));
                new_j.amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                new_j.energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
                new_j.faltasMaximoExcedido += cuandoFlatasMaximoExcedido;
                jugadores.Add(new_j);
            

            }

        }
        public bool moverJugadores()
        {
            bool mover = false;
            int cont = 0;
            for (int i = 0; i < jugadores.Count(); i++)
            {
                if (jugadores[i].todoOk())
                { 
                    jugadores[i].mover();
                    if (jugadores[i].todoOk())
                    {
                        cont++;
                    }
                }
            }
            if (cont >= minJugadores)
            {
                mover = true;
                movimientos += 1;
            }
            return mover;
        }
        public int sumarPuntos()
        {
            int suma = 0;
            for (int i = 0; i < jugadores.Count(); i++)
            {
                suma += jugadores[i].puntos;
            }
            return suma;
        }
        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return jugadoresExpulsados;
        }
        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return jugadoresLesionados;
        }
        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return jugadoresCansados;
        }

        public override string ToString()
        {
            string str;
            str = "[" + nombreEquipo + "]" + "Puntos: " + sumarPuntos() + "; Expulsados: " + getJugadoresExcedenLimiteAmonestaciones().Count()
                + "; Lesionados: " + getJugadoresExcedenLimiteFaltas().Count() + "; Retirados: " + getJugadoresExcedenMinimoEnergia().Count() ;
            for(int i=0; i < jugadores.Count(); i++)
            {
                str += "\n" +jugadores[i].ToString();
            }
            return str;
        }
        private void cuandoAmonestacionesMaximoExcedido(object slender,Jugador.AmonestacionesMaximoExcedidoArgs e)
        {
            Jugador jugador = (Hada.Jugador)slender;

            if (jugadoresExpulsados.Contains(jugador)==false)
            {
                jugadoresExpulsados.Add(jugador);
            }
            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.WriteLine("Jugador: " + jugador.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Amonestaciones: " + e.amonestaciones);

        }
        private void cuandoFlatasMaximoExcedido(object slender, Jugador.FaltasMaximoExcedidoArgs e)
        {
            Jugador jugador = (Hada.Jugador)slender;

            if (jugadoresLesionados.Contains(jugador)==false)
            {
                jugadoresLesionados.Add(jugador);

                Console.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
                Console.WriteLine("Jugador: " + jugador.nombre);
                Console.WriteLine("Equipo: " + nombreEquipo);
                Console.WriteLine("Faltas:" + e.faltas);
            }

        }
        private void cuandoEnergiaMinimaExcedida(object slender, Jugador.EnergiaMinimaExcedidaArgs e)
        {
            Jugador jugador = (Hada.Jugador)slender;

            if (jugadoresCansados.Contains(jugador)==false)
            {
                jugadoresCansados.Add(jugador);

                Console.WriteLine("¡¡Energia mínima excedida. Jugador retirado!!");
                Console.WriteLine("Jugador: " + jugador.nombre);
                Console.WriteLine("Equipo: " + nombreEquipo);
                Console.WriteLine("Faltas:" + e.energia + "%");
            }
        }


    }
}
