using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200053F RID: 1343
	public struct LODParameters : IEquatable<LODParameters>
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000648DC File Offset: 0x00062ADC
		// (set) Token: 0x06003369 RID: 13161 RVA: 0x000648F9 File Offset: 0x00062AF9
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x00064908 File Offset: 0x00062B08
		// (set) Token: 0x0600336B RID: 13163 RVA: 0x00064920 File Offset: 0x00062B20
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

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x0006492C File Offset: 0x00062B2C
		// (set) Token: 0x0600336D RID: 13165 RVA: 0x00064944 File Offset: 0x00062B44
		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
			set
			{
				this.m_FieldOfView = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x00064950 File Offset: 0x00062B50
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x00064968 File Offset: 0x00062B68
		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
			set
			{
				this.m_OrthoSize = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00064974 File Offset: 0x00062B74
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x0006498C File Offset: 0x00062B8C
		public int cameraPixelHeight
		{
			get
			{
				return this.m_CameraPixelHeight;
			}
			set
			{
				this.m_CameraPixelHeight = value;
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x00064998 File Offset: 0x00062B98
		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x00064A04 File Offset: 0x00062C04
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x00064A3C File Offset: 0x00062C3C
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_FieldOfView.GetHashCode();
			num = (num * 397) ^ this.m_OrthoSize.GetHashCode();
			return (num * 397) ^ this.m_CameraPixelHeight;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x00064AA8 File Offset: 0x00062CA8
		public static bool operator ==(LODParameters left, LODParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x00064AC4 File Offset: 0x00062CC4
		public static bool operator !=(LODParameters left, LODParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001137 RID: 4407
		private int m_IsOrthographic;

		// Token: 0x04001138 RID: 4408
		private Vector3 m_CameraPosition;

		// Token: 0x04001139 RID: 4409
		private float m_FieldOfView;

		// Token: 0x0400113A RID: 4410
		private float m_OrthoSize;

		// Token: 0x0400113B RID: 4411
		private int m_CameraPixelHeight;
	}
}
