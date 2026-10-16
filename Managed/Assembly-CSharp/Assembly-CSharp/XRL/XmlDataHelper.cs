using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using ConsoleLib.Console;
using UnityEngine;
using XRL.UI;

namespace XRL
{
	/// <summary>
	/// Enhanced XmlTextReader.  Biggest enhancements are the HandleNodes method and DoneWithElement
	/// </summary>
	// Token: 0x02000512 RID: 1298
	[HasModSensitiveStaticCache]
	public class XmlDataHelper : XmlTextReader
	{
		/// <summary>
		/// Create XmlDataHelper from Stream
		/// </summary>
		/// <param name="input">Input stream</param>
		/// <param name="modInfo">Mod (or null for base game)</param>
		/// <returns />
		// Token: 0x06003783 RID: 14211 RVA: 0x0010E94A File Offset: 0x0010CB4A
		public XmlDataHelper(Stream input, ModInfo modInfo = null)
			: base(input)
		{
			this.modInfo = modInfo;
			base.WhitespaceHandling = WhitespaceHandling.None;
		}

		/// <summary>
		/// Create XmlDataHelper from uri
		/// </summary>
		/// <param name="uri">URL</param>
		/// <param name="modInfo">Mod (or null for base game)</param>
		/// <returns />
		// Token: 0x06003784 RID: 14212 RVA: 0x0010E97E File Offset: 0x0010CB7E
		public XmlDataHelper(string uri, ModInfo modInfo = null)
			: base(uri)
		{
			this.modInfo = modInfo;
			base.WhitespaceHandling = WhitespaceHandling.None;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0010E9B2 File Offset: 0x0010CBB2
		public virtual bool IsMod()
		{
			return this.modInfo != null;
		}

		/// <summary>
		/// Logs Exception using mod channel or metrics.
		/// </summary>
		/// <param name="e" />
		// Token: 0x06003786 RID: 14214 RVA: 0x0010E9BD File Offset: 0x0010CBBD
		public void HandleException(Exception e)
		{
			if (this.modInfo != null)
			{
				this.modInfo.Error(e);
				return;
			}
			MetricsManager.LogException(this.GetSourcePoint(), e, "XML_Parse");
		}

		/// <summary>
		/// Quick source code reference to the current reader location.
		/// </summary>
		/// <returns />
		// Token: 0x06003787 RID: 14215 RVA: 0x0010E9E8 File Offset: 0x0010CBE8
		public string GetSourcePoint()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				":: ",
				DataManager.SanitizePathForDisplay(this.BaseURI),
				" line ",
				base.LineNumber.ToString(),
				" char ",
				base.LinePosition.ToString()
			});
		}

		/// <summary>
		/// Generate a parser warning.
		/// </summary>
		/// <param name="msg" />
		// Token: 0x06003788 RID: 14216 RVA: 0x0010EA58 File Offset: 0x0010CC58
		public void ParseWarning(object msg)
		{
			string text = this.GetSourcePoint() + "\n" + ((msg != null) ? msg.ToString() : null);
			if (this.modInfo != null)
			{
				this.modInfo.Warn(text);
				return;
			}
			MetricsManager.LogException(this.GetSourcePoint(), new Exception(msg.ToString()), "XML_Parse");
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x0010EAB4 File Offset: 0x0010CCB4
		public override bool Read()
		{
			this.attributeChecked.Clear();
			bool flag = base.Read();
			if (!this.hasReadFirstElement && this.NodeType == XmlNodeType.Element)
			{
				this.hasReadFirstElement = true;
				this.attributeChecked.Add("LoadPriority");
				this.attributeChecked.Add("Lang");
				string text;
				if (this.TryParseAttribute<string>("Encoding", out text, null))
				{
					this.StringEncoding = text;
				}
			}
			return flag;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x0010EB24 File Offset: 0x0010CD24
		public static void Parse(string path, Dictionary<string, Action<XmlDataHelper>> handlers, bool includeMods = false)
		{
			List<ValueTuple<string, ModInfo>> Paths = new List<ValueTuple<string, ModInfo>>();
			Paths.Add(new ValueTuple<string, ModInfo>(DataManager.FilePath(path), null));
			if (includeMods)
			{
				ModManager.ForEachFile(path, delegate(string modPath, ModInfo modInfo)
				{
					Paths.Add(new ValueTuple<string, ModInfo>(modPath, modInfo));
				}, false, false);
			}
			foreach (ValueTuple<string, ModInfo> valueTuple in Paths)
			{
				string item = valueTuple.Item1;
				ModInfo item2 = valueTuple.Item2;
				using (XmlDataHelper xmlstream = DataManager.GetXMLStream(item, item2))
				{
					xmlstream.HandleNodes(handlers, null);
					xmlstream.Close();
				}
			}
		}

		/// <summary>
		/// Checks if there are any extra unused attributes.  Disabled by sanityChecks false.
		/// </summary>
		// Token: 0x0600378B RID: 14219 RVA: 0x0010EBF0 File Offset: 0x0010CDF0
		public void AssertExtraAttributes()
		{
			if (!this.sanityChecks || this.AttributeCount == 0)
			{
				return;
			}
			for (int i = 0; i < this.AttributeCount; i++)
			{
				this.MoveToAttribute(i);
				if (!this.attributeChecked.Contains(this.Name))
				{
					this.ParseWarning(string.Format("Unused attribute \"{0}\" detected.", this.Name));
				}
			}
			this.MoveToElement();
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x0010EC56 File Offset: 0x0010CE56
		public override string GetAttribute(string name)
		{
			this.attributeChecked.Add(name);
			if (this.StringEncoding == "utf-8")
			{
				return Sidebar.ToCP437(base.GetAttribute(name));
			}
			return base.GetAttribute(name);
		}

		/// <summary>
		///     Parse an attribute as a class name in <paramref name="defaultNameSpace" /> and return the type.
		/// </summary>
		// Token: 0x0600378D RID: 14221 RVA: 0x0010EC8C File Offset: 0x0010CE8C
		public virtual Type ParseType(string attribute, string defaultNameSpace, bool required = false)
		{
			return this.ParseAttribute<Type>(attribute, null, required, delegate(string value)
			{
				Type type = ModManager.ResolveType(defaultNameSpace, value, false, false, true);
				if (type == null)
				{
					throw new Exception(string.Concat(new string[] { "Error finding ", defaultNameSpace, ".", value, " or ", value, " Type." }));
				}
				return type;
			});
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0010ECBC File Offset: 0x0010CEBC
		public bool TryParseAttribute<ResultType>(string attribute, out ResultType result, XmlDataHelper.AttributeParser<ResultType>.ParseDelegate parse = null)
		{
			result = default(ResultType);
			if (parse == null)
			{
				XmlDataHelper.AttributeParser<ResultType> attributeParser = XmlDataHelper.TryGetAttributeParser<ResultType>();
				if (attributeParser != null)
				{
					parse = new XmlDataHelper.AttributeParser<ResultType>.ParseDelegate(attributeParser.Invoke);
				}
				if (parse == null)
				{
					throw new Exception("No default parser for type " + typeof(ResultType).FullName);
				}
			}
			string attribute2 = this.GetAttribute(attribute);
			if (attribute2 == null)
			{
				return false;
			}
			bool flag;
			try
			{
				result = parse(attribute2);
				flag = true;
			}
			catch (Exception ex)
			{
				this.HandleException(new Exception(string.Concat(new string[] { "Error parsing attribute ", attribute, "=\"", attribute2, "\":" }), ex));
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0010ED78 File Offset: 0x0010CF78
		public virtual ResultType ParseAttribute<ResultType>(string attribute, ResultType defaultValue, bool required = false, XmlDataHelper.AttributeParser<ResultType>.ParseDelegate parse = null)
		{
			ResultType resultType;
			if (this.TryParseAttribute<ResultType>(attribute, out resultType, parse))
			{
				return resultType;
			}
			if (required)
			{
				this.ParseWarning("Required attribute \"" + attribute + "\" missing");
			}
			return defaultValue;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0010EDAE File Offset: 0x0010CFAE
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttribute(name) != null;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0010EDBA File Offset: 0x0010CFBA
		public virtual string GetAttributeString(string name, string defaultValue, bool required = false)
		{
			return this.ParseAttribute<string>(name, defaultValue, required, null);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0010EDC6 File Offset: 0x0010CFC6
		public virtual int GetAttributeInt(string name, int defaultValue, bool required = false)
		{
			return this.ParseAttribute<int>(name, defaultValue, required, null);
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0010EDD2 File Offset: 0x0010CFD2
		public virtual bool GetAttributeBool(string name, bool defaultValue, bool required = false)
		{
			return this.ParseAttribute<bool>(name, defaultValue, required, null);
		}

		/// <summary>
		/// Current XML element is done. Ensure a self-closing tag, or otherwise empty tag.  Advances the reader past the current node.
		/// </summary>
		// Token: 0x06003794 RID: 14228 RVA: 0x0010EDDE File Offset: 0x0010CFDE
		public void DoneWithElement()
		{
			this.HandleNodes(XmlDataHelper.noNodesExpected, null);
		}

		/// <summary>
		/// Reads the value of the text node, moves past this node, do not need to call DoneWithElement.
		/// </summary>
		/// <returns>the trimmed up value of the text node</returns>
		// Token: 0x06003795 RID: 14229 RVA: 0x0010EDEC File Offset: 0x0010CFEC
		public string GetTextNode()
		{
			this.AssertExtraAttributes();
			string name = this.Name;
			this.Read();
			string text = null;
			if (this.NodeType == XmlNodeType.Text)
			{
				text = ((this.StringEncoding == "utf-8") ? Sidebar.ToCP437(this.Value) : this.Value);
				text = XmlDataHelper.TrimSpacePerLine.Replace(text, "");
				text = XmlDataHelper.TrimNewlineRegex.Replace(text, "");
			}
			else
			{
				this.ParseWarning("Unexpected node type: " + this.NodeType.ToString() + " expecting Text.");
			}
			this.Read();
			if (this.NodeType != XmlNodeType.EndElement || this.Name != name)
			{
				this.ParseWarning("Expected closing tag for " + name + ".");
			}
			return text;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0010EEC4 File Offset: 0x0010D0C4
		public bool TryParseTextNode<T>(out T Value, bool FailOnEmpty = true, bool FailOnWhiteSpace = true)
		{
			string textNode = this.GetTextNode();
			if (textNode == null || (FailOnEmpty && textNode == "") || (FailOnWhiteSpace && string.IsNullOrWhiteSpace(textNode)))
			{
				Value = default(T);
				return false;
			}
			return this.TryParse<T>(textNode, out Value);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x0010EF08 File Offset: 0x0010D108
		public bool TryParse<T>(string Text, out T Value)
		{
			try
			{
				XmlDataHelper.AttributeParser attributeParser = XmlDataHelper.TryGetAttributeParser(typeof(T));
				if (attributeParser != null)
				{
					Value = (T)((object)attributeParser.Invoke(Text));
					return true;
				}
			}
			catch (Exception ex)
			{
				this.HandleException(new Exception("Error parsing text \"" + Text + "\":", ex));
			}
			Value = default(T);
			return false;
		}

		/// <summary>
		/// Handle children nodes of the current node given a dictionary of node name to Action handler.
		/// Advances the reader past the current node.
		/// </summary>
		/// <param name="nodeHandlers">Map of xml node names to action handlers</param>
		/// <param name="unknownNodeHandler">A fallback action to handle unknown nodes.</param>
		// Token: 0x06003798 RID: 14232 RVA: 0x0010EF7C File Offset: 0x0010D17C
		public void HandleNodes(IDictionary<string, Action<XmlDataHelper>> nodeHandlers = null, Action<XmlDataHelper> unknownNodeHandler = null)
		{
			this.AssertExtraAttributes();
			string name = this.Name;
			if (this.IsEmptyElement || this.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			Action<XmlDataHelper> action = null;
			while (this.Read())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType <= XmlNodeType.Text)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (nodeHandlers != null && nodeHandlers.TryGetValue(this.Name, out action))
						{
							try
							{
								action(this);
								continue;
							}
							catch (Exception ex)
							{
								this.HandleException(ex);
								throw ex;
							}
						}
						if (unknownNodeHandler != null)
						{
							try
							{
								unknownNodeHandler(this);
								continue;
							}
							catch (Exception ex2)
							{
								this.HandleException(ex2);
								throw ex2;
							}
						}
						this.ParseWarning(string.Format("Unexpected \"{0}\" node", this.Name));
						continue;
					}
					if (nodeType == XmlNodeType.Text)
					{
						if (nodeHandlers.TryGetValue("text", out action))
						{
							try
							{
								action(this);
								continue;
							}
							catch (Exception ex3)
							{
								this.HandleException(ex3);
								throw ex3;
							}
						}
						this.ParseWarning("Unexpected text node");
						continue;
					}
				}
				else
				{
					if (nodeType == XmlNodeType.Comment)
					{
						continue;
					}
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
					case XmlNodeType.XmlDeclaration:
						continue;
					case XmlNodeType.EndElement:
						if (this.Name == name)
						{
							return;
						}
						this.ParseWarning(string.Format("Unexpected EndElement for \"{0}\"", this.Name));
						continue;
					}
				}
				this.ParseWarning("Unexpected node type: " + this.NodeType.ToString());
			}
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0010F104 File Offset: 0x0010D304
		public static void AddAttributeParser(Type T, XmlDataHelper.AttributeParser parser)
		{
			XmlDataHelper.AttributeParsers.Set(T, parser);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0010F114 File Offset: 0x0010D314
		public static void AddDefaultParsers()
		{
			XmlDataHelper.AddAttributeParser(typeof(bool), new XmlDataHelper.AttributeParser<bool>
			{
				Parse = new XmlDataHelper.AttributeParser<bool>.ParseDelegate(Convert.ToBoolean)
			});
			Type typeFromHandle = typeof(bool?);
			XmlDataHelper.AttributeParser<bool?> attributeParser = new XmlDataHelper.AttributeParser<bool?>();
			attributeParser.Parse = delegate(string s)
			{
				if (s != null)
				{
					return new bool?(Convert.ToBoolean(s));
				}
				return null;
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle, attributeParser);
			XmlDataHelper.AddAttributeParser(typeof(double), new XmlDataHelper.AttributeParser<double>
			{
				Parse = new XmlDataHelper.AttributeParser<double>.ParseDelegate(Convert.ToDouble)
			});
			XmlDataHelper.AddAttributeParser(typeof(short), new XmlDataHelper.AttributeParser<short>
			{
				Parse = new XmlDataHelper.AttributeParser<short>.ParseDelegate(short.Parse)
			});
			XmlDataHelper.AddAttributeParser(typeof(int), new XmlDataHelper.AttributeParser<int>
			{
				Parse = new XmlDataHelper.AttributeParser<int>.ParseDelegate(int.Parse)
			});
			XmlDataHelper.AddAttributeParser(typeof(long), new XmlDataHelper.AttributeParser<long>
			{
				Parse = new XmlDataHelper.AttributeParser<long>.ParseDelegate(long.Parse)
			});
			XmlDataHelper.AddAttributeParser(typeof(float), new XmlDataHelper.AttributeParser<float>
			{
				Parse = new XmlDataHelper.AttributeParser<float>.ParseDelegate(Convert.ToSingle)
			});
			Type typeFromHandle2 = typeof(char);
			XmlDataHelper.AttributeParser<char> attributeParser2 = new XmlDataHelper.AttributeParser<char>();
			attributeParser2.Parse = delegate(string s)
			{
				if (s != null)
				{
					return s[0];
				}
				return '\0';
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle2, attributeParser2);
			Type typeFromHandle3 = typeof(string);
			XmlDataHelper.AttributeParser<string> attributeParser3 = new XmlDataHelper.AttributeParser<string>();
			attributeParser3.Parse = (string s) => string.Intern(s.Replace("\r", ""));
			XmlDataHelper.AddAttributeParser(typeFromHandle3, attributeParser3);
			Type typeFromHandle4 = typeof(object);
			XmlDataHelper.AttributeParser<object> attributeParser4 = new XmlDataHelper.AttributeParser<object>();
			attributeParser4.Parse = (string s) => s;
			XmlDataHelper.AddAttributeParser(typeFromHandle4, attributeParser4);
			Type typeFromHandle5 = typeof(List<string>);
			XmlDataHelper.AttributeParser<List<string>> attributeParser5 = new XmlDataHelper.AttributeParser<List<string>>();
			attributeParser5.Parse = delegate(string s)
			{
				if (s != null)
				{
					return new List<string>(s.Split(',', StringSplitOptions.None));
				}
				return null;
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle5, attributeParser5);
			Type typeFromHandle6 = typeof(CapabilityManager.CapabilityType?);
			XmlDataHelper.AttributeParser<CapabilityManager.CapabilityType?> attributeParser6 = new XmlDataHelper.AttributeParser<CapabilityManager.CapabilityType?>();
			attributeParser6.Parse = delegate(string s)
			{
				if (string.IsNullOrEmpty(s))
				{
					return null;
				}
				return new CapabilityManager.CapabilityType?(Enum.Parse<CapabilityManager.CapabilityType>(s));
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle6, attributeParser6);
			Type typeFromHandle7 = typeof(Color);
			XmlDataHelper.AttributeParser<Color> attributeParser7 = new XmlDataHelper.AttributeParser<Color>();
			attributeParser7.Parse = delegate(string s)
			{
				Exception ex = null;
				try
				{
					if (s[0] == '#')
					{
						return ConsoleLib.Console.ColorUtility.FromWebColor(s.Substring(1));
					}
					if (s.Contains(','))
					{
						string[] array = s.Split(',', StringSplitOptions.None);
						return new Color(Convert.ToSingle(array[0]), Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
					}
					if (s.Length == 6)
					{
						return ConsoleLib.Console.ColorUtility.FromWebColor(s);
					}
				}
				catch (Exception ex)
				{
				}
				throw new Exception("Could not figure out color format.  Supported formats are #rrggbb hex style; rrr,ggg,bbb from 0 -> 1", ex);
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle7, attributeParser7);
			Type typeFromHandle8 = typeof(MethodInfo);
			XmlDataHelper.AttributeParser<MethodInfo> attributeParser8 = new XmlDataHelper.AttributeParser<MethodInfo>();
			attributeParser8.Parse = delegate(string value)
			{
				int num = value.LastIndexOf('.');
				if (num == -1)
				{
					throw new ArgumentException("No path for type and namespace was specified for method '" + value + "'");
				}
				Type type = ModManager.ResolveType(value.Substring(0, num), false, true, true);
				MethodInfo method = type.GetMethod(value.Substring(num + 1));
				if (method == null)
				{
					throw new KeyNotFoundException(string.Concat(new string[] { "No method by name '", value, "' was found for type '", type.FullName, "'" }));
				}
				return method;
			};
			XmlDataHelper.AddAttributeParser(typeFromHandle8, attributeParser8);
		}

		/// <summary>
		///     Looks in the cache of attribute parsers for the specific Type <typeparam name="T" />.
		///     If not found in the cache, will attempt to generate one for enums, and will look for
		///     <see cref="T:XRL.XmlDataHelper.AttributeParserAttribute" /> static method on the type.
		/// </summary>
		// Token: 0x0600379B RID: 14235 RVA: 0x0010F3CD File Offset: 0x0010D5CD
		public static XmlDataHelper.AttributeParser<T> TryGetAttributeParser<T>()
		{
			XmlDataHelper.AttributeParser attributeParser = XmlDataHelper.TryGetAttributeParser(typeof(T));
			if (attributeParser == null)
			{
				return null;
			}
			return attributeParser.AsGeneric<T>();
		}

		/// <summary>
		///     Looks in the cache of attribute parsers for the specific Type <paramref name="T" />.
		///     If not found in the cache, will attempt to generate one for enums, and will look for
		///     <see cref="T:XRL.XmlDataHelper.AttributeParserAttribute" /> static method on the type.
		/// </summary>
		// Token: 0x0600379C RID: 14236 RVA: 0x0010F3EC File Offset: 0x0010D5EC
		public static XmlDataHelper.AttributeParser TryGetAttributeParser(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (XmlDataHelper.AttributeParsers.Count == 0)
			{
				XmlDataHelper.AddDefaultParsers();
			}
			XmlDataHelper.AttributeParser attributeParser;
			if (XmlDataHelper.AttributeParsers.TryGetValue(type, out attributeParser))
			{
				return attributeParser;
			}
			if (type.IsEnum)
			{
				XmlDataHelper.AttributeParser attributeParser2 = new XmlDataHelper.AttributeParser
				{
					Parse = (string propertyValue) => Enum.Parse(type, propertyValue)
				};
				XmlDataHelper.AddAttributeParser(type, attributeParser2);
				return attributeParser2;
			}
			MethodInfo reflectedParser = type.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault<MethodInfo>((MethodInfo methodInfo) => methodInfo.CustomAttributes.Any<CustomAttributeData>((CustomAttributeData s) => s.AttributeType.IsAssignableFrom(typeof(XmlDataHelper.AttributeParserAttribute))) && methodInfo.ReturnType == type);
			if (reflectedParser != null)
			{
				object[] @params = new object[1];
				XmlDataHelper.AttributeParser attributeParser3 = new XmlDataHelper.AttributeParser
				{
					Parse = delegate(string s)
					{
						@params[0] = s;
						return reflectedParser.Invoke(null, @params);
					}
				};
				XmlDataHelper.AddAttributeParser(type, attributeParser3);
				return attributeParser3;
			}
			foreach (KeyValuePair<Type, XmlDataHelper.AttributeParser> keyValuePair in XmlDataHelper.AttributeParsers)
			{
				if (type.IsAssignableFrom(keyValuePair.Key))
				{
					MetricsManager.LogWarning("Making a parser for " + type.FullName + " from " + keyValuePair.Key.FullName);
					XmlDataHelper.AddAttributeParser(type, keyValuePair.Value);
					return keyValuePair.Value;
				}
			}
			XmlDataHelper.AddAttributeParser(type, null);
			return null;
		}

		/// <summary>
		/// The mod responsible for this stream input.
		/// </summary>
		// Token: 0x04001BDA RID: 7130
		public readonly ModInfo modInfo;

		/// <summary>
		/// Perform (additional) sanity checks.
		/// </summary>
		// Token: 0x04001BDB RID: 7131
		public bool sanityChecks = true;

		// Token: 0x04001BDC RID: 7132
		public string StringEncoding = "cp437";

		// Token: 0x04001BDD RID: 7133
		public static readonly Regex TrimSpacePerLine = new Regex("^[ \\t\\r]+|[ \\t\\r]+$", RegexOptions.Multiline);

		// Token: 0x04001BDE RID: 7134
		public static readonly Regex TrimNewlineRegex = new Regex("^\\n+|\\n+$");

		// Token: 0x04001BDF RID: 7135
		private bool hasReadFirstElement;

		// Token: 0x04001BE0 RID: 7136
		protected HashSet<string> attributeChecked = new HashSet<string>();

		// Token: 0x04001BE1 RID: 7137
		private static readonly Dictionary<string, Action<XmlDataHelper>> noNodesExpected = new Dictionary<string, Action<XmlDataHelper>>();

		// Token: 0x04001BE2 RID: 7138
		[ModSensitiveStaticCache(true)]
		private static Dictionary<Type, XmlDataHelper.AttributeParser> AttributeParsers = new Dictionary<Type, XmlDataHelper.AttributeParser>();

		/// <summary>
		///     Abstract class to let us hold a dictionary of the <see cref="T:XRL.XmlDataHelper.AttributeParser`1" />.
		/// </summary>
		// Token: 0x02001933 RID: 6451
		public class AttributeParser
		{
			// Token: 0x0600E2AE RID: 58030 RVA: 0x005253EF File Offset: 0x005235EF
			public AttributeParser()
			{
			}

			// Token: 0x0600E2AF RID: 58031 RVA: 0x005253F7 File Offset: 0x005235F7
			public AttributeParser(XmlDataHelper.AttributeParser parser)
			{
				this._Parse = parser._Parse;
			}

			// Token: 0x17000E56 RID: 3670
			// (get) Token: 0x0600E2B0 RID: 58032 RVA: 0x0052540B File Offset: 0x0052360B
			// (set) Token: 0x0600E2B1 RID: 58033 RVA: 0x00525413 File Offset: 0x00523613
			public XmlDataHelper.AttributeParser.ParseDelegate Parse
			{
				get
				{
					return this._Parse;
				}
				set
				{
					this._Parse = value;
				}
			}

			// Token: 0x0600E2B2 RID: 58034 RVA: 0x0052541C File Offset: 0x0052361C
			public object Invoke(string s)
			{
				return this._Parse(s);
			}

			// Token: 0x0600E2B3 RID: 58035 RVA: 0x0052542C File Offset: 0x0052362C
			public XmlDataHelper.AttributeParser<T> AsGeneric<T>()
			{
				XmlDataHelper.AttributeParser<T> attributeParser = this as XmlDataHelper.AttributeParser<T>;
				if (attributeParser != null)
				{
					return attributeParser;
				}
				return new XmlDataHelper.AttributeParser<T>(this);
			}

			// Token: 0x04006531 RID: 25905
			public XmlDataHelper.AttributeParser.ParseDelegate _Parse;

			// Token: 0x02001E83 RID: 7811
			// (Invoke) Token: 0x0600F799 RID: 63385
			public delegate object ParseDelegate(string value);
		}

		/// <summary>
		///     A holder for a <see cref="P:XRL.XmlDataHelper.AttributeParser`1.Parse" /> function that takes a <see cref="T:System.String" /> and returns <see cref="!:AttributeType" />.
		/// </summary>
		// Token: 0x02001934 RID: 6452
		public class AttributeParser<AttributeType> : XmlDataHelper.AttributeParser
		{
			// Token: 0x0600E2B4 RID: 58036 RVA: 0x0052544B File Offset: 0x0052364B
			public AttributeParser()
			{
			}

			// Token: 0x0600E2B5 RID: 58037 RVA: 0x00525453 File Offset: 0x00523653
			public AttributeParser(XmlDataHelper.AttributeParser parser)
				: base(parser)
			{
			}

			// Token: 0x17000E57 RID: 3671
			// (set) Token: 0x0600E2B6 RID: 58038 RVA: 0x0052545C File Offset: 0x0052365C
			public new XmlDataHelper.AttributeParser<AttributeType>.ParseDelegate Parse
			{
				set
				{
					this._Parse = (string str) => value(str);
				}
			}

			// Token: 0x0600E2B7 RID: 58039 RVA: 0x00525488 File Offset: 0x00523688
			public new AttributeType Invoke(string s)
			{
				return (AttributeType)((object)this._Parse(s));
			}

			// Token: 0x02001E84 RID: 7812
			// (Invoke) Token: 0x0600F79D RID: 63389
			public new delegate AttributeType ParseDelegate(string value);
		}

		/// <summary>
		///     Add to a static method on a class to provide a method to return an instance from a string.
		/// </summary>
		// Token: 0x02001935 RID: 6453
		[AttributeUsage(AttributeTargets.Method)]
		public class AttributeParserAttribute : Attribute
		{
			// Token: 0x0600E2B8 RID: 58040 RVA: 0x0052549B File Offset: 0x0052369B
			public AttributeParserAttribute(Type T)
			{
				this.type = T;
			}

			// Token: 0x04006532 RID: 25906
			public readonly Type type;
		}
	}
}
