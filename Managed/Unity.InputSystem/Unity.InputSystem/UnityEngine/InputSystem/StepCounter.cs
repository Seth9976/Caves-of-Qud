using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000053 RID: 83
	[InputControlLayout(displayName = "Step Counter")]
	public class StepCounter : Sensor
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0002E5C8 File Offset: 0x0002C7C8
		[InputControl(displayName = "Step Counter", noisy = true)]
		public IntegerControl stepCounter { get; protected set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002E5D1 File Offset: 0x0002C7D1
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		public static StepCounter current { get; private set; }

		// Token: 0x06000844 RID: 2116 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			StepCounter.current = this;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002E5EE File Offset: 0x0002C7EE
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (StepCounter.current == this)
			{
				StepCounter.current = null;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002E604 File Offset: 0x0002C804
		protected override void FinishSetup()
		{
			this.stepCounter = base.GetChildControl<IntegerControl>("stepCounter");
			base.FinishSetup();
		}
	}
}
