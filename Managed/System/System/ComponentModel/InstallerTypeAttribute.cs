using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the installer for a type that installs components.</summary>
	// Token: 0x020003C1 RID: 961
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class, when given a <see cref="T:System.Type" /> that represents the installer for a component.</summary>
		/// <param name="installerType">A <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06001F2C RID: 7980 RVA: 0x0006CC8F File Offset: 0x0006AE8F
		public InstallerTypeAttribute(Type installerType)
		{
			this._typeName = installerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class with the name of the component's installer type.</summary>
		/// <param name="typeName">The name of a <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06001F2D RID: 7981 RVA: 0x0006CCA3 File Offset: 0x0006AEA3
		public InstallerTypeAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		/// <summary>Gets the type of installer associated with this attribute.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of installer associated with this attribute, or null if an installer does not exist.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0006CCB2 File Offset: 0x0006AEB2
		public virtual Type InstallerType
		{
			get
			{
				return Type.GetType(this._typeName);
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001F2F RID: 7983 RVA: 0x0006CCC0 File Offset: 0x0006AEC0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			InstallerTypeAttribute installerTypeAttribute = obj as InstallerTypeAttribute;
			return installerTypeAttribute != null && installerTypeAttribute._typeName == this._typeName;
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</returns>
		// Token: 0x06001F30 RID: 7984 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000F6C RID: 3948
		private string _typeName;
	}
}
