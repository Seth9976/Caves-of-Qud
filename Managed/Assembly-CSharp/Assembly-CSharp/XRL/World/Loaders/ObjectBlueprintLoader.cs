using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XRL.World.Loaders
{
	// Token: 0x020009A5 RID: 2469
	public class ObjectBlueprintLoader
	{
		// Token: 0x06006AA7 RID: 27303 RVA: 0x00224BE8 File Offset: 0x00222DE8
		static ObjectBlueprintLoader()
		{
			foreach (KeyValuePair<string, ObjectBlueprintLoader.KnownNode> keyValuePair in ObjectBlueprintLoader.KnownNodes)
			{
				string text;
				ObjectBlueprintLoader.KnownNode knownNode;
				keyValuePair.Deconstruct(out text, out knownNode);
				ObjectBlueprintLoader.KnownNode knownNode2 = knownNode;
				if (knownNode2.Type != ObjectBlueprintLoader.NodeType.Baked)
				{
					ObjectBlueprintLoader.RemovalNodes.Add(new ObjectBlueprintLoader.RemovalNode(knownNode2));
				}
			}
			foreach (ObjectBlueprintLoader.RemovalNode removalNode in ObjectBlueprintLoader.RemovalNodes)
			{
				ObjectBlueprintLoader.KnownNodes.Add(removalNode.Key, new ObjectBlueprintLoader.KnownNode(removalNode.Key, removalNode.Attribute, ObjectBlueprintLoader.NodeType.Baked));
			}
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x00224E30 File Offset: 0x00223030
		private ObjectBlueprintLoader.ObjectBlueprintXMLData Bake(ObjectBlueprintLoader.ObjectBlueprintXMLData obj)
		{
			ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData;
			if (this.Finalized.TryGetValue(obj.Name, out objectBlueprintXMLData))
			{
				return objectBlueprintXMLData;
			}
			if (this.BakeStack.Contains(obj.Name))
			{
				ModInfo mod = obj.Mod;
				string[] array = new string[6];
				array[0] = "blueprint ";
				array[1] = obj.Name;
				array[2] = " seems to have an inheritance loop. ";
				array[3] = this.BakeStack.Aggregate("", (string a, string b) => a + " -> " + b);
				array[4] = " -> ";
				array[5] = obj.Name;
				MetricsManager.LogPotentialModError(mod, string.Concat(array));
				return new ObjectBlueprintLoader.ObjectBlueprintXMLData();
			}
			this.BakeStack.Add(obj.Name);
			List<ObjectBlueprintLoader.Mixin> list = null;
			ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData2 = new ObjectBlueprintLoader.ObjectBlueprintXMLData();
			objectBlueprintXMLData2.Name = obj.Name;
			objectBlueprintXMLData2.PreviousName = obj.PreviousName;
			objectBlueprintXMLData2.Inherits = obj.Inherits;
			objectBlueprintXMLData2.Mod = obj.Mod;
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
			if (obj.Children.TryGetValue("mixin", out objectBlueprintXMLChildNodeCollection))
			{
				list = ((this.BakeMixins.Count > 0) ? this.BakeMixins.Pop() : new List<ObjectBlueprintLoader.Mixin>(objectBlueprintXMLChildNodeCollection.Named.Count));
				list.Clear();
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in objectBlueprintXMLChildNodeCollection.Named)
				{
					ObjectBlueprintLoader.Mixin mixin = default(ObjectBlueprintLoader.Mixin);
					mixin.Name = keyValuePair.Key;
					Dictionary<string, string> attributes = keyValuePair.Value.Attributes;
					mixin.Include = ((attributes != null) ? attributes.GetValue("Include", null) : null);
					Dictionary<string, string> attributes2 = keyValuePair.Value.Attributes;
					mixin.Exclude = ((attributes2 != null) ? attributes2.GetValue("Exclude", null) : null);
					ObjectBlueprintLoader.Mixin mixin2 = mixin;
					Dictionary<string, string> attributes3 = keyValuePair.Value.Attributes;
					string text = ((attributes3 != null) ? attributes3.GetValue("Priority", null) : null);
					int num;
					if (!text.IsNullOrEmpty() && int.TryParse(text, out num))
					{
						mixin2.Priority = num;
					}
					Dictionary<string, string> attributes4 = keyValuePair.Value.Attributes;
					mixin2.Fill = ((attributes4 != null) ? attributes4.GetValue("Load", null) : null) == "Fill";
					list.Add(mixin2);
				}
				list.Sort();
				foreach (ObjectBlueprintLoader.Mixin mixin3 in list)
				{
					if (mixin3.Fill)
					{
						this.Inherit(mixin3.Name, obj, objectBlueprintXMLData2, mixin3.Include, mixin3.Exclude);
					}
				}
			}
			if (!string.IsNullOrEmpty(obj.Inherits))
			{
				this.Inherit(obj.Inherits, obj, objectBlueprintXMLData2, null, null);
			}
			if (list != null)
			{
				foreach (ObjectBlueprintLoader.Mixin mixin4 in list)
				{
					if (!mixin4.Fill)
					{
						this.Inherit(mixin4.Name, obj, objectBlueprintXMLData2, mixin4.Include, mixin4.Exclude);
					}
				}
				list.Clear();
				this.BakeMixins.Push(list);
			}
			foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection> keyValuePair2 in obj.Children)
			{
				if (!(keyValuePair2.Key == "mixin"))
				{
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection2;
					if (objectBlueprintXMLData2.Children.TryGetValue(keyValuePair2.Key, out objectBlueprintXMLChildNodeCollection2))
					{
						if (keyValuePair2.Key.StartsWith("xtag"))
						{
							objectBlueprintXMLChildNodeCollection2.Unnamed[0].Merge(keyValuePair2.Value.Unnamed[0]);
						}
						else
						{
							objectBlueprintXMLChildNodeCollection2.Merge(keyValuePair2.Value.Clone());
						}
					}
					else
					{
						objectBlueprintXMLData2.Children[keyValuePair2.Key] = keyValuePair2.Value.Clone();
					}
				}
			}
			this.ProcessTagRemoval(objectBlueprintXMLData2);
			this.Finalized.Add(obj.Name, objectBlueprintXMLData2);
			this.BakeStack.Remove(obj.Name);
			return objectBlueprintXMLData2;
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0022528C File Offset: 0x0022348C
		private void ProcessTagRemoval(ObjectBlueprintLoader.ObjectBlueprintXMLData Result)
		{
			foreach (ObjectBlueprintLoader.RemovalNode removalNode in ObjectBlueprintLoader.RemovalNodes)
			{
				if (removalNode.Type == ObjectBlueprintLoader.NodeType.Named)
				{
					this.ProcessNamedRemoval(Result, removalNode.Target, removalNode.Key);
				}
				else
				{
					this.ProcessUnnamedRemoval(Result, removalNode.Target, removalNode.Key, removalNode.Attribute);
				}
			}
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x00225310 File Offset: 0x00223510
		private void ProcessNamedRemoval(ObjectBlueprintLoader.ObjectBlueprintXMLData Result, string CollectionKey, string RemovalKey)
		{
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection2;
			if (Result.Children.TryGetValue(RemovalKey, out objectBlueprintXMLChildNodeCollection) && objectBlueprintXMLChildNodeCollection.Named != null && Result.Children.TryGetValue(CollectionKey, out objectBlueprintXMLChildNodeCollection2) && objectBlueprintXMLChildNodeCollection2.Named != null)
			{
				bool flag = true;
				ObjectBlueprintLoader.ExhaustedRemovals.Clear();
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in objectBlueprintXMLChildNodeCollection.Named)
				{
					string text;
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode;
					keyValuePair.Deconstruct(out text, out objectBlueprintXMLChildNode);
					string text2 = text;
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode2 = objectBlueprintXMLChildNode;
					objectBlueprintXMLChildNodeCollection2.Named.Remove(text2);
					string attribute = objectBlueprintXMLChildNode2.GetAttribute("Depth");
					int num;
					if (!attribute.IsNullOrEmpty() && int.TryParse(attribute, out num) && num >= 1)
					{
						flag = false;
						if (num == 1)
						{
							objectBlueprintXMLChildNode2.Attributes.Remove("Depth");
						}
						else
						{
							objectBlueprintXMLChildNode2.Attributes["Depth"] = (num - 1).ToStringCached();
						}
					}
					else
					{
						ObjectBlueprintLoader.ExhaustedRemovals.Add(text2);
					}
				}
				if (flag)
				{
					Result.Children.Remove(RemovalKey);
					return;
				}
				foreach (string text3 in ObjectBlueprintLoader.ExhaustedRemovals)
				{
					objectBlueprintXMLChildNodeCollection.Named.Remove(text3);
				}
			}
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x00225494 File Offset: 0x00223694
		private void ProcessUnnamedRemoval(ObjectBlueprintLoader.ObjectBlueprintXMLData Result, string CollectionKey, string RemovalKey, string AttributeKey)
		{
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection2;
			if (Result.Children.TryGetValue(RemovalKey, out objectBlueprintXMLChildNodeCollection) && objectBlueprintXMLChildNodeCollection.Unnamed != null && Result.Children.TryGetValue(CollectionKey, out objectBlueprintXMLChildNodeCollection2) && objectBlueprintXMLChildNodeCollection2.Unnamed != null)
			{
				foreach (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode in objectBlueprintXMLChildNodeCollection.Unnamed)
				{
					string attribute = objectBlueprintXMLChildNode.GetAttribute(AttributeKey);
					bool flag = objectBlueprintXMLChildNode.GetAttribute("All").EqualsNoCase("true");
					if (!attribute.IsNullOrEmpty())
					{
						for (int i = objectBlueprintXMLChildNodeCollection2.Unnamed.Count - 1; i >= 0; i--)
						{
							if (!(attribute != objectBlueprintXMLChildNodeCollection2.Unnamed[i].GetAttribute(AttributeKey)))
							{
								objectBlueprintXMLChildNodeCollection2.Unnamed.RemoveAt(i);
								if (!flag)
								{
									break;
								}
							}
						}
					}
				}
				Result.Children.Remove(RemovalKey);
			}
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x00225598 File Offset: 0x00223798
		private void Inherit(string Name, ObjectBlueprintLoader.ObjectBlueprintXMLData Object, ObjectBlueprintLoader.ObjectBlueprintXMLData Result, string Include = null, string Exclude = null)
		{
			string text;
			if (CompatManager.TryGetCompatEntry("blueprint", Name, out text))
			{
				MetricsManager.LogPotentialModError(Object.Mod, string.Concat(new string[] { "blueprint \"Name\" inherited by \"", Object.Name, "\" was reanamed \"", text, "\"" }));
				Name = text;
			}
			ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData;
			if (!this.Objects.TryGetValue(Name, out objectBlueprintXMLData))
			{
				MetricsManager.LogPotentialModError(Object.Mod, string.Concat(new string[] { "blueprint \"", Name, "\" inherited by ", Object.Name, " not found" }));
				return;
			}
			foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection> keyValuePair in this.Bake(objectBlueprintXMLData).Children)
			{
				if ((Include.IsNullOrEmpty() || Include.HasDelimitedSubstring(',', keyValuePair.Key, StringComparison.Ordinal)) && (Exclude.IsNullOrEmpty() || !Exclude.HasDelimitedSubstring(',', keyValuePair.Key, StringComparison.Ordinal)))
				{
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
					if (Result.Children.TryGetValue(keyValuePair.Key, out objectBlueprintXMLChildNodeCollection))
					{
						if (keyValuePair.Key.StartsWith("xtag"))
						{
							objectBlueprintXMLChildNodeCollection.Unnamed[0].Merge(keyValuePair.Value.Unnamed[0]);
						}
						else
						{
							objectBlueprintXMLChildNodeCollection.Merge(keyValuePair.Value.Clone());
						}
					}
					else
					{
						Result.Children[keyValuePair.Key] = keyValuePair.Value.Clone();
					}
				}
			}
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection2;
			if (Result.Children.TryGetValue("tag", out objectBlueprintXMLChildNodeCollection2))
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair2 in objectBlueprintXMLChildNodeCollection2.Named)
				{
					if (keyValuePair2.Value.Attributes.ContainsValue("*noinherit"))
					{
						list.Add(keyValuePair2.Key);
					}
				}
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					objectBlueprintXMLChildNodeCollection2.Named.Remove(list[i]);
					i++;
				}
			}
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x002257FC File Offset: 0x002239FC
		public IEnumerable<ObjectBlueprintLoader.ObjectBlueprintXMLData> BakedBlueprints()
		{
			foreach (string text in this.Objects.Keys)
			{
				yield return this.Bake(this.Objects[text]);
			}
			Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData>.KeyCollection.Enumerator enumerator = default(Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x0022580C File Offset: 0x00223A0C
		public int ReadObjectsNode(XmlTextReader reader, ModInfo modInfo = null)
		{
			int num = 0;
			while (reader.Read())
			{
				if (reader.Name == "object")
				{
					ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData = ObjectBlueprintLoader.ObjectBlueprintXMLData.ReadObjectNode(reader);
					this.ProcessNodeCompat(objectBlueprintXMLData);
					objectBlueprintXMLData.Mod = modInfo;
					num++;
					if (objectBlueprintXMLData.Load == "Merge")
					{
						string text = objectBlueprintXMLData.Name;
						string text2;
						if (CompatManager.TryGetCompatEntry("blueprint", text, out text2))
						{
							ObjectBlueprintLoader.handleWarning(string.Format("File: {0}, Line: {1}:{2} Attempt to merge with {3} which has a new name \"{4}\"", new object[]
							{
								DataManager.SanitizePathForDisplay(reader.BaseURI),
								reader.LineNumber,
								reader.LinePosition,
								text,
								text2
							}));
							text = text2;
						}
						ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData2;
						if (!this.Objects.TryGetValue(text, out objectBlueprintXMLData2))
						{
							ObjectBlueprintLoader.handleError(string.Format("File: {0}, Line: {1}:{2} Attempt to merge with {3} which is an unknown blueprint, node discarded", new object[]
							{
								DataManager.SanitizePathForDisplay(reader.BaseURI),
								reader.LineNumber,
								reader.LinePosition,
								text
							}));
						}
						else
						{
							objectBlueprintXMLData2.Merge(objectBlueprintXMLData);
						}
					}
					else if (objectBlueprintXMLData.Load == "MergeIfExists")
					{
						string text3 = objectBlueprintXMLData.Name;
						string text4;
						if (CompatManager.TryGetCompatEntry("blueprint", text3, out text4))
						{
							ObjectBlueprintLoader.handleWarning(string.Format("File: {0}, Line: {1}:{2} Attempt to merge with {3} which has a new name \"{4}\"", new object[]
							{
								DataManager.SanitizePathForDisplay(reader.BaseURI),
								reader.LineNumber,
								reader.LinePosition,
								text3,
								text4
							}));
							text3 = text4;
						}
						ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData3;
						if (this.Objects.TryGetValue(text3, out objectBlueprintXMLData3))
						{
							objectBlueprintXMLData3.Merge(objectBlueprintXMLData);
						}
					}
					else
					{
						this.Objects[objectBlueprintXMLData.Name] = objectBlueprintXMLData;
					}
				}
				else if (reader.NodeType != XmlNodeType.Comment)
				{
					if (reader.Name == "objects" && reader.NodeType == XmlNodeType.EndElement)
					{
						return num;
					}
					throw new Exception("Unknown node '" + reader.Name + "'");
				}
			}
			return num;
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x00225A30 File Offset: 0x00223C30
		private void ProcessNodeCompat(ObjectBlueprintLoader.ObjectBlueprintXMLData Node)
		{
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
			ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode;
			if (Node.Children.TryGetValue("tag", out objectBlueprintXMLChildNodeCollection) && objectBlueprintXMLChildNodeCollection.Named != null && objectBlueprintXMLChildNodeCollection.Named.Remove("NoCombat", out objectBlueprintXMLChildNode))
			{
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection2;
				if (!Node.Children.TryGetValue("removepart", out objectBlueprintXMLChildNodeCollection2))
				{
					objectBlueprintXMLChildNodeCollection2 = (Node.Children["removepart"] = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection());
				}
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection3 = objectBlueprintXMLChildNodeCollection2;
				if (objectBlueprintXMLChildNodeCollection3.Named == null)
				{
					objectBlueprintXMLChildNodeCollection3.Named = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>();
				}
				if (!objectBlueprintXMLChildNodeCollection2.Named.ContainsKey("Combat"))
				{
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode2 = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNode("removepart");
					objectBlueprintXMLChildNode2.Attributes["Name"] = "Combat";
					objectBlueprintXMLChildNodeCollection2.Named["Combat"] = objectBlueprintXMLChildNode2;
				}
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection4;
				if (Node.Children.TryGetValue("part", out objectBlueprintXMLChildNodeCollection4))
				{
					Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> named = objectBlueprintXMLChildNodeCollection4.Named;
					if (named == null)
					{
						return;
					}
					named.Remove("Combat");
				}
			}
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x00225B2C File Offset: 0x00223D2C
		public void ReadObjectBlueprintsXML(XmlTextReader reader, ModInfo modInfo = null)
		{
			if (ObjectBlueprintLoader.handleError == null)
			{
				ObjectBlueprintLoader.handleError = new Action<object>(MetricsManager.LogError);
			}
			bool flag = false;
			try
			{
				reader.WhitespaceHandling = WhitespaceHandling.None;
				while (reader.Read())
				{
					if (reader.Name == "objects")
					{
						flag = true;
						if (MarkovCorpusGenerator.Generating && reader.GetAttribute("ExcludeFromCorpusGeneration").EqualsNoCase("true"))
						{
							return;
						}
						this.ReadObjectsNode(reader, modInfo);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("File: {0}, Line: {1}:{2}", DataManager.SanitizePathForDisplay(reader.BaseURI), reader.LineNumber, reader.LinePosition), ex);
			}
			finally
			{
				reader.Close();
			}
			if (!flag)
			{
				ObjectBlueprintLoader.handleError("No <objects> tag found in ObjectBlueprints.XML");
			}
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x00225C0C File Offset: 0x00223E0C
		public void LoadAllBlueprints()
		{
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Objects", true, true))
			{
				if (xmlDataHelper.modInfo != null)
				{
					ObjectBlueprintLoader.handleError = new Action<object>(xmlDataHelper.modInfo.Error);
					ObjectBlueprintLoader.handleWarning = new Action<object>(xmlDataHelper.modInfo.Warn);
				}
				else
				{
					ObjectBlueprintLoader.handleError = new Action<object>(MetricsManager.LogError);
					ObjectBlueprintLoader.handleWarning = new Action<object>(MetricsManager.LogWarning);
				}
				try
				{
					this.ReadObjectBlueprintsXML(xmlDataHelper, xmlDataHelper.modInfo);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
			GamePartBlueprint.PartReflectionCache.ClearOrphaned();
		}

		// Token: 0x040030CE RID: 12494
		private Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData> Objects = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData>();

		// Token: 0x040030CF RID: 12495
		private Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData> Finalized = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLData>();

		// Token: 0x040030D0 RID: 12496
		private List<string> BakeStack = new List<string>();

		// Token: 0x040030D1 RID: 12497
		private Stack<List<ObjectBlueprintLoader.Mixin>> BakeMixins = new Stack<List<ObjectBlueprintLoader.Mixin>>();

		// Token: 0x040030D2 RID: 12498
		private static List<ObjectBlueprintLoader.RemovalNode> RemovalNodes = new List<ObjectBlueprintLoader.RemovalNode>(ObjectBlueprintLoader.KnownNodes.Count);

		// Token: 0x040030D3 RID: 12499
		private static List<string> ExhaustedRemovals = new List<string>();

		// Token: 0x040030D4 RID: 12500
		protected static Action<object> handleError = new Action<object>(MetricsManager.LogError);

		// Token: 0x040030D5 RID: 12501
		protected static Action<object> handleWarning = new Action<object>(MetricsManager.LogWarning);

		// Token: 0x040030D6 RID: 12502
		private static Dictionary<string, ObjectBlueprintLoader.KnownNode> KnownNodes = new Dictionary<string, ObjectBlueprintLoader.KnownNode>
		{
			{
				"builder",
				new ObjectBlueprintLoader.KnownNode("builder", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"intproperty",
				new ObjectBlueprintLoader.KnownNode("intproperty", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"inventoryobject",
				new ObjectBlueprintLoader.KnownNode("inventoryobject", "Blueprint", ObjectBlueprintLoader.NodeType.Unnamed)
			},
			{
				"mutation",
				new ObjectBlueprintLoader.KnownNode("mutation", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"part",
				new ObjectBlueprintLoader.KnownNode("part", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"mixin",
				new ObjectBlueprintLoader.KnownNode("mixin", "Name", ObjectBlueprintLoader.NodeType.Baked)
			},
			{
				"property",
				new ObjectBlueprintLoader.KnownNode("property", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"skill",
				new ObjectBlueprintLoader.KnownNode("skill", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"stag",
				new ObjectBlueprintLoader.KnownNode("stag", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"stat",
				new ObjectBlueprintLoader.KnownNode("stat", "Name", ObjectBlueprintLoader.NodeType.Named)
			},
			{
				"tag",
				new ObjectBlueprintLoader.KnownNode("tag", "Name", ObjectBlueprintLoader.NodeType.Named)
			}
		};

		// Token: 0x02001AB9 RID: 6841
		private enum NodeType
		{
			// Token: 0x04006A5F RID: 27231
			Baked,
			// Token: 0x04006A60 RID: 27232
			Named,
			// Token: 0x04006A61 RID: 27233
			Unnamed
		}

		// Token: 0x02001ABA RID: 6842
		private struct KnownNode
		{
			// Token: 0x0600E8DB RID: 59611 RVA: 0x0053969A File Offset: 0x0053789A
			public KnownNode(string Key, string Attribute, ObjectBlueprintLoader.NodeType Type)
			{
				this.Key = Key;
				this.Attribute = Attribute;
				this.Type = Type;
			}

			// Token: 0x04006A62 RID: 27234
			public string Key;

			// Token: 0x04006A63 RID: 27235
			public string Attribute;

			// Token: 0x04006A64 RID: 27236
			public ObjectBlueprintLoader.NodeType Type;
		}

		// Token: 0x02001ABB RID: 6843
		private struct RemovalNode
		{
			// Token: 0x0600E8DC RID: 59612 RVA: 0x005396B1 File Offset: 0x005378B1
			public RemovalNode(ObjectBlueprintLoader.KnownNode Node)
			{
				this.Key = "remove" + Node.Key;
				this.Target = Node.Key;
				this.Attribute = Node.Attribute;
				this.Type = Node.Type;
			}

			// Token: 0x04006A65 RID: 27237
			public string Key;

			// Token: 0x04006A66 RID: 27238
			public string Target;

			// Token: 0x04006A67 RID: 27239
			public string Attribute;

			// Token: 0x04006A68 RID: 27240
			public ObjectBlueprintLoader.NodeType Type;
		}

		// Token: 0x02001ABC RID: 6844
		private struct Mixin : IComparable<ObjectBlueprintLoader.Mixin>
		{
			// Token: 0x0600E8DD RID: 59613 RVA: 0x005396ED File Offset: 0x005378ED
			public int CompareTo(ObjectBlueprintLoader.Mixin Other)
			{
				return this.Priority.CompareTo(Other.Priority);
			}

			// Token: 0x04006A69 RID: 27241
			public string Name;

			// Token: 0x04006A6A RID: 27242
			public string Include;

			// Token: 0x04006A6B RID: 27243
			public string Exclude;

			// Token: 0x04006A6C RID: 27244
			public int Priority;

			// Token: 0x04006A6D RID: 27245
			public bool Fill;
		}

		// Token: 0x02001ABD RID: 6845
		public class ObjectBlueprintXMLChildNode
		{
			// Token: 0x0600E8DE RID: 59614 RVA: 0x00539700 File Offset: 0x00537900
			public ObjectBlueprintXMLChildNode(string NodeName)
			{
				this.NodeName = NodeName;
			}

			// Token: 0x17000EEE RID: 3822
			// (get) Token: 0x0600E8DF RID: 59615 RVA: 0x0053971A File Offset: 0x0053791A
			public string Name
			{
				get
				{
					if (this.Attributes.ContainsKey("Name"))
					{
						return this.Attributes["Name"];
					}
					return null;
				}
			}

			// Token: 0x0600E8E0 RID: 59616 RVA: 0x00539740 File Offset: 0x00537940
			public bool HasAttribute(string name)
			{
				return this.Attributes.ContainsKey(name);
			}

			// Token: 0x0600E8E1 RID: 59617 RVA: 0x00539750 File Offset: 0x00537950
			public string GetAttribute(string name)
			{
				string text;
				if (this.Attributes.TryGetValue(name, out text))
				{
					return text;
				}
				return null;
			}

			// Token: 0x0600E8E2 RID: 59618 RVA: 0x00539770 File Offset: 0x00537970
			public void Merge(ObjectBlueprintLoader.ObjectBlueprintXMLChildNode other)
			{
				this.NodeName = other.NodeName;
				foreach (KeyValuePair<string, string> keyValuePair in other.Attributes)
				{
					this.Attributes[keyValuePair.Key] = keyValuePair.Value;
				}
			}

			// Token: 0x0600E8E3 RID: 59619 RVA: 0x005397E4 File Offset: 0x005379E4
			public ObjectBlueprintLoader.ObjectBlueprintXMLChildNode Clone()
			{
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNode(this.NodeName);
				foreach (KeyValuePair<string, string> keyValuePair in this.Attributes)
				{
					objectBlueprintXMLChildNode.Attributes[keyValuePair.Key] = keyValuePair.Value;
				}
				return objectBlueprintXMLChildNode;
			}

			// Token: 0x0600E8E4 RID: 59620 RVA: 0x00539858 File Offset: 0x00537A58
			public static ObjectBlueprintLoader.ObjectBlueprintXMLChildNode ReadChildNode(XmlTextReader reader)
			{
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNode(reader.Name);
				if (reader.HasAttributes)
				{
					string text = DataManager.SanitizePathForDisplay(reader.BaseURI);
					string text2 = null;
					string text3 = null;
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNode._tagToNamespace.TryGetValue(objectBlueprintXMLChildNode.NodeName, out text3);
					GamePartBlueprint.PartReflectionCache partReflectionCache = null;
					if (text3 != null)
					{
						text2 = reader.GetAttribute("Name");
						Func<string, string> func;
						if (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode._compatProcessors.TryGetValue(objectBlueprintXMLChildNode.NodeName, out func))
						{
							string text4 = func(text2);
							if (text4 != text2)
							{
								ObjectBlueprintLoader.handleWarning(new Exception(string.Format("File: {0}, Line: {1}:{2} Compat Warning: {3} Name \"{4}\" has been renamed \"{5}\".", new object[] { text, reader.LineNumber, reader.LinePosition, objectBlueprintXMLChildNode.NodeName, text2, text4 })));
								text2 = text4;
							}
						}
						partReflectionCache = GamePartBlueprint.PartReflectionCache.Get(ModManager.ResolveType(text3, text2, false, false, true));
						if (partReflectionCache == null)
						{
							ObjectBlueprintLoader.handleError(new Exception(string.Format("File: {0}, Line: {1}:{2} Could not find {3}.{4}, element ignored.", new object[] { text, reader.LineNumber, reader.LinePosition, text3, text2 })));
							return objectBlueprintXMLChildNode;
						}
					}
					reader.MoveToFirstAttribute();
					for (;;)
					{
						if (partReflectionCache == null)
						{
							goto IL_036C;
						}
						if (typeof(IPart).IsAssignableFrom(partReflectionCache.T) && reader.Name == "Builder")
						{
							if (typeof(IPartBuilder).IsAssignableFrom(ModManager.ResolveType("XRL.World.PartBuilders", reader.Value, false, false, true)))
							{
								goto IL_036C;
							}
							ObjectBlueprintLoader.handleError(new Exception(string.Format("File: {0}, Line: {1}:{2} Could not find IPartBuilder {3}.", new object[] { text, reader.LineNumber, reader.LinePosition, reader.Value })));
						}
						else
						{
							if (!(reader.Name != "Name") || !(reader.Name != "ChanceIn10000") || !(reader.Name != "ChanceOneIn"))
							{
								goto IL_036C;
							}
							ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute> propertyOrField = partReflectionCache.GetPropertyOrField(reader.Name);
							Type item = propertyOrField.Item1;
							ObsoleteAttribute item2 = propertyOrField.Item3;
							if (item2 != null)
							{
								ObjectBlueprintLoader.handleError(new Exception(string.Format("File: {0}, Line: {1}:{2} {3} is obsolete: {4}", new object[] { text, reader.LineNumber, reader.LinePosition, reader.Name, item2.Message })));
							}
							if (!(item == null))
							{
								try
								{
									XmlDataHelper.AttributeParser attributeParser = XmlDataHelper.TryGetAttributeParser(item);
									if (attributeParser != null)
									{
										attributeParser._Parse(reader.Value);
									}
								}
								catch (Exception ex)
								{
									ObjectBlueprintLoader.handleError(new Exception(string.Format("File: {0}, Line: {1}:{2} Error parsing {3}=\"{4}\" as {5}", new object[] { text, reader.LineNumber, reader.LinePosition, reader.Name, reader.Value, item }), ex));
								}
								goto IL_036C;
							}
							ObjectBlueprintLoader.handleError(new MissingMemberException(string.Format("File: {0}, Line: {1}:{2} No {3}.{4} property exists.", new object[]
							{
								text,
								reader.LineNumber,
								reader.LinePosition,
								partReflectionCache.T.FullName,
								reader.Name
							})));
						}
						IL_03AB:
						if (!reader.MoveToNextAttribute())
						{
							break;
						}
						continue;
						IL_036C:
						string text5 = ((reader.Name == "Name") ? text2 : null) ?? reader.Value;
						objectBlueprintXMLChildNode.Attributes[string.Intern(reader.Name)] = string.Intern(text5);
						goto IL_03AB;
					}
					reader.MoveToElement();
				}
				if (reader.NodeType == XmlNodeType.EndElement || reader.IsEmptyElement)
				{
					return objectBlueprintXMLChildNode;
				}
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement)
					{
						if (reader.Name != "" && reader.Name != objectBlueprintXMLChildNode.NodeName)
						{
							throw new Exception("Unexpected end node for " + reader.Name);
						}
						return objectBlueprintXMLChildNode;
					}
				}
				return objectBlueprintXMLChildNode;
			}

			// Token: 0x04006A6E RID: 27246
			public string NodeName;

			// Token: 0x04006A6F RID: 27247
			public Dictionary<string, string> Attributes = new Dictionary<string, string>();

			// Token: 0x04006A70 RID: 27248
			private static Dictionary<string, string> _tagToNamespace = new Dictionary<string, string>
			{
				{ "part", "XRL.World.Parts" },
				{ "mutation", "XRL.World.Parts.Mutation" },
				{ "skill", "XRL.World.Parts.Skill" },
				{ "builder", "XRL.World.ObjectBuilders" }
			};

			// Token: 0x04006A71 RID: 27249
			private static Dictionary<string, Func<string, string>> _compatProcessors = new Dictionary<string, Func<string, string>>
			{
				{
					"skill",
					new Func<string, string>(CompatManager.ProcessSkill)
				},
				{
					"mutation",
					new Func<string, string>(CompatManager.ProcessMutation)
				}
			};
		}

		// Token: 0x02001ABE RID: 6846
		public class ObjectBlueprintXMLChildNodeCollection
		{
			// Token: 0x0600E8E6 RID: 59622 RVA: 0x00539D28 File Offset: 0x00537F28
			public void Add(ObjectBlueprintLoader.ObjectBlueprintXMLChildNode node, XmlTextReader reader)
			{
				if (string.IsNullOrEmpty(node.Name))
				{
					if (this.Unnamed == null)
					{
						this.Unnamed = new List<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>(1);
					}
					this.Unnamed.Add(node);
					return;
				}
				if (this.Named == null)
				{
					this.Named = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>(1);
				}
				if (this.Named.ContainsKey(node.Name) && reader != null)
				{
					ObjectBlueprintLoader.handleError(string.Format("{0}: Duplicate {1} Name='{2}' found at line {3}", new object[]
					{
						DataManager.SanitizePathForDisplay(reader.BaseURI),
						node.NodeName,
						node.Name,
						reader.LineNumber
					}));
					this.Named[node.Name].Merge(node);
					return;
				}
				this.Named[node.Name] = node;
			}

			// Token: 0x0600E8E7 RID: 59623 RVA: 0x00539E00 File Offset: 0x00538000
			public ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection Clone()
			{
				ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection();
				if (this.Named != null)
				{
					objectBlueprintXMLChildNodeCollection.Named = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>(this.Named.Count);
					foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in this.Named)
					{
						objectBlueprintXMLChildNodeCollection.Named[keyValuePair.Key] = keyValuePair.Value.Clone();
					}
				}
				if (this.Unnamed != null)
				{
					objectBlueprintXMLChildNodeCollection.Unnamed = new List<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>(this.Unnamed.Count);
					foreach (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode in this.Unnamed)
					{
						objectBlueprintXMLChildNodeCollection.Unnamed.Add(objectBlueprintXMLChildNode.Clone());
					}
				}
				return objectBlueprintXMLChildNodeCollection;
			}

			// Token: 0x0600E8E8 RID: 59624 RVA: 0x00539EFC File Offset: 0x005380FC
			public override string ToString()
			{
				string text = "";
				if (this.Named != null)
				{
					text = text + "Named: " + this.Named.Count.ToString() + "\n";
					foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in this.Named)
					{
						text = text + "  [" + keyValuePair.Key + " ";
						foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair.Value.Attributes)
						{
							text = string.Concat(new string[] { text, keyValuePair2.Key, "=\"", keyValuePair2.Value, "\"" });
						}
						text += "]\n";
					}
				}
				if (this.Unnamed != null)
				{
					text = text + "Unnamed: " + this.Unnamed.Count.ToString() + "\n";
					foreach (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode in this.Unnamed)
					{
						text += "  [";
						foreach (KeyValuePair<string, string> keyValuePair3 in objectBlueprintXMLChildNode.Attributes)
						{
							text = string.Concat(new string[] { text, keyValuePair3.Key, "=\"", keyValuePair3.Value, "\"" });
						}
						text += "]\n";
					}
				}
				return text;
			}

			// Token: 0x0600E8E9 RID: 59625 RVA: 0x0053A118 File Offset: 0x00538318
			public void Merge(ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection other)
			{
				if (other.Named != null)
				{
					if (this.Named == null)
					{
						this.Named = other.Named;
					}
					else
					{
						foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in other.Named)
						{
							ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode;
							if (this.Named.TryGetValue(keyValuePair.Key, out objectBlueprintXMLChildNode))
							{
								objectBlueprintXMLChildNode.Merge(keyValuePair.Value);
							}
							else
							{
								this.Add(keyValuePair.Value, null);
							}
						}
					}
				}
				if (other.Unnamed != null)
				{
					if (this.Unnamed == null)
					{
						this.Unnamed = other.Unnamed;
						return;
					}
					this.Unnamed.AddRange(other.Unnamed);
				}
			}

			// Token: 0x04006A72 RID: 27250
			public Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> Named;

			// Token: 0x04006A73 RID: 27251
			public List<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> Unnamed;
		}

		// Token: 0x02001ABF RID: 6847
		public class ObjectBlueprintXMLData
		{
			// Token: 0x0600E8EB RID: 59627 RVA: 0x0053A1EC File Offset: 0x005383EC
			public IEnumerable<KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>> NamedNodes(string type)
			{
				if (this.Children.ContainsKey(type))
				{
					if (this.Children[type].Unnamed != null)
					{
						MetricsManager.LogWarning("Unnamed " + type + " nodes detected in " + this.Name);
					}
					return this.Children[type].Named.AsEnumerable<KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>>();
				}
				return Enumerable.Empty<KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>>();
			}

			// Token: 0x0600E8EC RID: 59628 RVA: 0x0053A254 File Offset: 0x00538454
			public IEnumerable<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> UnnamedNodes(string type)
			{
				if (this.Children.ContainsKey(type))
				{
					if (this.Children[type].Named != null)
					{
						MetricsManager.LogWarning("Named " + type + " nodes detected in " + this.Name);
					}
					return this.Children[type].Unnamed.AsEnumerable<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>();
				}
				return Enumerable.Empty<ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>();
			}

			// Token: 0x0600E8ED RID: 59629 RVA: 0x0053A2BC File Offset: 0x005384BC
			public void Merge(ObjectBlueprintLoader.ObjectBlueprintXMLData other)
			{
				if (!string.IsNullOrEmpty(other.Inherits))
				{
					this.Inherits = other.Inherits;
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection> keyValuePair in other.Children)
				{
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection objectBlueprintXMLChildNodeCollection;
					if (this.Children.TryGetValue(keyValuePair.Key, out objectBlueprintXMLChildNodeCollection))
					{
						objectBlueprintXMLChildNodeCollection.Merge(keyValuePair.Value);
					}
					else
					{
						this.Children[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				if (other.Mod != null)
				{
					this.Mod = other.Mod;
				}
			}

			// Token: 0x0600E8EE RID: 59630 RVA: 0x0053A374 File Offset: 0x00538574
			public static ObjectBlueprintLoader.ObjectBlueprintXMLData ReadObjectNode(XmlTextReader reader)
			{
				ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData = new ObjectBlueprintLoader.ObjectBlueprintXMLData();
				int num = 0;
				objectBlueprintXMLData.Name = reader.GetAttribute("Name");
				objectBlueprintXMLData.PreviousName = reader.GetAttribute("PreviousName");
				objectBlueprintXMLData.Inherits = reader.GetAttribute("Inherits");
				objectBlueprintXMLData.Load = reader.GetAttribute("Load");
				if (reader.NodeType == XmlNodeType.EndElement || reader.IsEmptyElement)
				{
					return objectBlueprintXMLData;
				}
				while (reader.Read())
				{
					if (reader.NodeType != XmlNodeType.Comment && reader.NodeType != XmlNodeType.Text)
					{
						if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "object")
						{
							return objectBlueprintXMLData;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							string name = reader.Name;
							if (!ObjectBlueprintLoader.KnownNodes.ContainsKey(name) && !name.StartsWith("xtag"))
							{
								ObjectBlueprintLoader.handleError(string.Format("{0}: Unknown object element {1} at line {2}", DataManager.SanitizePathForDisplay(reader.BaseURI), reader.Name, reader.LineNumber));
							}
							ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode = ObjectBlueprintLoader.ObjectBlueprintXMLChildNode.ReadChildNode(reader);
							if (!objectBlueprintXMLData.Children.ContainsKey(name))
							{
								objectBlueprintXMLData.Children[name] = new ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection();
							}
							if (name.EqualsNoCase("mixin") && objectBlueprintXMLChildNode.Attributes.TryAdd("Priority", num.ToString()))
							{
								num++;
							}
							objectBlueprintXMLData.Children[name].Add(objectBlueprintXMLChildNode, reader);
						}
						else
						{
							ObjectBlueprintLoader.handleError(string.Format("{0}: Unknown problem reading object: {1}", DataManager.SanitizePathForDisplay(reader.BaseURI), reader.NodeType));
						}
					}
				}
				return objectBlueprintXMLData;
			}

			// Token: 0x0600E8EF RID: 59631 RVA: 0x0053A514 File Offset: 0x00538714
			public override string ToString()
			{
				string text = string.Concat(new string[] { "[ObjectBlueprintXMLData ", this.Name, " Inherits=", this.Inherits, " Load=", this.Load, "]\n" });
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection> keyValuePair in this.Children)
				{
					text = text + "[" + keyValuePair.Key + "]\n";
					string text2 = text;
					ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection value = keyValuePair.Value;
					text = text2 + ((value != null) ? value.ToString() : null);
				}
				return text;
			}

			// Token: 0x04006A74 RID: 27252
			public string Name;

			// Token: 0x04006A75 RID: 27253
			public string PreviousName;

			// Token: 0x04006A76 RID: 27254
			public string Inherits;

			// Token: 0x04006A77 RID: 27255
			public string Load;

			// Token: 0x04006A78 RID: 27256
			public ModInfo Mod;

			// Token: 0x04006A79 RID: 27257
			public Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection> Children = new Dictionary<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNodeCollection>();
		}
	}
}
