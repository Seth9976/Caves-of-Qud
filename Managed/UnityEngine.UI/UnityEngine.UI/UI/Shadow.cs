using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000045 RID: 69
	[AddComponentMenu("UI/Effects/Shadow", 80)]
	public class Shadow : BaseMeshEffect
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x00016C94 File Offset: 0x00014E94
		protected Shadow()
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00016CE2 File Offset: 0x00014EE2
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00016CEA File Offset: 0x00014EEA
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00016D0C File Offset: 0x00014F0C
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00016D14 File Offset: 0x00014F14
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00016DB4 File Offset: 0x00014FB4
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00016DBC File Offset: 0x00014FBC
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00016DE0 File Offset: 0x00014FE0
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count + end - start;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00016E94 File Offset: 0x00015094
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016EA8 File Offset: 0x000150A8
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = CollectionPool<List<UIVertex>, UIVertex>.Get();
			vh.GetUIVertexStream(list);
			this.ApplyShadow(list, this.effectColor, 0, list.Count, this.effectDistance.x, this.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			CollectionPool<List<UIVertex>, UIVertex>.Release(list);
		}

		// Token: 0x04000191 RID: 401
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000192 RID: 402
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000193 RID: 403
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000194 RID: 404
		private const float kMaxEffectDistance = 600f;
	}
}
