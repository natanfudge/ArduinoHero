using System.Collections.Generic;

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
    }
}