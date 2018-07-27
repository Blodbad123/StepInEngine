
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class GameData
{
    /**
     *  @description
     *  The following is a list of data field extracted from the ePub format, as
     *  it is parsed. There is no guarantee, that all of these fields are actual
     *  stored in the ePub file, so they start out as empty and it is the reader
     *  who should check if they exist or not. Obviously the artist can then add
     *  or delete these information after the conversion have happened to modify
     *  them. The reason that all of these are lists, is kinda stupid, but there
     *  is a possibility that an ePub contains MORE than one of each and in that
     *  case, we would rather just save all of them than risk losing information
     *  @end
     */
    public List<string> identifier          = new List<string>();
    public List<string> title               = new List<string>();
    public List<string> language            = new List<string>();
    public List<string> contributor         = new List<string>();
    public List<string> coverage            = new List<string>();
    public List<string> creator             = new List<string>();
    public List<string> description         = new List<string>();
    public List<string> format              = new List<string>();
    public List<string> publisher           = new List<string>();
    public List<string> relation            = new List<string>();
    public List<string> rights              = new List<string>();
    public List<string> source              = new List<string>();
    public List<string> subject             = new List<string>();
    public List<string> type                = new List<string>();
    
    /**
     *  @description
     *  The following is two very important aspects to the ePub format, and they
     *  should be understood before moving on. EPub declares a 'spine' whitch is
     *  the order of elements, as they appear. So the order, of elements in this
     *  equals the order of elements as they would appear in the ereader version
     *  However! The names contained here are just references. Everything inside
     *  @end
     */
    public Dictionary<string, string> links = new Dictionary<string, string>();
    public List<string> spine               = new List<string>();
}
