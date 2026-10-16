using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000256 RID: 598
	public interface IMouseEvent
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011A2 RID: 4514
		EventModifiers modifiers { get; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011A3 RID: 4515
		Vector2 mousePosition { get; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011A4 RID: 4516
		Vector2 localMousePosition { get; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011A5 RID: 4517
		Vector2 mouseDelta { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060011A6 RID: 4518
		int clickCount { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060011A7 RID: 4519
		int button { get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060011A8 RID: 4520
		int pressedButtons { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060011A9 RID: 4521
		bool shiftKey { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060011AA RID: 4522
		bool ctrlKey { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060011AB RID: 4523
		bool commandKey { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060011AC RID: 4524
		bool altKey { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060011AD RID: 4525
		bool actionKey { get; }
	}
}
