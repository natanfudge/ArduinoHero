using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MidiToArduino.song
{
    public class AHSong
    {
        private const int SEGMENT_LENGTH = 50;

        public List<AHNote> notes;
        public string name;
        

        public AHSong(List<AHNote> notes, string name)
        {
            this.notes = notes;
            this.name = name;
        }


        const string EXTENSION = ".ino";


        public void WriteTo(string folder)
        {
            var notesArr = "\t\t\t" + string.Join(",\n\t\t\t",
                this.notes.Select(note => $"{{{note.time}, {note.column - 1}}}"));


            var fileStr = $"const PROGMEM Note {this.name}[] = {{\n{notesArr}\n}};";

            fileStr += $"\n\nNote* get_{this.name}(){{return {this.name};}}";

            var targetLocation = $"{folder}/{this.name}{EXTENSION}";

            File.WriteAllText(targetLocation, fileStr);
            Console.WriteLine($"Wrote '{targetLocation}'.");

        }

       




        // Sometimes a note is set at column =1, and there is also another note at the same time which is supposed to be lower,
        // but because 1 is at the edge the other note will also be at column = 1.
        // This function will remove one of the notes to resolve this issue.
        /// NOTE: The method used is REMOVING one of the notes.
        /// Another approach that would be more adequate but more difficult for players would be BALANCING the columns without removing.
        public void FixEdging()
        {
            var sameTime = notes.GroupBy(note => note.time);
            foreach (var group in sameTime)
            {
                int offendingIndex = 0;
                if (group.AnyTwo((note1, note2) =>
                {
                    if (note1.column == note2.column)
                    {
                        offendingIndex = notes.IndexOf(note1);
                        return true;
                    }

                    return false;
                }))
                {
                    notes.RemoveAt(offendingIndex);
                }
            }
        }

        // Makes it only output one note at a time
        public void MakeEasy()
        {
            // name += "_easy";
            // var sameTime = notes.GroupBy(note => note.time);
            // foreach (var group in sameTime)
            // {
            //     // All notes other than the first one
            //     var otherNotes = group.OrderByDescending(element => element.column).ToList()
            //         .GetRange(1, group.Count() - 1);
            //     foreach (var note in otherNotes)
            //     {
            //         notes.RemoveAll(element => element.Equals(note));
            //     }
            // }

            name += "_easy";
            this.notes = this.notes.Where(note => note.time != 0).ToList();
        }



    }
}