using System;
using System.Collections.Generic;
using System.IO;

namespace WeekLessons
{

    public class WeekLessonsClass
    {
        ///Дни недели и часы занятий
        private List<string> daysOfLessons = new List<string>
        {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        };
        private List<string> hoursOfLessons = new List<string> 
        {
        "8:00-9:35",
        "9:45-11:20",
        "11:30-13:05",
        "13:25-15:00",
        "15:10-16:45"
        };
       
        /// <summary>
        /// Название недели
        /// </summary>
        public string NameOfWeek { get; set; }
        /// <summary>
        /// Точка отсчета
        /// </summary>
        private DateTime startingDate = DateTime.Parse("31.08.2020");
        /// <summary>
        /// Расписание занятий
        /// </summary>
        private List<string> namesOfLessons = new List<string>();
        /// <summary>
        /// Конструктор, при отсутствии файла бросает исключение
        /// </summary>
        public WeekLessonsClass()
        {
            DateTime nowDate = DateTime.Now.Date;
            int days = (nowDate - startingDate).Days / 7;
            if (days == 0 || days % 2 == 0)
            {
                NameOfWeek = "Числитель";
            }
            else
            {
                NameOfWeek = "Знаменатель"; 
            }
            try
            {
                using (StreamReader sr = File.OpenText($"{NameOfWeek}_{DateTime.Now.DayOfWeek.ToString()}.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        namesOfLessons.Add(line);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
            
        }
        /// <summary>
        /// Специальный конструктор для создания файлов расписания
        /// </summary>
        /// <param name="value"></param>
        public WeekLessonsClass(object value)
        {
            Console.WriteLine("Увы, но файлов расписания не существует");
            Console.WriteLine("Сейчас они будут созданы");
        }
        /// <summary>
        /// Вывод расписания
        /// </summary>
        public void PrintLessons()
        {
            Console.WriteLine($"Сегодня {DateTime.Now.Date.ToString()}");
            Console.WriteLine($"Эта неделя - {NameOfWeek}");
            Console.WriteLine("Расписание на сегодня:");
            for (int n = 0; n <namesOfLessons.Count; n++)
            {
                Console.WriteLine($"{hoursOfLessons[n]}  {namesOfLessons[n]}");
            }

        }
        /// <summary>
        /// Создание файлов расписания
        /// </summary>
        public void CreateNewFiles()
        {
            FileInfo fi;
            for (int i = 0; i < 5; i++)
            {
                fi = new FileInfo($"Числитель_{daysOfLessons[i]}.txt");
                fi.CreateText();
                
            }
            for (int i = 0; i < 5; i++)
            {
                fi = new FileInfo($"Знаменатель_{daysOfLessons[i]}.txt");
                fi.CreateText();
            }
        }
        /// <summary>
        /// Редактирование расписания
        /// </summary>
        public void CorrectLesson()
        {
            Console.WriteLine("Введите время того занятия,которое вы хотите заменить");
            string time = Console.ReadLine();
            for (int i = 0; i < 5; i++)
            {
                if (time==hoursOfLessons[i])
                {
                    Console.WriteLine("Введите название нового предмета");
                    namesOfLessons[i] = Console.ReadLine();
                    using (StreamWriter sw = new StreamWriter($"{NameOfWeek}_{DateTime.Now.DayOfWeek.ToString()}.txt", true))
                    {
                        sw.Flush();
                        for (int k = 0; k < namesOfLessons.Count; k++)
                        {
                            sw.WriteLine(namesOfLessons[k]);
                        }
                        sw.Close();
                    }
                    break;
                }
                
            }
        }
        /// <summary>
        /// Поиск расписания по дате (при неудачном открытии файла бросает исключение)
        /// </summary>
        public void FindLessonTable()
        {
            List<string> lessonTable = new List<string>();
            string str = NameOfWeek;
            Console.WriteLine("Введите дату, на которое ищете расписание (формат дд.мм.гггг)");
            DateTime date = DateTime.Parse(Console.ReadLine());
            int days = (date - startingDate).Days / 7;
            if (days == 0 || days % 2 == 0)
            {
                NameOfWeek = "Числитель";
            }
            else
            {
                NameOfWeek = "Знаменатель";
            }
            try
            {
                using (StreamReader sr = File.OpenText($"{NameOfWeek}_{date.DayOfWeek.ToString()}.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lessonTable.Add(line);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
            Console.WriteLine();
            Console.WriteLine($"Эта неделя - {NameOfWeek}");
            Console.WriteLine("Расписание на этот день:");
            Console.WriteLine();
            for (int n = 0; n < lessonTable.Count; n++)
            {
                Console.WriteLine($"{hoursOfLessons[n]}  {lessonTable[n]}");
            }
            Console.WriteLine();
            NameOfWeek = str;
        }







    }

}
