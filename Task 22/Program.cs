using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива: ");
            
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumElem);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxElem);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            Action<Task<int>> action1 = new Action<Task<int>>(Print1);
            Task task4 = task2.ContinueWith(action1);

            Action<Task<int>> action2 = new Action<Task<int>>(Print2);
            Task task5 = task3.ContinueWith(action2);

            Action<Task<int[]>> action3 = new Action<Task<int[]>>(PrintArray);
            Task task6 = task1.ContinueWith(action3);


            task1.Start();
            Console.ReadKey();
            

        }


        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;

            
        }


        static int SumElem(Task<int[]> task)          
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }


        static int MaxElem(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }                
            }
            return max;
        }


        static void Print1(Task<int> task)
        {
            int sum = task.Result;
            Console.WriteLine("Сумма элементов массива = " + sum);
            Console.WriteLine();
        }

        static void Print2(Task<int> task)
        {
            int max = task.Result; 
            Console.WriteLine("Максимальный массива = " + max);
            Console.WriteLine();
        }

        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Сгенерированный массив: ");
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }

    }

}
