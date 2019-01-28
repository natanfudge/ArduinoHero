using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MidiToArduino
{
    public class ArduinoBuilder
    {
        private string name;
        private string folder;
        private string father;

        private StringBuilder CppFile = new StringBuilder();

        private StringBuilder Imports = new StringBuilder();

//        private StringBuilder HeaderFile = new StringBuilder();
        private StringBuilder PrivateFuncs = new StringBuilder();
        private StringBuilder PublicFuncs = new StringBuilder();

        public ArduinoBuilder(string className, string destinationFolder, string father = "")
        {
            this.name = className;
            this.folder = destinationFolder;
            this.father = father;

//            CppFile.AppendLine($"#include \"{name}.h\"\n");

            PrivateFuncs.AppendLine("\tprivate:");
            PrivateFuncs.AppendLine("\tpublic:");
        }

        public void AddConstructor(bool privateVis, string args, List<string> logicLines)
        {
            AddMethod(privateVis, "", this.name, args, logicLines);
//            var funcs = privateVis ? PrivateFuncs : PublicFuncs;
//            var decStr = new StringBuilder();
//            decStr.Append($"{name}");
//            // Append arguments
//            decStr.Append($"({args})");
//
////            funcs.Append("\t" + decStr);
////            funcs.AppendLine(";\n");
//
//            funcs.AppendLine($"{decStr} {{\n\t{string.Join("\n\t\t", logicLines)}\n}}\n");
        }

//        public void AddField(string type, string name, bool privateVis, string init = "")
//        {
//            var dec = privateVis ? PrivateFuncs : PublicFuncs;
//            dec.Append($"\t\t{type} {name}");
//            if (init != "") dec.Append($" = {init}");
//            dec.AppendLine(";");
//        }

        public void AddMethod(bool privateVis, string type, string name, string args,
            List<string> logicLines, bool virtualFunc = false,
            bool overrideFunc = false, bool pureVirtual = false)
        {
            var funcs = privateVis ? PrivateFuncs : PublicFuncs;
            var decStr = new StringBuilder();
//            if (virtualFunc || pureVirtual) funcs.Append("virtual ");
//            funcs.Append($"\t{type} ");
            decStr.Append($"{name}");
            // Append arguments
            decStr.Append($"({args})");

//            funcs.Append(decStr);
//            if (overrideFunc) funcs.Append(" override");
//            if (pureVirtual) funcs.Append(" = 0");
//            funcs.AppendLine(";\n");

            funcs.AppendLine($"\t{type} {decStr} {{\n\t\t{string.Join("\n\t\t", logicLines)}\n\t}}\n");
        }

        public void Import(string include)
        {
            Imports.Append("#include " + include);
        }


        public void Write()
        {
//            var imports = "";
//            
//            var header = new StringBuilder(HeaderFile.ToString());
//            header.Append($"\n\nclass {name}");
//            if (father != "") header.Append($" : public {father}");
//            header.AppendLine("{");
//            header.Append(PrivateDec);
//            header.Append(PublicDec);
//            header.Append("\n};");
//            
//            File.WriteAllText($"{folder}\\{name}.h",header.ToString());

            CppFile.Append($"{Imports}\n");
            CppFile.Append($"class {name} :public {father}{{\n");


            CppFile.Append(PrivateFuncs);
            CppFile.Append(PublicFuncs);

            CppFile.Append("\n};");
//            CppFile.Append()

            const string EXTENSION = ".cpp";

            File.WriteAllText($"{folder}\\{name}.{EXTENSION}", CppFile.ToString());
        }
    }
}