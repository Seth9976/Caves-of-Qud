using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000DF RID: 223
	internal class MimeXmlReflector : MimeReflector
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00002B4F File Offset: 0x00000D4F
		internal override bool ReflectParameters()
		{
			return false;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000195CC File Offset: 0x000177CC
		internal override bool ReflectReturn()
		{
			MessagePart messagePart = new MessagePart();
			messagePart.Name = "Body";
			base.ReflectionContext.OutputMessage.Parts.Add(messagePart);
			if (typeof(XmlNode).IsAssignableFrom(base.ReflectionContext.Method.ReturnType))
			{
				MimeContentBinding mimeContentBinding = new MimeContentBinding();
				mimeContentBinding.Type = "text/xml";
				mimeContentBinding.Part = messagePart.Name;
				base.ReflectionContext.OperationBinding.Output.Extensions.Add(mimeContentBinding);
			}
			else
			{
				MimeXmlBinding mimeXmlBinding = new MimeXmlBinding();
				mimeXmlBinding.Part = messagePart.Name;
				LogicalMethodInfo method = base.ReflectionContext.Method;
				XmlAttributes xmlAttributes = new XmlAttributes(method.ReturnTypeCustomAttributeProvider);
				XmlTypeMapping xmlTypeMapping = base.ReflectionContext.ReflectionImporter.ImportTypeMapping(method.ReturnType, xmlAttributes.XmlRoot);
				xmlTypeMapping.SetKey(method.GetKey() + ":Return");
				base.ReflectionContext.SchemaExporter.ExportTypeMapping(xmlTypeMapping);
				messagePart.Element = new XmlQualifiedName(xmlTypeMapping.XsdElementName, xmlTypeMapping.Namespace);
				base.ReflectionContext.OperationBinding.Output.Extensions.Add(mimeXmlBinding);
			}
			return true;
		}
	}
}
