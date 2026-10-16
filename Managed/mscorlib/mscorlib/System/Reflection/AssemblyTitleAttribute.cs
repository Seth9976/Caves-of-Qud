using System;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x0200088F RID: 2191
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title. </param>
		// Token: 0x06004877 RID: 18551 RVA: 0x000EE1D5 File Offset: 0x000EC3D5
		public AssemblyTitleAttribute(string title)
		{
			this.Title = title;
		}

		/// <summary>Gets assembly title information.</summary>
		/// <returns>The assembly title. </returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x000EE1E4 File Offset: 0x000EC3E4
		public string Title { get; }
	}
}
