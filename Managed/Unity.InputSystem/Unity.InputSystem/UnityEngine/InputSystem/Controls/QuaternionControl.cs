using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011E RID: 286
	public class QuaternionControl : InputControl<Quaternion>
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0004F6F7 File Offset: 0x0004D8F7
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x0004F6FF File Offset: 0x0004D8FF
		[InputControl(displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0004F708 File Offset: 0x0004D908
		// (set) Token: 0x0600105A RID: 4186 RVA: 0x0004F710 File Offset: 0x0004D910
		[InputControl(displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0004F719 File Offset: 0x0004D919
		// (set) Token: 0x0600105C RID: 4188 RVA: 0x0004F721 File Offset: 0x0004D921
		[InputControl(displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0004F72A File Offset: 0x0004D92A
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x0004F732 File Offset: 0x0004D932
		[InputControl(displayName = "W")]
		public AxisControl w { get; set; }

		// Token: 0x0600105F RID: 4191 RVA: 0x0004F73B File Offset: 0x0004D93B
		public QuaternionControl()
		{
			this.m_StateBlock.sizeInBits = 128U;
			this.m_StateBlock.format = InputStateBlock.FormatQuaternion;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004F764 File Offset: 0x0004D964
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			this.w = base.GetChildControl<AxisControl>("w");
			base.FinishSetup();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004F7BC File Offset: 0x0004D9BC
		public unsafe override Quaternion ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				return *(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Quaternion(this.x.ReadValueFromStateWithCaching(statePtr), this.y.ReadValueFromStateWithCaching(statePtr), this.z.ReadValueFromStateWithCaching(statePtr), this.w.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004F824 File Offset: 0x0004DA24
		public unsafe override void WriteValueIntoState(Quaternion value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				*(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
			this.w.WriteValueIntoState(value.w, statePtr);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004F8A0 File Offset: 0x0004DAA0
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 128U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.w.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U && this.w.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 12U && this.x.m_ProcessorStack.length == 0 && this.y.m_ProcessorStack.length == 0 && this.z.m_ProcessorStack.length == 0)
			{
				return InputStateBlock.FormatQuaternion;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
