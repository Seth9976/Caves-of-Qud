using System;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000E5 RID: 229
	internal class SchemaCompiler
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001B51E File Offset: 0x0001971E
		internal static StringCollection Warnings
		{
			get
			{
				if (SchemaCompiler.warnings == null)
				{
					SchemaCompiler.warnings = new StringCollection();
				}
				return SchemaCompiler.warnings;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001B536 File Offset: 0x00019736
		internal static StringCollection Compile(XmlSchemas schemas)
		{
			SchemaCompiler.AddImports(schemas);
			SchemaCompiler.Warnings.Clear();
			schemas.Compile(new ValidationEventHandler(SchemaCompiler.ValidationCallbackWithErrorCode), true);
			return SchemaCompiler.Warnings;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001B560 File Offset: 0x00019760
		private static void AddImport(XmlSchema schema, string ns)
		{
			if (schema.TargetNamespace == ns)
			{
				return;
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaImport xmlSchemaImport = ((XmlSchemaExternal)xmlSchemaObject) as XmlSchemaImport;
				if (xmlSchemaImport != null && xmlSchemaImport.Namespace == ns)
				{
					return;
				}
			}
			XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
			xmlSchemaImport2.Namespace = ns;
			schema.Includes.Add(xmlSchemaImport2);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001B5F4 File Offset: 0x000197F4
		private static void AddImports(XmlSchemas schemas)
		{
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				SchemaCompiler.AddImport(xmlSchema, "http://schemas.xmlsoap.org/soap/encoding/");
				SchemaCompiler.AddImport(xmlSchema, "http://schemas.xmlsoap.org/wsdl/");
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001B658 File Offset: 0x00019858
		internal static string WarningDetails(XmlSchemaException exception, string message)
		{
			XmlSchemaObject xmlSchemaObject = exception.SourceSchemaObject;
			string text;
			if (exception.LineNumber == 0 && exception.LinePosition == 0)
			{
				text = SchemaCompiler.GetSchemaItem(xmlSchemaObject, null, message);
			}
			else
			{
				string text2 = null;
				if (xmlSchemaObject != null)
				{
					while (xmlSchemaObject.Parent != null)
					{
						xmlSchemaObject = xmlSchemaObject.Parent;
					}
					if (xmlSchemaObject is XmlSchema)
					{
						text2 = ((XmlSchema)xmlSchemaObject).TargetNamespace;
					}
				}
				text = Res.GetString("Schema with targetNamespace='{0}' has invalid syntax. {1} Line {2}, position {3}.", new object[] { text2, message, exception.LineNumber, exception.LinePosition });
			}
			return text;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B6E8 File Offset: 0x000198E8
		private static string GetSchemaItem(XmlSchemaObject o, string ns, string details)
		{
			if (o == null)
			{
				return null;
			}
			while (o.Parent != null && !(o.Parent is XmlSchema))
			{
				o = o.Parent;
			}
			if (ns == null || ns.Length == 0)
			{
				XmlSchemaObject xmlSchemaObject = o;
				while (xmlSchemaObject.Parent != null)
				{
					xmlSchemaObject = xmlSchemaObject.Parent;
				}
				if (xmlSchemaObject is XmlSchema)
				{
					ns = ((XmlSchema)xmlSchemaObject).TargetNamespace;
				}
			}
			string text;
			if (o is XmlSchemaNotation)
			{
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[]
				{
					ns,
					"notation",
					((XmlSchemaNotation)o).Name,
					details
				});
			}
			else if (o is XmlSchemaGroup)
			{
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[]
				{
					ns,
					"group",
					((XmlSchemaGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)o;
				if (xmlSchemaElement.Name == null || xmlSchemaElement.Name.Length == 0)
				{
					XmlQualifiedName parentName = SchemaCompiler.GetParentName(o);
					text = Res.GetString("Element reference '{0}' declared in schema type '{1}' from namespace '{2}'.", new object[]
					{
						xmlSchemaElement.RefName.ToString(),
						parentName.Name,
						parentName.Namespace
					});
				}
				else
				{
					text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[] { ns, "element", xmlSchemaElement.Name, details });
				}
			}
			else if (o is XmlSchemaType)
			{
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[]
				{
					ns,
					(o.GetType() == typeof(XmlSchemaSimpleType)) ? "simpleType" : "complexType",
					((XmlSchemaType)o).Name,
					details
				});
			}
			else if (o is XmlSchemaAttributeGroup)
			{
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[]
				{
					ns,
					"attributeGroup",
					((XmlSchemaAttributeGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaAttribute)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)o;
				if (xmlSchemaAttribute.Name == null || xmlSchemaAttribute.Name.Length == 0)
				{
					XmlQualifiedName parentName2 = SchemaCompiler.GetParentName(o);
					return Res.GetString("Attribute reference '{0}' declared in schema type '{1}' from namespace '{2}'.", new object[]
					{
						xmlSchemaAttribute.RefName.ToString(),
						parentName2.Name,
						parentName2.Namespace
					});
				}
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[] { ns, "attribute", xmlSchemaAttribute.Name, details });
			}
			else if (o is XmlSchemaContent)
			{
				XmlQualifiedName parentName3 = SchemaCompiler.GetParentName(o);
				text = Res.GetString("Check content definition of schema type '{0}' from namespace '{1}'. {2}", new object[] { parentName3.Name, parentName3.Namespace, details });
			}
			else if (o is XmlSchemaExternal)
			{
				string text2 = ((o is XmlSchemaImport) ? "import" : ((o is XmlSchemaInclude) ? "include" : ((o is XmlSchemaRedefine) ? "redefine" : o.GetType().Name)));
				text = Res.GetString("Schema item '{1}' from namespace '{0}'. {2}", new object[] { ns, text2, details });
			}
			else if (o is XmlSchema)
			{
				text = Res.GetString("Schema with targetNamespace='{0}' has invalid syntax. {1}", new object[] { ns, details });
			}
			else
			{
				text = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}", new object[]
				{
					ns,
					o.GetType().Name,
					null,
					details
				});
			}
			return text;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001BA70 File Offset: 0x00019C70
		internal static XmlQualifiedName GetParentName(XmlSchemaObject item)
		{
			while (item.Parent != null)
			{
				if (item.Parent is XmlSchemaType)
				{
					XmlSchemaType xmlSchemaType = (XmlSchemaType)item.Parent;
					if (xmlSchemaType.Name != null && xmlSchemaType.Name.Length != 0)
					{
						return xmlSchemaType.QualifiedName;
					}
				}
				item = item.Parent;
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001BAC9 File Offset: 0x00019CC9
		private static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
		{
			SchemaCompiler.Warnings.Add(Res.GetString((args.Severity == XmlSeverityType.Error) ? "Schema validation error: {0}" : "Schema validation warning: {0}", new object[] { SchemaCompiler.WarningDetails(args.Exception, args.Message) }));
		}

		// Token: 0x040003C8 RID: 968
		private static StringCollection warnings;
	}
}
