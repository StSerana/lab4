using System;
using System.Collections.Generic;

namespace lab4
{
    class Rpn
        {
            
            public static double Calculate(string input)    //"входной" метод класса
            {
                
                var output = GetExpression(input);   //преобразование выражения в постфиксную запись
                Console.Out.WriteLine(output);
                var result = Counting(output);   //решение выражения
                return result;
                
            }
            
            public static string GetExpression(string input)   //метод перевода выражения в постфиксную запись
            {
                var output = string.Empty; //Строка для хранения выражения
                var operStack = new Stack<char>(); //Стек для хранения операторов

                for (var i = 0; i < input.Length; i++) //Для каждого символа в входной строке
                {

                    //Разделители пропускаем
                    if (IsDelimeter(input[i]))
                        continue; //Переходим к следующему символу
                    
                    //проверка на отрицательное число: если знак "-" в начале строки или перед знаком "-" нет числа 
                    if (input[i] == '-' && ((i > 0 && !Char.IsDigit(input[i - 1])) || i == 0))
                    {
                        i++;
                        output += "-"; //в переменную для чисел добавляется знак "-"    
                    }
                    
                    //Если символ - цифра, то считываем все число
                    if (char.IsDigit(input[i])) //Если цифра
                    {
                        //Читаем до разделителя или оператора, что бы получить число
                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            output += input[i]; //Добавляем каждую цифру числа к нашей строке
                            i++; //Переходим к следующему символу

                            if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                        }

                        output += " "; //Дописываем после числа пробел в строку с выражением
                        i--; //Возвращаемся на один символ назад, к символу перед разделителем
                    }

                    //Если символ - оператор
                    if (!IsOperator(input[i])) continue;
                    switch (input[i])
                    {
                        //Если символ - открывающая скобка
                        case '(':
                            operStack.Push(input[i]); //Записываем её в стек
                            break;
                        //Если символ - закрывающая скобка
                        case ')':
                        {
                            //Выписываем все операторы до открывающей скобки в строку
                            char s = operStack.Pop();

                            while (s != '(')
                            {
                                output += s.ToString() + ' ';

                                s = operStack.Pop();
                            }

                            break;
                        }
                        //Если любой другой оператор
                        default:
                        {
                            if (operStack.Count > 0) //Если в стеке есть элементы
                                if (GetPriority(input[i]) <=
                                    GetPriority(operStack
                                        .Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                    output += operStack.Pop().ToString() +
                                              " "; //То добавляем последний оператор из стека в строку с выражением

                            operStack.Push(
                                char.Parse(input[i]
                                    .ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека
                            break;
                        }
                    }
                }

                //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
                    while (operStack.Count > 0)
                        output += operStack.Pop() + " ";
                    return output; //Возвращаем выражение в постфиксной записи
            }
            
            public static double Counting(string output)   //метод решения OPN
            {
                string result;
                var mas = output.Split(' ');
                for (var i = 0; i < mas.Length; i++)
                    
                    switch (mas[i])
                    {
                        case "+":   //если найдена операция сложения
                            result = (double.Parse(mas[i - 2]) + double.Parse(mas[i - 1])).ToString();  //выполняем сложение и переводим ее в строку
                            mas[i - 2] = result;    //на место 1-ого операнда записывается результат (как если бы a=a+b)
                            for (int j = i - 1; j < mas.Length - 2; j++)    //удаляем из массива второй операнд и знак арифм действия
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);  //обрезаем массив элементов на 2 удаленнх элемента
                            i -= 2;
                            break;
 
 
                        case "-":   //далее все аналогично
                            result = (double.Parse(mas[i - 2]) - double.Parse(mas[i - 1])).ToString();
                            mas[i - 2] = result;
                            for (int j = i - 1; j < mas.Length - 2; j++)
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);
                            i -= 2;
                            break;
 
                        case "*":
                            result = (double.Parse(mas[i - 2]) * double.Parse(mas[i - 1])).ToString();
                            mas[i - 2] = result;
                            for (int j = i - 1; j < mas.Length - 2; j++)
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);
                            i -= 2;
                            break;
 
                        case "/":
                            result = (double.Parse(mas[i - 2]) / double.Parse(mas[i - 1])).ToString();
                            mas[i - 2] = result;
                            for (int j = i - 1; j < mas.Length - 2; j++)
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);
                            i -= 2;
                            break;

                        case "^":
                            result = Math.Pow(double.Parse(mas[i - 2]), double.Parse(mas[i - 1])).ToString();
                            mas[i - 2] = result;
                            for (int j = i - 1; j < mas.Length - 2; j++)
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);
                            i -= 2;
                            break;
                    }
                return double.Parse(mas[0]);
            }
                    
            
 
            
 
 
            //Метод возвращает приоритет оператора
            public static byte GetPriority(char s)
            {
                switch (s)
                {
                    case '(': return 0;
                    case ')': return 1;
                    case '+': return 2;
                    case '-': return 3;
                    case '*': return 4;
                    case '/': return 4;
                    case '^': return 5;
                    default: return 6;
                }
            }
            //Метод возвращает true, если проверяемый символ - оператор
            public static bool IsOperator(char с)
            {
                return "+-/*^()".IndexOf(с) != -1;
            }
 
            //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
            public static bool IsDelimeter(char c)
            {
                return " =".IndexOf(c) != -1;
            }
        }
}