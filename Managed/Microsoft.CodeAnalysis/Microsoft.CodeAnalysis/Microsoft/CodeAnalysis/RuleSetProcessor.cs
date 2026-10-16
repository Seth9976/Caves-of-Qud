using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D4 RID: 468
	internal class RuleSetProcessor
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x0003D454 File Offset: 0x0003B654
		[NullableContext(1)]
		public static RuleSet LoadFromFile(string filePath)
		{
			filePath = FileUtilities.NormalizeAbsolutePath(filePath);
			XmlReaderSettings defaultXmlReaderSettings = RuleSetProcessor.GetDefaultXmlReaderSettings();
			XDocument xdocument = null;
			XElement xelement = null;
			using (Stream stream = FileUtilities.OpenRead(filePath))
			{
				using (XmlReader xmlReader = XmlReader.Create(stream, defaultXmlReaderSettings))
				{
					try
					{
						xdocument = XDocument.Load(xmlReader);
					}
					catch (Exception ex)
					{
						throw new InvalidRuleSetException(ex.Message);
					}
					xelement = Enumerable.ToList<XElement>(xdocument.Elements("RuleSet"))[0];
				}
			}
			return RuleSetProcessor.ReadRuleSet(xelement, filePath);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003D4FC File Offset: 0x0003B6FC
		[NullableContext(1)]
		private static RuleSet ReadRuleSet(XElement ruleSetNode, string filePath)
		{
			ImmutableDictionary<string, ReportDiagnostic>.Builder builder = ImmutableDictionary.CreateBuilder<string, ReportDiagnostic>();
			ReportDiagnostic reportDiagnostic = ReportDiagnostic.Default;
			ImmutableArray<RuleSetInclude>.Builder builder2 = ImmutableArray.CreateBuilder<RuleSetInclude>();
			RuleSetProcessor.ValidateAttribute(ruleSetNode, "ToolsVersion");
			RuleSetProcessor.ValidateAttribute(ruleSetNode, "Name");
			foreach (XElement xelement in ruleSetNode.Elements())
			{
				if (xelement.Name == "Rules")
				{
					using (List<KeyValuePair<string, ReportDiagnostic>>.Enumerator enumerator2 = RuleSetProcessor.ReadRules(xelement).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<string, ReportDiagnostic> keyValuePair = enumerator2.Current;
							string key = keyValuePair.Key;
							ReportDiagnostic value = keyValuePair.Value;
							ReportDiagnostic reportDiagnostic2;
							if (builder.TryGetValue(key, out reportDiagnostic2))
							{
								if (reportDiagnostic2 != value)
								{
									throw new InvalidRuleSetException(string.Format(CodeAnalysisResources.RuleSetHasDuplicateRules, key, reportDiagnostic2, value));
								}
							}
							else
							{
								builder.Add(key, value);
							}
						}
						continue;
					}
				}
				if (xelement.Name == "Include")
				{
					builder2.Add(RuleSetProcessor.ReadRuleSetInclude(xelement));
				}
				else if (xelement.Name == "IncludeAll")
				{
					reportDiagnostic = RuleSetProcessor.ReadIncludeAll(xelement);
				}
			}
			return new RuleSet(filePath, reportDiagnostic, builder.ToImmutable(), builder2.ToImmutable());
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003D670 File Offset: 0x0003B870
		private static List<KeyValuePair<string, ReportDiagnostic>> ReadRules(XElement rulesNode)
		{
			string text = RuleSetProcessor.ReadNonEmptyAttribute(rulesNode, "AnalyzerId");
			string text2 = RuleSetProcessor.ReadNonEmptyAttribute(rulesNode, "RuleNamespace");
			List<KeyValuePair<string, ReportDiagnostic>> list = new List<KeyValuePair<string, ReportDiagnostic>>();
			foreach (XElement xelement in rulesNode.Elements())
			{
				if (xelement.Name == "Rule")
				{
					list.Add(RuleSetProcessor.ReadRule(xelement, text, text2));
				}
			}
			return list;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003D700 File Offset: 0x0003B900
		private static KeyValuePair<string, ReportDiagnostic> ReadRule(XElement ruleNode, string analyzer, string space)
		{
			string text = RuleSetProcessor.ReadNonEmptyAttribute(ruleNode, "Id");
			ReportDiagnostic reportDiagnostic = RuleSetProcessor.ReadAction(ruleNode, false);
			return new KeyValuePair<string, ReportDiagnostic>(text, reportDiagnostic);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003D728 File Offset: 0x0003B928
		private static RuleSetInclude ReadRuleSetInclude(XElement includeNode)
		{
			string text = RuleSetProcessor.ReadNonEmptyAttribute(includeNode, "Path");
			ReportDiagnostic reportDiagnostic = RuleSetProcessor.ReadAction(includeNode, true);
			return new RuleSetInclude(text, reportDiagnostic);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0003D750 File Offset: 0x0003B950
		private static ReportDiagnostic ReadAction(XElement node, bool allowDefault)
		{
			string text = RuleSetProcessor.ReadNonEmptyAttribute(node, "Action");
			if (string.Equals(text, "Warning"))
			{
				return ReportDiagnostic.Warn;
			}
			if (string.Equals(text, "Error"))
			{
				return ReportDiagnostic.Error;
			}
			if (string.Equals(text, "Info"))
			{
				return ReportDiagnostic.Info;
			}
			if (string.Equals(text, "Hidden"))
			{
				return ReportDiagnostic.Hidden;
			}
			if (string.Equals(text, "None"))
			{
				return ReportDiagnostic.Suppress;
			}
			if (string.Equals(text, "Default") && allowDefault)
			{
				return ReportDiagnostic.Default;
			}
			throw new InvalidRuleSetException(string.Format(CodeAnalysisResources.RuleSetBadAttributeValue, "Action", text));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0003D7DA File Offset: 0x0003B9DA
		private static ReportDiagnostic ReadIncludeAll(XElement includeAllNode)
		{
			return RuleSetProcessor.ReadAction(includeAllNode, false);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		private static string ReadNonEmptyAttribute(XElement node, string attributeName)
		{
			XAttribute xattribute = node.Attribute(attributeName);
			if (xattribute == null)
			{
				throw new InvalidRuleSetException(string.Format(CodeAnalysisResources.RuleSetMissingAttribute, node.Name, attributeName));
			}
			if (string.IsNullOrEmpty(xattribute.Value))
			{
				throw new InvalidRuleSetException(string.Format(CodeAnalysisResources.RuleSetBadAttributeValue, attributeName, xattribute.Value));
			}
			return xattribute.Value;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003D842 File Offset: 0x0003BA42
		private static XmlReaderSettings GetDefaultXmlReaderSettings()
		{
			return new XmlReaderSettings
			{
				CheckCharacters = true,
				CloseInput = true,
				ConformanceLevel = 2,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				IgnoreWhitespace = true,
				DtdProcessing = 0
			};
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003D87A File Offset: 0x0003BA7A
		private static void ValidateAttribute(XElement node, string attributeName)
		{
			RuleSetProcessor.ReadNonEmptyAttribute(node, attributeName);
		}

		// Token: 0x040005DB RID: 1499
		private const string RuleSetNodeName = "RuleSet";

		// Token: 0x040005DC RID: 1500
		private const string RuleSetNameAttributeName = "Name";

		// Token: 0x040005DD RID: 1501
		private const string RuleSetDescriptionAttributeName = "Description";

		// Token: 0x040005DE RID: 1502
		private const string RuleSetToolsVersionAttributeName = "ToolsVersion";

		// Token: 0x040005DF RID: 1503
		private const string RulesNodeName = "Rules";

		// Token: 0x040005E0 RID: 1504
		private const string RulesAnalyzerIdAttributeName = "AnalyzerId";

		// Token: 0x040005E1 RID: 1505
		private const string RulesNamespaceAttributeName = "RuleNamespace";

		// Token: 0x040005E2 RID: 1506
		private const string RuleNodeName = "Rule";

		// Token: 0x040005E3 RID: 1507
		private const string RuleIdAttributeName = "Id";

		// Token: 0x040005E4 RID: 1508
		private const string IncludeNodeName = "Include";

		// Token: 0x040005E5 RID: 1509
		private const string IncludePathAttributeName = "Path";

		// Token: 0x040005E6 RID: 1510
		private const string IncludeAllNodeName = "IncludeAll";

		// Token: 0x040005E7 RID: 1511
		private const string RuleActionAttributeName = "Action";

		// Token: 0x040005E8 RID: 1512
		private const string RuleActionNoneValue = "None";

		// Token: 0x040005E9 RID: 1513
		private const string RuleActionHiddenValue = "Hidden";

		// Token: 0x040005EA RID: 1514
		private const string RuleActionInfoValue = "Info";

		// Token: 0x040005EB RID: 1515
		private const string RuleActionWarningValue = "Warning";

		// Token: 0x040005EC RID: 1516
		private const string RuleActionErrorValue = "Error";

		// Token: 0x040005ED RID: 1517
		private const string RuleActionDefaultValue = "Default";
	}
}
