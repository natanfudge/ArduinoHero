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


        string ALLOCATE_PART = "allocatePart".Abbrev();
        string SEGMENT_INDEX = "segmentIndex".Abbrev();
        string CURRENT_SEGMENT_SIZE = "currentSegmentSize".Abbrev();
        string CURRENT_SEGMENT = "currentSegment".Abbrev();
        private string SEGMENT = "seg".Abbrev();
        private string NOTE = "Note".Abbrev();
        private string TIME = "time".Abbrev();
        private string COLUMN = "col".Abbrev();
        private string NOTES = "notes".Abbrev();

        const string EXTENSION = ".ino";


        public void WriteTo(string folder)
        {
            var notesArr = "\t\t\t" + string.Join(",\n\t\t\t",
                this.notes.Select(note => $"{{{note.time}, {note.column - 1}}}"));


            var fileStr = $"const PROGMEM Note {this.name}[] = {{\n{notesArr}\n}};";

            fileStr += $"\n\nNote* get_{this.name}(){{return {this.name};}}";

            File.WriteAllText($"{folder}/{this.name}{EXTENSION}", fileStr);

            // var builder = new ArduinoBuilder(name, folder, "Song");

            // var headerName = Util.ABBREVIATE ? "\"SmallSong.h\"" : "\"Song.h\"";
            // builder.Import(headerName);

            // var notesPassed = 0;
            // var parts = notes.Count / SEGMENT_LENGTH;


            // builder.AddConstructor(false, "", new List<string> {$"{ALLOCATE_PART}1();"});

            // var firstsegLen = Math.Min(SEGMENT_LENGTH, notes.Count);
            // builder.AddMethod(true, "void", $"{ALLOCATE_PART}1", "", AllocateLogic(firstsegLen, ref notesPassed));

            // // Add all allocate methods
            // for (int i = 1; i <= parts; i++)
            // {
            //     var segmentLength = Math.Min(SEGMENT_LENGTH, notes.Count - notesPassed);
            //     builder.AddMethod(true, "void", $"{ALLOCATE_PART}{i + 1}", "",
            //         AllocateLogic(segmentLength, ref notesPassed));
            // }

            // builder.AddMethod(false, "bool", "hasNext", "",
            //     new List<string> {$"return {SEGMENT_INDEX} < {parts} || noteIndex < {CURRENT_SEGMENT}.length - 1;"},
            //     overrideFunc: true);

            // builder.AddMethod(false, "void", "advanceNote", "", AdvanceNoteLogic(parts), overrideFunc: true);

            // builder.Write();
        }

        private List<string> AdvanceNoteLogic(int parts)
        {
            var AdvanceNoteLogic = new List<string>
            {
                $"if(noteIndex < {CURRENT_SEGMENT}.length - 1){{",
                "\tnoteIndex++;",
                "}else{",
                "\tnoteIndex = 0;",
                $"\t++{SEGMENT_INDEX};"
            };

            for (int i = 1; i <= parts; i++)
            {
                AdvanceNoteLogic.Add($"\tif({SEGMENT_INDEX} == {i}) {ALLOCATE_PART}{i + 1}();");
            }

            AdvanceNoteLogic.Add("}");

            return AdvanceNoteLogic;
        }


        private List<string> AllocateLogic(int segmentLength, ref int notesPassed)
        {

            var notesArr = "\t\t\t" + string.Join(",\n\t\t\t",
                this.notes.Skip(notesPassed).Take(segmentLength).Select(note => $"{{{note.time}, {note.column - 1}}}"));

            notesPassed = notesPassed + segmentLength;

            var segment = $"{CURRENT_SEGMENT} = {{\n\t\t{{\n{notesArr}\n\t\t\t}},\n\t\t\t{segmentLength}\n\t\t}};";


            var firstAllocateLogic = new List<string>
            {
                segment
            };

            return firstAllocateLogic;
        }

        private void InsertNotes(ref int notesPassed, int segmentLength, List<string> firstAllocateLogic,
            string segName)
        {
            for (int i = 0; i < segmentLength; i++)
            {
                var note = notes[notesPassed];
                firstAllocateLogic.Add(
                    $"{NOTE} n{i}; n{i}.{TIME} = {note.time}; n{i}.{COLUMN} = {note.column - 1}; {segName}.{NOTES}[{i}] = n{i};");
                notesPassed++;
            }
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