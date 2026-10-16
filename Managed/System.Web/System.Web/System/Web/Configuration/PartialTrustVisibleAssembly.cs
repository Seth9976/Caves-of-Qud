using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures a partial-trust assembly. This class cannot be inherited.</summary>
	// Token: 0x020006A9 RID: 1705
	public sealed class PartialTrustVisibleAssembly : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> class. </summary>
		/// <param name="assemblyName">The assembly name.</param>
		/// <param name="publicKey">The hexadecimal string representation of the public key that is associated with the assembly.</param>
		// Token: 0x060047F7 RID: 18423 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PartialTrustVisibleAssembly(string assemblyName, string publicKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the assembly name.</summary>
		/// <returns>The assembly name. </returns>
		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060047F9 RID: 18425 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string AssemblyName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the assembly public key.</summary>
		/// <returns>The hexadecimal string representation of the public key that is associated with the assembly.</returns>
		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060047FB RID: 18427 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string PublicKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
