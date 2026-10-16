using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000559 RID: 1369
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x00067CD4 File Offset: 0x00065ED4
		// (set) Token: 0x060034E1 RID: 13537 RVA: 0x00067CEC File Offset: 0x00065EEC
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to ShadowSplitData.maximumCullingPlaneCount ({1}), but was {2}.", 0, 10, value));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x00067D34 File Offset: 0x00065F34
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x00067D4C File Offset: 0x00065F4C
		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
			set
			{
				this.m_CullingSphere = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x00067D58 File Offset: 0x00065F58
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x00067D70 File Offset: 0x00065F70
		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x00067D7C File Offset: 0x00065F7C
		// (set) Token: 0x060034E7 RID: 13543 RVA: 0x00067D94 File Offset: 0x00065F94
		public float cullingNearPlane
		{
			get
			{
				return this.m_CullingNearPlane;
			}
			set
			{
				this.m_CullingNearPlane = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x00067DA0 File Offset: 0x00065FA0
		// (set) Token: 0x060034E9 RID: 13545 RVA: 0x00067DB8 File Offset: 0x00065FB8
		public float shadowCascadeBlendCullingFactor
		{
			get
			{
				return this.m_ShadowCascadeBlendCullingFactor;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00067E08 File Offset: 0x00066008
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x00067E84 File Offset: 0x00066084
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x00067EFC File Offset: 0x000660FC
		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag3 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag3)
					{
						return false;
					}
				}
				flag2 = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return flag2;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x00067F84 File Offset: 0x00066184
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x00067FBC File Offset: 0x000661BC
		public override int GetHashCode()
		{
			return (this.m_CullingPlaneCount * 397) ^ this.m_CullingSphere.GetHashCode();
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x00067FF0 File Offset: 0x000661F0
		public static bool operator ==(ShadowSplitData left, ShadowSplitData right)
		{
			return left.Equals(right);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x0006800C File Offset: 0x0006620C
		public static bool operator !=(ShadowSplitData left, ShadowSplitData right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040011B8 RID: 4536
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x040011B9 RID: 4537
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x040011BA RID: 4538
		private int m_CullingPlaneCount;

		// Token: 0x040011BB RID: 4539
		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x040011BC RID: 4540
		private Vector4 m_CullingSphere;

		// Token: 0x040011BD RID: 4541
		private float m_ShadowCascadeBlendCullingFactor;

		// Token: 0x040011BE RID: 4542
		private float m_CullingNearPlane;

		// Token: 0x040011BF RID: 4543
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x0200055A RID: 1370
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x040011C0 RID: 4544
			public byte FixedElementField;
		}
	}
}
