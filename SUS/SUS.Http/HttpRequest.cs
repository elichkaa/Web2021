namespace SUS.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class HttpRequest
    {
        private StringBuilder sb;
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.sb = new StringBuilder();
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
                    continue;
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

            if (this.Headers.Any(x => x.Name == HttpConstants.CookieOnRequest))
            {
                //Cookie: data
                var cookieValue = this.Headers.FirstOrDefault(x => x.Name == HttpConstants.CookieOnRequest)?.Value;
                var cookies = cookieValue.Split(new string[]{"; "}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookie in cookies)
                {
                    var cookieHttp = new Cookie(cookie);
                    this.Cookies.Add(cookieHttp);
                }
            }
        }

        public string Path { get; set; }

        //GET,POST etc
        public string Method { get; set; }
        public List<Header> Headers { get; set; }
        public List<Cookie> Cookies { get; set; }

        //if method is post
        public string Body { get; set; }

        public override string ToString()
        {
            foreach (var header in Headers)
            {
                sb.AppendLine(header.ToString());
            }
            
            sb.AppendLine(this.Body);
            return sb.ToString();
        }
    }
}
