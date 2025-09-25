// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace SistemaAcademico
{
    
    public class Persona
    {
        private string nombre;
        private string dpi;
        private string correo;

        public Persona(string nombre, string dpi, string correo)
        {
            this.nombre = nombre;
            this.dpi = dpi;
            this.correo = correo;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public void SetNombre(string nuevoNombre)
        {
            nombre = nuevoNombre;
        }

        public string GetDpi()
        {
            return dpi;
        }

        public string GetCorreo()
        {
            return correo;
        }

        public virtual string MostrarInformacion()
        {
            return "Nombre: " + nombre + ", DPI: " + dpi + ", Correo: " + correo;
        }
    }

    
    public class Estudiante : Persona
    {
        private string carnet;
        private Dictionary<string, double> notas;

        public Estudiante(string nombre, string dpi, string correo, string carnet)
            : base(nombre, dpi, correo)
        {
            this.carnet = carnet;
            notas = new Dictionary<string, double>();
        }

        public string GetCarnet()
        {
            return carnet;
        }

        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() + ", Carnet: " + carnet;
        }

        public void AgregarNota(string curso, double nota)
        {
            if (notas.ContainsKey(curso))
            {
                notas[curso] = nota;
            }
            else
            {
                notas.Add(curso, nota);
            }
        }

        public double ObtenerNota(string curso)
        {
            if (notas.ContainsKey(curso))
            {
                return notas[curso];
            }
            return -1; 
        }

        public Dictionary<string, double> GetNotas()
        {
            return new Dictionary<string, double>(notas);
        }

        public double CalcularPromedioEstudiante()
        {
            if (notas.Count == 0)
                return 0;

            double suma = 0;
            foreach (double nota in notas.Values)
            {
                suma += nota;
            }
            return suma / notas.Count;
        }
    }

   
    public class Profesor : Persona
    {
        private string especialidad;
        private List<Curso> cursosAsignados;

        public Profesor(string nombre, string dpi, string correo, string especialidad)
            : base(nombre, dpi, correo)
        {
            this.especialidad = especialidad;
            cursosAsignados = new List<Curso>();
        }

        public string GetEspecialidad()
        {
            return especialidad;
        }

        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() + ", Especialidad: " + especialidad;
        }

        public void AsignarCurso(Curso curso)
        {
            if (!cursosAsignados.Contains(curso))
            {
                cursosAsignados.Add(curso);
            }
        }

        public List<Curso> GetCursosAsignados()
        {
            return new List<Curso>(cursosAsignados);
        }

        public int GetCantidadCursos()
        {
            return cursosAsignados.Count;
        }
    }

    
    public class Curso
    {
        private string codigo;
        private string nombre;
        private Profesor profesor;
        private List<Estudiante> estudiantes;
        private Dictionary<Estudiante, double> notasCurso;

        public Curso(string codigo, string nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            estudiantes = new List<Estudiante>();
            notasCurso = new Dictionary<Estudiante, double>();
        }

        public string GetCodigo()
        {
            return codigo;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public Profesor GetProfesor()
        {
            return profesor;
        }

        public void AgregarEstudiante(Estudiante estudiante)
        {
            if (!estudiantes.Contains(estudiante))
            {
                estudiantes.Add(estudiante);
                notasCurso[estudiante] = 0.0;
            }
        }

        public void AsignarProfesor(Profesor profesorAsignado)
        {
            profesor = profesorAsignado;
            profesorAsignado.AsignarCurso(this);
        }

        public void RegistrarNota(Estudiante estudiante, double nota)
        {
            if (estudiantes.Contains(estudiante))
            {
                notasCurso[estudiante] = nota;
                estudiante.AgregarNota(nombre, nota);
            }
        }

        public double CalcularPromedio()
        {
            if (estudiantes.Count == 0)
                return 0;

            double total = 0;
            foreach (double nota in notasCurso.Values)
            {
                total += nota;
            }
            return total / estudiantes.Count;
        }

        public List<Estudiante> GetEstudiantes()
        {
            return new List<Estudiante>(estudiantes);
        }

        public int GetCantidadEstudiantes()
        {
            return estudiantes.Count;
        }

        public double ObtenerNotaEstudiante(Estudiante estudiante)
        {
            if (notasCurso.ContainsKey(estudiante))
            {
                return notasCurso[estudiante];
            }
            return 0.0;
        }

        public string MostrarInformacion()
        {
            string info = "Curso: " + nombre + " (Código: " + codigo + ")\n";
            if (profesor != null)
                info += "Profesor: " + profesor.GetNombre() + "\n";
            info += "Estudiantes inscritos: " + estudiantes.Count + "\n";
            info += "Promedio del curso: " + CalcularPromedio().ToString("F2") + "\n";
            return info;
        }
    }

    
    public class ProgramaPrincipal
    {
        private List<Profesor> profesores;
        private List<Estudiante> estudiantes;
        private List<Curso> cursos;

        public ProgramaPrincipal()
        {
            profesores = new List<Profesor>();
            estudiantes = new List<Estudiante>();
            cursos = new List<Curso>();
            CrearDatosDeEjemplo();
        }

        private void CrearDatosDeEjemplo()
        {
            
            Profesor profesor1 = new Profesor("Dr. Carlos Méndez", "1234567890101",
                                            "carlos.mendez@universidad.edu", "Matemáticas");
            Profesor profesor2 = new Profesor("Dra. Ana López", "9876543210101",
                                            "ana.lopez@universidad.edu", "Programación");

            profesores.Add(profesor1);
            profesores.Add(profesor2);

            
            Estudiante estudiante1 = new Estudiante("María González", "1111111110101",
                                                  "maria.gonzalez@estudiante.edu", "2023001");
            Estudiante estudiante2 = new Estudiante("Juan Pérez", "2222222220101",
                                                  "juan.perez@estudiante.edu", "2023002");
            Estudiante estudiante3 = new Estudiante("Laura Martínez", "3333333330101",
                                                  "laura.martinez@estudiante.edu", "2023003");
            Estudiante estudiante4 = new Estudiante("Pedro Sánchez", "4444444440101",
                                                  "pedro.sanchez@estudiante.edu", "2023004");

            estudiantes.Add(estudiante1);
            estudiantes.Add(estudiante2);
            estudiantes.Add(estudiante3);
            estudiantes.Add(estudiante4);

            
            Curso curso1 = new Curso("MAT101", "Cálculo I");
            Curso curso2 = new Curso("PROG101", "Programación Básica");
            Curso curso3 = new Curso("FIS101", "Física General");

            cursos.Add(curso1);
            cursos.Add(curso2);
            cursos.Add(curso3);

            
            curso1.AsignarProfesor(profesor1);
            curso2.AsignarProfesor(profesor2);
            curso3.AsignarProfesor(profesor1);

            
            foreach (Estudiante estudiante in estudiantes)
            {
                curso1.AgregarEstudiante(estudiante);
                curso2.AgregarEstudiante(estudiante);
            }

            curso3.AgregarEstudiante(estudiante1);
            curso3.AgregarEstudiante(estudiante2);

            
            curso1.RegistrarNota(estudiante1, 85);
            curso1.RegistrarNota(estudiante2, 90);
            curso1.RegistrarNota(estudiante3, 78);
            curso1.RegistrarNota(estudiante4, 92);

            curso2.RegistrarNota(estudiante1, 88);
            curso2.RegistrarNota(estudiante2, 95);
            curso2.RegistrarNota(estudiante3, 82);
            curso2.RegistrarNota(estudiante4, 79);

            curso3.RegistrarNota(estudiante1, 91);
            curso3.RegistrarNota(estudiante2, 87);
        }

        public void EjecutarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("         BIENVENIDO SISTEMA ACADEMICO UNIVERSITARIO INGENIERO ;)");
                Console.WriteLine("==================================================");
                Console.WriteLine("P. Mostrar informacion de Profesores");
                Console.WriteLine("E. Mostrar informacion de Estudiantes");
                Console.WriteLine("C. Mostrar informacion de Cursos");
                Console.WriteLine("N. Registrar Nueva nota");
                Console.WriteLine("A. Agregar estudiante a curso");
                Console.WriteLine("M. Mostrar proMedio de un curso");
                Console.WriteLine("S. Mostrar notaS de un estudiante");
                Console.WriteLine("X. Salir del sistema");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opcion (P/E/C/N/A/M/S/X): ");

                string opcion = Console.ReadLine().ToUpper();

                switch (opcion)
                {
                    case "P":
                        MostrarProfesores();
                        break;
                    case "E":
                        MostrarEstudiantes();
                        break;
                    case "C":
                        MostrarCursos();
                        break;
                    case "N":
                        RegistrarNuevaNota();
                        break;
                    case "A":
                        AgregarEstudianteACurso();
                        break;
                    case "M":
                        MostrarPromedioCurso();
                        break;
                    case "S":
                        MostrarNotasEstudiante();
                        break;
                    case "X":
                        salir = true;
                        Console.WriteLine("Gracias por usar el sistema academico!");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Presione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void MostrarProfesores()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE PROFESORES ===");
            Console.WriteLine();

            if (profesores.Count == 0)
            {
                Console.WriteLine("No hay profesores registrados.");
            }
            else
            {
                for (int i = 0; i < profesores.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + profesores[i].MostrarInformacion());
                    List<Curso> cursosProfesor = profesores[i].GetCursosAsignados();
                    if (cursosProfesor.Count > 0)
                    {
                        Console.WriteLine("   Cursos asignados: " + cursosProfesor.Count);
                        foreach (Curso curso in cursosProfesor)
                        {
                            Console.WriteLine("   - " + curso.GetNombre());
                        }
                    }
                    else
                    {
                        Console.WriteLine("   Cursos asignados: Ninguno");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarEstudiantes()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE ESTUDIANTES ===");
            Console.WriteLine();

            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
            }
            else
            {
                for (int i = 0; i < estudiantes.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + estudiantes[i].MostrarInformacion());
                }
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarCursos()
        {
            Console.Clear();
            Console.WriteLine("=== INFORMACION DE CURSOS ===");
            Console.WriteLine();

            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos registrados.");
            }
            else
            {
                for (int i = 0; i < cursos.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + cursos[i].MostrarInformacion());

                    Console.WriteLine("   Estudiantes inscritos:");
                    List<Estudiante> estudiantesCurso = cursos[i].GetEstudiantes();
                    for (int j = 0; j < estudiantesCurso.Count; j++)
                    {
                        double nota = cursos[i].ObtenerNotaEstudiante(estudiantesCurso[j]);
                        Console.WriteLine("   " + (j + 1) + ". " + estudiantesCurso[j].GetNombre() +
                                         " - Nota: " + nota.ToString("F2"));
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void RegistrarNuevaNota()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR NUEVA NOTA ===");
            Console.WriteLine();

            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos disponibles.");
                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
                return;
            }

            try
            {
               
                Console.WriteLine("Cursos disponibles:");
                for (int i = 0; i < cursos.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + cursos[i].GetNombre());
                }

                Console.Write("Seleccione el numero del curso: ");
                int numeroCurso = int.Parse(Console.ReadLine()) - 1;

                if (numeroCurso < 0 || numeroCurso >= cursos.Count)
                {
                    Console.WriteLine("Numero de curso no valido.");
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    return;
                }

                Curso cursoSeleccionado = cursos[numeroCurso];
                List<Estudiante> estudiantesCurso = cursoSeleccionado.GetEstudiantes();

                if (estudiantesCurso.Count == 0)
                {
                    Console.WriteLine("Este curso no tiene estudiantes inscritos.");
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    return;
                }

                
                Console.WriteLine("Estudiantes en el curso:");
                for (int i = 0; i < estudiantesCurso.Count; i++)
                {
                    double notaActual = cursoSeleccionado.ObtenerNotaEstudiante(estudiantesCurso[i]);
                    Console.WriteLine((i + 1) + ". " + estudiantesCurso[i].GetNombre() +
                                     " - Nota actual: " + notaActual.ToString("F2"));
                }

                Console.Write("Seleccione el numero del estudiante: ");
                int numeroEstudiante = int.Parse(Console.ReadLine()) - 1;

                if (numeroEstudiante < 0 || numeroEstudiante >= estudiantesCurso.Count)
                {
                    Console.WriteLine("Numero de estudiante no valido.");
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    return;
                }

                Estudiante estudianteSeleccionado = estudiantesCurso[numeroEstudiante];

                Console.Write("Ingrese la nueva nota: ");
                double nuevaNota = double.Parse(Console.ReadLine());

                if (nuevaNota < 0 || nuevaNota > 100)
                {
                    Console.WriteLine("La nota debe estar entre 0 y 100.");
                }
                else
                {
                    cursoSeleccionado.RegistrarNota(estudianteSeleccionado, nuevaNota);
                    Console.WriteLine("Nota registrada exitosamente!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Debe ingresar un numero valido.");
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void AgregarEstudianteACurso()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR ESTUDIANTE A CURSO ===");
            Console.WriteLine();

            if (estudiantes.Count == 0 || cursos.Count == 0)
            {
                Console.WriteLine("No hay estudiantes o cursos disponibles.");
                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
                return;
            }

            try
            {
                
                Console.WriteLine("Estudiantes disponibles:");
                for (int i = 0; i < estudiantes.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + estudiantes[i].GetNombre());
                }

                Console.Write("Seleccione el numero del estudiante: ");
                int numeroEstudiante = int.Parse(Console.ReadLine()) - 1;

                if (numeroEstudiante < 0 || numeroEstudiante >= estudiantes.Count)
                {
                    Console.WriteLine("Numero de estudiante no valido.");
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    return;
                }

                Estudiante estudianteSeleccionado = estudiantes[numeroEstudiante];

                
                Console.WriteLine("Cursos disponibles:");
                for (int i = 0; i < cursos.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + cursos[i].GetNombre());
                }

                Console.Write("Seleccione el numero del curso: ");
                int numeroCurso = int.Parse(Console.ReadLine()) - 1;

                if (numeroCurso < 0 || numeroCurso >= cursos.Count)
                {
                    Console.WriteLine("Numero de curso no valido.");
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    return;
                }

                Curso cursoSeleccionado = cursos[numeroCurso];

                
                if (cursoSeleccionado.GetEstudiantes().Contains(estudianteSeleccionado))
                {
                    Console.WriteLine("El estudiante ya esta inscrito en este curso.");
                }
                else
                {
                    cursoSeleccionado.AgregarEstudiante(estudianteSeleccionado);
                    Console.WriteLine("Estudiante agregado exitosamente al curso!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Debe ingresar un numero valido.");
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarPromedioCurso()
        {
            Console.Clear();
            Console.WriteLine("=== PROMEDIO DE CURSO ===");
            Console.WriteLine();

            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos disponibles.");
                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.WriteLine("Cursos disponibles:");
                for (int i = 0; i < cursos.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + cursos[i].GetNombre());
                }

                Console.Write("Seleccione el numero del curso: ");
                int numeroCurso = int.Parse(Console.ReadLine()) - 1;

                if (numeroCurso < 0 || numeroCurso >= cursos.Count)
                {
                    Console.WriteLine("Numero de curso no valido.");
                }
                else
                {
                    Curso cursoSeleccionado = cursos[numeroCurso];
                    double promedio = cursoSeleccionado.CalcularPromedio();
                    Console.WriteLine("Promedio del curso " + cursoSeleccionado.GetNombre() +
                                     ": " + promedio.ToString("F2"));
                    Console.WriteLine("Total de estudiantes: " + cursoSeleccionado.GetCantidadEstudiantes());
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Debe ingresar un numero valido.");
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarNotasEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== NOTAS DE ESTUDIANTE ===");
            Console.WriteLine();

            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes disponibles.");
                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.WriteLine("Estudiantes disponibles:");
                for (int i = 0; i < estudiantes.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + estudiantes[i].GetNombre());
                }

                Console.Write("Seleccione el numero del estudiante: ");
                int numeroEstudiante = int.Parse(Console.ReadLine()) - 1;

                if (numeroEstudiante < 0 || numeroEstudiante >= estudiantes.Count)
                {
                    Console.WriteLine("Numero de estudiante no valido.");
                }
                else
                {
                    Estudiante estudianteSeleccionado = estudiantes[numeroEstudiante];
                    Dictionary<string, double> notas = estudianteSeleccionado.GetNotas();

                    Console.WriteLine("Notas de " + estudianteSeleccionado.GetNombre() + ":");
                    Console.WriteLine("----------------------------");

                    if (notas.Count == 0)
                    {
                        Console.WriteLine("No tiene notas registradas.");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, double> nota in notas)
                        {
                            Console.WriteLine(nota.Key + ": " + nota.Value.ToString("F2"));
                        }

                        double promedio = estudianteSeleccionado.CalcularPromedioEstudiante();
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Promedio general: " + promedio.ToString("F2"));
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Debe ingresar un numero valido.");
            }

            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            ProgramaPrincipal sistema = new ProgramaPrincipal();
            sistema.EjecutarMenu();
        }
    }
}