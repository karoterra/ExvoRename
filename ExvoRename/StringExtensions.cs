using System;
using System.Collections.Generic;
using System.Text;

namespace ExvoRename
{
    static class StringExtensions
    {
        public static readonly Dictionary<string, string> InvalidFileNameChars = new Dictionary<string, string>()
        {
            {"\\", "￥"},
            {"/", "／"},
            {":", "："},
            {"*", "＊"},
            {"?", "？"},
            {"\"", "”"},
            {"<", "＜"},
            {">", "＞"},
            {"|", "｜"},
        };

        public static readonly Dictionary<string, string> HanZenNum = new Dictionary<string, string>()
        {
            {"0", "０"},
            {"1", "１"},
            {"2", "２"},
            {"3", "３"},
            {"4", "４"},
            {"5", "５"},
            {"6", "６"},
            {"7", "７"},
            {"8", "８"},
            {"9", "９"},
        };

        public static string ConvertInvalidFileNameChars(this string s)
        {
            foreach (var item in InvalidFileNameChars)
            {
                s = s.Replace(item.Key, item.Value);
            }
            return s;
        }

        public static string ConvertNumHanToZen(this string s)
        {
            foreach (var item in HanZenNum)
            {
                s = s.Replace(item.Key, item.Value);
            }
            return s;
        }
    }
}
