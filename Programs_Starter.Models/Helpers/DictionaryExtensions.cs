using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programs_Starter.Models.Helpers
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Inserts ProgramToStart with given index as Key, increments all key which are equal or greater
        /// </summary>
        /// <param name="dict">Dictionary with programs to start</param>
        /// <param name="program">New program to be insterted into dictionary</param>
        /// <param name="index">index on which new program should be inserted</param>
        public static void Insert(this Dictionary<int, ProgramToStart> dict, ProgramToStart program, int index)
        {
            if (program == null || dict == null)
                throw new ArgumentNullException();
            if (index <= 0)
                throw new ArgumentException("Index below or equal 0!");
            

            List<KeyValuePair<int, ProgramToStart>> tempList = new List<KeyValuePair<int, ProgramToStart>>();

            foreach (var item in dict.OrderBy(x => x.Key))
            {
                if (item.Key >= index)
                {
                    tempList.Add(item);
                    dict.Remove(item.Key);
                }
            }

            dict.Add(index, program);

            foreach (var item in tempList.OrderBy(x => x.Key))
            {
                dict.Add(item.Key + 1, item.Value);
            }

            tempList.Clear();
        }
    }
}
