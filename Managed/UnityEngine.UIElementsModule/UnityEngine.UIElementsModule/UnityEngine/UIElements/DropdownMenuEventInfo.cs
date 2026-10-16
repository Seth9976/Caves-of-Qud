using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000204 RID: 516
	public class DropdownMenuEventInfo
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00046724 File Offset: 0x00044924
		public EventModifiers modifiers { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0004672C File Offset: 0x0004492C
		public Vector2 mousePosition { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00046734 File Offset: 0x00044934
		public Vector2 localMousePosition { get; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0004673C File Offset: 0x0004493C
		private char character { get; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00046744 File Offset: 0x00044944
		private KeyCode keyCode { get; }

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004674C File Offset: 0x0004494C
		public DropdownMenuEventInfo(EventBase e)
		{
			IMouseEvent mouseEvent = e as IMouseEvent;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.mousePosition = mouseEvent.mousePosition;
				this.localMousePosition = mouseEvent.localMousePosition;
				this.modifiers = mouseEvent.modifiers;
				this.character = '\0';
				this.keyCode = KeyCode.None;
			}
			else
			{
				IPointerEvent pointerEvent = e as IPointerEvent;
				bool flag2 = pointerEvent != null;
				if (flag2)
				{
					this.mousePosition = pointerEvent.position;
					this.localMousePosition = pointerEvent.localPosition;
					this.modifiers = pointerEvent.modifiers;
					this.character = '\0';
					this.keyCode = KeyCode.None;
				}
				else
				{
					IKeyboardEvent keyboardEvent = e as IKeyboardEvent;
					bool flag3 = keyboardEvent != null;
					if (flag3)
					{
						this.character = keyboardEvent.character;
						this.keyCode = keyboardEvent.keyCode;
						this.modifiers = keyboardEvent.modifiers;
						this.mousePosition = Vector2.zero;
						this.localMousePosition = Vector2.zero;
					}
				}
			}
		}
	}
}
