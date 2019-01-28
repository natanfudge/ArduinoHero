﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Note = Melanchall.DryWetMidi.Smf.Interaction.Note;

namespace MidiToArduino
{
    internal class Program
    {
        public static void Main(string[] args)
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
                return;
            }

            {
                var extension = Path.GetExtension(source);
                if (extension == null || !extension.Equals(".mid"))
                {
                    Console.WriteLine($"The target file must be a .mid file (not a {extension} file).");
                    Console.ReadKey();
                    return;
                }
            }


            var midiFile = MidiFile.Read(source);
            var notes = midiFile.GetNotes();


            var song = new Song(notes
                .Select(note => new JsonNote(note.NoteName, note.Octave, note.Length, note.Time)).ToList());


            var ahNotes = new List<AHNote>();

            ahNotes.Add(new AHNote(song[0].time, 3));

            for (var i = 1; i < song.notes.Count; i++)
            {
                var note = song[i];
                var prevNote = song[i - 1];
                int prevNoteCol = ahNotes[i - 1].column;

                int distance = note.Distance(prevNote);
                int column = DetermineColumn(note, prevNote, distance, prevNoteCol, ahNotes);
                ahNotes.Add(new AHNote(note.time, column));
            }

            //    var dest = Path.ChangeExtension(source, ".ino");

            var dest = source.Replace(" ", "_");

            var ahSong = new AHSong(ahNotes, Path.GetFileNameWithoutExtension(dest));
            ahSong.FixEdging();
            ahSong.FixEdging();


            ahSong.WriteTo(Path.GetDirectoryName(dest));
            ahSong.MakeEasy();
            ahSong.WriteTo(Path.GetDirectoryName(dest));
            Console.WriteLine($"C++ classes added at \"{Path.GetDirectoryName(dest)}\". \nPress any key to close.");
            Console.ReadKey();
        }


        private static int DetermineColumn(JsonNote note, JsonNote prevNote, int distance, int prevNoteCol,
            List<AHNote> ahNotes)
        {
            int column;

            // If both notes are at the same time it's a cord which should be more spread out
            var sameTime = note.time == prevNote.time;


            // Prev column is about the same
            if ((!sameTime && distance <= 2 && distance >= -2) || (sameTime && distance == 0))
            {
                column = prevNoteCol;
            }
            // Prev column is lower
            else if ((distance > 2 || (sameTime && distance > 0)) && distance < 6)
            {
                column = Math.Min(4, prevNoteCol + 1);
            }
            else if (distance >= 6 && distance < 13)
            {
                column = Math.Min(4, prevNoteCol + 2);
            }
            else if (distance >= 13)
            {
                column = Math.Min(4, prevNoteCol + 3);
            }
            // Prev column is higher
            else if ((distance < -2 || (sameTime && distance < 0)) && distance > -6)
            {
                column = Math.Max(1, prevNoteCol - 1);
            }
            else if (distance >= -6 && distance > -13)
            {
                column = Math.Max(1, prevNoteCol - 2);
            }
            else
            {
                column = Math.Max(1, prevNoteCol - 3);
            }

            return column;
        }
    }

    public static class Extension
    {
        public delegate bool BiPredicate<in T1, in T2>(T1 obj1, T2 obj2);

        public static bool AnyTwo<T>(this IEnumerable<T> list, BiPredicate<T, T> condition)
        {
            return list.Any(e1 => list.Any(e2 => !e1.Equals(e2) && condition(e1, e2)));
        }
    }
}