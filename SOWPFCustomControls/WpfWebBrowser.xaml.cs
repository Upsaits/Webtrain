using mshtml;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.SOWPFCustomControls
{
    /// <summary>
    /// Interaction logic for WebBrowser.xaml
    /// </summary>
    public partial class WPFWebBrowser : UserControl
    {
        public HTMLDocument doc=null;
        private Format format=null;
        private Gui gui=null;
        private PageItem pageItem=null;

        public Gui Gui
        {
            get { return gui; }
            set { gui = value; }
        }

        public Format Format
        {
            get { return format; }
        }

        public WebBrowser WebBrowser
        {
            get { return this.webBrowser1; }
        }

        public WPFWebBrowser()
        {
            InitializeComponent();
        }

        public void newWb(string url,PageItem _pageItem)
        {
            if (doc != null)
                doc.clear();

            Script.HideScriptErrors(webBrowser1, true);
            try
            {
	            if (url=="")
	                webBrowser1.NavigateToString(Properties.Resources.New);
	            else
	                webBrowser1.Navigate(url);
                pageItem = _pageItem;
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void gridwebBrowser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift)!=0)
                {
                    e.Handled = false;
                }
                else 
                    e.Handled = true;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            doc = webBrowser1.Document as HTMLDocument;
            doc.designMode = "On";
            format = new Format(doc, webBrowser1);

            // search for customized Text-Fields and set new size
            if (pageItem != null)
            {
                int actCnt = pageItem.PageActions.Length;
                for (int i = 0; i < actCnt; ++i)
                {
                    ActionItem item = pageItem.PageActions[i];
                    if (item != null)
                    {
                        // Text
                        if (item is TextActionItem)
                        {
                            TextActionItem txtItem = (TextActionItem)item;
                            if (txtItem.customWidth > 0 || txtItem.customHeight> 0)
                                SetDivSize("div" + txtItem.id, txtItem.customWidth, txtItem.customHeight);
                        }
                    }
                }
            }
        }


        private bool SetDivSize(string _id, int iWidth, int iHeight)
        {
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
            {
                if (iWidth > 0)
                    el.style.width = iWidth.ToString();
                if (iHeight > 0)
                    el.style.height = iHeight.ToString();
                return true;
            }

            return false;
        }

        private void gridwebBrowser_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V)
            {
                int test1 = 10;
            }
        }

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Send(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }
    }
    
}
