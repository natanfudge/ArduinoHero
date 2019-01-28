using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Melanchall.DryWetMidi.MusicTheory;


namespace MidiToArduino
{
    public class Song
    {
        public List<JsonNote> notes { get; set; }

        public Song(List<JsonNote> notes)
        {
            this.notes = notes;
        }

        public Song()
        {
        }

        public JsonNote this[int key]
        {
            get { return notes[key]; }
            set { notes[0] = value; }
        }
    }


    public class JsonNote
    {
        public NoteName note { get; set; }
        public int octave { get; set; }
        public long length { get; set; }
        public long time { get; set; }

        public JsonNote(NoteName note, int octave, long length, long time)
        {
            this.note = note;
            this.octave = octave;
            this.length = length;
            this.time = time;
        }

        public JsonNote()
        {
        }

        public override string ToString()
        {
            return $"ArdNote{{Note = {note}, Octave = {octave}, Length = {length}, Time = {time}}}";
        }

        /*How many half lines is this note above the other note*/
        public int Distance(JsonNote other)
        {
            int thisTotal = octave * 12 + (int) note;
            int otherTotal = other.octave * 12 + (int) other.note;

            return thisTotal - otherTotal;
        }
    }


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


        public void WriteTo(string folder)
        {
            var builder = new ArduinoBuilder(name, folder, "Song");

            var headerName = Util.ABBREVIATE ? "\"SmallSong.h\"" : "\"Song.h\"";
            builder.Import(headerName);

            var notesPassed = 0;
            var parts = notes.Count / SEGMENT_LENGTH;


            builder.AddConstructor(false, "", new List<string> {$"{ALLOCATE_PART}1();"});

            var firstsegLen = Math.Min(SEGMENT_LENGTH, notes.Count);
            builder.AddMethod(true, "void", $"{ALLOCATE_PART}1", "", AllocateLogic(firstsegLen, ref notesPassed));

            // Add all allocate methods
            for (int i = 1; i <= parts; i++)
            {
                var segmentLength = Math.Min(SEGMENT_LENGTH, notes.Count - notesPassed);
                builder.AddMethod(true, "void", $"{ALLOCATE_PART}{i + 1}", "",
                    AllocateLogic(segmentLength, ref notesPassed));
            }

            builder.AddMethod(false, "bool", "hasNext", "",
                new List<string> {$"return {SEGMENT_INDEX} < {parts} || noteIndex < {CURRENT_SEGMENT}.length - 1;"},
                overrideFunc: true);

            builder.AddMethod(false, "void", "advanceNote", "", AdvanceNoteLogic(parts), overrideFunc: true);

            builder.Write();
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
            name += "_easy";
            var sameTime = notes.GroupBy(note => note.time);
            foreach (var group in sameTime)
            {
                // All notes other than the first one
                var otherNotes = group.OrderByDescending(element => element.column).ToList()
                    .GetRange(1, group.Count() - 1);
                foreach (var note in otherNotes)
                {
                    notes.RemoveAll(element => element.Equals(note));
                }
            }
        }



    }

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