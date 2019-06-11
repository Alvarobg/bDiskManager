using bDisk.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ElementViewerUC.xaml
    /// </summary>
    public partial class ElementViewerUC : UserControl
    {
        public ElementViewerUC()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Loaded += OnWindowLoaded;
            }
        }

        private void OnWindowLoaded(object sender, EventArgs e)
        {
            App.mainWindow.treeViewExplorer.MainTreeView.SelectedItemChanged += UpdateElement;
        }

        private void UpdateElement(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item;
            TreeViewItemElement element;

            if ((sender as TreeView).SelectedItem == null)
            {
                return;
            }

            item = (sender as TreeView).SelectedItem as TreeViewItem;
            if (item is TreeViewItemElement)
            {
                element = (TreeViewItemElement)((sender as TreeView).SelectedItem);

                switch(element.Info.Type)
                {
                    case ElementType.Software:
                        UpdateSoftwareElement(element.Info as TreeViewSoftwareElementInfo);
                        break;
                }
            }
        }

        private void UpdateSoftwareElement(TreeViewSoftwareElementInfo info)
        {
            elementNameText.Content = info.Name;
            softwareVersionText.Content = info.Version;
            softwareDeveloperText.Content = info.Developer;
            elementReleaseDateText.Content = info.ReleaseDate;
            elementSizeText.Content = info.Size + "MB";
            elementDescriptionText.Text = info.Info;
        }
    }
}
