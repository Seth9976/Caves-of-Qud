using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the type of resource provider factory for design time. This class cannot be inherited.</summary>
	// Token: 0x020005FE RID: 1534
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DesignTimeResourceProviderFactoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the qualified name of the specified factory type. </summary>
		/// <param name="factoryType">The type of the resource provider factory.</param>
		// Token: 0x06004263 RID: 16995 RVA: 0x000AD100 File Offset: 0x000AB300
		public DesignTimeResourceProviderFactoryAttribute(Type factoryType)
		{
			this._factoryTypeName = factoryType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the specified factory type name. </summary>
		/// <param name="factoryTypeName">The name of the resource provider factory type.</param>
		// Token: 0x06004264 RID: 16996 RVA: 0x000AD114 File Offset: 0x000AB314
		public DesignTimeResourceProviderFactoryAttribute(string factoryTypeName)
		{
			this._factoryTypeName = factoryTypeName;
		}

		/// <summary>Gets the value of the factory type name.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the factory type.</returns>
		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x000AD123 File Offset: 0x000AB323
		public string FactoryTypeName
		{
			get
			{
				return this._factoryTypeName;
			}
		}

		/// <summary>Determines whether the default provider is used.</summary>
		/// <returns>true if <see cref="P:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute.FactoryTypeName" /> equals null; otherwise, false.</returns>
		// Token: 0x06004266 RID: 16998 RVA: 0x000AD12B File Offset: 0x000AB32B
		public override bool IsDefaultAttribute()
		{
			return this._factoryTypeName == null;
		}

		// Token: 0x040023A5 RID: 9125
		private string _factoryTypeName;
	}
}
