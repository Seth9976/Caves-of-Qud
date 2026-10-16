using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000526 RID: 1318
	internal sealed class QuickAttributeChecker
	{
		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x06006EC0 RID: 28352 RVA: 0x0019738A File Offset: 0x0019558A
		internal static QuickAttributeChecker Predefined
		{
			get
			{
				if (QuickAttributeChecker._lazyPredefinedQuickAttributeChecker == null)
				{
					Interlocked.CompareExchange<QuickAttributeChecker>(ref QuickAttributeChecker._lazyPredefinedQuickAttributeChecker, QuickAttributeChecker.CreatePredefinedQuickAttributeChecker(), null);
				}
				return QuickAttributeChecker._lazyPredefinedQuickAttributeChecker;
			}
		}

		// Token: 0x06006EC1 RID: 28353 RVA: 0x001973AC File Offset: 0x001955AC
		private static QuickAttributeChecker CreatePredefinedQuickAttributeChecker()
		{
			QuickAttributeChecker quickAttributeChecker = new QuickAttributeChecker();
			quickAttributeChecker.AddName(AttributeDescription.TypeIdentifierAttribute.Name, QuickAttributes.TypeIdentifier);
			quickAttributeChecker.AddName(AttributeDescription.TypeForwardedToAttribute.Name, QuickAttributes.TypeForwardedTo);
			quickAttributeChecker.AddName(AttributeDescription.AssemblyKeyNameAttribute.Name, QuickAttributes.AssemblyKeyName);
			quickAttributeChecker.AddName(AttributeDescription.AssemblyKeyFileAttribute.Name, QuickAttributes.AssemblyKeyFile);
			quickAttributeChecker.AddName(AttributeDescription.AssemblySignatureKeyAttribute.Name, QuickAttributes.AssemblySignatureKey);
			return quickAttributeChecker;
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x00197414 File Offset: 0x00195614
		private QuickAttributeChecker()
		{
			this._nameToAttributeMap = new Dictionary<string, QuickAttributes>(StringComparer.Ordinal);
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x0019742C File Offset: 0x0019562C
		private QuickAttributeChecker(QuickAttributeChecker previous)
		{
			this._nameToAttributeMap = new Dictionary<string, QuickAttributes>(previous._nameToAttributeMap, StringComparer.Ordinal);
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x0019744C File Offset: 0x0019564C
		private void AddName(string name, QuickAttributes newAttributes)
		{
			QuickAttributes quickAttributes = QuickAttributes.None;
			this._nameToAttributeMap.TryGetValue(name, ref quickAttributes);
			QuickAttributes quickAttributes2 = newAttributes | quickAttributes;
			this._nameToAttributeMap[name] = quickAttributes2;
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x0019747C File Offset: 0x0019567C
		internal QuickAttributeChecker AddAliasesIfAny(SyntaxList<UsingDirectiveSyntax> usingsSyntax, bool onlyGlobalAliases = false)
		{
			if (usingsSyntax.Count == 0)
			{
				return this;
			}
			QuickAttributeChecker quickAttributeChecker = null;
			foreach (UsingDirectiveSyntax usingDirectiveSyntax in usingsSyntax)
			{
				if (usingDirectiveSyntax.Alias != null && (!onlyGlobalAliases || usingDirectiveSyntax.GlobalKeyword.IsKind(SyntaxKind.GlobalKeyword)))
				{
					string valueText = usingDirectiveSyntax.Alias.Name.Identifier.ValueText;
					string valueText2 = usingDirectiveSyntax.Name.GetUnqualifiedName().Identifier.ValueText;
					QuickAttributes quickAttributes;
					if (this._nameToAttributeMap.TryGetValue(valueText2, ref quickAttributes))
					{
						QuickAttributeChecker quickAttributeChecker2;
						if ((quickAttributeChecker2 = quickAttributeChecker) == null)
						{
							quickAttributeChecker2 = (quickAttributeChecker = new QuickAttributeChecker(this));
						}
						quickAttributeChecker2.AddName(valueText, quickAttributes);
					}
				}
			}
			if (quickAttributeChecker != null)
			{
				return quickAttributeChecker;
			}
			return this;
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x00197538 File Offset: 0x00195738
		public bool IsPossibleMatch(AttributeSyntax attr, QuickAttributes pattern)
		{
			string valueText = attr.Name.GetUnqualifiedName().Identifier.ValueText;
			QuickAttributes quickAttributes;
			return (this._nameToAttributeMap.TryGetValue(valueText, ref quickAttributes) || this._nameToAttributeMap.TryGetValue(valueText + "Attribute", ref quickAttributes)) && (quickAttributes & pattern) > QuickAttributes.None;
		}

		// Token: 0x04001B3C RID: 6972
		private readonly Dictionary<string, QuickAttributes> _nameToAttributeMap;

		// Token: 0x04001B3D RID: 6973
		private static QuickAttributeChecker _lazyPredefinedQuickAttributeChecker;
	}
}
