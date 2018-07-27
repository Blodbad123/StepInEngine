
using System;
using UnityEngine; 
using System.IO;

public class TemporaryFolder : IDisposable
{
    public string path
    {
        get
        {
			return (Application.dataPath + "/tmp/"); 
        }
    }
    
    /*
     *  This constructs a temporary folder object. It'll totally delete anything
     *  that is already there with the same name, then recreate the folder again
     */
    public TemporaryFolder ()
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        
        Directory.CreateDirectory(path); 
    }
    
    /*
     *  This function is called when the IDisposable object ends it's call scope
     *  and will then trigger the deletion of the folder again to clean stuff up
     */
    public void Dispose ()
    {
        Directory.Delete(path, true);
    }
    
    /*
     *  This is an implicit cast, that converts this object into the string type
     *  instead. It returns the path variable so the whole object can be used as
     *  the path, rather than having to extract the content of the path variable
     *  every time. This cast will also allow printing to standard out correctly
     */
    public static implicit operator string (TemporaryFolder target)
    {
        return (target.path);
    }
}
