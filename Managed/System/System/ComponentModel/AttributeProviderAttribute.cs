using System;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x02000382 RID: 898
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x06001D77 RID: 7543 RVA: 0x00068EFC File Offset: 0x000670FC
		public AttributeProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.TypeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name and property name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <param name="propertyName">The name of the property for which attributes will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is null.</exception>
		// Token: 0x06001D78 RID: 7544 RVA: 0x00068F19 File Offset: 0x00067119
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			this.TypeName = typeName;
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type.</summary>
		/// <param name="type">The type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06001D79 RID: 7545 RVA: 0x00068F4B File Offset: 0x0006714B
		public AttributeProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.TypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00068F73 File Offset: 0x00067173
		public string TypeName { get; }

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00068F7B File Offset: 0x0006717B
		public string PropertyName { get; }
	}
}
