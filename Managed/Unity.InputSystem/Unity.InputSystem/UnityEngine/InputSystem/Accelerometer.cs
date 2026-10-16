using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000048 RID: 72
	[InputControlLayout(stateType = typeof(AccelerometerState))]
	public class Accelerometer : Sensor
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0002E169 File Offset: 0x0002C369
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0002E171 File Offset: 0x0002C371
		public Vector3Control acceleration { get; protected set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002E17A File Offset: 0x0002C37A
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0002E181 File Offset: 0x0002C381
		public static Accelerometer current { get; private set; }

		// Token: 0x060007EC RID: 2028 RVA: 0x0002E189 File Offset: 0x0002C389
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Accelerometer.current = this;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002E197 File Offset: 0x0002C397
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Accelerometer.current == this)
			{
				Accelerometer.current = null;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002E1AD File Offset: 0x0002C3AD
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
