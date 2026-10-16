using System;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000289 RID: 649
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ArrayParameterAttribute : Attribute
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x0007A0B3 File Offset: 0x000782B3
		public ArrayParameterAttribute(Type itemType, string elementName)
		{
			this.ItemType = itemType;
			this.ElementName = elementName;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0007A0C9 File Offset: 0x000782C9
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x0007A0D1 File Offset: 0x000782D1
		public Type ItemType { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x0007A0DA File Offset: 0x000782DA
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x0007A0E2 File Offset: 0x000782E2
		public string ElementName { get; private set; }
	}
}
