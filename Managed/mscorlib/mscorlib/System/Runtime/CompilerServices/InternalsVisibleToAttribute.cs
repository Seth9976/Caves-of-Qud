using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that types that are ordinarily visible only within the current assembly are visible to a specified assembly.</summary>
	// Token: 0x02000836 RID: 2102
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> class with the name of the specified friend assembly. </summary>
		/// <param name="assemblyName">The name of a friend assembly.</param>
		// Token: 0x060046BB RID: 18107 RVA: 0x000E718E File Offset: 0x000E538E
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		/// <summary>Gets the name of the friend assembly to which all types and type members that are marked with the internal keyword are to be made visible. </summary>
		/// <returns>A string that represents the name of the friend assembly.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x000E71A4 File Offset: 0x000E53A4
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		/// <summary>This property is not implemented.</summary>
		/// <returns>This property does not return a value.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x000E71AC File Offset: 0x000E53AC
		// (set) Token: 0x060046BE RID: 18110 RVA: 0x000E71B4 File Offset: 0x000E53B4
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04002D7C RID: 11644
		private string _assemblyName;

		// Token: 0x04002D7D RID: 11645
		private bool _allInternalsVisible = true;
	}
}
