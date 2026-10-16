using System;

namespace System.Web.Util
{
	// Token: 0x02000145 RID: 325
	internal sealed class SimpleWebObjectFactory : IWebObjectFactory
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x00027CBC File Offset: 0x00025EBC
		public SimpleWebObjectFactory(Type type)
		{
			this.type = type;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00027CCB File Offset: 0x00025ECB
		public object CreateInstance()
		{
			if (this.type == null)
			{
				return null;
			}
			return Activator.CreateInstance(this.type);
		}

		// Token: 0x04001217 RID: 4631
		private Type type;
	}
}
