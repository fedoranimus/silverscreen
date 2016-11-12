using System.Collections.Generic;

namespace Silverscreen.Core.Model {
    public class Attributes
    {
        public string version { get; set; }
    }

    public class Attributes2
    {
        public string offset { get; set; }
        public string total { get; set; }
    }

    public class Response
    {
        public Attributes2 @attributes { get; set; }
    }

    public class Attributes3
    {
        public string url { get; set; }
        public string length { get; set; }
        public string type { get; set; }
    }

    public class Enclosure
    {
        public Attributes3 @attributes { get; set; }
    }

    public class Attributes4
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Attr
    {
        public Attributes4 @attributes { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string title { get; set; }
        public string guid { get; set; }
        public string link { get; set; }
        public string comments { get; set; }
        public string pubDate { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public Enclosure enclosure { get; set; }
        public List<Attr> attr { get; set; }
    }

    public class Channel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string uuid { get; set; }
        public Response response { get; set; }
        public string language { get; set; }
        public List<Item> item { get; set; }
    }

    public class RootObject
    {
        public Attributes @attributes { get; set; }
        public Channel channel { get; set; }
    }
}