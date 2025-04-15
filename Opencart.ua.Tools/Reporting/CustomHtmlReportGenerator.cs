using System.Text;

namespace Opencart.ua.Tools.Reporting
{
    public static class CustomHtmlReportGenerator
    {
        public static void Generate(List<TestResult> results, string outputPath)
        {
            var rows = new StringBuilder();

            foreach (var r in results)
            {
                var css = r.Status.ToLower();
                rows.AppendLine($@"
                    <tr class=""{css}"">
                        <td>{r.TestName}</td>
                        <td>{r.Status}</td>
                        <td>{r.Duration:F2}</td>
                        <td>{r.ErrorMessage}</td>
                        <td><a href={r.ScreenshotPath}>Screenshot</a></td>
                    </tr>");
            }

            string html = File.ReadAllText("Reporting\\template.html");
            html = html.Replace("{{rows}}", rows.ToString());

            File.WriteAllText(outputPath, html);
        }
    }
}
