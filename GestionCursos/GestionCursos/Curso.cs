using System;
using System.Collections.Generic;

namespace GestionCursos
{
    
    public class Curso
    {
       
        private string codigo;
        private string nombre;
        private int creditos;
        private int cupoMaximo;
        private List<Inscripcion> inscripciones;

       
        public Curso(string codigo, string nombre, int creditos, int cupoMaximo)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.creditos = creditos;
            this.cupoMaximo = cupoMaximo;
            this.inscripciones = new List<Inscripcion>();
        }

       
        public string Codigo
        {
            get { return codigo; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public int Creditos
        {
            get { return creditos; }
        }

        public int CupoMaximo
        {
            get { return cupoMaximo; }
        }

        public int CantidadInscritos
        {
            get { return inscripciones.Count; }
        }

        public int CuposDisponibles
        {
            get { return cupoMaximo - inscripciones.Count; }
        }

        public List<Inscripcion> Inscripciones
        {
            get { return inscripciones; }
        }

        public void AgregarInscripcion(Inscripcion inscripcion)
        {
            inscripciones.Add(inscripcion);
        }

        public void EliminarInscripcion(Inscripcion inscripcion)
        {
            inscripciones.Remove(inscripcion);
        }

        public override string ToString()
        {
            return $"Código: {codigo} | Curso: {nombre} | Créditos: {creditos} | Inscritos: {CantidadInscritos}/{cupoMaximo}";
        }
    }
}