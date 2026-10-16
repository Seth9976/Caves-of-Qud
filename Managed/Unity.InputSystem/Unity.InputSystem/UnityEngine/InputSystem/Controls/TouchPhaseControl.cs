using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000121 RID: 289
	[InputControlLayout(hideInUI = true)]
	public class TouchPhaseControl : InputControl<TouchPhase>
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x0004FCF6 File Offset: 0x0004DEF6
		public TouchPhaseControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004FD10 File Offset: 0x0004DF10
		public unsafe override TouchPhase ReadUnprocessedValueFromState(void* statePtr)
		{
			return (TouchPhase)base.stateBlock.ReadInt(statePtr);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0004FD2C File Offset: 0x0004DF2C
		public unsafe override void WriteValueIntoState(TouchPhase value, void* statePtr)
		{
			*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = (int)value;
		}
	}
}
