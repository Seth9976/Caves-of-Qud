using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides access to an application domain.</summary>
	// Token: 0x0200075B RID: 1883
	public class AppDomainInfoEnum : IAppDomainInfoEnum
	{
		// Token: 0x06004CE8 RID: 19688 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal AppDomainInfoEnum()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of application domains.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x06004CE9 RID: 19689 RVA: 0x000C8868 File Offset: 0x000C6A68
		public int Count()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets an <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>An <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</returns>
		// Token: 0x06004CEA RID: 19690 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IAppDomainInfo GetData()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Moves to the next <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>Returns true if a new interface is available; otherwise returns false.</returns>
		// Token: 0x06004CEB RID: 19691 RVA: 0x000C8884 File Offset: 0x000C6A84
		public bool MoveNext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		// Token: 0x06004CEC RID: 19692 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Reset()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
