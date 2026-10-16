using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004C RID: 76
	[InputControlLayout(stateType = typeof(LinearAccelerationState), displayName = "Linear Acceleration")]
	public class LinearAccelerationSensor : Sensor
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0002E2FD File Offset: 0x0002C4FD
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0002E305 File Offset: 0x0002C505
		public Vector3Control acceleration { get; protected set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002E30E File Offset: 0x0002C50E
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0002E315 File Offset: 0x0002C515
		public static LinearAccelerationSensor current { get; private set; }

		// Token: 0x0600080C RID: 2060 RVA: 0x0002E31D File Offset: 0x0002C51D
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LinearAccelerationSensor.current = this;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002E32B File Offset: 0x0002C52B
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LinearAccelerationSensor.current == this)
			{
				LinearAccelerationSensor.current = null;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002E341 File Offset: 0x0002C541
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
