using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001F RID: 31
	public readonly struct BindingActivationContext
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000362A File Offset: 0x0000182A
		public VisualElement targetElement
		{
			get
			{
				return this.m_TargetElement;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003632 File Offset: 0x00001832
		public BindingId bindingId
		{
			get
			{
				return this.m_BindingId;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000363A File Offset: 0x0000183A
		internal BindingActivationContext(VisualElement element, in BindingId property)
		{
			this.m_TargetElement = element;
			this.m_BindingId = property;
		}

		// Token: 0x0400003F RID: 63
		private readonly VisualElement m_TargetElement;

		// Token: 0x04000040 RID: 64
		private readonly BindingId m_BindingId;
	}
}
