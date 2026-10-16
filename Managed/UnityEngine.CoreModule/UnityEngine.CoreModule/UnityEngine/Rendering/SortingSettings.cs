using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200055E RID: 1374
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x00068188 File Offset: 0x00066388
		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000681A0 File Offset: 0x000663A0
		// (set) Token: 0x060034FF RID: 13567 RVA: 0x000681B8 File Offset: 0x000663B8
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				return this.m_WorldToCameraMatrix;
			}
			set
			{
				this.m_WorldToCameraMatrix = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000681C4 File Offset: 0x000663C4
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x000681DC File Offset: 0x000663DC
		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
			set
			{
				this.m_CameraPosition = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000681E8 File Offset: 0x000663E8
		// (set) Token: 0x06003503 RID: 13571 RVA: 0x00068200 File Offset: 0x00066400
		public Vector3 customAxis
		{
			get
			{
				return this.m_CustomAxis;
			}
			set
			{
				this.m_CustomAxis = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x0006820C File Offset: 0x0006640C
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x00068224 File Offset: 0x00066424
		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x00068230 File Offset: 0x00066430
		// (set) Token: 0x06003507 RID: 13575 RVA: 0x00068248 File Offset: 0x00066448
		public DistanceMetric distanceMetric
		{
			get
			{
				return this.m_DistanceMetric;
			}
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x00068254 File Offset: 0x00066454
		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000682C0 File Offset: 0x000664C0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000682F8 File Offset: 0x000664F8
		public override int GetHashCode()
		{
			int num = this.m_WorldToCameraMatrix.GetHashCode();
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_CustomAxis.GetHashCode();
			num = (num * 397) ^ (int)this.m_Criteria;
			return (num * 397) ^ (int)this.m_DistanceMetric;
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x00068370 File Offset: 0x00066570
		public static bool operator ==(SortingSettings left, SortingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x0006838C File Offset: 0x0006658C
		public static bool operator !=(SortingSettings left, SortingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040011D2 RID: 4562
		private Matrix4x4 m_WorldToCameraMatrix;

		// Token: 0x040011D3 RID: 4563
		private Vector3 m_CameraPosition;

		// Token: 0x040011D4 RID: 4564
		private Vector3 m_CustomAxis;

		// Token: 0x040011D5 RID: 4565
		private SortingCriteria m_Criteria;

		// Token: 0x040011D6 RID: 4566
		private DistanceMetric m_DistanceMetric;
	}
}
