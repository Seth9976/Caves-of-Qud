using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000115 RID: 277
	[InputControlLayout(hideInUI = true)]
	public class AnyKeyControl : ButtonControl
	{
		// Token: 0x06001019 RID: 4121 RVA: 0x0004EA37 File Offset: 0x0004CC37
		public AnyKeyControl()
		{
			this.m_StateBlock.sizeInBits = 1U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004EA5B File Offset: 0x0004CC5B
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			if (!this.CheckStateIsAtDefault(statePtr, null))
			{
				return 1f;
			}
			return 0f;
		}
	}
}
