using System;
using System.IO;
using System.Reflection;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008A RID: 138
	public class TestAssembly : TestSuite
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0000C31A File Offset: 0x0000A51A
		public TestAssembly(Assembly assembly, string path)
			: base(path)
		{
			this.Assembly = assembly;
			base.Name = Path.GetFileName(path);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000C336 File Offset: 0x0000A536
		public TestAssembly(string path)
			: base(path)
		{
			base.Name = Path.GetFileName(path);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000C34B File Offset: 0x0000A54B
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0000C353 File Offset: 0x0000A553
		public Assembly Assembly { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000C35C File Offset: 0x0000A55C
		public override string TestType
		{
			get
			{
				return "Assembly";
			}
		}
	}
}
