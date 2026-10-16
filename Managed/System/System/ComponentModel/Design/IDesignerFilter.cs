using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface that enables a designer to access and filter the dictionaries of a <see cref="T:System.ComponentModel.TypeDescriptor" /> that stores the property, attribute, and event descriptors that a component designer can expose to the design-time environment.</summary>
	// Token: 0x0200045E RID: 1118
	public interface IDesignerFilter
	{
		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes. </param>
		// Token: 0x06002425 RID: 9253
		void PostFilterAttributes(IDictionary attributes);

		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names. </param>
		// Token: 0x06002426 RID: 9254
		void PostFilterEvents(IDictionary events);

		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names. </param>
		// Token: 0x06002427 RID: 9255
		void PostFilterProperties(IDictionary properties);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes. </param>
		// Token: 0x06002428 RID: 9256
		void PreFilterAttributes(IDictionary attributes);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names. </param>
		// Token: 0x06002429 RID: 9257
		void PreFilterEvents(IDictionary events);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names. </param>
		// Token: 0x0600242A RID: 9258
		void PreFilterProperties(IDictionary properties);
	}
}
