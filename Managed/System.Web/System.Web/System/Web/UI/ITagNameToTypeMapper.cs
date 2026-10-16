using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020001DB RID: 475
	internal interface ITagNameToTypeMapper
	{
		// Token: 0x06001350 RID: 4944
		Type GetControlType(string tagName, IDictionary attribs);
	}
}
