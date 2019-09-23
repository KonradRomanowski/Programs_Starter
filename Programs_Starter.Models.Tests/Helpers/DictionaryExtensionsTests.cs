using NUnit.Framework;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Tests.Helpers
{
    [TestFixture]
    class DictionaryExtensionsTests
    {
        [TestCase]
        public void InsertProgramToStartTest1()
        {
            Dictionary<int, ProgramToStart> testDict = new Dictionary<int, ProgramToStart>();
            ProgramToStart program1 = new ProgramToStart("test1", "D:\test1.txt");
            testDict.Add(1, program1);
            ProgramToStart program2 = new ProgramToStart("test2", "D:\test2.txt");
            testDict.Add(2, program2);
            ProgramToStart program3 = new ProgramToStart("test3", "D:\test3.txt");
            testDict.Add(3, program3);

            ProgramToStart testProgram = new ProgramToStart("testProgram", "D:\testProgram.txt");
            testDict.InsertProgramToStart(testProgram, 2);

            Assert.AreEqual(4, testDict.Count);
            Assert.AreEqual(testProgram, testDict[2]);
            Assert.AreEqual(program2, testDict[3]);
            Assert.AreEqual(program3, testDict[4]);
        }

        [TestCase]
        public void RemoveProgramToStartTest1()
        {
            Dictionary<int, ProgramToStart> testDict = new Dictionary<int, ProgramToStart>();
            ProgramToStart program1 = new ProgramToStart("test1", "D:\test1.txt");
            testDict.Add(1, program1);
            ProgramToStart program2 = new ProgramToStart("test2", "D:\test2.txt");
            testDict.Add(2, program2);
            ProgramToStart program3 = new ProgramToStart("test3", "D:\test3.txt");
            testDict.Add(3, program3);

            testDict.RemoveProgramToStart(2);
            
            Assert.AreEqual(2, testDict.Count);
            Assert.AreEqual(program1, testDict[1]);
            Assert.AreEqual(program3, testDict[2]);
        }
        
    }
}
