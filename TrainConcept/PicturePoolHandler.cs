using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace SoftObject.TrainConcept
{
    public class PicturePoolHandler
    {
        string m_strPathName="";

        Dictionary<string,Image>  m_dImages=new Dictionary<string,Image>();
        Dictionary<string, string> m_dFullFileNames = new Dictionary<string, string>();

        public PicturePoolHandler()
        {
        }

        public int Initialize(string strPathName)
        {
            m_strPathName = strPathName;
            if (Directory.Exists(strPathName))
            {
                string[] aFiles = System.IO.Directory.GetFiles(m_strPathName, "*.*");
                for (int i = 0; i < aFiles.Length; ++i)
                {
                    Image img = Image.FromFile(aFiles[i]);
                    Bitmap imgSmall = ResizeImage(img, 256, 256);
                    string strKey=Path.GetFileNameWithoutExtension(aFiles[i]);
                    m_dImages[strKey] = imgSmall;
                    m_dFullFileNames[strKey] = aFiles[i];
                }
            }
            return 0;
        }

        public int Add(string strFileName,Image image)
        {
            string l_strFileName=m_strPathName+"\\"+strFileName;

            if (m_dFullFileNames.ContainsValue(l_strFileName))
                return 0;

            if (!File.Exists(l_strFileName) && image!=null)
            {
                try
                {
                    if (!Directory.Exists(m_strPathName))
                        Directory.CreateDirectory(m_strPathName);
                    image.Save(l_strFileName);
                }
                catch
                {
                    return -1;
                }

                Bitmap imgSmall = ResizeImage(image, 256, 256);
                string strKey = Path.GetFileNameWithoutExtension(l_strFileName);
                m_dImages[strKey]=imgSmall;
                m_dFullFileNames[strKey] = l_strFileName;
                return m_dImages.Count;
            }
            return -1;
        }

        public bool Remove(string strUserName)
        {
            string l_strFileName = (string)m_dFullFileNames[strUserName];
            m_dImages.Remove(strUserName);
            m_dFullFileNames.Remove(strUserName);
            GC.Collect();

            if (File.Exists(l_strFileName))
            {
                try
                {
                    File.Delete(l_strFileName);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
            return true;
        }


        public Image Get(string strUserName)
        {
            if (m_dImages.ContainsKey(strUserName))
                return (Image)m_dImages[strUserName];
            return null;
        }

        public Image GetById(int iImageId)
        {
            int i = 0;
            foreach (var e in m_dImages)
            {
                if (i==iImageId)
                    return e.Value;
                ++i;
            }
            return null;
        }

        public int GetId(string strUserName)
        {
            int i = 0;
            foreach(var e in m_dImages)
            {
                if (e.Key == strUserName)
                    return i;
                ++i;
            }
            return -1;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


    }
}
