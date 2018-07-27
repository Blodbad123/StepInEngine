
using System;
using System.Collections.Generic;
using System.Xml;

public static class Extensions
{
    public static XmlNamespaceManager Assign (this XmlNamespaceManager source, Dictionary<string, string> namespaces)
    {
        foreach (KeyValuePair<string, string> ns in namespaces)
        {
            source.AddNamespace(ns.Key, ns.Value);
        }
        
        return (source);
    }
    
    public static XmlDocument From (this XmlDocument source, string path)
    {
        source.Load(path);
        
        return (source);
    }
}
