using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000548 RID: 1352
	public abstract class TypedUxmlAttributeDescription<T> : UxmlAttributeDescription
	{
		// Token: 0x06002960 RID: 10592
		public abstract T GetValueFromBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x00095FD7 File Offset: 0x000941D7
		// (set) Token: 0x06002962 RID: 10594 RVA: 0x00095FDF File Offset: 0x000941DF
		public T defaultValue { get; set; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x00095FE8 File Offset: 0x000941E8
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = this.defaultValue;
				return defaultValue.ToString();
			}
		}
	}
}
