using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides name/value-pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BD RID: 445
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BAB RID: 2987 RVA: 0x00031498 File Offset: 0x0002F698
		public object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetNameValueCollection(parent as NameValueCollection, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000311D3 File Offset: 0x0002F3D3
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000311DA File Offset: 0x0002F3DA
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
