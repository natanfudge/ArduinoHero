namespace MidiToArduino.song
{
    public class AHNote
    {
        public long time { get; set; }
        public int column { get; set; }

        public AHNote(long time, int column)
        {
            this.time = time;
            this.column = column;
        }
    }
}