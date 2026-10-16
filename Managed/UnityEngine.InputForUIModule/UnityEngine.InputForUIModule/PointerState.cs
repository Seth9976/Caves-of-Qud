using System;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002E RID: 46
	internal struct PointerState
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000558C File Offset: 0x0000378C
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00005594 File Offset: 0x00003794
		public PointerEvent.Button LastPressedButton { readonly get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000559D File Offset: 0x0000379D
		public PointerEvent.ButtonsState ButtonsState
		{
			get
			{
				return this._buttonsState;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000055A5 File Offset: 0x000037A5
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000055AD File Offset: 0x000037AD
		public DiscreteTime NextPressTime { readonly get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000055B6 File Offset: 0x000037B6
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000055BE File Offset: 0x000037BE
		public int ClickCount { readonly get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000055C7 File Offset: 0x000037C7
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000055CF File Offset: 0x000037CF
		public Vector2 LastPosition { readonly get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000055D8 File Offset: 0x000037D8
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000055E0 File Offset: 0x000037E0
		public int LastDisplayIndex { readonly get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000055E9 File Offset: 0x000037E9
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000055F1 File Offset: 0x000037F1
		public bool LastPositionValid { readonly get; set; }

		// Token: 0x0600010B RID: 267 RVA: 0x000055FC File Offset: 0x000037FC
		public void OnButtonDown(DiscreteTime currentTime, PointerEvent.Button button)
		{
			bool flag = this.LastPressedButton != button || currentTime >= this.NextPressTime;
			if (flag)
			{
				this.ClickCount = 0;
			}
			this.LastPressedButton = button;
			this._buttonsState.Set(button, true);
			int clickCount = this.ClickCount;
			this.ClickCount = clickCount + 1;
			this.NextPressTime = currentTime + PointerState.kClickDelay;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005668 File Offset: 0x00003868
		public void OnButtonUp(DiscreteTime currentTime, PointerEvent.Button button)
		{
			bool flag = this.LastPressedButton != button;
			if (flag)
			{
				this.ClickCount = 1;
			}
			this._buttonsState.Set(button, false);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000569C File Offset: 0x0000389C
		public void OnButtonChange(DiscreteTime currentTime, PointerEvent.Button button, bool previousState, bool newState)
		{
			bool flag = newState && !previousState;
			if (flag)
			{
				this.OnButtonDown(currentTime, button);
			}
			else
			{
				bool flag2 = !newState && previousState;
				if (flag2)
				{
					this.OnButtonUp(currentTime, button);
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000056D8 File Offset: 0x000038D8
		public void OnMove(DiscreteTime currentTime, Vector2 position, int displayIndex)
		{
			this.LastPosition = position;
			this.LastDisplayIndex = displayIndex;
			this.LastPositionValid = true;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000056F4 File Offset: 0x000038F4
		public void Reset()
		{
			this.LastPressedButton = PointerEvent.Button.None;
			this.ButtonsState.Reset();
			this.NextPressTime = DiscreteTime.Zero;
			this.ClickCount = 0;
			this.LastPosition = Vector2.zero;
			this.LastDisplayIndex = 0;
			this.LastPositionValid = false;
		}

		// Token: 0x040000E2 RID: 226
		private PointerEvent.ButtonsState _buttonsState;

		// Token: 0x040000E8 RID: 232
		private static readonly DiscreteTime kClickDelay = new DiscreteTime((double)Event.GetDoubleClickTime() / 1000.0);
	}
}
