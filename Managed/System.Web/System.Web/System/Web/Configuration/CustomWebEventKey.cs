using System;

namespace System.Web.Configuration
{
	// Token: 0x0200055E RID: 1374
	internal class CustomWebEventKey
	{
		// Token: 0x06003B0C RID: 15116 RVA: 0x0009C6B1 File Offset: 0x0009A8B1
		internal CustomWebEventKey(Type eventType, int eventCode)
		{
			this._type = eventType;
			this._eventCode = eventCode;
		}

		// Token: 0x0400200B RID: 8203
		internal Type _type;

		// Token: 0x0400200C RID: 8204
		internal int _eventCode;
	}
}
