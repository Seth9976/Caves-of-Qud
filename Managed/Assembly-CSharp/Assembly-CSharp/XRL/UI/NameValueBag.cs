using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Platform.IO;
using SimpleJSON;

namespace XRL.UI
{
	// Token: 0x020014BF RID: 5311
	public class NameValueBag
	{
		// Token: 0x0600D065 RID: 53349 RVA: 0x004CE9C7 File Offset: 0x004CCBC7
		public NameValueBag(string Path)
		{
			this.fileName = Path;
			this.Bag = new Dictionary<string, string>();
		}

		// Token: 0x0600D066 RID: 53350 RVA: 0x004CE9E4 File Offset: 0x004CCBE4
		public string GetValue(string Name, string Default = null)
		{
			if (this.Bag == null)
			{
				this.Load();
			}
			string text;
			if (this.Bag.TryGetValue(Name, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x0600D067 RID: 53351 RVA: 0x004CEA12 File Offset: 0x004CCC12
		public void SetValue(string Name, string Value, bool FlushToFile = true)
		{
			this.Bag[Name] = Value;
			if (FlushToFile)
			{
				this.Flush().Wait();
			}
		}

		// Token: 0x0600D068 RID: 53352 RVA: 0x004CEA30 File Offset: 0x004CCC30
		public void Load()
		{
			this.Bag = new Dictionary<string, string>();
			if (!File.Exists(this.fileName))
			{
				return;
			}
			try
			{
				foreach (object obj in (JSON.Parse(File.ReadAllText(this.fileName)) as JSONClass))
				{
					KeyValuePair<string, JSONNode> keyValuePair = (KeyValuePair<string, JSONNode>)obj;
					this.SetValue(keyValuePair.Key, keyValuePair.Value, false);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Failed to parse " + this.fileName + "!", ex);
			}
		}

		// Token: 0x0600D069 RID: 53353 RVA: 0x004CEAF4 File Offset: 0x004CCCF4
		public async Task Flush()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\n");
			int num = 0;
			foreach (string text in this.Bag.Keys)
			{
				if (num != 0)
				{
					stringBuilder.Append(",\n");
				}
				num++;
				stringBuilder.Append(string.Concat(new string[]
				{
					"\"",
					text,
					"\":\"",
					this.Bag[text],
					"\""
				}));
			}
			stringBuilder.Append("\n}");
			(await Blob.WriteAllTextAsync(this.fileName, stringBuilder.ToString())).LogIfErrored();
		}

		// Token: 0x040051E1 RID: 20961
		private string fileName;

		// Token: 0x040051E2 RID: 20962
		public Dictionary<string, string> Bag;
	}
}
