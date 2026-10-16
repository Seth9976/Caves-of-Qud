using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000050 RID: 80
	[InputControlLayout(displayName = "Proximity")]
	public class ProximitySensor : Sensor
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0002E491 File Offset: 0x0002C691
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0002E499 File Offset: 0x0002C699
		[InputControl(displayName = "Distance", noisy = true)]
		public AxisControl distance { get; protected set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0002E4A2 File Offset: 0x0002C6A2
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0002E4A9 File Offset: 0x0002C6A9
		public static ProximitySensor current { get; private set; }

		// Token: 0x0600082C RID: 2092 RVA: 0x0002E4B1 File Offset: 0x0002C6B1
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			ProximitySensor.current = this;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002E4BF File Offset: 0x0002C6BF
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (ProximitySensor.current == this)
			{
				ProximitySensor.current = null;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		protected override void FinishSetup()
		{
			this.distance = base.GetChildControl<AxisControl>("distance");
			base.FinishSetup();
		}
	}
}
