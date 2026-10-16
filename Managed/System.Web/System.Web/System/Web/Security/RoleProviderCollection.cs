using System;
using System.Configuration.Provider;

namespace System.Web.Security
{
	/// <summary>A collection of objects that inherit the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</summary>
	// Token: 0x020004C6 RID: 1222
	public sealed class RoleProviderCollection : ProviderCollection
	{
		/// <summary>Adds a role provider to the collection.</summary>
		/// <param name="provider">The role provider to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not of a type that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</exception>
		// Token: 0x0600373E RID: 14142 RVA: 0x0008E688 File Offset: 0x0008C888
		public override void Add(ProviderBase provider)
		{
			if (provider is RoleProvider)
			{
				base.Add(provider);
				return;
			}
			throw new HttpException();
		}

		/// <summary>Gets the role provider in the collection referenced by the specified provider name.</summary>
		/// <returns>An object that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</returns>
		/// <param name="name">The name of the role provider.</param>
		// Token: 0x17001162 RID: 4450
		public RoleProvider this[string name]
		{
			get
			{
				return (RoleProvider)base[name];
			}
		}

		/// <summary>Copies the role provider collection to a one-dimensional array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Security.RoleProviderCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003740 RID: 14144 RVA: 0x0008E6B0 File Offset: 0x0008C8B0
		public void CopyTo(RoleProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
