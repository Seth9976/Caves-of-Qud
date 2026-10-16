using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004E RID: 78
	[InputControlLayout(displayName = "Light")]
	public class LightSensor : Sensor
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0002E3C7 File Offset: 0x0002C5C7
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0002E3CF File Offset: 0x0002C5CF
		[InputControl(displayName = "Light Level", noisy = true)]
		public AxisControl lightLevel { get; protected set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0002E3DF File Offset: 0x0002C5DF
		public static LightSensor current { get; private set; }

		// Token: 0x0600081C RID: 2076 RVA: 0x0002E3E7 File Offset: 0x0002C5E7
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LightSensor.current = this;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002E3F5 File Offset: 0x0002C5F5
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LightSensor.current == this)
			{
				LightSensor.current = null;
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002E40B File Offset: 0x0002C60B
		protected override void FinishSetup()
		{
			this.lightLevel = base.GetChildControl<AxisControl>("lightLevel");
			base.FinishSetup();
		}
	}
}
