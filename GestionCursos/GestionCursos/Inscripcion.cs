using System;

namespace GestionCursos
{
    
    public class Inscripcion
    {
        private Estudiante estudiante;
        private Curso curso;
        private DateTime fechaInscripcion;
        private string estado;

        public Inscripcion(Estudiante estudiante, Curso curso)
        {
            this.estudiante = estudiante;
            this.curso = curso;
            this.fechaInscripcion = DateTime.Now;
            this.estado = "Activa";
        }

        public Estudiante Estudiante
        {
            get { return estudiante; }
        }

        public Curso Curso
        {
            get { return curso; }
        }

        public DateTime FechaInscripcion
        {
            get { return fechaInscripcion; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public override string ToString()
        {
            return $"Estudiante: {estudiante.Nombre} ({estudiante.Matricula}) | " +
                   $"Curso: {curso.Nombre} ({curso.Codigo}) | " +
                   $"Fecha: {fechaInscripcion:dd/MM/yyyy} | Estado: {estado}";
        }
    }
}