using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000051 RID: 81
	[InputControlLayout(displayName = "Humidity")]
	public class HumiditySensor : Sensor
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002E4F6 File Offset: 0x0002C6F6
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0002E4FE File Offset: 0x0002C6FE
		[InputControl(displayName = "Relative Humidity", noisy = true)]
		public AxisControl relativeHumidity { get; protected set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0002E507 File Offset: 0x0002C707
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0002E50E File Offset: 0x0002C70E
		public static HumiditySensor current { get; private set; }

		// Token: 0x06000834 RID: 2100 RVA: 0x0002E516 File Offset: 0x0002C716
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HumiditySensor.current = this;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002E524 File Offset: 0x0002C724
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HumiditySensor.current == this)
			{
				HumiditySensor.current = null;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002E53A File Offset: 0x0002C73A
		protected override void FinishSetup()
		{
			this.relativeHumidity = base.GetChildControl<AxisControl>("relativeHumidity");
			base.FinishSetup();
		}
	}
}
