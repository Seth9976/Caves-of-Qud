using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000620 RID: 1568
	internal class AspGenerator
	{
		// Token: 0x0600431F RID: 17183 RVA: 0x000B12E8 File Offset: 0x000AF4E8
		public AspGenerator(TemplateParser tparser, AspComponentFoundry componentFoundry)
			: this(tparser)
		{
			this.componentFoundry = componentFoundry;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000B12F8 File Offset: 0x000AF4F8
		public AspGenerator(TemplateParser tparser)
		{
			this.tparser = tparser;
			this.text = new StringBuilder();
			this.stack = new BuilderLocationStack();
			this.pstack = new ParserStack();
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x000B1333 File Offset: 0x000AF533
		public RootBuilder RootBuilder
		{
			get
			{
				return this.rootBuilder;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06004322 RID: 17186 RVA: 0x000B133B File Offset: 0x000AF53B
		public AspParser Parser
		{
			get
			{
				return this.pstack.Parser;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x000B1348 File Offset: 0x000AF548
		public string Filename
		{
			get
			{
				return this.pstack.Filename;
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x000B1355 File Offset: 0x000AF555
		private PageParserFilter PageParserFilter
		{
			get
			{
				if (this.tparser == null)
				{
					return null;
				}
				return this.tparser.PageParserFilter;
			}
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000B136C File Offset: 0x000AF56C
		private IDictionary GetDirectiveAttributesDictionary(string skipKeyName, CaptureCollection names, CaptureCollection values)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			int num = 0;
			foreach (object obj in names)
			{
				Capture capture = (Capture)obj;
				string value = capture.Value;
				if (string.Compare(skipKeyName, value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					num++;
				}
				else
				{
					dictionary.Add(capture.Value, values[num++].Value);
				}
			}
			return dictionary;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000B1400 File Offset: 0x000AF600
		private string GetDirectiveName(CaptureCollection names)
		{
			foreach (object obj in names)
			{
				string value = ((Capture)obj).Value;
				if (Directive.IsDirective(value))
				{
					return value;
				}
			}
			return this.tparser.DefaultDirectiveName;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000B146C File Offset: 0x000AF66C
		private int GetLineNumberForIndex(string fileContents, int index)
		{
			int num = 1;
			bool flag = false;
			for (int i = 0; i < index; i++)
			{
				char c = fileContents[i];
				if (c == '\n' || flag)
				{
					num++;
				}
				flag = c == '\r';
			}
			return num;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000B14A8 File Offset: 0x000AF6A8
		private int GetNumberOfLinesForRange(string fileContents, int index, int length)
		{
			int num = 0;
			int num2 = index + length;
			bool flag = false;
			for (int i = index; i < num2; i++)
			{
				char c = fileContents[i];
				if (c == '\n' || flag)
				{
					num++;
				}
				flag = c == '\r';
			}
			return num;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000B14E8 File Offset: 0x000AF6E8
		private Type GetInheritedType(string fileContents, string filename)
		{
			MatchCollection matchCollection = AspGenerator.DirectiveRegex.Matches(fileContents);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return null;
			}
			string text = this.tparser.DefaultDirectiveName.ToLower(Helpers.InvariantCulture);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				GroupCollection groups = match.Groups;
				if (groups.Count >= 6)
				{
					CaptureCollection captures = groups[3].Captures;
					string directiveName = this.GetDirectiveName(captures);
					if (!string.IsNullOrEmpty(directiveName) && string.Compare(directiveName.ToLower(Helpers.InvariantCulture), text, StringComparison.Ordinal) == 0)
					{
						Location location = new Location(null);
						int index = match.Index;
						location.Filename = filename;
						location.BeginLine = this.GetLineNumberForIndex(fileContents, index);
						location.EndLine = location.BeginLine + this.GetNumberOfLinesForRange(fileContents, index, match.Length);
						this.tparser.Location = location;
						this.tparser.allowedMainDirectives = 2;
						this.tparser.AddDirective(text, this.GetDirectiveAttributesDictionary(text, captures, groups[5].Captures));
						return this.tparser.BaseType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000B1654 File Offset: 0x000AF854
		private string ReadFileContents(Stream inputStream, string filename)
		{
			string text = null;
			if (inputStream != null)
			{
				if (inputStream.CanSeek)
				{
					long position = inputStream.Position;
					inputStream.Seek(0L, SeekOrigin.Begin);
					Encoding fileEncoding = WebEncoding.FileEncoding;
					StringBuilder stringBuilder = new StringBuilder();
					byte[] array = new byte[8192];
					int num;
					while ((num = inputStream.Read(array, 0, 8192)) > 0)
					{
						stringBuilder.Append(fileEncoding.GetString(array, 0, num));
					}
					inputStream.Seek(position, SeekOrigin.Begin);
					text = stringBuilder.ToString();
					stringBuilder.Length = 0;
					stringBuilder.Capacity = 0;
				}
				else
				{
					FileStream fileStream = inputStream as FileStream;
					if (fileStream != null)
					{
						string name = fileStream.Name;
						try
						{
							if (File.Exists(name))
							{
								text = File.ReadAllText(name);
							}
						}
						catch
						{
						}
					}
				}
			}
			if (text == null && !string.IsNullOrEmpty(filename) && string.Compare(filename, "@@inner_string@@", StringComparison.Ordinal) != 0)
			{
				try
				{
					if (File.Exists(filename))
					{
						text = File.ReadAllText(filename);
					}
				}
				catch
				{
				}
			}
			return text;
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x000B1758 File Offset: 0x000AF958
		private Type GetRootBuilderType(Stream inputStream, string filename)
		{
			Type type = null;
			string text;
			if (this.tparser != null)
			{
				text = this.ReadFileContents(inputStream, filename);
			}
			else
			{
				text = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				Type inheritedType = this.GetInheritedType(text, filename);
				if (inheritedType != null)
				{
					FileLevelControlBuilderAttribute fileLevelControlBuilderAttribute;
					try
					{
						object[] customAttributes = inheritedType.GetCustomAttributes(typeof(FileLevelControlBuilderAttribute), true);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							fileLevelControlBuilderAttribute = customAttributes[0] as FileLevelControlBuilderAttribute;
						}
						else
						{
							fileLevelControlBuilderAttribute = null;
						}
					}
					catch
					{
						fileLevelControlBuilderAttribute = null;
					}
					type = ((fileLevelControlBuilderAttribute != null) ? fileLevelControlBuilderAttribute.BuilderType : null);
				}
			}
			if (!(type == null))
			{
				return type;
			}
			if (this.tparser is PageParser)
			{
				return typeof(FileLevelPageControlBuilder);
			}
			if (this.tparser is UserControlParser)
			{
				return typeof(FileLevelUserControlBuilder);
			}
			return typeof(RootBuilder);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000B182C File Offset: 0x000AFA2C
		private void CreateRootBuilder(Stream inputStream, string filename)
		{
			if (this.rootBuilder != null)
			{
				return;
			}
			Type rootBuilderType = this.GetRootBuilderType(inputStream, filename);
			this.rootBuilder = Activator.CreateInstance(rootBuilderType) as RootBuilder;
			if (this.rootBuilder == null)
			{
				throw new HttpException("Cannot create an instance of file-level control builder.");
			}
			this.rootBuilder.Init(this.tparser, null, null, null, null, null);
			if (this.componentFoundry != null)
			{
				this.rootBuilder.Foundry = this.componentFoundry;
			}
			this.stack.Push(this.rootBuilder, null);
			this.tparser.RootBuilder = this.rootBuilder;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000B18C4 File Offset: 0x000AFAC4
		private BaseCompiler GetCompilerFromType()
		{
			Type type = this.tparser.GetType();
			if (type == typeof(PageParser))
			{
				return new PageCompiler((PageParser)this.tparser);
			}
			if (type == typeof(ApplicationFileParser))
			{
				return new GlobalAsaxCompiler((ApplicationFileParser)this.tparser);
			}
			if (type == typeof(UserControlParser))
			{
				return new UserControlCompiler((UserControlParser)this.tparser);
			}
			if (type == typeof(MasterPageParser))
			{
				return new MasterPageCompiler((MasterPageParser)this.tparser);
			}
			string text = "Got type: ";
			Type type2 = type;
			throw new Exception(text + ((type2 != null) ? type2.ToString() : null));
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000B1988 File Offset: 0x000AFB88
		private void InitParser(TextReader reader, string filename)
		{
			AspParser aspParser = new AspParser(filename, reader);
			aspParser.Error += this.ParseError;
			aspParser.TagParsed += this.TagParsed;
			aspParser.TextParsed += this.TextParsed;
			aspParser.ParsingComplete += this.ParsingCompleted;
			this.tparser.AspGenerator = this;
			this.CreateRootBuilder(this.inputStream, filename);
			if (!this.pstack.Push(aspParser))
			{
				throw new ParseException(this.Location, "Infinite recursion detected including file: " + filename);
			}
			if (filename != "@@inner_string@@")
			{
				string text = Path.Combine(this.tparser.BaseVirtualDir, Path.GetFileName(filename));
				if (VirtualPathUtility.IsAbsolute(text))
				{
					text = VirtualPathUtility.ToAppRelative(text);
				}
				this.tparser.AddDependency(text);
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000B1A64 File Offset: 0x000AFC64
		private void InitParser(string filename)
		{
			StreamReader streamReader = new StreamReader(filename, WebEncoding.FileEncoding);
			this.InitParser(streamReader, filename);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000B1A88 File Offset: 0x000AFC88
		private void CheckForDuplicateIds(ControlBuilder root, Stack scopes)
		{
			if (root == null)
			{
				return;
			}
			if (scopes == null)
			{
				scopes = new Stack();
			}
			Dictionary<string, bool> dictionary;
			if (scopes.Count == 0 || root.IsNamingContainer)
			{
				dictionary = new Dictionary<string, bool>(StringComparer.Ordinal);
				scopes.Push(dictionary);
			}
			else
			{
				dictionary = scopes.Peek() as Dictionary<string, bool>;
			}
			if (dictionary == null)
			{
				return;
			}
			ArrayList children = root.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					ControlBuilder controlBuilder = obj as ControlBuilder;
					if (controlBuilder != null)
					{
						string id = controlBuilder.ID;
						if (id != null && id.Length != 0)
						{
							if (dictionary.ContainsKey(id))
							{
								throw new ParseException(controlBuilder.Location, "Id '" + id + "' is already used by another control.");
							}
							dictionary.Add(id, true);
							this.CheckForDuplicateIds(controlBuilder, scopes);
						}
					}
				}
			}
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000B1B78 File Offset: 0x000AFD78
		public void Parse(string file)
		{
			this.Parse(file, false);
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000B1B84 File Offset: 0x000AFD84
		public void Parse(TextReader reader, string filename, bool doInitParser)
		{
			try
			{
				this.isApplication = this.tparser.DefaultDirectiveName == "application";
				if (doInitParser)
				{
					this.InitParser(reader, filename);
				}
				this.pstack.Parser.Parse();
				if (this.text.Length > 0)
				{
					this.FlushText();
				}
				this.tparser.MD5Checksum = this.pstack.Parser.MD5Checksum;
				this.pstack.Pop();
				if (this.stack.Count > 1 && this.pstack.Count == 0)
				{
					ILocation location = this.stack.Builder.Location;
					string text = "Expecting </";
					string tagName = this.stack.Builder.TagName;
					string text2 = "> ";
					ControlBuilder builder = this.stack.Builder;
					throw new ParseException(location, text + tagName + text2 + ((builder != null) ? builder.ToString() : null));
				}
				this.CheckForDuplicateIds(this.RootBuilder, null);
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000B1C94 File Offset: 0x000AFE94
		public void Parse(Stream stream, string filename, bool doInitParser)
		{
			this.inputStream = stream;
			this.Parse(new StreamReader(stream, WebEncoding.FileEncoding), filename, doInitParser);
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000B1CB0 File Offset: 0x000AFEB0
		public void Parse(string filename, bool doInitParser)
		{
			StreamReader streamReader = new StreamReader(filename, WebEncoding.FileEncoding);
			this.Parse(streamReader, filename, doInitParser);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000B1CD4 File Offset: 0x000AFED4
		public void Parse()
		{
			string text = this.tparser.InputFile;
			TextReader reader = this.tparser.Reader;
			try
			{
				if (string.IsNullOrEmpty(text))
				{
					StreamReader streamReader = reader as StreamReader;
					if (streamReader != null)
					{
						FileStream fileStream = streamReader.BaseStream as FileStream;
						if (fileStream != null)
						{
							text = fileStream.Name;
						}
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "@@inner_string@@";
					}
				}
				if (reader != null)
				{
					this.Parse(reader, text, true);
				}
				else
				{
					if (string.IsNullOrEmpty(text))
					{
						throw new HttpException("Parser input file is empty, cannot continue.");
					}
					text = Path.GetFullPath(text);
					this.InitParser(text);
					this.Parse(text);
				}
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000B1D84 File Offset: 0x000AFF84
		internal static void AddTypeToCache(List<string> dependencies, string inputFile, Type type)
		{
			if (type == null || inputFile == null || inputFile.Length == 0)
			{
				return;
			}
			if (dependencies == null || dependencies.Count <= 0)
			{
				HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type);
				return;
			}
			string[] array = dependencies.ToArray();
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest == null)
			{
				throw new HttpException("No current context, cannot compile.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = httpRequest.MapPath(array[i]);
			}
			HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type, new CacheDependency(array));
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000B1E28 File Offset: 0x000B0028
		public Type GetCompiledType()
		{
			Type type = (Type)HttpRuntime.InternalCache.Get("@@Type" + this.tparser.InputFile);
			if (type != null)
			{
				return type;
			}
			this.Parse();
			type = this.GetCompilerFromType().GetCompiledType();
			AspGenerator.AddTypeToCache(this.tparser.Dependencies, this.tparser.InputFile, type);
			return type;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x00039AB0 File Offset: 0x00037CB0
		private void ParseError(ILocation location, string message)
		{
			throw new ParseException(location, message);
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x000B1E94 File Offset: 0x000B0094
		private bool ProcessTagsInAttributes(ILocation location, string tagid, TagAttributes attributes, TagType type)
		{
			if (attributes == null || attributes.Count == 0)
			{
				return false;
			}
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\t<{0}", tagid);
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				string text2 = attributes[text] as string;
				if (text2 == null || text2.Length < 16)
				{
					stringBuilder.AppendFormat(" {0}=\"{1}\"", text, text2);
				}
				else
				{
					Match match = AspGenerator.runatServer.Match(attributes[text] as string);
					if (!match.Success)
					{
						stringBuilder.AppendFormat(" {0}=\"{1}\"", text, text2);
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							this.TextParsed(location, stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
						flag = true;
						Group group = match.Groups[0];
						int index = group.Index;
						int length = group.Length;
						this.TextParsed(location, string.Format(" {0}=\"{1}", text, (index > 0) ? text2.Substring(0, index) : string.Empty));
						this.FlushText();
						this.ParseAttributeTag(group.Value, location);
						if (index + length < text2.Length)
						{
							this.TextParsed(location, text2.Substring(index + length) + "\"");
						}
						else
						{
							this.TextParsed(location, "\"");
						}
					}
				}
			}
			if (type == TagType.SelfClosing)
			{
				stringBuilder.Append("/>");
			}
			else
			{
				stringBuilder.Append(">");
			}
			if (flag && stringBuilder.Length > 0)
			{
				this.TextParsed(location, stringBuilder.ToString());
			}
			return flag;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x000B2078 File Offset: 0x000B0278
		private void ParseAttributeTag(string code, ILocation location)
		{
			AspParser aspParser = location as AspParser;
			int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
			AspParser aspParser2 = new AspParser("@@attribute_tag@@", new StringReader(code), location.BeginLine - 1, num, aspParser);
			aspParser2.Error += this.ParseError;
			aspParser2.TagParsed += this.TagParsed;
			aspParser2.TextParsed += this.TextParsed;
			aspParser2.Parse();
			if (this.text.Length > 0)
			{
				this.FlushText();
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000B2104 File Offset: 0x000B0304
		private void ParsingCompleted()
		{
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter == null)
			{
				return;
			}
			pageParserFilter.ParseComplete(this.RootBuilder);
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000B2128 File Offset: 0x000B0328
		private void CheckIfIncludeFileIsSecure(string filePath)
		{
			if (filePath == null || filePath.Length == 0)
			{
				return;
			}
			string text = null;
			Exception ex = null;
			try
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(Path.GetDirectoryName(filePath));
				text = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(currentDirectory);
				if (text[text.Length - 1] != '/')
				{
					text += "/";
				}
			}
			catch (DirectoryNotFoundException)
			{
				return;
			}
			catch (FileNotFoundException)
			{
				return;
			}
			catch (Exception ex)
			{
			}
			if (ex != null || !StrUtils.StartsWith(text, HttpRuntime.AppDomainAppPath))
			{
				throw new ParseException(this.Location, "Files above the application's root directory cannot be included.");
			}
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000B21D4 File Offset: 0x000B03D4
		private string ChopOffTagStart(ILocation location, string content, string tagid)
		{
			string text = "<" + tagid;
			if (content.StartsWith(text))
			{
				this.TextParsed(location, text);
				content = content.Substring(text.Length);
			}
			return content;
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000B2210 File Offset: 0x000B0410
		private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
		{
			this.location = new Location(location);
			if (this.tparser != null)
			{
				this.tparser.Location = location;
			}
			if (this.text.Length != 0)
			{
				bool flag = this.lastTag == TagType.CodeRender;
				this.FlushText(flag);
			}
			if (string.Compare(tagid, "script", true, Helpers.InvariantCulture) == 0)
			{
				if (this.inScript || this.ignore_text)
				{
					if (this.ProcessScript(tagtype, attributes))
					{
						return;
					}
				}
				else if (this.ProcessScript(tagtype, attributes))
				{
					return;
				}
			}
			this.lastTag = tagtype;
			switch (tagtype)
			{
			case TagType.Tag:
			{
				bool flag2;
				if (this.ProcessTag(location, tagid, attributes, tagtype, out flag2))
				{
					if (!flag2)
					{
						this.useOtherTags = true;
						return;
					}
				}
				else
				{
					if (this.useOtherTags)
					{
						this.stack.Builder.EnsureOtherTags();
						this.stack.Builder.OtherTags.Add(tagid);
					}
					string plainText = location.PlainText;
					if (!this.ProcessTagsInAttributes(location, tagid, attributes, TagType.Tag))
					{
						this.TextParsed(location, this.ChopOffTagStart(location, plainText, tagid));
						return;
					}
				}
				break;
			}
			case TagType.Close:
				if ((this.useOtherTags && AspGenerator.TryRemoveTag(tagid, this.stack.Builder.OtherTags)) || !this.CloseControl(tagid))
				{
					this.TextParsed(location, location.PlainText);
					return;
				}
				break;
			case TagType.SelfClosing:
			{
				int count = this.stack.Count;
				bool flag2;
				if (!this.ProcessTag(location, tagid, attributes, tagtype, out flag2) && !flag2)
				{
					string plainText2 = location.PlainText;
					if (!this.ProcessTagsInAttributes(location, tagid, attributes, TagType.SelfClosing))
					{
						this.TextParsed(location, this.ChopOffTagStart(location, plainText2, tagid));
						return;
					}
				}
				else if (this.stack.Count != count)
				{
					this.CloseControl(tagid);
					return;
				}
				break;
			}
			case TagType.Directive:
				if (tagid.Length == 0)
				{
					tagid = this.tparser.DefaultDirectiveName;
				}
				this.tparser.AddDirective(tagid, attributes.GetDictionary(null));
				return;
			case TagType.ServerComment:
				break;
			case TagType.DataBinding:
			case TagType.CodeRender:
			case TagType.CodeRenderExpression:
			case TagType.CodeRenderEncode:
				if (this.isApplication)
				{
					throw new ParseException(location, "Invalid content for application file.");
				}
				this.ProcessCode(tagtype, tagid, location);
				return;
			case TagType.Include:
			{
				if (this.isApplication)
				{
					throw new ParseException(location, "Invalid content for application file.");
				}
				string text = attributes["virtual"] as string;
				bool flag3 = text != null;
				if (!flag3)
				{
					text = attributes["file"] as string;
				}
				if (flag3)
				{
					bool flag4 = false;
					VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
					if (virtualPathProvider.FileExists(text))
					{
						VirtualFile file = virtualPathProvider.GetFile(text);
						if (file != null)
						{
							this.Parse(file.Open(), text, true);
							flag4 = true;
						}
					}
					if (!flag4)
					{
						this.Parse(this.tparser.MapPath(text), true);
						return;
					}
				}
				else
				{
					string includeFilePath = AspGenerator.GetIncludeFilePath(this.tparser.ParserDir, text);
					this.CheckIfIncludeFileIsSecure(includeFilePath);
					this.tparser.PushIncludeDir(Path.GetDirectoryName(includeFilePath));
					try
					{
						this.Parse(includeFilePath, true);
					}
					finally
					{
						this.tparser.PopIncludeDir();
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000B2514 File Offset: 0x000B0714
		private static bool TryRemoveTag(string tagid, ArrayList otags)
		{
			if (otags == null || otags.Count == 0)
			{
				return false;
			}
			for (int i = otags.Count - 1; i >= 0; i--)
			{
				string text = (string)otags[i];
				if (string.Compare(tagid, text, true, Helpers.InvariantCulture) == 0)
				{
					do
					{
						otags.RemoveAt(i);
					}
					while (otags.Count - 1 >= i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x000B2572 File Offset: 0x000B0772
		private static string GetIncludeFilePath(string basedir, string filename)
		{
			if (Path.DirectorySeparatorChar == '/')
			{
				filename = filename.Replace("\\", "/");
			}
			return Path.GetFullPath(Path.Combine(basedir, filename));
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x000B259B File Offset: 0x000B079B
		private bool CheckTagEndNeeded(string text)
		{
			return !text.EndsWith("/>");
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000B25AC File Offset: 0x000B07AC
		private List<TextBlock> FindRegexBlocks(Regex rxStart, Regex rxEnd, AspGenerator.CheckBlockEnd checkEnd, IList blocks, TextBlockType typeForMatches, bool discardBlocks)
		{
			List<TextBlock> list = new List<TextBlock>();
			foreach (object obj in blocks)
			{
				TextBlock textBlock = (TextBlock)obj;
				if (textBlock.Type != TextBlockType.Verbatim)
				{
					list.Add(textBlock);
				}
				else
				{
					int num = 0;
					MatchCollection matchCollection = rxStart.Matches(textBlock.Content);
					bool flag = matchCollection.Count > 0;
					foreach (object obj2 in matchCollection)
					{
						Match match = (Match)obj2;
						flag = true;
						int index = match.Index;
						if (num < index)
						{
							list.Add(new TextBlock(TextBlockType.Verbatim, textBlock.Content.Substring(num, index - num)));
						}
						string text = match.Value;
						if (rxEnd != null && checkEnd(text))
						{
							int num2 = index + text.Length;
							Match match2 = rxEnd.Match(textBlock.Content, num2);
							if (match2.Success)
							{
								text = text + textBlock.Content.Substring(num2, match2.Index - num2) + match2.Value;
							}
						}
						if (!discardBlocks)
						{
							list.Add(new TextBlock(typeForMatches, text));
						}
						num = index + text.Length;
					}
					if (num > 0 && num < textBlock.Content.Length)
					{
						list.Add(new TextBlock(TextBlockType.Verbatim, textBlock.Content.Substring(num)));
					}
					if (!flag)
					{
						list.Add(textBlock);
					}
				}
			}
			return list;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000B2778 File Offset: 0x000B0978
		private IList SplitTextIntoBlocks(string text)
		{
			List<TextBlock> list = new List<TextBlock>();
			list.Add(new TextBlock(TextBlockType.Verbatim, text));
			list = this.FindRegexBlocks(AspGenerator.clientCommentRegex, null, null, list, TextBlockType.Comment, false);
			list = this.FindRegexBlocks(AspGenerator.runatServer, AspGenerator.endOfTag, new AspGenerator.CheckBlockEnd(this.CheckTagEndNeeded), list, TextBlockType.Tag, false);
			return this.FindRegexBlocks(AspGenerator.expressionRegex, null, null, list, TextBlockType.Expression, false);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000B27DC File Offset: 0x000B09DC
		private void TextParsed(ILocation location, string text)
		{
			if (this.ignore_text)
			{
				return;
			}
			if (this.inScript)
			{
				this.text.Append(text);
				this.FlushText(true);
				return;
			}
			foreach (object obj in this.SplitTextIntoBlocks(text))
			{
				TextBlock textBlock = (TextBlock)obj;
				switch (textBlock.Type)
				{
				case TextBlockType.Verbatim:
					this.text.Append(textBlock.Content);
					break;
				case TextBlockType.Expression:
					if (this.text.Length > 0)
					{
						this.FlushText(true);
					}
					new AspGenerator.CodeRenderParser(textBlock.Content, this.stack.Builder, location).AddChildren(this);
					break;
				case TextBlockType.Tag:
					this.ParseAttributeTag(textBlock.Content, location);
					break;
				case TextBlockType.Comment:
					if (this.javascript)
					{
						this.text.Append(textBlock.Content);
					}
					else
					{
						this.text.Append("<!--");
						this.FlushText(true);
						string text2 = textBlock.Content.Substring(4, textBlock.Length - 7);
						bool flag;
						if (text2.EndsWith("<![endif]"))
						{
							text2 = text2.Substring(0, text2.Length - 9);
							flag = true;
						}
						else
						{
							flag = false;
						}
						AspParser aspParser = location as AspParser;
						int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
						AspParser aspParser2 = new AspParser("@@comment_code@@", new StringReader(text2), location.BeginLine - 1, num, aspParser);
						aspParser2.Error += this.ParseError;
						aspParser2.TagParsed += this.TagParsed;
						aspParser2.TextParsed += this.TextParsed;
						aspParser2.Parse();
						if (flag)
						{
							this.text.Append("<![endif]");
						}
						this.text.Append("-->");
						this.FlushText(true);
					}
					break;
				}
			}
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000B29FC File Offset: 0x000B0BFC
		private void FlushText()
		{
			this.FlushText(false);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000B2A08 File Offset: 0x000B0C08
		private void FlushText(bool ignoreEmptyString)
		{
			string text = this.text.ToString();
			this.text.Length = 0;
			if (ignoreEmptyString && text.Trim().Length == 0)
			{
				return;
			}
			if (this.inScript)
			{
				PageParserFilter pageParserFilter = this.PageParserFilter;
				if (pageParserFilter != null && !pageParserFilter.ProcessCodeConstruct(CodeConstructType.ScriptTag, text))
				{
					return;
				}
				this.tparser.Scripts.Add(new ServerSideScript(text, new Location(this.tparser.Location)));
				return;
			}
			else
			{
				if (this.tparser.DefaultDirectiveName == "application" && text.Trim() != "")
				{
					throw new ParseException(this.location, "Content not valid for application file.");
				}
				ControlBuilder builder = this.stack.Builder;
				builder.AppendLiteralString(text);
				if (builder.NeedsTagInnerText())
				{
					this.tagInnerText.Append(text);
				}
				return;
			}
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		private bool BuilderHasOtherThan(Type type, ControlBuilder cb)
		{
			ArrayList arrayList = cb.OtherTags;
			if (arrayList != null && arrayList.Count > 0)
			{
				return true;
			}
			arrayList = cb.Children;
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					if (obj != null)
					{
						ControlBuilder controlBuilder = obj as ControlBuilder;
						if (controlBuilder == null)
						{
							string text = obj as string;
							if (text == null || !string.IsNullOrEmpty(text.Trim()))
							{
								return true;
							}
						}
						else if (!(controlBuilder is ContentBuilderInternal) && controlBuilder.ControlType != typeof(Content))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000B2BA4 File Offset: 0x000B0DA4
		private bool OtherControlsAllowed(ControlBuilder cb)
		{
			return cb == null || !typeof(Content).IsAssignableFrom(cb.ControlType) || !this.BuilderHasOtherThan(typeof(Content), this.RootBuilder);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000B2BE0 File Offset: 0x000B0DE0
		public void AddControl(Type type, IDictionary attributes)
		{
			ControlBuilder builder = this.stack.Builder;
			ControlBuilder controlBuilder = ControlBuilder.CreateBuilderFromType(this.tparser, builder, type, null, null, attributes, this.location.BeginLine, this.location.Filename);
			if (controlBuilder != null)
			{
				builder.AppendSubBuilder(controlBuilder);
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000B2C2C File Offset: 0x000B0E2C
		private bool ProcessTag(ILocation location, string tagid, TagAttributes atts, TagType tagtype, out bool ignored)
		{
			ignored = false;
			if (this.isApplication && string.Compare(tagid, "object", true, Helpers.InvariantCulture) != 0)
			{
				throw new ParseException(location, "Invalid tag for application file.");
			}
			ControlBuilder builder = this.stack.Builder;
			ControlBuilder controlBuilder = null;
			if (builder != null && builder.ControlType == typeof(HtmlTable) && (string.Compare(tagid, "thead", true, Helpers.InvariantCulture) == 0 || string.Compare(tagid, "tbody", true, Helpers.InvariantCulture) == 0))
			{
				ignored = true;
				return true;
			}
			IDictionary dictionary = ((atts != null) ? atts.GetDictionary(null) : AspGenerator.emptyHash);
			if (this.stack.Count > 1)
			{
				try
				{
					controlBuilder = builder.CreateSubBuilder(tagid, dictionary, null, this.tparser, location);
				}
				catch (TypeLoadException ex)
				{
					throw new ParseException(this.Location, "Type not found.", ex);
				}
				catch (Exception ex2)
				{
					throw new ParseException(this.Location, ex2.Message, ex2);
				}
			}
			bool flag = atts != null && atts.IsRunAtServer();
			if (controlBuilder == null && flag)
			{
				string text = dictionary["id"] as string;
				if (text != null && !CodeGenerator.IsValidLanguageIndependentIdentifier(text))
				{
					throw new ParseException(this.Location, "'" + text + "' is not a valid identifier");
				}
				try
				{
					controlBuilder = this.RootBuilder.CreateSubBuilder(tagid, dictionary, null, this.tparser, location);
				}
				catch (TypeLoadException ex3)
				{
					throw new ParseException(this.Location, "Type not found.", ex3);
				}
				catch (HttpException ex4)
				{
					CompilationException ex5 = ex4.InnerException as CompilationException;
					if (ex5 != null)
					{
						throw ex5;
					}
					throw new ParseException(this.Location, ex4.Message, ex4);
				}
				catch (Exception ex6)
				{
					throw new ParseException(this.Location, ex6.Message, ex6);
				}
			}
			if (controlBuilder == null)
			{
				return false;
			}
			string plainText = location.PlainText;
			if (!flag && plainText.IndexOf("<%$") == -1 && plainText.IndexOf("<%") > -1)
			{
				return false;
			}
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter != null && !pageParserFilter.AllowControl(controlBuilder.ControlType, controlBuilder))
			{
				ILocation location2 = this.Location;
				string text2 = "Control type '";
				Type controlType = controlBuilder.ControlType;
				throw new ParseException(location2, text2 + ((controlType != null) ? controlType.ToString() : null) + "' not allowed.");
			}
			if (!this.OtherControlsAllowed(controlBuilder))
			{
				throw new ParseException(this.Location, "Only Content controls are allowed directly in a content page that contains Content controls.");
			}
			controlBuilder.Location = location;
			controlBuilder.ID = dictionary["id"] as string;
			if (typeof(HtmlForm).IsAssignableFrom(controlBuilder.ControlType))
			{
				if (this.inForm)
				{
					throw new ParseException(location, "Only one <form> allowed.");
				}
				this.inForm = true;
			}
			if (controlBuilder.HasBody() && !(controlBuilder is ObjectTagBuilder))
			{
				TemplateBuilder templateBuilder = controlBuilder as TemplateBuilder;
				this.stack.Push(controlBuilder, location);
			}
			else
			{
				if (!this.isApplication && controlBuilder is ObjectTagBuilder)
				{
					ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)controlBuilder;
					if (objectTagBuilder.Scope != null && objectTagBuilder.Scope.Length > 0)
					{
						throw new ParseException(location, "Scope not allowed here");
					}
					if (tagtype == TagType.Tag)
					{
						this.stack.Push(controlBuilder, location);
						return true;
					}
				}
				builder.AppendSubBuilder(controlBuilder);
				controlBuilder.CloseControl();
			}
			return true;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000B2F84 File Offset: 0x000B1184
		private string ReadFile(string filename)
		{
			string text;
			using (StreamReader streamReader = new StreamReader(this.tparser.MapPath(filename), WebEncoding.FileEncoding))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000B2FCC File Offset: 0x000B11CC
		private bool ProcessScript(TagType tagtype, TagAttributes attributes)
		{
			if (tagtype == TagType.Close)
			{
				bool flag;
				if (this.inScript)
				{
					flag = this.inScript;
					this.inScript = false;
				}
				else if (!this.ignore_text)
				{
					flag = this.javascript;
					this.javascript = false;
					this.TextParsed(this.location, this.location.PlainText);
				}
				else
				{
					this.ignore_text = false;
					flag = true;
				}
				return flag;
			}
			if (attributes != null && attributes.IsRunAtServer())
			{
				string text = (string)attributes["language"];
				if (text != null && text.Length > 0 && this.tparser.ImplicitLanguage)
				{
					this.tparser.SetLanguage(text);
				}
				this.CheckLanguage(text);
				string text2 = (string)attributes["src"];
				if (text2 != null)
				{
					if (text2.Length == 0)
					{
						throw new ParseException(this.Parser, "src cannot be an empty string");
					}
					string text3 = this.ReadFile(text2);
					this.inScript = true;
					this.TextParsed(this.Parser, text3);
					this.FlushText();
					this.inScript = false;
					if (tagtype != TagType.SelfClosing)
					{
						this.ignore_text = true;
						this.Parser.VerbatimID = "script";
					}
				}
				else if (tagtype == TagType.Tag)
				{
					this.Parser.VerbatimID = "script";
					this.inScript = true;
				}
				return true;
			}
			if (tagtype != TagType.SelfClosing)
			{
				this.Parser.VerbatimID = "script";
				this.javascript = true;
			}
			string text4 = this.location.PlainText;
			if (text4.StartsWith("<script"))
			{
				this.TextParsed(this.location, "<script");
				text4 = text4.Substring(7);
			}
			this.TextParsed(this.location, text4);
			return true;
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000B3170 File Offset: 0x000B1370
		private bool CloseControl(string tagid)
		{
			ControlBuilder builder = this.stack.Builder;
			string originalTagName = builder.OriginalTagName;
			if (string.Compare(originalTagName, "tbody", true, Helpers.InvariantCulture) != 0 && string.Compare(tagid, "tbody", true, Helpers.InvariantCulture) == 0)
			{
				if (!builder.ChildrenAsProperties)
				{
					try
					{
						this.TextParsed(this.location, this.location.PlainText);
						this.FlushText();
					}
					catch
					{
					}
				}
				return true;
			}
			if (builder.ControlType == typeof(HtmlTable) && string.Compare(tagid, "thead", true, Helpers.InvariantCulture) == 0)
			{
				return true;
			}
			if (string.Compare(tagid, originalTagName, true, Helpers.InvariantCulture) != 0)
			{
				return false;
			}
			if (builder.NeedsTagInnerText())
			{
				try
				{
					builder.SetTagInnerText(this.tagInnerText.ToString());
				}
				catch (Exception ex)
				{
					throw new ParseException(builder.Location, ex.Message, ex);
				}
				this.tagInnerText.Length = 0;
			}
			if (typeof(HtmlForm).IsAssignableFrom(builder.ControlType))
			{
				this.inForm = false;
			}
			builder.CloseControl();
			this.stack.Pop();
			this.stack.Builder.AppendSubBuilder(builder);
			return true;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000B32B8 File Offset: 0x000B14B8
		private CodeConstructType MapTagTypeToConstructType(TagType tagtype)
		{
			switch (tagtype)
			{
			case TagType.DataBinding:
				return CodeConstructType.DataBindingSnippet;
			case TagType.CodeRender:
			case TagType.CodeRenderEncode:
				return CodeConstructType.CodeSnippet;
			case TagType.CodeRenderExpression:
				return CodeConstructType.ExpressionSnippet;
			}
			throw new InvalidOperationException("Unexpected tag type.");
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000B32E8 File Offset: 0x000B14E8
		private bool ProcessCode(TagType tagtype, string code, ILocation location)
		{
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter != null && (!pageParserFilter.AllowCode || pageParserFilter.ProcessCodeConstruct(this.MapTagTypeToConstructType(tagtype), code)))
			{
				return true;
			}
			ControlBuilder controlBuilder;
			if (tagtype == TagType.CodeRender)
			{
				controlBuilder = new CodeRenderBuilder(code, false, location);
			}
			else if (tagtype == TagType.CodeRenderExpression)
			{
				controlBuilder = new CodeRenderBuilder(code, true, location);
			}
			else if (tagtype == TagType.DataBinding)
			{
				controlBuilder = new DataBindingBuilder(code, location);
			}
			else
			{
				if (tagtype != TagType.CodeRenderEncode)
				{
					throw new HttpException("Should never happen");
				}
				controlBuilder = new CodeRenderBuilder(code, true, location, true);
			}
			this.stack.Builder.AppendSubBuilder(controlBuilder);
			return true;
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000B3375 File Offset: 0x000B1575
		public ILocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000B3380 File Offset: 0x000B1580
		private void CheckLanguage(string lang)
		{
			if (lang == null || lang == "")
			{
				return;
			}
			if (string.Compare(lang, this.tparser.Language, true, Helpers.InvariantCulture) == 0)
			{
				return;
			}
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			if (compilationSection.Compilers[this.tparser.Language] != compilationSection.Compilers[lang])
			{
				throw new ParseException(this.Location, string.Format("Trying to mix language '{0}' and '{1}'.", this.tparser.Language, lang));
			}
		}

		// Token: 0x04002400 RID: 9216
		private const int READ_BUFFER_SIZE = 8192;

		// Token: 0x04002401 RID: 9217
		internal static Regex DirectiveRegex = new Regex("<%\\s*@(\\s*(?<attrname>\\w[\\w:]*(?=\\W))(\\s*(?<equal>=)\\s*\"(?<attrval>[^\"]*)\"|\\s*(?<equal>=)\\s*'(?<attrval>[^']*)'|\\s*(?<equal>=)\\s*(?<attrval>[^\\s%>]*)|(?<equal>)(?<attrval>\\s*?)))*\\s*?%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04002402 RID: 9218
		private static readonly Regex runatServer = new Regex("<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*(?:/>|>)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04002403 RID: 9219
		private static readonly Regex endOfTag = new Regex("</[\\w:\\.]+\\s*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04002404 RID: 9220
		private static readonly Regex expressionRegex = new Regex("<%.*?%>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04002405 RID: 9221
		private static readonly Regex clientCommentRegex = new Regex("<!--(.|\\s)*?-->", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04002406 RID: 9222
		private ParserStack pstack;

		// Token: 0x04002407 RID: 9223
		private BuilderLocationStack stack;

		// Token: 0x04002408 RID: 9224
		private TemplateParser tparser;

		// Token: 0x04002409 RID: 9225
		private StringBuilder text;

		// Token: 0x0400240A RID: 9226
		private RootBuilder rootBuilder;

		// Token: 0x0400240B RID: 9227
		private bool inScript;

		// Token: 0x0400240C RID: 9228
		private bool javascript;

		// Token: 0x0400240D RID: 9229
		private bool ignore_text;

		// Token: 0x0400240E RID: 9230
		private ILocation location;

		// Token: 0x0400240F RID: 9231
		private bool isApplication;

		// Token: 0x04002410 RID: 9232
		private StringBuilder tagInnerText = new StringBuilder();

		// Token: 0x04002411 RID: 9233
		private static IDictionary emptyHash = new Dictionary<string, object>();

		// Token: 0x04002412 RID: 9234
		private bool inForm;

		// Token: 0x04002413 RID: 9235
		private bool useOtherTags;

		// Token: 0x04002414 RID: 9236
		private TagType lastTag;

		// Token: 0x04002415 RID: 9237
		private AspComponentFoundry componentFoundry;

		// Token: 0x04002416 RID: 9238
		private Stream inputStream;

		// Token: 0x02000621 RID: 1569
		// (Invoke) Token: 0x06004354 RID: 17236
		private delegate bool CheckBlockEnd(string text);

		// Token: 0x02000622 RID: 1570
		private class CodeRenderParser
		{
			// Token: 0x06004357 RID: 17239 RVA: 0x000B3488 File Offset: 0x000B1688
			public CodeRenderParser(string str, ControlBuilder builder, ILocation location)
			{
				this.str = str;
				this.builder = builder;
				this.location = location;
			}

			// Token: 0x06004358 RID: 17240 RVA: 0x000B34A5 File Offset: 0x000B16A5
			public void AddChildren(AspGenerator generator)
			{
				this.generator = generator;
				if (this.str.IndexOf("<%") > 0)
				{
					this.DoParseExpressions(this.str);
					return;
				}
				this.DoParse(this.str);
			}

			// Token: 0x06004359 RID: 17241 RVA: 0x000B34DC File Offset: 0x000B16DC
			private void DoParseExpressions(string str)
			{
				int num = 0;
				int num2 = 0;
				Regex regex = new Regex("(<%(?!@)(?<code>(.|\\s)*?)%>)|(<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*?/>)", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				int length = str.Length;
				while (num2 > -1 && num < length)
				{
					Match match = regex.Match(str, num2);
					if (!match.Success)
					{
						break;
					}
					string value = match.Value;
					num2 = match.Index;
					if (num2 > num)
					{
						this.TextParsed(null, str.Substring(num, num2 - num));
					}
					this.DoParse(value);
					num2 += value.Length;
					num = num2;
					if (num2 >= length)
					{
						break;
					}
					num2 = str.IndexOf('<', num2);
				}
				if (num < length)
				{
					this.TextParsed(null, str.Substring(num));
				}
			}

			// Token: 0x0600435A RID: 17242 RVA: 0x000B357C File Offset: 0x000B177C
			private void DoParse(string str)
			{
				AspParser aspParser = this.location as AspParser;
				int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
				AspParser aspParser2 = new AspParser("@@code_render@@", new StringReader(str), this.location.BeginLine - 1, num, aspParser);
				aspParser2.Error += this.ParseError;
				aspParser2.TagParsed += this.TagParsed;
				aspParser2.TextParsed += this.TextParsed;
				aspParser2.Parse();
			}

			// Token: 0x0600435B RID: 17243 RVA: 0x000B35FC File Offset: 0x000B17FC
			private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
			{
				switch (tagtype)
				{
				case TagType.Tag:
				case TagType.Close:
				case TagType.SelfClosing:
					if (this.generator != null)
					{
						this.generator.TagParsed(location, tagtype, tagid, attributes);
						return;
					}
					break;
				case TagType.DataBinding:
					this.builder.AppendSubBuilder(new DataBindingBuilder(tagid, location));
					return;
				case TagType.CodeRender:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, false, location));
					return;
				case TagType.CodeRenderExpression:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, true, location));
					return;
				case TagType.CodeRenderEncode:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, true, location, true));
					return;
				}
				string plainText = location.PlainText;
				if (plainText != null && plainText.Trim().Length > 0)
				{
					this.builder.AppendLiteralString(plainText);
				}
			}

			// Token: 0x0600435C RID: 17244 RVA: 0x000B36C8 File Offset: 0x000B18C8
			private void TextParsed(ILocation location, string text)
			{
				this.builder.AppendLiteralString(text);
			}

			// Token: 0x0600435D RID: 17245 RVA: 0x00039AB0 File Offset: 0x00037CB0
			private void ParseError(ILocation location, string message)
			{
				throw new ParseException(location, message);
			}

			// Token: 0x04002417 RID: 9239
			private string str;

			// Token: 0x04002418 RID: 9240
			private ControlBuilder builder;

			// Token: 0x04002419 RID: 9241
			private AspGenerator generator;

			// Token: 0x0400241A RID: 9242
			private ILocation location;
		}
	}
}
