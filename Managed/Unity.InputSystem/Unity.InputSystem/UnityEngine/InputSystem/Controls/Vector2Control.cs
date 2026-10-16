using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000123 RID: 291
	public class Vector2Control : InputControl<Vector2>
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0004FDF0 File Offset: 0x0004DFF0
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0004FE01 File Offset: 0x0004E001
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0004FE09 File Offset: 0x0004E009
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x06001098 RID: 4248 RVA: 0x0004FE12 File Offset: 0x0004E012
		public Vector2Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector2;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004FE2A File Offset: 0x0004E02A
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			base.FinishSetup();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004FE54 File Offset: 0x0004E054
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				return *(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector2(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				*(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0004FEFC File Offset: 0x0004E0FC
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004FF18 File Offset: 0x0004E118
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 64U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U)
			{
				return InputStateBlock.FormatVector2;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
