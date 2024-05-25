using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Templates
{
    public  class EmailTemplateService
    {
        private static readonly string TemplatesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BusinessService\Templates");

        public static string GetTemplate(string templateName, params (string, string)[] placeholders)
        {
            string templatePath = Path.Combine(TemplatesPath, templateName + ".html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template '{templateName}' not found at path '{templatePath}'");
            }

            string templateContent = File.ReadAllText(templatePath);

            foreach (var (placeholder, value) in placeholders)
            {
                templateContent = templateContent.Replace($"{{{placeholder}}}", value);
            }

            return templateContent;
        }

    }
}
