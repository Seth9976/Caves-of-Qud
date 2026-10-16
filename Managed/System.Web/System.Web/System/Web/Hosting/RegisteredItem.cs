using System;

namespace System.Web.Hosting
{
	// Token: 0x02000546 RID: 1350
	internal class RegisteredItem
	{
		// Token: 0x06003A72 RID: 14962 RVA: 0x0009BA3E File Offset: 0x00099C3E
		public RegisteredItem(IRegisteredObject item, bool autoclean)
		{
			this.Item = item;
			this.AutoClean = autoclean;
		}

		// Token: 0x04001FD2 RID: 8146
		public IRegisteredObject Item;

		// Token: 0x04001FD3 RID: 8147
		public bool AutoClean;
	}
}
