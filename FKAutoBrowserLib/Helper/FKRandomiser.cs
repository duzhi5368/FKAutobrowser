//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-1
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    public class FKRandomiser
    {
        private IEnumerable<string> lastNames;
        private IEnumerable<string> firstNames;
        private const string firstNamesResource = "FKAutoBrowser.Helper.CSV_Database_of_First_Names.csv";
        private const string lastNamesResource = "FKAutoBrowser.Helper.CSV_Database_of_Last_Names.csv";

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="withSpaces"></param>
        /// <returns></returns>
        public string GetAlphabeticalText(int minLength, int maxLength, bool withSpaces)
        {
            string output = string.Empty;
            string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // 若需要添加空格
            if (withSpaces)
            {
                letters += "      ";
            }
            Random rand = new Random();
            for (int x = minLength; x <= maxLength; x++)
            {
                output += letters.Substring(rand.Next(letters.Length), 1);
            }
            return output;
        }
        /// <summary>
        /// 返回随机整数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetNumber(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
        /// <summary>
        /// 返回一个随机的西方FirstName
        /// Returns a random (Western) first name
        /// </summary>
        /// <returns></returns>
        public string GetWesternFirstName()
        {
            if (firstNames == null || !firstNames.Any())
            {
                LoadWesternFirstNames();
            }

            Random rand = new Random();
            return firstNames.ElementAt(rand.Next(firstNames.Count()));
        }

        /// <summary>
        /// 加载CSV
        /// Loads the first names from the CSV into memory
        /// </summary>
        private void LoadWesternFirstNames()
        {
            List<string> names = new List<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(firstNamesResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    names.Add(reader.ReadLine());
                }
            }
            firstNames = names;
        }

        /// <summary>
        /// 返回一个随机的西方LastName
        /// Returns a random (Western) last name
        /// </summary>
        /// <returns></returns>
        public string GetWesternLastName()
        {
            if (lastNames == null || !lastNames.Any())
            {
                LoadWesternLastNames();
            }

            Random rand = new Random();
            return lastNames.ElementAt(rand.Next(lastNames.Count()));
        }

        /// <summary>
        /// 加载CSV
        /// Loads the last names from the CSV into memory
        /// </summary>
        private void LoadWesternLastNames()
        {
            List<string> names = new List<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(lastNamesResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    names.Add(reader.ReadLine());
                }
            }
            lastNames = names;
        }
    }
}
