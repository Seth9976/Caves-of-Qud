using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000480 RID: 1152
	public struct TransformOrigin : IEquatable<TransformOrigin>
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x000847C0 File Offset: 0x000829C0
		public TransformOrigin(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000847D8 File Offset: 0x000829D8
		public TransformOrigin(Length x, Length y)
		{
			this = new TransformOrigin(x, y, 0f);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000847E9 File Offset: 0x000829E9
		internal TransformOrigin(Vector3 vector)
		{
			this = new TransformOrigin(vector.x, vector.y, vector.z);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00084810 File Offset: 0x00082A10
		public static TransformOrigin Initial()
		{
			return new TransformOrigin(Length.Percent(50f), Length.Percent(50f), 0f);
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x00084840 File Offset: 0x00082A40
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x00084848 File Offset: 0x00082A48
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x00084851 File Offset: 0x00082A51
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x00084859 File Offset: 0x00082A59
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x00084862 File Offset: 0x00082A62
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x0008486A File Offset: 0x00082A6A
		public float z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00084874 File Offset: 0x00082A74
		public static bool operator ==(TransformOrigin lhs, TransformOrigin rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000848C0 File Offset: 0x00082AC0
		public static bool operator !=(TransformOrigin lhs, TransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000848DC File Offset: 0x00082ADC
		public bool Equals(TransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000848FC File Offset: 0x00082AFC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is TransformOrigin)
			{
				TransformOrigin transformOrigin = (TransformOrigin)obj;
				flag = this.Equals(transformOrigin);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00084928 File Offset: 0x00082B28
		public override int GetHashCode()
		{
			return (this.m_X.GetHashCode() * 793) ^ (this.m_Y.GetHashCode() * 791) ^ (this.m_Z.GetHashCode() * 571);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x0008497C File Offset: 0x00082B7C
		public override string ToString()
		{
			string text = this.m_Z.ToString(CultureInfo.InvariantCulture.NumberFormat);
			return string.Concat(new string[]
			{
				this.m_X.ToString(),
				" ",
				this.m_Y.ToString(),
				" ",
				text
			});
		}

		// Token: 0x04000FEA RID: 4074
		private Length m_X;

		// Token: 0x04000FEB RID: 4075
		private Length m_Y;

		// Token: 0x04000FEC RID: 4076
		private float m_Z;

		// Token: 0x02000481 RID: 1153
		internal class PropertyBag : ContainerPropertyBag<TransformOrigin>
		{
			// Token: 0x0600243C RID: 9276 RVA: 0x000849EB File Offset: 0x00082BEB
			public PropertyBag()
			{
				base.AddProperty<Length>(new TransformOrigin.PropertyBag.XProperty());
				base.AddProperty<Length>(new TransformOrigin.PropertyBag.YProperty());
				base.AddProperty<float>(new TransformOrigin.PropertyBag.ZProperty());
			}

			// Token: 0x02000482 RID: 1154
			private class XProperty : Property<TransformOrigin, Length>
			{
				// Token: 0x1700095F RID: 2399
				// (get) Token: 0x0600243D RID: 9277 RVA: 0x00084A19 File Offset: 0x00082C19
				public override string Name { get; } = "x";

				// Token: 0x17000960 RID: 2400
				// (get) Token: 0x0600243E RID: 9278 RVA: 0x00084A21 File Offset: 0x00082C21
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600243F RID: 9279 RVA: 0x00084A29 File Offset: 0x00082C29
				public override Length GetValue(ref TransformOrigin container)
				{
					return container.x;
				}

				// Token: 0x06002440 RID: 9280 RVA: 0x00084A31 File Offset: 0x00082C31
				public override void SetValue(ref TransformOrigin container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000483 RID: 1155
			private class YProperty : Property<TransformOrigin, Length>
			{
				// Token: 0x17000961 RID: 2401
				// (get) Token: 0x06002442 RID: 9282 RVA: 0x00084A56 File Offset: 0x00082C56
				public override string Name { get; } = "y";

				// Token: 0x17000962 RID: 2402
				// (get) Token: 0x06002443 RID: 9283 RVA: 0x00084A5E File Offset: 0x00082C5E
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002444 RID: 9284 RVA: 0x00084A66 File Offset: 0x00082C66
				public override Length GetValue(ref TransformOrigin container)
				{
					return container.y;
				}

				// Token: 0x06002445 RID: 9285 RVA: 0x00084A6E File Offset: 0x00082C6E
				public override void SetValue(ref TransformOrigin container, Length value)
				{
					container.y = value;
				}
			}

			// Token: 0x02000484 RID: 1156
			private class ZProperty : Property<TransformOrigin, float>
			{
				// Token: 0x17000963 RID: 2403
				// (get) Token: 0x06002447 RID: 9287 RVA: 0x00084A93 File Offset: 0x00082C93
				public override string Name { get; } = "z";

				// Token: 0x17000964 RID: 2404
				// (get) Token: 0x06002448 RID: 9288 RVA: 0x00084A9B File Offset: 0x00082C9B
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002449 RID: 9289 RVA: 0x00084AA3 File Offset: 0x00082CA3
				public override float GetValue(ref TransformOrigin container)
				{
					return container.z;
				}

				// Token: 0x0600244A RID: 9290 RVA: 0x00084AAB File Offset: 0x00082CAB
				public override void SetValue(ref TransformOrigin container, float value)
				{
					container.z = value;
				}
			}
		}
	}
}
