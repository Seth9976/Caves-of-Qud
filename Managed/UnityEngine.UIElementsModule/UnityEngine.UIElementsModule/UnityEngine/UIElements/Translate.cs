using System;
using System.Globalization;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000485 RID: 1157
	public struct Translate : IEquatable<Translate>
	{
		// Token: 0x0600244C RID: 9292 RVA: 0x00084AD0 File Offset: 0x00082CD0
		public Translate(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
			this.m_isNone = false;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00084AEF File Offset: 0x00082CEF
		public Translate(Length x, Length y)
		{
			this = new Translate(x, y, 0f);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00084B00 File Offset: 0x00082D00
		internal Translate(Vector3 v)
		{
			this = new Translate(v.x, v.y, v.z);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00084B28 File Offset: 0x00082D28
		public static Translate None()
		{
			return new Translate
			{
				m_isNone = true
			};
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x00084B4B File Offset: 0x00082D4B
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x00084B53 File Offset: 0x00082D53
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x00084B5C File Offset: 0x00082D5C
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x00084B64 File Offset: 0x00082D64
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

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x00084B6D File Offset: 0x00082D6D
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x00084B75 File Offset: 0x00082D75
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

		// Token: 0x06002456 RID: 9302 RVA: 0x00084B7E File Offset: 0x00082D7E
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool IsNone()
		{
			return this.m_isNone;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00084B88 File Offset: 0x00082D88
		public static bool operator ==(Translate lhs, Translate rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z && lhs.m_isNone == rhs.m_isNone;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00084BE0 File Offset: 0x00082DE0
		public static bool operator !=(Translate lhs, Translate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00084BFC File Offset: 0x00082DFC
		public bool Equals(Translate other)
		{
			return other == this;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00084C1C File Offset: 0x00082E1C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Translate)
			{
				Translate translate = (Translate)obj;
				flag = this.Equals(translate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00084C48 File Offset: 0x00082E48
		public override int GetHashCode()
		{
			return (this.m_X.GetHashCode() * 793) ^ (this.m_Y.GetHashCode() * 791) ^ (this.m_Z.GetHashCode() * 571);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00084C9C File Offset: 0x00082E9C
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

		// Token: 0x04000FF3 RID: 4083
		private Length m_X;

		// Token: 0x04000FF4 RID: 4084
		private Length m_Y;

		// Token: 0x04000FF5 RID: 4085
		private float m_Z;

		// Token: 0x04000FF6 RID: 4086
		private bool m_isNone;

		// Token: 0x02000486 RID: 1158
		internal class PropertyBag : ContainerPropertyBag<Translate>
		{
			// Token: 0x0600245D RID: 9309 RVA: 0x00084D0B File Offset: 0x00082F0B
			public PropertyBag()
			{
				base.AddProperty<Length>(new Translate.PropertyBag.XProperty());
				base.AddProperty<Length>(new Translate.PropertyBag.YProperty());
				base.AddProperty<float>(new Translate.PropertyBag.ZProperty());
			}

			// Token: 0x02000487 RID: 1159
			private class XProperty : Property<Translate, Length>
			{
				// Token: 0x17000968 RID: 2408
				// (get) Token: 0x0600245E RID: 9310 RVA: 0x00084D39 File Offset: 0x00082F39
				public override string Name { get; } = "x";

				// Token: 0x17000969 RID: 2409
				// (get) Token: 0x0600245F RID: 9311 RVA: 0x00084D41 File Offset: 0x00082F41
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002460 RID: 9312 RVA: 0x00084D49 File Offset: 0x00082F49
				public override Length GetValue(ref Translate container)
				{
					return container.x;
				}

				// Token: 0x06002461 RID: 9313 RVA: 0x00084D51 File Offset: 0x00082F51
				public override void SetValue(ref Translate container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000488 RID: 1160
			private class YProperty : Property<Translate, Length>
			{
				// Token: 0x1700096A RID: 2410
				// (get) Token: 0x06002463 RID: 9315 RVA: 0x00084D76 File Offset: 0x00082F76
				public override string Name { get; } = "y";

				// Token: 0x1700096B RID: 2411
				// (get) Token: 0x06002464 RID: 9316 RVA: 0x00084D7E File Offset: 0x00082F7E
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002465 RID: 9317 RVA: 0x00084D86 File Offset: 0x00082F86
				public override Length GetValue(ref Translate container)
				{
					return container.y;
				}

				// Token: 0x06002466 RID: 9318 RVA: 0x00084D8E File Offset: 0x00082F8E
				public override void SetValue(ref Translate container, Length value)
				{
					container.y = value;
				}
			}

			// Token: 0x02000489 RID: 1161
			private class ZProperty : Property<Translate, float>
			{
				// Token: 0x1700096C RID: 2412
				// (get) Token: 0x06002468 RID: 9320 RVA: 0x00084DB3 File Offset: 0x00082FB3
				public override string Name { get; } = "z";

				// Token: 0x1700096D RID: 2413
				// (get) Token: 0x06002469 RID: 9321 RVA: 0x00084DBB File Offset: 0x00082FBB
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600246A RID: 9322 RVA: 0x00084DC3 File Offset: 0x00082FC3
				public override float GetValue(ref Translate container)
				{
					return container.z;
				}

				// Token: 0x0600246B RID: 9323 RVA: 0x00084DCB File Offset: 0x00082FCB
				public override void SetValue(ref Translate container, float value)
				{
					container.z = value;
				}
			}
		}
	}
}
