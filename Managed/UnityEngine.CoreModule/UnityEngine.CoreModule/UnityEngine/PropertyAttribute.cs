using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200022E RID: 558
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0003A7BC File Offset: 0x000389BC
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0003A7C4 File Offset: 0x000389C4
		public int order { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0003A7CD File Offset: 0x000389CD
		public bool applyToCollection { get; }

		// Token: 0x06001E5B RID: 7771 RVA: 0x0003A7D5 File Offset: 0x000389D5
		protected PropertyAttribute()
			: this(false)
		{
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0003A7E0 File Offset: 0x000389E0
		protected PropertyAttribute(bool applyToCollection)
		{
			this.applyToCollection = applyToCollection;
		}
	}
}
