using System;
using System.Collections;
using Unity;

namespace System.Security
{
	/// <summary>Represents a read-only collection that can contain many different types of permissions.</summary>
	// Token: 0x02000BCD RID: 3021
	[Serializable]
	public sealed class ReadOnlyPermissionSet : PermissionSet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.ReadOnlyPermissionSet" /> class. </summary>
		/// <param name="permissionSetXml">The XML element from which to take the value of the new <see cref="T:System.Security.ReadOnlyPermissionSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionSetXml" /> is null.</exception>
		// Token: 0x06006B5B RID: 27483 RVA: 0x000173AD File Offset: 0x000155AD
		public ReadOnlyPermissionSet(SecurityElement permissionSetXml)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x00052955 File Offset: 0x00050B55
		protected override IPermission AddPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x00052955 File Offset: 0x00050B55
		protected override IEnumerator GetEnumeratorImpl()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x00052955 File Offset: 0x00050B55
		protected override IPermission GetPermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x00052955 File Offset: 0x00050B55
		protected override IPermission RemovePermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x00052955 File Offset: 0x00050B55
		protected override IPermission SetPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
