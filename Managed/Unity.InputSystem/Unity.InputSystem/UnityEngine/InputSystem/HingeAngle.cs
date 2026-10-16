using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000054 RID: 84
	[InputControlLayout(displayName = "Hinge Angle")]
	public class HingeAngle : Sensor
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0002E625 File Offset: 0x0002C825
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0002E62D File Offset: 0x0002C82D
		public AxisControl angle { get; protected set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0002E636 File Offset: 0x0002C836
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0002E63D File Offset: 0x0002C83D
		public static HingeAngle current { get; private set; }

		// Token: 0x0600084C RID: 2124 RVA: 0x0002E645 File Offset: 0x0002C845
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HingeAngle.current = this;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002E653 File Offset: 0x0002C853
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HingeAngle.current == this)
			{
				HingeAngle.current = null;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002E669 File Offset: 0x0002C869
		protected override void FinishSetup()
		{
			this.angle = base.GetChildControl<AxisControl>("angle");
			base.FinishSetup();
		}
	}
}
