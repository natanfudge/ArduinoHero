using Melanchall.DryWetMidi.MusicTheory;

namespace MidiToArduino.song
{
    public class UnproccessedNote
    {
        public NoteName note { get; set; }
        public int octave { get; set; }
        public long length { get; set; }
        public long time { get; set; }

        public UnproccessedNote(NoteName note, int octave, long length, long time)
        {
            this.note = note;
            this.octave = octave;
            this.length = length;
            this.time = time;
        }

        public UnproccessedNote()
        {
        }

        public override string ToString()
        {
            return $"ArdNote{{Note = {note}, Octave = {octave}, Length = {length}, Time = {time}}}";
        }

        /*How many half lines is this note above the other note*/
        public int Distance(UnproccessedNote other)
        {
            int thisTotal = octave * 12 + (int)note;
            int otherTotal = other.octave * 12 + (int)other.note;

            return thisTotal - otherTotal;
        }
    }
}