using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000049 RID: 73
	[InputControlLayout(stateType = typeof(GyroscopeState))]
	public class Gyroscope : Sensor
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002E1CE File Offset: 0x0002C3CE
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0002E1D6 File Offset: 0x0002C3D6
		public Vector3Control angularVelocity { get; protected set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002E1DF File Offset: 0x0002C3DF
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002E1E6 File Offset: 0x0002C3E6
		public static Gyroscope current { get; private set; }

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002E1EE File Offset: 0x0002C3EE
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gyroscope.current = this;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Gyroscope.current == this)
			{
				Gyroscope.current = null;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002E212 File Offset: 0x0002C412
		protected override void FinishSetup()
		{
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}
	}
}
