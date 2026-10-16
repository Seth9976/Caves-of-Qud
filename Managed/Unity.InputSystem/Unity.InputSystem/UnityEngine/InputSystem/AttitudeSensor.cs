using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004B RID: 75
	[InputControlLayout(stateType = typeof(AttitudeState), displayName = "Attitude")]
	public class AttitudeSensor : Sensor
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002E298 File Offset: 0x0002C498
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public QuaternionControl attitude { get; protected set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002E2A9 File Offset: 0x0002C4A9
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		public static AttitudeSensor current { get; private set; }

		// Token: 0x06000804 RID: 2052 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AttitudeSensor.current = this;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002E2C6 File Offset: 0x0002C4C6
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AttitudeSensor.current == this)
			{
				AttitudeSensor.current = null;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		protected override void FinishSetup()
		{
			this.attitude = base.GetChildControl<QuaternionControl>("attitude");
			base.FinishSetup();
		}
	}
}
