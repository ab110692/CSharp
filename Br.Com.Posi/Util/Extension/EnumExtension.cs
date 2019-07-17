using System;
using System.Collections.Generic;
using System.Linq;

namespace Br.Com.Posi.Util.Extension
{
    public static class EnumExtension
    {
        public static List<T> GetAll<T>(this T e) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("[EnumExtension].[GetAll]: Tipo generico não é um Enum");
            }

            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
