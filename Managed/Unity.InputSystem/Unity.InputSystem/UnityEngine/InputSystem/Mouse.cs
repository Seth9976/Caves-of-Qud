using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003E RID: 62
	[InputControlLayout(stateType = typeof(MouseState), isGenericTypeOfDevice = true)]
	public class Mouse : Pointer, IInputStateCallbackReceiver
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00015A65 File Offset: 0x00013C65
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00015A6D File Offset: 0x00013C6D
		public DeltaControl scroll { get; protected set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00015A76 File Offset: 0x00013C76
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00015A7E File Offset: 0x00013C7E
		public ButtonControl leftButton { get; protected set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00015A87 File Offset: 0x00013C87
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00015A8F File Offset: 0x00013C8F
		public ButtonControl middleButton { get; protected set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00015A98 File Offset: 0x00013C98
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public ButtonControl rightButton { get; protected set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00015AA9 File Offset: 0x00013CA9
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x00015AB1 File Offset: 0x00013CB1
		public ButtonControl backButton { get; protected set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00015ABA File Offset: 0x00013CBA
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00015AC2 File Offset: 0x00013CC2
		public ButtonControl forwardButton { get; protected set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00015ACB File Offset: 0x00013CCB
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00015AD3 File Offset: 0x00013CD3
		public IntegerControl clickCount { get; protected set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00015ADC File Offset: 0x00013CDC
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00015AE3 File Offset: 0x00013CE3
		public new static Mouse current { get; private set; }

		// Token: 0x060005A8 RID: 1448 RVA: 0x00015AEB File Offset: 0x00013CEB
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Mouse.current = this;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00015AF9 File Offset: 0x00013CF9
		protected override void OnAdded()
		{
			base.OnAdded();
			if (base.native && Mouse.s_PlatformMouseDevice == null)
			{
				Mouse.s_PlatformMouseDevice = this;
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00015B16 File Offset: 0x00013D16
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Mouse.current == this)
			{
				Mouse.current = null;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00015B2C File Offset: 0x00013D2C
		public void WarpCursorPosition(Vector2 position)
		{
			WarpMousePositionCommand warpMousePositionCommand = WarpMousePositionCommand.Create(position);
			base.ExecuteCommand<WarpMousePositionCommand>(ref warpMousePositionCommand);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015B4C File Offset: 0x00013D4C
		protected override void FinishSetup()
		{
			this.scroll = base.GetChildControl<DeltaControl>("scroll");
			this.leftButton = base.GetChildControl<ButtonControl>("leftButton");
			this.middleButton = base.GetChildControl<ButtonControl>("middleButton");
			this.rightButton = base.GetChildControl<ButtonControl>("rightButton");
			this.forwardButton = base.GetChildControl<ButtonControl>("forwardButton");
			this.backButton = base.GetChildControl<ButtonControl>("backButton");
			base.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.clickCount = base.GetChildControl<IntegerControl>("clickCount");
			base.FinishSetup();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00015BE8 File Offset: 0x00013DE8
		protected new void OnNextUpdate()
		{
			base.OnNextUpdate();
			InputState.Change<Vector2>(this.scroll, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00015C15 File Offset: 0x00013E15
		protected new void OnStateEvent(InputEventPtr eventPtr)
		{
			this.scroll.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			base.OnStateEvent(eventPtr);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00015C30 File Offset: 0x00013E30
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00015C38 File Offset: 0x00013E38
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x04000229 RID: 553
		internal static Mouse s_PlatformMouseDevice;
	}
}
