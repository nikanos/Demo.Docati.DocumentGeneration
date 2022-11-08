using CommandLine;
using CommandLine.Text;

namespace Demo.Docati.DocumentGeneration.CLI
{
    class Options
    {
        [Option('p', "path", HelpText = "Template path", DefaultValue = "Files")]
        public string TemplatePath { get; set; }

        [Option('t', "template", HelpText = "Template file name", DefaultValue = "Template.docx")]
        public string TemplateFile { get; set; }

        [Option('d', "data", HelpText = "Template data file name", DefaultValue = "TemplateData.xml")]
        public string TemplateDataFile { get; set; }

        [Option('o', "output-prefix", HelpText = "Output file name prefix", DefaultValue = "Out")]
        public string OutputFilePrefix { get; set; }

        [Option('c', "output-count", HelpText = "Output file count", DefaultValue = 1)]
        public int OutputCount { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("Demo.Docati.DocumentGeneration.CLI"),
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };
            help.AddOptions(this);
            return help;

        }
    }
}
