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

        /// <summary>
        /// Constructor de la clase equipo, se crean los jugadores y se conectan los manejadores y los eventos.
        /// </summary>
        /// <param name="nj"></param>
        /// <param name="nom"></param>
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
        /// <summary>
        /// Mueve los jugadores y comprueba que la partida pueda continuar, es decir,el numero de jugadores no es menor al minimo.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Llama al metodo mover jugadores mientras devuelva true
        /// </summary>
        public void moverJugadoresEnBucle()
        {
            while (moverJugadores() == true)
            {
            }
            
        }
        /// <summary>
        /// Suma los puntos de todos los jugadores de un equipo
        /// </summary>
        /// <returns></returns>
        public int sumarPuntos()
        {
            int suma = 0;
            for (int i = 0; i < jugadores.Count(); i++)
            {
                suma += jugadores[i].puntos;
            }
            return suma;
        }
        /// <summary>
        /// Devuelve la lista con los jugadores cuya propiedad amonestaciones supere el máximo
        /// </summary>
        /// <returns></returns>
        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return jugadoresExpulsados;
        }
        /// <summary>
        /// Devuelve la lista con los jugadores cuya propiedad propiedades supere el máximo
        /// </summary>
        /// <returns></returns>
        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return jugadoresLesionados;
        }
        /// <summary>
        /// Devuelve la lista con los jugadores cuya propiedad energia este por debajo del mínimo.
        /// </summary>
        /// <returns></returns>
        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return jugadoresCansados;
        }

        /// <summary>
        /// Muestra la informacion de cada equipo y de sus respectivos jugadores.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Manejador del evento AmonestacionesMaximoExcedido,muestra la informacion del jugador que provoca el evento y lo añade a la lista.
        /// </summary>
        /// <param name="slender"></param>
        /// <param name="e"></param>
        private void cuandoAmonestacionesMaximoExcedido(object slender,Jugador.AmonestacionesMaximoExcedidoArgs e)
        {
            Jugador jugador = (Hada.Jugador)slender;

            if (jugadoresExpulsados.Contains(jugador) == false)
            {
                jugadoresExpulsados.Add(jugador);

                Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
                Console.WriteLine("Jugador: " + jugador.nombre);
                Console.WriteLine("Equipo: " + nombreEquipo);
                Console.WriteLine("Amonestaciones: " + e.amonestaciones);
            }

        }
        /// <summary>
        /// anejador del evento FaltasMaximoExcedido,muestra la informacion del jugador que provoca el evento y lo añade a la lista.
        /// </summary>
        /// <param name="slender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// anejador del evento EnergiaMinimaExcedida,muestra la informacion del jugador que provoca el evento y lo añade a la lista.
        /// </summary>
        /// <param name="slender"></param>
        /// <param name="e"></param>
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
