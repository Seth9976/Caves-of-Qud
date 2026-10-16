using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000585 RID: 1413
	internal interface IUxmlSerializedDataCustomAttributeHandler
	{
		// Token: 0x06002A6C RID: 10860
		void SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes);
	}
}
