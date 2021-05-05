using System;
using System.Collections.Generic;
using System.Text;

namespace YetiSharp
{
    public class RecordInfo
    {
        public string status { get; set; }
        public AddResult result { get; set; }
    }
    public class AddResult
    {
        public int id { get; set; }
        public object skippedData { get; set; }
    }
}
