using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Melanchall.DryWetMidi.MusicTheory;
using MidiToArduino.song;

namespace MidiToArduino
{
    public class UnproccesedSong
    {
        public List<UnproccessedNote> notes { get; set; }

        public UnproccesedSong(List<UnproccessedNote> notes)
        {
            this.notes = notes;
        }

        public UnproccesedSong()
        {
        }

        public UnproccessedNote this[int key]
        {
            get { return notes[key]; }
            set { notes[0] = value; }
        }
    }


    


   


}