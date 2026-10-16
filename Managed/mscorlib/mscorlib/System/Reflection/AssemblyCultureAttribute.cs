using System;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x02000882 RID: 2178
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly. </param>
		// Token: 0x0600485A RID: 18522 RVA: 0x000EE095 File Offset: 0x000EC295
		public AssemblyCultureAttribute(string culture)
		{
			this.Culture = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x000EE0A4 File Offset: 0x000EC2A4
		public string Culture { get; }
	}
}
