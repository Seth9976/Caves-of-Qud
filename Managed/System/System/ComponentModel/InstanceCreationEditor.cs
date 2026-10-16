using System;

namespace System.ComponentModel
{
	/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />. </summary>
	// Token: 0x020003C2 RID: 962
	public abstract class InstanceCreationEditor
	{
		/// <summary>Gets the specified text.</summary>
		/// <returns>The specified text.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0006CCF0 File Offset: 0x0006AEF0
		public virtual string Text
		{
			get
			{
				return "(New...)";
			}
		}

		/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
		/// <returns>An instance of the specified type or null.</returns>
		/// <param name="context">The context information.</param>
		/// <param name="instanceType">The specified type.</param>
		// Token: 0x06001F32 RID: 7986
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);
	}
}
