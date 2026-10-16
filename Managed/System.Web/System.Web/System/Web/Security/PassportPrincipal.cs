using System;
using System.Security.Principal;
using Unity;

namespace System.Web.Security
{
	/// <summary>Represents a Passport-authenticated principal. This class is deprecated.</summary>
	// Token: 0x020006E6 RID: 1766
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportPrincipal : GenericPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportPrincipal" /> class from a <see cref="T:System.Web.Security.PassportIdentity" /> instance and an array of role names to which the user represented by that <see cref="T:System.Web.Security.PassportIdentity" /> belongs. This class is deprecated.</summary>
		/// <param name="identity">An implementation of the <see cref="T:System.Security.Principal.IIdentity" /> interface that represents the user.</param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06004AB3 RID: 19123 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PassportPrincipal(PassportIdentity identity, string[] roles)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
