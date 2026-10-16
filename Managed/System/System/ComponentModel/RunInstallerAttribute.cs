using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed.</summary>
	// Token: 0x020003EC RID: 1004
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">true if an installer should be invoked during installation of an assembly; otherwise, false. </param>
		// Token: 0x060020D9 RID: 8409 RVA: 0x0007174E File Offset: 0x0006F94E
		public RunInstallerAttribute(bool runInstaller)
		{
			this.RunInstaller = runInstaller;
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>true if an installer should be invoked during installation of an assembly; otherwise, false.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0007175D File Offset: 0x0006F95D
		public bool RunInstaller { get; }

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x060020DB RID: 8411 RVA: 0x00071768 File Offset: 0x0006F968
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.RunInstaller;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x060020DC RID: 8412 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x060020DD RID: 8413 RVA: 0x00071795 File Offset: 0x0006F995
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x04001005 RID: 4101
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should not be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x04001006 RID: 4102
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04001007 RID: 4103
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}
