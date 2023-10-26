using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

internal static class Extensions
{
    public static Rectangle GetRectangle(int top, int left, int bottom, int right)
    {
        return new Rectangle(left, top, right - left, bottom - top);
    }

    public static bool IsInteger(this string input)
    {
        int num;
        return int.TryParse(input, out num);
    }

    public static bool IsListEmpty(this IEnumerable list)
    {
        return list == null || !list.GetEnumerator().MoveNext();
    }

    public static bool IsNumeric(this string input)
    {
        double num;
        return double.TryParse(input, out num);
    }

    public static string Left(this string input, int length)
    {
        if (input.Length <= length)
        {
            return input;
        }
        return input.Substring(0, length);
    }

    public static bool Like(this string input, string pattern)
    {
        string text = pattern.Replace("\\", "\\\\").Replace("[", "\\[").Replace("]", "\\]").Replace("(", "\\(").Replace(")", "\\)").Replace("{", "\\{").Replace("}", "\\}").Replace(".", "\\.").Replace("^", "\\^").Replace("$", "\\$").Replace("|", "\\|").Replace("+", "\\+");
        text = text.Replace("*", ".*").Replace("?", ".{1}");
        return Regex.IsMatch(input, text, RegexOptions.IgnoreCase);
    }

    public static uint SwitchColorFormatBGRRGB(uint lColor)
    {
        uint num = (lColor & 255u) ^ lColor / 65536u;
        return lColor ^ num * 65537u;
    }

    private static void UnitTest()
    {
        "234".IsNumeric();
        "234,3".IsNumeric();
        "33".IsInteger();
        "file1.doc".Like("file*.doc");
        "file123424.doc".Like("file*.doc");
        "1file1.doc".Like("?file?.doc");
    }
}
