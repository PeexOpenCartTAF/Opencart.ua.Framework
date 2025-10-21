using Opencart.ua.Tools.LogsHelpers;
using System.Text;

namespace Opencart.ua.Tools.Reporting
{
    public static class CustomHtmlReportGenerator
    {
        public static void Generate(List<TestResult> results, string outputPath)
        {
            OpenCartSeriLog.Debug("Preparing Report...");

            var rows = new StringBuilder();

            foreach (var r in results)
            {
                var css = r.Status.ToLower();

                string screenshotCell = string.IsNullOrEmpty(r.ScreenshotPath)
                    ? @"<div class='screenshot-container'>
                            <div class='screenshot-placeholder'>PASS</div>
                        </div>"
                    : $@"
                        <div class='screenshot-container'>
                            <img src='{r.ScreenshotPath}' class='screenshot-thumb' alt='screenshot' onclick='openModal(this)' /><br>
                            <a href='{r.ScreenshotPath}' target='_blank'>Open screenshot in a new tab</a>
                        </div>";

                rows.AppendLine($@"
                    <tr class=""{css}"">
                        <td>{r.TestName}</td>
                        <td>{r.Status}</td>
                        <td>{r.Duration:F2}</td>
                        <td>{r.ErrorMessage}</td>
                        <td>{screenshotCell}</td>
                    </tr>");
            }

            string html = File.ReadAllText("Reporting\\template.html");
            html = html.Replace("{{rows}}", rows.ToString());

            File.WriteAllText(outputPath, html);

            OpenCartSeriLog.Debug($"Report saved at {outputPath}");
        }
    }
}
