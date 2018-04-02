using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Se pondrá un array por pantalla desordenado con números aleatorios.
 * Se llamará a un método que lo ordene (algoritmo de la burbuja).
 * Después haremos otro array que será ordenado, y lo meteremos en un tercer array
 * poniendo cada dato intercalado.
 */ 

namespace aleatorios
{
    class Program
    {
        //Método ordenar
        public void Ordenar(int [] array)
        {
            //Ordenar por algoritmo de la burbuja

            int t;
            for (int a = 1; a < array.Length; a++)
                for (int b = array.Length - 1; b >= a; b--)
                {
                    if (array[b - 1] > array[b])
                    {
                        t = array[b - 1];
                        array[b - 1] = array[b];
                        array[b] = t;
                    }
                }

        }

        static void Main(string[] args)
        {
            int[] array = new int[15];
            int[] array2 = new int[15];
            int[] array3 = new int[array.Length+array2.Length];
            //Creo el Random
            Random aleatorio = new Random();
            
            //Relleno el array
            //Muestro el array sin ordenar por pantalla

            Console.WriteLine("El array sin ordenar de nº aleatorios es: ");

            for (int i = 0; i < array.Length; i++)
            {
                 //Relleno el array con nº aleatorios entre 0 y 25

                Console.Write(array[i] = aleatorio.Next(0, 25));

            }

            Console.WriteLine(" ");


            //Ordenar array. Para ello creamos objeto

            Program objeto = new Program();
            objeto.Ordenar(array);

            //Mostrar array ordenado
            Console.WriteLine("El array ordenado es: ");

            foreach (int i in array)
            {

                Console.Write(i);
            }

            //Genero array2 y lo ordeno

            for (int i=0;i<array2.Length;i++)
            {
                array2[i] = aleatorio.Next(0, 25);
            }

            objeto.Ordenar(array2);

            Console.WriteLine(" ");
            Console.WriteLine("El array 2 ordenado es: ");
            foreach (int i in array2)
            {

                Console.Write(i);
            }

            Console.WriteLine(" ");

            //Pongo intercalados en array3 los dos arrays anteriores
            
            //Primero ponemos el array a en el array c

            /*
             * En el primer array, en a guardamos en la posición 0, y a partir de ahí
             * en una posición no, y en otra sí. En 0,2,4...los valores de a.
             * Es por esto por lo que recorremos así el array3 y lo asignamos a todos los valores de 
             * array mediante j++ ya que se tienen que coger todos los elementos de array.
             * Es por esto por lo que tendremos i+=2.
             * Además sólo llegamos al penúltimo valor ya que el último corresponde al array2.
             * 
             * En el segundo es igual sólo que empezamos en 1 y seguiremos en 3,5...
             * Para ello tenemos que recorrer todo el array3 ya que llegamos a la última posición.
             */ 

            int j = 0;

            for (int i = 0; i < array3.Length - 1; i+=2)
            {
                array3[i] = array[j];
                j++;
            }

            //Se rellena el array 3 con el array2
            int k = 0;
            for (int i = 1; i < array3.Length; i += 2)
            {
                array3[i] = array2[k];
                k++;
            }

            //Imprimimos array3

            Console.WriteLine("El array3 es: ");

            foreach (int i in array3)
            {

                Console.Write(i);
            }

            Console.WriteLine(" ");

            for (int i = 0; i < array3.Length; i++)
            {
                
                Console.WriteLine("Los valores de array3 son ");
                Console.WriteLine(array3[i]);

            }
                Console.ReadLine();

        }
    }
}
