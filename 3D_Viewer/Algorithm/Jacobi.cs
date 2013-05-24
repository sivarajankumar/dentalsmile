using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.Algorithm
{
    class Jacobi
    {
        static void Mainxx()
        {
            Console.Clear();
            int n;      // number of equations/variables
            double[,] a; // co-efficients of variables (on LHS)
            double[] b;  // constant values (on RHS)
            double[] x0; // previous approximation to variable values
            double[] x;  // current approximation to variable values
            double[] diff; // absolute difference between approximations
            double tol;  // tolerance
            int max;     // maximum number of iterations      
            int iterations; // actual number of iterations required
            bool withinTol; // whether the results are within the tolerance

            bool isValidNumber;
            string temp;
            string[] tempArray;
            double value;
            bool finished;

            while (true)
            {
                Console.Write("Enter number of equations/variables (2 to 20): ");
                isValidNumber = int.TryParse(Console.ReadLine(), out n);
                if (isValidNumber && n > 1 && n < 21) break;
                Console.WriteLine("\nInvalid number, please re-enter\n");
            }

            Console.WriteLine("\nEnter variable co-efficients, separated by spaces :\n");
            a = new double[n, n];


            for (int i = 0; i < n; i++)
            {
                do
                {
                    Console.Write(" Equation {0} : ", i + 1);
                    temp = Console.ReadLine();
                    tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tempArray.Length != n)
                    {
                        Console.WriteLine("\nInvalid number of coefficients, please re-enter\n");
                        isValidNumber = false;
                        continue;
                    }
                    for (int j = 0; j < n; j++)
                    {
                        isValidNumber = double.TryParse(tempArray[j], out value);
                        if (!isValidNumber)
                        {
                            Console.WriteLine("\nLine contains an invalid number, please re-enter whole line\n");
                            break;
                        }
                        else if (j == i && value == 0)
                        {
                            Console.WriteLine("\nMain diagonal cannot contain zero co-efficients, please re-enter whole line\n");
                            isValidNumber = false;
                            break;
                        }
                        else
                        {
                            a[i, j] = value;
                        }
                    }
                }
                while (!isValidNumber);
            }

            Console.WriteLine("\nEnter constant values, separated by spaces\n");
            b = new double[n];

            do
            {
                Console.Write(" For all equations : ");
                temp = Console.ReadLine();
                tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempArray.Length != n)
                {
                    Console.WriteLine("\nInvalid number of constant values, please re-enter\n");
                    isValidNumber = false;
                    continue;
                }
                for (int i = 0; i < n; i++)
                {
                    isValidNumber = double.TryParse(tempArray[i], out value);
                    if (!isValidNumber)
                    {
                        Console.WriteLine("\nLine contains an invalid number, please re-enter whole line\n");
                        break;
                    }
                    else
                    {
                        b[i] = value;
                    }
                }

            }
            while (!isValidNumber);

            Console.WriteLine("\nEnter initial approximations, separated by spaces\n");
            x0 = new double[n];
            x = new double[n];
            diff = new double[n];

            do
            {
                Console.Write(" For all variables : ");
                temp = Console.ReadLine();
                tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempArray.Length != n)
                {
                    Console.WriteLine("\nInvalid number of approximations, please re-enter\n");
                    isValidNumber = false;
                    continue;
                }
                for (int i = 0; i < n; i++)
                {
                    isValidNumber = double.TryParse(tempArray[i], out value);
                    if (!isValidNumber)
                    {
                        Console.WriteLine("\nLine contains an invalid number, please re-enter whole line\n");
                        break;
                    }
                    else
                    {
                        x0[i] = value;
                    }
                }

            }
            while (!isValidNumber);

            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter tolerance ( > 0) for all variables : ");
                isValidNumber = double.TryParse(Console.ReadLine(), out tol);
                if (isValidNumber && tol > 0) break;
                Console.WriteLine("\nInvalid number, please re-enter\n");
            }

            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter maximum number of iterations (5 to 99) : ");
                isValidNumber = int.TryParse(Console.ReadLine(), out max);
                if (isValidNumber && max > 4 && max < 100) break;
                Console.WriteLine("\nInvalid number, please re-enter\n");
            }

            Console.WriteLine();
            iterations = max;
            withinTol = false;

            for (int iteration = 1; iteration <= max; iteration++)
            {
                finished = true;
                for (int i = 0; i < n; i++)
                {
                    x[i] = b[i];
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i) continue;
                        x[i] -= a[i, j] * x0[j];
                    }
                    x[i] /= a[i, i];
                    diff[i] = Math.Abs(x[i] - x0[i]);
                    if (finished && diff[i] > tol) finished = false;
                }
                if (finished)
                {
                    iterations = iteration;
                    withinTol = true;
                    break;
                }
                Array.Copy(x, x0, n);
            }

            Console.WriteLine("The approximate values of the variables are :\n");
            for (int i = 1; i <= n; i++)
            {
                Console.Write(" x{0} = {1:F5}", i, x[i - 1]); // display to 5 dp
                Console.WriteLine();
            }

            Console.WriteLine("\nNumber of iterations : {0}", iterations);
            Console.WriteLine("Approximations are within tolerance : {0}", withinTol);

            Console.Write("\nPress any key to exit program\n");
            Console.ReadKey();

        }
    }
}
