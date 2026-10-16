using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	public readonly struct BindablePropertyChangedEventArgs
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000B2BC File Offset: 0x000094BC
		public BindablePropertyChangedEventArgs(in BindingId propertyName)
		{
			this.m_PropertyName = propertyName;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B2CA File Offset: 0x000094CA
		public BindingId propertyName
		{
			get
			{
				return this.m_PropertyName;
			}
		}

		// Token: 0x0400016C RID: 364
		private readonly BindingId m_PropertyName;
	}
}
