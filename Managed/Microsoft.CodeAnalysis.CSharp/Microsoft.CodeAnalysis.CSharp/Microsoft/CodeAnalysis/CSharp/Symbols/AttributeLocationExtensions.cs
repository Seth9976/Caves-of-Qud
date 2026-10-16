using System;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000517 RID: 1303
	internal static class AttributeLocationExtensions
	{
		// Token: 0x06006D8C RID: 28044 RVA: 0x00192C3C File Offset: 0x00190E3C
		internal static string ToDisplayString(this AttributeLocation locations)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < 1024; i <<= 1)
			{
				if ((locations & (AttributeLocation)i) != AttributeLocation.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					AttributeLocation attributeLocation = (AttributeLocation)i;
					if (attributeLocation <= AttributeLocation.Property)
					{
						if (attributeLocation <= AttributeLocation.Method)
						{
							switch (attributeLocation)
							{
							case AttributeLocation.Assembly:
								stringBuilder.Append("assembly");
								goto IL_013A;
							case AttributeLocation.Module:
								stringBuilder.Append("module");
								goto IL_013A;
							case AttributeLocation.Assembly | AttributeLocation.Module:
								break;
							case AttributeLocation.Type:
								stringBuilder.Append("type");
								goto IL_013A;
							default:
								if (attributeLocation == AttributeLocation.Method)
								{
									stringBuilder.Append("method");
									goto IL_013A;
								}
								break;
							}
						}
						else
						{
							if (attributeLocation == AttributeLocation.Field)
							{
								stringBuilder.Append("field");
								goto IL_013A;
							}
							if (attributeLocation == AttributeLocation.Property)
							{
								stringBuilder.Append("property");
								goto IL_013A;
							}
						}
					}
					else if (attributeLocation <= AttributeLocation.Parameter)
					{
						if (attributeLocation == AttributeLocation.Event)
						{
							stringBuilder.Append("event");
							goto IL_013A;
						}
						if (attributeLocation == AttributeLocation.Parameter)
						{
							stringBuilder.Append("param");
							goto IL_013A;
						}
					}
					else
					{
						if (attributeLocation == AttributeLocation.Return)
						{
							stringBuilder.Append("return");
							goto IL_013A;
						}
						if (attributeLocation == AttributeLocation.TypeParameter)
						{
							stringBuilder.Append("typevar");
							goto IL_013A;
						}
					}
					throw ExceptionUtilities.UnexpectedValue(i);
				}
				IL_013A:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x00192D98 File Offset: 0x00190F98
		internal static AttributeLocation ToAttributeLocation(this SyntaxToken token)
		{
			return AttributeLocationExtensions.ToAttributeLocation(token.ValueText);
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x00192DA6 File Offset: 0x00190FA6
		internal static AttributeLocation ToAttributeLocation(this SyntaxToken token)
		{
			return AttributeLocationExtensions.ToAttributeLocation(token.ValueText);
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x00192DB4 File Offset: 0x00190FB4
		private static AttributeLocation ToAttributeLocation(string text)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2246981567U)
			{
				if (num <= 1309554226U)
				{
					if (num != 601537893U)
					{
						if (num == 1309554226U)
						{
							if (text == "param")
							{
								return AttributeLocation.Parameter;
							}
						}
					}
					else if (text == "assembly")
					{
						return AttributeLocation.Assembly;
					}
				}
				else if (num != 1361572173U)
				{
					if (num != 1736598119U)
					{
						if (num == 2246981567U)
						{
							if (text == "return")
							{
								return AttributeLocation.Return;
							}
						}
					}
					else if (text == "field")
					{
						return AttributeLocation.Field;
					}
				}
				else if (text == "type")
				{
					return AttributeLocation.Type;
				}
			}
			else if (num <= 3295821558U)
			{
				if (num != 2873489200U)
				{
					if (num == 3295821558U)
					{
						if (text == "typevar")
						{
							return AttributeLocation.TypeParameter;
						}
					}
				}
				else if (text == "method")
				{
					return AttributeLocation.Method;
				}
			}
			else if (num != 3602055880U)
			{
				if (num != 3617558685U)
				{
					if (num == 4264611999U)
					{
						if (text == "event")
						{
							return AttributeLocation.Event;
						}
					}
				}
				else if (text == "module")
				{
					return AttributeLocation.Module;
				}
			}
			else if (text == "property")
			{
				return AttributeLocation.Property;
			}
			return AttributeLocation.None;
		}
	}
}
