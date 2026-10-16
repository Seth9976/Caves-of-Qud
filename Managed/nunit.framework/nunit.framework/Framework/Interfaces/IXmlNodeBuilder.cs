using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000DC RID: 220
	public interface IXmlNodeBuilder
	{
		// Token: 0x06000704 RID: 1796
		TNode ToXml(bool recursive);

		// Token: 0x06000705 RID: 1797
		TNode AddToXml(TNode parentNode, bool recursive);
	}
}
