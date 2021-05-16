using System;
using System.IO;

namespace MasterSystem.Classes
{
    /// <summary>
    /// Класс работы с файловой системой
    /// </summary>
    public class FileOperator
    {
        /// <summary>
        /// Читает файл
        /// </summary>
        /// <param name="filePath">Адрес файла</param>
        /// <returns>Результат в стороковом представлении</returns>
        public string ReadTextFromFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine($"{filePath} file doesn't exists");
                return string.Empty;
            }
        }

        /// <summary>
        /// Записывает строку в файл
        /// </summary>
        /// <param name="filePath">Адрес файла</param>
        /// <param name="content">Записыаемое содержимое
        /// </param>
        public void WriteTextToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine($"{filePath} file doesn't exists");
            }
        }
    }
}