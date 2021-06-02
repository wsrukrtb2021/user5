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
        public int GetQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            //елсли тип продукта больше 3 или тип продукта меньше или равно 0 или тип материала больше 2 или тип продукта меньше или равно 0 иначе возвращаем -1
            if ((productType > 3 || productType <= 0) || (materialType > 2 || productType <= 0))
                return -1;
            //если кол-во меньше или равно 0 или ширина меньше или равно 0 или высота меньше или равно 0
            if (count <= 0 || width <= 0 || length <= 0)
                return -1;
            //перемножаем все
            return Convert.ToInt32(
               Math.Ceiling(arrProduct[productType - 1] * arrMaterial[materialType - 1] * count * width * length));
        }
    }
}