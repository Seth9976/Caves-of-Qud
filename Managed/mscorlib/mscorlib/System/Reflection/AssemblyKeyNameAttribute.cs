using System;

namespace System.Reflection
{
	/// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
	// Token: 0x0200088A RID: 2186
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> class with the name of the container holding the key pair used to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyName">The name of the container containing the key pair. </param>
		// Token: 0x0600486D RID: 18541 RVA: 0x000EE15B File Offset: 0x000EC35B
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.KeyName = keyName;
		}

		/// <summary>Gets the name of the container having the key pair that is used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the container that has the relevant key pair.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x000EE16A File Offset: 0x000EC36A
		public string KeyName { get; }
	}
}
