using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200025C RID: 604
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class DllImportData : IPlatformInvokeInformation
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x0004598B File Offset: 0x00043B8B
		internal DllImportData(string moduleName, string entryPointName, MethodImportAttributes flags)
		{
			this._moduleName = moduleName;
			this._entryPointName = entryPointName;
			this._flags = flags;
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000459A8 File Offset: 0x00043BA8
		public string ModuleName
		{
			get
			{
				return this._moduleName;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000459B0 File Offset: 0x00043BB0
		public string EntryPointName
		{
			get
			{
				return this._entryPointName;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x000459B8 File Offset: 0x00043BB8
		MethodImportAttributes IPlatformInvokeInformation.Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x000459C0 File Offset: 0x00043BC0
		public bool ExactSpelling
		{
			get
			{
				return (this._flags & MethodImportAttributes.ExactSpelling) > MethodImportAttributes.None;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000459D0 File Offset: 0x00043BD0
		public CharSet CharacterSet
		{
			get
			{
				switch (this._flags & MethodImportAttributes.CharSetAuto)
				{
				case MethodImportAttributes.None:
					return 1;
				case MethodImportAttributes.CharSetAnsi:
					return 2;
				case MethodImportAttributes.CharSetUnicode:
					return 3;
				case MethodImportAttributes.CharSetAuto:
					return 4;
				}
				throw ExceptionUtilities.UnexpectedValue(this._flags);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00045A22 File Offset: 0x00043C22
		public bool SetLastError
		{
			get
			{
				return (this._flags & MethodImportAttributes.SetLastError) > MethodImportAttributes.None;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00045A30 File Offset: 0x00043C30
		public CallingConvention CallingConvention
		{
			get
			{
				MethodImportAttributes methodImportAttributes = this._flags & MethodImportAttributes.CallingConventionMask;
				if (methodImportAttributes <= MethodImportAttributes.CallingConventionStdCall)
				{
					if (methodImportAttributes == MethodImportAttributes.CallingConventionCDecl)
					{
						return 2;
					}
					if (methodImportAttributes == MethodImportAttributes.CallingConventionStdCall)
					{
						return 3;
					}
				}
				else
				{
					if (methodImportAttributes == MethodImportAttributes.CallingConventionThisCall)
					{
						return 4;
					}
					if (methodImportAttributes == MethodImportAttributes.CallingConventionFastCall)
					{
						return 5;
					}
				}
				return 1;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00045A80 File Offset: 0x00043C80
		public bool? BestFitMapping
		{
			get
			{
				MethodImportAttributes methodImportAttributes = this._flags & MethodImportAttributes.BestFitMappingMask;
				if (methodImportAttributes == MethodImportAttributes.BestFitMappingEnable)
				{
					return new bool?(true);
				}
				if (methodImportAttributes != MethodImportAttributes.BestFitMappingDisable)
				{
					return default(bool?);
				}
				return new bool?(false);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x00045ABC File Offset: 0x00043CBC
		public bool? ThrowOnUnmappableCharacter
		{
			get
			{
				MethodImportAttributes methodImportAttributes = this._flags & MethodImportAttributes.ThrowOnUnmappableCharMask;
				if (methodImportAttributes == MethodImportAttributes.ThrowOnUnmappableCharEnable)
				{
					return new bool?(true);
				}
				if (methodImportAttributes != MethodImportAttributes.ThrowOnUnmappableCharDisable)
				{
					return default(bool?);
				}
				return new bool?(false);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00045B00 File Offset: 0x00043D00
		internal static MethodImportAttributes MakeFlags(bool exactSpelling, CharSet charSet, bool setLastError, CallingConvention callingConvention, bool? useBestFit, bool? throwOnUnmappable)
		{
			MethodImportAttributes methodImportAttributes = MethodImportAttributes.None;
			if (exactSpelling)
			{
				methodImportAttributes |= MethodImportAttributes.ExactSpelling;
			}
			switch (charSet)
			{
			case 2:
				methodImportAttributes |= MethodImportAttributes.CharSetAnsi;
				break;
			case 3:
				methodImportAttributes |= MethodImportAttributes.CharSetUnicode;
				break;
			case 4:
				methodImportAttributes |= MethodImportAttributes.CharSetAuto;
				break;
			}
			if (setLastError)
			{
				methodImportAttributes |= MethodImportAttributes.SetLastError;
			}
			switch (callingConvention)
			{
			case 2:
				methodImportAttributes |= MethodImportAttributes.CallingConventionCDecl;
				break;
			case 3:
				methodImportAttributes |= MethodImportAttributes.CallingConventionStdCall;
				break;
			case 4:
				methodImportAttributes |= MethodImportAttributes.CallingConventionThisCall;
				break;
			case 5:
				methodImportAttributes |= MethodImportAttributes.CallingConventionFastCall;
				break;
			default:
				methodImportAttributes |= MethodImportAttributes.CallingConventionWinApi;
				break;
			}
			if (throwOnUnmappable != null)
			{
				if (throwOnUnmappable.Value)
				{
					methodImportAttributes |= MethodImportAttributes.ThrowOnUnmappableCharEnable;
				}
				else
				{
					methodImportAttributes |= MethodImportAttributes.ThrowOnUnmappableCharDisable;
				}
			}
			if (useBestFit != null)
			{
				if (useBestFit.Value)
				{
					methodImportAttributes |= MethodImportAttributes.BestFitMappingEnable;
				}
				else
				{
					methodImportAttributes |= MethodImportAttributes.BestFitMappingDisable;
				}
			}
			return methodImportAttributes;
		}

		// Token: 0x0400094A RID: 2378
		private readonly string _moduleName;

		// Token: 0x0400094B RID: 2379
		private readonly string _entryPointName;

		// Token: 0x0400094C RID: 2380
		private readonly MethodImportAttributes _flags;
	}
}
