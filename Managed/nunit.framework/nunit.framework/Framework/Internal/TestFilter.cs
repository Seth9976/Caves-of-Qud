using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Filters;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public abstract class TestFilter : ITestFilter, IXmlNodeBuilder
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000AFE3 File Offset: 0x000091E3
		public bool IsEmpty
		{
			get
			{
				return this is TestFilter.EmptyFilter;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000AFEE File Offset: 0x000091EE
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000AFF6 File Offset: 0x000091F6
		public bool TopLevel { get; set; }

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000AFFF File Offset: 0x000091FF
		public virtual bool Pass(ITest test)
		{
			return this.Match(test) || this.MatchParent(test) || this.MatchDescendant(test);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000B01C File Offset: 0x0000921C
		public virtual bool IsExplicitMatch(ITest test)
		{
			return this.Match(test) || this.MatchDescendant(test);
		}

		// Token: 0x060004FA RID: 1274
		public abstract bool Match(ITest test);

		// Token: 0x060004FB RID: 1275 RVA: 0x0000B030 File Offset: 0x00009230
		public bool MatchParent(ITest test)
		{
			return test.Parent != null && (this.Match(test.Parent) || this.MatchParent(test.Parent));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000B058 File Offset: 0x00009258
		protected virtual bool MatchDescendant(ITest test)
		{
			if (test.Tests == null)
			{
				return false;
			}
			foreach (ITest test2 in test.Tests)
			{
				if (this.Match(test2) || this.MatchDescendant(test2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public static TestFilter FromXml(string xmlText)
		{
			TNode tnode = TNode.FromXml(xmlText);
			if (tnode.Name != "filter")
			{
				throw new Exception("Expected filter element at top level");
			}
			int count = tnode.ChildNodes.Count;
			TestFilter testFilter = ((count == 0) ? TestFilter.Empty : ((count == 1) ? TestFilter.FromXml(tnode.FirstChild) : TestFilter.FromXml(tnode)));
			testFilter.TopLevel = true;
			return testFilter;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000B12C File Offset: 0x0000932C
		public static TestFilter FromXml(TNode node)
		{
			bool flag = node.Attributes["re"] == "1";
			string name = node.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 2:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c != 'o')
						{
							goto IL_030D;
						}
						if (!(name == "or"))
						{
							goto IL_030D;
						}
						OrFilter orFilter = new OrFilter();
						foreach (TNode tnode in node.ChildNodes)
						{
							orFilter.Add(TestFilter.FromXml(tnode));
						}
						return orFilter;
					}
					else
					{
						if (!(name == "id"))
						{
							goto IL_030D;
						}
						return new IdFilter(node.Value);
					}
					break;
				}
				case 3:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c != 'c')
						{
							if (c != 'n')
							{
								goto IL_030D;
							}
							if (!(name == "not"))
							{
								goto IL_030D;
							}
							return new NotFilter(TestFilter.FromXml(node.FirstChild));
						}
						else
						{
							if (!(name == "cat"))
							{
								goto IL_030D;
							}
							return new CategoryFilter(node.Value)
							{
								IsRegex = flag
							};
						}
					}
					else if (!(name == "and"))
					{
						goto IL_030D;
					}
					break;
				}
				case 4:
				{
					char c = name[0];
					if (c != 'n')
					{
						if (c != 'p')
						{
							if (c != 't')
							{
								goto IL_030D;
							}
							if (!(name == "test"))
							{
								goto IL_030D;
							}
							return new FullNameFilter(node.Value)
							{
								IsRegex = flag
							};
						}
						else
						{
							if (!(name == "prop"))
							{
								goto IL_030D;
							}
							string text = node.Attributes["name"];
							if (text != null)
							{
								return new PropertyFilter(text, node.Value)
								{
									IsRegex = flag
								};
							}
							goto IL_030D;
						}
					}
					else
					{
						if (!(name == "name"))
						{
							goto IL_030D;
						}
						return new TestNameFilter(node.Value)
						{
							IsRegex = flag
						};
					}
					break;
				}
				case 5:
					if (!(name == "class"))
					{
						goto IL_030D;
					}
					return new ClassNameFilter(node.Value)
					{
						IsRegex = flag
					};
				case 6:
				{
					char c = name[0];
					if (c != 'f')
					{
						if (c != 'm')
						{
							goto IL_030D;
						}
						if (!(name == "method"))
						{
							goto IL_030D;
						}
						return new MethodNameFilter(node.Value)
						{
							IsRegex = flag
						};
					}
					else if (!(name == "filter"))
					{
						goto IL_030D;
					}
					break;
				}
				default:
					goto IL_030D;
				}
				AndFilter andFilter = new AndFilter();
				foreach (TNode tnode2 in node.ChildNodes)
				{
					andFilter.Add(TestFilter.FromXml(tnode2));
				}
				return andFilter;
			}
			IL_030D:
			throw new ArgumentException("Invalid filter element: " + node.Name, "xmlNode");
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000B47C File Offset: 0x0000967C
		public TNode ToXml(bool recursive)
		{
			return this.AddToXml(new TNode("dummy"), recursive);
		}

		// Token: 0x06000500 RID: 1280
		public abstract TNode AddToXml(TNode parentNode, bool recursive);

		// Token: 0x040000F4 RID: 244
		public static readonly TestFilter Empty = new TestFilter.EmptyFilter();

		// Token: 0x0200017C RID: 380
		[Serializable]
		private class EmptyFilter : TestFilter
		{
			// Token: 0x06000AA2 RID: 2722 RVA: 0x0000C168 File Offset: 0x0000A368
			public override bool Match(ITest test)
			{
				return true;
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x0000C168 File Offset: 0x0000A368
			public override bool Pass(ITest test)
			{
				return true;
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x00004B9A File Offset: 0x00002D9A
			public override bool IsExplicitMatch(ITest test)
			{
				return false;
			}

			// Token: 0x06000AA5 RID: 2725 RVA: 0x00019AB9 File Offset: 0x00017CB9
			public override TNode AddToXml(TNode parentNode, bool recursive)
			{
				return parentNode.AddElement("filter");
			}
		}
	}
}
