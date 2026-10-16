using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles configuration sections that are represented by a single XML tag in the .config file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DC RID: 476
	public class SingleTagSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Used internally to create a new instance of this object.</summary>
		/// <returns>The created object handler.</returns>
		/// <param name="parent">The parent of this object.</param>
		/// <param name="context">The context of this object.</param>
		/// <param name="section">The <see cref="T:System.Xml.XmlNode" /> object in the configuration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C60 RID: 3168 RVA: 0x00032754 File Offset: 0x00030954
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable();
			}
			else
			{
				hashtable = (Hashtable)parent;
			}
			if (section.HasChildNodes)
			{
				throw new ConfigurationException("Child Nodes not allowed.");
			}
			XmlAttributeCollection attributes = section.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				hashtable.Add(attributes[i].Name, attributes[i].Value);
			}
			return hashtable;
		}
	}
}
