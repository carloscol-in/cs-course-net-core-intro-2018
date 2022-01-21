using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        private Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _objetosEscuela;

        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dictObjsEscuela)
        {
            if (dictObjsEscuela == null)
                throw new ArgumentNullException(nameof(dictObjsEscuela));

            _objetosEscuela = dictObjsEscuela;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            IEnumerable<ObjetoEscuelaBase> lista;

            lista = _objetosEscuela.GetValueOrDefault(LlaveDiccionario.EVALUACIONES, new List<Evaluacion>());

            return lista.Cast<Evaluacion>();
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> evaluaciones)
        {
            evaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in evaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out IEnumerable<Evaluacion> dummy);
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDiccionarioEvaluacionesPorAsignatura()
        {
            Dictionary<string, IEnumerable<Evaluacion>> evaluaciones = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsignaturas = GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones);

            foreach (var asignatura in listaAsignaturas)
            {
                var evals = from ev in listaEvaluaciones
                            where ev.Asignatura.Nombre == asignatura
                            select ev;
                evaluaciones.Add(asignatura, evals);
            }

            return evaluaciones;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetAlumnosPromedioPorAsignatura()
        {
            var respuesta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();

            var listaEvaluacionesPorAsignatura = GetDiccionarioEvaluacionesPorAsignatura();

            foreach (var asignatura in listaEvaluacionesPorAsignatura)
            {
                var promediosAlumnos = from eval in asignatura.Value
                                       group eval by new
                                       {
                                           eval.Alumno.UniqueId,
                                           eval.Alumno.Nombre
                                       }
                                       into grupoEvaluacionesAlumno
                                       select new AlumnoPromedio
                                       {
                                           alumnoId = grupoEvaluacionesAlumno.Key.UniqueId,
                                           alumnoNombre = grupoEvaluacionesAlumno.Key.Nombre,
                                           promedio = grupoEvaluacionesAlumno.Average((evaluacion) => evaluacion.Nota),
                                       };

                respuesta.Add(asignatura.Key, promediosAlumnos);
            }

            return respuesta;
        }
    }
}