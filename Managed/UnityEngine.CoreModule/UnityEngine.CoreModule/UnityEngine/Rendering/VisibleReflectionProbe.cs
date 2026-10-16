using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000566 RID: 1382
	[UsedByNativeCode]
	public struct VisibleReflectionProbe : IEquatable<VisibleReflectionProbe>
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000692D1 File Offset: 0x000674D1
		public Texture texture
		{
			get
			{
				return (Texture)Object.FindObjectFromInstanceID(this.m_TextureId);
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000692E3 File Offset: 0x000674E3
		public ReflectionProbe reflectionProbe
		{
			get
			{
				return (ReflectionProbe)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000692F8 File Offset: 0x000674F8
		// (set) Token: 0x060035A3 RID: 13731 RVA: 0x00069310 File Offset: 0x00067510
		public Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
			set
			{
				this.m_Bounds = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x0006931C File Offset: 0x0006751C
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x00069334 File Offset: 0x00067534
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
			set
			{
				this.m_LocalToWorldMatrix = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x00069340 File Offset: 0x00067540
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x00069358 File Offset: 0x00067558
		public Vector4 hdrData
		{
			get
			{
				return this.m_HdrData;
			}
			set
			{
				this.m_HdrData = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x00069364 File Offset: 0x00067564
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x0006937C File Offset: 0x0006757C
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x00069388 File Offset: 0x00067588
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x000693A0 File Offset: 0x000675A0
		public float blendDistance
		{
			get
			{
				return this.m_BlendDistance;
			}
			set
			{
				this.m_BlendDistance = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000693AC File Offset: 0x000675AC
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000693C4 File Offset: 0x000675C4
		public int importance
		{
			get
			{
				return this.m_Importance;
			}
			set
			{
				this.m_Importance = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000693D0 File Offset: 0x000675D0
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x000693ED File Offset: 0x000675ED
		public bool isBoxProjection
		{
			get
			{
				return Convert.ToBoolean(this.m_BoxProjection);
			}
			set
			{
				this.m_BoxProjection = Convert.ToInt32(value);
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000693FC File Offset: 0x000675FC
		public bool Equals(VisibleReflectionProbe other)
		{
			return this.m_Bounds.Equals(other.m_Bounds) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_HdrData.Equals(other.m_HdrData) && this.m_Center.Equals(other.m_Center) && this.m_BlendDistance.Equals(other.m_BlendDistance) && this.m_Importance == other.m_Importance && this.m_BoxProjection == other.m_BoxProjection && this.m_InstanceId == other.m_InstanceId && this.m_TextureId == other.m_TextureId;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000694AC File Offset: 0x000676AC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleReflectionProbe && this.Equals((VisibleReflectionProbe)obj);
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000694E4 File Offset: 0x000676E4
		public override int GetHashCode()
		{
			int num = this.m_Bounds.GetHashCode();
			num = (num * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			num = (num * 397) ^ this.m_HdrData.GetHashCode();
			num = (num * 397) ^ this.m_Center.GetHashCode();
			num = (num * 397) ^ this.m_BlendDistance.GetHashCode();
			num = (num * 397) ^ this.m_Importance;
			num = (num * 397) ^ this.m_BoxProjection;
			num = (num * 397) ^ this.m_InstanceId;
			return (num * 397) ^ this.m_TextureId;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000695A8 File Offset: 0x000677A8
		public static bool operator ==(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return left.Equals(right);
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000695C4 File Offset: 0x000677C4
		public static bool operator !=(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400121C RID: 4636
		private Bounds m_Bounds;

		// Token: 0x0400121D RID: 4637
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x0400121E RID: 4638
		private Vector4 m_HdrData;

		// Token: 0x0400121F RID: 4639
		private Vector3 m_Center;

		// Token: 0x04001220 RID: 4640
		private float m_BlendDistance;

		// Token: 0x04001221 RID: 4641
		private int m_Importance;

		// Token: 0x04001222 RID: 4642
		private int m_BoxProjection;

		// Token: 0x04001223 RID: 4643
		private int m_InstanceId;

		// Token: 0x04001224 RID: 4644
		private int m_TextureId;
	}
}
