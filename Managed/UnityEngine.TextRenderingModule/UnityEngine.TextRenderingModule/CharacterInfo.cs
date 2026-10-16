using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[UsedByNativeCode]
	public struct CharacterInfo
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002F54 File Offset: 0x00001154
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002F74 File Offset: 0x00001174
		public int advance
		{
			get
			{
				return (int)Math.Round((double)this.width, MidpointRounding.AwayFromZero);
			}
			set
			{
				this.width = (float)value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002F9E File Offset: 0x0000119E
		public int glyphWidth
		{
			get
			{
				return (int)this.vert.width;
			}
			set
			{
				this.vert.width = (float)value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002FB0 File Offset: 0x000011B0
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002FD0 File Offset: 0x000011D0
		public int glyphHeight
		{
			get
			{
				return (int)(-(int)this.vert.height);
			}
			set
			{
				float height = this.vert.height;
				this.vert.height = (float)(-(float)value);
				this.vert.y = this.vert.y + (height - this.vert.height);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000301C File Offset: 0x0000121C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000303A File Offset: 0x0000123A
		public int bearing
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				this.vert.x = (float)value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000304C File Offset: 0x0000124C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003076 File Offset: 0x00001276
		public int minY
		{
			get
			{
				return (int)(this.vert.y + this.vert.height);
			}
			set
			{
				this.vert.height = (float)value - this.vert.y;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003094 File Offset: 0x00001294
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000030B4 File Offset: 0x000012B4
		public int maxY
		{
			get
			{
				return (int)this.vert.y;
			}
			set
			{
				float y = this.vert.y;
				this.vert.y = (float)value;
				this.vert.height = this.vert.height + (y - this.vert.y);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000030FC File Offset: 0x000012FC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000311C File Offset: 0x0000131C
		public int minX
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				float x = this.vert.x;
				this.vert.x = (float)value;
				this.vert.width = this.vert.width + (x - this.vert.x);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003164 File Offset: 0x00001364
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000318E File Offset: 0x0000138E
		public int maxX
		{
			get
			{
				return (int)(this.vert.x + this.vert.width);
			}
			set
			{
				this.vert.width = (float)value - this.vert.x;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000031AC File Offset: 0x000013AC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000031DC File Offset: 0x000013DC
		internal Vector2 uvBottomLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.y = value.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003254 File Offset: 0x00001454
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003290 File Offset: 0x00001490
		internal Vector2 uvBottomRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.width = value.x - this.uv.x;
				this.uv.y = value.y;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000032F4 File Offset: 0x000014F4
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003339 File Offset: 0x00001539
		internal Vector2 uvTopRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y + this.uv.height);
			}
			set
			{
				this.uv.width = value.x - this.uv.x;
				this.uv.height = value.y - this.uv.y;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000033B4 File Offset: 0x000015B4
		internal Vector2 uvTopLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y + this.uv.height);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.height = value.y - this.uv.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003418 File Offset: 0x00001618
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003430 File Offset: 0x00001630
		public Vector2 uvBottomLeft
		{
			get
			{
				return this.uvBottomLeftUnFlipped;
			}
			set
			{
				this.uvBottomLeftUnFlipped = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000343C File Offset: 0x0000163C
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003464 File Offset: 0x00001664
		public Vector2 uvBottomRight
		{
			get
			{
				return this.flipped ? this.uvTopLeftUnFlipped : this.uvBottomRightUnFlipped;
			}
			set
			{
				bool flag = this.flipped;
				if (flag)
				{
					this.uvTopLeftUnFlipped = value;
				}
				else
				{
					this.uvBottomRightUnFlipped = value;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003490 File Offset: 0x00001690
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000034A8 File Offset: 0x000016A8
		public Vector2 uvTopRight
		{
			get
			{
				return this.uvTopRightUnFlipped;
			}
			set
			{
				this.uvTopRightUnFlipped = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000034B4 File Offset: 0x000016B4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000034DC File Offset: 0x000016DC
		public Vector2 uvTopLeft
		{
			get
			{
				return this.flipped ? this.uvBottomRightUnFlipped : this.uvTopLeftUnFlipped;
			}
			set
			{
				bool flag = this.flipped;
				if (flag)
				{
					this.uvBottomRightUnFlipped = value;
				}
				else
				{
					this.uvTopLeftUnFlipped = value;
				}
			}
		}

		// Token: 0x0400003F RID: 63
		public int index;

		// Token: 0x04000040 RID: 64
		[Obsolete("CharacterInfo.uv is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead.")]
		public Rect uv;

		// Token: 0x04000041 RID: 65
		[Obsolete("CharacterInfo.vert is deprecated. Use minX, maxX, minY, maxY instead.")]
		public Rect vert;

		// Token: 0x04000042 RID: 66
		[Obsolete("CharacterInfo.width is deprecated. Use advance instead.")]
		[NativeName("advance")]
		public float width;

		// Token: 0x04000043 RID: 67
		public int size;

		// Token: 0x04000044 RID: 68
		public FontStyle style;

		// Token: 0x04000045 RID: 69
		[Obsolete("CharacterInfo.flipped is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead, which will be correct regardless of orientation.")]
		public bool flipped;
	}
}
