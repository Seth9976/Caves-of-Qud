using System;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000509 RID: 1289
	internal static class ObsoleteAttributeHelpers
	{
		// Token: 0x06006C88 RID: 27784 RVA: 0x00190A10 File Offset: 0x0018EC10
		internal static void InitializeObsoleteDataFromMetadata(ref ObsoleteAttributeData data, EntityHandle token, PEModuleSymbol containingModule, bool ignoreByRefLikeMarker)
		{
			if (data == ObsoleteAttributeData.Uninitialized)
			{
				ObsoleteAttributeData obsoleteDataFromMetadata = ObsoleteAttributeHelpers.GetObsoleteDataFromMetadata(token, containingModule, ignoreByRefLikeMarker);
				Interlocked.CompareExchange<ObsoleteAttributeData>(ref data, obsoleteDataFromMetadata, ObsoleteAttributeData.Uninitialized);
			}
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x00190A3C File Offset: 0x0018EC3C
		internal static ObsoleteAttributeData GetObsoleteDataFromMetadata(EntityHandle token, PEModuleSymbol containingModule, bool ignoreByRefLikeMarker)
		{
			return containingModule.Module.TryGetDeprecatedOrExperimentalOrObsoleteAttribute(token, new MetadataDecoder(containingModule), ignoreByRefLikeMarker);
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x00190A54 File Offset: 0x0018EC54
		private static ThreeState GetObsoleteContextState(Symbol symbol, bool forceComplete)
		{
			while (symbol != null)
			{
				if (symbol.Kind == SymbolKind.Field)
				{
					Symbol associatedSymbol = ((FieldSymbol)symbol).AssociatedSymbol;
					if (associatedSymbol != null)
					{
						symbol = associatedSymbol;
					}
				}
				if (forceComplete)
				{
					symbol.ForceCompleteObsoleteAttribute();
				}
				ThreeState obsoleteState = symbol.ObsoleteState;
				if (obsoleteState != ThreeState.False)
				{
					return obsoleteState;
				}
				if (symbol.IsAccessor())
				{
					symbol = ((MethodSymbol)symbol).AssociatedSymbol;
				}
				else
				{
					symbol = symbol.ContainingSymbol;
				}
			}
			return ThreeState.False;
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x00190AB8 File Offset: 0x0018ECB8
		internal static ObsoleteDiagnosticKind GetObsoleteDiagnosticKind(Symbol symbol, Symbol containingMember, bool forceComplete = false)
		{
			switch (symbol.ObsoleteKind)
			{
			case ObsoleteAttributeKind.None:
				return ObsoleteDiagnosticKind.NotObsolete;
			case ObsoleteAttributeKind.Uninitialized:
				return ObsoleteDiagnosticKind.Lazy;
			case ObsoleteAttributeKind.Experimental:
				return ObsoleteDiagnosticKind.Diagnostic;
			}
			ThreeState obsoleteContextState = ObsoleteAttributeHelpers.GetObsoleteContextState(containingMember, forceComplete);
			if (obsoleteContextState == ThreeState.False)
			{
				return ObsoleteDiagnosticKind.Diagnostic;
			}
			if (obsoleteContextState != ThreeState.True)
			{
				return ObsoleteDiagnosticKind.LazyPotentiallySuppressed;
			}
			return ObsoleteDiagnosticKind.Suppressed;
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x00190B08 File Offset: 0x0018ED08
		internal static DiagnosticInfo CreateObsoleteDiagnostic(Symbol symbol, BinderFlags location)
		{
			ObsoleteAttributeData obsoleteAttributeData = symbol.ObsoleteAttributeData;
			if (obsoleteAttributeData == null)
			{
				return null;
			}
			if (location.Includes(BinderFlags.SuppressObsoleteChecks))
			{
				return null;
			}
			if (obsoleteAttributeData.Kind == ObsoleteAttributeKind.Experimental)
			{
				return new CSDiagnosticInfo(ErrorCode.WRN_Experimental, new object[]
				{
					new FormattedSymbol(symbol, SymbolDisplayFormat.CSharpErrorMessageFormat)
				});
			}
			bool flag = location.Includes(BinderFlags.CollectionInitializerAddMethod);
			bool message = obsoleteAttributeData.Message != null;
			bool isError = obsoleteAttributeData.IsError;
			ErrorCode errorCode;
			if (!message)
			{
				if (!flag)
				{
					errorCode = ErrorCode.WRN_DeprecatedSymbol;
				}
				else
				{
					errorCode = ErrorCode.WRN_DeprecatedCollectionInitAdd;
				}
			}
			else if (isError)
			{
				if (!flag)
				{
					errorCode = ErrorCode.ERR_DeprecatedSymbolStr;
				}
				else
				{
					errorCode = ErrorCode.ERR_DeprecatedCollectionInitAddStr;
				}
			}
			else if (!flag)
			{
				errorCode = ErrorCode.WRN_DeprecatedSymbolStr;
			}
			else
			{
				errorCode = ErrorCode.WRN_DeprecatedCollectionInitAddStr;
			}
			ErrorCode errorCode2 = errorCode;
			string message2 = obsoleteAttributeData.Message;
			object[] array;
			if (message2 == null)
			{
				(array = new object[1])[0] = symbol;
			}
			else
			{
				object[] array2 = new object[2];
				array2[0] = symbol;
				array = array2;
				array2[1] = message2;
			}
			object[] array3 = array;
			return new CustomObsoleteDiagnosticInfo(MessageProvider.Instance, (int)errorCode2, obsoleteAttributeData, array3);
		}
	}
}
