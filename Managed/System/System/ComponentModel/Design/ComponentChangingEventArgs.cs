using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000445 RID: 1093
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x00081127 File Offset: 0x0007F327
		public object Component { get; }

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or null otherwise.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0008112F File Offset: 0x0007F32F
		public MemberDescriptor Member { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed. </param>
		// Token: 0x060023A0 RID: 9120 RVA: 0x00081137 File Offset: 0x0007F337
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.Component = component;
			this.Member = member;
		}
	}
}
