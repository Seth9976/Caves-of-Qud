using System;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Determines the permission flags that apply to a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x0200087F RID: 2175
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x060044CB RID: 17611 RVA: 0x00003917 File Offset: 0x00001B17
		public TypeDescriptorPermissionAttribute(SecurityAction action)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />. </summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x000ED254 File Offset: 0x000EB454
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x00003917 File Offset: 0x00001B17
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the type descriptor can be accessed from partial trust. </summary>
		/// <returns>true if the type descriptor can be accessed from partial trust; otherwise, false. </returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x000ED270 File Offset: 0x000EB470
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x00003917 File Offset: 0x00001B17
		public bool RestrictedRegistrationAccess
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <returns>A serializable permission object.</returns>
		// Token: 0x060044D0 RID: 17616 RVA: 0x00032868 File Offset: 0x00030A68
		public override IPermission CreatePermission()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
