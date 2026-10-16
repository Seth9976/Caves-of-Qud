using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004F RID: 79
	[InputControlLayout(displayName = "Pressure")]
	public class PressureSensor : Sensor
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0002E42C File Offset: 0x0002C62C
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0002E434 File Offset: 0x0002C634
		[InputControl(displayName = "Atmospheric Pressure", noisy = true)]
		public AxisControl atmosphericPressure { get; protected set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0002E43D File Offset: 0x0002C63D
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0002E444 File Offset: 0x0002C644
		public static PressureSensor current { get; private set; }

		// Token: 0x06000824 RID: 2084 RVA: 0x0002E44C File Offset: 0x0002C64C
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			PressureSensor.current = this;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002E45A File Offset: 0x0002C65A
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (PressureSensor.current == this)
			{
				PressureSensor.current = null;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002E470 File Offset: 0x0002C670
		protected override void FinishSetup()
		{
			this.atmosphericPressure = base.GetChildControl<AxisControl>("atmosphericPressure");
			base.FinishSetup();
		}
	}
}
