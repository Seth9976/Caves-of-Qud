using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000084 RID: 132
	public abstract class TestParameters : ITestData, IApplyToTest
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000B970 File Offset: 0x00009B70
		public TestParameters()
		{
			this.RunState = RunState.Runnable;
			this.Properties = new PropertyBag();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000B98A File Offset: 0x00009B8A
		public TestParameters(object[] args)
		{
			this.RunState = RunState.Runnable;
			this.InitializeAguments(args);
			this.Properties = new PropertyBag();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public TestParameters(Exception exception)
		{
			this.RunState = RunState.NotRunnable;
			this.Properties = new PropertyBag();
			this.Properties.Set("_SKIPREASON", ExceptionHelper.BuildMessage(exception));
			this.Properties.Set("_PROVIDERSTACKTRACE", ExceptionHelper.BuildStackTrace(exception));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000BA00 File Offset: 0x00009C00
		public TestParameters(ITestData data)
		{
			this.RunState = data.RunState;
			this.Properties = new PropertyBag();
			this.TestName = data.TestName;
			this.InitializeAguments(data.Arguments);
			foreach (string text in data.Properties.Keys)
			{
				this.Properties[text] = data.Properties[text];
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000BA98 File Offset: 0x00009C98
		private void InitializeAguments(object[] args)
		{
			this.OriginalArguments = args;
			int num = args.Length;
			this.Arguments = new object[num];
			Array.Copy(args, this.Arguments, num);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000BAC9 File Offset: 0x00009CC9
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		public RunState RunState { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000BADA File Offset: 0x00009CDA
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		public object[] Arguments { get; internal set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000BAEB File Offset: 0x00009CEB
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0000BAF3 File Offset: 0x00009CF3
		public string TestName { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000BAFC File Offset: 0x00009CFC
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0000BB04 File Offset: 0x00009D04
		public IPropertyBag Properties { get; private set; }

		// Token: 0x06000521 RID: 1313 RVA: 0x0000BB10 File Offset: 0x00009D10
		public void ApplyToTest(Test test)
		{
			if (this.RunState != RunState.Runnable)
			{
				test.RunState = this.RunState;
			}
			foreach (string text in this.Properties.Keys)
			{
				foreach (object obj in this.Properties[text])
				{
					test.Properties.Add(text, obj);
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public object[] OriginalArguments { get; private set; }
	}
}
