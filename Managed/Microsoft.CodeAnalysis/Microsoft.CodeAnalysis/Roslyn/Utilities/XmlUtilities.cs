using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Roslyn.Utilities
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(0)]
	internal static class XmlUtilities
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00009414 File Offset: 0x00007614
		internal static TNode Copy<[Nullable(0)] TNode>(this TNode node, bool copyAttributeAnnotations) where TNode : XNode
		{
			XNode xnode;
			if (node.NodeType == 9)
			{
				xnode = new XDocument((XDocument)((object)node));
			}
			else
			{
				XElement xelement = new XElement("temp");
				xelement.Add(node);
				xnode = xelement.LastNode;
				xelement.RemoveNodes();
			}
			XmlUtilities.CopyAnnotations(node, xnode);
			if (copyAttributeAnnotations && node.NodeType == 1)
			{
				XElement xelement2 = (XElement)((object)node);
				XElement xelement3 = (XElement)xnode;
				IEnumerator<XAttribute> enumerator = xelement2.Attributes().GetEnumerator();
				IEnumerator<XAttribute> enumerator2 = xelement3.Attributes().GetEnumerator();
				while (enumerator.MoveNext() && enumerator2.MoveNext())
				{
					XmlUtilities.CopyAnnotations(enumerator.Current, enumerator2.Current);
				}
			}
			return (TNode)((object)xnode);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000094DC File Offset: 0x000076DC
		private static void CopyAnnotations(XObject source, XObject target)
		{
			foreach (object obj in source.Annotations<object>())
			{
				target.AddAnnotation(obj);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000952C File Offset: 0x0000772C
		[return: Nullable(new byte[] { 2, 1 })]
		internal static XElement[] TrySelectElements(XNode node, string xpath, [Nullable(2)] out string errorMessage, out bool invalidXPath)
		{
			errorMessage = null;
			invalidXPath = false;
			XElement[] array;
			try
			{
				IEnumerable<XElement> enumerable = Extensions.XPathSelectElements(node, xpath);
				array = ((enumerable != null) ? Enumerable.ToArray<XElement>(enumerable) : null);
			}
			catch (InvalidOperationException ex)
			{
				errorMessage = ex.Message;
				array = null;
			}
			catch (Exception ex2) when (ex2.GetType().FullName == "System.Xml.XPath.XPathException")
			{
				errorMessage = ex2.Message;
				invalidXPath = true;
				array = null;
			}
			return array;
		}
	}
}
