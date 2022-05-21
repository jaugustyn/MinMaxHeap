using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerformanceTest
{
    [TestClass]
    public class PerfTest
    {
        [TestMethod]
        public void AddTest()
        {
            var heap = new MinMaxHeap<int>();
            var sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 10000000; i++) heap.Insert(i);
            sw.Stop();

            Console.WriteLine("Add elements took {0} ms.", sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void DeleteMinTest()
        {
            var heap = new MinMaxHeap<int>();
            var temp = 0;
            var sw = new Stopwatch();

            for (int i = 0; i < 1000000; i++) heap.Insert(i);

            sw.Start();
            for (int i = 0; i < 1000000; i++) temp += heap.DeleteMin();
            sw.Stop();

            Console.WriteLine("DeleteMin took {0} ms.", sw.ElapsedMilliseconds);
        }
        [TestMethod]
        public void DeleteMaxTest()
        {
            var heap = new MinMaxHeap<int>();
            var temp = 0;
            var sw = new Stopwatch();

            for (int i = 0; i < 1000000; i++) heap.Insert(i);

            sw.Start();
            for (int i = 0; i < 1000000; i++) temp += heap.DeleteMax();
            sw.Stop();

            Console.WriteLine("DeleteMax took {0} ms.", sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void GetMin()
        {
            var heap = new MinMaxHeap<int>();
            var temp = 0;
            var sw = new Stopwatch();

            for (int i = 0; i < 1000000; i++) heap.Insert(i);

            sw.Start();
            for (int i = 0; i < 1000000; i++) temp += heap.Min;
            sw.Stop();

            Console.WriteLine("Getting Min took {0} ms.", sw.ElapsedMilliseconds);
        }
        [TestMethod]
        public void GetMax()
        {
            var heap = new MinMaxHeap<int>();
            var temp = 0;
            var sw = new Stopwatch();

            for (int i = 0; i < 1000000; i++) heap.Insert(i);

            sw.Start();
            for (int i = 0; i < 1000000; i++) temp += heap.Max;
            sw.Stop();

            Console.WriteLine("Getting Max took {0} ms.", sw.ElapsedMilliseconds);
        }
    }
}
