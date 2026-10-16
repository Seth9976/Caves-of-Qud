using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the metadata class to associate with a data model class.</summary>
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class MetadataTypeAttribute : Attribute
	{
		/// <summary>Gets the metadata class that is associated with a data-model partial class.</summary>
		/// <returns>The type value that represents the metadata class.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000039B1 File Offset: 0x00001BB1
		public Type MetadataClassType
		{
			get
			{
				if (this._metadataClassType == null)
				{
					throw new InvalidOperationException("MetadataClassType cannot be null.");
				}
				return this._metadataClassType;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MetadataTypeAttribute" /> class.</summary>
		/// <param name="metadataClassType">The metadata class to reference.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="metadataClassType" /> is null. </exception>
		// Token: 0x060000C6 RID: 198 RVA: 0x000039D2 File Offset: 0x00001BD2
		public MetadataTypeAttribute(Type metadataClassType)
		{
			this._metadataClassType = metadataClassType;
		}

		// Token: 0x04000084 RID: 132
		private Type _metadataClassType;
	}
}
