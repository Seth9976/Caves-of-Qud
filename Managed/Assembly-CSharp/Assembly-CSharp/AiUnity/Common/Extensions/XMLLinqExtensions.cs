using System;
using System.Xml.Linq;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002E2 RID: 738
	public static class XMLLinqExtensions
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x000813E4 File Offset: 0x0007F5E4
		public static void MoveElementUp(this XElement element)
		{
			XNode xnode = element.PreviousNode;
			while (xnode != null && !(xnode is XElement))
			{
				xnode = xnode.PreviousNode;
			}
			if (xnode == null)
			{
				throw new ArgumentException("Nowhere to move element to!");
			}
			element.Remove();
			xnode.AddBeforeSelf(element);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00081428 File Offset: 0x0007F628
		public static void MoveElementDown(this XElement element)
		{
			XNode xnode = element.NextNode;
			while (xnode != null && !(xnode is XElement))
			{
				xnode = xnode.NextNode;
			}
			if (xnode == null)
			{
				throw new ArgumentException("Nowhere to move element to!");
			}
			element.Remove();
			xnode.AddAfterSelf(element);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0008146C File Offset: 0x0007F66C
		public static XAttribute GetOrSetAttribute(this XElement element, string attributeName, string defaultValue)
		{
			XAttribute xattribute = element.Attribute(attributeName);
			if (xattribute == null)
			{
				xattribute = new XAttribute(attributeName, defaultValue);
				element.Add(xattribute);
			}
			return xattribute;
		}
	}
}
