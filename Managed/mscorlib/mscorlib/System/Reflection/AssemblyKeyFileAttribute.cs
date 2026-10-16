using System;

namespace System.Reflection
{
	/// <summary>Specifies the name of a file containing the key pair used to generate a strong name.</summary>
	// Token: 0x02000889 RID: 2185
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyKeyFileAttribute class with the name of the file containing the key pair to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyFile">The name of the file containing the key pair. </param>
		// Token: 0x0600486B RID: 18539 RVA: 0x000EE144 File Offset: 0x000EC344
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.KeyFile = keyFile;
		}

		/// <summary>Gets the name of the file containing the key pair used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the file that contains the key pair.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x000EE153 File Offset: 0x000EC353
		public string KeyFile { get; }
	}
}
