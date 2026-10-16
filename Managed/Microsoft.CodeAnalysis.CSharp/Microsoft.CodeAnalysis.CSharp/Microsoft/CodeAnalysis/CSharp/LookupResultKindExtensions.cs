using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003A RID: 58
	internal static class LookupResultKindExtensions
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public static CandidateReason ToCandidateReason(this LookupResultKind resultKind)
		{
			switch (resultKind)
			{
			case LookupResultKind.Empty:
				return CandidateReason.None;
			case LookupResultKind.NotATypeOrNamespace:
				return CandidateReason.NotATypeOrNamespace;
			case LookupResultKind.NotAnAttributeType:
				return CandidateReason.NotAnAttributeType;
			case LookupResultKind.WrongArity:
				return CandidateReason.WrongArity;
			case LookupResultKind.NotCreatable:
				return CandidateReason.NotCreatable;
			case LookupResultKind.Inaccessible:
				return CandidateReason.Inaccessible;
			case LookupResultKind.NotReferencable:
				return CandidateReason.NotReferencable;
			case LookupResultKind.NotAValue:
				return CandidateReason.NotAValue;
			case LookupResultKind.NotAVariable:
				return CandidateReason.NotAVariable;
			case LookupResultKind.NotInvocable:
				return CandidateReason.NotInvocable;
			case LookupResultKind.StaticInstanceMismatch:
				return CandidateReason.StaticInstanceMismatch;
			case LookupResultKind.OverloadResolutionFailure:
				return CandidateReason.OverloadResolutionFailure;
			case LookupResultKind.Ambiguous:
				return CandidateReason.Ambiguous;
			case LookupResultKind.MemberGroup:
				return CandidateReason.MemberGroup;
			case LookupResultKind.Viable:
				return CandidateReason.None;
			}
			throw ExceptionUtilities.UnexpectedValue(resultKind);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0003B67D File Offset: 0x0003987D
		public static LookupResultKind WorseResultKind(this LookupResultKind resultKind1, LookupResultKind resultKind2)
		{
			if (resultKind1 == LookupResultKind.Empty)
			{
				return resultKind2;
			}
			if (resultKind2 == LookupResultKind.Empty)
			{
				return resultKind1;
			}
			if (resultKind1 < resultKind2)
			{
				return resultKind1;
			}
			return resultKind2;
		}
	}
}
