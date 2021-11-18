using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCs
{
    class Program
    {
        static void Main(string[] args)
        {
            Date date1 = new Date("3", "6", "1996");
            Date date2 = new Date("3", "6", "1996");
 
            Console.WriteLine(date2 > date1);
            Console.WriteLine(date2 <= date1);

            Console.ReadKey();
        }
    }

    class Date
    {
        private int _day;
        private int _month;
        private int _year;

        public string Day => _day.ToString();
        public string Month => _month.ToString();
        public string Year => _year.ToString();

        public Date()
        {
            _day = 0;
            _month = 0;
            _year = 0;
        }

        public Date(string day, string month, string year)
        {
            TryConvertDate(day, month, year);
        }

        public Date(int day, int month, int year)
        {
            _day = day;
            _month = month;
            _year = year;
        }

        public static Date GetDateFromString(string date, char splitter = '-')
        {
            string[] values = date.Split(splitter);
            if (values.Length < 3 || values.Length > 3)
            {
                throw new Exception("Неверный формат даты!");
            }
            return new Date(values[0], values[1], values[2]);
        }

        public static Date operator +(Date date, int d)
        {
            Date newDate = date;
            int maxDay = date.Maxday();
            if (date._day + d > maxDay)
            {
                d -= maxDay - date._day;
                if (++date._month > 12)
                {
                    newDate._month = 1;
                    newDate._year++;
                }
                while (d / maxDay != 0)
                {
                    if (++date._month > 12)
                    {
                        newDate._month = 1;
                        newDate._year++;
                    }
                    d -= maxDay;
                }
                newDate._day = d;
            }
            else
            {
                newDate._day += d;
            }
            return newDate;
        }

        public static bool operator >(Date c1, Date c2)
        {
            return c1._year > c2._year || c1._month > c2._month || c1._day > c2._day;
        }

        public static bool operator <(Date c1, Date c2)
        {
            return c1._year < c2._year || c1._month < c2._month || c1._day < c2._day;
        }

        public static bool operator >=(Date c1, Date c2)
        {
            return c1._year >= c2._year || c1._month >= c2._month || c1._day >= c2._day;
        }

        public static bool operator <=(Date c1, Date c2)
        {
            return c1._year <= c2._year || c1._month <= c2._month || c1._day <= c2._day;
        }
        public static bool operator ==(Date c1, Date c2)
        {
            return c1._year == c2._year && c1._month == c2._month && c1._day == c2._day;
        }

        public static bool operator !=(Date c1, Date c2)
        {
            return c1._year != c2._year || c1._month != c2._month || c1._day != c2._day;
        }


        public static Date operator -(Date d1, Date d2)
        {
            if (d2._year > d1._year)
            {
                throw new Exception("Нельзя вычитать года... из меньшего большее");
            }
            else if (d2._month > d1._month)
            {
                throw new Exception("Нельзя вычитать месяца... из меньшего большее");
            }
            else if (d2._day > d1._day)
            {
                throw new Exception("Нельзя вычитать дни... из меньшего большее");
            }
            else
            {
                return new Date(d1._day - d2._day, d1._month - d2._month, d1._year - d2._year);
            }
        }

        public override string ToString()
        {
            return $"{Day} day(s): {Month} month(s): {Year} year(s)";
        }

        private void TryConvertDate(string d, string m, string y)
        {
            int day;
            int mounth;
            int year;
            if (int.TryParse(d, out day) == false)
            {
                throw new Exception("Неверный формат для дня!");
            }

            if (int.TryParse(m, out mounth) == false)
            {
                throw new Exception("Неверный формат для месяца!");
            }

            if (int.TryParse(y, out year) == false)
            {
                throw new Exception("Неверный формат для года!");
            }

            _day = day;
            _month = mounth;
            _year = year;
        }

        private int Maxday()
        {
            List<int> days = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (Leap()) days[1] = 29;
            return days[_month - 1];

        }

        private bool Leap()
        {
            return ((_year % 4 == 0 && _year % 100 != 0) || _year % 400 != 0);
        }
    }
}