using Ez.Collections;
using Ez.Tests.Helpers;
using System.Collections.Generic;
using Xunit;
using CollectionExtensions = Ez.Collections.CollectionExtensions;

namespace Ez.Tests.Collections
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void AsReadOnlyTest1()
        {
            var list = (ICollection<int>)new List<int>();
            
            for(var i = 0; i < 64; i++)
                list.Add(i);

            var roc = list.AsReadOnly();
            foreach(var element in list)
                Assert.Contains(element, roc);

            var value = 65;
            Assert.DoesNotContain(value, roc);
            list.Add(value);

            Assert.Contains(value, roc);
        }

        [Fact]
        public void ShallowCopyTest1()
        {
            var array = new object[32];
            for (var i = 0; i < array.Length; i++)
                array[i] = new object();

            var copy = array.ShallowCopy();

            foreach (var element in copy)
                Assert.Contains(element, array);
        }

        [Fact]
        public void ShallowCopyTest2()
        {
            var array = new object[32];
            for (var i = 0; i < array.Length; i++)
                array[i] = new Boxing();

            var copy = array.ShallowCopy<object, Boxing>();

            foreach (var element in array)
                Assert.Contains(element, copy);
        }

        [Fact]
        public void ShallowCopyTest3()
        {
            var array = new Boxing[32];
            for (var i = 0; i < array.Length; i++)
                array[i] = new Boxing();

            var copy = array.ShallowCopy<object, Boxing>();

            foreach (var element in array)
                Assert.Contains(element, copy);
        }

        [Fact]
        public void MinimumArrayTest1()
        {
            int[] array = null;
            CollectionExtensions.MinimumArray(ref array, 1);
            Assert.Single(array);
            array[0] = 1;
            
            CollectionExtensions.MinimumArray(ref array, 2);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array.Length);
        }
    }
}
