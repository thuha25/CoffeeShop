using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VietnamNumber
{
    public static class Number
    {
        
        private static readonly string[] ZeroLeftPadding = { "", "00", "0" };

        private static readonly string[] Digits =
        {
            "không",
            "một",
            "hai",
            "ba",
            "bốn",
            "năm",
            "sáu",
            "bảy",
            "tám",
            "chín"
        };

        private static readonly string[] MultipleThousand =
        {
            "",
            "nghìn",
            "triệu",
            "tỷ",
            "nghìn tỷ",
            "triệu tỷ",
            "tỷ tỷ"
        };

        private static IEnumerable<string> Chunked(this string str, int chunkSize) => Enumerable
            .Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));

        private static bool ShouldShowZeroHundred(this string[] groups) =>
            groups.Reverse().TakeWhile(it => it == "000").Count() < groups.Count() - 1;

        private static void Deconstruct<T>(this IReadOnlyList<T> items, out T t0, out T t1, out T t2)
        {
            t0 = items.Count > 0 ? items[0] : default;
            t1 = items.Count > 1 ? items[1] : default;
            t2 = items.Count > 2 ? items[2] : default;
        }

        private static string ReadTriple(string triple, bool showZeroHundred)
        {
            var (a, b, c) = triple.Select(ch => int.Parse(ch.ToString())).ToArray();
            string res = "";
            switch(a)
            {
                case 0:
                    if (b == 0 && c == 0) res += "";
                    else if (showZeroHundred) res += "không trăm " + ReadPair(b, c);
                    else if (b == 0) res += Digits[c];
                    else ReadPair(b, c);
                    break;
                default: res += Digits[a] + " trăm " + ReadPair(b, c); break;
            }
            return res;
        }

        private static string ReadPair(int b, int c)
        {
            string res = "";
            switch(b)
            {
                case 0:  res += c == 0 ? "" : " lẻ " + Digits[c]; break;
                case 1:  res += "mười "; 
                   switch(c)
                   {
                        case 0: res += ""; break;
                        case 5: res += "lăm"; break;
                        default: res += Digits[c]; break;
                   } break;
                default: res += Digits[b] + " mươi ";
                    switch (c)
                    {
                        case 0: res += ""; break;
                        case 1: res += "mốt"; break;
                        case 4: res += "tư"; break;
                        default: res += Digits[c]; break;
                    } break;
            };
            return res;
        }

        // "10" -> "mười"
        public static string ToVietnameseWords(int n)
        {
            if (n == 0) return "không";
            //if (n < 0) return "âm " + (-n).ToVietnameseWords().ToLower();

            var s = n.ToString();
            var groups = (ZeroLeftPadding[s.Length % 3] + s).Chunked(3).ToArray();
            var showZeroHundred = groups.ShouldShowZeroHundred();

            var index = -1;
            var rawResult = groups.Aggregate("", (acc, e) =>
            {
                checked
                {
                    index++;
                }

                var readTriple = ReadTriple(e, showZeroHundred && index > 0);
                var multipleThousand = (string.IsNullOrWhiteSpace(readTriple)
                    ? ""
                    : (MultipleThousand.ElementAtOrDefault(groups.Length - 1 - index) ?? ""));
                return $"{acc} {readTriple} {multipleThousand} ";
            });

            return Regex
                .Replace(rawResult, "\\s+", " ")
                .Trim();
        }

        // "09" -> "không chín"
        public static string ToVietnameseSingleWords(this string n)
        {
            var raw = "";
            foreach (var s in n)
            {
                if (int.TryParse(s.ToString(), out int num))
                    raw += Digits[num] + " ";
            }
            return Regex
                .Replace(raw, "\\s+", " ")
                .Trim();
        }
    }
    public static class Time
    {
        public static string TimeAgo(this DateTime dateTime, DateTime? from = null)
        {
            string result = string.Empty;
            var now = from ?? DateTime.Now;
            var timeSpan = now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(3))
            {
                result = string.Format("bây giờ", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} giây trước", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = string.Format("{0} phút trước", timeSpan.Minutes);
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = string.Format("{0} giờ trước", timeSpan.Hours);
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = string.Format("{0} ngày trước", timeSpan.Days);
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = string.Format("{0} tháng trước", timeSpan.Days / 30);
            }
            else
            {
                result = string.Format("{0} năm trước", timeSpan.Days / 365);
            }

            return result;
        }
    }
}
