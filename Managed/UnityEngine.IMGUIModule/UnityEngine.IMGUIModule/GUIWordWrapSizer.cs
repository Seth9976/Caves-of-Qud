using System;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	internal sealed class GUIWordWrapSizer : GUILayoutEntry
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0000F77C File Offset: 0x0000D97C
		public GUIWordWrapSizer(GUIStyle style, GUIContent content, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, style)
		{
			this.m_Content = new GUIContent(content);
			this.ApplyOptions(options);
			this.m_ForcedMinHeight = this.minHeight;
			this.m_ForcedMaxHeight = this.maxHeight;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public override void CalcWidth()
		{
			bool flag = this.minWidth == 0f || this.maxWidth == 0f;
			if (flag)
			{
				float num;
				float num2;
				base.style.CalcMinMaxWidth(this.m_Content, out num, out num2);
				num = Mathf.Ceil(num);
				num2 = Mathf.Ceil(num2);
				bool flag2 = this.minWidth == 0f;
				if (flag2)
				{
					this.minWidth = num;
				}
				bool flag3 = this.maxWidth == 0f;
				if (flag3)
				{
					this.maxWidth = num2;
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public override void CalcHeight()
		{
			bool flag = this.m_ForcedMinHeight == 0f || this.m_ForcedMaxHeight == 0f;
			if (flag)
			{
				float num = base.style.CalcHeight(this.m_Content, this.rect.width);
				bool flag2 = this.m_ForcedMinHeight == 0f;
				if (flag2)
				{
					this.minHeight = num;
				}
				else
				{
					this.minHeight = this.m_ForcedMinHeight;
				}
				bool flag3 = this.m_ForcedMaxHeight == 0f;
				if (flag3)
				{
					this.maxHeight = num;
				}
				else
				{
					this.maxHeight = this.m_ForcedMaxHeight;
				}
			}
		}

		// Token: 0x04000117 RID: 279
		private readonly GUIContent m_Content;

		// Token: 0x04000118 RID: 280
		private readonly float m_ForcedMinHeight;

		// Token: 0x04000119 RID: 281
		private readonly float m_ForcedMaxHeight;
	}
}
