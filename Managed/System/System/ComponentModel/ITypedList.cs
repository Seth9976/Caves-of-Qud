using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to discover the schema for a bindable list, where the properties available for binding differ from the public properties of the object to bind to. </summary>
	// Token: 0x020003C0 RID: 960
	public interface ITypedList
	{
		/// <summary>Returns the name of the list.</summary>
		/// <returns>The name of the list.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be null. </param>
		// Token: 0x06001F2A RID: 7978
		string GetListName(PropertyDescriptor[] listAccessors);

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the collection as bindable. This can be null. </param>
		// Token: 0x06001F2B RID: 7979
		PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
	}
}
