using System.Collections.Generic;
using System.Linq;
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
            var listaObjs = engine.GetObjetosEscuela();

            var listaILugar = from obj in listaObjs
                              where obj is ILugar
                              select (ILugar)obj;
            // engine.Escuela.LimpiarLugar();
            Dictionary<int, string> diccionario = new Dictionary<int, string>();


            diccionario.Add(23, "Lorem Ipsum");
            diccionario.Add(11, "Dolor Sit");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key}, Value: {keyValPair.Value}");
            }

            WriteLine("Acceso a diccionario");
            WriteLine(diccionario[23]);

            var dict = engine.GetDiccionarioObjetos();
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
