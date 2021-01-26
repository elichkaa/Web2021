
namespace SUS.Http
{
    using System;

    public class Cookie
    {
        public Cookie(string cookie)
        {
            var cookieParts = cookie.Split("=", 2, StringSplitOptions.RemoveEmptyEntries);
            this.Name = cookieParts[0];
            this.Value = cookieParts[1];
        }
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
