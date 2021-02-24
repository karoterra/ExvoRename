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

        public static readonly Dictionary<string, string> ZenHanNum = new Dictionary<string, string>()
        {
            {"０", "0"},
            {"１", "1"},
            {"２", "2"},
            {"３", "3"},
            {"４", "4"},
            {"５", "5"},
            {"６", "6"},
            {"７", "7"},
            {"８", "8"},
            {"９", "9"},
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

        public static string ConvertNumZenToHan(this string s)
        {
            foreach (var item in ZenHanNum)
            {
                s = s.Replace(item.Key, item.Value);
            }
            return s;
        }
    }
}
