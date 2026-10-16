using System;
using System.Reflection;

namespace System.Web
{
	// Token: 0x020000C6 RID: 198
	internal sealed class NoParamsInvoker
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public NoParamsInvoker(object o, MethodInfo method)
		{
			if (method.IsStatic)
			{
				this.real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), method);
			}
			else
			{
				this.real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), o, method);
			}
			this.faked = new EventHandler(this.InvokeNoParams);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001C356 File Offset: 0x0001A556
		private void InvokeNoParams(object o, EventArgs args)
		{
			this.real();
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001C363 File Offset: 0x0001A563
		public EventHandler FakeDelegate
		{
			get
			{
				return this.faked;
			}
		}

		// Token: 0x0400106E RID: 4206
		private EventHandler faked;

		// Token: 0x0400106F RID: 4207
		private NoParamsDelegate real;
	}
}
