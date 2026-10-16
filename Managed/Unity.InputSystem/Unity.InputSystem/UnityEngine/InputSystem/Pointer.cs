using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000041 RID: 65
	[InputControlLayout(stateType = typeof(PointerState), isGenericTypeOfDevice = true)]
	public class Pointer : InputDevice, IInputStateCallbackReceiver
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00015E55 File Offset: 0x00014055
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00015E5D File Offset: 0x0001405D
		public Vector2Control position { get; protected set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00015E66 File Offset: 0x00014066
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00015E6E File Offset: 0x0001406E
		public DeltaControl delta { get; protected set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00015E77 File Offset: 0x00014077
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00015E7F File Offset: 0x0001407F
		public Vector2Control radius { get; protected set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00015E88 File Offset: 0x00014088
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00015E90 File Offset: 0x00014090
		public AxisControl pressure { get; protected set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00015E99 File Offset: 0x00014099
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00015EA1 File Offset: 0x000140A1
		public ButtonControl press { get; protected set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00015EAA File Offset: 0x000140AA
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00015EB2 File Offset: 0x000140B2
		public IntegerControl displayIndex { get; protected set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00015EBB File Offset: 0x000140BB
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00015EC2 File Offset: 0x000140C2
		public static Pointer current { get; internal set; }

		// Token: 0x060005D9 RID: 1497 RVA: 0x00015ECA File Offset: 0x000140CA
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pointer.current = this;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00015ED8 File Offset: 0x000140D8
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pointer.current == this)
			{
				Pointer.current = null;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00015EF0 File Offset: 0x000140F0
		protected override void FinishSetup()
		{
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.press = base.GetChildControl<ButtonControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			base.FinishSetup();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015F6C File Offset: 0x0001416C
		protected void OnNextUpdate()
		{
			InputState.Change<Vector2>(this.delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015F93 File Offset: 0x00014193
		protected void OnStateEvent(InputEventPtr eventPtr)
		{
			this.delta.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015FAF File Offset: 0x000141AF
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00015FB7 File Offset: 0x000141B7
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00015FC0 File Offset: 0x000141C0
		bool IInputStateCallbackReceiver.GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}
	}
}
