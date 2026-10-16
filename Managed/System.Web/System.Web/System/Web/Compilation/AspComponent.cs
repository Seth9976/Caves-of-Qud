using System;

namespace System.Web.Compilation
{
	// Token: 0x02000614 RID: 1556
	internal sealed class AspComponent
	{
		// Token: 0x060042EA RID: 17130 RVA: 0x000B06EE File Offset: 0x000AE8EE
		public AspComponent(Type type, string ns, string prefix, string source, bool fromConfig)
		{
			this.Type = type;
			this.Namespace = ns;
			this.Prefix = prefix;
			this.Source = source;
			this.FromConfig = fromConfig;
		}

		// Token: 0x040023E0 RID: 9184
		public readonly Type Type;

		// Token: 0x040023E1 RID: 9185
		public readonly string Prefix;

		// Token: 0x040023E2 RID: 9186
		public readonly string Source;

		// Token: 0x040023E3 RID: 9187
		public readonly bool FromConfig;

		// Token: 0x040023E4 RID: 9188
		public readonly string Namespace;
	}
}
