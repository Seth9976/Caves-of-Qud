using System;

namespace System.ComponentModel
{
	/// <summary>Provides a top-level mapping layer between a COM object and a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020003B5 RID: 949
	[Obsolete("This interface has been deprecated. Add a TypeDescriptionProvider to handle type TypeDescriptor.ComObjectType instead.  https://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IComNativeDescriptorHandler
	{
		/// <summary>Gets the attributes for the specified component.</summary>
		/// <returns>A collection of attributes for <paramref name="component" />.</returns>
		/// <param name="component">The component to get attributes for.</param>
		// Token: 0x06001F00 RID: 7936
		AttributeCollection GetAttributes(object component);

		/// <summary>Gets the class name for the specified component.</summary>
		/// <returns>The name of the class that corresponds with <paramref name="component" />.</returns>
		/// <param name="component">The component to get the class name for.</param>
		// Token: 0x06001F01 RID: 7937
		string GetClassName(object component);

		/// <summary>Gets the type converter for the specified component.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.TypeConverter" /> for <paramref name="component" />.</returns>
		/// <param name="component">The component to get the <see cref="T:System.ComponentModel.TypeConverter" /> for.</param>
		// Token: 0x06001F02 RID: 7938
		TypeConverter GetConverter(object component);

		/// <summary>Gets the default event for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents <paramref name="component" />'s default event.</returns>
		/// <param name="component">The component to get the default event for.</param>
		// Token: 0x06001F03 RID: 7939
		EventDescriptor GetDefaultEvent(object component);

		/// <summary>Gets the default property for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents <paramref name="component" />'s default property.</returns>
		/// <param name="component">The component to get the default property for.</param>
		// Token: 0x06001F04 RID: 7940
		PropertyDescriptor GetDefaultProperty(object component);

		/// <summary>Gets the editor for the specified component.</summary>
		/// <returns>The editor for <paramref name="component" />. </returns>
		/// <param name="component">The component to get the editor for.</param>
		/// <param name="baseEditorType">The base type of the editor for <paramref name="component" />.</param>
		// Token: 0x06001F05 RID: 7941
		object GetEditor(object component, Type baseEditorType);

		/// <summary>Gets the name of the specified component.</summary>
		/// <returns>The name of <paramref name="component" />.</returns>
		/// <param name="component">The component to get the name of.</param>
		// Token: 0x06001F06 RID: 7942
		string GetName(object component);

		/// <summary>Gets the events for the specified component.</summary>
		/// <returns>A collection of event descriptors for <paramref name="component" />. </returns>
		/// <param name="component">The component to get events for.</param>
		// Token: 0x06001F07 RID: 7943
		EventDescriptorCollection GetEvents(object component);

		/// <summary>Gets the events with the specified attributes for the specified component.</summary>
		/// <returns>A collection of event descriptors for <paramref name="component" />. </returns>
		/// <param name="component">The component to get events for.</param>
		/// <param name="attributes">The attributes used to filter events. </param>
		// Token: 0x06001F08 RID: 7944
		EventDescriptorCollection GetEvents(object component, Attribute[] attributes);

		/// <summary>Gets the properties with the specified attributes for the specified component.</summary>
		/// <returns>A collection of property descriptors for <paramref name="component" />.</returns>
		/// <param name="component">The component to get events for.</param>
		/// <param name="attributes">The attributes used to filter properties.</param>
		// Token: 0x06001F09 RID: 7945
		PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		/// <summary>Gets the value of the property that has the specified name.</summary>
		/// <returns>The value of the property that has the specified name.</returns>
		/// <param name="component">The object to which the property belongs.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="success">A <see cref="T:System.Boolean" />, passed by reference, that represents whether the property was retrieved. </param>
		// Token: 0x06001F0A RID: 7946
		object GetPropertyValue(object component, string propertyName, ref bool success);

		/// <summary>Gets the value of the property that has the specified dispatch identifier.</summary>
		/// <returns>The value of the property that has the specified dispatch identifier.</returns>
		/// <param name="component">The object to which the property belongs.</param>
		/// <param name="dispid">The dispatch identifier.</param>
		/// <param name="success">A <see cref="T:System.Boolean" />, passed by reference, that represents whether the property was retrieved. </param>
		// Token: 0x06001F0B RID: 7947
		object GetPropertyValue(object component, int dispid, ref bool success);
	}
}
