using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011A RID: 282
	public class DoubleControl : InputControl<double>
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0004F272 File Offset: 0x0004D472
		public DoubleControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatDouble;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004F28A File Offset: 0x0004D48A
		public unsafe override double ReadUnprocessedValueFromState(void* statePtr)
		{
			return this.m_StateBlock.ReadDouble(statePtr);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004F298 File Offset: 0x0004D498
		public unsafe override void WriteValueIntoState(double value, void* statePtr)
		{
			this.m_StateBlock.WriteDouble(statePtr, value);
		}
	}
}
