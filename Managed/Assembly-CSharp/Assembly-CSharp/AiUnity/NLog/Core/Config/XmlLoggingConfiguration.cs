using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using AiUnity.Common.Extensions;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Filters;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;
using AiUnity.NLog.Core.Targets;
using AiUnity.NLog.Core.Targets.Wrappers;
using AiUnity.NLog.Core.Time;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200029D RID: 669
	public class XmlLoggingConfiguration : LoggingConfiguration
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0007B89C File Offset: 0x00079A9C
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0007B8A4 File Offset: 0x00079AA4
		public XmlLoggingConfiguration(string fileName)
		{
			using (XmlReader xmlReader = XmlReader.Create(fileName))
			{
				this.Initialize(xmlReader, fileName, false);
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0007B910 File Offset: 0x00079B10
		public XmlLoggingConfiguration(string fileName, bool ignoreErrors)
		{
			using (XmlReader xmlReader = XmlReader.Create(fileName))
			{
				this.Initialize(xmlReader, fileName, ignoreErrors);
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0007B97C File Offset: 0x00079B7C
		public XmlLoggingConfiguration(XmlReader reader, string fileName)
		{
			this.Initialize(reader, fileName, false);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0007B9B8 File Offset: 0x00079BB8
		public XmlLoggingConfiguration(XmlReader reader, string fileName, bool ignoreErrors)
		{
			this.Initialize(reader, fileName, ignoreErrors);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0007B9F4 File Offset: 0x00079BF4
		public XmlLoggingConfiguration(string text, string fileName)
		{
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(text)))
			{
				this.Initialize(xmlReader, fileName, false);
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0007BA64 File Offset: 0x00079C64
		public XmlLoggingConfiguration(string text, string fileName, bool ignoreErrors)
		{
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(text)))
			{
				this.Initialize(xmlReader, fileName, ignoreErrors);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007BAD4 File Offset: 0x00079CD4
		internal XmlLoggingConfiguration(XmlElement element, string fileName)
		{
			using (StringReader stringReader = new StringReader(element.OuterXml))
			{
				XmlReader xmlReader = XmlReader.Create(stringReader);
				this.Initialize(xmlReader, fileName, false);
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0007BB4C File Offset: 0x00079D4C
		internal XmlLoggingConfiguration(XmlElement element, string fileName, bool ignoreErrors)
		{
			using (StringReader stringReader = new StringReader(element.OuterXml))
			{
				XmlReader xmlReader = XmlReader.Create(stringReader);
				this.Initialize(xmlReader, fileName, ignoreErrors);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0007BBC4 File Offset: 0x00079DC4
		public Dictionary<string, string> Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0007BBCC File Offset: 0x00079DCC
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x0007BBD4 File Offset: 0x00079DD4
		public bool AutoReload { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0007BBDD File Offset: 0x00079DDD
		public override IEnumerable<string> FileNamesToWatch
		{
			get
			{
				if (this.AutoReload)
				{
					return this.visitedFile.Keys;
				}
				return new string[0];
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0007BBFC File Offset: 0x00079DFC
		public override LoggingConfiguration Reload()
		{
			string configText = Singleton<NLogConfigFile>.Instance.GetConfigText();
			if (!string.IsNullOrEmpty(configText))
			{
				return new XmlLoggingConfiguration(configText, Singleton<NLogConfigFile>.Instance.FileInfo.FullName);
			}
			XmlLoggingConfiguration.Logger.Info("Using default configuration because unable to locate {0}.", new object[] { Singleton<NLogConfigFile>.Instance.RelativeName });
			return null;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007BC55 File Offset: 0x00079E55
		private static bool IsTargetElement(string name)
		{
			return name.Equals("target", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0007BC8F File Offset: 0x00079E8F
		private static bool IsTargetRefElement(string name)
		{
			return name.Equals("target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target-ref", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0007BCBB File Offset: 0x00079EBB
		private static string CleanWhitespace(string s)
		{
			s = s.Replace(" ", string.Empty);
			return s;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0007BCD0 File Offset: 0x00079ED0
		private static string StripOptionalNamespacePrefix(string attributeValue)
		{
			if (attributeValue == null)
			{
				return null;
			}
			int num = attributeValue.IndexOf(':');
			if (num < 0)
			{
				return attributeValue;
			}
			return attributeValue.Substring(num + 1);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0007BCFC File Offset: 0x00079EFC
		private static Target WrapWithAsyncTargetWrapper(Target target)
		{
			AsyncTargetWrapper asyncTargetWrapper = new AsyncTargetWrapper();
			asyncTargetWrapper.WrappedTarget = target;
			asyncTargetWrapper.Name = target.Name;
			target.Name += "_wrapped";
			XmlLoggingConfiguration.Logger.Debug("Wrapping target '{0}' with AsyncTargetWrapper and renaming to '{1}", new object[] { asyncTargetWrapper.Name, target.Name });
			target = asyncTargetWrapper;
			return target;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0007BD64 File Offset: 0x00079F64
		private void Initialize(XmlReader reader, string fileName, bool ignoreErrors)
		{
			try
			{
				reader.MoveToContent();
				NLogXmlElement nlogXmlElement = null;
				try
				{
					nlogXmlElement = new NLogXmlElement(reader);
				}
				catch (Exception ex)
				{
					XmlLoggingConfiguration.Logger.Error("Fail to read configuration file {0} because {1}", new object[] { fileName, ex.Message });
				}
				if (fileName != null)
				{
					XmlLoggingConfiguration.Logger.Info("Initialize NLog based upon {0}", new object[] { fileName });
					string fullPath = Path.GetFullPath(fileName);
					this.visitedFile[fullPath] = true;
					this.ParseTopLevel(nlogXmlElement, Path.GetDirectoryName(fileName));
				}
				else
				{
					this.ParseTopLevel(nlogXmlElement, null);
				}
			}
			catch (Exception ex2)
			{
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				NLogConfigurationException ex3 = new NLogConfigurationException("Exception occurred when loading configuration from " + fileName, ex2);
				if (!ignoreErrors)
				{
					if (Singleton<NLogManager>.Instance.ThrowExceptions)
					{
						throw ex3;
					}
					XmlLoggingConfiguration.Logger.Error("Error in Parsing Configuration File. Exception : {0}", new object[] { ex3 });
				}
				else
				{
					XmlLoggingConfiguration.Logger.Assert(ex3, "Error in Parsing Configuration File. Exception : {0}", Array.Empty<object>());
				}
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0007BE74 File Offset: 0x0007A074
		private void ConfigureFromFile(string fileName)
		{
			string fullPath = Path.GetFullPath(fileName);
			if (this.visitedFile.ContainsKey(fullPath))
			{
				return;
			}
			this.visitedFile[fullPath] = true;
			this.ParseTopLevel(new NLogXmlElement(fileName), Path.GetDirectoryName(fileName));
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0007BEB8 File Offset: 0x0007A0B8
		private void ParseTopLevel(NLogXmlElement content, string baseDirectory)
		{
			content.AssertName(new string[] { "nlog", "configuration" });
			string text = content.LocalName.ToUpper(CultureInfo.InvariantCulture);
			if (text == "CONFIGURATION")
			{
				this.ParseConfigurationElement(content, baseDirectory);
				return;
			}
			if (!(text == "NLOG"))
			{
				return;
			}
			this.ParseNLogElement(content, baseDirectory);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0007BF20 File Offset: 0x0007A120
		private void ParseConfigurationElement(NLogXmlElement configurationElement, string baseDirectory)
		{
			XmlLoggingConfiguration.Logger.Trace("ParseConfigurationElement", Array.Empty<object>());
			configurationElement.AssertName(new string[] { "configuration" });
			foreach (NLogXmlElement nlogXmlElement in configurationElement.Elements("nlog"))
			{
				this.ParseNLogElement(nlogXmlElement, baseDirectory);
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0007BF9C File Offset: 0x0007A19C
		private void ParseNLogElement(NLogXmlElement nlogElement, string baseDirectory)
		{
			XmlLoggingConfiguration.Logger.Trace("ParseNLogElement", Array.Empty<object>());
			nlogElement.AssertName(new string[] { "nlog" });
			if (nlogElement.GetOptionalBooleanAttribute("useInvariantCulture", false))
			{
				base.DefaultCultureInfo = CultureInfo.InvariantCulture;
			}
			this.AutoReload = nlogElement.GetOptionalBooleanAttribute("autoReload", false);
			Singleton<NLogManager>.Instance.ThrowExceptions = nlogElement.GetOptionalBooleanAttribute("throwExceptions", Singleton<NLogManager>.Instance.ThrowExceptions);
			Singleton<NLogManager>.Instance.GlobalLogLevel = LogLevels.Everything;
			Singleton<NLogManager>.Instance.AssertException = bool.Parse(nlogElement.GetOptionalAttribute("assertException", false.ToString()));
			Singleton<NLogManager>.Instance.ShowUnityLog = bool.Parse(nlogElement.GetOptionalAttribute("enableUnityLogListener", false.ToString()));
			foreach (NLogXmlElement nlogXmlElement in nlogElement.Children)
			{
				string text = nlogXmlElement.LocalName.ToUpper(CultureInfo.InvariantCulture);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 582941476U)
				{
					if (num != 79073797U)
					{
						if (num != 221811691U)
						{
							if (num != 582941476U)
							{
								goto IL_0207;
							}
							if (!(text == "TIME"))
							{
								goto IL_0207;
							}
							this.ParseTimeElement(nlogXmlElement);
							continue;
						}
						else if (!(text == "APPENDERS"))
						{
							goto IL_0207;
						}
					}
					else
					{
						if (!(text == "VARIABLE"))
						{
							goto IL_0207;
						}
						this.ParseVariableElement(nlogXmlElement);
						continue;
					}
				}
				else if (num <= 1086804487U)
				{
					if (num != 1065641344U)
					{
						if (num != 1086804487U)
						{
							goto IL_0207;
						}
						if (!(text == "INCLUDE"))
						{
							goto IL_0207;
						}
						this.ParseIncludeElement(nlogXmlElement, baseDirectory);
						continue;
					}
					else
					{
						if (!(text == "RULES"))
						{
							goto IL_0207;
						}
						this.ParseRulesElement(nlogXmlElement, base.LoggingRules);
						continue;
					}
				}
				else if (num != 1482583691U)
				{
					if (num != 4066453633U)
					{
						goto IL_0207;
					}
					if (!(text == "TARGETS"))
					{
						goto IL_0207;
					}
				}
				else
				{
					if (!(text == "EXTENSIONS"))
					{
						goto IL_0207;
					}
					this.ParseExtensionsElement(nlogXmlElement, baseDirectory);
					continue;
				}
				this.ParseTargetsElement(nlogXmlElement);
				continue;
				IL_0207:
				XmlLoggingConfiguration.Logger.Warn("Skipping unknown node: {0}", new object[] { nlogXmlElement.LocalName });
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0007C204 File Offset: 0x0007A404
		private void ParseRulesElement(NLogXmlElement rulesElement, IList<LoggingRule> rulesCollection)
		{
			XmlLoggingConfiguration.Logger.Trace("ParseRulesElement", Array.Empty<object>());
			rulesElement.AssertName(new string[] { "rules" });
			foreach (NLogXmlElement nlogXmlElement in rulesElement.Elements("logger"))
			{
				this.ParseLoggerElement(nlogXmlElement, rulesCollection);
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0007C280 File Offset: 0x0007A480
		private void ParseLoggerElement(NLogXmlElement loggerElement, IList<LoggingRule> rulesCollection)
		{
			loggerElement.AssertName(new string[] { "logger" });
			if (!loggerElement.GetOptionalBooleanAttribute("enabled", true))
			{
				XmlLoggingConfiguration.Logger.Debug("The logger named '{0}' are disabled", Array.Empty<object>());
				return;
			}
			LoggingRule loggingRule = new LoggingRule();
			string text = loggerElement.GetOptionalAttribute("appendTo", null);
			if (text == null)
			{
				text = loggerElement.GetOptionalAttribute("target", null);
			}
			loggingRule.namePatternMatch.LoggerNamePattern = loggerElement.GetOptionalAttribute("name", "*");
			loggingRule.namespacePatternMatch.LoggerNamePattern = loggerElement.GetOptionalAttribute("namespace", "*");
			loggingRule.TargetPlatforms = loggerElement.GetOptionalAttribute("platforms", "Everything");
			if (text != null)
			{
				string[] array = text.Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					Target target = base.FindTargetByName(text2);
					if (target == null)
					{
						throw new NLogConfigurationException(string.Format("The rule having pattern \"{0}\" has an unknown target \"{1}\".", loggingRule.namePatternMatch.LoggerNamePattern, text2));
					}
					loggingRule.Targets.Add(target);
				}
			}
			loggingRule.Final = loggerElement.GetOptionalBooleanAttribute("final", false);
			string text3;
			if (loggerElement.AttributeValues.TryGetValue("level", out text3))
			{
				LogLevels logLevels = text3.ToEnum((LogLevels)0);
				loggingRule.EnableLoggingForLevel(logLevels);
			}
			else if (loggerElement.AttributeValues.TryGetValue("levels", out text3))
			{
				text3 = XmlLoggingConfiguration.CleanWhitespace(text3);
				string[] array = text3.Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						LogLevels logLevels2 = text3.ToEnum((LogLevels)0);
						loggingRule.EnableLoggingForLevel(logLevels2);
					}
				}
			}
			else
			{
				LogLevels logLevels3 = LogLevels.Assert;
				LogLevels logLevels4 = LogLevels.Trace;
				string text4;
				if (loggerElement.AttributeValues.TryGetValue("minLevel", out text4))
				{
					logLevels3 = text4.ToEnum((LogLevels)0);
				}
				string text5;
				if (loggerElement.AttributeValues.TryGetValue("maxLevel", out text5))
				{
					logLevels4 = text5.ToEnum((LogLevels)0);
				}
				for (int j = (int)logLevels3; j <= (int)logLevels4; j <<= 1)
				{
					loggingRule.EnableLoggingForLevel((LogLevels)j);
				}
			}
			foreach (NLogXmlElement nlogXmlElement in loggerElement.Children)
			{
				string text6 = nlogXmlElement.LocalName.ToUpper(CultureInfo.InvariantCulture);
				if (!(text6 == "FILTERS"))
				{
					if (text6 == "LOGGER")
					{
						this.ParseLoggerElement(nlogXmlElement, loggingRule.ChildRules);
					}
				}
				else
				{
					this.ParseFilters(loggingRule, nlogXmlElement);
				}
			}
			rulesCollection.Add(loggingRule);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0007C51C File Offset: 0x0007A71C
		private void ParseFilters(LoggingRule rule, NLogXmlElement filtersElement)
		{
			filtersElement.AssertName(new string[] { "filters" });
			foreach (NLogXmlElement nlogXmlElement in filtersElement.Children)
			{
				string localName = nlogXmlElement.LocalName;
				Filter filter = this.configurationItemFactory.Filters.CreateInstance(localName);
				this.ConfigureObjectFromAttributes(filter, nlogXmlElement, false);
				rule.Filters.Add(filter);
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0007C5A4 File Offset: 0x0007A7A4
		private void ParseVariableElement(NLogXmlElement variableElement)
		{
			variableElement.AssertName(new string[] { "variable" });
			string requiredAttribute = variableElement.GetRequiredAttribute("name");
			string text = this.ExpandVariables(variableElement.GetRequiredAttribute("value"));
			this.variables[requiredAttribute] = text;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		private void ParseTargetsElement(NLogXmlElement targetsElement)
		{
			targetsElement.AssertName(new string[] { "targets", "appenders" });
			bool optionalBooleanAttribute = targetsElement.GetOptionalBooleanAttribute("async", false);
			NLogXmlElement nlogXmlElement = null;
			Dictionary<string, NLogXmlElement> dictionary = new Dictionary<string, NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement2 in targetsElement.Children)
			{
				string localName = nlogXmlElement2.LocalName;
				string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement2.GetOptionalAttribute("type", null));
				string text2 = localName.ToUpper(CultureInfo.InvariantCulture);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1970515547U)
				{
					if (num != 934512166U)
					{
						if (num != 1924432750U)
						{
							if (num != 1970515547U)
							{
								continue;
							}
							if (!(text2 == "DEFAULT-TARGET-PARAMETERS"))
							{
								continue;
							}
							if (text == null)
							{
								throw new NLogConfigurationException("Missing 'type' attribute on <" + localName + "/>.");
							}
							dictionary[text] = nlogXmlElement2;
							continue;
						}
						else if (!(text2 == "WRAPPER-TARGET"))
						{
							continue;
						}
					}
					else if (!(text2 == "COMPOUND-TARGET"))
					{
						continue;
					}
				}
				else if (num <= 3098859044U)
				{
					if (num != 2730440272U)
					{
						if (num != 3098859044U)
						{
							continue;
						}
						if (!(text2 == "WRAPPER"))
						{
							continue;
						}
					}
					else
					{
						if (!(text2 == "DEFAULT-WRAPPER"))
						{
							continue;
						}
						nlogXmlElement = nlogXmlElement2;
						continue;
					}
				}
				else if (num != 3535797018U)
				{
					if (num != 4080126536U)
					{
						continue;
					}
					if (!(text2 == "TARGET"))
					{
						continue;
					}
				}
				else if (!(text2 == "APPENDER"))
				{
					continue;
				}
				if (text == null)
				{
					throw new NLogConfigurationException("Missing 'type' attribute on <" + localName + "/>.");
				}
				Target target = this.configurationItemFactory.Targets.CreateInstance(text);
				NLogXmlElement nlogXmlElement3;
				if (dictionary.TryGetValue(text, out nlogXmlElement3))
				{
					this.ParseTargetElement(target, nlogXmlElement3);
				}
				this.ParseTargetElement(target, nlogXmlElement2);
				if (optionalBooleanAttribute)
				{
					target = XmlLoggingConfiguration.WrapWithAsyncTargetWrapper(target);
				}
				if (nlogXmlElement != null)
				{
					target = this.WrapWithDefaultWrapper(target, nlogXmlElement);
				}
				XmlLoggingConfiguration.Logger.Debug("Adding target {0}", new object[] { target });
				base.AddTarget(target.Name, target);
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0007C860 File Offset: 0x0007AA60
		private void ParseTargetElement(Target target, NLogXmlElement targetElement)
		{
			CompoundTargetBase compoundTargetBase = target as CompoundTargetBase;
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			this.ConfigureObjectFromAttributes(target, targetElement, true);
			foreach (NLogXmlElement nlogXmlElement in targetElement.Children)
			{
				string localName = nlogXmlElement.LocalName;
				if (compoundTargetBase != null)
				{
					if (XmlLoggingConfiguration.IsTargetRefElement(localName))
					{
						string requiredAttribute = nlogXmlElement.GetRequiredAttribute("name");
						Target target2 = base.FindTargetByName(requiredAttribute);
						if (target2 == null)
						{
							throw new NLogConfigurationException("Referenced target '" + requiredAttribute + "' not found.");
						}
						compoundTargetBase.Targets.Add(target2);
						continue;
					}
					else if (XmlLoggingConfiguration.IsTargetElement(localName))
					{
						string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetRequiredAttribute("type"));
						Target target3 = this.configurationItemFactory.Targets.CreateInstance(text);
						if (target3 != null)
						{
							this.ParseTargetElement(target3, nlogXmlElement);
							if (target3.Name != null)
							{
								base.AddTarget(target3.Name, target3);
							}
							compoundTargetBase.Targets.Add(target3);
							continue;
						}
						continue;
					}
				}
				if (wrapperTargetBase != null)
				{
					if (XmlLoggingConfiguration.IsTargetRefElement(localName))
					{
						string requiredAttribute2 = nlogXmlElement.GetRequiredAttribute("name");
						Target target4 = base.FindTargetByName(requiredAttribute2);
						if (target4 == null)
						{
							throw new NLogConfigurationException("Referenced target '" + requiredAttribute2 + "' not found.");
						}
						wrapperTargetBase.WrappedTarget = target4;
						continue;
					}
					else if (XmlLoggingConfiguration.IsTargetElement(localName))
					{
						string text2 = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetRequiredAttribute("type"));
						Target target5 = this.configurationItemFactory.Targets.CreateInstance(text2);
						if (target5 == null)
						{
							continue;
						}
						this.ParseTargetElement(target5, nlogXmlElement);
						if (target5.Name != null)
						{
							base.AddTarget(target5.Name, target5);
						}
						if (wrapperTargetBase.WrappedTarget != null)
						{
							throw new NLogConfigurationException("Wrapper target can only have one child.");
						}
						wrapperTargetBase.WrappedTarget = target5;
						continue;
					}
				}
				this.SetPropertyFromElement(target, nlogXmlElement);
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0007CA58 File Offset: 0x0007AC58
		private void ParseExtensionsElement(NLogXmlElement extensionsElement, string baseDirectory)
		{
			extensionsElement.AssertName(new string[] { "extensions" });
			foreach (NLogXmlElement nlogXmlElement in extensionsElement.Elements("add"))
			{
				string text = nlogXmlElement.GetOptionalAttribute("prefix", null);
				if (text != null)
				{
					text += ".";
				}
				string text2 = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetOptionalAttribute("type", null));
				if (text2 != null)
				{
					this.configurationItemFactory.RegisterType(Type.GetType(text2, true), text);
				}
				string optionalAttribute = nlogXmlElement.GetOptionalAttribute("assemblyFile", null);
				if (optionalAttribute != null)
				{
					try
					{
						string text3 = Path.Combine(baseDirectory, optionalAttribute);
						XmlLoggingConfiguration.Logger.Info("Loading assembly file: {0}", new object[] { text3 });
						Assembly assembly = Assembly.LoadFrom(text3);
						this.configurationItemFactory.RegisterItemsFromAssembly(assembly, text);
						continue;
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						XmlLoggingConfiguration.Logger.Error("Error loading extensions: {0}", new object[] { ex });
						if (Singleton<NLogManager>.Instance.ThrowExceptions)
						{
							throw new NLogConfigurationException("Error loading extensions: " + optionalAttribute, ex);
						}
						continue;
					}
				}
				string optionalAttribute2 = nlogXmlElement.GetOptionalAttribute("assembly", null);
				if (optionalAttribute2 != null)
				{
					try
					{
						XmlLoggingConfiguration.Logger.Info("Loading assembly name: {0}", new object[] { optionalAttribute2 });
						Assembly assembly2 = Assembly.Load(optionalAttribute2);
						this.configurationItemFactory.RegisterItemsFromAssembly(assembly2, text);
					}
					catch (Exception ex2)
					{
						if (ex2.MustBeRethrown())
						{
							throw;
						}
						XmlLoggingConfiguration.Logger.Error("Error loading extensions: {0}", new object[] { ex2 });
						if (Singleton<NLogManager>.Instance.ThrowExceptions)
						{
							throw new NLogConfigurationException("Error loading extensions: " + optionalAttribute2, ex2);
						}
					}
				}
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0007CC6C File Offset: 0x0007AE6C
		private void ParseIncludeElement(NLogXmlElement includeElement, string baseDirectory)
		{
			includeElement.AssertName(new string[] { "include" });
			string text = includeElement.GetRequiredAttribute("file");
			try
			{
				text = this.ExpandVariables(text);
				text = SimpleLayout.Evaluate(text);
				if (baseDirectory != null)
				{
					text = Path.Combine(baseDirectory, text);
				}
				if (!File.Exists(text))
				{
					throw new FileNotFoundException("Included file not found: " + text);
				}
				XmlLoggingConfiguration.Logger.Debug("Including file '{0}'", new object[] { text });
				this.ConfigureFromFile(text);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				XmlLoggingConfiguration.Logger.Error("Error when including '{0}' {1}", new object[] { text, ex });
				if (!includeElement.GetOptionalBooleanAttribute("ignoreErrors", false))
				{
					throw new NLogConfigurationException("Error when including: " + text, ex);
				}
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0007CD4C File Offset: 0x0007AF4C
		private void ParseTimeElement(NLogXmlElement timeElement)
		{
			timeElement.AssertName(new string[] { "time" });
			string requiredAttribute = timeElement.GetRequiredAttribute("type");
			TimeSource timeSource = this.configurationItemFactory.TimeSources.CreateInstance(requiredAttribute);
			this.ConfigureObjectFromAttributes(timeSource, timeElement, true);
			XmlLoggingConfiguration.Logger.Debug("Selecting time source {0}", new object[] { timeSource });
			TimeSource.Current = timeSource;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0007CDB3 File Offset: 0x0007AFB3
		private void SetPropertyFromElement(object o, NLogXmlElement element)
		{
			if (this.AddArrayItemFromElement(o, element))
			{
				return;
			}
			if (this.SetLayoutFromElement(o, element))
			{
				return;
			}
			PropertyHelper.SetPropertyFromString(o, element.LocalName, this.ExpandVariables(element.Value), this.configurationItemFactory);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0007CDEC File Offset: 0x0007AFEC
		private bool AddArrayItemFromElement(object o, NLogXmlElement element)
		{
			string localName = element.LocalName;
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(o, localName, out propertyInfo))
			{
				return false;
			}
			Type arrayItemType = PropertyHelper.GetArrayItemType(propertyInfo);
			if (arrayItemType != null)
			{
				IList list = (IList)propertyInfo.GetValue(o, null);
				object obj = FactoryHelper.CreateInstance(arrayItemType);
				this.ConfigureObjectFromAttributes(obj, element, true);
				this.ConfigureObjectFromElement(obj, element);
				list.Add(obj);
				return true;
			}
			return false;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0007CE4C File Offset: 0x0007B04C
		private void ConfigureObjectFromAttributes(object targetObject, NLogXmlElement element, bool ignoreType)
		{
			foreach (KeyValuePair<string, string> keyValuePair in element.AttributeValues)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!ignoreType || !key.Equals("type", StringComparison.OrdinalIgnoreCase))
				{
					PropertyHelper.SetPropertyFromString(targetObject, key, this.ExpandVariables(value), this.configurationItemFactory);
				}
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0007CED0 File Offset: 0x0007B0D0
		private bool SetLayoutFromElement(object o, NLogXmlElement layoutElement)
		{
			string localName = layoutElement.LocalName;
			PropertyInfo propertyInfo;
			if (PropertyHelper.TryGetPropertyInfo(o, localName, out propertyInfo) && typeof(Layout).IsAssignableFrom(propertyInfo.PropertyType))
			{
				string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(layoutElement.GetOptionalAttribute("type", null));
				if (text != null)
				{
					Layout layout = this.configurationItemFactory.Layouts.CreateInstance(this.ExpandVariables(text));
					this.ConfigureObjectFromAttributes(layout, layoutElement, true);
					this.ConfigureObjectFromElement(layout, layoutElement);
					propertyInfo.SetValue(o, layout, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0007CF50 File Offset: 0x0007B150
		private void ConfigureObjectFromElement(object targetObject, NLogXmlElement element)
		{
			foreach (NLogXmlElement nlogXmlElement in element.Children)
			{
				this.SetPropertyFromElement(targetObject, nlogXmlElement);
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0007CFA0 File Offset: 0x0007B1A0
		private Target WrapWithDefaultWrapper(Target t, NLogXmlElement defaultParameters)
		{
			string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(defaultParameters.GetRequiredAttribute("type"));
			Target target = this.configurationItemFactory.Targets.CreateInstance(text);
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			if (wrapperTargetBase == null)
			{
				throw new NLogConfigurationException("Target type specified on <default-wrapper /> is not a wrapper.");
			}
			this.ParseTargetElement(target, defaultParameters);
			while (wrapperTargetBase.WrappedTarget != null)
			{
				wrapperTargetBase = wrapperTargetBase.WrappedTarget as WrapperTargetBase;
				if (wrapperTargetBase == null)
				{
					throw new NLogConfigurationException("Child target type specified on <default-wrapper /> is not a wrapper.");
				}
			}
			wrapperTargetBase.WrappedTarget = t;
			target.Name = t.Name;
			t.Name += "_wrapped";
			XmlLoggingConfiguration.Logger.Debug("Wrapping target '{0}' with '{1}' and renaming to '{2}", new object[]
			{
				target.Name,
				target.GetType().Name,
				t.Name
			});
			return target;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0007D070 File Offset: 0x0007B270
		private string ExpandVariables(string input)
		{
			string text = input;
			foreach (KeyValuePair<string, string> keyValuePair in this.variables)
			{
				text = text.Replace("${" + keyValuePair.Key + "}", keyValuePair.Value);
			}
			return text;
		}

		// Token: 0x04000E34 RID: 3636
		private readonly ConfigurationItemFactory configurationItemFactory = ConfigurationItemFactory.Default;

		// Token: 0x04000E35 RID: 3637
		private readonly Dictionary<string, bool> visitedFile = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E36 RID: 3638
		private readonly Dictionary<string, string> variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
