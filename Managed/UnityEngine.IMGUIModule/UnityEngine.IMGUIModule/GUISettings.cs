using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x060002C3 RID: 707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B340 File Offset: 0x00009540
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000B358 File Offset: 0x00009558
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B364 File Offset: 0x00009564
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000B37C File Offset: 0x0000957C
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B388 File Offset: 0x00009588
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000B3A0 File Offset: 0x000095A0
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B3AC File Offset: 0x000095AC
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000B3E1 File Offset: 0x000095E1
		public float cursorFlashSpeed
		{
			get
			{
				bool flag = this.m_CursorFlashSpeed >= 0f;
				float num;
				if (flag)
				{
					num = this.m_CursorFlashSpeed;
				}
				else
				{
					num = GUISettings.Internal_GetCursorFlashSpeed();
				}
				return num;
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B3EC File Offset: 0x000095EC
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B404 File Offset: 0x00009604
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
