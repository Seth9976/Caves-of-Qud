using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000138 RID: 312
	internal sealed class webReferenceOptionsSerializer : XmlSerializer
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x000418D6 File Offset: 0x0003FAD6
		protected override XmlSerializationReader CreateReader()
		{
			return new WebReferenceOptionsSerializationReader();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000418DD File Offset: 0x0003FADD
		protected override XmlSerializationWriter CreateWriter()
		{
			return new WebReferenceOptionsSerializationWriter();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00002B52 File Offset: 0x00000D52
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return true;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000418E4 File Offset: 0x0003FAE4
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((WebReferenceOptionsSerializationWriter)writer).Write5_webReferenceOptions(objectToSerialize);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000418F2 File Offset: 0x0003FAF2
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((WebReferenceOptionsSerializationReader)reader).Read5_webReferenceOptions();
		}
	}
}
