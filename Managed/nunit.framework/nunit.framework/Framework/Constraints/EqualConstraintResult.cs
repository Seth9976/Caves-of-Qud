using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013F RID: 319
	public class EqualConstraintResult : ConstraintResult
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00016EA0 File Offset: 0x000150A0
		public EqualConstraintResult(EqualConstraint constraint, object actual, bool hasSucceeded)
			: base(constraint, actual, hasSucceeded)
		{
			this.expectedValue = constraint.Arguments[0];
			this.tolerance = constraint.Tolerance;
			this.caseInsensitive = constraint.CaseInsensitive;
			this.clipStrings = constraint.ClipStrings;
			this.failurePoints = constraint.FailurePoints;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00016EF4 File Offset: 0x000150F4
		public override void WriteMessageTo(MessageWriter writer)
		{
			this.DisplayDifferences(writer, this.expectedValue, base.ActualValue, 0);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00016F0C File Offset: 0x0001510C
		private void DisplayDifferences(MessageWriter writer, object expected, object actual, int depth)
		{
			if (expected is string && actual is string)
			{
				this.DisplayStringDifferences(writer, (string)expected, (string)actual);
				return;
			}
			if (expected is ICollection && actual is ICollection)
			{
				this.DisplayCollectionDifferences(writer, (ICollection)expected, (ICollection)actual, depth);
				return;
			}
			if (expected is IEnumerable && actual is IEnumerable)
			{
				this.DisplayEnumerableDifferences(writer, (IEnumerable)expected, (IEnumerable)actual, depth);
				return;
			}
			if (expected is Stream && actual is Stream)
			{
				this.DisplayStreamDifferences(writer, (Stream)expected, (Stream)actual, depth);
				return;
			}
			if (this.tolerance != null)
			{
				writer.DisplayDifferences(expected, actual, this.tolerance);
				return;
			}
			writer.DisplayDifferences(expected, actual);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00016FD0 File Offset: 0x000151D0
		private void DisplayStringDifferences(MessageWriter writer, string expected, string actual)
		{
			int num = MsgUtils.FindMismatchPosition(expected, actual, 0, this.caseInsensitive);
			if (expected.Length == actual.Length)
			{
				writer.WriteMessageLine(EqualConstraintResult.StringsDiffer_1, new object[] { expected.Length, num });
			}
			else
			{
				writer.WriteMessageLine(EqualConstraintResult.StringsDiffer_2, new object[] { expected.Length, actual.Length, num });
			}
			writer.DisplayStringDifferences(expected, actual, num, this.caseInsensitive, this.clipStrings);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00017070 File Offset: 0x00015270
		private void DisplayStreamDifferences(MessageWriter writer, Stream expected, Stream actual, int depth)
		{
			if (expected.Length == actual.Length)
			{
				long position = this.failurePoints[depth].Position;
				writer.WriteMessageLine(EqualConstraintResult.StreamsDiffer_1, new object[] { expected.Length, position });
				return;
			}
			writer.WriteMessageLine(EqualConstraintResult.StreamsDiffer_2, new object[] { expected.Length, actual.Length });
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000170F4 File Offset: 0x000152F4
		private void DisplayCollectionDifferences(MessageWriter writer, ICollection expected, ICollection actual, int depth)
		{
			this.DisplayTypesAndSizes(writer, expected, actual, depth);
			if (this.failurePoints.Count > depth)
			{
				NUnitEqualityComparer.FailurePoint failurePoint = this.failurePoints[depth];
				this.DisplayFailurePoint(writer, expected, actual, failurePoint, depth);
				if (failurePoint.ExpectedHasData && failurePoint.ActualHasData)
				{
					this.DisplayDifferences(writer, failurePoint.ExpectedValue, failurePoint.ActualValue, ++depth);
					return;
				}
				if (failurePoint.ActualHasData)
				{
					writer.Write("  Extra:    ");
					writer.WriteCollectionElements(actual, failurePoint.Position, 3);
					return;
				}
				writer.Write("  Missing:  ");
				writer.WriteCollectionElements(expected, failurePoint.Position, 3);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000171A0 File Offset: 0x000153A0
		private void DisplayTypesAndSizes(MessageWriter writer, IEnumerable expected, IEnumerable actual, int indent)
		{
			string text = MsgUtils.GetTypeRepresentation(expected);
			if (expected is ICollection && !(expected is Array))
			{
				text += string.Format(" with {0} elements", ((ICollection)expected).Count);
			}
			string text2 = MsgUtils.GetTypeRepresentation(actual);
			if (actual is ICollection && !(actual is Array))
			{
				text2 += string.Format(" with {0} elements", ((ICollection)actual).Count);
			}
			if (text == text2)
			{
				writer.WriteMessageLine(indent, EqualConstraintResult.CollectionType_1, new object[] { text });
				return;
			}
			writer.WriteMessageLine(indent, EqualConstraintResult.CollectionType_2, new object[] { text, text2 });
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001725C File Offset: 0x0001545C
		private void DisplayFailurePoint(MessageWriter writer, IEnumerable expected, IEnumerable actual, NUnitEqualityComparer.FailurePoint failurePoint, int indent)
		{
			Array array = expected as Array;
			Array array2 = actual as Array;
			int num = ((array != null) ? array.Rank : 1);
			int num2 = ((array2 != null) ? array2.Rank : 1);
			bool flag = num == num2;
			if (array != null && array2 != null)
			{
				int num3 = 1;
				while (num3 < num && flag)
				{
					if (array.GetLength(num3) != array2.GetLength(num3))
					{
						flag = false;
					}
					num3++;
				}
			}
			int[] arrayIndicesFromCollectionIndex = MsgUtils.GetArrayIndicesFromCollectionIndex(expected, failurePoint.Position);
			if (flag)
			{
				writer.WriteMessageLine(indent, EqualConstraintResult.ValuesDiffer_1, new object[] { MsgUtils.GetArrayIndicesAsString(arrayIndicesFromCollectionIndex) });
				return;
			}
			int[] arrayIndicesFromCollectionIndex2 = MsgUtils.GetArrayIndicesFromCollectionIndex(actual, failurePoint.Position);
			writer.WriteMessageLine(indent, EqualConstraintResult.ValuesDiffer_2, new object[]
			{
				MsgUtils.GetArrayIndicesAsString(arrayIndicesFromCollectionIndex),
				MsgUtils.GetArrayIndicesAsString(arrayIndicesFromCollectionIndex2)
			});
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00017330 File Offset: 0x00015530
		private static object GetValueFromCollection(ICollection collection, int index)
		{
			Array array = collection as Array;
			if (array != null && array.Rank > 1)
			{
				return array.GetValue(MsgUtils.GetArrayIndicesFromCollectionIndex(array, (long)index));
			}
			if (collection is IList)
			{
				return ((IList)collection)[index];
			}
			foreach (object obj in collection)
			{
				if (--index < 0)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000173C4 File Offset: 0x000155C4
		private void DisplayEnumerableDifferences(MessageWriter writer, IEnumerable expected, IEnumerable actual, int depth)
		{
			this.DisplayTypesAndSizes(writer, expected, actual, depth);
			if (this.failurePoints.Count > depth)
			{
				NUnitEqualityComparer.FailurePoint failurePoint = this.failurePoints[depth];
				this.DisplayFailurePoint(writer, expected, actual, failurePoint, depth);
				if (failurePoint.ExpectedHasData && failurePoint.ActualHasData)
				{
					this.DisplayDifferences(writer, failurePoint.ExpectedValue, failurePoint.ActualValue, ++depth);
				}
			}
		}

		// Token: 0x040001EF RID: 495
		private object expectedValue;

		// Token: 0x040001F0 RID: 496
		private Tolerance tolerance;

		// Token: 0x040001F1 RID: 497
		private bool caseInsensitive;

		// Token: 0x040001F2 RID: 498
		private bool clipStrings;

		// Token: 0x040001F3 RID: 499
		private IList<NUnitEqualityComparer.FailurePoint> failurePoints;

		// Token: 0x040001F4 RID: 500
		private static readonly string StringsDiffer_1 = "String lengths are both {0}. Strings differ at index {1}.";

		// Token: 0x040001F5 RID: 501
		private static readonly string StringsDiffer_2 = "Expected string length {0} but was {1}. Strings differ at index {2}.";

		// Token: 0x040001F6 RID: 502
		private static readonly string StreamsDiffer_1 = "Stream lengths are both {0}. Streams differ at offset {1}.";

		// Token: 0x040001F7 RID: 503
		private static readonly string StreamsDiffer_2 = "Expected Stream length {0} but was {1}.";

		// Token: 0x040001F8 RID: 504
		private static readonly string CollectionType_1 = "Expected and actual are both {0}";

		// Token: 0x040001F9 RID: 505
		private static readonly string CollectionType_2 = "Expected is {0}, actual is {1}";

		// Token: 0x040001FA RID: 506
		private static readonly string ValuesDiffer_1 = "Values differ at index {0}";

		// Token: 0x040001FB RID: 507
		private static readonly string ValuesDiffer_2 = "Values differ at expected index {0}, actual index {1}";
	}
}
