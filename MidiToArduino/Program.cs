using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using MidiToArduino.song;
using Note = Melanchall.DryWetMidi.Smf.Interaction.Note;


namespace MidiToArduino
{
    internal class Program
    {




        public static void Main(string[] args)
        {

            var midiFile = GetInput(args);
            if (midiFile == null) return;

            var song = ParseMidi(midiFile);


            var normalSong = SongManipulation.MakeNormalArduinoSong(song,midiFile);
            var easySong = SongManipulation.MakeEasyArduinoSong(song,midiFile);

            var destination = midiFile.Replace(" ", "_");
            normalSong.WriteTo(Path.GetDirectoryName(destination));
            easySong.WriteTo(Path.GetDirectoryName(destination));

            Console.WriteLine("Press any key to close.");

            // Console.WriteLine($"C++ classes added at \"{Path.GetDirectoryName(dest)}\". \nPress any key to close.");
            Console.ReadKey();
        }

        private static string GetInput(string[] args)
        {
            string source;
            bool easy = false;
            if (args.Length >= 1)
            {
                source = args[0];
            }
            else
            {
                // Fix hebrew input
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;

                Console.WriteLine("Please enter file to convert.");
                source = Console.ReadLine();
            }


            if (!File.Exists(source))
            {
                Console.WriteLine($"The file {source} doesn't exist!'");
                Console.ReadKey();
                return null;
            }

            {
                var extension = Path.GetExtension(source);
                if (extension == null || !extension.Equals(".mid"))
                {
                    Console.WriteLine($"The target file must be a .mid file (not a {extension} file).");
                    Console.ReadKey();
                    return null;
                }
            }

            return source;
        }

        public static UnproccesedSong ParseMidi(string path)
        {
            var midiFile = MidiFile.Read(path);
            var notes = midiFile.GetNotes();

            return new UnproccesedSong(notes
                 .Select(note => new UnproccessedNote(note.NoteName, note.Octave, note.Length, note.Time)).ToList());
        }





    }
}