using System;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E1 RID: 1249
	internal class SequencePointList
	{
		// Token: 0x0600324E RID: 12878 RVA: 0x0008ED59 File Offset: 0x0008CF59
		private SequencePointList()
		{
			this._points = Array.Empty<SequencePointList.OffsetAndSpan>();
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x0008ED6C File Offset: 0x0008CF6C
		private SequencePointList(SyntaxTree tree, SequencePointList.OffsetAndSpan[] points)
		{
			this._tree = tree;
			this._points = points;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x0008ED84 File Offset: 0x0008CF84
		public static SequencePointList Create(ArrayBuilder<RawSequencePoint> seqPointBuilder, ILBuilder builder)
		{
			if (seqPointBuilder.Count == 0)
			{
				return SequencePointList.s_empty;
			}
			SequencePointList sequencePointList = null;
			SequencePointList sequencePointList2 = null;
			int count = seqPointBuilder.Count;
			int num = 0;
			for (int i = 1; i <= count; i++)
			{
				if (i == count || seqPointBuilder[i].SyntaxTree != seqPointBuilder[i - 1].SyntaxTree)
				{
					SequencePointList sequencePointList3 = new SequencePointList(seqPointBuilder[i - 1].SyntaxTree, SequencePointList.GetSubArray(seqPointBuilder, num, i - num, builder));
					num = i;
					if (sequencePointList2 == null)
					{
						sequencePointList2 = (sequencePointList = sequencePointList3);
					}
					else
					{
						sequencePointList2._next = sequencePointList3;
						sequencePointList2 = sequencePointList3;
					}
				}
			}
			return sequencePointList;
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x0008EE1A File Offset: 0x0008D01A
		public bool IsEmpty
		{
			get
			{
				return this._next == null && this._points.Length == 0;
			}
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0008EE30 File Offset: 0x0008D030
		private static SequencePointList.OffsetAndSpan[] GetSubArray(ArrayBuilder<RawSequencePoint> seqPointBuilder, int start, int length, ILBuilder builder)
		{
			SequencePointList.OffsetAndSpan[] array = new SequencePointList.OffsetAndSpan[length];
			for (int i = 0; i < array.Length; i++)
			{
				RawSequencePoint rawSequencePoint = seqPointBuilder[i + start];
				int iloffsetFromMarker = builder.GetILOffsetFromMarker(rawSequencePoint.ILMarker);
				array[i] = new SequencePointList.OffsetAndSpan(iloffsetFromMarker, rawSequencePoint.Span);
			}
			return array;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0008EE80 File Offset: 0x0008D080
		public void GetSequencePoints(DebugDocumentProvider documentProvider, ArrayBuilder<SequencePoint> builder)
		{
			FileLinePositionSpan? fileLinePositionSpan = this.FindFirstRealSequencePoint();
			if (fileLinePositionSpan == null)
			{
				return;
			}
			string text = fileLinePositionSpan.Value.Path;
			bool flag = fileLinePositionSpan.Value.HasMappedPath;
			DebugSourceDocument debugSourceDocument = documentProvider(text, flag ? this._tree.FilePath : null);
			for (SequencePointList sequencePointList = this; sequencePointList != null; sequencePointList = sequencePointList._next)
			{
				SyntaxTree tree = sequencePointList._tree;
				foreach (SequencePointList.OffsetAndSpan offsetAndSpan in sequencePointList._points)
				{
					TextSpan span = offsetAndSpan.Span;
					bool flag2 = span == RawSequencePoint.HiddenSequencePointSpan;
					FileLinePositionSpan fileLinePositionSpan2 = default(FileLinePositionSpan);
					if (!flag2)
					{
						fileLinePositionSpan2 = tree.GetMappedLineSpanAndVisibility(span, out flag2);
					}
					if (flag2)
					{
						if (text == null)
						{
							text = tree.FilePath;
							debugSourceDocument = documentProvider(text, null);
						}
						if (debugSourceDocument != null)
						{
							builder.Add(new SequencePoint(debugSourceDocument, offsetAndSpan.Offset, 16707566, 0, 16707566, 0));
						}
					}
					else
					{
						if (text != fileLinePositionSpan2.Path || flag != fileLinePositionSpan2.HasMappedPath)
						{
							text = fileLinePositionSpan2.Path;
							flag = fileLinePositionSpan2.HasMappedPath;
							debugSourceDocument = documentProvider(text, flag ? tree.FilePath : null);
						}
						if (debugSourceDocument != null)
						{
							int num = ((fileLinePositionSpan2.StartLinePosition.Line == -1) ? 0 : (fileLinePositionSpan2.StartLinePosition.Line + 1));
							int num2 = ((fileLinePositionSpan2.EndLinePosition.Line == -1) ? 0 : (fileLinePositionSpan2.EndLinePosition.Line + 1));
							int num3 = fileLinePositionSpan2.StartLinePosition.Character + 1;
							int num4 = fileLinePositionSpan2.EndLinePosition.Character + 1;
							if (num3 > 65534)
							{
								num3 = ((num == num2) ? 65533 : 65534);
							}
							if (num4 > 65534)
							{
								num4 = 65534;
							}
							builder.Add(new SequencePoint(debugSourceDocument, offsetAndSpan.Offset, num, (ushort)num3, num2, (ushort)num4));
						}
					}
				}
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x0008F0AC File Offset: 0x0008D2AC
		private FileLinePositionSpan? FindFirstRealSequencePoint()
		{
			for (SequencePointList sequencePointList = this; sequencePointList != null; sequencePointList = sequencePointList._next)
			{
				SequencePointList.OffsetAndSpan[] points = sequencePointList._points;
				for (int i = 0; i < points.Length; i++)
				{
					TextSpan span = points[i].Span;
					bool flag = span == RawSequencePoint.HiddenSequencePointSpan;
					if (!flag)
					{
						FileLinePositionSpan mappedLineSpanAndVisibility = sequencePointList._tree.GetMappedLineSpanAndVisibility(span, out flag);
						if (!flag)
						{
							return new FileLinePositionSpan?(mappedLineSpanAndVisibility);
						}
					}
				}
			}
			return default(FileLinePositionSpan?);
		}

		// Token: 0x04001368 RID: 4968
		private readonly SyntaxTree _tree;

		// Token: 0x04001369 RID: 4969
		private readonly SequencePointList.OffsetAndSpan[] _points;

		// Token: 0x0400136A RID: 4970
		private SequencePointList _next;

		// Token: 0x0400136B RID: 4971
		private static readonly SequencePointList s_empty = new SequencePointList();

		// Token: 0x02000781 RID: 1921
		private struct OffsetAndSpan
		{
			// Token: 0x06003EDF RID: 16095 RVA: 0x000B0DCC File Offset: 0x000AEFCC
			public OffsetAndSpan(int offset, TextSpan span)
			{
				this.Offset = offset;
				this.Span = span;
			}

			// Token: 0x04001D81 RID: 7553
			public readonly int Offset;

			// Token: 0x04001D82 RID: 7554
			public readonly TextSpan Span;
		}
	}
}
