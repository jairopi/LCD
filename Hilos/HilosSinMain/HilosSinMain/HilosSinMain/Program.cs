using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ExamenConsola
{
    class Program
    {

        /*                  PRUEBA
         * 
         * Main es el hilo de la estación 1, el Task es el hilo de la estación 2.
         * Para el task, se pondrá un método estático dedicada a la estación 2.
         * 
         * En el main inicializamos objeto obteniendo los datos de su constructor.
         * Una vez hecho esto, inicializamos los métodos de los dos hilos.
         * Sigue saliendo igual aunque se haga por separado
         * 
         * 
         */

        public static int index;                                  //Indice de la cola
        public static double[,] cola = new double[20, 2];         //Primer valor es el tiempo, el segundo la estación. 
        //TIENE QUE SER ESTÁTICO


        /*
         * Como index es estático, cuando se incrementa en un hilo, se tiene en cuenta en el otro
         * hilo también, por lo que si queremos 10 de cada uno, tendremos que hacer la comprobación
         * de <20 que es el total. Además el segundo hilo, está dentro del primero, por lo que 
         * se generará 10 de cada uno ( y no 14 de 1 y 6 de otro).
         * Si hacemos la comprobación de index<10, saldrá 5 de cada uno.
         * 
         * GUARDAR EN ARRAY (APARTADO B)
         * 
         * Aprovecharemos el tiempo generado en el apartado A para que sea el mismo que se guarda en el array.
         * Cada vez que se genere un tiempo, llamaremos a un método estático común a ambos hilos en el que 
         * indicaremos el tiempo, y la estación a la que corresponde.
         * Una vez vuelve de almacenarlo, incrementamos index.
         * Cuando hemos llegado a index=20, es cuando el array está completo, y lo mostramos por consola.
         */


        public Program()
        {
            iniciar1();
            iniciar2();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.ReadLine();                 //Para que la consola no se cierre

        }

        //inicia hilo1
        public void iniciar1()
        {
            Task t1 = new Task(D1);
            t1.Start();
        }
        //inicia hilo2
        public void iniciar2()
        {
            Task t2 = new Task(D2);
            t2.Start();
        }
        //método del hilo1
        public static void D1()
        {
            double tiempo1;

            while (true)
            {
                tiempo1 = new Random().NextDouble();
                if (index < 20)
                {
                    Console.WriteLine("Tiempo de la estación 1:" + tiempo1);         //Se genera el tiempo
                    GuardarArray(tiempo1, 1);                                       //Se guarda en el array indicando estación
                    index++;                                                        //Se aumenta index cada vez que se haya guardado
                    if (index == 20)                                                //Si index = 20, mostramos en consola array
                    {
                        mostrarArray();
                    }
                }
                Thread.Sleep(1000);
            }
        }
        //método del hilo2
        public static void D2()
        {
            //Tiempo de la estación 2. Generado con Random como dice el enunciado
            double tiempo2;

            while (true)
            {
                tiempo2 = new Random().NextDouble();
                if (index < 20)
                {
                    Console.WriteLine("Tiempo de la estación 2:" + tiempo2);          //Se genera tiempo2
                    GuardarArray(tiempo2, 2);                                       //Guardamos el tiempo generado en array
                    index++;
                    if (index == 20)                                                //Si el array está lleno, se muestra por consola
                    {
                        mostrarArray();
                    }
                }
                Thread.Sleep(1000);                     //Si Le pongo otro 001ms más para que no repita valores random (opcional)
            }                                    //Esto hace que siempre llegue antes estación 1, si ponemos a ambos 1000
            //Pueden llegar cada vez uno.
        }

        //Método para almacenar los tiempos en el array cola
        public static void GuardarArray(double tiempo, int estacion)
        {
            cola[index, 0] = tiempo;
            cola[index, 1] = estacion;

        }

        //Método para mostrar por consola el array una vez está completo
        public static void mostrarArray()
        {
            Console.WriteLine("---------------------------APARTADO B----------------------------");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Estación: " + cola[i, 1] + " Tiempo: " + cola[i, 0]);
            }

            //APARTADO C
            /*
             * Una vez que ya tenemos el array completo, llamamos al método ordenarArray1() para que los ordene
             */

            ordenarArray1();

        }

        //Método para ordenar array. Primeros 10 elementos estación 1, siguientes 10 estación 2
        public static void ordenarArray1()
        {
            double[,] aux = new double[20, 2];
            int j = 0;

            //Estación 1
            for (int i = 0; i < 20; i++)
            {
                if (cola[i, 1] == 1)
                {

                    aux[j, 0] = cola[i, 0];
                    aux[j, 1] = cola[i, 1];
                    j++;

                }
            }

            //Estación 2
            for (int i = 0; i < 20; i++)
            {
                if (cola[i, 1] == 2)
                {

                    aux[j, 0] = cola[i, 0];
                    aux[j, 1] = cola[i, 1];
                    j++;

                }
            }

            //Asignamos el ordenado y mostramos por consola
            cola = aux;
            Console.WriteLine("-------------------------APARTADO C--------------------------");
            mostrarOrdenado1();

            /* También se puede poner aquí, pero prefiero método.
             * 
            Console.WriteLine("-------------------------APARTADO C--------------------------");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Estación: " + cola[i, 1] + " Tiempo: " + cola[i, 0]);
            }

             */


            /*
             * En primer lugar, si en el array cola, el segundo valor (que está entre 0=tiempo, 1=estacion)
             * comprobamos el segundo valor (1) para todas las posiciones cola[i,1]==1, es decir es igual a la estacion1
             * Guardamos todo su contenido, tiempo(0) y estacion(1) en aux. A continuación se incrementa j++, para que así
             * tengamos ordenados sólo los de la estación 1.
             * 
             * Una vez se han guardado todos los valores de la estación 1, no reseteamos j, ya que queremos que de 0-9 sea
             * de la estación 1 y de 9-19 de la estación 2, por lo que hacemos la misma comprobación pero con la estación 2.
             * 
             * Cuando se haya rellenado completamente tendremos en aux el array ordenado en las primeras 10 posiciones para 
             * la estación 1 y en las 10 siguientes para la posición 2. 
             * Por último al salir de los bucles for(ya está ordenado) ponemos aux como resultado para luego poder mostrarlo.
             */


            //Llamamos a ordenar de la otra forma
            ordenarArray2();
        }

        public static void mostrarOrdenado1()
        {

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Estación: " + cola[i, 1] + " Tiempo: " + cola[i, 0]);
            }

        }

       


        /*
         * En el turno 2 es igual, sólo que el último apartado, en lugar de ser 10 estación 1 y 10 estación2,
         * son primero estación 1 luego estación 2 y así sucesivamente. Esto lo implementamos aquí.
         */

        public static void ordenarArray2()
        {
            double[,] aux2 = new double[20, 2];
            int a = 0;
            int b = 1;

            for (int i = 0; i < 20; i++)
            {
                if (cola[i, 1] == 1)
                {

                    aux2[a, 0] = cola[i, 0];
                    aux2[a, 1] = cola[i, 1];
                    a += 2;
                }
                else
                {
                    aux2[b, 0] = cola[i, 0];
                    aux2[b, 1] = cola[i, 1];
                    b += 2;
                }
            }

            cola = aux2;
            Console.WriteLine("--------------------------------APARTADO C TURNO 2------------------------");
            mostrarOrdenado1();

            /*
             * Como tenemos que poner una posición de la estación 1 y otra posición de la estación 2, recorremos todo el array
             * ordenado, si es de la estación 1, lo guardamos en aux2[a,0] y en aux2[a,1]. a empieza en 0 ya que la primera
             * posición corresponde a la estación 1, a partir de ahí se incrementa de dos en dos hasta que llegamos, ya que la
             * siguiente vez no será en esa posición, sino saltando una posición.
             * 
             * Cuando sea de la estación 2, lo guardamos en aux2[b,0] o en aux2[b,1]. b empieza en 1 ya que para la primera
             * estación empezamos en 1, y también aumentaremos de dos en dos (b+=2) ya que también irá salteado.
             * 
             * Una vez que se ha recorrido entero y ordenado, lo asignamos a cola (que es estático y se puede usar en los
             * métodos) y llamamos al método de mostrarArray (mismo que antes).
             */
        }

    }
}

