using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

public enum ElementType { Media, Software, Game, NULL}

namespace bDisk.Classes
{
    #region ELEMENT
    public class TreeViewItemElementInfo
    {
        protected XmlDocument xmlDoc = new XmlDocument();

        public string Name { get; set; }
        public ElementType Type {get; set;}
        public string Info { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Size { get; set; }
        public string RunPath { get; set; }

        public TreeViewItemElementInfo(string path)
        {
            LoadInfoFromXml(path);
        }

        protected virtual void LoadInfoFromXml(string path)
        {
            xmlDoc.Load(path);

            Name = xmlDoc.SelectSingleNode("/INFO/NAME").InnerText;

            string auxString = xmlDoc.SelectSingleNode("/INFO/TYPE").InnerText;
            foreach (ElementType e in Enum.GetValues(typeof(ElementType)))
            {
                if (string.Equals(e.ToString().ToLower(), auxString.ToLower()))
                {
                    Type = e;
                    break;
                }
            }

            String[] auxStrings = xmlDoc.SelectSingleNode("/INFO/RELEASEDATE").InnerText.Split('/');
            ReleaseDate = new DateTime(int.Parse(auxStrings[2]), int.Parse(auxStrings[1]), int.Parse(auxStrings[0]));

            Size = double.Parse(xmlDoc.SelectSingleNode("/INFO/SIZE").InnerText);

            Info = xmlDoc.SelectSingleNode("/INFO/DESCRIPTION").InnerText;

            RunPath = xmlDoc.SelectSingleNode("/INFO/RUNPATH").InnerText;
        }

        public virtual void Run()
        {
        }
    }

    public class TreeViewItemElement : TreeViewItem
    {
        public TreeViewItemElementInfo Info;

        public TreeViewItemElement(string path)
        {
            switch(CheckElementType(path))
            {
                case ElementType.Software:
                    Info = new TreeViewSoftwareElementInfo(path);
                    break;
            }
        }

        public void Run()
        {
            Info.Run();
        }

        private ElementType CheckElementType(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(path);

            string elementTypeString = xmlDoc.SelectSingleNode("/INFO/TYPE").InnerText.ToLower();
            switch(elementTypeString)
            {
                case "software":
                    return ElementType.Software;
                default:
                    return ElementType.NULL;
            }
        }
    }
    #endregion

    #region SOFTWARE
    public class TreeViewSoftwareElementInfo : TreeViewItemElementInfo
    {
        public string Version { get; set; }
        public string Developer { get; set; }

        public TreeViewSoftwareElementInfo(string path) : base(path) {}

        protected override void LoadInfoFromXml(string path)
        {
            base.LoadInfoFromXml(path);
            Version = xmlDoc.SelectSingleNode("/INFO/VERSION").InnerText;
            Developer = xmlDoc.SelectSingleNode("/INFO/DEVELOPER").InnerText;
        }
    }
    #endregion

    #region GAME
    public class TreeViewGameElementInfo : TreeViewSoftwareElementInfo
    {
        public string Publisher { get; set; }
        public TreeViewGameElementInfo(string path) : base(path) { }

        protected override void LoadInfoFromXml(string path)
        {
            base.LoadInfoFromXml(path);
        }
    }
    #endregion
}
