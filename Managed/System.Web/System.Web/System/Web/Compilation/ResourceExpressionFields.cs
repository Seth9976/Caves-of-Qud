using System;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains the fields from a parsed resource expression.</summary>
	// Token: 0x02000661 RID: 1633
	public sealed class ResourceExpressionFields
	{
		// Token: 0x060045EA RID: 17898 RVA: 0x000BE7F5 File Offset: 0x000BC9F5
		internal ResourceExpressionFields(string classKey, string resourceKey)
		{
			this.classKey = classKey;
			this.resourceKey = resourceKey;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000BE80B File Offset: 0x000BCA0B
		internal ResourceExpressionFields(string resourceKey)
			: this(null, resourceKey)
		{
		}

		/// <summary>Gets the class key for a parsed resource expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the class key, or <see cref="F:System.String.Empty" /> if the class key has not been set.</returns>
		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x000BE815 File Offset: 0x000BCA15
		public string ClassKey
		{
			get
			{
				return this.classKey;
			}
		}

		/// <summary>Gets the resource key for a parsed resource expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the resource key, or <see cref="F:System.String.Empty" /> if the resource key has not been set.</returns>
		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x000BE81D File Offset: 0x000BCA1D
		public string ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ResourceExpressionFields()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002521 RID: 9505
		private string classKey;

		// Token: 0x04002522 RID: 9506
		private string resourceKey;
	}
}
