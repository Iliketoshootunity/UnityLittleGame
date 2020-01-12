using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class StringUtil
{
    /// <summary>
    /// 转换成int
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToInt(this string value)
    {
        int temp = 0;
        int.TryParse(value, out temp);
        return temp;
    }
    /// <summary>
    /// 转换成ilong
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static long ToLong(this string value)
    {
        long temp = 0;
        long.TryParse(value, out temp);
        return temp;
    }

    /// <summary>
    /// 转换成float
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float ToFloat(this string value)
    {
        float temp = 0;
        float.TryParse(value, out temp);
        return temp;
    }
}

