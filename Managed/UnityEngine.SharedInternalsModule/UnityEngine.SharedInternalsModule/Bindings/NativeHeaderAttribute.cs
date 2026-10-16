using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000013 RID: 19
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	internal class NativeHeaderAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021C1 File Offset: 0x000003C1
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000021C9 File Offset: 0x000003C9
		public string Header { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002078 File Offset: 0x00000278
		public NativeHeaderAttribute()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000021D4 File Offset: 0x000003D4
		public NativeHeaderAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.Header = header;
		}
	}
}
