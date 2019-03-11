using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overload
{
    class Program
    {
        static void Main(string[] args)
        {
            Human h1 = new Human();
            h1.Name = "111";
            Human h2 = new Human();
            h1.Name = "112";
            if (h1.Equals(h2)) Console.WriteLine("Эквивалентны");
            else Console.WriteLine("Неэквивалентны"); 
        }
        //1.	Создать класс с несколькими свойствами. Реализовать перегрузку операторов ==, != и Equals.
        public class Human
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            private string IIN_;
            public string IIN
            {
                get
                {
                    return IIN_;
                }
                set
                {
                    int k = 0;
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (char.IsLetter(value[i]))
                            k++;
                    }
                    if (value.Length != 12 || k != 0)
                        throw new ArgumentException("Некорректный ИИН!");
                    IIN_ = value;
                }
            }
            public Human() { }
            public Human(string name, string surname, string iin)
            {
                this.Name = name;
                this.Surname = surname;
                this.IIN = iin;
            }
            public static bool operator ==(Human a, Human b)
            {
                return (a.Name == b.Name && a.Surname == b.Surname && a.IIN == b.IIN);
            }
            public static bool operator !=(Human a, Human b)
            {
                return (a.Name != b.Name && a.Surname != b.Surname && a.IIN != b.IIN);
            }
            public override bool Equals(object obj)
            {
                if (obj is Human)
                {
                    return (this.Name == ((Human)obj).Name && this.Surname == ((Human)obj).Surname && this.IIN == ((Human)obj).IIN);
                }
                else
                    return false;
            }
        }
        //2.	Дан класс содержащий внутри себя массив. 
        //Реализовать перегрузку операторов < , > так, чтобы если сумма элементов массива 1 класса больше, возвращалось значение true и наоборот.
        public class Arr
        {
            public List<int> a = null;
            public static bool operator >(Arr x, Arr y)
            {
                int sum1 = 0, sum2 = 0;
                for (int i = 0; i < x.a.Count; i++)
                {
                    sum1 += x.a[i];
                }
                for (int i = 0; i < y.a.Count; i++)
                {
                    sum2 += y.a[i];
                }
                return (sum1 > sum2);
            }
            public Arr()
            {
                a = new List<int>();
            }
            public Arr(int num)
            {
                a = new List<int>(num);
            }
            public static bool operator <(Arr x, Arr y)
            {
                int sum1 = 0, sum2 = 0;
                for (int i = 0; i < x.a.Count; i++)
                {
                    sum1 += x.a[i];
                }
                for (int i = 0; i < y.a.Count; i++)
                {
                    sum2 += y.a[i];
                }
                return sum1 < sum2;
            }
        }
        //3.	Задание будет базироваться на примере в этом уроке. 
        //Необходимо реализовать второй вариант сложения денег – чтобы можно было суммировать деньги в разных валютах. 
        //Для этого создайте отдельный класс, который будет предоставлять механизм конвертации денег по заданному курсу. 
        //Кроме этого, перегрузите для класса Money оператор сравнения «==» (при перегрузке данного оператора, 
        //обязательной является и перегрузка противоположного ему оператора «!=»).
        public enum currency { kzt = 0, usd = 1, rur = 2 };
        public class Money
        {
            public string Number { get; set; }
            public decimal Amount { get; set; }
            public currency Currency { get; set; }
            public Money(decimal amount, currency cur)
            {
                this.Amount = amount;
                this.Currency = cur;
            }
            public static Money operator +(Money a, Money b)
            {
                if (a.Currency == b.Currency)
                    return new Money(a.Amount + b.Amount, a.Currency);
                else
                {
                    throw new Exception("Валюты счетов разные!");
                }
            }
            public static bool operator ==(Money a, Money b)
            {
                return (a.Number == b.Number);
            }
            public static bool operator !=(Money a, Money b)
            {
                return (a.Number != b.Number);
            }
            public string getInfo()
            {
                return string.Format("{0} {1}", Amount, Currency);
            }
        }
        public class ConvertMoney
        {
            public decimal Sum { get; set; }
            public ConvertMoney(Money b, double kurs)
            {
                Sum = b.Amount * Convert.ToDecimal(kurs);
            }
        }
        //4.	Класс – одномерный массив. Дополнительно перегрузить следующие операции: 
        //* – умножение массивов; [] – доступ по индексу, int() – размер массива; == – проверка на равенство; <= – сравнение
        public class A
        {
            private int[] arr = null;
            public int this[int index]
            {
                get
                {
                    return arr[index];
                }
                set
                {
                    arr[index] = value;
                }
            }
            public A()
            {
                arr = new int[10];
            }
            public A(int num)
            {
                arr = new int[num];
            }
            public static A operator *(A x, A y)
            {
                if (x.arr.Length != y.arr.Length)
                    throw new Exception("Размеры массивов не равны!");
                A z = new A(x.arr.Length);
                for (int i = 0; i < x.arr.Length; i++)
                {
                    z.arr[i] = x.arr[i] * y.arr[i];
                }
                return z;
            }
            public static bool operator ==(A x, A y)
            {
                if (x.arr.Length != y.arr.Length)
                    return false;
                else
                {
                    for (int i = 0; i < x.arr.Length; i++)
                    {
                        if (x.arr[i] != y.arr[i])
                            return false;
                    }
                    return true;
                }
            }
            public static bool operator !=(A x, A y)
            {
                if (x.arr.Length != y.arr.Length)
                    return true;
                else
                {
                    for (int i = 0; i < x.arr.Length; i++)
                    {
                        if (x.arr[i] != y.arr[i])
                            return true;
                    }
                    return false;
                }
            }
            public static bool operator <=(A x, A y)
            {
                int sum1 = 0, sum2 = 0;
                for (int i = 0; i < x.arr.Length; i++)
                {
                    sum1 += x.arr[i];
                }
                for (int i = 0; i < y.arr.Length; i++)
                {
                    sum2 += y.arr[i];
                }
                return sum1 <= sum2;
            }
            public static bool operator >=(A x, A y)
            {
                int sum1 = 0, sum2 = 0;
                for (int i = 0; i < x.arr.Length; i++)
                {
                    sum1 += x.arr[i];
                }
                for (int i = 0; i < y.arr.Length; i++)
                {
                    sum2 += y.arr[i];
                }
                return sum1 >= sum2;
            }
            public int Length()
            {
                return arr.Length;
            }
        }

        //5.	Класс – одномерный массив. 
        //Дополнительно перегрузить следующие операции: [] – доступ по индексу; == – проверка на равенство; != – проверка на неравенство; + – объединение массивов
        public class B
        {
            private int[] arr = null;
            public B()
            {
                arr = new int[10];
            }
            public B(int num)
            {
                arr = new int[num];
            }
            public int this[int index]
            {
                get
                {
                    return arr[index];
                }
                set
                {
                    arr[index] = value;
                }
            }
            public static bool operator ==(B x, B y)
            {
                if (x.arr.Length != y.arr.Length)
                    return false;
                else
                {
                    for (int i = 0; i < x.arr.Length; i++)
                    {
                        if (x.arr[i] != y.arr[i])
                            return false;
                    }
                    return true;
                }
            }
            public static bool operator !=(B x, B y)
            {
                if (x.arr.Length != y.arr.Length)
                    return true;
                else
                {
                    for (int i = 0; i < x.arr.Length; i++)
                    {
                        if (x.arr[i] != y.arr[i])
                            return true;
                    }
                    return false;
                }
            }
            public static B operator +(B x, B y)
            {
                if (x.arr.Length != y.arr.Length)
                    throw new Exception("Разные размры у массивов");
                B z = new B(x.arr.Length);
                for (int i = 0; i < x.arr.Length; i++)
                {
                    z[i] = x[i] + y[i];
                }
                return z;
            }
        }
        //6.	Создать класс Decimal для работы с без знаковыми целыми десятичными числами, 
        //используя для представления массив из 100 элементов типа чар, каждый из которых является десятичной цифрой. 
        //Реализовать арифметические операции + - * /
        public class Decimal_
        {
            public decimal d { get; set; }
            public Decimal_() { }
            public Decimal_(decimal x)
            {
                d = x;
            }
            public static explicit operator Decimal_(int x)
            {
                return new Decimal_ { d = x };
            }
            public static explicit operator Decimal_(double x)
            {
                return new Decimal_ { d = (int)x };
            }
            public static Decimal_ operator +(Decimal_ a, Decimal_ b)
            {
                return new Decimal_(a.d + b.d);
            }
            public static Decimal_ operator -(Decimal_ a, Decimal_ b)
            {
                return new Decimal_(a.d - b.d);
            }
            public static Decimal_ operator *(Decimal_ a, Decimal_ b)
            {
                return new Decimal_(a.d * b.d);
            }
            public static Decimal_ operator /(Decimal_ a, Decimal_ b)
            {
                return new Decimal_(a.d / b.d);
            }
        }
        //7.	Создать структуру Complex с перегруженными операциями, а также с возможностью приведения типа double->complex. 
        //Должны быть реализованы также ToString(), Equals(), ==, !=. Сравнить производительность в случае реализации Complex как класса и как структуры.
        public class Complex_
        {
            public double num { get; set; }
            public static explicit operator Complex_(double x)
            {
                return new Complex_ { num = x };
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", num, 0);
            }
            public override bool Equals(object obj)
            {
                return num == ((Complex_)obj).num;
            }
            public static bool operator ==(Complex_ a, Complex_ b)
            {
                return a.num == b.num;
            }
            public static bool operator !=(Complex_ a, Complex_ b)
            {
                return a.num != b.num;
            }
        }
        //8.	Создать класс Frac с перегруженными операциями + - * / , а также с возможностью приведения типа Frac->double. 
        //Должны быть реализованы также ToString(), Equals(), ==, !=. Вычислить значение полинома в точке. Все коэффициенты и x должны иметь тип Frac. 
        //Сравнить производительность в случае реализации Frac как класса и как структуры.
        public class Frac
        {
            private int x, y;
            public Frac(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public static explicit operator double(Frac f)
            {
                return (double)f.x / f.y;
            }
            public static Frac operator +(Frac a, Frac b)
            {
                return new Frac(a.x * b.y + b.x * a.y, a.y * b.y);
            }
            public static Frac operator -(Frac a, Frac b)
            {
                return new Frac(a.x * b.y - b.x * a.y, a.y * b.y);
            }
            public static Frac operator *(Frac a, Frac b)
            {
                return new Frac(a.x * b.x, a.y * b.y);
            }
            public static Frac operator /(Frac a, Frac b)
            {
                return new Frac(a.x * b.y, a.y * b.x);
            }
            public override string ToString()
            {
                return string.Format("{0}/{1}", x, y);
            }
            public override bool Equals(object obj)
            {
                return (x == ((Frac)obj).x && y == ((Frac)obj).y) || (double)x / y == (double)obj;
            }
            public static bool operator ==(Frac a, Frac b)
            {
                return (a.x == b.x && a.y == b.y) || (double)a.x / a.y == (double)b.x / b.y;
            }
            public static bool operator !=(Frac a, Frac b)
            {
                return (a.x != b.x || a.y != b.y) || (double)a.x / a.y != (double)b.x / b.y;
            }
        }
    }
}
