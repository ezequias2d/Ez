using Ez.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ez.Tests.IO
{
    public class PathHelperTests
    {
        private string[] GetPaths() => new string[] { "home", "user", "folder", "file.txt" };
        
        [Fact]
        public void SeparatePathTest1()
        {
            var paths = GetPaths();
            var fullPath = Path.Combine(paths);
            var separatePath = PathHelper.SeparatePath(fullPath);

            Assert.Equal(paths.Length, separatePath.Length);
            for (var i = 0; i < paths.Length; i++)
                Assert.Equal(paths[i], separatePath[i]);
        }

        [Fact]
        public void GetFolderNameTest1()
        {
            var paths = GetPaths();
            var fullPath = Path.Combine(paths);

            var folderName = PathHelper.GetFolderName(fullPath);
            Assert.Equal(paths[paths.Length - 2], folderName);
        }

    }
}
