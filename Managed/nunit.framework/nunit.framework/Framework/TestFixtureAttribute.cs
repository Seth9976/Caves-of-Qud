using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class TestFixtureAttribute : NUnitAttribute, IFixtureBuilder, ITestFixtureData, ITestData
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00005849 File Offset: 0x00003A49
		public TestFixtureAttribute()
			: this(new object[0])
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005857 File Offset: 0x00003A57
		public TestFixtureAttribute(params object[] arguments)
		{
			this.RunState = RunState.Runnable;
			this.Arguments = arguments;
			this.TypeArgs = new Type[0];
			this.Properties = new PropertyBag();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000588F File Offset: 0x00003A8F
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005897 File Offset: 0x00003A97
		public string TestName { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000058A0 File Offset: 0x00003AA0
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000058A8 File Offset: 0x00003AA8
		public RunState RunState { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000058B1 File Offset: 0x00003AB1
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000058B9 File Offset: 0x00003AB9
		public object[] Arguments { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000058C2 File Offset: 0x00003AC2
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000058CA File Offset: 0x00003ACA
		public IPropertyBag Properties { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000058D3 File Offset: 0x00003AD3
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000058DB File Offset: 0x00003ADB
		public Type[] TypeArgs { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000058E4 File Offset: 0x00003AE4
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000058FB File Offset: 0x00003AFB
		public string Description
		{
			get
			{
				return this.Properties.Get("Description") as string;
			}
			set
			{
				this.Properties.Set("Description", value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000590E File Offset: 0x00003B0E
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00005925 File Offset: 0x00003B25
		public string Author
		{
			get
			{
				return this.Properties.Get("Author") as string;
			}
			set
			{
				this.Properties.Set("Author", value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00005938 File Offset: 0x00003B38
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00005940 File Offset: 0x00003B40
		public Type TestOf
		{
			get
			{
				return this._testOf;
			}
			set
			{
				this._testOf = value;
				this.Properties.Set("TestOf", value.FullName);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000595F File Offset: 0x00003B5F
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00005967 File Offset: 0x00003B67
		public string Ignore
		{
			get
			{
				return this.IgnoreReason;
			}
			set
			{
				this.IgnoreReason = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005970 File Offset: 0x00003B70
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00005987 File Offset: 0x00003B87
		public string Reason
		{
			get
			{
				return this.Properties.Get("_SKIPREASON") as string;
			}
			set
			{
				this.Properties.Set("_SKIPREASON", value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000599A File Offset: 0x00003B9A
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000059A2 File Offset: 0x00003BA2
		public string IgnoreReason
		{
			get
			{
				return this.Reason;
			}
			set
			{
				this.RunState = RunState.Ignored;
				this.Reason = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000059B2 File Offset: 0x00003BB2
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000059BD File Offset: 0x00003BBD
		public bool Explicit
		{
			get
			{
				return this.RunState == RunState.Explicit;
			}
			set
			{
				this.RunState = (value ? RunState.Explicit : RunState.Runnable);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000059CC File Offset: 0x00003BCC
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00005A78 File Offset: 0x00003C78
		public string Category
		{
			get
			{
				IList list = this.Properties["Category"];
				if (list == null)
				{
					return null;
				}
				int count = list.Count;
				if (count == 0)
				{
					return null;
				}
				if (count != 1)
				{
					string[] array = new string[list.Count];
					int num = 0;
					foreach (object obj in list)
					{
						string text = (string)obj;
						array[num++] = text;
					}
					return string.Join(",", array);
				}
				return list[0] as string;
			}
			set
			{
				foreach (string text in value.Split(new char[] { ',' }))
				{
					this.Properties.Add("Category", text);
				}
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005ABA File Offset: 0x00003CBA
		public IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo)
		{
			yield return this._builder.BuildFrom(typeInfo, this);
			yield break;
		}

		// Token: 0x0400004B RID: 75
		private readonly NUnitTestFixtureBuilder _builder = new NUnitTestFixtureBuilder();

		// Token: 0x04000051 RID: 81
		private Type _testOf;
	}
}
