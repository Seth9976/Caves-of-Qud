using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a service for registering event handlers for component events.</summary>
	// Token: 0x02000463 RID: 1123
	public interface IEventBindingService
	{
		/// <summary>Creates a unique name for an event-handler method for the specified component and event.</summary>
		/// <returns>The recommended name for the event-handler method for this event.</returns>
		/// <param name="component">The component instance the event is connected to. </param>
		/// <param name="e">The event to create a name for. </param>
		// Token: 0x0600244D RID: 9293
		string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		/// <summary>Gets a collection of event-handler methods that have a method signature compatible with the specified event.</summary>
		/// <returns>A collection of strings.</returns>
		/// <param name="e">The event to get the compatible event-handler methods for. </param>
		// Token: 0x0600244E RID: 9294
		ICollection GetCompatibleMethods(EventDescriptor e);

		/// <summary>Gets an <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the specified property descriptor represents, if it represents an event.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the property represents, or null if the property does not represent an event.</returns>
		/// <param name="property">The property that represents an event. </param>
		// Token: 0x0600244F RID: 9295
		EventDescriptor GetEvent(PropertyDescriptor property);

		/// <summary>Converts a set of event descriptors to a set of property descriptors.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that describe the event set.</returns>
		/// <param name="events">The events to convert to properties. </param>
		// Token: 0x06002450 RID: 9296
		PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events);

		/// <summary>Converts a single event descriptor to a property descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the event.</returns>
		/// <param name="e">The event to convert. </param>
		// Token: 0x06002451 RID: 9297
		PropertyDescriptor GetEventProperty(EventDescriptor e);

		/// <summary>Displays the user code for the designer.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		// Token: 0x06002452 RID: 9298
		bool ShowCode();

		/// <summary>Displays the user code for the designer at the specified line.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="lineNumber">The line number to place the caret on. </param>
		// Token: 0x06002453 RID: 9299
		bool ShowCode(int lineNumber);

		/// <summary>Displays the user code for the specified event.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="component">The component that the event is connected to. </param>
		/// <param name="e">The event to display. </param>
		// Token: 0x06002454 RID: 9300
		bool ShowCode(IComponent component, EventDescriptor e);
	}
}
