using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MidiToArduino
{
    public class CppBuilder
    {
        private string name;
        private string folder;
        private string father;

        private StringBuilder CppFile = new StringBuilder();
        private StringBuilder HeaderFile = new StringBuilder();
        private StringBuilder PrivateDec = new StringBuilder();
        private StringBuilder PublicDec = new StringBuilder();

        public CppBuilder(string className, string destinationFolder, string father = "")
        {
            this.name = className;
            this.folder = destinationFolder;
            this.father = father;

            CppFile.AppendLine($"#include \"{name}.h\"\n");

            PrivateDec.AppendLine("\tprivate:");
            PrivateDec.AppendLine("\tpublic:");
        }

        public void AddConstructor(bool privateVis, string args, List<string> logicLines)
        {
            var dec = privateVis ? PrivateDec : PublicDec;
            var decStr = new StringBuilder();
            decStr.Append($"{name}");
            // Append arguments
            decStr.Append($"({args})");

            dec.Append("\t" + decStr);
            dec.AppendLine(";\n");

            CppFile.AppendLine($"{this.name}::{decStr} {{\n\t{string.Join("\n\t\t", logicLines)}\n}}\n");
        }

        public void AddField(string type, string name, bool privateVis, string init = "")
        {
            var dec = privateVis ? PrivateDec : PublicDec;
            dec.Append($"\t\t{type} {name}");
            if (init != "") dec.Append($" = {init}");
            dec.AppendLine(";");
        }

        public void AddMethod(bool privateVis, string type, string name, string args,
            List<string> logicLines, bool virtualFunc = false,
            bool overrideFunc = false, bool pureVirtual = false)
        {
            var dec = privateVis ? PrivateDec : PublicDec;
            var decStr = new StringBuilder();
            if (virtualFunc || pureVirtual) dec.Append("virtual ");
            dec.Append($"\t{type} ");
            decStr.Append($"{name}");
            // Append arguments
            decStr.Append($"({args})");

            dec.Append(decStr);
            if (overrideFunc) dec.Append(" override");
            if (pureVirtual) dec.Append(" = 0");
            dec.AppendLine(";\n");

            CppFile.AppendLine($"{type} {this.name}::{decStr} {{\n\t{string.Join("\n\t", logicLines)}\n}}\n");
           
        }

        public void Import(string include)
        {
            HeaderFile.Append("#include " + include);
        }


        public void Write()
        {
            var imports = "";
            
            var header = new StringBuilder(HeaderFile.ToString());
            header.Append($"\n\nclass {name}");
            if (father != "") header.Append($" : public {father}");
            header.AppendLine("{");
            header.Append(PrivateDec);
            header.Append(PublicDec);
            header.Append("\n};");
            
            File.WriteAllText($"{folder}\\{name}.h",header.ToString());
            File.WriteAllText($"{folder}\\{name}.cpp",CppFile.ToString());

        }
    }
}