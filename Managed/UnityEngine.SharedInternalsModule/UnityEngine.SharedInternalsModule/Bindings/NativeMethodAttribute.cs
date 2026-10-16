using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeMethodAttribute : Attribute, IBindingsNameProviderAttribute, IBindingsAttribute, IBindingsIsThreadSafeProviderAttribute, IBindingsIsFreeFunctionProviderAttribute, IBindingsThrowsProviderAttribute
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002283 File Offset: 0x00000483
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000228B File Offset: 0x0000048B
		public string Name { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002294 File Offset: 0x00000494
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000229C File Offset: 0x0000049C
		public bool IsThreadSafe { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000022A5 File Offset: 0x000004A5
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000022AD File Offset: 0x000004AD
		public bool IsFreeFunction { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000022B6 File Offset: 0x000004B6
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000022BE File Offset: 0x000004BE
		public bool ThrowsException { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000022C7 File Offset: 0x000004C7
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000022CF File Offset: 0x000004CF
		public bool HasExplicitThis { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002078 File Offset: 0x00000278
		public NativeMethodAttribute()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000022D8 File Offset: 0x000004D8
		public NativeMethodAttribute(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name == "";
			if (flag2)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			this.Name = name;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002327 File Offset: 0x00000527
		public NativeMethodAttribute(string name, bool isFreeFunction)
			: this(name)
		{
			this.IsFreeFunction = isFreeFunction;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000233A File Offset: 0x0000053A
		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe)
			: this(name, isFreeFunction)
		{
			this.IsThreadSafe = isThreadSafe;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000234E File Offset: 0x0000054E
		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe, bool throws)
			: this(name, isFreeFunction, isThreadSafe)
		{
			this.ThrowsException = throws;
		}
	}
}
