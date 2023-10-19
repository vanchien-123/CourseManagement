 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Email
{
    public class BaseEmailTemplateModel
    {
        public string To { get; set; }

        public string GetBody()
        {
            return ReadTemplateFile("html");
        }

        public string GetSubject()
        {
            return ReadTemplateFile("txt");
        }

        private static string GetPath([CallerFilePath] string fileName = null)
        {
            return fileName;
        }

        private string ReadTemplateFile(string fileType)
        {
            var fileContent = string.Empty;
            var modelName = this.GetType().Name.Replace("EmailModel", "");
            var test = Directory.GetParent(GetPath()).GetDirectories();
            var htmlFile = Directory.GetParent(GetPath()).GetDirectories()
                .FirstOrDefault(x => x.Name == modelName)?
                .GetFiles().Where(x => x.Extension == $".{fileType}").FirstOrDefault();

            if (htmlFile != null)
            {
                fileContent = File.ReadAllText(htmlFile.FullName);
            }

            var allProperties = this.GetType()
                .GetProperties().ToList();

            foreach (var prop in allProperties)
            {
                fileContent = fileContent.Replace($"{{{{{prop.Name}}}}}",
                    prop.GetValue(this, null)?.ToString());
            }

            return fileContent;
        }
    }
}
