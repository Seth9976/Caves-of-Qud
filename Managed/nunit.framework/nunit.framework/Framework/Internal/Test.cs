using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000089 RID: 137
	public abstract class Test : ITest, IXmlNodeBuilder, IComparable
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x0000BF25 File Offset: 0x0000A125
		protected Test(string name)
		{
			Guard.ArgumentNotNullOrEmpty(name, "name");
			this.Initialize(name);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000BF3F File Offset: 0x0000A13F
		protected Test(string pathName, string name)
		{
			Guard.ArgumentNotNullOrEmpty(pathName, "pathName");
			this.Initialize(name);
			this.FullName = pathName + "." + name;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000BF6C File Offset: 0x0000A16C
		protected Test(ITypeInfo typeInfo)
		{
			this.Initialize(typeInfo.GetDisplayName());
			string @namespace = typeInfo.Namespace;
			if (@namespace != null && @namespace != "")
			{
				this.FullName = @namespace + "." + this.Name;
			}
			this.TypeInfo = typeInfo;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		protected Test(IMethodInfo method)
		{
			this.Initialize(method.Name);
			this.Method = method;
			this.TypeInfo = method.TypeInfo;
			this.FullName = method.TypeInfo.FullName + "." + this.Name;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000C014 File Offset: 0x0000A214
		private void Initialize(string name)
		{
			this.Name = name;
			this.FullName = name;
			this.Id = Test.GetNextId();
			this.Properties = new PropertyBag();
			this.RunState = RunState.Runnable;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000C04E File Offset: 0x0000A24E
		internal void SetNextId()
		{
			this.Id = Test.GetNextId();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000C05C File Offset: 0x0000A25C
		private static string GetNextId()
		{
			return Test.IdPrefix + Test._nextID++.ToString();
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000C088 File Offset: 0x0000A288
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000C090 File Offset: 0x0000A290
		public string Id { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000C099 File Offset: 0x0000A299
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public string Name { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000C0AA File Offset: 0x0000A2AA
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
		public string FullName { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000C0BC File Offset: 0x0000A2BC
		public string ClassName
		{
			get
			{
				ITypeInfo typeInfo = this.TypeInfo;
				if (this.Method != null)
				{
					if (this.DeclaringTypeInfo == null)
					{
						this.DeclaringTypeInfo = new TypeWrapper(this.Method.MethodInfo.DeclaringType);
					}
					typeInfo = this.DeclaringTypeInfo;
				}
				if (typeInfo == null)
				{
					return null;
				}
				if (!typeInfo.IsGenericType)
				{
					return typeInfo.FullName;
				}
				return typeInfo.GetGenericTypeDefinition().FullName;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000020A6 File Offset: 0x000002A6
		public virtual string MethodName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000C121 File Offset: 0x0000A321
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000C129 File Offset: 0x0000A329
		public ITypeInfo TypeInfo { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000C132 File Offset: 0x0000A332
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000C13A File Offset: 0x0000A33A
		public IMethodInfo Method
		{
			get
			{
				return this._method;
			}
			set
			{
				this.DeclaringTypeInfo = null;
				this._method = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000C14A File Offset: 0x0000A34A
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000C152 File Offset: 0x0000A352
		public RunState RunState { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000545 RID: 1349
		public abstract string XmlElementName { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000C15B File Offset: 0x0000A35B
		public virtual string TestType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000C168 File Offset: 0x0000A368
		public virtual int TestCaseCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000C16B File Offset: 0x0000A36B
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000C173 File Offset: 0x0000A373
		public IPropertyBag Properties { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000C17C File Offset: 0x0000A37C
		public bool IsSuite
		{
			get
			{
				return this is TestSuite;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600054B RID: 1355
		public abstract bool HasChildren { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000C187 File Offset: 0x0000A387
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0000C18F File Offset: 0x0000A38F
		public ITest Parent { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600054E RID: 1358
		public abstract IList<ITest> Tests { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000C198 File Offset: 0x0000A398
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		public virtual object Fixture { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public static string IdPrefix { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public int Seed { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		internal bool RequiresThread { get; set; }

		// Token: 0x06000557 RID: 1367
		public abstract TestResult MakeTestResult();

		// Token: 0x06000558 RID: 1368 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public void ApplyAttributesToTest(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(typeof(IApplyToTest), true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				((IApplyToTest)customAttributes[i]).ApplyToTest(this);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000C218 File Offset: 0x0000A418
		protected void PopulateTestNode(TNode thisNode, bool recursive)
		{
			thisNode.AddAttribute("id", this.Id.ToString());
			thisNode.AddAttribute("name", this.Name);
			thisNode.AddAttribute("fullname", this.FullName);
			if (this.MethodName != null)
			{
				thisNode.AddAttribute("methodname", this.MethodName);
			}
			if (this.ClassName != null)
			{
				thisNode.AddAttribute("classname", this.ClassName);
			}
			thisNode.AddAttribute("runstate", this.RunState.ToString());
			if (this.Properties.Keys.Count > 0)
			{
				this.Properties.AddToXml(thisNode, recursive);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000C2CF File Offset: 0x0000A4CF
		public TNode ToXml(bool recursive)
		{
			return this.AddToXml(new TNode("dummy"), recursive);
		}

		// Token: 0x0600055B RID: 1371
		public abstract TNode AddToXml(TNode parentNode, bool recursive);

		// Token: 0x0600055C RID: 1372 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public int CompareTo(object obj)
		{
			Test test = obj as Test;
			if (test == null)
			{
				return -1;
			}
			return this.FullName.CompareTo(test.FullName);
		}

		// Token: 0x04000103 RID: 259
		private static int _nextID = 1000;

		// Token: 0x04000104 RID: 260
		protected MethodInfo[] setUpMethods;

		// Token: 0x04000105 RID: 261
		protected MethodInfo[] tearDownMethods;

		// Token: 0x04000106 RID: 262
		protected ITypeInfo DeclaringTypeInfo;

		// Token: 0x04000107 RID: 263
		private IMethodInfo _method;
	}
}
