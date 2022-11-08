using Demo.Docati.DocumentGeneration.Lib;
using Docati.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Docati.DocumentGeneration.CLI
{
    class Program
    {
        private static List<Task> documentGenerationTasks = new List<Task>();
        private static Options options = new Options();
        private static int generatedDocumentsCount = 0;
        private static object _lock = new object();

        static void Main(string[] args)
        {
            var parser = new CommandLine.Parser(opt =>
            {
                opt.HelpWriter = Console.Error;
            });
            if (parser.ParseArguments(args, options))
            {
                //Check for some hard limits first
                if (options.OutputCount > Constants.MAXIMUM_OUTPUT_COUNT)
                    throw new ApplicationException("Output count cannot be greater than " + Constants.MAXIMUM_OUTPUT_COUNT);

                //Apply docati license
                if (File.Exists(Constants.LICENSE_FILE_NAME))
                    License.ApplyLicense(File.ReadAllText(Constants.LICENSE_FILE_NAME));
                else
                    License.ApplyLicense(Constants.FREE_LICENSE);

                DirectoryInfo templatePathDirectoryInfo = new DirectoryInfo(options.TemplatePath);

                Stopwatch watch = Stopwatch.StartNew();
                for (int i = 1; i <= options.OutputCount; i++)
                {
                    int documentNumber = i;
                    Task documentGenerationTask = new Task(()=> { GenerateDocument(documentNumber); });
                    documentGenerationTask.Start();
                    documentGenerationTasks.Add(documentGenerationTask);
                }
                Task.WaitAll(documentGenerationTasks.ToArray());

                watch.Stop();
                Console.WriteLine($"Tried to generate {options.OutputCount} documents.");
                Console.WriteLine($"Generated {generatedDocumentsCount} documents sucessfully.");
                Console.WriteLine($"Total milliseconds ellapsed: {watch.ElapsedMilliseconds}");
            }
        }

        private static void GenerateDocument(int documentNumber)
        {
            try
            {
                DirectoryInfo templatePathDirectoryInfo = new DirectoryInfo(options.TemplatePath);
                int outputFileNamePaddingLength = ((int)Math.Log10(options.OutputCount)) + 1;
                string templateData = GetTemplateData(options.TemplateDataFile, options.TemplatePath);
                var data = DocumentGenerator.GenerateDocument(templatePathDirectoryInfo.FullName, options.TemplateFile, templateData, GeneratedDocumentType.PDF);
                string outputFileName = string.Format("{0}{1}.pdf", options.OutputFilePrefix, documentNumber.ToString("D" + outputFileNamePaddingLength));
                File.WriteAllBytes(outputFileName, data);
                Interlocked.Increment(ref generatedDocumentsCount);
            }
            catch (Exception ex)
            {
                lock (_lock)
                {
                    //PrintError is not thread safe, so we call it in a lock statement
                    Helpers.PrintError(ex);
                }
                //And do not propagate exception
            }
        }

        private static string GetTemplateData(string templateDataFileName, string templatePath)
        {
            if (string.IsNullOrEmpty(templateDataFileName))
                throw new ArgumentException("Should not be empty", nameof(templateDataFileName));

            if (Path.IsPathRooted(templateDataFileName))
            {
                if (File.Exists(templateDataFileName))
                    return File.ReadAllText(templateDataFileName);
            }
            else
            {
                //Filename is relative. First check based on the file name.
                if (File.Exists(templateDataFileName))
                    return File.ReadAllText(templateDataFileName);
                else
                {
                    //File was not found so we will use the templatepath
                    if (string.IsNullOrEmpty(templatePath))
                        throw new ArgumentException("Should not be empty", nameof(templatePath));

                    string templateDataFileNameInTemplatePath = Path.Combine(templatePath, templateDataFileName);
                    if (File.Exists(templateDataFileNameInTemplatePath))
                        return File.ReadAllText(templateDataFileNameInTemplatePath);
                }
            }

            //Could not find template
            throw new ApplicationException("Could not find template:" + templateDataFileName);
        }
    }
}
