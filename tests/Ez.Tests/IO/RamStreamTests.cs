using Ez.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ez.Tests.IO
{
    public class RamStreamTests
    {

        [Fact]
        public static void RamStream_WriteToTests()
        {
            using (RamStream ms2 = new RamStream())
            {
                byte[] bytArrRet;
                byte[] bytArr = new byte[256];
                new Random(123).NextBytes(bytArr);

                ms2.Write(bytArr, 0, bytArr.Length);

                using (RamStream readonlyStream = new RamStream())
                {
                    ms2.Position = 0;
                    ms2.CopyTo(readonlyStream);

                    readonlyStream.Flush();
                    readonlyStream.Position = 0;
                    bytArrRet = new byte[(int)readonlyStream.Length];
                    readonlyStream.Read(bytArrRet, 0, (int)readonlyStream.Length);
                    for (int i = 0; i < bytArr.Length; i++)
                    {
                        Assert.Equal(bytArr[i], bytArrRet[i]);
                    }
                }
            }
        }
    }
}
