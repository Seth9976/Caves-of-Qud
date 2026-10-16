using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000CE RID: 206
	internal class nsDOMProcessingInstruction
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x000054C3 File Offset: 0x000036C3
		public static nsIDOMProcessingInstruction GetProxy(IWebBrowser control, nsIDOMProcessingInstruction obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMProcessingInstruction).GUID, obj) as nsIDOMProcessingInstruction;
		}
	}
}
