using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EventInterests
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004012 File Offset: 0x00002212
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000401A File Offset: 0x0000221A
		public bool wantsMouseMove { readonly get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004023 File Offset: 0x00002223
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000402B File Offset: 0x0000222B
		public bool wantsMouseEnterLeaveWindow { readonly get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004034 File Offset: 0x00002234
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000403C File Offset: 0x0000223C
		public bool wantsLessLayoutEvents { readonly get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00004048 File Offset: 0x00002248
		public bool WantsEvent(EventType type)
		{
			bool flag;
			if (type != EventType.MouseMove)
			{
				flag = type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow;
			}
			else
			{
				flag = this.wantsMouseMove;
			}
			return flag;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004080 File Offset: 0x00002280
		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					return this.wantsMouseMove;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					goto IL_006C;
				case EventType.KeyDown:
				case EventType.KeyUp:
					return GUIUtility.textFieldInput;
				case EventType.Repaint:
					break;
				default:
					if (type != EventType.ExecuteCommand)
					{
						if (type - EventType.MouseEnterWindow > 1)
						{
							goto IL_006C;
						}
						return this.wantsMouseEnterLeaveWindow;
					}
					break;
				}
				return true;
				IL_006C:
				flag2 = false;
			}
			return flag2;
		}
	}
}
