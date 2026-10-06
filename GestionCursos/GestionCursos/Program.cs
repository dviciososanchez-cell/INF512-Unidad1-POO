using System;
using System.Collections.Generic;

namespace GestionCursos
{
    class Program
    {
        static GestorAcademico gestor = new GestorAcademico();

        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("   SISTEMA DE GESTIÓN DE CURSOS - INF512");
                Console.WriteLine("==============================================");
                Console.WriteLine("1.  Registrar estudiante");
                Console.WriteLine("2.  Registrar curso");
                Console.WriteLine("3.  Inscribir estudiante en curso");
                Console.WriteLine("4.  Retirar inscripción");
                Console.WriteLine("5.  Buscar estudiante por matrícula");
                Console.WriteLine("6.  Buscar curso por código");
                Console.WriteLine("7.  Consultar cursos por estudiante");
                Console.WriteLine("8.  Consultar estudiantes por curso");
                Console.WriteLine("9.  Listar todos los estudiantes");
                Console.WriteLine("10. Listar todos los cursos");
                Console.WriteLine("11. Modificar estudiante");
                Console.WriteLine("12. Eliminar estudiante");
                Console.WriteLine("13. Eliminar curso");
                Console.WriteLine("14. Generar resumen académico");
                Console.WriteLine("0.  Salir");
                Console.WriteLine("==============================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                    opcion = -1;

                Console.Clear();

                switch (opcion)
                {
                    case 1: RegistrarEstudiante(); break;
                    case 2: RegistrarCurso(); break;
                    case 3: InscribirEstudiante(); break;
                    case 4: RetirarInscripcion(); break;
                    case 5: BuscarEstudiante(); break;
                    case 6: BuscarCurso(); break;
                    case 7: ConsultarCursosPorEstudiante(); break;
                    case 8: ConsultarEstudiantesPorCurso(); break;
                    case 9: ListarEstudiantes(); break;
                    case 10: ListarCursos(); break;
                    case 11: ModificarEstudiante(); break;
                    case 12: EliminarEstudiante(); break;
                    case 13: EliminarCurso(); break;
                    case 14: MostrarResumen(); break;
                    case 0: Console.WriteLine("Saliendo del sistema..."); break;
                    default: Console.WriteLine("Opción no válida."); break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

   

        static void RegistrarEstudiante()
        {
            Console.WriteLine(" REGISTRAR ESTUDIANTE \n");

            Console.Write("Matrícula: ");
            string mat = Console.ReadLine();

            Console.Write("Nombre completo: ");
            string nom = Console.ReadLine();

            Console.Write("Carrera: ");
            string car = Console.ReadLine();

            Console.Write("Correo: ");
            string cor = Console.ReadLine();

            if (gestor.RegistrarEstudiante(mat, nom, car, cor))
                Console.WriteLine("\n✔ Estudiante registrado correctamente.");
            else
                Console.WriteLine("\n✘ Error: Ya existe un estudiante con esa matrícula.");
        }

        static void RegistrarCurso()
        {
            Console.WriteLine(" REGISTRAR CURSO \n");

            Console.Write("Código: ");
            string cod = Console.ReadLine();

            Console.Write("Nombre del curso: ");
            string nom = Console.ReadLine();

            Console.Write("Créditos: ");
            if (!int.TryParse(Console.ReadLine(), out int cred))
            {
                Console.WriteLine("Créditos inválidos.");
                return;
            }

            Console.Write("Cupo máximo: ");
            if (!int.TryParse(Console.ReadLine(), out int cupo) || cupo <= 0)
            {
                Console.WriteLine("Cupo inválido.");
                return;
            }

            if (gestor.RegistrarCurso(cod, nom, cred, cupo))
                Console.WriteLine("\n✔ Curso registrado correctamente.");
            else
                Console.WriteLine("\n✘ Error: Ya existe un curso con ese código.");
        }

        static void InscribirEstudiante()
        {
            Console.WriteLine(" INSCRIBIR ESTUDIANTE \n");

            Console.Write("Matrícula del estudiante: ");
            string mat = Console.ReadLine();

            Console.Write("Código del curso: ");
            string cod = Console.ReadLine();

            Console.WriteLine("\n" + gestor.InscribirEstudiante(mat, cod));
        }

        static void RetirarInscripcion()
        {
            Console.WriteLine(" RETIRAR INSCRIPCIÓN \n");

            Console.Write("Matrícula del estudiante: ");
            string mat = Console.ReadLine();

            Console.Write("Código del curso: ");
            string cod = Console.ReadLine();

            Console.WriteLine("\n" + gestor.RetirarInscripcion(mat, cod));
        }

        static void BuscarEstudiante()
        {
            Console.WriteLine(" BUSCAR ESTUDIANTE \n");

            Console.Write("Matrícula: ");
            string mat = Console.ReadLine();

            Estudiante e = gestor.BuscarEstudiante(mat);
            if (e == null)
                Console.WriteLine("\n✘ Estudiante no encontrado.");
            else
                Console.WriteLine("\n✔ " + e.ToString());
        }

        static void BuscarCurso()
        {
            Console.WriteLine(" BUSCAR CURSO \n");

            Console.Write("Código: ");
            string cod = Console.ReadLine();

            Curso c = gestor.BuscarCurso(cod);
            if (c == null)
                Console.WriteLine("\n✘ Curso no encontrado.");
            else
                Console.WriteLine("\n✔ " + c.ToString());
        }

        static void ConsultarCursosPorEstudiante()
        {
            Console.WriteLine(" CURSOS POR ESTUDIANTE \n");

            Console.Write("Matrícula del estudiante: ");
            string mat = Console.ReadLine();

            List<Inscripcion> lista = gestor.ConsultarCursosPorEstudiante(mat);
            if (lista.Count == 0)
            {
                Console.WriteLine("\n✘ El estudiante no tiene cursos activos o no existe.");
                return;
            }

            Console.WriteLine($"\nCursos activos del estudiante {mat}:");
            foreach (Inscripcion i in lista)
                Console.WriteLine("  - " + i.Curso.ToString());
        }

        static void ConsultarEstudiantesPorCurso()
        {
            Console.WriteLine(" ESTUDIANTES POR CURSO \n");

            Console.Write("Código del curso: ");
            string cod = Console.ReadLine();

            List<Inscripcion> lista = gestor.ConsultarEstudiantesPorCurso(cod);
            if (lista.Count == 0)
            {
                Console.WriteLine("\n✘ El curso no tiene estudiantes activos o no existe.");
                return;
            }

            Console.WriteLine($"\nEstudiantes activos en el curso {cod}:");
            foreach (Inscripcion i in lista)
                Console.WriteLine("  - " + i.Estudiante.ToString());
        }

        static void ListarEstudiantes()
        {
            Console.WriteLine(" LISTADO DE ESTUDIANTES \n");

            List<Estudiante> lista = gestor.ListarEstudiantes();
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            foreach (Estudiante e in lista)
                Console.WriteLine("  • " + e.ToString());
        }

        static void ListarCursos()
        {
            Console.WriteLine(" LISTADO DE CURSOS \n");

            List<Curso> lista = gestor.ListarCursos();
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay cursos registrados.");
                return;
            }

            foreach (Curso c in lista)
                Console.WriteLine("  • " + c.ToString());
        }

        static void ModificarEstudiante()
        {
            Console.WriteLine(" MODIFICAR ESTUDIANTE \n");

            Console.Write("Matrícula del estudiante a modificar: ");
            string mat = Console.ReadLine();

            Estudiante e = gestor.BuscarEstudiante(mat);
            if (e == null)
            {
                Console.WriteLine("\n✘ Estudiante no encontrado.");
                return;
            }

            Console.WriteLine($"Datos actuales: {e}");
            Console.Write("Nueva carrera: ");
            string nuevaCar = Console.ReadLine();

            Console.Write("Nuevo correo: ");
            string nuevoCor = Console.ReadLine();

            if (gestor.ModificarEstudiante(mat, nuevaCar, nuevoCor))
                Console.WriteLine("\n✔ Estudiante modificado correctamente.");
            else
                Console.WriteLine("\n✘ No se pudo modificar el estudiante.");
        }

        static void EliminarEstudiante()
        {
            Console.WriteLine(" ELIMINAR ESTUDIANTE \n");

            Console.Write("Matrícula del estudiante a eliminar: ");
            string mat = Console.ReadLine();

            if (gestor.EliminarEstudiante(mat))
                Console.WriteLine("\n✔ Estudiante eliminado (junto con sus inscripciones).");
            else
                Console.WriteLine("\n✘ Estudiante no encontrado.");
        }

        static void EliminarCurso()
        {
            Console.WriteLine(" ELIMINAR CURSO \n");

            Console.Write("Código del curso a eliminar: ");
            string cod = Console.ReadLine();

            if (gestor.EliminarCurso(cod))
                Console.WriteLine("\n✔ Curso eliminado (junto con sus inscripciones).");
            else
                Console.WriteLine("\n✘ Curso no encontrado.");
        }

        static void MostrarResumen()
        {
            Console.WriteLine(gestor.GenerarResumen());
        }
    }
}