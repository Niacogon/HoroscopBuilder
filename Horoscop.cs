using System.Drawing;
using System.Drawing.Text;
using System.Net;

Console.Title = "Horoscop";

while (true)
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    
    Console.WriteLine("Комманды\n");
    Console.WriteLine("Введите \"текст\" для прочтения гороскопа\n");
    Console.WriteLine("Введите Русскую \"г\" для создания гороскопа\n");
    Console.WriteLine("\"конец\" - выход\n");
    Console.WriteLine("введите команду");
    string? com = Console.ReadLine();
    switch (com)
    {
        case "текст":
            {
                PrintInfo();
                break;
            }

        case "г": 
        {
                
        Console.WriteLine("Введите Дизайн \"1\", \"2\", \"3\", \"4\"");
        string Dizign = Console.ReadLine();
        Console.WriteLine("Введите день \"с\" - сегодня  \"з\"- завтра");
        string dayG = Console.ReadLine();

        GetInfo(Dizign, dayG);
        Console.Clear();
        break;
        }

        case "конец":
            {
                Environment.Exit(0);
                break;
            }

        default:
            {
            Console.WriteLine("Введена неизвестная комманда");
            Thread.Sleep(1000);
            Console.Clear(); 
            }
 break;
	}
}


//сегодня
static void PrintInfo()
{
    string[] HtttpToday =
      {
                                                                // today
            "https://horo.mail.ru/prediction/aries/today/",           // Овен 
            "https://horo.mail.ru/prediction/taurus/today/",          // Телец   
            "https://horo.mail.ru/prediction/gemini/today/",          // Близнецы
            "https://horo.mail.ru/prediction/cancer/today/",          // Рак   
            "https://horo.mail.ru/prediction/leo/today/",             // Лев
            "https://horo.mail.ru/prediction/virgo/today/",           // Дева  
            "https://horo.mail.ru/prediction/virgo/today/",           // Весы  
            "https://horo.mail.ru/prediction/scorpio/today/",         // Скорпион 
            "https://horo.mail.ru/prediction/sagittarius/today/",     // Стрелец 
            "https://horo.mail.ru/prediction/capricorn/today/",       // Козерог
            "https://horo.mail.ru/prediction/aquarius/today/",        // Водолей 
            "https://horo.mail.ru/prediction/pisces/today/",          // Рыбы 
             };

    for (int i = 0; i < 12; i++)
    {
        WebRequest req = WebRequest.Create(HtttpToday[i]);
        req.Method = "GET";

        string source;
        using (StreamReader reader = new(req.GetResponse().GetResponseStream()))
        {
            source = reader.ReadToEnd();
        }

        // получение кода сайта && получения главного текста
        //---------------------------------------------------------------------
        string l = "<div class=\"article__item article__item_alignment_left article__item_html\">";
        source = source[(source.IndexOf(l) + l.Length)..];
        source = source.Remove(source.IndexOf("</div>"));
        //---------------------------------------------------------------------

        // очитка текстаот тегов 
        //---------------------------------------------------------------------
        var charsToRemove = new string[] { "<", ">", "&", "n", "b", "s", ";", "a", "d", "h" };
        foreach (var c in charsToRemove)
        {
            source = source.Replace(c, string.Empty);
        }

        int indText = source.IndexOf("/p");
        source = source.Remove(indText);

        var charsToRemove2 = new string[] { "p", "/" };
        foreach (var c in charsToRemove2)
        {
            source = source.Replace(c, string.Empty);
        }

        //данные
        //----------------------------------------------------------------------
        string[] MixHoro =
            { "Овен", "Телец", "Близнецы", "Рак", "Лев", "Дева", "Весы", "Скорпион", "Стрелец", "Козерог", "Водолей", "Рыбы" };
        string[] MixMood =
            {
            "Беспокойное","Бодрое","Боязливое","Веселое","Возбужденное","Возмущенное","Волшебное","Воспевающее","Восторженное",
            "Высмеивающее","Героическое","Грустная радость","Грустное","Добродушное","Добродушно-насмешливое","Жуткое","Загадочное","Задиристое"
            ,"Заносчивое","Капризное","Ликующее","Любование","Мечтательное","Мрачное","Насмешливое","Нежное","Нетерпеливое","Обиженное","Ожидание"
            ,"Печальное","Поучительное","Прихотливое","Прославляющее","Радостное","Радостная грусть","Робкое","Светлое","Сердитое","Серьезное"
            ,"Сказочное","Скорбное","Скрытное","Смешливое","Смешное","Солнечное","Сонное","Сосредоточенное","Сочувствующе","Сострадание","Спокойное"
            ,"Таинственное","Теплое","Тихое","Торжественное","Тоскливое","Тревожное","Уютное","Хвалебное","Хмурое","Шаловливое","Шумное","Шумливое"
        };
        string[] MixCollor =
        {
            "Красный","Оранжевый","Желтый","Зеленый","Голубой","Синий","Фиолетовый"
        };
        string[] MixTime =
        {
           "6 утра","7 утра","8 утра","9 утра","10 утра","11 утра",
           "12 дня","час дня","2 часа дня","3 часа дня","4 часа дня","5 часoв дня",
           "6 часа вечера","7 часов вечера","8 часов вечера","9 часов вечера","10 часов вечера","11 часов вечера",
           "12 часов ночи","час ночи"
        };

        //обьявление структуры
        //----------------------------------------------------------------------
        HoroSign[] ToDay = new HoroSign[12];
        ToDay[i].MainTxt(source);
      
        Random rnd = new();
        ToDay[i].Mixtxt(MixHoro[rnd.Next(0, 12)], MixMood[rnd.Next(0, 61)], rnd.Next(0, 100), MixCollor[rnd.Next(0, 6)], MixTime[rnd.Next(0, 20)], MixHoro[i]);
        ToDay[i].Print();
        Console.ReadLine();
        Console.Clear();
   }
}


static void GetInfo(string Dizgn,string dayG)
{
    string[] HtttpToday =
        {
                                                                // today
            "https://horo.mail.ru/prediction/aries/today/",           // Овен 
            "https://horo.mail.ru/prediction/taurus/today/",          // Телец   
            "https://horo.mail.ru/prediction/gemini/today/",          // Близнецы
            "https://horo.mail.ru/prediction/cancer/today/",          // Рак   
            "https://horo.mail.ru/prediction/leo/today/",             // Лев
            "https://horo.mail.ru/prediction/virgo/today/",           // Дева  
            "https://horo.mail.ru/prediction/virgo/today/",           // Весы  
            "https://horo.mail.ru/prediction/scorpio/today/",         // Скорпион 
            "https://horo.mail.ru/prediction/sagittarius/today/",     // Стрелец 
            "https://horo.mail.ru/prediction/capricorn/today/",       // Козерог
            "https://horo.mail.ru/prediction/aquarius/today/",        // Водолей 
            "https://horo.mail.ru/prediction/pisces/today/",          // Рыбы 
             };
    string[] HttpTomorrow =
       {                                                   // Tomorrow
            "https://horo.mail.ru/prediction/aries/tomorrow/",           // Овен 
            "https://horo.mail.ru/prediction/taurus/tomorrow/",          // Телец   
            "https://horo.mail.ru/prediction/gemini/tomorrow/",          // Близнецы
            "https://horo.mail.ru/prediction/cancer/tomorrow/",          // Рак   
            "https://horo.mail.ru/prediction/leo/tomorrow/",             // Лев
            "https://horo.mail.ru/prediction/virgo/tomorrow/",           // Дева  
            "https://horo.mail.ru/prediction/virgo/tomorrow//",           // Весы  
            "https://horo.mail.ru/prediction/scorpio/tomorrow/",         // Скорпион 
            "https://horo.mail.ru/prediction/sagittarius/tomorrow//",     // Стрелец 
            "https://horo.mail.ru/prediction/capricorn/tomorrow/",       // Козерог
            "https://horo.mail.ru/prediction/aquarius/tomorrow/",        // Водолей 
            "https://horo.mail.ru/prediction/pisces/tomorrow/",          // Рыбы 
    };



    for (int i = 0; i < 12; i++)
    {
        WebRequest req;
        if (dayG == "с")
        {
            req = WebRequest.Create(HtttpToday[i]);
        }
        else
        {
            req = WebRequest.Create(HttpTomorrow[i]);
        }
        req.Method = "GET";

        string source;
        using (StreamReader reader = new(req.GetResponse().GetResponseStream()))
        {
            source = reader.ReadToEnd();
        }

        // получение кода сайта && получения главного текста
        //---------------------------------------------------------------------
        string l = "<div class=\"article__item article__item_alignment_left article__item_html\">";
        source = source[(source.IndexOf(l) + l.Length)..];
        source = source.Remove(source.IndexOf("</div>"));
        //---------------------------------------------------------------------

        // очитка текстаот тегов 
        //---------------------------------------------------------------------
        var charsToRemove = new string[] { "<", ">", "&", "n", "b", "s", ";", "a", "d", "h" };
        foreach (var c in charsToRemove)
        {
            source = source.Replace(c, string.Empty);
        }

        int indText = source.IndexOf("/p");
        source = source.Remove(indText);

        var charsToRemove2 = new string[] { "p", "/" };
        foreach (var c in charsToRemove2)
        {
            source = source.Replace(c, string.Empty);
        }


        //данные
        //----------------------------------------------------------------------
        string[] MixHoro =
            {"Овен", "Телец", "Близнецы", "Рак", "Лев", "Дева", "Весы", "Скорпион", "Стрелец", "Козерог", "Водолей", "Рыбы" };
        string[] MixMood =
            {
            "Беспокойное","Бодрое","Боязливое","Веселое","Возбужденное","Возмущенное","Волшебное","Воспевающее","Восторженное",
            "Высмеивающее","Героическое","Грустная радость","Грустное","Добродушное","Жуткое","Загадочное","Задиристое"
            ,"Заносчивое","Капризное","Ликующее","Любование","Мечтательное","Мрачное","Насмешливое","Нежное","Нетерпеливое","Обиженное","Ожидание"
            ,"Печальное","Поучительное","Прихотливое","Прославляющее","Радостное","Радостная грусть","Робкое","Светлое","Сердитое","Серьезное"
            ,"Сказочное","Скорбное","Скрытное","Смешливое","Смешное","Солнечное","Сонное","Сосредоточенное","Сочувствующе","Сострадание","Спокойное"
            ,"Таинственное","Теплое","Тихое","Торжественное","Тоскливое","Тревожное","Уютное","Хвалебное","Хмурое","Шаловливое","Шумное","Шумливое"
        };
        string[] MixCollor =
        {
            "Красный","Оранжевый","Желтый","Зеленый","Голубой","Синий","Фиолетовый"
        };
        string[] MixTime =
        {
           "6 утра","7 утра","8 утра","9 утра","10 утра","11 утра",
           "12 дня","час дня","2 дня","3 дня","4 дня","5 дня",
           "6 вечера","7 вечера","8 вечера","9 вечера","10 вечера","11 вечера",
           "12 ночи","час ночи"
        };
        string[] Namefile =
                {
         "Овен.jpg", "Телец.jpg", "Близнецы.jpg", "Рак.jpg", "Лев.jpg", "Дева.jpg", "Весы.jpg", "Скорпион.jpg", "Стрелец.jpg", "Козерог.jpg", "Водолей.jpg", "Рыбы.jpg"
    };

        string[] fLocalDiz1 =
        {
        "C:\\Horoscop\\1diz\\1-1.jpg",
        "C:\\Horoscop\\1diz\\1-2.jpg",
        "C:\\Horoscop\\1diz\\1-3.jpg",
        "C:\\Horoscop\\1diz\\1-4.jpg",
        "C:\\Horoscop\\1diz\\1-5.jpg",
        "C:\\Horoscop\\1diz\\1-6.jpg",
        "C:\\Horoscop\\1diz\\1-7.jpg",
        "C:\\Horoscop\\1diz\\1-8.jpg",
        "C:\\Horoscop\\1diz\\1-9.jpg",
        "C:\\Horoscop\\1diz\\1-10.jpg",
        "C:\\Horoscop\\1diz\\1-11.jpg",
        "C:\\Horoscop\\1diz\\1-12.jpg",
    };
        string[] fLocalDiz2 =
        {
        "C:\\Horoscop\\2diz\\2-1.jpg",
        "C:\\Horoscop\\2diz\\2-2.jpg",
        "C:\\Horoscop\\2diz\\2-3.jpg",
        "C:\\Horoscop\\2diz\\2-4.jpg",
        "C:\\Horoscop\\2diz\\2-5.jpg",
        "C:\\Horoscop\\2diz\\2-6.jpg",
        "C:\\Horoscop\\2diz\\2-7.jpg",
        "C:\\Horoscop\\2diz\\2-8.jpg",
        "C:\\Horoscop\\2diz\\2-9.jpg",
        "C:\\Horoscop\\2diz\\2-10.jpg",
        "C:\\Horoscop\\2diz\\2-11.jpg",
        "C:\\Horoscop\\2diz\\2-12.jpg",
    };
        string[] fLocalDiz3 =
        {
        "C:\\Horoscop\\3diz\\3-1.jpg",
        "C:\\Horoscop\\3diz\\3-2.jpg",
        "C:\\Horoscop\\3diz\\3-3.jpg",
        "C:\\Horoscop\\3diz\\3-4.jpg",
        "C:\\Horoscop\\3diz\\3-5.jpg",
        "C:\\Horoscop\\3diz\\3-6.jpg",
        "C:\\Horoscop\\3diz\\3-7.jpg",
        "C:\\Horoscop\\3diz\\3-8.jpg",
        "C:\\Horoscop\\3diz\\3-9.jpg",
        "C:\\Horoscop\\3diz\\3-10.jpg",
        "C:\\Horoscop\\3diz\\3-11.jpg",
        "C:\\Horoscop\\3diz\\3-12.jpg",
    };
        string[] fLocalDiz4 =
        {
        "C:\\Horoscop\\4diz\\4-1.jpg",
        "C:\\Horoscop\\4diz\\4-2.jpg",
        "C:\\Horoscop\\4diz\\4-3.jpg",
        "C:\\Horoscop\\4diz\\4-4.jpg",
        "C:\\Horoscop\\4diz\\4-5.jpg",
        "C:\\Horoscop\\4diz\\4-6.jpg",
        "C:\\Horoscop\\4diz\\4-7.jpg",
        "C:\\Horoscop\\4diz\\4-8.jpg",
        "C:\\Horoscop\\4diz\\4-9.jpg",
        "C:\\Horoscop\\4diz\\4-10.jpg",
        "C:\\Horoscop\\4diz\\4-11.jpg",
        "C:\\Horoscop\\4diz\\4-12.jpg",
    };

        //обьявление структуры
        //----------------------------------------------------------------------
        HoroSign[] ToDay = new HoroSign[12];
        ToDay[i].MainTxt(source);

        Random rnd = new();
        ToDay[i].Mixtxt(MixHoro[rnd.Next(0, 12)], MixMood[rnd.Next(0, 61)], rnd.Next(0, 100), MixCollor[rnd.Next(0, 6)], MixTime[rnd.Next(0, 20)], MixHoro[i]);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(ToDay[i].name + ".png создан");

        // печать
        //-----------------------------------------------------------------------
        static void LoadFont()
        {
            PrivateFontCollection custom_Font = new();
        }

        Image FRF = Dizgn switch
        {
            "1" => Image.FromFile(fLocalDiz1[i]),
            "2" => Image.FromFile(fLocalDiz2[i]),
            "3" => Image.FromFile(fLocalDiz3[i]),
            "4" => Image.FromFile(fLocalDiz4[i]),
            //_ => Image.FromFile(fLocalDiz3[i]),
        };
        using var gr = Graphics.FromImage(FRF);
        // MAIN
        //_______________________________________________________________________
        FontFamily[] fontFamilies;
        PrivateFontCollection privateFontCollection = new();
        privateFontCollection.AddFontFile("C:\\Horoscop\\font\\AttackType-Regular.ttf");
        privateFontCollection.AddFontFile("C:\\Horoscop\\font\\AtypDisplay-Regular.ttf");
        fontFamilies = privateFontCollection.Families;
        Font Myfont = new(fontFamilies[1], 26);

        if (Dizgn == "1" || Dizgn == "2")
        {
            var colOR = Brushes.White;
            var colORdop = Brushes.DarkBlue;
           if (Dizgn == "1") {colOR = Brushes.Black; colORdop = Brushes.DarkOrange; }

                PointF MyPoint1 = new()
                {
                    X = 95,
                    Y = 235
                };
                SizeF addSize = new(350, 700);
                RectangleF Size = new(MyPoint1, addSize);
                gr.DrawString(ToDay[i].MainText, Myfont, colOR, Size);

                // 1 BOX
                //_______________________________________________________________________
                MyPoint1.X = 585;
                MyPoint1.Y = 300;
                addSize = new(699, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString("Лучшее сочетание:\n          " + ToDay[i].bestComb, Myfont, colOR, Size);

                MyPoint1.X = 625;
                MyPoint1.Y = 410;
                addSize = new(699, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString("Настроение:\n" + ToDay[i].mood, Myfont, colOR, Size);

                MyPoint1.X = 585;
                MyPoint1.Y = 530;
                addSize = new(699, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString("Счастливое число:\n              " + ToDay[i].bestnumber, Myfont, colOR, Size);

                // 2 box 
                //_________________________________________________________________________________________________________________
                MyPoint1.X = 520;
                MyPoint1.Y = 750;
                addSize = new(599, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString("Цвет: " + ToDay[i].favoritecolor, Myfont, colORdop, Size);


                MyPoint1.X = 520;
                MyPoint1.Y = 820;
                addSize = new(599, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString("Сачсливое время суток:\n" + ToDay[i].besttime, Myfont, colORdop, Size);


            //Time 
            //___________________________________________________________________________________________________
            int day;
            string Date;
            string[] MS = {
            " января", " февраля", " марта", " апреля", " майя", " июня", " июля", " августа", " сентября", " октября", " ноября", " декабря" };

            LoadFont();
            Font MyfontTH = new(fontFamilies[0], 60);

            if (dayG == "с")
            {
                day = DateTime.Now.Day;
                Date = day.ToString();
                Date += MS[DateTime.Now.Month];

                MyPoint1.X = 685;
                MyPoint1.Y = 85;
                addSize = new(350, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString(Date, MyfontTH, colOR, Size);
            }
            else
            {
                DateTime dayNext = DateTime.Now.AddDays(1);
                Date = dayNext.Day.ToString();

                if (dayNext.Day == 0)
                {
                    Date += MS[DateTime.Now.Month + 1];
                }
                else
                {
                    Date += MS[DateTime.Now.Month];
                }

                MyPoint1.X = 685;
                MyPoint1.Y = 85;
                addSize = new(350, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString(Date, MyfontTH, colOR, Size);
            }
        }
        else
        {
            var colOR = Brushes.White;
            var colORdop = Brushes.DarkOrange;
            if (Dizgn == "4") {colOR = Brushes.Black; colORdop = Brushes.DarkBlue; }

            PointF MyPoint1 = new()
            {
                X = 101,
                Y = 231
            };
            SizeF addSize = new(800, 350);
            RectangleF Size = new(MyPoint1, addSize);
            gr.DrawString(ToDay[i].MainText, Myfont, colOR, Size);

            // 1 BOX
            //_______________________________________________________________________
            Myfont = new(fontFamilies[1], 23);
            MyPoint1.X = 555;
            MyPoint1.Y = 620;
            addSize = new(699, 700);
            Size = new(MyPoint1, addSize);
            gr.DrawString("Лучшее сочетание:\n         " + ToDay[i].bestComb, Myfont, colOR, Size);

            MyPoint1.X = 595;
            MyPoint1.Y = 700;
            addSize = new(699, 700);
            Size = new(MyPoint1, addSize);
            gr.DrawString("Настроение:\n" + ToDay[i].mood, Myfont, colOR, Size);

            MyPoint1.X = 555;
            MyPoint1.Y = 790;
            addSize = new(699, 700);
            Size = new(MyPoint1, addSize);
            gr.DrawString("Счастливое число:\n             " + ToDay[i].bestnumber, Myfont, colOR, Size);

            // 2 box 
            //_________________________________________________________________________________________________________________
            MyPoint1.X = 120;
            MyPoint1.Y = 585;
            addSize = new(599, 700);
            Size = new(MyPoint1, addSize);
            gr.DrawString("Цвет: " + ToDay[i].favoritecolor, Myfont, colORdop, Size);


            MyPoint1.X = 120;
            MyPoint1.Y = 655;
            addSize = new(599, 700);
            Size = new(MyPoint1, addSize);
            gr.DrawString("Сачсливое\nвремя суток:\n" + ToDay[i].besttime, Myfont, colORdop, Size);

            //Time 
            //___________________________________________________________________________________________________
            int day;
            string Date;
            string[] MS = {
            " января", " февраля", " марта", " апреля", " майя", " июня", " июля", " августа", " сентября", " октября", " ноября", " декабря" };

            LoadFont();
            Font MyfontTH = new(fontFamilies[0], 60);

            if (dayG == "с")
            {
                day = DateTime.Now.Day;
                Date = day.ToString();
                Date += MS[DateTime.Now.Month];

                MyPoint1.X = 685;
                MyPoint1.Y = 85;
                addSize = new(350, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString(Date, MyfontTH, colOR, Size);
            }
            else
            {
                DateTime dayNext = DateTime.Now.AddDays(1);
                Date = dayNext.Day.ToString();

                if (dayNext.Day == 0)
                {
                    Date += MS[DateTime.Now.Month + 1];
                }
                else
                {
                    Date += MS[DateTime.Now.Month];
                }

                MyPoint1.X = 685;
                MyPoint1.Y = 85;
                addSize = new(350, 700);
                Size = new(MyPoint1, addSize);
                gr.DrawString(Date, MyfontTH, colOR, Size);
            }
        }

        //save
        //____________________________________________________________________________________________________
        var path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),// путь сохранения
                Namefile[i]);
        FRF.Save(path);
    }
}

public struct HoroSign
{
    public string name;
    public string MainText { get; set; }                 // Основной текст
    public string bestComb;                 // Лучшее сочетание
    public string mood;                     // настроение
    public int bestnumber;                 // счасливое число
    public string favoritecolor;            // любимый цвет
    public string besttime;                 // лучшее время
    
    public void Mixtxt( string BComb, string Mood, int Bestn, string favoriteC, string bestT, string MixHoro) // Основной текст
    {
        
        bestComb = BComb;
        name = MixHoro;
        mood = Mood;
        bestnumber = Bestn;
        favoritecolor = favoriteC;
        besttime = bestT;
    }
   public void MainTxt(string Mtxt)
    {
        MainText = Mtxt;
    }
    public void Print()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" ");
        Console.WriteLine(name);
        Console.WriteLine(" ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{MainText}");
        Console.WriteLine(" ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"лучшее сочетание: {bestComb}");
        Console.WriteLine(" ");
        Console.WriteLine($"настоение: {mood}");
        Console.WriteLine(" ");
        Console.WriteLine($"счасливое число: {bestnumber}");
        Console.WriteLine(" ");
        Console.WriteLine($"счастливый цвет: {favoritecolor}");
        Console.WriteLine(" ");
        Console.WriteLine($"счастливое время: {besttime}");
    }
}
//2767