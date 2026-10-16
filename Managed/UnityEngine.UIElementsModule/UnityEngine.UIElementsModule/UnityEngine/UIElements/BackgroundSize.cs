using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000013 RID: 19
	public struct BackgroundSize : IEquatable<BackgroundSize>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003098 File Offset: 0x00001298
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000030B0 File Offset: 0x000012B0
		public BackgroundSizeType sizeType
		{
			get
			{
				return this.m_SizeType;
			}
			set
			{
				this.m_SizeType = value;
				this.m_X = new Length(0f);
				this.m_Y = new Length(0f);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030DC File Offset: 0x000012DC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000030F4 File Offset: 0x000012F4
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
				this.m_SizeType = BackgroundSizeType.Length;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003108 File Offset: 0x00001308
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003120 File Offset: 0x00001320
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
				this.m_SizeType = BackgroundSizeType.Length;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003131 File Offset: 0x00001331
		public BackgroundSize(Length sizeX, Length sizeY)
		{
			this.m_SizeType = BackgroundSizeType.Length;
			this.m_X = sizeX;
			this.m_Y = sizeY;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030B0 File Offset: 0x000012B0
		public BackgroundSize(BackgroundSizeType sizeType)
		{
			this.m_SizeType = sizeType;
			this.m_X = new Length(0f);
			this.m_Y = new Length(0f);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000314C File Offset: 0x0000134C
		internal static BackgroundSize Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundSize(ScaleMode.StretchToFill);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003164 File Offset: 0x00001364
		public override bool Equals(object obj)
		{
			return obj is BackgroundSize && this.Equals((BackgroundSize)obj);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003190 File Offset: 0x00001390
		public bool Equals(BackgroundSize other)
		{
			return other.x == this.x && other.y == this.y && other.sizeType == this.sizeType;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031DC File Offset: 0x000013DC
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.m_SizeType.GetHashCode();
			num = num * -1521134295 + this.m_X.GetHashCode();
			return num * -1521134295 + this.m_Y.GetHashCode();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003244 File Offset: 0x00001444
		public static bool operator ==(BackgroundSize style1, BackgroundSize style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003260 File Offset: 0x00001460
		public static bool operator !=(BackgroundSize style1, BackgroundSize style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000327C File Offset: 0x0000147C
		public override string ToString()
		{
			return string.Format("(sizeType:{0} x:{1}, y:{2})", this.sizeType, this.x, this.y);
		}

		// Token: 0x04000024 RID: 36
		private BackgroundSizeType m_SizeType;

		// Token: 0x04000025 RID: 37
		private Length m_X;

		// Token: 0x04000026 RID: 38
		private Length m_Y;

		// Token: 0x02000014 RID: 20
		internal class PropertyBag : ContainerPropertyBag<BackgroundSize>
		{
			// Token: 0x06000074 RID: 116 RVA: 0x000032B9 File Offset: 0x000014B9
			public PropertyBag()
			{
				base.AddProperty<BackgroundSizeType>(new BackgroundSize.PropertyBag.SizeTypeProperty());
				base.AddProperty<Length>(new BackgroundSize.PropertyBag.XProperty());
				base.AddProperty<Length>(new BackgroundSize.PropertyBag.YProperty());
			}

			// Token: 0x02000015 RID: 21
			private class SizeTypeProperty : Property<BackgroundSize, BackgroundSizeType>
			{
				// Token: 0x17000016 RID: 22
				// (get) Token: 0x06000075 RID: 117 RVA: 0x000032E7 File Offset: 0x000014E7
				public override string Name { get; } = "sizeType";

				// Token: 0x17000017 RID: 23
				// (get) Token: 0x06000076 RID: 118 RVA: 0x000032EF File Offset: 0x000014EF
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000077 RID: 119 RVA: 0x000032F7 File Offset: 0x000014F7
				public override BackgroundSizeType GetValue(ref BackgroundSize container)
				{
					return container.sizeType;
				}

				// Token: 0x06000078 RID: 120 RVA: 0x000032FF File Offset: 0x000014FF
				public override void SetValue(ref BackgroundSize container, BackgroundSizeType value)
				{
					container.sizeType = value;
				}
			}

			// Token: 0x02000016 RID: 22
			private class XProperty : Property<BackgroundSize, Length>
			{
				// Token: 0x17000018 RID: 24
				// (get) Token: 0x0600007A RID: 122 RVA: 0x00003324 File Offset: 0x00001524
				public override string Name { get; } = "x";

				// Token: 0x17000019 RID: 25
				// (get) Token: 0x0600007B RID: 123 RVA: 0x0000332C File Offset: 0x0000152C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600007C RID: 124 RVA: 0x00003334 File Offset: 0x00001534
				public override Length GetValue(ref BackgroundSize container)
				{
					return container.x;
				}

				// Token: 0x0600007D RID: 125 RVA: 0x0000333C File Offset: 0x0000153C
				public override void SetValue(ref BackgroundSize container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000017 RID: 23
			private class YProperty : Property<BackgroundSize, Length>
			{
				// Token: 0x1700001A RID: 26
				// (get) Token: 0x0600007F RID: 127 RVA: 0x00003361 File Offset: 0x00001561
				public override string Name { get; } = "y";

				// Token: 0x1700001B RID: 27
				// (get) Token: 0x06000080 RID: 128 RVA: 0x00003369 File Offset: 0x00001569
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000081 RID: 129 RVA: 0x00003371 File Offset: 0x00001571
				public override Length GetValue(ref BackgroundSize container)
				{
					return container.y;
				}

				// Token: 0x06000082 RID: 130 RVA: 0x00003379 File Offset: 0x00001579
				public override void SetValue(ref BackgroundSize container, Length value)
				{
					container.y = value;
				}
			}
		}
	}
}
