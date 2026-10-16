using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000042 RID: 66
	public readonly struct BindingInfo
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A78B File Offset: 0x0000898B
		public VisualElement targetElement { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A793 File Offset: 0x00008993
		public BindingId bindingId { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A79B File Offset: 0x0000899B
		public Binding binding { get; }

		// Token: 0x06000245 RID: 581 RVA: 0x0000A7A3 File Offset: 0x000089A3
		private BindingInfo(VisualElement targetElement, in BindingId bindingId, Binding binding)
		{
			this.targetElement = targetElement;
			this.bindingId = bindingId;
			this.binding = binding;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000A7C0 File Offset: 0x000089C0
		internal static BindingInfo FromRequest(VisualElement target, in PropertyPath targetPath, Binding binding)
		{
			BindingId bindingId = in targetPath;
			return new BindingInfo(target, in bindingId, binding);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000A7E4 File Offset: 0x000089E4
		internal static BindingInfo FromBindingData(in DataBindingManager.BindingData bindingData)
		{
			return new BindingInfo(bindingData.target.element, in bindingData.target.bindingId, bindingData.binding);
		}
	}
}
