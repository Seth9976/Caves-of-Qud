using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> attribute is placed on a serializer to indicate the class to use as a default provider of that type of serializer. </summary>
	// Token: 0x02000485 RID: 1157
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the given provider type.</summary>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the serialization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerType" /> is null.</exception>
		// Token: 0x0600250F RID: 9487 RVA: 0x00082B69 File Offset: 0x00080D69
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.ProviderTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the named provider type.</summary>
		/// <param name="providerTypeName">The name of the serialization provider type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerTypeName" /> is null.</exception>
		// Token: 0x06002510 RID: 9488 RVA: 0x00082B91 File Offset: 0x00080D91
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.ProviderTypeName = providerTypeName;
		}

		/// <summary>Gets the type name of the serialization provider.</summary>
		/// <returns>A string containing the name of the provider.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x00082BAE File Offset: 0x00080DAE
		public string ProviderTypeName { get; }
	}
}
