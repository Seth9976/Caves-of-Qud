using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000A4 RID: 164
	internal class DiscoveryDocumentSerializer : XmlSerializer
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x00013B8D File Offset: 0x00011D8D
		protected override XmlSerializationReader CreateReader()
		{
			return new DiscoveryDocumentSerializationReader();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013B94 File Offset: 0x00011D94
		protected override XmlSerializationWriter CreateWriter()
		{
			return new DiscoveryDocumentSerializationWriter();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013B9B File Offset: 0x00011D9B
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("discovery", "http://schemas.xmlsoap.org/disco/");
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013BAD File Offset: 0x00011DAD
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((DiscoveryDocumentSerializationWriter)writer).Write10_discovery(objectToSerialize);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013BBB File Offset: 0x00011DBB
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((DiscoveryDocumentSerializationReader)reader).Read10_discovery();
		}
	}
}
