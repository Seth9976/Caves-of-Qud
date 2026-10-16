using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011B RID: 283
	public class DpadControl : Vector2Control
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004F2A7 File Offset: 0x0004D4A7
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0004F2AF File Offset: 0x0004D4AF
		[InputControl(name = "x", layout = "DpadAxis", useStateFrom = "right", synthetic = true)]
		[InputControl(name = "y", layout = "DpadAxis", useStateFrom = "up", synthetic = true)]
		[InputControl(bit = 0U, displayName = "Up")]
		public ButtonControl up { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0004F2B8 File Offset: 0x0004D4B8
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
		[InputControl(bit = 1U, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004F2C9 File Offset: 0x0004D4C9
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0004F2D1 File Offset: 0x0004D4D1
		[InputControl(bit = 2U, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0004F2DA File Offset: 0x0004D4DA
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0004F2E2 File Offset: 0x0004D4E2
		[InputControl(bit = 3U, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x06001048 RID: 4168 RVA: 0x0004F2EB File Offset: 0x0004D4EB
		public DpadControl()
		{
			this.m_StateBlock.sizeInBits = 4U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004F310 File Offset: 0x0004D510
		protected override void FinishSetup()
		{
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
			base.FinishSetup();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004F368 File Offset: 0x0004D568
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			bool flag = this.up.ReadValueFromStateWithCaching(statePtr) >= this.up.pressPointOrDefault;
			bool flag2 = this.down.ReadValueFromStateWithCaching(statePtr) >= this.down.pressPointOrDefault;
			bool flag3 = this.left.ReadValueFromStateWithCaching(statePtr) >= this.left.pressPointOrDefault;
			bool flag4 = this.right.ReadValueFromStateWithCaching(statePtr) >= this.right.pressPointOrDefault;
			return DpadControl.MakeDpadVector(flag, flag2, flag3, flag4, true);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			bool flag = this.up.IsValueConsideredPressed(value.y);
			bool flag2 = this.down.IsValueConsideredPressed(value.y * -1f);
			bool flag3 = this.left.IsValueConsideredPressed(value.x * -1f);
			bool flag4 = this.right.IsValueConsideredPressed(value.x);
			this.up.WriteValueIntoState((flag && !flag2) ? value.y : 0f, statePtr);
			this.down.WriteValueIntoState((flag2 && !flag) ? (value.y * -1f) : 0f, statePtr);
			this.left.WriteValueIntoState((flag3 && !flag4) ? (value.x * -1f) : 0f, statePtr);
			this.right.WriteValueIntoState((flag4 && !flag3) ? value.x : 0f, statePtr);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004F4E0 File Offset: 0x0004D6E0
		public static Vector2 MakeDpadVector(bool up, bool down, bool left, bool right, bool normalize = true)
		{
			float num = (up ? 1f : 0f);
			float num2 = (down ? (-1f) : 0f);
			float num3 = (left ? (-1f) : 0f);
			float num4 = (right ? 1f : 0f);
			Vector2 vector = new Vector2(num3 + num4, num + num2);
			if (normalize && vector.x != 0f && vector.y != 0f)
			{
				vector = new Vector2(vector.x * 0.707107f, vector.y * 0.707107f);
			}
			return vector;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0004F57D File Offset: 0x0004D77D
		public static Vector2 MakeDpadVector(float up, float down, float left, float right)
		{
			return new Vector2(-left + right, up - down);
		}

		// Token: 0x0200023D RID: 573
		[InputControlLayout(hideInUI = true)]
		public class DpadAxisControl : AxisControl
		{
			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001614 RID: 5652 RVA: 0x00065F83 File Offset: 0x00064183
			// (set) Token: 0x06001615 RID: 5653 RVA: 0x00065F8B File Offset: 0x0006418B
			public int component { get; set; }

			// Token: 0x06001616 RID: 5654 RVA: 0x00065F94 File Offset: 0x00064194
			protected override void FinishSetup()
			{
				base.FinishSetup();
				this.component = ((base.name == "x") ? 0 : 1);
				this.m_StateBlock = this.m_Parent.m_StateBlock;
			}

			// Token: 0x06001617 RID: 5655 RVA: 0x00065FCC File Offset: 0x000641CC
			public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
			{
				return ((DpadControl)this.m_Parent).ReadUnprocessedValueFromState(statePtr)[this.component];
			}
		}

		// Token: 0x0200023E RID: 574
		internal enum ButtonBits
		{
			// Token: 0x04000C65 RID: 3173
			Up,
			// Token: 0x04000C66 RID: 3174
			Down,
			// Token: 0x04000C67 RID: 3175
			Left,
			// Token: 0x04000C68 RID: 3176
			Right
		}
	}
}
