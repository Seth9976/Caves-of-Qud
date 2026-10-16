using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EE RID: 494
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000284B8 File Offset: 0x000266B8
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x000284D0 File Offset: 0x000266D0
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x000284DC File Offset: 0x000266DC
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x000284F4 File Offset: 0x000266F4
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00028500 File Offset: 0x00026700
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00028518 File Offset: 0x00026718
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00028524 File Offset: 0x00026724
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0002853C File Offset: 0x0002673C
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00028548 File Offset: 0x00026748
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x00028560 File Offset: 0x00026760
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0002856C File Offset: 0x0002676C
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00028584 File Offset: 0x00026784
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00028590 File Offset: 0x00026790
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x000285A8 File Offset: 0x000267A8
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000285B4 File Offset: 0x000267B4
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x000285CC File Offset: 0x000267CC
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000285D8 File Offset: 0x000267D8
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00028670 File Offset: 0x00026870
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0002869C File Offset: 0x0002689C
		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00028758 File Offset: 0x00026958
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000287F4 File Offset: 0x000269F4
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040006CD RID: 1741
		[SerializeField]
		private float m_Weight0;

		// Token: 0x040006CE RID: 1742
		[SerializeField]
		private float m_Weight1;

		// Token: 0x040006CF RID: 1743
		[SerializeField]
		private float m_Weight2;

		// Token: 0x040006D0 RID: 1744
		[SerializeField]
		private float m_Weight3;

		// Token: 0x040006D1 RID: 1745
		[SerializeField]
		private int m_BoneIndex0;

		// Token: 0x040006D2 RID: 1746
		[SerializeField]
		private int m_BoneIndex1;

		// Token: 0x040006D3 RID: 1747
		[SerializeField]
		private int m_BoneIndex2;

		// Token: 0x040006D4 RID: 1748
		[SerializeField]
		private int m_BoneIndex3;
	}
}
