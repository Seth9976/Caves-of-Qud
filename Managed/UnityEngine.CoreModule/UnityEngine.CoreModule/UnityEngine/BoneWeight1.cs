using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EF RID: 495
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight1 : IEquatable<BoneWeight1>
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00028810 File Offset: 0x00026A10
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x00028828 File Offset: 0x00026A28
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00028834 File Offset: 0x00026A34
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x0002884C File Offset: 0x00026A4C
		public int boneIndex
		{
			get
			{
				return this.m_BoneIndex;
			}
			set
			{
				this.m_BoneIndex = value;
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00028858 File Offset: 0x00026A58
		public override bool Equals(object other)
		{
			return other is BoneWeight1 && this.Equals((BoneWeight1)other);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00028884 File Offset: 0x00026A84
		public bool Equals(BoneWeight1 other)
		{
			return this.boneIndex.Equals(other.boneIndex) && this.weight.Equals(other.weight);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000288C8 File Offset: 0x00026AC8
		public override int GetHashCode()
		{
			return this.boneIndex.GetHashCode() ^ this.weight.GetHashCode();
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000288F8 File Offset: 0x00026AF8
		public static bool operator ==(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return lhs.boneIndex == rhs.boneIndex && lhs.weight == rhs.weight;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00028930 File Offset: 0x00026B30
		public static bool operator !=(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040006D5 RID: 1749
		[SerializeField]
		private float m_Weight;

		// Token: 0x040006D6 RID: 1750
		[SerializeField]
		private int m_BoneIndex;
	}
}
