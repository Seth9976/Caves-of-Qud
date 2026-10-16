using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005F4 RID: 1524
	internal class Build : CapabilitiesBuild
	{
		// Token: 0x0600421B RID: 16923 RVA: 0x000AAD0A File Offset: 0x000A8F0A
		public Build()
		{
			this.Browserfiles = new Dictionary<string, File>();
			this.nbrowserfiles = new List<File>();
			this.DefaultKeys = new Dictionary<string, string>();
			this.BrowserKeys = new Dictionary<string, string>();
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000AAD4C File Offset: 0x000A8F4C
		public void AddBrowserDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				FileInfo[] files = new DirectoryInfo(path).GetFiles("*.browser");
				for (int i = 0; i <= files.Length - 1; i++)
				{
					this.AddBrowserFile(files[i].FullName);
				}
				return;
			}
			if (File.Exists(path))
			{
				this.AddBrowserFile(path);
			}
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000AADA0 File Offset: 0x000A8FA0
		public void AddBrowserFile(string fileName)
		{
			if (!this.Browserfiles.ContainsKey(fileName))
			{
				File file = new File(fileName);
				this.AddBrowserFile(file);
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000AADCC File Offset: 0x000A8FCC
		private void AddBrowserFile(File file)
		{
			if (!this.Browserfiles.ContainsKey(file.FileName))
			{
				this.Browserfiles.Add(file.FileName, file);
				this.nbrowserfiles.Add(file);
				string[] array = file.Keys;
				for (int i = 0; i <= array.Length - 1; i++)
				{
					if (this.BrowserKeys.ContainsKey(array[i]))
					{
						throw new Exception(string.Concat(new string[]
						{
							"Duplicate Key \"",
							array[i],
							"\" found in ",
							file.FileName,
							" and in file ",
							this.BrowserKeys[array[i]]
						}));
					}
					this.BrowserKeys.Add(array[i], file.FileName);
				}
				array = file.DefaultKeys;
				for (int j = 0; j <= array.Length - 1; j++)
				{
					if (this.DefaultKeys.ContainsKey(array[j]))
					{
						throw new Exception(string.Concat(new string[]
						{
							"Duplicate Key \"",
							array[j],
							"\" found in ",
							file.FileName,
							" and in file ",
							this.DefaultKeys[array[j]]
						}));
					}
					this.DefaultKeys.Add(array[j], file.FileName);
				}
			}
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000AAF1C File Offset: 0x000A911C
		public void AddBrowserFile(XmlDocument browser, string fileName)
		{
			if (!this.Browserfiles.ContainsKey(fileName))
			{
				File file = new File(browser, fileName);
				this.AddBrowserFile(file);
			}
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000AAF48 File Offset: 0x000A9148
		public Node Browser()
		{
			if (this.browser == null)
			{
				object obj = this.browserSyncRoot;
				lock (obj)
				{
					if (this.browser == null)
					{
						this.browser = this.InitializeTree();
					}
				}
			}
			return this.browser;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000AAFA4 File Offset: 0x000A91A4
		private Node InitializeTree()
		{
			Node node = new Node();
			SortedList<string, List<File>> sortedList = new SortedList<string, List<File>>();
			for (int i = 0; i <= this.Browserfiles.Count - 1; i++)
			{
				if (!sortedList.ContainsKey(this.nbrowserfiles[i].FileName))
				{
					List<File> list = new List<File>();
					sortedList.Add(this.nbrowserfiles[i].FileName, list);
				}
				sortedList[this.nbrowserfiles[i].FileName].Add(this.nbrowserfiles[i]);
			}
			File[] array = new File[this.Browserfiles.Count];
			int num = 0;
			for (int j = 0; j <= sortedList.Count - 1; j++)
			{
				List<File> list2 = sortedList[sortedList.Keys[j]];
				for (int k = 0; k <= list2.Count - 1; k++)
				{
					array[num] = list2[k];
					num++;
				}
			}
			for (int l = 0; l <= this.Browserfiles.Count - 1; l++)
			{
				for (int m = 0; m <= array[l].Keys.Length - 1; m++)
				{
					Node node2 = array[l].GetNode(array[l].Keys[m]);
					Node node3 = null;
					if (node2.ParentId.Length > 0)
					{
						node3 = this.GetNode(node2.ParentId);
						if (node3 == null)
						{
							throw new Exception(string.Format("Parent not found with id = {0}", node2.ParentId));
						}
					}
					if (node3 == null)
					{
						node3 = node;
					}
					node3.AddChild(node2);
				}
			}
			for (int n = 0; n <= this.Browserfiles.Count - 1; n++)
			{
				for (int num2 = 0; num2 <= array[n].DefaultKeys.Length - 1; num2++)
				{
					Node defaultNode = array[n].GetDefaultNode(array[n].DefaultKeys[num2]);
					Node node4 = this.GetNode(defaultNode.Id);
					if (node4 != defaultNode)
					{
						Node node5 = this.GetNode(node4.ParentId);
						if (node5 == null)
						{
							node5 = node;
						}
						node5.RemoveChild(node4);
						defaultNode.AddChild(node4);
						node5.AddChild(defaultNode);
					}
				}
			}
			for (int num3 = 0; num3 <= this.Browserfiles.Count - 1; num3++)
			{
				foreach (Node node6 in array[num3].RefNodes)
				{
					this.GetNode(node6.RefId).MergeFrom(node6);
				}
			}
			return node;
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000AB254 File Offset: 0x000A9454
		private Node GetNode(string Key)
		{
			if (Key == null || Key.Length == 0)
			{
				return null;
			}
			string text;
			if (!this.BrowserKeys.TryGetValue(Key, out text) && !this.DefaultKeys.TryGetValue(Key, out text))
			{
				return null;
			}
			if (text != null && text.Length > 0)
			{
				return this.Browserfiles[text].GetNode(Key);
			}
			return null;
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000AB2B0 File Offset: 0x000A94B0
		public Node[] Nodes()
		{
			File[] array = new File[this.Browserfiles.Count];
			this.Browserfiles.Values.CopyTo(array, 0);
			int num = 0;
			for (int i = 0; i <= array.Length - 1; i++)
			{
				num += array[i].Nodes.Length;
			}
			Node[] array2 = new Node[num];
			num = 0;
			for (int j = 0; j <= array.Length - 1; j++)
			{
				for (int k = 0; k <= array[j].Nodes.Length - 1; k++)
				{
					array2[num] = array[j].Nodes[k];
					num++;
				}
			}
			return array2;
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000AB34C File Offset: 0x000A954C
		public override CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities)
		{
			if (initialCapabilities == null)
			{
				initialCapabilities = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			}
			Result result = new Result(initialCapabilities);
			this.Browser().Process(header, result, new List<Match>());
			return result;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000AB383 File Offset: 0x000A9583
		protected override Collection<string> HeaderNames(Collection<string> list)
		{
			return this.Browser().HeaderNames(list);
		}

		// Token: 0x04002372 RID: 9074
		private Dictionary<string, File> Browserfiles;

		// Token: 0x04002373 RID: 9075
		private List<File> nbrowserfiles;

		// Token: 0x04002374 RID: 9076
		private Dictionary<string, string> DefaultKeys;

		// Token: 0x04002375 RID: 9077
		private Dictionary<string, string> BrowserKeys;

		// Token: 0x04002376 RID: 9078
		private object browserSyncRoot = new object();

		// Token: 0x04002377 RID: 9079
		private Node browser;
	}
}
