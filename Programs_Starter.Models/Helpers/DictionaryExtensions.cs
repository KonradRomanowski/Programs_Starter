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
        public static void InsertProgramToStart(this Dictionary<int, ProgramToStart> dict, ProgramToStart program, int index)
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

        public static void RemoveProgramToStart(this Dictionary<int, ProgramToStart> dict, int index)
        {
            if (dict == null)
                throw new ArgumentNullException();
            if (index <= 0)
                throw new ArgumentException("Index below or equal 0!");
            if (index > dict.Count)
                throw new ArgumentOutOfRangeException("Index is bigger then number of objects in dictionary!");

            List<KeyValuePair<int, ProgramToStart>> tempList = new List<KeyValuePair<int, ProgramToStart>>();

            foreach (var item in dict.OrderBy(x => x.Key))
            {
                if (item.Key > index)
                {
                    tempList.Add(item);
                    dict.Remove(item.Key);
                }
                else if (item.Key == index)
                {
                    dict.Remove(item.Key);
                }
            }

            foreach (var item in tempList.OrderBy(x => x.Key))
            {
                dict.Add(item.Key - 1, item.Value);
            }

            tempList.Clear();
        }

        public static void ChangeProgramToStartIndex(this Dictionary<int, ProgramToStart> dict, int oldIndex, int newIndex)
        {
            if (dict == null)
                throw new ArgumentNullException();
            if (oldIndex <= 0)
                throw new ArgumentException("oldIndex below or equal 0!");
            if (oldIndex > dict.Count)
                throw new ArgumentOutOfRangeException("oldIndex is bigger then number of objects in dictionary!");
            if (newIndex <= 0)
                throw new ArgumentException("newIndex below or equal 0!");
            if (newIndex > dict.Count)
                throw new ArgumentOutOfRangeException("newIndex is bigger then number of objects in dictionary!");

            ProgramToStart movedProgram = dict[oldIndex];
            dict.RemoveProgramToStart(oldIndex);
            dict.InsertProgramToStart(movedProgram, newIndex);
        }
    }
}
