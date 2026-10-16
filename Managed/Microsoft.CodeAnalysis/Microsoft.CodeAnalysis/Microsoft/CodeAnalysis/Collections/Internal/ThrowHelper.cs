using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B7 RID: 1207
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ThrowHelper
	{
		// Token: 0x06003045 RID: 12357 RVA: 0x0008A0ED File Offset: 0x000882ED
		[DoesNotReturn]
		internal static void ThrowIndexOutOfRangeException()
		{
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0008A0F4 File Offset: 0x000882F4
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0008A0FB File Offset: 0x000882FB
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_IndexException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0008A104 File Offset: 0x00088304
		[NullableContext(2)]
		[DoesNotReturn]
		internal static void ThrowArgumentException_BadComparer(object comparer)
		{
			throw new ArgumentException(string.Format(SR.Arg_BogusIComparer, comparer));
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0008A116 File Offset: 0x00088316
		[DoesNotReturn]
		internal static void ThrowIndexArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0008A11F File Offset: 0x0008831F
		[DoesNotReturn]
		internal static void ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.length, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0008A129 File Offset: 0x00088329
		[DoesNotReturn]
		internal static void ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0008A132 File Offset: 0x00088332
		[DoesNotReturn]
		internal static void ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0008A13C File Offset: 0x0008833C
		[DoesNotReturn]
		internal static void ThrowWrongKeyTypeArgumentException<[Nullable(2)] T>(T key, Type targetType)
		{
			throw ThrowHelper.GetWrongKeyTypeArgumentException(key, targetType);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0008A14A File Offset: 0x0008834A
		[DoesNotReturn]
		internal static void ThrowWrongValueTypeArgumentException<[Nullable(2)] T>(T value, Type targetType)
		{
			throw ThrowHelper.GetWrongValueTypeArgumentException(value, targetType);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0008A158 File Offset: 0x00088358
		private static ArgumentException GetAddingDuplicateWithKeyArgumentException([Nullable(2)] object key)
		{
			return new ArgumentException(string.Format(SR.Argument_AddingDuplicateWithKey, key));
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0008A16A File Offset: 0x0008836A
		[DoesNotReturn]
		internal static void ThrowAddingDuplicateWithKeyArgumentException<[Nullable(2)] T>(T key)
		{
			throw ThrowHelper.GetAddingDuplicateWithKeyArgumentException(key);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0008A177 File Offset: 0x00088377
		[DoesNotReturn]
		internal static void ThrowKeyNotFoundException<[Nullable(2)] T>(T key)
		{
			throw ThrowHelper.GetKeyNotFoundException(key);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0008A184 File Offset: 0x00088384
		[DoesNotReturn]
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw ThrowHelper.GetArgumentException(resource);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0008A18C File Offset: 0x0008838C
		private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x0008A199 File Offset: 0x00088399
		[DoesNotReturn]
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentNullException(argument);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0008A1A1 File Offset: 0x000883A1
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0008A1AE File Offset: 0x000883AE
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(argument, resource);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0008A1B7 File Offset: 0x000883B7
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException(ExceptionResource resource, Exception e)
		{
			throw new InvalidOperationException(ThrowHelper.GetResourceString(resource), e);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0008A1C5 File Offset: 0x000883C5
		[DoesNotReturn]
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0008A1D2 File Offset: 0x000883D2
		[DoesNotReturn]
		internal static void ThrowArgumentException_Argument_InvalidArrayType()
		{
			throw new ArgumentException(SR.Argument_InvalidArrayType);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0008A1DE File Offset: 0x000883DE
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0008A1EA File Offset: 0x000883EA
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x0008A1F6 File Offset: 0x000883F6
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_ConcurrentOperationsNotSupported()
		{
			throw new InvalidOperationException(SR.InvalidOperation_ConcurrentOperationsNotSupported);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0008A202 File Offset: 0x00088402
		private static ArgumentException GetArgumentException(ExceptionResource resource)
		{
			return new ArgumentException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0008A20F File Offset: 0x0008840F
		private static ArgumentException GetWrongKeyTypeArgumentException([Nullable(2)] object key, Type targetType)
		{
			return new ArgumentException(string.Format(SR.Arg_WrongType, key, targetType), "key");
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x0008A227 File Offset: 0x00088427
		private static ArgumentException GetWrongValueTypeArgumentException([Nullable(2)] object value, Type targetType)
		{
			return new ArgumentException(string.Format(SR.Arg_WrongType, value, targetType), "value");
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0008A23F File Offset: 0x0008843F
		private static KeyNotFoundException GetKeyNotFoundException([Nullable(2)] object key)
		{
			return new KeyNotFoundException(string.Format(SR.Arg_KeyNotFoundWithKey, key));
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x0008A251 File Offset: 0x00088451
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0008A264 File Offset: 0x00088464
		[NullableContext(2)]
		[MethodImpl(256)]
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (default(T) != null && value == null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0008A28C File Offset: 0x0008848C
		private static string GetArgumentName(ExceptionArgument argument)
		{
			switch (argument)
			{
			case ExceptionArgument.dictionary:
				return "dictionary";
			case ExceptionArgument.array:
				return "array";
			case ExceptionArgument.key:
				return "key";
			case ExceptionArgument.value:
				return "value";
			case ExceptionArgument.startIndex:
				return "startIndex";
			case ExceptionArgument.index:
				return "index";
			case ExceptionArgument.capacity:
				return "capacity";
			case ExceptionArgument.collection:
				return "collection";
			case ExceptionArgument.item:
				return "item";
			case ExceptionArgument.converter:
				return "converter";
			case ExceptionArgument.match:
				return "match";
			case ExceptionArgument.count:
				return "count";
			case ExceptionArgument.action:
				return "action";
			case ExceptionArgument.comparison:
				return "comparison";
			case ExceptionArgument.source:
				return "source";
			case ExceptionArgument.length:
				return "length";
			case ExceptionArgument.destinationArray:
				return "destinationArray";
			default:
				return "";
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0008A350 File Offset: 0x00088550
		private static string GetResourceString(ExceptionResource resource)
		{
			switch (resource)
			{
			case ExceptionResource.ArgumentOutOfRange_Index:
				return SR.ArgumentOutOfRange_Index;
			case ExceptionResource.ArgumentOutOfRange_Count:
				return SR.ArgumentOutOfRange_Count;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				return SR.Arg_ArrayPlusOffTooSmall;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				return SR.Arg_RankMultiDimNotSupported;
			case ExceptionResource.Arg_NonZeroLowerBound:
				return SR.Arg_NonZeroLowerBound;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				return SR.ArgumentOutOfRange_ListInsert;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				return SR.ArgumentOutOfRange_NeedNonNegNum;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				return SR.ArgumentOutOfRange_SmallCapacity;
			case ExceptionResource.Argument_InvalidOffLen:
				return SR.Argument_InvalidOffLen;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				return SR.ArgumentOutOfRange_BiggerThanCollection;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				return SR.NotSupported_KeyCollectionSet;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				return SR.NotSupported_ValueCollectionSet;
			case ExceptionResource.InvalidOperation_IComparerFailed:
				return SR.InvalidOperation_IComparerFailed;
			default:
				return "";
			}
		}
	}
}
