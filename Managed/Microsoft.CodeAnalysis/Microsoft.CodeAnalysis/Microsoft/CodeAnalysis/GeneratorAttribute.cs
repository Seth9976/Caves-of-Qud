using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DB RID: 475
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(4)]
	public sealed class GeneratorAttribute : Attribute
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0003DDD4 File Offset: 0x0003BFD4
		public string[] Languages { get; }

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		public GeneratorAttribute()
			: this("C#", Array.Empty<string>())
		{
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		public GeneratorAttribute(string firstLanguage, params string[] additionalLanguages)
		{
			if (firstLanguage == null)
			{
				throw new ArgumentNullException("firstLanguage");
			}
			if (additionalLanguages == null)
			{
				throw new ArgumentNullException("additionalLanguages");
			}
			string[] array = new string[additionalLanguages.Length + 1];
			array[0] = firstLanguage;
			for (int i = 0; i < additionalLanguages.Length; i++)
			{
				array[i + 1] = additionalLanguages[i];
			}
			this.Languages = array;
		}
	}
}
