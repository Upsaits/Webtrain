using mshtml;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.SOWPFCustomControls
{
    public partial class WebEditor : Window
    {
        private string m_strFileName = "";
        PageItem m_pageItem = null;
        Gui m_gui;

        public Gui Gui
        {
            get { return m_gui; }
            set { m_gui = value; }
        }

       
        public WebEditor(ref PageItem pageItem, string strFileName = "")
        {
            m_strFileName = strFileName;
            m_pageItem = pageItem;
            InitializeComponent();
        }

        private void SettingsBold_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.bold();  
        }

        private void SettingsItalic_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.Italic();
        }

        private void SettingsUnderLine_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.Underline();
        }

        private void SettingsRightAlign_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.Underline();
        }

        private void SettingsLeftAlign_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.JustifyLeft();
        }

        private void SettingsCenter2_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.JustifyCenter();
        }

        private void SettingsJustifyRight_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.JustifyRight();
        }

        private void SettingsJustifyFull_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.JustifyFull();
        }

        private void SettingsInsertOrderedList_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.InsertOrderedList();
        }

        private void SettingsBullets_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.InsertUnorderedList();
        }

        private void SettingsOutIdent_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.Outdent();            
        }

        private void SettingsIdent_Click(object sender, RoutedEventArgs e)
        {
            Gui.Format.Indent();  
        }

        private void RibbonButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocument();
        }

        private void RibbonButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocumentSelectFile();
        }

        private void RibbonButtonOpenweb_Click(object sender, RoutedEventArgs e)
        {
            webBrowserEditor.newWb(@"http://www.codeproject.com/",null);
        }

        private void SettingsFontColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsFontColor();
        }

        private void SettingsBackColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsBackColor();
        }

        private void SettingsAddLink_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddLink();
        }

        private void SettingsAddImage_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddImage();
        }

        private void RibbonButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Gui.RibbonButtonSave(ref m_pageItem);
        }

        private void RibbonComboboxFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFonts(RibbonComboboxFonts);            
        }

        private void RibbonComboboxFontHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFontHeight(RibbonComboboxFontHeight);
        }

        private void RibbonComboboxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFormat(RibbonComboboxFormat);
        }

        private void EditWeb_Click(object sender, RoutedEventArgs e)
        {
            Gui.EditWeb();
        }

        private void ViewHTML_Click(object sender, RoutedEventArgs e)
        {
            Gui.ViewHTML();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gui = new Gui(webBrowserEditor, HtmlEditor1);
            this.webBrowserEditor.Gui = Gui;

            RibbonComboboxFormat.ItemsSource = Gui.RibbonComboboxFormatInitionalisation();
            RibbonComboboxFormat.SelectedIndex = 0;

            RibbonComboboxFontHeight.ItemsSource = Gui.RibbonComboboxFontSizeInitialisation();
            RibbonComboboxFontHeight.Text = "3";

            RibbonComboboxFonts.ItemsSource = Fonts.SystemFontFamilies;
            RibbonComboboxFonts.Text = "Times New Roman";

            if (m_strFileName.Length > 0)
                Gui.newdocumentFile(m_strFileName,m_pageItem);
            else
                Gui.newdocument();
        }

        private void MainWindow1_Closed(object sender, System.EventArgs e)
        {
        }
    }
}
