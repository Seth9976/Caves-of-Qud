using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004D RID: 77
	[InputControlLayout(displayName = "Magnetic Field")]
	public class MagneticFieldSensor : Sensor
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0002E362 File Offset: 0x0002C562
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0002E36A File Offset: 0x0002C56A
		[InputControl(displayName = "Magnetic Field", noisy = true)]
		public Vector3Control magneticField { get; protected set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0002E373 File Offset: 0x0002C573
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0002E37A File Offset: 0x0002C57A
		public static MagneticFieldSensor current { get; private set; }

		// Token: 0x06000814 RID: 2068 RVA: 0x0002E382 File Offset: 0x0002C582
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			MagneticFieldSensor.current = this;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002E390 File Offset: 0x0002C590
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (MagneticFieldSensor.current == this)
			{
				MagneticFieldSensor.current = null;
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002E3A6 File Offset: 0x0002C5A6
		protected override void FinishSetup()
		{
			this.magneticField = base.GetChildControl<Vector3Control>("magneticField");
			base.FinishSetup();
		}
	}
}
