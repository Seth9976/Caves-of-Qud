using System;
using System.Diagnostics;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005D RID: 93
	[DebuggerDisplay("Value = {Get()}")]
	public class InputValue
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00034AE8 File Offset: 0x00032CE8
		public object Get()
		{
			return this.m_Context.Value.ReadValueAsObject();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00034B08 File Offset: 0x00032D08
		public TValue Get<TValue>() where TValue : struct
		{
			if (this.m_Context == null)
			{
				throw new InvalidOperationException("Values can only be retrieved while in message callbacks");
			}
			return this.m_Context.Value.ReadValue<TValue>();
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00034B40 File Offset: 0x00032D40
		public bool isPressed
		{
			get
			{
				return this.Get<float>() >= ButtonControl.s_GlobalDefaultButtonPressPoint;
			}
		}

		// Token: 0x040002F4 RID: 756
		internal InputAction.CallbackContext? m_Context;
	}
}
