using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited. </summary>
	// Token: 0x0200082B RID: 2091
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value. </summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x060046AD RID: 18093 RVA: 0x000E7141 File Offset: 0x000E5341
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		/// <summary>Gets the value of the dependent assembly. </summary>
		/// <returns>The name of the dependent assembly.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x000E7157 File Offset: 0x000E5357
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly is to load a dependency. </summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060046AF RID: 18095 RVA: 0x000E715F File Offset: 0x000E535F
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002D77 RID: 11639
		private string dependentAssembly;

		// Token: 0x04002D78 RID: 11640
		private LoadHint loadHint;
	}
}
