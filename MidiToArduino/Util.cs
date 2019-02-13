using System.Collections.Generic;
using System.Linq;

namespace MidiToArduino
{
    public static class Util
    {
        // ORIG NORMAL: 25KB
        // ORIG EASY : 16KB
        private static readonly Dictionary<string, string> Abbreviations = new Dictionary<string, string>
        {
            {"allocatePart", "AP"},
            {"segmentIndex","SI"},
            {"currentSegmentSize","CSS"},
            {"currentSegment","C"},
            {"seg","s"},
            {"Note","N"},
            {"time","t"},
            {"col","c"},
            {"notes","n"}
//            {}
            
        };

        public const bool ABBREVIATE = false;

        public static string Abbrev(this string str)
        {
            return ABBREVIATE ? Abbreviations[str] : str;
        }


        public delegate bool BiPredicate<in T1, in T2>(T1 obj1, T2 obj2);

        public static bool AnyTwo<T>(this IEnumerable<T> list, BiPredicate<T, T> condition)
        {
            return list.Any(e1 => list.Any(e2 => !e1.Equals(e2) && condition(e1, e2)));
        }

    }
}