using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab8CSharp
{
    class Task1_14
    {
        private readonly string _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lab8CSharp");

        public void Run()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 1.14: Координати векторів ===");
            Directory.CreateDirectory(_basePath);

            string inputFile = Path.Combine(_basePath, "input1_14.txt");
            string outputFile = Path.Combine(_basePath, "output1_14.txt");
            string modFile = Path.Combine(_basePath, "modified1_14.txt");

            if (!File.Exists(inputFile))
                File.WriteAllText(inputFile,
                    "Вектори: a=(1.5, -3), b=(0, 4, 7), c=(10, 20.5). Кінець.",
                    Encoding.UTF8);

            string text = File.ReadAllText(inputFile, Encoding.UTF8);

            string pattern =
                @"\(\s*[-+]?\d+(\.\d+)?\s*,\s*[-+]?\d+(\.\d+)?" +
                @"(\s*,\s*[-+]?\d+(\.\d+)?)?\s*\)";

            MatchCollection matches = Regex.Matches(text, pattern);
            Console.WriteLine($"Знайдено векторів: {matches.Count}");

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
                foreach (Match m in matches)
                    writer.WriteLine(m.Value);

            string modified = Regex.Replace(text, pattern, "[0,0,0]",
                                            RegexOptions.None, TimeSpan.FromSeconds(1));
            File.WriteAllText(modFile, modified, Encoding.UTF8);
            Console.WriteLine("Усі вектори замінено на [0,0,0]");
            Console.WriteLine($"Знайдені вектори у: {outputFile}");
            Console.WriteLine($"Змінений текст у:   {modFile}");
        }
    }

    class Task2_14
    {
        private readonly string _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lab8CSharp");

        public void Run()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 2.14: Видалення ідентифікаторів ===");
            Directory.CreateDirectory(_basePath);

            string inputFile = Path.Combine(_basePath, "input2_14.txt");
            string outputFile = Path.Combine(_basePath, "output2_14.txt");

            if (!File.Exists(inputFile))
                File.WriteAllText(inputFile,
                    "int x = 10; string name_var = \"test\"; double value_2 = 3.14;",
                    Encoding.UTF8);

            string text = File.ReadAllText(inputFile, Encoding.UTF8);

            string pattern = @"\b[a-zA-Z_][a-zA-Z0-9_]*\b";

            MatchCollection matches = Regex.Matches(text, pattern);
            Console.WriteLine($"Знайдено ідентифікаторів: {matches.Count}");

            string result = Regex.Replace(text, pattern, "");

            result = Regex.Replace(result, @" {2,}", " ").Trim();

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
                writer.Write(result);

            Console.WriteLine($" Ідентифікатори видалено.");
            Console.WriteLine($"  Результат: {result}");
            Console.WriteLine($" Файл: {outputFile}");
        }
    }

    class Task3_14
    {
        private readonly string _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lab8CSharp");

        public void Run()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 3.14: Вставка тексту після кожного входження слова ===");
            Directory.CreateDirectory(_basePath);

            string inputFile = Path.Combine(_basePath, "input3_14.txt");
            string outputFile = Path.Combine(_basePath, "output3_14.txt");

            if (!File.Exists(inputFile))
                File.WriteAllText(inputFile,
                    "Це текст. Слово тут. Ще слово.",
                    Encoding.UTF8);

            string text1 = File.ReadAllText(inputFile, Encoding.UTF8);

            string text2 = " [ВСТАВЛЕНО] ";

            Console.Write("Введіть слово для пошуку: ");
            string word = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("✗ Слово не введено");
                return;
            }

            string pattern = $@"\b{Regex.Escape(word)}\b";

            int count = Regex.Matches(text1, pattern, RegexOptions.IgnoreCase).Count;
            Console.WriteLine($" Знайдено входжень слова \"{word}\": {count}");

            string result = Regex.Replace(text1, pattern,
                m => m.Value + text2,
                RegexOptions.IgnoreCase);

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
                writer.Write(result);

            Console.WriteLine($" Результат: {result}");
            Console.WriteLine($" Записано у: {outputFile}");
        }
    }

    class Task4_14
    {
        private readonly string _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lab8CSharp");

        public void Run()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 4.14: Двійковий файл (без розділових знаків) ===");
            Directory.CreateDirectory(_basePath);

            Console.Write("Введіть речення: ");
            string sentence = Console.ReadLine();
            if (string.IsNullOrEmpty(sentence))
            {
                Console.WriteLine("✗ Речення не введено");
                return;
            }

            string outputFile = Path.Combine(_basePath, "output4_14.dat");

            string cleaned = Regex.Replace(sentence, @"\p{P}", "");

            byte[] bytes = Encoding.UTF8.GetBytes(cleaned);

            using (BinaryWriter writer = new BinaryWriter(
                       File.Open(outputFile, FileMode.Create)))
            {
                foreach (byte b in bytes)
                    writer.Write(b);
            }

            using (BinaryReader reader = new BinaryReader(
                       File.Open(outputFile, FileMode.Open)))
            {
                long length = reader.BaseStream.Length;
                byte[] readBytes = reader.ReadBytes((int)length);
                string content = Encoding.UTF8.GetString(readBytes);
                Console.WriteLine($"\n Вміст файлу (без розділових знаків): {content}");
                Console.WriteLine($"  Записано байтів: {length}");
            }

            Console.WriteLine($" Файл: {outputFile}");
        }
    }

    class Task5
    {
        private readonly string _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lab8CSharp");

        public void Run()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 5: Робота з файловою системою ===");
            Directory.CreateDirectory(_basePath);

            Console.Write("Введіть прізвище (латиницею): ");
            string surname = Console.ReadLine()?.Trim() ?? "Student";

            foreach (char c in Path.GetInvalidFileNameChars())
                surname = surname.Replace(c, '_');

            string dir1 = Path.Combine(_basePath, surname + "1");
            string dir2 = Path.Combine(_basePath, surname + "2");
            string dirALL = Path.Combine(_basePath, "ALL");

            SafeDelete(dir1); SafeDelete(dir2); SafeDelete(dirALL);

            Directory.CreateDirectory(dir1);
            Directory.CreateDirectory(dir2);
            Console.WriteLine($" Створено папки: {surname}1, {surname}2");

            string t1 = Path.Combine(dir1, "t1.txt");
            string t2 = Path.Combine(dir1, "t2.txt");

            File.WriteAllText(t1,
                "[Шевченко Степан Іванович, 2001] року народження, місце проживання [м. Суми]",
                Encoding.UTF8);
            File.WriteAllText(t2,
                "[Комар Сергій Федорович, 2000] року народження, місце проживання [м. Київ]",
                Encoding.UTF8);
            Console.WriteLine("✓ Створено t1.txt, t2.txt");

            string t3 = Path.Combine(dir2, "t3.txt");
            File.WriteAllText(t3,
                File.ReadAllText(t1, Encoding.UTF8) + "\n" +
                File.ReadAllText(t2, Encoding.UTF8),
                Encoding.UTF8);
            Console.WriteLine("Створено t3.txt");

]            Console.WriteLine("\nІнформація про файли:");
            PrintInfo(t1); PrintInfo(t2); PrintInfo(t3);

            File.Move(t2, Path.Combine(dir2, "t2.txt"));
            Console.WriteLine("t2.txt перенесено у папку 2");

            File.Copy(t1, Path.Combine(dir2, "t1.txt"), true);
            Console.WriteLine("t1.txt скопійовано у папку 2");

            Directory.Move(dir2, dirALL);
            Directory.Delete(dir1, true);
            Console.WriteLine($"{surname}2 → ALL, {surname}1 видалено");

            Console.WriteLine("\nФайли в папці ALL:");
            foreach (string f in Directory.GetFiles(dirALL))
                PrintInfo(f);

            Console.WriteLine($"\nЗавдання 5 виконано! Папка: {dirALL}");
        }

        private void SafeDelete(string path)
        {
            try
            {
                if (Directory.Exists(path)) Directory.Delete(path, true);
                else if (File.Exists(path)) File.Delete(path);
            }
            catch { }
        }

        private void PrintInfo(string filePath)
        {
            FileInfo fi = new(filePath);
            Console.WriteLine(
                $" {fi.Name} | {fi.Length} байт | створено: {fi.CreationTime:dd.MM HH:mm}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №8 ===");
            Console.WriteLine("1 - Завдання 1.14 (координати векторів)");
            Console.WriteLine("2 - Завдання 2.14 (видалення ідентифікаторів)");
            Console.WriteLine("3 - Завдання 3.14 (вставка тексту після слова)");
            Console.WriteLine("4 - Завдання 4.14 (двійковий файл без пунктуації)");
            Console.WriteLine("5 - Завдання 5   (файлова система)");
            Console.Write("\nОберіть завдання (1-5): ");

            switch (Console.ReadLine())
            {
                case "1": new Task1_14().Run(); break;
                case "2": new Task2_14().Run(); break;
                case "3": new Task3_14().Run(); break;
                case "4": new Task4_14().Run(); break;
                case "5": new Task5().Run(); break;
                default: Console.WriteLine("Невірний вибір!"); break;
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
