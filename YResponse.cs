using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiSharp
{
    public class YResponse
    {
        public string status { get; set; }
        public IResult result { get; set; }
        public IError error { get; set; }
    }
    public class IError
    {
        public string message { get; set; }
        public int code { get; set; }
        public string file { get; set; }
        public int line { get; set; }
        public string backtrace { get; set; }
    }

    public class IResult
    {
        public Dictionary<string, string> headers { get; set; }
        public object records { get; set; }
        public object rawData { get; set; }
        public int count { get; set; }
        public bool isMorePages { get; set; }
    }
}
