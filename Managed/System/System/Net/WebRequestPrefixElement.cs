using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x020005F2 RID: 1522
	internal class WebRequestPrefixElement
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000A6B38 File Offset: 0x000A4D38
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x000A6BB8 File Offset: 0x000A4DB8
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000A6BC4 File Offset: 0x000A4DC4
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(SR.GetString("Invalid cast from {0} to {1}.", new object[] { creatorType.AssemblyQualifiedName, "IWebRequestCreate" }));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000A6C1E File Offset: 0x000A4E1E
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x04001BD8 RID: 7128
		public string Prefix;

		// Token: 0x04001BD9 RID: 7129
		internal IWebRequestCreate creator;

		// Token: 0x04001BDA RID: 7130
		internal Type creatorType;
	}
}
