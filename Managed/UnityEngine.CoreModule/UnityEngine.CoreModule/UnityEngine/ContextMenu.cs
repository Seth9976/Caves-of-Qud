using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200025E RID: 606
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x0003C487 File Offset: 0x0003A687
		public ContextMenu(string itemName)
			: this(itemName, false)
		{
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0003C493 File Offset: 0x0003A693
		public ContextMenu(string itemName, bool isValidateFunction)
			: this(itemName, isValidateFunction, 1000000)
		{
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0003C4A4 File Offset: 0x0003A6A4
		public ContextMenu(string itemName, bool isValidateFunction, int priority)
		{
			this.menuItem = itemName;
			this.validate = isValidateFunction;
			this.priority = priority;
		}

		// Token: 0x0400093F RID: 2367
		public readonly string menuItem;

		// Token: 0x04000940 RID: 2368
		public readonly bool validate;

		// Token: 0x04000941 RID: 2369
		public readonly int priority;
	}
}
