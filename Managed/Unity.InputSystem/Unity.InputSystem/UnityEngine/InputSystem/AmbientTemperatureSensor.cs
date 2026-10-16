using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000052 RID: 82
	[InputControlLayout(displayName = "Ambient Temperature")]
	public class AmbientTemperatureSensor : Sensor
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002E55B File Offset: 0x0002C75B
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0002E563 File Offset: 0x0002C763
		[InputControl(displayName = "Ambient Temperature", noisy = true)]
		public AxisControl ambientTemperature { get; protected set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0002E56C File Offset: 0x0002C76C
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0002E573 File Offset: 0x0002C773
		public static AmbientTemperatureSensor current { get; private set; }

		// Token: 0x0600083C RID: 2108 RVA: 0x0002E57B File Offset: 0x0002C77B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AmbientTemperatureSensor.current = this;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002E589 File Offset: 0x0002C789
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AmbientTemperatureSensor.current == this)
			{
				AmbientTemperatureSensor.current = null;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002E59F File Offset: 0x0002C79F
		protected override void FinishSetup()
		{
			this.ambientTemperature = base.GetChildControl<AxisControl>("ambientTemperature");
			base.FinishSetup();
		}
	}
}
