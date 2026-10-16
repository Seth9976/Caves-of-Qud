using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000037 RID: 55
	[InputControlLayout(stateType = typeof(GamepadState), isGenericTypeOfDevice = true)]
	public class Gamepad : InputDevice, IDualMotorRumble, IHaptics
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00013977 File Offset: 0x00011B77
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0001397F File Offset: 0x00011B7F
		public ButtonControl buttonWest { get; protected set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00013988 File Offset: 0x00011B88
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00013990 File Offset: 0x00011B90
		public ButtonControl buttonNorth { get; protected set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00013999 File Offset: 0x00011B99
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000139A1 File Offset: 0x00011BA1
		public ButtonControl buttonSouth { get; protected set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000139AA File Offset: 0x00011BAA
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x000139B2 File Offset: 0x00011BB2
		public ButtonControl buttonEast { get; protected set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000139BB File Offset: 0x00011BBB
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000139C3 File Offset: 0x00011BC3
		public ButtonControl leftStickButton { get; protected set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000139CC File Offset: 0x00011BCC
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x000139D4 File Offset: 0x00011BD4
		public ButtonControl rightStickButton { get; protected set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000139DD File Offset: 0x00011BDD
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x000139E5 File Offset: 0x00011BE5
		public ButtonControl startButton { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000139EE File Offset: 0x00011BEE
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x000139F6 File Offset: 0x00011BF6
		public ButtonControl selectButton { get; protected set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000139FF File Offset: 0x00011BFF
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00013A07 File Offset: 0x00011C07
		public DpadControl dpad { get; protected set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00013A10 File Offset: 0x00011C10
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00013A18 File Offset: 0x00011C18
		public ButtonControl leftShoulder { get; protected set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00013A21 File Offset: 0x00011C21
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00013A29 File Offset: 0x00011C29
		public ButtonControl rightShoulder { get; protected set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00013A32 File Offset: 0x00011C32
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00013A3A File Offset: 0x00011C3A
		public StickControl leftStick { get; protected set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00013A43 File Offset: 0x00011C43
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00013A4B File Offset: 0x00011C4B
		public StickControl rightStick { get; protected set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00013A54 File Offset: 0x00011C54
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00013A5C File Offset: 0x00011C5C
		public ButtonControl leftTrigger { get; protected set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00013A65 File Offset: 0x00011C65
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00013A6D File Offset: 0x00011C6D
		public ButtonControl rightTrigger { get; protected set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00013A76 File Offset: 0x00011C76
		public ButtonControl aButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00013A7E File Offset: 0x00011C7E
		public ButtonControl bButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00013A86 File Offset: 0x00011C86
		public ButtonControl xButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00013A8E File Offset: 0x00011C8E
		public ButtonControl yButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00013A96 File Offset: 0x00011C96
		public ButtonControl triangleButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00013A9E File Offset: 0x00011C9E
		public ButtonControl squareButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00013AA6 File Offset: 0x00011CA6
		public ButtonControl circleButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00013AAE File Offset: 0x00011CAE
		public ButtonControl crossButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x17000139 RID: 313
		public ButtonControl this[GamepadButton button]
		{
			get
			{
				switch (button)
				{
				case GamepadButton.DpadUp:
					return this.dpad.up;
				case GamepadButton.DpadDown:
					return this.dpad.down;
				case GamepadButton.DpadLeft:
					return this.dpad.left;
				case GamepadButton.DpadRight:
					return this.dpad.right;
				case GamepadButton.North:
					return this.buttonNorth;
				case GamepadButton.East:
					return this.buttonEast;
				case GamepadButton.South:
					return this.buttonSouth;
				case GamepadButton.West:
					return this.buttonWest;
				case GamepadButton.LeftStick:
					return this.leftStickButton;
				case GamepadButton.RightStick:
					return this.rightStickButton;
				case GamepadButton.LeftShoulder:
					return this.leftShoulder;
				case GamepadButton.RightShoulder:
					return this.rightShoulder;
				case GamepadButton.Start:
					return this.startButton;
				case GamepadButton.Select:
					return this.selectButton;
				default:
					if (button == GamepadButton.LeftTrigger)
					{
						return this.leftTrigger;
					}
					if (button != GamepadButton.RightTrigger)
					{
						throw new InvalidEnumArgumentException("button", (int)button, typeof(GamepadButton));
					}
					return this.rightTrigger;
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00013BAB File Offset: 0x00011DAB
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00013BB2 File Offset: 0x00011DB2
		public static Gamepad current { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00013BBA File Offset: 0x00011DBA
		public new static ReadOnlyArray<Gamepad> all
		{
			get
			{
				return new ReadOnlyArray<Gamepad>(Gamepad.s_Gamepads, 0, Gamepad.s_GamepadCount);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00013BCC File Offset: 0x00011DCC
		protected override void FinishSetup()
		{
			this.buttonWest = base.GetChildControl<ButtonControl>("buttonWest");
			this.buttonNorth = base.GetChildControl<ButtonControl>("buttonNorth");
			this.buttonSouth = base.GetChildControl<ButtonControl>("buttonSouth");
			this.buttonEast = base.GetChildControl<ButtonControl>("buttonEast");
			this.startButton = base.GetChildControl<ButtonControl>("start");
			this.selectButton = base.GetChildControl<ButtonControl>("select");
			this.leftStickButton = base.GetChildControl<ButtonControl>("leftStickPress");
			this.rightStickButton = base.GetChildControl<ButtonControl>("rightStickPress");
			this.dpad = base.GetChildControl<DpadControl>("dpad");
			this.leftShoulder = base.GetChildControl<ButtonControl>("leftShoulder");
			this.rightShoulder = base.GetChildControl<ButtonControl>("rightShoulder");
			this.leftStick = base.GetChildControl<StickControl>("leftStick");
			this.rightStick = base.GetChildControl<StickControl>("rightStick");
			this.leftTrigger = base.GetChildControl<ButtonControl>("leftTrigger");
			this.rightTrigger = base.GetChildControl<ButtonControl>("rightTrigger");
			base.FinishSetup();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00013CDE File Offset: 0x00011EDE
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gamepad.current = this;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013CEC File Offset: 0x00011EEC
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Gamepad>(ref Gamepad.s_Gamepads, ref Gamepad.s_GamepadCount, this, 10);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00013D04 File Offset: 0x00011F04
		protected override void OnRemoved()
		{
			if (Gamepad.current == this)
			{
				Gamepad.current = null;
			}
			int num = Gamepad.s_Gamepads.IndexOfReference(this, Gamepad.s_GamepadCount);
			if (num != -1)
			{
				Gamepad.s_Gamepads.EraseAtWithCapacity<Gamepad>(ref Gamepad.s_GamepadCount, num);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00013D44 File Offset: 0x00011F44
		public virtual void PauseHaptics()
		{
			this.m_Rumble.PauseHaptics(this);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013D52 File Offset: 0x00011F52
		public virtual void ResumeHaptics()
		{
			this.m_Rumble.ResumeHaptics(this);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00013D60 File Offset: 0x00011F60
		public virtual void ResetHaptics()
		{
			this.m_Rumble.ResetHaptics(this);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013D6E File Offset: 0x00011F6E
		public virtual void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_Rumble.SetMotorSpeeds(this, lowFrequency, highFrequency);
		}

		// Token: 0x0400015E RID: 350
		private DualMotorRumble m_Rumble;

		// Token: 0x0400015F RID: 351
		private static int s_GamepadCount;

		// Token: 0x04000160 RID: 352
		private static Gamepad[] s_Gamepads;
	}
}
