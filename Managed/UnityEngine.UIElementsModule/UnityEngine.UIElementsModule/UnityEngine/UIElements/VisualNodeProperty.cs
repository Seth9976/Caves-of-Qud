using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000601 RID: 1537
	internal struct VisualNodeProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x000A2505 File Offset: 0x000A0705
		internal unsafe VisualNodeProperty(VisualNodePropertyData* data)
		{
			this.m_Data = data;
		}

		// Token: 0x17000B33 RID: 2867
		public unsafe ref T this[VisualNodeHandle handle]
		{
			get
			{
				Debug.Assert(handle.Id > 0);
				return ref *(T*)((byte*)this.m_Data->Ptr + (IntPtr)(handle.Id - 1) * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x040014AB RID: 5291
		private unsafe readonly VisualNodePropertyData* m_Data;
	}
}
