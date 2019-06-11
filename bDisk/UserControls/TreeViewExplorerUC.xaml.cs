using bDisk.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bDisk.UserControls
{
    /// <summary>
    /// Interaction logic for TreeViewExplorerUC.xaml
    /// </summary>
    public partial class TreeViewExplorerUC : UserControl
    {
        public List<TreeViewItem> TreeViewItemList { get; set; }

        public TreeViewExplorerUC()
        {
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            MainTreeView.DataContext = this;
            TreeViewItemList = SearchForXML(@"dirtest");
        }

        private List<TreeViewItem> SearchForXML(string pathToSearch)
        {
            List<TreeViewItem> returnList = new List<TreeViewItem>();

            foreach (string s in Directory.GetDirectories(pathToSearch))
            {
                string name = s.Split('\\').Last();
                TreeViewItem item = new TreeViewItem();
                item.Header = name;
                item.Tag = s;
                item.ItemsSource = SearchForXML(s);
                item.Focusable = false;
                returnList.Add(item);
            }

            foreach (string s in Directory.GetFiles(pathToSearch))
            {
                if (!s.EndsWith(".xml"))
                {
                    continue;
                }
                string name = s.Split('\\').Last();
                TreeViewItemElement item = new TreeViewItemElement(s);

                item.Tag = s;
                item.Header = item.Info.Name;
                returnList.Add(item);
            }

            return returnList;
        }
    }
}
