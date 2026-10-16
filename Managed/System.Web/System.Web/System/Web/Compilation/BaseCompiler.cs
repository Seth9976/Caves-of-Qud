using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000631 RID: 1585
	internal abstract class BaseCompiler
	{
		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x000B6290 File Offset: 0x000B4490
		public VirtualPath InputVirtualPath
		{
			get
			{
				if (this.inputVirtualPath == null)
				{
					this.inputVirtualPath = new VirtualPath(VirtualPathUtility.Combine(this.parser.BaseVirtualDir, Path.GetFileName(this.parser.InputFile)));
				}
				return this.inputVirtualPath;
			}
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000B62CB File Offset: 0x000B44CB
		protected BaseCompiler(TemplateParser parser)
		{
			this.parser = parser;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000B62E8 File Offset: 0x000B44E8
		protected void AddReferencedAssembly(Assembly asm)
		{
			if (this.unit == null || asm == null)
			{
				return;
			}
			StringCollection referencedAssemblies = this.unit.ReferencedAssemblies;
			string location = asm.Location;
			if (!referencedAssemblies.Contains(location))
			{
				referencedAssemblies.Add(location);
			}
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000B632B File Offset: 0x000B452B
		internal CodeStatement AddLinePragma(CodeExpression expression, ControlBuilder builder)
		{
			return this.AddLinePragma(new CodeExpressionStatement(expression), builder);
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000B633C File Offset: 0x000B453C
		internal CodeStatement AddLinePragma(CodeStatement statement, ControlBuilder builder)
		{
			if (builder == null || statement == null)
			{
				return statement;
			}
			ILocation location = null;
			if (!(builder is CodeRenderBuilder))
			{
				location = builder.Location;
			}
			if (location != null)
			{
				return this.AddLinePragma(statement, location);
			}
			return this.AddLinePragma(statement, builder.Line, builder.FileName);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000B6381 File Offset: 0x000B4581
		internal CodeStatement AddLinePragma(CodeStatement statement, ILocation location)
		{
			if (location == null || statement == null)
			{
				return statement;
			}
			return this.AddLinePragma(statement, location.BeginLine, location.Filename);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000B639E File Offset: 0x000B459E
		private bool IgnoreFile(string fileName)
		{
			return (this.parser != null && !this.parser.LinePragmasOn) || string.Compare(fileName, "@@inner_string@@", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000B63C6 File Offset: 0x000B45C6
		internal CodeStatement AddLinePragma(CodeStatement statement, int line, string fileName)
		{
			if (statement == null || this.IgnoreFile(fileName))
			{
				return statement;
			}
			statement.LinePragma = new CodeLinePragma(fileName, line);
			return statement;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000B63E4 File Offset: 0x000B45E4
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, ControlBuilder builder)
		{
			if (builder == null || member == null)
			{
				return member;
			}
			ILocation location = builder.Location;
			if (location != null)
			{
				return this.AddLinePragma(member, location);
			}
			return this.AddLinePragma(member, builder.Line, builder.FileName);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x000B641F File Offset: 0x000B461F
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, ILocation location)
		{
			if (location == null || member == null)
			{
				return member;
			}
			return this.AddLinePragma(member, location.BeginLine, location.Filename);
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000B643C File Offset: 0x000B463C
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, int line, string fileName)
		{
			if (member == null || this.IgnoreFile(fileName))
			{
				return member;
			}
			member.LinePragma = new CodeLinePragma(fileName, line);
			return member;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x000B645C File Offset: 0x000B465C
		internal void ConstructType()
		{
			this.unit = new CodeCompileUnit();
			byte[] md5Checksum = this.parser.MD5Checksum;
			if (md5Checksum != null)
			{
				CodeChecksumPragma codeChecksumPragma = new CodeChecksumPragma();
				codeChecksumPragma.FileName = this.parser.InputFile;
				codeChecksumPragma.ChecksumAlgorithmId = BaseCompiler.HashMD5;
				codeChecksumPragma.ChecksumData = md5Checksum;
				this.unit.StartDirectives.Add(codeChecksumPragma);
			}
			if (this.parser.IsPartial)
			{
				string text = null;
				string text2 = this.parser.PartialClassName;
				int num = text2.LastIndexOf('.');
				if (num != -1)
				{
					text = text2.Substring(0, num);
					text2 = text2.Substring(num + 1);
				}
				CodeNamespace codeNamespace = new CodeNamespace(text);
				this.partialClass = new CodeTypeDeclaration(text2);
				this.partialClass.IsPartial = true;
				this.partialClassExpr = new CodeTypeReferenceExpression(this.parser.PartialClassName);
				this.unit.Namespaces.Add(codeNamespace);
				this.partialClass.TypeAttributes = TypeAttributes.Public;
				codeNamespace.Types.Add(this.partialClass);
			}
			string text3 = this.parser.ClassName;
			string text4 = "ASP";
			int num2 = text3.LastIndexOf('.');
			if (num2 != -1)
			{
				text4 = text3.Substring(0, num2);
				text3 = text3.Substring(num2 + 1);
			}
			this.mainNS = new CodeNamespace(text4);
			this.mainClass = new CodeTypeDeclaration(text3);
			CodeTypeReference codeTypeReference;
			if (this.partialClass != null)
			{
				codeTypeReference = new CodeTypeReference(this.parser.PartialClassName);
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			else
			{
				codeTypeReference = new CodeTypeReference(this.parser.BaseType.FullName);
				if (this.parser.BaseTypeIsGlobal)
				{
					codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
				}
			}
			this.mainClass.BaseTypes.Add(codeTypeReference);
			this.mainClassExpr = new CodeTypeReferenceExpression(text4 + "." + text3);
			this.unit.Namespaces.Add(this.mainNS);
			this.mainClass.TypeAttributes = TypeAttributes.Public;
			this.mainNS.Types.Add(this.mainClass);
			foreach (object obj in this.parser.Imports.Keys)
			{
				if (obj is string)
				{
					this.mainNS.Imports.Add(new CodeNamespaceImport((string)obj));
				}
			}
			StringCollection referencedAssemblies = this.unit.ReferencedAssemblies;
			if (this.parser.Assemblies != null)
			{
				foreach (string text5 in this.parser.Assemblies)
				{
					string text6 = text5 as string;
					if (text6 != null && !referencedAssemblies.Contains(text6))
					{
						referencedAssemblies.Add(text6);
					}
				}
			}
			ArrayList extraAssemblies = WebConfigurationManager.ExtraAssemblies;
			if (extraAssemblies != null && extraAssemblies.Count > 0)
			{
				foreach (object obj2 in extraAssemblies)
				{
					string text6 = obj2 as string;
					if (text6 != null && !referencedAssemblies.Contains(text6))
					{
						referencedAssemblies.Add(text6);
					}
				}
			}
			IList codeAssemblies = BuildManager.CodeAssemblies;
			if (codeAssemblies != null && codeAssemblies.Count > 0)
			{
				foreach (object obj3 in codeAssemblies)
				{
					Assembly assembly = obj3 as Assembly;
					if (obj3 != null)
					{
						string text6 = assembly.Location;
						if (text6 != null && !referencedAssemblies.Contains(text6))
						{
							referencedAssemblies.Add(text6);
						}
					}
				}
			}
			this.unit.UserData["RequireVariableDeclaration"] = this.parser.ExplicitOn;
			this.unit.UserData["AllowLateBound"] = !this.parser.StrictOn;
			this.InitializeType();
			this.AddInterfaces();
			this.AddClassAttributes();
			this.CreateStaticFields();
			this.AddApplicationAndSessionObjects();
			this.AddScripts();
			this.CreateMethods();
			this.CreateConstructor(null, null);
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x000B68E0 File Offset: 0x000B4AE0
		internal CodeFieldReferenceExpression GetMainClassFieldReferenceExpression(string fieldName)
		{
			CodeTypeReference codeTypeReference = new CodeTypeReference(this.mainNS.Name + "." + this.mainClass.Name);
			codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(codeTypeReference), fieldName);
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void InitializeType()
		{
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000B6920 File Offset: 0x000B4B20
		protected virtual void CreateStaticFields()
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(bool), "__initialized");
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodePrimitiveExpression(false);
			this.mainClass.Members.Add(codeMemberField);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000B6970 File Offset: 0x000B4B70
		private void AssignAppRelativeVirtualPath(CodeConstructor ctor)
		{
			if (string.IsNullOrEmpty(this.parser.InputFile))
			{
				return;
			}
			Type type = this.parser.CodeFileBaseClassType;
			if (type == null)
			{
				type = this.parser.BaseType;
			}
			if (type == null)
			{
				return;
			}
			if (!type.IsSubclassOf(typeof(TemplateControl)))
			{
				return;
			}
			CodeTypeReference codeTypeReference = new CodeTypeReference(type.FullName);
			if (this.parser.BaseTypeIsGlobal)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeCastExpression(codeTypeReference, new CodeThisReferenceExpression()), "AppRelativeVirtualPath");
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codePropertyReferenceExpression;
			codeAssignStatement.Right = new CodePrimitiveExpression(VirtualPathUtility.RemoveTrailingSlash(this.InputVirtualPath.AppRelative));
			ctor.Statements.Add(codeAssignStatement);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000B6A40 File Offset: 0x000B4C40
		protected virtual void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
		{
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			this.mainClass.Members.Add(codeConstructor);
			if (localVars != null)
			{
				codeConstructor.Statements.AddRange(localVars);
			}
			this.AssignAppRelativeVirtualPath(codeConstructor);
			CodeFieldReferenceExpression mainClassFieldReferenceExpression = this.GetMainClassFieldReferenceExpression("__initialized");
			CodeBinaryOperatorExpression codeBinaryOperatorExpression = new CodeBinaryOperatorExpression(mainClassFieldReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(mainClassFieldReferenceExpression, new CodePrimitiveExpression(true));
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = codeBinaryOperatorExpression;
			if (trueStmt != null)
			{
				codeConditionStatement.TrueStatements.AddRange(trueStmt);
			}
			codeConditionStatement.TrueStatements.Add(codeAssignStatement);
			codeConstructor.Statements.Add(codeConditionStatement);
			this.AddStatementsToConstructor(codeConstructor);
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void AddStatementsToConstructor(CodeConstructor ctor)
		{
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000B6AF4 File Offset: 0x000B4CF4
		private void AddScripts()
		{
			if (this.parser.Scripts == null || this.parser.Scripts.Count == 0)
			{
				return;
			}
			foreach (ServerSideScript serverSideScript in this.parser.Scripts)
			{
				ServerSideScript serverSideScript2 = serverSideScript as ServerSideScript;
				if (serverSideScript2 != null)
				{
					this.mainClass.Members.Add(this.AddLinePragma(new CodeSnippetTypeMember(serverSideScript2.Script), serverSideScript2.Location));
				}
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void CreateMethods()
		{
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000B6B98 File Offset: 0x000B4D98
		private void InternalCreatePageProperty(string retType, string name, string contextProperty)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = new CodeTypeReference(retType);
			codeMemberProperty.Attributes = (MemberAttributes)12290;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeCastExpression codeCastExpression = new CodeCastExpression();
			codeMethodReturnStatement.Expression = codeCastExpression;
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Context");
			codePropertyReferenceExpression.PropertyName = contextProperty;
			codeCastExpression.TargetType = new CodeTypeReference(retType);
			codeCastExpression.Expression = codePropertyReferenceExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			if (this.partialClass == null)
			{
				this.mainClass.Members.Add(codeMemberProperty);
				return;
			}
			this.partialClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000B6C4C File Offset: 0x000B4E4C
		protected void CreateProfileProperty()
		{
			string text;
			if (AppCodeCompiler.HaveCustomProfile(WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection))
			{
				text = "ProfileCommon";
			}
			else
			{
				text = "System.Web.Profile.DefaultProfile";
			}
			this.InternalCreatePageProperty(text, "Profile", "Profile");
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000B6C90 File Offset: 0x000B4E90
		protected virtual void AddInterfaces()
		{
			if (this.parser.Interfaces == null)
			{
				return;
			}
			foreach (object obj in this.parser.Interfaces)
			{
				if (obj is string)
				{
					this.mainClass.BaseTypes.Add(new CodeTypeReference((string)obj));
				}
			}
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void AddClassAttributes()
		{
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void AddApplicationAndSessionObjects()
		{
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000B6D14 File Offset: 0x000B4F14
		protected void CreateApplicationOrSessionPropertyForObject(Type type, string propName, bool isApplication, bool isPublic)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Name = propName;
			if (isPublic)
			{
				codeMemberProperty.Attributes = (MemberAttributes)24578;
			}
			else
			{
				codeMemberProperty.Attributes = (MemberAttributes)20482;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression;
			if (isApplication)
			{
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Application");
			}
			else
			{
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Session");
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(codePropertyReferenceExpression, "StaticObjects"), "GetObject"), new CodeExpression[]
			{
				new CodePrimitiveExpression(propName)
			});
			CodeCastExpression codeCastExpression = new CodeCastExpression(codeMemberProperty.Type, codeMethodInvokeExpression);
			CodeExpression codeExpression;
			if (isApplication)
			{
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, "cached" + propName);
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = codeFieldReferenceExpression;
				codeAssignStatement.Right = codeCastExpression;
				codeConditionStatement.TrueStatements.Add(codeAssignStatement);
				codeMemberProperty.GetStatements.Add(codeConditionStatement);
				codeExpression = codeFieldReferenceExpression;
			}
			else
			{
				codeExpression = codeCastExpression;
			}
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000B6E50 File Offset: 0x000B5050
		protected string CreateFieldForObject(Type type, string name)
		{
			string text = "cached" + name;
			CodeMemberField codeMemberField = new CodeMemberField(type, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			this.mainClass.Members.Add(codeMemberField);
			return text;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000B6E90 File Offset: 0x000B5090
		protected void CreatePropertyForObject(Type type, string propName, string fieldName, bool isPublic)
		{
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, fieldName);
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Name = propName;
			if (isPublic)
			{
				codeMemberProperty.Attributes = (MemberAttributes)24578;
			}
			else
			{
				codeMemberProperty.Attributes = (MemberAttributes)20482;
			}
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(codeMemberProperty.Type, Array.Empty<CodeExpression>());
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression, codeObjectCreateExpression));
			codeMemberProperty.GetStatements.Add(codeConditionStatement);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeFieldReferenceExpression));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000B6F4C File Offset: 0x000B514C
		private void CheckCompilerErrors(CompilerResults results)
		{
			if (results.NativeCompilerReturnValue == 0)
			{
				return;
			}
			string text = null;
			CompilerErrorCollection errors = results.Errors;
			CompilerError compilerError = ((errors != null && errors.Count > 0) ? errors[0] : null);
			string text2 = ((compilerError != null) ? compilerError.FileName : null);
			if (text2 != null && File.Exists(text2))
			{
				using (StreamReader streamReader = File.OpenText(text2))
				{
					text = streamReader.ReadToEnd();
					goto IL_0087;
				}
			}
			StringWriter stringWriter = new StringWriter();
			this.provider.CreateGenerator().GenerateCodeFromCompileUnit(this.unit, stringWriter, null);
			text = stringWriter.ToString();
			IL_0087:
			throw new CompilationException(this.parser.InputFile, errors, text);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x0009BB6A File Offset: 0x00099D6A
		protected string DynamicDir()
		{
			return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000B7004 File Offset: 0x000B5204
		internal static CodeDomProvider CreateProvider(string lang)
		{
			CompilerParameters compilerParameters;
			string text;
			return BaseCompiler.CreateProvider(HttpContext.Current, lang, out compilerParameters, out text);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000B7020 File Offset: 0x000B5220
		internal static CodeDomProvider CreateProvider(string lang, out string compilerOptions, out int warningLevel, out string tempdir)
		{
			return BaseCompiler.CreateProvider(HttpContext.Current, lang, out compilerOptions, out warningLevel, out tempdir);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000B7030 File Offset: 0x000B5230
		internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out string compilerOptions, out int warningLevel, out string tempdir)
		{
			CompilerParameters compilerParameters;
			CodeDomProvider codeDomProvider = BaseCompiler.CreateProvider(context, lang, out compilerParameters, out tempdir);
			if (compilerParameters != null)
			{
				warningLevel = compilerParameters.WarningLevel;
				compilerOptions = compilerParameters.CompilerOptions;
				return codeDomProvider;
			}
			warningLevel = 2;
			compilerOptions = string.Empty;
			return codeDomProvider;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000B7068 File Offset: 0x000B5268
		internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out CompilerParameters par, out string tempdir)
		{
			CodeDomProvider codeDomProvider = null;
			par = null;
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			global::System.Web.Configuration.Compiler compiler = compilationSection.Compilers[lang];
			if (compiler == null)
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(lang);
				if (compilerInfo != null && compilerInfo.IsCodeDomProviderTypeValid)
				{
					codeDomProvider = compilerInfo.CreateProvider();
					par = compilerInfo.CreateDefaultCompilerParameters();
				}
			}
			else
			{
				codeDomProvider = Activator.CreateInstance(HttpApplication.LoadType(compiler.Type, true)) as CodeDomProvider;
				par = new CompilerParameters();
				par.CompilerOptions = compiler.CompilerOptions;
				par.WarningLevel = compiler.WarningLevel;
			}
			tempdir = compilationSection.TempDirectory;
			return codeDomProvider;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000B7100 File Offset: 0x000B5300
		[global::System.MonoTODO("find out how to extract the warningLevel and compilerOptions in the <system.codedom> case")]
		public virtual Type GetCompiledType()
		{
			Type typeFromCache = CachingCompiler.GetTypeFromCache(this.parser.InputFile);
			if (typeFromCache != null)
			{
				return typeFromCache;
			}
			this.ConstructType();
			string language = this.parser.Language;
			string text;
			int num;
			string text2;
			this.Provider = BaseCompiler.CreateProvider(this.parser.Context, language, out text, out num, out text2);
			if (this.Provider == null)
			{
				throw new HttpException("Configuration error. Language not supported: " + language, 500);
			}
			CompilerParameters compilerParameters = this.CompilerParameters;
			compilerParameters.IncludeDebugInformation = this.parser.Debug;
			compilerParameters.CompilerOptions = text + " " + this.parser.CompilerOptions;
			compilerParameters.WarningLevel = num;
			bool flag = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;
			if (text2 == null || text2 == "")
			{
				text2 = this.DynamicDir();
			}
			TempFileCollection tempFileCollection = new TempFileCollection(text2, flag);
			compilerParameters.TempFiles = tempFileCollection;
			string fileName = Path.GetFileName(tempFileCollection.AddExtension("dll", true));
			compilerParameters.OutputAssembly = Path.Combine(this.DynamicDir(), fileName);
			CompilerResults compilerResults = CachingCompiler.Compile(this);
			this.CheckCompilerErrors(compilerResults);
			Assembly assembly = compilerResults.CompiledAssembly;
			if (assembly == null)
			{
				if (!File.Exists(compilerParameters.OutputAssembly))
				{
					compilerResults.TempFiles.Delete();
					throw new CompilationException(this.parser.InputFile, compilerResults.Errors, "No assembly returned after compilation!?");
				}
				assembly = Assembly.LoadFrom(compilerParameters.OutputAssembly);
			}
			compilerResults.TempFiles.Delete();
			Type type = assembly.GetType(this.MainClassType, true);
			if (this.parser.IsPartial && !this.isRebuilding && this.CheckPartialBaseType(type))
			{
				this.isRebuilding = true;
				this.parser.RootBuilder.ResetState();
				return this.GetCompiledType();
			}
			return type;
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000B72DA File Offset: 0x000B54DA
		internal string MainClassType
		{
			get
			{
				if (this.mainClassExpr == null)
				{
					return null;
				}
				return this.mainClassExpr.Type.BaseType;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000B72F6 File Offset: 0x000B54F6
		internal bool IsRebuildingPartial
		{
			get
			{
				return this.isRebuilding;
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000B7300 File Offset: 0x000B5500
		internal bool CheckPartialBaseType(Type type)
		{
			Type baseType = type.BaseType;
			if (baseType == null || baseType == typeof(Page))
			{
				return false;
			}
			bool flag = false;
			if (this.CheckPartialBaseFields(type, baseType))
			{
				flag = true;
			}
			if (this.CheckPartialBaseProperties(type, baseType))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x000B734C File Offset: 0x000B554C
		internal bool CheckPartialBaseFields(Type type, Type baseType)
		{
			bool flag = false;
			foreach (FieldInfo fieldInfo in baseType.GetFields(BaseCompiler.replaceableFlags))
			{
				if (!fieldInfo.IsPrivate)
				{
					FieldInfo field = type.GetField(fieldInfo.Name, BaseCompiler.replaceableFlags);
					if (field != null && field.DeclaringType == type)
					{
						this.partialNameOverride[field.Name] = true;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000B73C8 File Offset: 0x000B55C8
		internal bool CheckPartialBaseProperties(Type type, Type baseType)
		{
			bool flag = false;
			foreach (PropertyInfo propertyInfo in baseType.GetProperties())
			{
				PropertyInfo property = type.GetProperty(propertyInfo.Name);
				if (property != null && property.DeclaringType == type)
				{
					this.partialNameOverride[property.Name] = true;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x000B7432 File Offset: 0x000B5632
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x000B743A File Offset: 0x000B563A
		internal CodeDomProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000B7443 File Offset: 0x000B5643
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000B744B File Offset: 0x000B564B
		internal ICodeCompiler Compiler
		{
			get
			{
				return this.compiler;
			}
			set
			{
				this.compiler = value;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000B7454 File Offset: 0x000B5654
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x000B746F File Offset: 0x000B566F
		internal CompilerParameters CompilerParameters
		{
			get
			{
				if (this.compilerParameters == null)
				{
					this.compilerParameters = new CompilerParameters();
				}
				return this.compilerParameters;
			}
			set
			{
				this.compilerParameters = value;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x000B7478 File Offset: 0x000B5678
		internal CodeCompileUnit CompileUnit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x000B7480 File Offset: 0x000B5680
		internal CodeTypeDeclaration DerivedType
		{
			get
			{
				return this.mainClass;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x000B7488 File Offset: 0x000B5688
		internal CodeTypeDeclaration BaseType
		{
			get
			{
				if (this.partialClass == null)
				{
					return this.DerivedType;
				}
				return this.partialClass;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x000B749F File Offset: 0x000B569F
		internal TemplateParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x0400246D RID: 9325
		private const string DEFAULT_NAMESPACE = "ASP";

		// Token: 0x0400246E RID: 9326
		internal static Guid HashMD5 = new Guid(1080993376, 25807, 19586, 182, 240, 66, 212, 129, 114, 167, 153);

		// Token: 0x0400246F RID: 9327
		private static BindingFlags replaceableFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002470 RID: 9328
		private TemplateParser parser;

		// Token: 0x04002471 RID: 9329
		private CodeDomProvider provider;

		// Token: 0x04002472 RID: 9330
		private ICodeCompiler compiler;

		// Token: 0x04002473 RID: 9331
		private CodeCompileUnit unit;

		// Token: 0x04002474 RID: 9332
		private CodeNamespace mainNS;

		// Token: 0x04002475 RID: 9333
		private CompilerParameters compilerParameters;

		// Token: 0x04002476 RID: 9334
		private bool isRebuilding;

		// Token: 0x04002477 RID: 9335
		protected Hashtable partialNameOverride = new Hashtable();

		// Token: 0x04002478 RID: 9336
		protected CodeTypeDeclaration partialClass;

		// Token: 0x04002479 RID: 9337
		protected CodeTypeReferenceExpression partialClassExpr;

		// Token: 0x0400247A RID: 9338
		protected CodeTypeDeclaration mainClass;

		// Token: 0x0400247B RID: 9339
		protected CodeTypeReferenceExpression mainClassExpr;

		// Token: 0x0400247C RID: 9340
		protected static CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();

		// Token: 0x0400247D RID: 9341
		private VirtualPath inputVirtualPath;
	}
}
