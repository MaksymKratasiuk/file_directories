namespace file_directories
{
    internal class Task1
    {
        static List<Poem> poems = new List<Poem>();

        public static void task1()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Переглянути вміст файлу");
            Console.WriteLine("2. Додати вірш");
            Console.WriteLine("3. Видалити вірш");
            Console.WriteLine("4. Змінити інформацію про вірш");
            Console.WriteLine("5. Пошук вірша");
            Console.WriteLine("6. Зберегти колекцію віршів у файл");
            Console.WriteLine("7. Завантажити колекцію віршів з файлу");
            Console.WriteLine("8. Згенерувати звіт за назвою вірша");
            Console.WriteLine("9. Згенерувати звіт за ПІБ автора");
            Console.WriteLine("10. Згенерувати звіт за темою вірша");
            Console.WriteLine("11. Згенерувати звіт за словом у тексті вірша");
            Console.WriteLine("12. Згенерувати звіт за роком написання вірша");
            Console.WriteLine("13. Згенерувати звіт за довжиною вірша");
            Console.WriteLine("0. Вийти з програми");

            while (true)
            {
                Console.Write("Виберіть опцію: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Програма завершує роботу.");
                        return;
                    case 1:
                        DisplayPoems(poems);
                        break;
                    case 2:
                        AddPoem();
                        break;
                    case 3:
                        DeletePoem();
                        break;
                    case 4:
                        EditPoem();
                        break;
                    case 5:
                        SearchPoem();
                        break;
                    case 6:
                        SavePoemsToFile();
                        break;
                    case 7:
                        LoadPoemsFromFile();
                        break;
                    case 8:
                        GenerateReportByTitle();
                        break;
                    case 9:
                        GenerateReportByAuthor();
                        break;
                    case 10:
                        GenerateReportByTheme();
                        break;
                    case 11:
                        GenerateReportByWord();
                        break;
                    case 12:
                        GenerateReportByYear();
                        break;
                    case 13:
                        GenerateReportByLength();
                        break;
                    default:
                        Console.WriteLine("Невірний номер пункту меню. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddPoem()
        {
            Console.WriteLine("Додавання вірша");
            Console.Write("Назва вірша: ");
            string title = Console.ReadLine();
            Console.Write("П.І.Б. автора: ");
            string author = Console.ReadLine();
            Console.Write("Рік написання: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Текст вірша: ");
            string text = Console.ReadLine();
            Console.Write("Тема вірша: ");
            string theme = Console.ReadLine();

            poems.Add(new Poem(title, author, year, text, theme));
            Console.WriteLine("Вірш успішно додано до колекції.");
        }

        static void DeletePoem()
        {
            Console.WriteLine("Видалення вірша");
            Console.Write("Введіть назву вірша, який потрібно видалити: ");
            string title = Console.ReadLine();

            int removedCount = poems.RemoveAll(p => p.Title == title);
            if (removedCount > 0)
            {
                Console.WriteLine($"Видалено {removedCount} вірш(ів) з назвою \"{title}\".");
            }
            else
            {
                Console.WriteLine($"Вірш з назвою \"{title}\" не знайдено.");
            }
        }

        static void EditPoem()
        {
            Console.WriteLine("Зміна інформації про вірш");
            Console.Write("Введіть назву вірша, який потрібно змінити: ");
            string title = Console.ReadLine();

            Poem poem = poems.Find(p => p.Title == title);
            if (poem != null)
            {
                Console.WriteLine("Знайдено наступний вірш:");
                Console.WriteLine(poem);

                Console.WriteLine("Введіть нові дані:");
                Console.Write("Назва вірша: ");
                poem.Title = Console.ReadLine();
                Console.Write("П.І.Б. автора: ");
                poem.Author = Console.ReadLine();
                Console.Write("Рік написання: ");
                poem.Year = int.Parse(Console.ReadLine());
                Console.Write("Текст вірша: ");
                poem.Text = Console.ReadLine();
                Console.Write("Тема вірша: ");
                poem.Theme = Console.ReadLine();

                Console.WriteLine("Інформацію про вірш змінено.");
            }
            else
            {
                Console.WriteLine($"Вірш з назвою \"{title}\" не знайдено.");
            }
        }

        static void SearchPoem()
        {
            Console.WriteLine("Пошук вірша");
            Console.WriteLine("1. Пошук за назвою");
            Console.WriteLine("2. Пошук за автором");
            Console.WriteLine("3. Пошук за роком");
            Console.Write("Виберіть опцію: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть назву вірша: ");
                    string title = Console.ReadLine();
                    List<Poem> poemsByTitle = poems.FindAll(p => p.Title == title);
                    DisplayPoems(poemsByTitle);
                    break;
                case "2":
                    Console.Write("Введіть П.І.Б. автора: ");
                    string author = Console.ReadLine();
                    List<Poem> poemsByAuthor = poems.FindAll(p => p.Author == author);
                    DisplayPoems(poemsByAuthor);
                    break;
                case "3":
                    Console.Write("Введіть рік: ");
                    int year = int.Parse(Console.ReadLine());
                    List<Poem> poemsByYear = poems.FindAll(p => p.Year == year);
                    DisplayPoems(poemsByYear);
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }

        static void DisplayPoems(List<Poem> poems)
        {
            if (poems.Count > 0)
            {
                Console.WriteLine("Знайдено наступні вірші:");
                foreach (var poem in poems)
                {
                    Console.WriteLine(poem);
                }
            }
            else
            {
                Console.WriteLine("Вірші не знайдено.");
            }
        }

        static void SavePoemsToFile()
        {
            Console.Write("Введіть шлях до файлу: ");
            string path = Console.ReadLine();

            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        foreach (var poem in poems)
                        {
                            sw.WriteLine(poem.Title);
                            sw.WriteLine(poem.Author);
                            sw.WriteLine(poem.Year);
                            sw.WriteLine(poem.Text);
                            sw.WriteLine(poem.Theme);
                            sw.WriteLine();
                        }
                    }
                }

                Console.WriteLine("Колекцію віршів успішно збережено у файлі.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка при збереженні колекції віршів у файл: " + ex.Message);
            }
        }

        static void LoadPoemsFromFile()
        {
            Console.Write("Введіть шлях до файлу: ");
            string path = Console.ReadLine();

            if (File.Exists(path))
            {
                try
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            poems.Clear();

                            while (!sr.EndOfStream)
                            {
                                string title = sr.ReadLine();
                                string author = sr.ReadLine();
                                int year = int.Parse(sr.ReadLine());
                                string text = sr.ReadLine();
                                string theme = sr.ReadLine();

                                // Пропускаємо порожній рядок між віршами
                                sr.ReadLine();

                                poems.Add(new Poem(title, author, year, text, theme));
                            }
                        }
                    }

                    Console.WriteLine("Колекцію віршів успішно завантажено з файлу.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сталася помилка при завантаженні колекції віршів з файлу: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        static void GenerateReportByTitle()
        {
            Console.WriteLine("Генерація звіту за назвою вірша");
            Console.Write("Введіть назву для звіту: ");
            string reportName = Console.ReadLine();
            Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
            string filePath = Console.ReadLine();

            List<string> reportData = new List<string>();

            foreach (var poem in poems)
            {
                reportData.Add($"Назва вірша: {poem.Title}");
                reportData.Add($"Автор: {poem.Author}");
                reportData.Add($"Рік написання: {poem.Year}");
                reportData.Add($"Текст вірша: {poem.Text}");
                reportData.Add($"Тема вірша: {poem.Theme}");
                reportData.Add("");
            }

            GenerateReport(reportName, filePath, reportData);
        }

        static void GenerateReportByAuthor()
        {
            Console.WriteLine("Генерація звіту за ПІБ автора");
            Console.Write("Введіть П.І.Б. для звіту: ");
            string reportName = Console.ReadLine();
            Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
            string filePath = Console.ReadLine();

            List<string> reportData = new List<string>();

            foreach (var poem in poems)
            {
                reportData.Add($"Автор: {poem.Author}");
                reportData.Add($"Назва вірша: {poem.Title}");
                reportData.Add($"Рік написання: {poem.Year}");
                reportData.Add($"Текст вірша: {poem.Text}");
                reportData.Add($"Тема вірша: {poem.Theme}");
                reportData.Add("");
            }

            GenerateReport(reportName, filePath, reportData);
        }

        static void GenerateReportByTheme()
        {
            Console.WriteLine("Генерація звіту за темою вірша");
            Console.Write("Введіть тему для звіту: ");
            string reportName = Console.ReadLine();
            Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
            string filePath = Console.ReadLine();

            List<string> reportData = new List<string>();

            foreach (var poem in poems)
            {
                reportData.Add($"Тема вірша: {poem.Theme}");
                reportData.Add($"Назва вірша: {poem.Title}");
                reportData.Add($"Автор: {poem.Author}");
                reportData.Add($"Рік написання: {poem.Year}");
                reportData.Add($"Текст вірша: {poem.Text}");
                reportData.Add("");
            }

            GenerateReport(reportName, filePath, reportData);
        }

        static void GenerateReport(string reportName, string filePath, List<string> reportData)
        {
            Console.WriteLine($"Звіт \"{reportName}\":");
            Console.WriteLine();

            foreach (var data in reportData)
            {
                Console.WriteLine(data);
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    File.WriteAllLines(filePath, reportData);
                    Console.WriteLine($"Звіт успішно збережено у файлі \"{filePath}\".");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сталася помилка при збереженні звіту у файл: " + ex.Message);
                }
            }
        }
            static void GenerateReportByWord()
            {
                Console.WriteLine("Генерація звіту за словом у тексті вірша");
                Console.Write("Введіть слово для звіту: ");
                string word = Console.ReadLine();
                Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
                string filePath = Console.ReadLine();

                List<string> reportData = new List<string>();

                foreach (var poem in poems)
                {
                    if (poem.Text.Contains(word))
                    {
                        reportData.Add($"Назва вірша: {poem.Title}");
                        reportData.Add($"Автор: {poem.Author}");
                        reportData.Add($"Рік написання: {poem.Year}");
                        reportData.Add($"Текст вірша: {poem.Text}");
                        reportData.Add($"Тема вірша: {poem.Theme}");
                        reportData.Add("");
                    }
                }

                GenerateReport("Звіт за словом у тексті вірша", filePath, reportData);
            }

            static void GenerateReportByYear()
            {
                Console.WriteLine("Генерація звіту за роком написання вірша");
                Console.Write("Введіть рік для звіту: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
                string filePath = Console.ReadLine();

                List<string> reportData = new List<string>();

                foreach (var poem in poems)
                {
                    if (poem.Year == year)
                    {
                        reportData.Add($"Рік написання: {poem.Year}");
                        reportData.Add($"Назва вірша: {poem.Title}");
                        reportData.Add($"Автор: {poem.Author}");
                        reportData.Add($"Текст вірша: {poem.Text}");
                        reportData.Add($"Тема вірша: {poem.Theme}");
                        reportData.Add("");
                    }
                }

                GenerateReport("Звіт за роком написання вірша", filePath, reportData);
            }

            static void GenerateReportByLength()
            {
                Console.WriteLine("Генерація звіту за довжиною вірша");
                Console.Write("Введіть довжину для звіту: ");
                int length = int.Parse(Console.ReadLine());
                Console.Write("Введіть шлях до файлу або натисніть Enter, щоб вивести на екран: ");
                string filePath = Console.ReadLine();

                List<string> reportData = new List<string>();

                foreach (var poem in poems)
                {
                    if (poem.Text.Length == length)
                    {
                        reportData.Add($"Довжина вірша: {poem.Text.Length}");
                        reportData.Add($"Назва вірша: {poem.Title}");
                        reportData.Add($"Автор: {poem.Author}");
                        reportData.Add($"Рік написання: {poem.Year}");
                        reportData.Add($"Текст вірша: {poem.Text}");
                        reportData.Add($"Тема вірша: {poem.Theme}");
                        reportData.Add("");
                    }
                }

                GenerateReport("Звіт за довжиною вірша", filePath, reportData);
            }

        }
    


        class Poem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public int Year { get; set; }
            public string Text { get; set; }
            public string Theme { get; set; }

            public Poem(string title, string author, int year, string text, string theme)
            {
                Title = title;
                Author = author;
                Year = year;
                Text = text;
                Theme = theme;
            }

            public override string ToString()
            {
                return $"Назва вірша: {Title}\n" +
                       $"Автор: {Author}\n" +
                       $"Рік написання: {Year}\n" +
                       $"Текст вірша: {Text}\n" +
                       $"Тема вірша: {Theme}\n";
            }

        }
    
}
