using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib
{
    public class Calculation
    {
        static double[] arrProduct = { 1.1f, 2.5f, 8.43f };
        static double[] arrMaterial = { 1.003f, 1.0012f };
        public int GetQuantityForProduct(int ProductType,int MaterialType,int Count,float Width,float Length)
        {
            if ((ProductType > 3 || ProductType <= 0 || (MaterialType > 2 || ProductType <= 0)))
                return -1;
            if (Count <= 0 || Width <= 0 || Length <= 0)
                return -1;
            return Convert.ToInt32(
                Math.Ceiling(arrProduct[ProductType - 1] * arrMaterial[MaterialType - 1] * Count * Width * Length));
        }
    }
}
