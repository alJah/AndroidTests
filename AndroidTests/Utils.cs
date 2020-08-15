using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTests
{
    public static class Utils
    {
        /// <summary>
        /// Вернет случайное число от мин до мах
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] random = new byte[1];

            //Заполнить массив случайными байтами
            provider.GetBytes(random);

            //случайное значение от 0 до 1
            double multypleer = (random[0] / 255d);
            double diff = max - min;
            int result = (int)(min + Math.Floor(multypleer * diff));
            provider.Dispose();
            return result;
        }
    }
}
