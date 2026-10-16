using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides key/value pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B1 RID: 433
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B7C RID: 2940 RVA: 0x000311B9 File Offset: 0x0002F3B9
		public virtual object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetDictionary(parent as IDictionary, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A string value containing the name of the key attribute.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x000311D3 File Offset: 0x0002F3D3
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A string value containing the name of the value attribute.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000311DA File Offset: 0x0002F3DA
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
