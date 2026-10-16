using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x02000459 RID: 1113
	public interface IComponentChangeService
	{
		/// <summary>Occurs when a component has been added.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002404 RID: 9220
		// (remove) Token: 0x06002405 RID: 9221
		event ComponentEventHandler ComponentAdded;

		/// <summary>Occurs when a component is in the process of being added.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002406 RID: 9222
		// (remove) Token: 0x06002407 RID: 9223
		event ComponentEventHandler ComponentAdding;

		/// <summary>Occurs when a component has been changed.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002408 RID: 9224
		// (remove) Token: 0x06002409 RID: 9225
		event ComponentChangedEventHandler ComponentChanged;

		/// <summary>Occurs when a component is in the process of being changed.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600240A RID: 9226
		// (remove) Token: 0x0600240B RID: 9227
		event ComponentChangingEventHandler ComponentChanging;

		/// <summary>Occurs when a component has been removed.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600240C RID: 9228
		// (remove) Token: 0x0600240D RID: 9229
		event ComponentEventHandler ComponentRemoved;

		/// <summary>Occurs when a component is in the process of being removed.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600240E RID: 9230
		// (remove) Token: 0x0600240F RID: 9231
		event ComponentEventHandler ComponentRemoving;

		/// <summary>Occurs when a component is renamed.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002410 RID: 9232
		// (remove) Token: 0x06002411 RID: 9233
		event ComponentRenameEventHandler ComponentRename;

		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed. </param>
		/// <param name="member">The member that has changed. This is null if this change is not related to a single member. </param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not null. </param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not null. </param>
		// Token: 0x06002412 RID: 9234
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change. </param>
		/// <param name="member">The member that is changing. This is null if this change is not related to a single member. </param>
		// Token: 0x06002413 RID: 9235
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
