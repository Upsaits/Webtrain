using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using mshtml;
using System.Reflection;
using System.Security.Permissions;
using SoftObject.SOComponents.Controls;
using Vereyon.Windows;

namespace SoftObject.SOComponents.UtilityLibrary
{
	/// <summary>
	/// Summary description for DHTMLParser.
	/// </summary>
	/// 
	// User Log-in 
	public delegate bool fnFindKeyword(string _key,ref string _text,ref string _description);

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class DHTMLParser : IDisposable
	{
        private WebBrowserEx webBrowser;
		private IHTMLDocument2 doc;
		private fnFindKeyword  fnFindKeyword=null;
        private ScriptingBridge m_bridge = null;
        private Action<string,bool,string> m_quizResultAction=null;
        private Action<bool> m_setWorkoutButtonStateAction = null;

        public DHTMLParser(WebBrowserEx _webBrowser, IHTMLDocument2 _doc, fnFindKeyword _fnFindKeyword,
                           Action<string, bool, string> _getQuizResultAction = null,
                           Action<bool> setWorkoutButtonStateAction=null)
        {
            webBrowser = _webBrowser;
            doc = _doc;
            fnFindKeyword = _fnFindKeyword;
            m_quizResultAction = _getQuizResultAction;
            m_setWorkoutButtonStateAction = setWorkoutButtonStateAction;
            InitScript();
        }

		// call über Content\common\controls\SOQuiz_Client.js#13
		// window.external.OnQuizResult(params.quizType, params.result, params.inputs);
		// ermöglicht über ScriptingBridge Klasse in Modul Windows.WebBrowser
		// Zusatzinfo: sendQuizResult wird aufgerufen im RQuiz-Javascript Teil
		// Contents\Content\common\controls\rquiz-3.0\rquiz\rquiz.js#3071
		// OnSetWorkoutButtonState() gleiches Konzepts
		public void OnQuizResult(string strType, bool bResult, string strInputs)
        {
            if (m_quizResultAction != null)
                m_quizResultAction.Invoke(strType,bResult,strInputs);
        }

        public void OnSetWorkoutButtonState(bool bIsEnabled)
        {
            if (m_setWorkoutButtonStateAction != null)
                m_setWorkoutButtonStateAction.Invoke(bIsEnabled);
        }


        public DHTMLParser(IHTMLDocument2 _doc)
        {
            doc = _doc;
        }

        public void InitScript()
        {
            // Ensure that the JSON object is available.
            var src = GetSODhtmlJsSource();
            if (webBrowser.Document != null) 
                webBrowser.Document.InvokeScript("eval", new object[] {src});

            m_bridge = new ScriptingBridge(webBrowser, true);
            m_bridge.Initialize();

            webBrowser.ObjectForScripting = this;
            //webBrowser.Document.InvokeScript("execScript", new object[] { strJavaScript, "JavaScript" });
        }

        /// <summary>
        /// Return the JSON polyfill source code. Override to provide a custom implemenation.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSODhtmlJsSource()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Content.so_dhtml.js";

            //string[] strNames = assembly.GetManifestResourceNames();
            //Console.WriteLine(strNames.ToString());
            using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
        }

        public bool Call_setShowElement(string objId, bool bIsVisible)
        {
            if (bIsVisible)
                webBrowser.Document.InvokeScript("showDiv", new object[] { objId });
            else
                webBrowser.Document.InvokeScript("hideDiv", new object[] { objId });
            return true;
        }

        public void Call_setEnabled(string objId, bool bIsEnabled)
        {
            webBrowser.Document.InvokeScript("setEnabled", new object[] { objId, bIsEnabled });
        }

        public void Call_setChecked(string objId, bool bIsChecked)
        {
            webBrowser.Document.InvokeScript("setChecked", new object[] { objId, bIsChecked });
        }

        public bool Call_getChecked(string objId)
        {
            try
            {
	            return (bool)webBrowser.Document.InvokeScript("getChecked", new object[] { objId });
            }
            catch (System.Exception /*ex*/)
            {
                return false;
            }
        }

        public bool Call_getZoom(out int iZoomVal)
        {
            object result = webBrowser.Document.InvokeScript("getZoom", new object[] { });
            if (result != null)
            {
                double dVal = Convert.ToDouble(result);
                iZoomVal = (int)(dVal * 100);
                return true;
            }

            iZoomVal = -1;
            return false;
        }

		public bool FindText(string _id,ref string _text)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
			if (el!=null)
			{
				// id des Elementes mit Text ersetzen
				_text=el.innerHTML;
				return true;
			}
			return false;
		}

        public bool SetDivAt(string _id, int xPos, int yPos)
        {
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
            {
                el.style.left = xPos.ToString();
                el.style.top = yPos.ToString();
                return true;
            }

            return false;
        }

        public bool SetDivSize(string _id, int iWidth, int iHeight)
        {
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
            {
                if (iWidth>0)
                    el.style.width = iWidth.ToString();
                if (iHeight>0)
                    el.style.height = iHeight.ToString();
                return true;
            }

            return false;
        }

        public bool InsertAfter(string _id, string _text)
        {
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
            {
                try
                {
                    el.insertAdjacentHTML("afterend", _text);
                    return true;
                }
                catch (System.Exception)
                {

                }
            }
            return false;
        }


		public bool ReplaceText(string _id,string _text)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
			{
                string text = _text;
                string str = el.innerHTML;
                text = str.Replace(_id, text);
                try
				{
					// id des Elementes mit Text ersetzen
					// Text durchparsen
	                if (ParseText(ref text))
	                {
	                    // Änderungen im Text durchgeführt->neu einfügen
	                    el.innerHTML = "";
	                    el.insertAdjacentHTML("afterBegin", text);
	                }
	                else
	                {
	                    // HTML-Text ersetzen
	                    el.innerHTML = text;
	                }
	                return true;
				}
                catch (System.Exception /*ex*/)
				{
                    try
                    {
	                    el.outerHTML = text;
                    }
                    catch (System.Exception ex1)
                    {
                        Debug.WriteLine(ex1.Message);                    	
                    }
				}
			}
			return false;
		}

		public bool SetElementAttributes(string _id,string _Attr_Src,int _Attr_width,int _Attr_height)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
			if (el!=null)
			{
                if (_Attr_Src.Length>0)
                    el.setAttribute("src", _Attr_Src);
				// Wenn Größen angegeben wurden, dann übernehmen
                if (_Attr_width > 0)
                    el.style.width = _Attr_width;
				if (_Attr_height>0)
                    el.style.height = _Attr_height;
                return true;
			}
			return false;
		}

		public bool SetShowElement(string _id,bool _isVisible)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null)
			{
				if (_isVisible)
					el.style.visibility = "visible";
				else
					el.style.visibility = "hidden";
				return true;
			}
			return false;
		}

		public bool GetInputCheck(string _id)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null && el is IHTMLInputElement)
				return (((IHTMLInputElement)el).@value == "on");

			return false;
		}

		public void SetInputEnabled(string _id,bool _isEnabled)
		{
            IHTMLElement el = doc.all.item(_id, 0) as IHTMLElement;
            if (el != null && el is IHTMLInputElement)
				((IHTMLInputElement)el).disabled = !_isEnabled;
		}

		public void SetInputCheck(string _id,bool _isChecked)
		{
            IHTMLElement inpEl = doc.all.item(_id, 0) as IHTMLElement;
            if (inpEl != null && inpEl is IHTMLInputElement)
			{	
				if (_isChecked)
					((IHTMLInputElement)inpEl).@value = "on";
				else
					((IHTMLInputElement)inpEl).@value = "off";
			}

			string imgId = _id + "_Image";

            IHTMLElement imgEl = doc.all.item(imgId, 0) as IHTMLElement;
            if (imgEl != null && imgEl is IHTMLImgElement)
			{	
				if (_isChecked)
					((IHTMLImgElement)imgEl).src = "img/checked.gif";
				else
					((IHTMLImgElement)imgEl).src = "img/unchecked.gif";
			}
		}

		// ParseText
		// Text inhaltlich durchparsen
		// --> Änderungen durchgeführt?
	    public bool ParseText(ref string text)
		{
			// Schlüsselwörter gefunden?
			bool keyFound = ReplaceKeywords(ref text);
			// Kommandos gefunden=
			bool cmdFound = ReplaceCommands(ref text);

			return (keyFound || cmdFound);
		}

		// ReplaceKeywords
		// Schlüsselwörter ersetzen
		// Syntax: {{*[keyWord]*}}
		// --> Änderungen durchgeführt?
		private bool ReplaceKeywords(ref string _text)
		{
			int found=0;
			int keyStart;
			// Startstring suchen
			while ((keyStart=_text.IndexOf("{{*"))>=0)
			{
				// Ende suchen
				int keyEnd = _text.IndexOf("*}}");
				if (keyEnd>=0)
				{
					// Schlüsselname suchen
					string key=_text.Substring(keyStart+3,keyEnd-keyStart-3);
					string keyText="";
					string keyDescription="";
		
					if (fnFindKeyword!=null)
					{
                        fnFindKeyword(key, ref keyText, ref keyDescription);
						// Schlüsselwort als SPAN einfügen, Beschreibung via Java-Script aktivieren
						string toReplace=_text.Substring(keyStart,keyEnd-keyStart+3);
						string replText=toReplace;

						if (keyDescription.Length>0)
							replText=String.Format("<span id=\"{0}\" class=\"tooltip\">{1}<span class=\"tooltiptext\">{2}</span></span>",key,keyText,keyDescription);
						else
							replText=keyText;

						_text=_text.Replace(toReplace,replText);
						++found; // gefunden!
					}
				}
				else
				{
					string l_text;
					l_text = String.Format("zugehöriges *}} nicht gefunden!");
                    MessageBox.Show(l_text, "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				}
			}
			return (found>0);
		}

		// ReplaceCommands
		// Kommandos ersetzen
		// Syntax: {{+[Command]+}}
		// --> Änderungen durchgeführt?
		private bool ReplaceCommands(ref string text)
		{
			int found=0;
			int keyStart;
			// Startstring suchen
			while ((keyStart=text.IndexOf("{{+"))>=0)
			{
				// Ende suchen
				int keyEnd = text.IndexOf("+}}");
				if (keyEnd>=0)
				{
					string key=text.Substring(keyStart+3,keyEnd-keyStart-3);
					string toReplace=text.Substring(keyStart,keyEnd-keyStart+3);
					string replText=toReplace;

					// Linefeed?
					if (key=="LF")
					{
						replText="<br>";
						// durch <BR> ersetzen
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "ALPHA")
					{
						replText="&#945;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "BETA")
					{
						replText="&#946;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "GAMMA")
					{
						replText="&#947;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "THETA")
					{
						replText="&#952;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "KAPPA")
					{
						replText="&#954;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "EPSILON")
					{
						replText="&#949;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "LAMBDA")
					{
						replText="&#955;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "ETA")
					{
						replText="&#951;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else if (key == "PHI")
					{
						replText="&#966;";
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
					else
					{
						replText=key;
						text=text.Replace(toReplace,replText);
						++found; // gefunden!
					}
				}
				else
				{
					MessageBox.Show("zugehöriges +}} nicht gefunden!","WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				}
			}

			// Startstring suchen
			while ((keyStart=text.IndexOf("{{-"))>=0)
			{
				// Ende suchen
				int keyEnd = text.IndexOf("-}}");
				if (keyEnd>=0)
				{
					string subStr=text.Substring(keyStart+3,keyEnd-keyStart-3);
					string toReplace=text.Substring(keyStart,keyEnd-keyStart+3);

					string replText=String.Format("<SUB>{0}</SUB>",subStr);
					text=text.Replace(toReplace,replText);
					++found; // gefunden!
				}
				else
				{
					MessageBox.Show("zugehöriges -}} nicht gefunden!","WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				}
			}

			return (found>0);
		}

        public void Call_addImage2Gallery(string imgSmall,string imgLarge)
        {
            m_bridge.InvokeFunction("so_addImage2Gallery", new object[] { imgSmall, imgLarge });
        }

        public void Call_showGallery()
        {
            m_bridge.InvokeFunction("so_showGallery");
        }

        public void Call_initQuiz()
        {
            m_bridge.InvokeFunction("so_initQuiz");
        }

        public void Call_EndQuiz()
        {
            m_bridge.InvokeFunction("so_endQuiz");
        }

        public int Call_getQuizResult()
        {
            return m_bridge.InvokeFunction<int>("so_getQuizResult", new object[] {0, "test"});
        }

        public void Dispose()
        {
        }
    }
}
