using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200044A RID: 1098
	public class Notification : INotification
	{
		// Token: 0x06002A3E RID: 10814 RVA: 0x00051505 File Offset: 0x0004F705
		public Notification(string name)
		{
			this.id = new PropertyName(name);
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x0005151B File Offset: 0x0004F71B
		public PropertyName id { get; }
	}
}
