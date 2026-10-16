using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200019F RID: 415
	internal class ConfigHelper
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x0002EA44 File Offset: 0x0002CC44
		internal static IDictionary GetDictionary(IDictionary prev, XmlNode region, string nameAtt, string valueAtt)
		{
			Hashtable hashtable;
			if (prev == null)
			{
				hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				hashtable = (Hashtable)((Hashtable)prev).Clone();
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(hashtable);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as IDictionary;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002EA98 File Offset: 0x0002CC98
		internal static ConfigNameValueCollection GetNameValueCollection(NameValueCollection prev, XmlNode region, string nameAtt, string valueAtt)
		{
			ConfigNameValueCollection configNameValueCollection = new ConfigNameValueCollection(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			if (prev != null)
			{
				configNameValueCollection.Add(prev);
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(configNameValueCollection);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as ConfigNameValueCollection;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		private static ConfigHelper.CollectionWrapper GoGetThem(ConfigHelper.CollectionWrapper result, XmlNode region, string nameAtt, string valueAtt)
		{
			if (region.Attributes != null && region.Attributes.Count != 0 && (region.Attributes.Count != 1 || region.Attributes[0].Name != "xmlns"))
			{
				throw new ConfigurationException("Unknown attribute", region);
			}
			foreach (object obj in region.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						throw new ConfigurationException("Only XmlElement allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Clear();
					}
					else if (name == "remove")
					{
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Remove(xmlNode2.Value);
					}
					else
					{
						if (!(name == "add"))
						{
							throw new ConfigurationException("Unknown element", xmlNode);
						}
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						XmlNode xmlNode3 = xmlNode.Attributes.RemoveNamedItem(valueAtt);
						if (xmlNode3 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result[xmlNode2.Value] = xmlNode3.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x020001A0 RID: 416
		private class CollectionWrapper
		{
			// Token: 0x06000ADF RID: 2783 RVA: 0x0002ED28 File Offset: 0x0002CF28
			public CollectionWrapper(IDictionary dict)
			{
				this.dict = dict;
				this.isDict = true;
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x0002ED3E File Offset: 0x0002CF3E
			public CollectionWrapper(NameValueCollection collection)
			{
				this.collection = collection;
				this.isDict = false;
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x0002ED54 File Offset: 0x0002CF54
			public void Remove(string s)
			{
				if (this.isDict)
				{
					this.dict.Remove(s);
					return;
				}
				this.collection.Remove(s);
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x0002ED77 File Offset: 0x0002CF77
			public void Clear()
			{
				if (this.isDict)
				{
					this.dict.Clear();
					return;
				}
				this.collection.Clear();
			}

			// Token: 0x170001B6 RID: 438
			public string this[string key]
			{
				set
				{
					if (this.isDict)
					{
						this.dict[key] = value;
						return;
					}
					this.collection[key] = value;
				}
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0002EDBD File Offset: 0x0002CFBD
			public object UnWrap()
			{
				if (this.isDict)
				{
					return this.dict;
				}
				return this.collection;
			}

			// Token: 0x04000751 RID: 1873
			private IDictionary dict;

			// Token: 0x04000752 RID: 1874
			private NameValueCollection collection;

			// Token: 0x04000753 RID: 1875
			private bool isDict;
		}
	}
}
