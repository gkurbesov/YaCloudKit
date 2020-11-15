using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ
{
    public interface IRequestContext
    {
        IDictionary<string, string> RequestParameters { get; set; }
        IDictionary<string, string> Headers { get; set; }
        DateTime RequestDateTime { get; set; }

        IRequestContext AddParametr(string key, string value);
        IRequestContext AddHeader(string key, string value);
        byte[] GetContent();

    }
}
