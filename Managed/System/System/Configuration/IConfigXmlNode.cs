using System;

namespace System.Configuration
{
	// Token: 0x020001B3 RID: 435
	internal interface IConfigXmlNode
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B83 RID: 2947
		string Filename { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B84 RID: 2948
		int LineNumber { get; }
	}
}
