using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class GUILayoutEntry
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
		public virtual int marginLeft
		{
			get
			{
				return this.style.margin.left;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		public virtual int marginRight
		{
			get
			{
				return this.style.margin.right;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000ECCA File Offset: 0x0000CECA
		public virtual int marginTop
		{
			get
			{
				return this.style.margin.top;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public virtual int marginBottom
		{
			get
			{
				return this.style.margin.bottom;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000ECEE File Offset: 0x0000CEEE
		public int marginHorizontal
		{
			get
			{
				return this.marginLeft + this.marginRight;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000ECFD File Offset: 0x0000CEFD
		public int marginVertical
		{
			get
			{
				return this.marginBottom + this.marginTop;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			bool flag = _style == null;
			if (flag)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000027FD File Offset: 0x000009FD
		public virtual void CalcWidth()
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000027FD File Offset: 0x000009FD
		public virtual void CalcHeight()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000EDFD File Offset: 0x0000CFFD
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000EE1A File Offset: 0x0000D01A
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000EE38 File Offset: 0x0000D038
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
			this.stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
			this.m_Style = style;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000EE8C File Offset: 0x0000D08C
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				foreach (GUILayoutOption guilayoutOption in options)
				{
					switch (guilayoutOption.type)
					{
					case GUILayoutOption.Type.fixedWidth:
						this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
						this.stretchWidth = 0;
						break;
					case GUILayoutOption.Type.fixedHeight:
						this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
						this.stretchHeight = 0;
						break;
					case GUILayoutOption.Type.minWidth:
					{
						this.minWidth = (float)guilayoutOption.value;
						bool flag2 = this.maxWidth < this.minWidth;
						if (flag2)
						{
							this.maxWidth = this.minWidth;
						}
						break;
					}
					case GUILayoutOption.Type.maxWidth:
					{
						this.maxWidth = (float)guilayoutOption.value;
						bool flag3 = this.minWidth > this.maxWidth;
						if (flag3)
						{
							this.minWidth = this.maxWidth;
						}
						this.stretchWidth = 0;
						break;
					}
					case GUILayoutOption.Type.minHeight:
					{
						this.minHeight = (float)guilayoutOption.value;
						bool flag4 = this.maxHeight < this.minHeight;
						if (flag4)
						{
							this.maxHeight = this.minHeight;
						}
						break;
					}
					case GUILayoutOption.Type.maxHeight:
					{
						this.maxHeight = (float)guilayoutOption.value;
						bool flag5 = this.minHeight > this.maxHeight;
						if (flag5)
						{
							this.minHeight = this.maxHeight;
						}
						this.stretchHeight = 0;
						break;
					}
					case GUILayoutOption.Type.stretchWidth:
						this.stretchWidth = (int)guilayoutOption.value;
						break;
					case GUILayoutOption.Type.stretchHeight:
						this.stretchHeight = (int)guilayoutOption.value;
						break;
					}
				}
				bool flag6 = this.maxWidth != 0f && this.maxWidth < this.minWidth;
				if (flag6)
				{
					this.maxWidth = this.minWidth;
				}
				bool flag7 = this.maxHeight != 0f && this.maxHeight < this.minHeight;
				if (flag7)
				{
					this.maxHeight = this.minHeight;
				}
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new string[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style != null) ? this.style.name : "NULL",
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth.ToString(),
				"-",
				this.maxWidth.ToString(),
				(this.stretchWidth != 0) ? "+" : "",
				", H: ",
				this.minHeight.ToString(),
				"-",
				this.maxHeight.ToString(),
				(this.stretchHeight != 0) ? "+" : ""
			});
		}

		// Token: 0x04000105 RID: 261
		public float minWidth;

		// Token: 0x04000106 RID: 262
		public float maxWidth;

		// Token: 0x04000107 RID: 263
		public float minHeight;

		// Token: 0x04000108 RID: 264
		public float maxHeight;

		// Token: 0x04000109 RID: 265
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x0400010A RID: 266
		public int stretchWidth;

		// Token: 0x0400010B RID: 267
		public int stretchHeight;

		// Token: 0x0400010C RID: 268
		public bool consideredForMargin = true;

		// Token: 0x0400010D RID: 269
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x0400010E RID: 270
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x0400010F RID: 271
		protected static int indent = 0;
	}
}
