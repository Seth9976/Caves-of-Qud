using System;

namespace NUnit.Framework
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class AuthorAttribute : PropertyAttribute
	{
		// Token: 0x06000126 RID: 294 RVA: 0x000039E0 File Offset: 0x00001BE0
		public AuthorAttribute(string name)
			: base("Author", name)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000039EE File Offset: 0x00001BEE
		public AuthorAttribute(string name, string email)
			: base("Author", string.Format("{0} <{1}>", name, email))
		{
		}
	}
}
