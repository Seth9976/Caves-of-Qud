using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000040 RID: 64
	[InputControlLayout(stateType = typeof(PenState), isGenericTypeOfDevice = true)]
	public class Pen : Pointer
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00015C49 File Offset: 0x00013E49
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00015C51 File Offset: 0x00013E51
		public ButtonControl tip { get; protected set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00015C5A File Offset: 0x00013E5A
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00015C62 File Offset: 0x00013E62
		public ButtonControl eraser { get; protected set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00015C6B File Offset: 0x00013E6B
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00015C73 File Offset: 0x00013E73
		public ButtonControl firstBarrelButton { get; protected set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00015C7C File Offset: 0x00013E7C
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00015C84 File Offset: 0x00013E84
		public ButtonControl secondBarrelButton { get; protected set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00015C8D File Offset: 0x00013E8D
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00015C95 File Offset: 0x00013E95
		public ButtonControl thirdBarrelButton { get; protected set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00015C9E File Offset: 0x00013E9E
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00015CA6 File Offset: 0x00013EA6
		public ButtonControl fourthBarrelButton { get; protected set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00015CAF File Offset: 0x00013EAF
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00015CB7 File Offset: 0x00013EB7
		public ButtonControl inRange { get; protected set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00015CC0 File Offset: 0x00013EC0
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public Vector2Control tilt { get; protected set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00015CD1 File Offset: 0x00013ED1
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00015CD9 File Offset: 0x00013ED9
		public AxisControl twist { get; protected set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00015CE2 File Offset: 0x00013EE2
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00015CE9 File Offset: 0x00013EE9
		public new static Pen current { get; internal set; }

		// Token: 0x170001F8 RID: 504
		public ButtonControl this[PenButton button]
		{
			get
			{
				switch (button)
				{
				case PenButton.Tip:
					return this.tip;
				case PenButton.Eraser:
					return this.eraser;
				case PenButton.BarrelFirst:
					return this.firstBarrelButton;
				case PenButton.BarrelSecond:
					return this.secondBarrelButton;
				case PenButton.InRange:
					return this.inRange;
				case PenButton.BarrelThird:
					return this.thirdBarrelButton;
				case PenButton.BarrelFourth:
					return this.fourthBarrelButton;
				default:
					throw new InvalidEnumArgumentException("button", (int)button, typeof(PenButton));
				}
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015D6B File Offset: 0x00013F6B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pen.current = this;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015D79 File Offset: 0x00013F79
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pen.current == this)
			{
				Pen.current = null;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00015D90 File Offset: 0x00013F90
		protected override void FinishSetup()
		{
			this.tip = base.GetChildControl<ButtonControl>("tip");
			this.eraser = base.GetChildControl<ButtonControl>("eraser");
			this.firstBarrelButton = base.GetChildControl<ButtonControl>("barrel1");
			this.secondBarrelButton = base.GetChildControl<ButtonControl>("barrel2");
			this.thirdBarrelButton = base.GetChildControl<ButtonControl>("barrel3");
			this.fourthBarrelButton = base.GetChildControl<ButtonControl>("barrel4");
			this.inRange = base.GetChildControl<ButtonControl>("inRange");
			this.tilt = base.GetChildControl<Vector2Control>("tilt");
			this.twist = base.GetChildControl<AxisControl>("twist");
			base.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			base.FinishSetup();
		}
	}
}
