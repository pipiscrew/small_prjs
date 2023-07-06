using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using NMG.Core.Domain;
using NMG.Core.TextFormatter;
using NMG.Core.Util;

namespace NMG.Core.Generator
{
    public class CastleGenerator : AbstractGenerator
    {
        public CastleGenerator(ApplicationPreferences applicationPreferences, Table table) : base(applicationPreferences.FolderPath, "Mapping", applicationPreferences.TableName, applicationPreferences.NameSpaceMap, applicationPreferences.AssemblyName, applicationPreferences.Sequence, table, applicationPreferences)
        {
            this.applicationPreferences = applicationPreferences;
        }

        public override void Generate(bool writeToFile = true)
        {
            var compileUnit = GetCompileUnit();

            if (writeToFile)
            {
                WriteToFile(compileUnit, Formatter.FormatSingular(tableName));
            }
            else
            {
                WriteToString(compileUnit);
            }
        }

        public CodeCompileUnit GetCompileUnit()
        {
            var codeGenerationHelper = new CodeGenerationHelper();
            // This is where we construct the constructor
            var compileUnit = codeGenerationHelper.GetCodeCompileUnit(nameSpace, Table.Name.GetFormattedText().MakeSingular(),true);
            
            var mapper = new DataTypeMapper();
            var newType = compileUnit.Namespaces[0].Types[0];
            newType.IsPartial = applicationPreferences.GeneratePartialClasses;

            if (Table.PrimaryKey != null && Table.PrimaryKey.Columns.Count() != 0)
            {
                foreach (var pk in Table.PrimaryKey.Columns)
                {
                    var mapFromDbType = mapper.MapFromDBType(applicationPreferences.ServerType, pk.DataType, pk.DataLength, pk.DataPrecision, pk.DataScale);

                    var declaration = new CodeAttributeDeclaration("PrimaryKey");
                    declaration.Arguments.Add(new CodeAttributeArgument("Column", new CodePrimitiveExpression(pk.Name)));
                    newType.Members.Add(codeGenerationHelper.CreateAutoProperty(mapFromDbType.ToString(), pk.Name.GetFormattedText(), declaration));
                }
            }

            foreach (var fk in Table.ForeignKeys)
            {
                newType.Members.Add(codeGenerationHelper.CreateAutoProperty(fk.References.GetFormattedText().MakeSingular(), fk.References.GetFormattedText().MakeSingular()));
            }

            foreach (var property in Table.Columns.Where(x => x.IsPrimaryKey != true && x.IsForeignKey != true))
            {
                var declaration = new CodeAttributeDeclaration("Property");
                declaration.Arguments.Add(new CodeAttributeArgument("Column", new CodePrimitiveExpression(property.Name)));

                if(property.DataLength.HasValue)
                    declaration.Arguments.Add(new CodeAttributeArgument("Length", new CodePrimitiveExpression(property.DataLength)));

                if (!property.IsNullable)
                {
                    declaration.Arguments.Add(new CodeAttributeArgument("NotNull", new CodePrimitiveExpression(true)));
                }

                var mapFromDbType = mapper.MapFromDBType(applicationPreferences.ServerType, property.DataType, property.DataLength, property.DataPrecision, property.DataScale);
                newType.Members.Add(codeGenerationHelper.CreateAutoProperty(mapFromDbType.ToString(), property.Name.GetFormattedText(), declaration));
            }

            return compileUnit;
        }

        private void WriteToFile(CodeCompileUnit compileUnit, string className)
        {
            var provider = (CodeDomProvider) new CSharpCodeProvider();
            string sourceFile = GetCompleteFilePath(provider, className.MakeSingular());
            var streamWriter = new StringWriter();
            using (provider)
            {
                var textWriter = new IndentedTextWriter(streamWriter, "    ");
                using (textWriter)
                {
                    using (streamWriter)
                    {
                        var options = new CodeGeneratorOptions {BlankLinesBetweenMembers = true};
                        provider.GenerateCodeFromCompileUnit(compileUnit, textWriter, options);
                    }
                }
            }
            var entireContent = CleanupGeneratedFile(streamWriter.ToString());

            using (var writer = new StreamWriter(sourceFile))
            {
                writer.Write(entireContent);
            }
        }

        private void WriteToString(CodeCompileUnit compileUnit)
        {
            var provider = (CodeDomProvider)new CSharpCodeProvider();
            var streamWriter = new StringWriter();
            using (provider)
            {
                var textWriter = new IndentedTextWriter(streamWriter, "    ");
                using (textWriter)
                {
                    using (streamWriter)
                    {
                        var options = new CodeGeneratorOptions { BlankLinesBetweenMembers = true };
                        provider.GenerateCodeFromCompileUnit(compileUnit, textWriter, options);
                    }
                }
            }
            GeneratedCode = CleanupGeneratedFile(streamWriter.ToString());
        }

        protected override string CleanupGeneratedFile(string entireContent)
        {
            entireContent = RemoveComments(entireContent);
            entireContent = AddStandardHeader(entireContent);
            entireContent = FixAutoProperties(entireContent);
            return entireContent;
        }

        // Hack : Auto property generator is not there in CodeDom.
        private static string FixAutoProperties(string entireContent)
        {
            //            entireContent = entireContent.Replace(@"get {
            //            }", "get;");
            //            entireContent = entireContent.Replace(@"set {
            //            }", "set;");
            // Do NOT mess with this...
            entireContent = entireContent.Replace(@"{
        }", "{ }");
            entireContent = entireContent.Replace(
                @"{
            get {
            }
            set {
            }
        }", "{ get; set; }");
            return entireContent;
        }

        private static string AddStandardHeader(string entireContent)
        {
            entireContent = "using Castle.ActiveRecord;" + Environment.NewLine + entireContent;
            entireContent = "using System;" + Environment.NewLine + entireContent;
            entireContent = "using System.Text;" + Environment.NewLine + entireContent;
            entireContent = "using System.Collections.Generic;" + Environment.NewLine + entireContent;
            return entireContent;
        }

        private static string RemoveComments(string entireContent)
        {
            var end = entireContent.LastIndexOf("----------", StringComparison.Ordinal);
            entireContent = entireContent.Remove(0, end + 10);
            return entireContent;
        }

        private string GetCompleteFilePath(CodeDomProvider provider, string className)
        {
            string fileName = filePath + className;
            return provider.FileExtension[0] == '.' ? fileName + provider.FileExtension : fileName + "." + provider.FileExtension;
        }
    }
}