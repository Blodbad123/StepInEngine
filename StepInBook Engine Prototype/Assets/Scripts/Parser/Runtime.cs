
using System;
using UnityEngine; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Reflection;
using Ionic.Zip;

public class Runtime : MonoBehaviour
{
	/*
     *  @description
     *  This will assume that you called the program with one or more epub files
     *  and it will then iterate through each of them, converting it into a more
     *  handable format. The function here silently skip any non-epub file types
     *  and it will save the content of the files it process under the same name
     *  as they were fed into. You need to monitor the output, from this program
     *  to check if any of the files that you gave it, failed for any reason. If
     *  they do, then you need to look at how the parsing is done in relation to
     *  how the failed epub looks. The format is very volatile so it is very meh
     *  This is the first stage. It will create a temporarily folder and then it
     *  extracts the content of the epub file into this folder, using the ziplib
     *  dll plugin, since Windows by default don't have anything to unzip. After
     *  this it attempts to read the container.xml file with all the information
     *  about the file structure. After this it will create all the requied file
     *  references and then pass the actual root file path onwards to extraction
     *  @end
     */


	static string pathToParsedBooks;
	/*
    public void Awake ()
    {
		pathToParsedBooks = Application.dataPath + "/Books/";
		string pathToEpub = Application.dataPath + "/EpubBooks/bughuntersdiary.epub";

		//foreach (string file in paths.Where(n => n.ToLower().EndsWith(".epub")))
		//{
		if (pathToEpub.EndsWith(".epub", StringComparison.CurrentCulture))
		{
			//Debug.Log("This is an epub file...");

			using (TemporaryFolder path = new TemporaryFolder())
			{
				//string path = Application.dataPath + "/Books/"; 
				using (ZipFile epub = ZipFile.Read(pathToEpub))
				{
					epub.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);

					if (File.Exists(path + "/META-INF/container.xml"))
					{
						var xmlDocument = new XmlDocument().From(path + "/META-INF/container.xml");
						var xmlNamespaces = new XmlNamespaceManager(xmlDocument.NameTable).Assign(new Dictionary<string, string>()
					{
						{ "ns", "urn:oasis:names:tc:opendocument:xmlns:container" }
					});
						Extract(path, path + xmlDocument.SelectNodes("/ns:container/ns:rootfiles/ns:rootfile", xmlNamespaces)[0].Attributes["full-path"].InnerText);
					}
					else
					{
						Debug.Log("The file does not contain a 'container.xml' file...");
					}
				}
			}
		}
    }

*/

	public static void Parse(string inputPath)
	{
		pathToParsedBooks = Application.dataPath + "/Books/";
		//string pathToEpub = Application.dataPath + "/EpubBooks/bughuntersdiary.epub";
		string pathToEpub = inputPath; 
		//foreach (string file in paths.Where(n => n.ToLower().EndsWith(".epub")))
		//{
		if (pathToEpub.EndsWith(".epub", StringComparison.CurrentCulture))
		{
			//Debug.Log("This is an epub file...");

			using (TemporaryFolder path = new TemporaryFolder())
			{
				//string path = Application.dataPath + "/Books/"; 
				using (ZipFile epub = ZipFile.Read(pathToEpub))
				{
					epub.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);

					if (File.Exists(path + "/META-INF/container.xml"))
					{
						var xmlDocument = new XmlDocument().From(path + "/META-INF/container.xml");
						var xmlNamespaces = new XmlNamespaceManager(xmlDocument.NameTable).Assign(new Dictionary<string, string>()
					{
						{ "ns", "urn:oasis:names:tc:opendocument:xmlns:container" }
					});
						Extract(path, path + xmlDocument.SelectNodes("/ns:container/ns:rootfiles/ns:rootfile", xmlNamespaces)[0].Attributes["full-path"].InnerText);
					}
					else
					{
						Debug.Log("The file does not contain a 'container.xml' file...");
					}
				}
			}
		}
	}
    
    /**
     *  @description
     *  This's the main part of the function. You pass the path to the container
     *  of the file, and it will then extract, first all of the meta information
     *  from anything in the 'dc' fields, under the package - metadata namespace
     *  the outputs are pretty much random as it depends entirely on what fields
     *  have been filled out in the epub file. However, some fields are actually
     *  required, so we should at least have a title. You should note that there
     *  can actually be more than one title, more than one authors and so on. It
     *  is a result of the flexibility of epubs. When we pass this to a designer
     *  they can then pick what they want our job here is just to extract all of
     *  the data. You can pipe / parse the output if you want. Here I'm printing
     *  it. But at some point, this runtime should likely be put into the Unitys
     *  runtime, and from there you should just grab all the information and put
     *  them into whatever data structure you have, but again, here I'm printing
     *  @end
     */
    public static void Extract (string root, string file)
    {
        var data            = new GameData();
        var xmlDocument     = new XmlDocument().From(file);
        var xmlNamespaces   = new XmlNamespaceManager(xmlDocument.NameTable).Assign(new Dictionary<string, string>()
        {
            { "ns",         "http://www.idpf.org/2007/opf"      },
            { "dc",         "http://purl.org/dc/elements/1.1/"  },
            { "dcterms",    "http://purl.org/dc/terms/"         }
        });

		List<XmlNode> nodes = new List<XmlNode>(); 
        foreach (XmlNode node in xmlDocument.SelectNodes("/ns:package/ns:metadata/dc:*", xmlNamespaces))
        {
			Debug.Log(node.LocalName.PadRight(25) + " : " + node.InnerText);
			nodes.Add(node); 
        }

		//data.title.Add(title); 
        foreach (XmlNode node in xmlDocument.SelectNodes("/ns:package/ns:manifest/ns:item", xmlNamespaces))
        {
            data.links[node.Attributes["id"].InnerText] = node.Attributes["href"].InnerText;
        }

        foreach (XmlNode node in xmlDocument.SelectNodes("/ns:package/ns:spine/ns:itemref", xmlNamespaces))
        {
            data.spine.Add(data.links[node.Attributes["idref"].InnerText]);
        }

		foreach(XmlNode item in nodes){
			switch(item.LocalName){

				case "title":
					data.title.Add (item.InnerText); 
				break; 

				case "language":
					data.language.Add (item.InnerText); 
				break; 

				case "contributor":
					data.contributor.Add(item.InnerText);
				break; 

				case "coverage":
					data.coverage.Add(item.InnerText);
				break; 

				case "creator":
					data.coverage.Add(item.InnerText);
				break; 

				case "description":
					data.description.Add(item.InnerText);
				break; 

				case "format":
					data.format.Add(item.InnerText);
				break; 

				case "publisher":
					data.publisher.Add(item.InnerText);
				break; 

				case "relation":
					data.relation.Add(item.InnerText);
				break; 

				case "rights":
					data.rights.Add(item.InnerText);
				break; 

				case "source":
					data.source.Add(item.InnerText);
				break; 

				case "subject":
					data.subject.Add(item.InnerText);
				break; 

				case "type":
					data.type.Add(item.InnerText);
				break; 

				default:
					Debug.LogWarning(item.InnerText + " was not included in the data structure...\n" +
				    "Either add it into the script or ignore this message..."); 
				break; 
			}
		}

		// At this point in time, you should do:
		// Read each file at root + the path in data.spine and insert them as pages


		string subPath = "";
		string defaultName = "DefaultBook"; 
		// Create folders and structure... 
		if (data.title.Count >= 1)
		{
			CreateDirectory(data.title[0], pathToParsedBooks);
			subPath = pathToParsedBooks + data.title[0];
		}else{
			Debug.LogWarning("The book has no title... Creating folder with default name...: 'DefaultBook'"); 
			CreateDirectory(defaultName, pathToParsedBooks);
			subPath = pathToParsedBooks + defaultName; 
		}

		// I need a better solution for this...
		int folderCount = 0;
		string strippedString;
		foreach (string sp in data.spine){
			int index = sp.IndexOf("/");
			strippedString = sp; 
			if (index > 0)
			strippedString = sp.Substring(index+1, sp.Length-index - 7);
			string folderName = folderCount + "_" + strippedString; 
			CreateDirectory(folderName, subPath);
			string path = subPath + "/" + folderName + "/";
			string textFolderName = "TEXT/";
			string ImagesFolderName = "IMAGES/";
			CreateDirectory(textFolderName, path);
			CreateDirectory(ImagesFolderName, path); 
			File.AppendAllText(path+textFolderName+"xhtmlContent.txt", File.ReadAllText(root + "/OEBPS/" + sp));
			folderCount++; 
		}

		Debug.Log("Paused..");

    }
	static void CreateDirectory(string name, string path){
		
		Directory.CreateDirectory(path + "/" + name + "/"); 
	}
}
