### Demo.Docati.DocumentGeneration
PDF generation demo based on word templates and XML data using the Docati library and Word-AddIn. See https://www.docati.com/

### Command line arguments
  -p, --path             (Default: Files) Template path

  -t, --template         (Default: Template.docx) Template file name

  -d, --data             (Default: TemplateData.xml) Template data file name

  -o, --output-prefix    (Default: Out) Output file name prefix

  -c, --output-count     (Default: 1) Output file count

### Examples
1. Generate 1 document with prefix Result using the template template.docx in the default template path and the XML data in TemplateData.xml

        Demo.Docati.DocumentGeneration.CLI.exe -t Template.docx -d TemplateData.xml -o Result -c 1

2. Generate 1 document (default) with prefix Out (default) using the template template.el.docx in the default template path and the XML data in TemplateData.el.xml

        Demo.Docati.DocumentGeneration.CLI.exe -t Template.el.docx -d TemplateData.el.xml

3. Generate 10 documents with prefix Out (default) using the template template.ru.docx in the default template path and the XML data in TemplateData.ru.xml
 
        Demo.Docati.DocumentGeneration.CLI.exe -t Template.ru.docx -d TemplateData.ru.xml -c 10


### Notes
Please note that there are some limitations with the free version of the library:
1. Documents cannot contain more than 15 paragraphs
2. The generation process will always take at least 2 seconds

See https://www.docati.com/Pricing for a description of the limitations
