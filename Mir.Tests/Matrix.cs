using Mir;

using System;
using NUnit.Framework;

namespace Mir.Test
{
    [TestFixture]
    public class MatrixExample
    {
        [Test]
        public void MatrixTest()
        {
            var m = new Matrix<double>(3, 4);
            int rowi = 0;
            foreach (var row in m) // as collection of rows
            {
                for(int coli = 0; coli < row.Count; coli++)
                {
                    row[coli] = rowi * coli;
                    m.At(rowi, coli) += 2; // At returns by reference if T is unmanaged
                }
                rowi++;
            }

            for(int i = 0; i < m.Count0; i++)
                for(int j = 0; j < m.Count1; j++)
                    // Assert.AreEqual(i * j + 2, m[i, j]); // .Net Standard 2.1
                    Assert.AreEqual(i * j + 2, m.At(i, j));
        }
    }
}
