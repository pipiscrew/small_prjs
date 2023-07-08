using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using NMG.Core.Domain;
using NMG.Core.TextFormatter;

namespace NMG.Core.Generator
{
    public abstract class AbstractGenerator : IGenerator
    {
        protected Table Table;
        protected string assemblyName;
        protected string filePath;
        protected string nameSpace;
        protected string sequenceName;
        protected string tableName;
		internal const string TABS = "\t\t\t";
    	protected string ClassNamePrefix { get; set;}
        protected ApplicationPreferences applicationPreferences;

        protected AbstractGenerator(string filePath, string specificFolder, string tableName, string nameSpace, string assemblyName, string sequenceName, Table table, ApplicationPreferences appPrefs)
        {
            this.filePath = filePath;
            if(appPrefs.GenerateInFolders)
            {
                this.filePath = Path.Combine(filePath, specificFolder);
                if(!this.filePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    this.filePath = this.filePath + Path.DirectorySeparatorChar;
                }
            }
            this.tableName = tableName;
            this.nameSpace = nameSpace;
            this.assemblyName = assemblyName;
            this.sequenceName = sequenceName;
            Table = table;
            Formatter = TextFormatterFactory.GetTextFormatter(appPrefs);
            this.applicationPreferences = appPrefs;
        }

        public bool UsesSequence
        {
            get
            {
                return !String.IsNullOrEmpty(sequenceName);
            }
        }

        public ITextFormatter Formatter { get; set; }

        public string GeneratedCode { get; set; }

        public abstract void Generate(bool writeToFile = true);

        protected string WriteToString(CodeCompileUnit compileUnit, CodeDomProvider provider)
        {
            var streamWriter = new StringWriter();
            using (provider)
            {
                var textWriter = new IndentedTextWriter(streamWriter, "    ");
                using (textWriter)
                {
                    using (streamWriter)
                    {
                        var options = new CodeGeneratorOptions { BlankLinesBetweenMembers = false };
                        provider.GenerateCodeFromCompileUnit(compileUnit, textWriter, options);
                    }
                }
            }

            return CleanupGeneratedFile(streamWriter.ToString());
        }

        protected abstract string CleanupGeneratedFile(string generatedContent);
    }
}