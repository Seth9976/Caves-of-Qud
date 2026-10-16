using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000124 RID: 292
	public class Vector3Control : InputControl<Vector3>
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0004FF9E File Offset: 0x0004E19E
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x0004FFA6 File Offset: 0x0004E1A6
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0004FFAF File Offset: 0x0004E1AF
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x0004FFB7 File Offset: 0x0004E1B7
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004FFC0 File Offset: 0x0004E1C0
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x0004FFC8 File Offset: 0x0004E1C8
		[InputControl(offset = 8U, displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x060010A4 RID: 4260 RVA: 0x0004FFD1 File Offset: 0x0004E1D1
		public Vector3Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector3;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0004FFE9 File Offset: 0x0004E1E9
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			base.FinishSetup();
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00050024 File Offset: 0x0004E224
		public unsafe override Vector3 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				return *(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector3(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr), this.z.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00050080 File Offset: 0x0004E280
		public unsafe override void WriteValueIntoState(Vector3 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				*(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000500EC File Offset: 0x0004E2EC
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00050108 File Offset: 0x0004E308
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 96U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U)
			{
				return InputStateBlock.FormatVector3;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
