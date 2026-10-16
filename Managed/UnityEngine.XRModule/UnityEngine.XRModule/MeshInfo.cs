using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000032 RID: 50
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshInfo : IEquatable<MeshInfo>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005CF3 File Offset: 0x00003EF3
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005CFB File Offset: 0x00003EFB
		public MeshId MeshId { readonly get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005D04 File Offset: 0x00003F04
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005D0C File Offset: 0x00003F0C
		public MeshChangeState ChangeState { readonly get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005D15 File Offset: 0x00003F15
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005D1D File Offset: 0x00003F1D
		public int PriorityHint { readonly get; set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x00005D28 File Offset: 0x00003F28
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshInfo);
			return !flag && this.Equals((MeshInfo)obj);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005D5C File Offset: 0x00003F5C
		public bool Equals(MeshInfo other)
		{
			return this.MeshId.Equals(other.MeshId) && this.ChangeState.Equals(other.ChangeState) && this.PriorityHint.Equals(other.PriorityHint);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public static bool operator ==(MeshInfo lhs, MeshInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005DDC File Offset: 0x00003FDC
		public static bool operator !=(MeshInfo lhs, MeshInfo rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005DFC File Offset: 0x00003FFC
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), ((int)this.ChangeState).GetHashCode(), this.PriorityHint.GetHashCode());
		}
	}
}
