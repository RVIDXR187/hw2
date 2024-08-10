using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;
namespace hw2
{
    internal class Program
    {
        static void Divider() 
        {
            Console.WriteLine(new string ('-', 12));
        }
        static void Task1_A()
        {
            Console.WriteLine("Task1_A");
            Divider();

            string input = "World";
            char[] chararray = input.ToCharArray();
            ReverseArray(chararray);

            string reversed = new string(chararray);
            Console.WriteLine(reversed);
        }
        static void ReverseArray(char[] array)
        {
            int left = 0,
            right = array.Length - 1;

            while (left < right)
            {
                char temp = array[left];
                array[left] = array[right];
                array[right] = temp;

                left++;
                right--;
            }
        }
        static void Task1_B() 
        {
            Console.WriteLine("Task1_B");
            Divider();

            int[] array = { 1, 2, 3, 4, 5, 6, 7 };
        
        ReverseArray(array);
        
        Console.WriteLine(string.Join(", ", array));
        }
        static void ReverseArray(int[] array) 
        {
            int left = 0;
            int right = array.Length - 1;

            while (left < right)
            { 
            int temp = array[left];               
            array[left] = array[right];
            array[right] = temp;

            left++;
            right--;
            }
        }
        static void Task2() 
        {
            string input = "late , safari , satellite , acquaintance, late-night-tip";
            List<string> forbiddenwords = new List<string> { "late , safari , satellite" };

            string filtred = FilterFobriddenWords(input, forbiddenwords);

            Console.WriteLine(filtred);
        }
        static string FilterFobriddenWords(string input, List<string> forbiddenwords) 
        {
            string pattern = @"\b(" + string.Join("|", forbiddenwords) + @")\b";

            string result = Regex.Replace(input, pattern, m => new string('*', m.Length), RegexOptions.IgnoreCase);

            return result;
        }
        static void Task3() 
        {
            Console.Write("Введіть кількість символів: ");
            if (int.TryParse(Console.ReadLine(), out int lenght) && lenght > 0)
            {
                string randomString = GenerateRandomString(lenght);
                Console.Write("Випадковий рядок: ");
                Console.WriteLine(randomString);
            }
            else 
            {
                Console.WriteLine("Некоректне введення. Будь ласка, введіть додатнє ціле число.");
            }
        }
        static string GenerateRandomString(int lenght) 
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random rand = new Random();

            for (int i = 0; i < lenght; i++) 
            {
                char randomChar = chars[rand.Next(chars.Length)];
                stringBuilder.Append(randomChar);
            }
            return stringBuilder.ToString();
        }
        static void Task4() 
        {
            int[] array = { 2, 0, 5, 4 };
            int missingNumber = FindMissingNumber(array);
            Console.WriteLine($"Пропущене число: {missingNumber}");
        }
        static int FindMissingNumber(int[] array) 
        {
        int n = array.Length;
        int expectedSum = n * (n + 1) / 2;
        int actualSum = 0;

        for (int i = 0; i < n; i++) 
            {
            actualSum += array[i];
            }
        return expectedSum - actualSum;
        }
        static void Task5() 
        {
            string dna = "AAACCCGGTTTAAAGG";

            string compressedDna = CompressDna(dna);
            Console.WriteLine($"Компресована ДНК: {compressedDna}");

            string decompressedDna = DecompressDna(compressedDna);
            Console.WriteLine($"Декомпресована ДНК: {decompressedDna}");
        }
        static string CompressDna(string dna) 
        {
            if (string.IsNullOrEmpty(dna))
                return "";

            StringBuilder compressed = new StringBuilder();
            char currentChar = dna[0];
            int count = 1;

            for(int i = 1; i < dna.Length; i++)
            {
            if (dna[i]  == currentChar) 
                {
                count++;
                }
                else
                {
                    compressed.Append(currentChar);
                    compressed.Append(count);
                    currentChar = dna[i];
                    count = 1;
                }
            }
            compressed.Append(currentChar);
            compressed.Append(count);

            return compressed.ToString();
        }
        static string DecompressDna(string compressedDna) 
        {
            if (string.IsNullOrEmpty(compressedDna))
                return "";

            StringBuilder decompressed = new StringBuilder();
            for (int i = 0; i < compressedDna.Length; i += 2) 
            {
            char nucleotide = compressedDna[i];
                int count = int.Parse(compressedDna[i + 1].ToString());

                decompressed.Append(nucleotide, count);
            }
            return decompressed.ToString();
        }
        static void Task6() 
        {
            string originaltext = "Тестове слово";
            using (Aes aes = Aes.Create()) 
            {
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                byte[] encrypted = Encrypt(originaltext, key, iv);
                Console.WriteLine($"Зашифрований текст: {Convert.ToBase64String(encrypted)}");

                string decrypted = Decrypt(encrypted, key, iv);
                Console.WriteLine($"Розшифрований текст: {decrypted}");
            }
        }
        static byte[] Encrypt(string palneText, byte[] key, byte[] iv) 
        {
            using (Aes aes = Aes.Create()) 
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) 
                    {
                        using (StreamWriter sw = new StreamWriter(cs)) 
                        {
                            sw.WriteLine(palneText);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }
        static string Decrypt(byte[] cipherText, byte[] key, byte[] iv) 
        {
            using (Aes aes = Aes.Create()) 
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText)) 
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) 
                    {
                        using (StreamReader sr = new StreamReader(cs)) 
                        {
                        return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Task1_A();
            Divider();

            Task1_B();
            Divider();

            Task2();
            Divider();

            Task3();
            Divider();

            Task4();
            Divider();

            Task5();
            Divider();

            Task6();
            Divider();
        }
    }
}
