namespace SUS.Http
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            var lines = requestString.Split(new string[] {HttpConstants.NewLine}, StringSplitOptions.None);

            //GET /page HTTP/1.1
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = headerLineParts[0];
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }

                this.Body = bodyBuilder.ToString();
            }
        }

        public string Path { get; set; }

        //GET,POST etc
        public string Method { get; set; }
        public List<Header> Headers { get; set; }
        public List<Cookie> Cookies { get; set; }

        //html
        public string Body { get; set; }
    }
}
