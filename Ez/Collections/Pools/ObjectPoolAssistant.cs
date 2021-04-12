using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections.Pools
{
    public sealed class ObjectPoolAssistant<T, TSpecs> : IObjectPoolAssistant<T, TSpecs>
    {
        public delegate T CreateFunction(object specs);
        public delegate bool MeetsExpectationFunction(in T item, object specs, int currentTolerance);
        public delegate void ResetFunction(ref T item);

        private readonly CreateFunction _create;
        private readonly MeetsExpectationFunction _meetsExpectation;
        private readonly uint _clearCount;
        private uint _count;

        public ObjectPoolAssistant(CreateFunction create, MeetsExpectationFunction meetsExpectation, uint clearCount = 32)
        {
            _create = create ?? throw new ArgumentNullException(nameof(create));
            _meetsExpectation = meetsExpectation;
            _clearCount = clearCount;
            _count = _clearCount;
        }

        public T Create(in TSpecs specs) => _create(specs);

        public bool MeetsExpectation(in T item, in TSpecs specs, int currentTolerance) 
        {
            bool? b = _meetsExpectation?.Invoke(item, specs, currentTolerance);
            if (b.HasValue)
                return b.Value;
            return true;
        }

        public bool IsClear()
        {
            return _count >= _clearCount;
        }

        public void RegisterReturn(in T item)
        {
            _count--;
        }

        public void RegisterGet(in T item)
        {
            _count++;
        }
    }
}
