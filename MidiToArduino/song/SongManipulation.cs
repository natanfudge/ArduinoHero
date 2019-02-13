using System;
using System.Collections.Generic;
using System.IO;

namespace MidiToArduino.song
{
    public class SongManipulation
    {
        public static AHSong MakeNormalArduinoSong(UnproccesedSong song, string midiFile)
        {
            var ahNotes = new List<AHNote>();

            ahNotes.Add(new AHNote(song[0].time, 3));

            // Loop through the unproccessed song and replace each proccessed note with an AHNote
            for (var i = 1; i < song.notes.Count; i++)
            {
                var note = song[i];
                var prevNote = song[i - 1];
                int prevNoteCol = ahNotes[i - 1].column;

                int column = DetermineColumn(note, prevNote, note.Distance(prevNote), prevNoteCol, ahNotes);
                ahNotes.Add(new AHNote(note.time - prevNote.time, column));
            }


            var dest = midiFile.Replace(" ", "_");

            var ahSong = new AHSong(ahNotes, Path.GetFileNameWithoutExtension(dest));
            ahSong.FixEdging();
            ahSong.FixEdging();

            return ahSong;
        }

        public static AHSong MakeEasyArduinoSong(UnproccesedSong song, string midiFile){
            var ahSong = MakeNormalArduinoSong(song,midiFile);
            ahSong.MakeEasy();
            return ahSong;
        }


        private static int DetermineColumn(UnproccessedNote note, UnproccessedNote prevNote, int distance, int prevNoteCol,
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
    
}