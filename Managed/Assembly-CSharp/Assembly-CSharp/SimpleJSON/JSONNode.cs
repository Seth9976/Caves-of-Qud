using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SimpleJSON
{
	// Token: 0x02000359 RID: 857
	public class JSONNode
	{
		// Token: 0x06001FB8 RID: 8120 RVA: 0x0009C686 File Offset: 0x0009A886
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x1700043C RID: 1084
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700043D RID: 1085
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0009C692 File Offset: 0x0009A892
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x0009C699 File Offset: 0x0009A899
		public virtual string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0009C69B File Offset: 0x0009A89B
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0009C69E File Offset: 0x0009A89E
		public virtual void Add(JSONNode aItem)
		{
			this.Add("", aItem);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0009C6AC File Offset: 0x0009A8AC
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0009C6AF File Offset: 0x0009A8AF
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0009C6B2 File Offset: 0x0009A8B2
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0009C6B5 File Offset: 0x0009A8B5
		public virtual IEnumerable<JSONNode> Childs
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0009C6BE File Offset: 0x0009A8BE
		public IEnumerable<JSONNode> DeepChilds
		{
			get
			{
				foreach (JSONNode jsonnode in this.Childs)
				{
					foreach (JSONNode jsonnode2 in jsonnode.DeepChilds)
					{
						yield return jsonnode2;
					}
					IEnumerator<JSONNode> enumerator2 = null;
				}
				IEnumerator<JSONNode> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0009C6CE File Offset: 0x0009A8CE
		public override string ToString()
		{
			return "JSONNode";
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0009C6D5 File Offset: 0x0009A8D5
		public virtual string ToString(string aPrefix)
		{
			return "JSONNode";
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0009C6DC File Offset: 0x0009A8DC
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0009C6FD File Offset: 0x0009A8FD
		public virtual int AsInt
		{
			get
			{
				int num = 0;
				if (int.TryParse(this.Value, out num))
				{
					return num;
				}
				return 0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0009C70C File Offset: 0x0009A90C
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x0009C735 File Offset: 0x0009A935
		public virtual float AsFloat
		{
			get
			{
				float num = 0f;
				if (float.TryParse(this.Value, out num))
				{
					return num;
				}
				return 0f;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0009C744 File Offset: 0x0009A944
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x0009C775 File Offset: 0x0009A975
		public virtual double AsDouble
		{
			get
			{
				double num = 0.0;
				if (double.TryParse(this.Value, out num))
				{
					return num;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0009C784 File Offset: 0x0009A984
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x0009C7B2 File Offset: 0x0009A9B2
		public virtual bool AsBool
		{
			get
			{
				bool flag = false;
				if (bool.TryParse(this.Value, out flag))
				{
					return flag;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = (value ? "true" : "false");
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0009C7C9 File Offset: 0x0009A9C9
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0009C7D1 File Offset: 0x0009A9D1
		public virtual JSONClass AsObject
		{
			get
			{
				return this as JSONClass;
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0009C7D9 File Offset: 0x0009A9D9
		public static implicit operator JSONNode(string s)
		{
			return new JSONData(s);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0009C7E1 File Offset: 0x0009A9E1
		public static implicit operator string(JSONNode d)
		{
			if (!(d == null))
			{
				return d.Value;
			}
			return null;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0009C7F4 File Offset: 0x0009A9F4
		public static bool operator ==(JSONNode a, object b)
		{
			return (b == null && a is JSONLazyCreator) || a == b;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0009C807 File Offset: 0x0009AA07
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0009C813 File Offset: 0x0009AA13
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0009C819 File Offset: 0x0009AA19
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0009C824 File Offset: 0x0009AA24
		internal static string Escape(string aText)
		{
			string text = "";
			int i = 0;
			while (i < aText.Length)
			{
				char c = aText[i];
				switch (c)
				{
				case '\b':
					text += "\\b";
					break;
				case '\t':
					text += "\\t";
					break;
				case '\n':
					text += "\\n";
					break;
				case '\v':
					goto IL_00A3;
				case '\f':
					text += "\\f";
					break;
				case '\r':
					text += "\\r";
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_00A3;
						}
						text += "\\\\";
					}
					else
					{
						text += "\\\"";
					}
					break;
				}
				IL_00B1:
				i++;
				continue;
				IL_00A3:
				text += c.ToString();
				goto IL_00B1;
			}
			return text;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0009C8F4 File Offset: 0x0009AAF4
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			string text = "";
			string text2 = "";
			bool flag = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				if (c <= ',')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
						case '\r':
							goto IL_0429;
						case '\v':
						case '\f':
							goto IL_0412;
						default:
							if (c != ' ')
							{
								goto IL_0412;
							}
							break;
						}
						if (flag)
						{
							text += aJSON[i].ToString();
						}
					}
					else if (c != '"')
					{
						if (c != ',')
						{
							goto IL_0412;
						}
						if (flag)
						{
							text += aJSON[i].ToString();
						}
						else
						{
							if (text != "")
							{
								if (jsonnode is JSONArray)
								{
									jsonnode.Add(text);
								}
								else if (text2 != "")
								{
									jsonnode.Add(text2, text);
								}
							}
							text2 = "";
							text = "";
						}
					}
					else
					{
						flag = !flag;
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != ':')
						{
							switch (c)
							{
							case '[':
								if (flag)
								{
									text += aJSON[i].ToString();
									goto IL_0429;
								}
								stack.Push(new JSONArray());
								if (jsonnode != null)
								{
									text2 = text2.Trim();
									if (jsonnode is JSONArray)
									{
										jsonnode.Add(stack.Peek());
									}
									else if (text2 != "")
									{
										jsonnode.Add(text2, stack.Peek());
									}
								}
								text2 = "";
								text = "";
								jsonnode = stack.Peek();
								goto IL_0429;
							case '\\':
								i++;
								if (flag)
								{
									char c2 = aJSON[i];
									if (c2 <= 'f')
									{
										if (c2 == 'b')
										{
											text += "\b";
											goto IL_0429;
										}
										if (c2 == 'f')
										{
											text += "\f";
											goto IL_0429;
										}
									}
									else
									{
										if (c2 == 'n')
										{
											text += "\n";
											goto IL_0429;
										}
										switch (c2)
										{
										case 'r':
											text += "\r";
											goto IL_0429;
										case 't':
											text += "\t";
											goto IL_0429;
										case 'u':
										{
											string text3 = aJSON.Substring(i + 1, 4);
											text += ((char)int.Parse(text3, NumberStyles.AllowHexSpecifier)).ToString();
											i += 4;
											goto IL_0429;
										}
										}
									}
									text += c2.ToString();
									goto IL_0429;
								}
								goto IL_0429;
							case ']':
								break;
							default:
								goto IL_0412;
							}
						}
						else
						{
							if (flag)
							{
								text += aJSON[i].ToString();
								goto IL_0429;
							}
							text2 = text;
							text = "";
							goto IL_0429;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							goto IL_0412;
						}
					}
					else
					{
						if (flag)
						{
							text += aJSON[i].ToString();
							goto IL_0429;
						}
						stack.Push(new JSONClass());
						if (jsonnode != null)
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(stack.Peek());
							}
							else if (text2 != "")
							{
								jsonnode.Add(text2, stack.Peek());
							}
						}
						text2 = "";
						text = "";
						jsonnode = stack.Peek();
						goto IL_0429;
					}
					if (flag)
					{
						text += aJSON[i].ToString();
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (text != "")
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(text);
							}
							else if (text2 != "")
							{
								jsonnode.Add(text2, text);
							}
						}
						text2 = "";
						text = "";
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
				}
				IL_0429:
				i++;
				continue;
				IL_0412:
				text += aJSON[i].ToString();
				goto IL_0429;
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jsonnode;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0009CD4A File Offset: 0x0009AF4A
		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0009CD4C File Offset: 0x0009AF4C
		public void SaveToStream(Stream aData)
		{
			BinaryWriter binaryWriter = new BinaryWriter(aData);
			this.Serialize(binaryWriter);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0009CD67 File Offset: 0x0009AF67
		public void SaveToCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0009CD73 File Offset: 0x0009AF73
		public void SaveToCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0009CD7F File Offset: 0x0009AF7F
		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		public void SaveToFile(string aFileName)
		{
			Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
			using (FileStream fileStream = File.OpenWrite(aFileName))
			{
				this.SaveToStream(fileStream);
			}
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0009CDDC File Offset: 0x0009AFDC
		public string SaveToBase64()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			return text;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0009CE28 File Offset: 0x0009B028
		public static JSONNode Deserialize(BinaryReader aReader)
		{
			JSONBinaryTag jsonbinaryTag = (JSONBinaryTag)aReader.ReadByte();
			switch (jsonbinaryTag)
			{
			case JSONBinaryTag.Array:
			{
				int num = aReader.ReadInt32();
				JSONArray jsonarray = new JSONArray();
				for (int i = 0; i < num; i++)
				{
					jsonarray.Add(JSONNode.Deserialize(aReader));
				}
				return jsonarray;
			}
			case JSONBinaryTag.Class:
			{
				int num2 = aReader.ReadInt32();
				JSONClass jsonclass = new JSONClass();
				for (int j = 0; j < num2; j++)
				{
					string text = aReader.ReadString();
					JSONNode jsonnode = JSONNode.Deserialize(aReader);
					jsonnode.Key = text;
					jsonclass.Add(text, jsonnode);
				}
				return jsonclass;
			}
			case JSONBinaryTag.Value:
				return new JSONData(aReader.ReadString());
			case JSONBinaryTag.IntValue:
				return new JSONData(aReader.ReadInt32());
			case JSONBinaryTag.DoubleValue:
				return new JSONData(aReader.ReadDouble());
			case JSONBinaryTag.BoolValue:
				return new JSONData(aReader.ReadBoolean());
			case JSONBinaryTag.FloatValue:
				return new JSONData(aReader.ReadSingle());
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jsonbinaryTag.ToString());
			}
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0009CF2B File Offset: 0x0009B12B
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0009CF37 File Offset: 0x0009B137
		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0009CF43 File Offset: 0x0009B143
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009CF50 File Offset: 0x0009B150
		public static JSONNode LoadFromStream(Stream aData)
		{
			JSONNode jsonnode;
			using (BinaryReader binaryReader = new BinaryReader(aData))
			{
				jsonnode = JSONNode.Deserialize(binaryReader);
			}
			return jsonnode;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009CF88 File Offset: 0x0009B188
		public static JSONNode LoadFromFile(string aFileName)
		{
			JSONNode jsonnode;
			using (FileStream fileStream = File.OpenRead(aFileName))
			{
				jsonnode = JSONNode.LoadFromStream(fileStream);
			}
			return jsonnode;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0009CFC0 File Offset: 0x0009B1C0
		public static JSONNode LoadFromBase64(string aBase64)
		{
			return JSONNode.LoadFromStream(new MemoryStream(Convert.FromBase64String(aBase64))
			{
				Position = 0L
			});
		}

		// Token: 0x0400105F RID: 4191
		public string Key;
	}
}
