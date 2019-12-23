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

        [TestCase]
        public void ChangeProgramToStartIndexTest1()
        {
            Dictionary<int, ProgramToStart> testDict = new Dictionary<int, ProgramToStart>();
            ProgramToStart program1 = new ProgramToStart("test1", "D:\test1.txt");
            testDict.Add(1, program1);
            ProgramToStart program2 = new ProgramToStart("test2", "D:\test2.txt");
            testDict.Add(2, program2);
            ProgramToStart program3 = new ProgramToStart("test3", "D:\test3.txt");
            testDict.Add(3, program3);

            testDict.ChangeProgramToStartIndex(2,3);

            Assert.AreEqual(3, testDict.Count);
            Assert.AreEqual(program1, testDict[1]);
            Assert.AreEqual(program3, testDict[2]);
            Assert.AreEqual(program2, testDict[3]);
        }

        [TestCase]
        public void ChangeProgramToStartIndexTest2()
        {
            Dictionary<int, ProgramToStart> testDict = new Dictionary<int, ProgramToStart>();
            ProgramToStart program1 = new ProgramToStart("test1", "D:\test1.txt");
            testDict.Add(1, program1);
            ProgramToStart program2 = new ProgramToStart("test2", "D:\test2.txt");
            testDict.Add(2, program2);
            ProgramToStart program3 = new ProgramToStart("test3", "D:\test3.txt");
            testDict.Add(3, program3);

            testDict.ChangeProgramToStartIndex(3, 1);

            Assert.AreEqual(3, testDict.Count);
            Assert.AreEqual(program1, testDict[2]);
            Assert.AreEqual(program3, testDict[1]);
            Assert.AreEqual(program2, testDict[3]);
        }

        [TestCase]
        public void ChangeProgramToStartIndexTest3()
        {
            Dictionary<int, ProgramToStart> testDict = new Dictionary<int, ProgramToStart>();
            ProgramToStart program1 = new ProgramToStart("test1", "D:\test1.txt");
            testDict.Add(1, program1);
            ProgramToStart program2 = new ProgramToStart("test2", "D:\test2.txt");
            testDict.Add(2, program2);
            ProgramToStart program3 = new ProgramToStart("test3", "D:\test3.txt");
            testDict.Add(3, program3);

            testDict.ChangeProgramToStartIndex(1, 3);

            Assert.AreEqual(3, testDict.Count);
            Assert.AreEqual(program1, testDict[3]);
            Assert.AreEqual(program3, testDict[2]);
            Assert.AreEqual(program2, testDict[1]);
        }
    }
}
