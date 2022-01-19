using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            ImpimirCursosEscuela(engine.Escuela);

            Printer.DrawLine(20);
            Printer.WriteTitle("Pruebas de Polimorfismo");

            var alumnoTest = new Alumno()
            {
                Nombre = "Claire Underwood",
            };

            ObjetoEscuelaBase ob = alumnoTest;

            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"ID: {alumnoTest.UniqueId}");
            WriteLine($"Evaluaciones: {alumnoTest.Evaluaciones}");
            WriteLine($"Type: {alumnoTest.GetType()}");
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Objeto Escuela: {ob.Nombre}");
            WriteLine($"ID: {ob.UniqueId}");
            WriteLine($"Type: {ob.GetType()}");
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
