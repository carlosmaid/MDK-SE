﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDK.Services
{
    /// <summary>
    /// Provides helper methods for loading and saving a dictionary into a very simple equals-separated dictionary text file.
    /// </summary>
    public static class DictionaryFile
    {
        /// <summary>
        /// Loads a very simple equals-separated dictionary text file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Load(string fileName, IEqualityComparer<string> comparer = null)
        {
            return File.ReadAllLines(fileName)
                .Select(l => l.Trim())
                .Where(l => l.Length > 0)
                .Select(SplitIt)
                .ToDictionary(p => p.Key, p => p.Value, comparer ?? StringComparer.CurrentCulture);
        }

        static KeyValuePair<string, string> SplitIt(string value)
        {
            var index = value.IndexOf('=');
            if (index >= 0)
                return new KeyValuePair<string, string>(value.Substring(0, index).Trim(), value.Substring(index + 1).Trim());
            return new KeyValuePair<string, string>(value.Trim(), "");
        }

        /// <summary>
        /// Saves a very simple equals-separated dictionary text file.
        /// </summary>
        /// <param name="metaFileName"></param>
        /// <param name="content"></param>
        public static void Save(string metaFileName, Dictionary<string, string> content)
        {
            var text = string.Join(Environment.NewLine, content.Select(pair => $"{pair.Key}={pair.Value}"));
            File.WriteAllText(metaFileName, text);
        }
    }
}
