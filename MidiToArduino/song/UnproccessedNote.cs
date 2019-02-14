using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.MusicTheory;

namespace MidiToArduino.song
{
    public class UnproccessedNote
    {
        public UnproccessedNote(int number, long length, long time, SevenBitNumber strength)
        {
            this.number = number;
            this.length = length;
            this.time = time;
            this.strength = strength;

        }
        public int number { get; set; }
        public long length { get; set; }
        public long time { get; set; }

        public SevenBitNumber strength{get;set;}


        public UnproccessedNote()
        {
        }

        public override string ToString(){
            return $"UnproccessedNote{{number = {number}, length = {length}, time = {time}, strength = {strength}}}";
        }



        /*How many half lines is this note above the other note*/
        public int Distance(UnproccessedNote other)
        {
            return this.number - other.number;
        }

      
    }
}