using System;

namespace GestionCursos
{
   
    public class Estudiante
    {
        
        private string matricula;
        private string nombre;
        private string carrera;
        private string correo;

        
        public Estudiante(string matricula, string nombre, string carrera, string correo)
        {
            this.matricula = matricula;
            this.nombre = nombre;
            this.carrera = carrera;
            this.correo = correo;
        }

       
        public string Matricula
        {
            get { return matricula; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string Carrera
        {
            get { return carrera; }
            set { carrera = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public override string ToString()
        {
            return $"Matrícula: {matricula} | Nombre: {nombre} | Carrera: {carrera} | Correo: {correo}";
        }
    }
}