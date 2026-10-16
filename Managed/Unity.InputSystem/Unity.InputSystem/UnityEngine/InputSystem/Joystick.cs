using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003A RID: 58
	[InputControlLayout(stateType = typeof(JoystickState), isGenericTypeOfDevice = true)]
	public class Joystick : InputDevice
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00014BF3 File Offset: 0x00012DF3
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00014BFB File Offset: 0x00012DFB
		public ButtonControl trigger { get; protected set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00014C04 File Offset: 0x00012E04
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00014C0C File Offset: 0x00012E0C
		public StickControl stick { get; protected set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00014C15 File Offset: 0x00012E15
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00014C1D File Offset: 0x00012E1D
		public AxisControl twist { get; protected set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00014C26 File Offset: 0x00012E26
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00014C2E File Offset: 0x00012E2E
		public Vector2Control hatswitch { get; protected set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00014C37 File Offset: 0x00012E37
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00014C3E File Offset: 0x00012E3E
		public static Joystick current { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00014C46 File Offset: 0x00012E46
		public new static ReadOnlyArray<Joystick> all
		{
			get
			{
				return new ReadOnlyArray<Joystick>(Joystick.s_Joysticks, 0, Joystick.s_JoystickCount);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00014C58 File Offset: 0x00012E58
		protected override void FinishSetup()
		{
			this.trigger = base.GetChildControl<ButtonControl>("{PrimaryTrigger}");
			this.stick = base.GetChildControl<StickControl>("{Primary2DMotion}");
			this.twist = base.TryGetChildControl<AxisControl>("{Twist}");
			this.hatswitch = base.TryGetChildControl<Vector2Control>("{Hatswitch}");
			base.FinishSetup();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00014CAF File Offset: 0x00012EAF
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Joystick.current = this;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00014CBD File Offset: 0x00012EBD
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Joystick>(ref Joystick.s_Joysticks, ref Joystick.s_JoystickCount, this, 10);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00014CD4 File Offset: 0x00012ED4
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Joystick.current == this)
			{
				Joystick.current = null;
			}
			int num = Joystick.s_Joysticks.IndexOfReference(this, Joystick.s_JoystickCount);
			if (num != -1)
			{
				Joystick.s_Joysticks.EraseAtWithCapacity<Joystick>(ref Joystick.s_JoystickCount, num);
			}
		}

		// Token: 0x0400018A RID: 394
		private static int s_JoystickCount;

		// Token: 0x0400018B RID: 395
		private static Joystick[] s_Joysticks;
	}
}
