using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_practice___Loop_Performance
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private readonly List<string> _listToLoopThrough = [];
        private readonly int _size = 1_000_000;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(420);

            for (int i = 0; i < _size; i++)
            {
                _listToLoopThrough.Add(random.Next().ToString());
            }
        }

        [Benchmark]
        public string For() 
        {
            var result = string.Empty;

            //list size calculated outside of the loop for a TINY performance boost
            var size = _listToLoopThrough.Count;
            for (int i = 0; i < size; i++)
            {
                result = _listToLoopThrough[i];
            }

            return result;
        }

        [Benchmark]
        public string Foreach()
        {
            var result = string.Empty;
            foreach (var item in _listToLoopThrough) 
            { 
                result = item; 
            }

            return result;
        }

        //Do not use this foreach method as its dog slow!
        [Benchmark]
        public string ForEach() 
        {
            var result = string.Empty;
            _listToLoopThrough.ForEach(item => result = item);
            return result;
        }

        [Benchmark]
        public string While() 
        {
            var i = 0;
            var size = _listToLoopThrough.Count;
            var result = string.Empty;

            while (i < size)
            {
                result = _listToLoopThrough[i];
                i++;
            }   

            return result;
        }

        [Benchmark]
        public string DoWhile() 
        {
            var i = 0;
            var size = _listToLoopThrough.Count;
            var result = string.Empty;

            do
            {
                result = _listToLoopThrough[i];
                i++;
            } while (i < size);

            return result;
        }

        [Benchmark]
        public string Goto()
        {
            var i = 0;
            var size = _listToLoopThrough.Count;
            var result = string.Empty;

            Start:
                if (i < size) 
                {
                    result = _listToLoopThrough[i];
                    i++;
                    goto Start;
                }

            return result;
        }
    }
}
