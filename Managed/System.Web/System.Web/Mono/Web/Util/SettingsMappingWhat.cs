using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Mono.Web.Util
{
	// Token: 0x0200000E RID: 14
	public class SettingsMappingWhat
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002B38 File Offset: 0x00000D38
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002B40 File Offset: 0x00000D40
		public List<SettingsMappingWhatContents> Contents
		{
			get
			{
				return this._contents;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B48 File Offset: 0x00000D48
		public SettingsMappingWhat(XPathNavigator nav)
		{
			this._value = nav.GetAttribute("value", string.Empty);
			XPathNodeIterator xpathNodeIterator = nav.Select("./*");
			this._contents = new List<SettingsMappingWhatContents>();
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				string localName = xpathNavigator.LocalName;
				if (!(localName == "replace"))
				{
					if (!(localName == "add"))
					{
						if (!(localName == "clear"))
						{
							if (localName == "remove")
							{
								this._contents.Add(new SettingsMappingWhatContents(xpathNavigator, SettingsMappingWhatOperation.Remove));
							}
						}
						else
						{
							this._contents.Add(new SettingsMappingWhatContents(xpathNavigator, SettingsMappingWhatOperation.Clear));
						}
					}
					else
					{
						this._contents.Add(new SettingsMappingWhatContents(xpathNavigator, SettingsMappingWhatOperation.Add));
					}
				}
				else
				{
					this._contents.Add(new SettingsMappingWhatContents(xpathNavigator, SettingsMappingWhatOperation.Replace));
				}
			}
		}

		// Token: 0x04000D41 RID: 3393
		private string _value;

		// Token: 0x04000D42 RID: 3394
		private List<SettingsMappingWhatContents> _contents;
	}
}
