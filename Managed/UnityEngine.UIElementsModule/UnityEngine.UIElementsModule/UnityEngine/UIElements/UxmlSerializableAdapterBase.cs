using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000589 RID: 1417
	internal abstract class UxmlSerializableAdapterBase
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002A75 RID: 10869
		// (set) Token: 0x06002A76 RID: 10870
		public abstract object dataBoxed { get; set; }

		// Token: 0x06002A77 RID: 10871
		public abstract object CloneInstanceBoxed(object value);
	}
}
