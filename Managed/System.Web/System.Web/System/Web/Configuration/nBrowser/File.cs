using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005F6 RID: 1526
	internal class File
	{
		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x000AB3AC File Offset: 0x000A95AC
		public string FileName
		{
			get
			{
				return this.pFileName;
			}
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000AB3B4 File Offset: 0x000A95B4
		public File(string file)
		{
			this.pFileName = file;
			this.BrowserFile = new XmlDocument();
			this.BrowserFile.Load(file);
			this.Load(this.BrowserFile);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000AB3F1 File Offset: 0x000A95F1
		public File(XmlDocument BrowserFile, string filename)
		{
			this.pFileName = filename;
			this.Load(BrowserFile);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000AB414 File Offset: 0x000A9614
		private void Load(XmlDocument BrowserFile)
		{
			this.Lookup = new ListDictionary();
			this.DefaultLookup = new ListDictionary();
			this.RefNodes = new List<Node>();
			this.Nodes = new Node[BrowserFile.DocumentElement.ChildNodes.Count];
			for (int i = 0; i <= BrowserFile.DocumentElement.ChildNodes.Count - 1; i++)
			{
				XmlNode xmlNode = BrowserFile.DocumentElement.ChildNodes[i];
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					this.Nodes[i] = new Node(xmlNode);
					this.Nodes[i].FileName = this.FileName;
					if (this.Nodes[i].NameType != NodeType.DefaultBrowser)
					{
						if (this.Nodes[i].RefId.Length > 0)
						{
							this.RefNodes.Add(this.Nodes[i]);
						}
						else
						{
							if (this.Lookup.Contains(this.Nodes[i].Id))
							{
								throw new Exception("Duplicate ID found \"" + this.Nodes[i].Id + "\"");
							}
							this.Lookup.Add(this.Nodes[i].Id, i);
						}
					}
					else if (this.Nodes[i].RefId.Length > 0)
					{
						this.RefNodes.Add(this.Nodes[i]);
					}
					else
					{
						if (this.DefaultLookup.Contains(this.Nodes[i].Id))
						{
							throw new Exception("Duplicate ID found \"" + this.Nodes[i].Id + "\"");
						}
						this.DefaultLookup.Add(this.Nodes[i].Id, i);
					}
				}
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x000AB5E4 File Offset: 0x000A97E4
		public string[] Keys
		{
			get
			{
				string[] array = new string[this.Lookup.Keys.Count];
				int num = 0;
				for (int i = 0; i <= this.Nodes.Length - 1; i++)
				{
					if (this.Nodes[i] != null && this.Nodes[i].NameType != NodeType.DefaultBrowser && this.Nodes[i].RefId.Length == 0)
					{
						array[num] = this.Nodes[i].Id;
						num++;
					}
				}
				return array;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x000AB664 File Offset: 0x000A9864
		public string[] DefaultKeys
		{
			get
			{
				string[] array = new string[this.DefaultLookup.Keys.Count];
				int num = 0;
				for (int i = 0; i <= this.Nodes.Length - 1; i++)
				{
					if (this.Nodes[i] != null && this.Nodes[i].NameType == NodeType.DefaultBrowser)
					{
						array[num] = this.Nodes[i].Id;
						num++;
					}
				}
				return array;
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000AB6D0 File Offset: 0x000A98D0
		internal Node GetNode(string Key)
		{
			object obj = this.Lookup[Key];
			if (obj == null)
			{
				return this.GetDefaultNode(Key);
			}
			return this.Nodes[(int)obj];
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000AB704 File Offset: 0x000A9904
		internal Node GetDefaultNode(string Key)
		{
			object obj = this.DefaultLookup[Key];
			if (obj == null)
			{
				return null;
			}
			return this.Nodes[(int)obj];
		}

		// Token: 0x04002378 RID: 9080
		private XmlDocument BrowserFile;

		// Token: 0x04002379 RID: 9081
		internal Node[] Nodes;

		// Token: 0x0400237A RID: 9082
		private ListDictionary Lookup;

		// Token: 0x0400237B RID: 9083
		private ListDictionary DefaultLookup;

		// Token: 0x0400237C RID: 9084
		internal List<Node> RefNodes;

		// Token: 0x0400237D RID: 9085
		private string pFileName = string.Empty;
	}
}
