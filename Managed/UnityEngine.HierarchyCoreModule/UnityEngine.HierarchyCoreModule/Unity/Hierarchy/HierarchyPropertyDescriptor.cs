using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyDescriptor.h")]
	public struct HierarchyPropertyDescriptor
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000058CB File Offset: 0x00003ACB
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000058D3 File Offset: 0x00003AD3
		public int Size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000058DC File Offset: 0x00003ADC
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000058E4 File Offset: 0x00003AE4
		public HierarchyPropertyStorageType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x0400006D RID: 109
		private int m_Size;

		// Token: 0x0400006E RID: 110
		private HierarchyPropertyStorageType m_Type;
	}
}
