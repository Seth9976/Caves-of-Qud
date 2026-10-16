using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000564 RID: 1380
	[UsedByNativeCode]
	public struct VisibleLight : IEquatable<VisibleLight>
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x00068F5C File Offset: 0x0006715C
		public Light light
		{
			get
			{
				return (Light)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x00068F70 File Offset: 0x00067170
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x00068F88 File Offset: 0x00067188
		public LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				this.m_LightType = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x00068F94 File Offset: 0x00067194
		// (set) Token: 0x0600358D RID: 13709 RVA: 0x00068FAC File Offset: 0x000671AC
		public Color finalColor
		{
			get
			{
				return this.m_FinalColor;
			}
			set
			{
				this.m_FinalColor = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x00068FB8 File Offset: 0x000671B8
		// (set) Token: 0x0600358F RID: 13711 RVA: 0x00068FD0 File Offset: 0x000671D0
		public Rect screenRect
		{
			get
			{
				return this.m_ScreenRect;
			}
			set
			{
				this.m_ScreenRect = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x00068FDC File Offset: 0x000671DC
		// (set) Token: 0x06003591 RID: 13713 RVA: 0x00068FF4 File Offset: 0x000671F4
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

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x00069000 File Offset: 0x00067200
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x00069018 File Offset: 0x00067218
		public float range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				this.m_Range = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x00069024 File Offset: 0x00067224
		// (set) Token: 0x06003595 RID: 13717 RVA: 0x0006903C File Offset: 0x0006723C
		public float spotAngle
		{
			get
			{
				return this.m_SpotAngle;
			}
			set
			{
				this.m_SpotAngle = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x00069048 File Offset: 0x00067248
		// (set) Token: 0x06003597 RID: 13719 RVA: 0x00069068 File Offset: 0x00067268
		public bool intersectsNearPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsNearPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsNearPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsNearPlane;
				}
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x0006909C File Offset: 0x0006729C
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x000690BC File Offset: 0x000672BC
		public bool intersectsFarPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsFarPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsFarPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsFarPlane;
				}
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000690F0 File Offset: 0x000672F0
		public bool forcedVisible
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.ForcedVisible) > (VisibleLightFlags)0;
			}
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x00069110 File Offset: 0x00067310
		public bool Equals(VisibleLight other)
		{
			return this.m_LightType == other.m_LightType && this.m_FinalColor.Equals(other.m_FinalColor) && this.m_ScreenRect.Equals(other.m_ScreenRect) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_Range.Equals(other.m_Range) && this.m_SpotAngle.Equals(other.m_SpotAngle) && this.m_InstanceId == other.m_InstanceId && this.m_Flags == other.m_Flags;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000691B0 File Offset: 0x000673B0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleLight && this.Equals((VisibleLight)obj);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000691E8 File Offset: 0x000673E8
		public override int GetHashCode()
		{
			int num = (int)this.m_LightType;
			num = (num * 397) ^ this.m_FinalColor.GetHashCode();
			num = (num * 397) ^ this.m_ScreenRect.GetHashCode();
			num = (num * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			num = (num * 397) ^ this.m_Range.GetHashCode();
			num = (num * 397) ^ this.m_SpotAngle.GetHashCode();
			num = (num * 397) ^ this.m_InstanceId;
			return (num * 397) ^ (int)this.m_Flags;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00069298 File Offset: 0x00067498
		public static bool operator ==(VisibleLight left, VisibleLight right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000692B4 File Offset: 0x000674B4
		public static bool operator !=(VisibleLight left, VisibleLight right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001210 RID: 4624
		private LightType m_LightType;

		// Token: 0x04001211 RID: 4625
		private Color m_FinalColor;

		// Token: 0x04001212 RID: 4626
		private Rect m_ScreenRect;

		// Token: 0x04001213 RID: 4627
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04001214 RID: 4628
		private float m_Range;

		// Token: 0x04001215 RID: 4629
		private float m_SpotAngle;

		// Token: 0x04001216 RID: 4630
		private int m_InstanceId;

		// Token: 0x04001217 RID: 4631
		private VisibleLightFlags m_Flags;
	}
}
