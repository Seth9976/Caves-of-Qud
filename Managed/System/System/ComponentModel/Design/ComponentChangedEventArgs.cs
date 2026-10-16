using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000443 RID: 1091
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000810E2 File Offset: 0x0007F2E2
		public object Component { get; }

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000810EA File Offset: 0x0007F2EA
		public MemberDescriptor Member { get; }

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be null.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000810F2 File Offset: 0x0007F2F2
		public object NewValue { get; }

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be null.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x000810FA File Offset: 0x0007F2FA
		public object OldValue { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed. </param>
		/// <param name="oldValue">The old value of the changed member. </param>
		/// <param name="newValue">The new value of the changed member. </param>
		// Token: 0x06002399 RID: 9113 RVA: 0x00081102 File Offset: 0x0007F302
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.Component = component;
			this.Member = member;
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}
