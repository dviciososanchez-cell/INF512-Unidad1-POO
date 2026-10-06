using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionCursos
{
    
    public class GestorAcademico
    {
        private List<Estudiante> estudiantes;
        private List<Curso> cursos;
        private List<Inscripcion> inscripciones;

        public GestorAcademico()
        {
            estudiantes = new List<Estudiante>();
            cursos = new List<Curso>();
            inscripciones = new List<Inscripcion>();
        }

      

        public bool RegistrarEstudiante(string matricula, string nombre, string carrera, string correo)
        {
            if (estudiantes.Any(e => e.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase)))
                return false;

            estudiantes.Add(new Estudiante(matricula, nombre, carrera, correo));
            return true;
        }

        public Estudiante BuscarEstudiante(string matricula)
        {
            return estudiantes.FirstOrDefault(e => e.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        }

        public List<Estudiante> ListarEstudiantes()
        {
            return new List<Estudiante>(estudiantes);
        }

        public bool EliminarEstudiante(string matricula)
        {
            Estudiante est = BuscarEstudiante(matricula);
            if (est == null) return false;

            List<Inscripcion> aEliminar = inscripciones
                .Where(i => i.Estudiante.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (Inscripcion ins in aEliminar)
            {
                ins.Curso.EliminarInscripcion(ins);
                inscripciones.Remove(ins);
            }

            estudiantes.Remove(est);
            return true;
        }

        public bool ModificarEstudiante(string matricula, string nuevaCarrera, string nuevoCorreo)
        {
            Estudiante est = BuscarEstudiante(matricula);
            if (est == null) return false;

            est.Carrera = nuevaCarrera;
            est.Correo = nuevoCorreo;
            return true;
        }


        public bool RegistrarCurso(string codigo, string nombre, int creditos, int cupoMaximo)
        {
            if (cursos.Any(c => c.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
                return false;

            cursos.Add(new Curso(codigo, nombre, creditos, cupoMaximo));
            return true;
        }

        public Curso BuscarCurso(string codigo)
        {
            return cursos.FirstOrDefault(c => c.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        public List<Curso> ListarCursos()
        {
            return new List<Curso>(cursos);
        }

        public bool EliminarCurso(string codigo)
        {
            Curso cur = BuscarCurso(codigo);
            if (cur == null) return false;

            List<Inscripcion> aEliminar = inscripciones
                .Where(i => i.Curso.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (Inscripcion ins in aEliminar)
                inscripciones.Remove(ins);

            cursos.Remove(cur);
            return true;
        }


        public string InscribirEstudiante(string matricula, string codigoCurso)
        {
            Estudiante est = BuscarEstudiante(matricula);
            if (est == null) return "ERROR: El estudiante no existe.";

            Curso cur = BuscarCurso(codigoCurso);
            if (cur == null) return "ERROR: El curso no existe.";

            if (cur.CuposDisponibles <= 0)
                return "ERROR: El curso no tiene cupos disponibles.";

            bool yaInscrito = inscripciones.Any(i =>
                i.Estudiante.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase) &&
                i.Curso.Codigo.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase) &&
                i.Estado == "Activa");

            if (yaInscrito)
                return "ERROR: El estudiante ya está inscrito en este curso.";

            Inscripcion nueva = new Inscripcion(est, cur);
            inscripciones.Add(nueva);
            cur.AgregarInscripcion(nueva);

            return "OK: Inscripción realizada correctamente.";
        }

        public string RetirarInscripcion(string matricula, string codigoCurso)
        {
            Inscripcion ins = inscripciones.FirstOrDefault(i =>
                i.Estudiante.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase) &&
                i.Curso.Codigo.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase) &&
                i.Estado == "Activa");

            if (ins == null)
                return "ERROR: No existe una inscripción activa con esos datos.";

            ins.Estado = "Retirada";
            ins.Curso.EliminarInscripcion(ins);

            return "OK: Inscripción retirada correctamente.";
        }

        public List<Inscripcion> ConsultarCursosPorEstudiante(string matricula)
        {
            return inscripciones
                .Where(i => i.Estudiante.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase)
                            && i.Estado == "Activa")
                .ToList();
        }

        public List<Inscripcion> ConsultarEstudiantesPorCurso(string codigoCurso)
        {
            return inscripciones
                .Where(i => i.Curso.Codigo.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase)
                            && i.Estado == "Activa")
                .ToList();
        }


        public string GenerarResumen()
        {
            string resumen = "";
            resumen += "       RESUMEN ACADÉMICO \n";
            resumen += $"Total de estudiantes registrados: {estudiantes.Count}\n";
            resumen += $"Total de cursos registrados:     {cursos.Count}\n";
            resumen += $"Total de inscripciones activas:  {inscripciones.Count(i => i.Estado == "Activa")}\n";
            resumen += $"Total de inscripciones retiradas:{inscripciones.Count(i => i.Estado == "Retirada")}\n\n";

            resumen += " Ocupación por curso \n";
            if (cursos.Count == 0)
                resumen += "No hay cursos registrados.\n";
            else
                foreach (Curso c in cursos)
                {
                    decimal porc = c.CupoMaximo > 0 ? ((decimal)c.CantidadInscritos / c.CupoMaximo) * 100 : 0;
                    resumen += $"{c.Codigo} - {c.Nombre}: {c.CantidadInscritos}/{c.CupoMaximo} ({porc:F1}%)\n";
                }

            return resumen;
        }
    }
}