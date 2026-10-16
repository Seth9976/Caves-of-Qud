using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000668 RID: 1640
	internal abstract class TemplateBuildProvider : GenericBuildProvider<TemplateParser>
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000BF10D File Offset: 0x000BD30D
		internal override string LanguageName
		{
			get
			{
				if (string.IsNullOrEmpty(this.compilationLanguage))
				{
					this.ExtractDependencies();
					if (string.IsNullOrEmpty(this.compilationLanguage))
					{
						this.compilationLanguage = base.LanguageName;
					}
				}
				return this.compilationLanguage;
			}
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000BF144 File Offset: 0x000BD344
		static TemplateBuildProvider()
		{
			TemplateBuildProvider.directiveAttributes.Add("Control", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractControlDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Master", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPageOrMasterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("MasterType", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPreviousPageTypeOrMasterTypeDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Page", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPageOrMasterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("PreviousPageType", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPreviousPageTypeOrMasterTypeDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Reference", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractReferenceDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Register", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractRegisterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("WebHandler", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractLanguage));
			TemplateBuildProvider.directiveAttributes.Add("WebService", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractLanguage));
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x000BF269 File Offset: 0x000BD469
		private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values)
		{
			return TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, name, names, values, true);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x000BF278 File Offset: 0x000BD478
		private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values, bool isPath)
		{
			if (names.Count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			int count = values.Count;
			using (IEnumerator enumerator = names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(((Capture)enumerator.Current).Value, name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						num++;
					}
					else
					{
						if (num > count)
						{
							return string.Empty;
						}
						if (!isPath)
						{
							return values[num].Value.Trim();
						}
						string text = values[num].Value.Trim(TemplateBuildProvider.directiveValueTrimChars);
						if (string.IsNullOrEmpty(text))
						{
							return string.Empty;
						}
						return new VirtualPath(text, baseDirectory).Absolute;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000BF35C File Offset: 0x000BD55C
		private static void ExtractControlDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			TemplateBuildProvider.ExtractLanguage(baseDirectory, names, values, bp);
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000BF370 File Offset: 0x000BD570
		private static void ExtractLanguage(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Language", names, values, false);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			bp.compilationLanguage = text;
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000BF3A8 File Offset: 0x000BD5A8
		private static void ExtractPageOrMasterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			TemplateBuildProvider.ExtractLanguage(baseDirectory, names, values, bp);
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "MasterPageFile", names, values);
			if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
			{
				bp.dependencies.Add(text, true);
			}
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000BF3F8 File Offset: 0x000BD5F8
		private static void ExtractCodeBehind(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			foreach (string text in new string[]
			{
				TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "CodeFile", names, values),
				TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Src", names, values)
			})
			{
				if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
				{
					bp.dependencies.Add(text, true);
				}
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000BF464 File Offset: 0x000BD664
		private static void ExtractRegisterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Src", names, values);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (string.IsNullOrEmpty(TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "TagName", names, values)))
			{
				return;
			}
			if (string.IsNullOrEmpty(TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "TagPrefix", names, values)))
			{
				return;
			}
			if (bp.dependencies.ContainsKey(text))
			{
				return;
			}
			bp.dependencies.Add(text, true);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000BF4D0 File Offset: 0x000BD6D0
		private static void ExtractPreviousPageTypeOrMasterTypeDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (bp.dependencies.ContainsKey(text))
			{
				return;
			}
			bp.dependencies.Add(text, true);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000BF510 File Offset: 0x000BD710
		private static void ExtractReferenceDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Control", names, values);
			string text2 = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
			string text3 = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Page", names, values);
			bool flag = string.IsNullOrEmpty(text);
			bool flag2 = string.IsNullOrEmpty(text2);
			bool flag3 = string.IsNullOrEmpty(text3);
			if (flag && flag2 && flag3)
			{
				return;
			}
			if ((flag ? 1 : 0) + (flag2 ? 1 : 0) + (flag3 ? 1 : 0) != 2)
			{
				return;
			}
			string text4;
			if (!flag)
			{
				text4 = text;
			}
			else if (!flag2)
			{
				text4 = text2;
			}
			else
			{
				text4 = text3;
			}
			if (bp.dependencies.ContainsKey(text4))
			{
				return;
			}
			bp.dependencies.Add(text4, true);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000BF5B8 File Offset: 0x000BD7B8
		private IDictionary<string, bool> AddParsedDependencies(IDictionary<string, bool> dict)
		{
			if (base.Parsed)
			{
				List<string> list = base.Parser.Dependencies;
				if (list == null || list.Count > 0)
				{
					return dict;
				}
				if (dict == null)
				{
					dict = this.dependencies;
					if (dict == null)
					{
						dict = (this.dependencies = new SortedDictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
					}
				}
				foreach (string text in list)
				{
					string text2 = text as string;
					if (text2 != null && !dict.ContainsKey(text2))
					{
						dict.Add(text2, true);
					}
				}
			}
			if (dict == null || dict.Count == 0)
			{
				return null;
			}
			return dict;
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000BF670 File Offset: 0x000BD870
		internal override IDictionary<string, bool> ExtractDependencies()
		{
			if (this.dependencies != null)
			{
				return this.AddParsedDependencies(this.dependencies);
			}
			string virtualPath = base.VirtualPath;
			if (string.IsNullOrEmpty(virtualPath))
			{
				return this.AddParsedDependencies(null);
			}
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			if (!virtualPathProvider.FileExists(virtualPath))
			{
				return this.AddParsedDependencies(null);
			}
			VirtualFile file = virtualPathProvider.GetFile(virtualPath);
			if (file == null)
			{
				return this.AddParsedDependencies(null);
			}
			string text;
			using (Stream stream = file.Open())
			{
				if (stream == null || !stream.CanRead)
				{
					return this.AddParsedDependencies(null);
				}
				using (StreamReader streamReader = new StreamReader(stream, WebEncoding.FileEncoding))
				{
					text = streamReader.ReadToEnd();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return this.AddParsedDependencies(null);
			}
			MatchCollection matchCollection = AspGenerator.DirectiveRegex.Matches(text);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return this.AddParsedDependencies(null);
			}
			this.dependencies = new SortedDictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			string directory = VirtualPathUtility.GetDirectory(virtualPath);
			foreach (object obj in matchCollection)
			{
				GroupCollection groups = ((Match)obj).Groups;
				if (groups.Count >= 6)
				{
					CaptureCollection captures = groups[3].Captures;
					string value = captures[0].Value;
					TemplateBuildProvider.ExtractDirectiveDependencies extractDirectiveDependencies;
					if (TemplateBuildProvider.directiveAttributes.TryGetValue(value, out extractDirectiveDependencies))
					{
						extractDirectiveDependencies(directory, captures, groups[5].Captures, this);
					}
				}
			}
			return this.AddParsedDependencies(this.dependencies);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000BF838 File Offset: 0x000BDA38
		protected override string GetClassType(BaseCompiler compiler, TemplateParser parser)
		{
			if (compiler != null)
			{
				return compiler.MainClassType;
			}
			return null;
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x000BF845 File Offset: 0x000BDA45
		protected override ICollection GetParserDependencies(TemplateParser parser)
		{
			if (parser != null)
			{
				return parser.Dependencies;
			}
			return null;
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x000BF852 File Offset: 0x000BDA52
		protected override string GetParserLanguage(TemplateParser parser)
		{
			if (parser != null)
			{
				return parser.Language;
			}
			return null;
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000BF85F File Offset: 0x000BDA5F
		protected override string GetCodeBehindSource(TemplateParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(parser.CodeBehindSource))
			{
				return null;
			}
			return parser.CodeBehindSource;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000BF87B File Offset: 0x000BDA7B
		protected override AspGenerator CreateAspGenerator(TemplateParser parser)
		{
			if (parser != null)
			{
				return new AspGenerator(parser);
			}
			return null;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000BF888 File Offset: 0x000BDA88
		protected override List<string> GetReferencedAssemblies(TemplateParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			List<string> assemblies = parser.Assemblies;
			if (assemblies == null || assemblies.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (string text in assemblies)
			{
				if (!string.IsNullOrEmpty(text) && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x04002538 RID: 9528
		private static SortedDictionary<string, TemplateBuildProvider.ExtractDirectiveDependencies> directiveAttributes = new SortedDictionary<string, TemplateBuildProvider.ExtractDirectiveDependencies>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04002539 RID: 9529
		private static char[] directiveValueTrimChars = new char[] { ' ', '\t', '\r', '\n', '"', '\'' };

		// Token: 0x0400253A RID: 9530
		private SortedDictionary<string, bool> dependencies;

		// Token: 0x0400253B RID: 9531
		private string compilationLanguage;

		// Token: 0x02000669 RID: 1641
		// (Invoke) Token: 0x0600462E RID: 17966
		private delegate void ExtractDirectiveDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp);
	}
}
