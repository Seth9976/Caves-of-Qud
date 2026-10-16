using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004A RID: 74
	[InputControlLayout(stateType = typeof(GravityState), displayName = "Gravity")]
	public class GravitySensor : Sensor
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002E233 File Offset: 0x0002C433
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002E23B File Offset: 0x0002C43B
		public Vector3Control gravity { get; protected set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002E244 File Offset: 0x0002C444
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002E24B File Offset: 0x0002C44B
		public static GravitySensor current { get; private set; }

		// Token: 0x060007FC RID: 2044 RVA: 0x0002E253 File Offset: 0x0002C453
		protected override void FinishSetup()
		{
			this.gravity = base.GetChildControl<Vector3Control>("gravity");
			base.FinishSetup();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002E26C File Offset: 0x0002C46C
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			GravitySensor.current = this;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002E27A File Offset: 0x0002C47A
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (GravitySensor.current == this)
			{
				GravitySensor.current = null;
			}
		}
	}
}
