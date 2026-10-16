using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000578 RID: 1400
	public struct LinearColor
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x0006AFFC File Offset: 0x000691FC
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x0006B014 File Offset: 0x00069214
		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x0006B05C File Offset: 0x0006925C
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x0006B074 File Offset: 0x00069274
		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x0006B0BC File Offset: 0x000692BC
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x0006B0D4 File Offset: 0x000692D4
		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x0006B11C File Offset: 0x0006931C
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x0006B134 File Offset: 0x00069334
		public float intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Intensity (" + value.ToString() + ") must be positive.");
				}
				this.m_intensity = value;
			}
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x0006B174 File Offset: 0x00069374
		public static LinearColor Convert(Color color, float intensity)
		{
			Color color2 = (GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear);
			float maxColorComponent = color2.maxColorComponent;
			bool flag = color2.r < 0f || color2.g < 0f || color2.b < 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new string[]
				{
					"The input color to be converted must not contain negative values (red: ",
					color2.r.ToString(),
					", green: ",
					color2.g.ToString(),
					", blue: ",
					color2.b.ToString(),
					")."
				}));
			}
			bool flag2 = maxColorComponent <= 1E-20f;
			LinearColor linearColor;
			if (flag2)
			{
				linearColor = LinearColor.Black();
			}
			else
			{
				float num = 1f / color2.maxColorComponent;
				LinearColor linearColor2;
				linearColor2.m_red = color2.r * num;
				linearColor2.m_green = color2.g * num;
				linearColor2.m_blue = color2.b * num;
				linearColor2.m_intensity = maxColorComponent;
				linearColor = linearColor2;
			}
			return linearColor;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x0006B2A8 File Offset: 0x000694A8
		public static LinearColor Black()
		{
			LinearColor linearColor;
			linearColor.m_red = (linearColor.m_green = (linearColor.m_blue = (linearColor.m_intensity = 0f)));
			return linearColor;
		}

		// Token: 0x04001278 RID: 4728
		private float m_red;

		// Token: 0x04001279 RID: 4729
		private float m_green;

		// Token: 0x0400127A RID: 4730
		private float m_blue;

		// Token: 0x0400127B RID: 4731
		private float m_intensity;
	}
}
