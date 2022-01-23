using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCWebUpdate
{
    public partial class update : System.Web.UI.Page
    {
        //private string szMainVer = null;  //Hauptversion
        //private string szBesVer = null;  //Nebenversion
        //private string szUpgVer = null;  //Upgrade Version
        //private string szUpdVer = null;  //Update Version
        //private string szLanguage = null;  //de, en_GB, fr_FR, it_IT, es_ES
        //private string szUseType = null;  //0=StandAlone  1=Client  2=Server  
        //private string szVersion = null;
        //private string szKeyId = null;  //KeyId

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //["Language"],["UseType"],["Version"],["KeyId"] -> sind an die Seite übergebenen Argumente
            szLanguage = Request.QueryString["Language"]; //de, en_GB, fr_FR, it_IT, es_ES
            szUseType = Request.QueryString["UseType"];  //0=StandAlone  1=Client  2=Server  
            szVersion = Request.QueryString["Version"];  //z.B. 1.0.0.0
            szKeyId = Request.QueryString["KeyId"];    //KeyId

            //Ermittle Versionsinformationen
            if (szVersion != null)
            {
                string[] aResult = szVersion.Split('.');
                szMainVer = aResult[0];
                szBesVer = aResult[1];
                szUpgVer = aResult[2];
                szUpdVer = aResult[3];
            }
            else
            {
                //Fehler! Keine Versionsinformationen
            }

            //Ermittle die Sprache
            if (szLanguage != null)
            {
                switch (szLanguage)
                {
                    case "de": IsLanguage("de");
                        break;
                    case "en_GB": IsLanguage("en_GB");
                        break;
                    case "fr_FR": IsLanguage("fr_FR");
                        break;
                    case "it_IT": IsLanguage("it_IT");
                        break;
                    case "es_ES": IsLanguage("es_ES");
                        break;
                }
            }
            else
            {
                //Fehler! Keine Sprache -> Standardsprache englisch
                IsLanguage("en_GB");
            }
             * 
            //["Language"],["UseType"],["Version"],["KeyId"] -> sind an die Seite übergebenen Argumente
            szLanguage = Request.QueryString["Language"]; //de, en_GB, fr_FR, it_IT, es_ES
            szUseType = Request.QueryString["UseType"];  //0=StandAlone  1=Client  2=Server  
            szVersion = Request.QueryString["Version"];  //z.B. 1.0.0.0
            szKeyId = Request.QueryString["KeyId"];    //KeyId

            //Ermittle Versionsinformationen
            if (szVersion != null)
            {
                string[] aResult = szVersion.Split('.');
                szMainVer = aResult[0];
                szBesVer = aResult[1];
                szUpgVer = aResult[2];
                szUpdVer = aResult[3];
            }
            else
            {
                //Fehler! Keine Versionsinformationen
            }

            //Ermittle die Sprache
            if (szLanguage != null)
            {
                switch (szLanguage)
                {
                    case "de": IsLanguage("de");
                        break;
                    case "en_GB": IsLanguage("en_GB");
                        break;
                    case "fr_FR": IsLanguage("fr_FR");
                        break;
                    case "it_IT": IsLanguage("it_IT");
                        break;
                    case "es_ES": IsLanguage("es_ES");
                        break;
                }
            }
            else
            {
                //Fehler! Keine Sprache -> Standardsprache englisch
                IsLanguage("en_GB");
            }*/
        }

        protected void NavBarDownload_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
        {

        }

        private void IsLanguage(string language)
        {
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

	}
}