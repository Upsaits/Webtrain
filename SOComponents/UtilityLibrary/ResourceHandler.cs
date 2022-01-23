using System;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace SoftObject.SOComponents.UtilityLibrary
{
	/// <summary>
	/// Zusammendfassende Beschreibung für ResourceHandler.
	/// </summary>
	public class ResourceHandler
	{
		ResourceManager		m_rm=null;

		public ResourceHandler(string resxName,Assembly assembly)
		{
			m_rm = new ResourceManager(resxName,assembly);
		}

		public Bitmap GetBitmap(string name)
		{
			return (Bitmap)m_rm.GetObject(name);
		}

		public Icon GetIcon(string name)
		{
			return (Icon)m_rm.GetObject(name);
		}
	}
}
