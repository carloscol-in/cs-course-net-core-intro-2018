using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var asignaturasList = reporteador.GetListaAsignaturas();
            var dictEvaluaciones = reporteador.GetDiccionarioEvaluacionesPorAsignatura();
            var promediosPorAsignatura = reporteador.GetAlumnosPromedioPorAsignatura();

            Printer.WriteTitle("Captura de una evaluacion por Consola");
            var eval = new Evaluacion();
            string nombre;
            float nota;

            WriteLine("Ingrese el nombre de la evaluacion:");
            nombre = ReadLine();

            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("El valor del nombre no puede ser nulo.");

            WriteLine("Ingrese la nota de la evaluacion:");
            bool obtuvoNota = float.TryParse(ReadLine(), out nota);

            if (!obtuvoNota)
                throw new ArgumentException("El argumento de la nota fue incorrecto.");

            eval.Nombre = nombre;
            eval.Nota = nota;
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            WriteLine("SALIENDO");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
