using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002EA RID: 746
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SourceText
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x000626E4 File Offset: 0x000608E4
		[NullableContext(0)]
		protected SourceText(ImmutableArray<byte> checksum = default(ImmutableArray<byte>), SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, [Nullable(2)] SourceTextContainer container = null)
		{
			SourceText.ValidateChecksumAlgorithm(checksumAlgorithm);
			if (!checksum.IsDefault && checksum.Length != CryptographicHashProvider.GetHashSize(checksumAlgorithm))
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidHash, "checksum");
			}
			this._checksumAlgorithm = checksumAlgorithm;
			this._lazyChecksum = checksum;
			this._lazyContainer = container;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0006273A File Offset: 0x0006093A
		[NullableContext(0)]
		internal SourceText(ImmutableArray<byte> checksum, SourceHashAlgorithm checksumAlgorithm, ImmutableArray<byte> embeddedTextBlob)
			: this(checksum, checksumAlgorithm, null)
		{
			if (!checksum.IsDefault && embeddedTextBlob.IsDefault)
			{
				this._precomputedEmbeddedTextBlob = ImmutableArray<byte>.Empty;
				return;
			}
			this._precomputedEmbeddedTextBlob = embeddedTextBlob;
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0006276A File Offset: 0x0006096A
		internal static void ValidateChecksumAlgorithm(SourceHashAlgorithm checksumAlgorithm)
		{
			if (!SourceHashAlgorithms.IsSupportedAlgorithm(checksumAlgorithm))
			{
				throw new ArgumentException(CodeAnalysisResources.UnsupportedHashAlgorithm, "checksumAlgorithm");
			}
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00062784 File Offset: 0x00060984
		public static SourceText From(string text, [Nullable(2)] Encoding encoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return new StringText(text, encoding, default(ImmutableArray<byte>), checksumAlgorithm, default(ImmutableArray<byte>));
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000627BB File Offset: 0x000609BB
		public static SourceText From(TextReader reader, int length, [Nullable(2)] Encoding encoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (length >= 40960)
			{
				return LargeText.Decode(reader, length, encoding, checksumAlgorithm);
			}
			return SourceText.From(reader.ReadToEnd(), encoding, checksumAlgorithm);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000627EA File Offset: 0x000609EA
		[EditorBrowsable(1)]
		public static SourceText From(Stream stream, [Nullable(2)] Encoding encoding, SourceHashAlgorithm checksumAlgorithm, bool throwIfBinaryDetected)
		{
			return SourceText.From(stream, encoding, checksumAlgorithm, throwIfBinaryDetected, false);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000627F8 File Offset: 0x000609F8
		public static SourceText From(Stream stream, [Nullable(2)] Encoding encoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, bool throwIfBinaryDetected = false, bool canBeEmbedded = false)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportReadAndSeek, "stream");
			}
			SourceText.ValidateChecksumAlgorithm(checksumAlgorithm);
			encoding = encoding ?? SourceText.s_utf8EncodingWithNoBOM;
			if (stream.CanSeek && encoding.GetMaxCharCountOrThrowIfHuge(stream) >= 40960)
			{
				return LargeText.Decode(stream, encoding, checksumAlgorithm, throwIfBinaryDetected, canBeEmbedded);
			}
			string text = SourceText.Decode(stream, encoding, out encoding);
			if (throwIfBinaryDetected && SourceText.IsBinary(text))
			{
				throw new InvalidDataException();
			}
			ImmutableArray<byte> immutableArray = SourceText.CalculateChecksum(stream, checksumAlgorithm);
			ImmutableArray<byte> immutableArray2 = (canBeEmbedded ? EmbeddedText.CreateBlob(stream) : default(ImmutableArray<byte>));
			return new StringText(text, encoding, immutableArray, checksumAlgorithm, immutableArray2);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000628A2 File Offset: 0x00060AA2
		[EditorBrowsable(1)]
		public static SourceText From(byte[] buffer, int length, [Nullable(2)] Encoding encoding, SourceHashAlgorithm checksumAlgorithm, bool throwIfBinaryDetected)
		{
			return SourceText.From(buffer, length, encoding, checksumAlgorithm, throwIfBinaryDetected, false);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000628B0 File Offset: 0x00060AB0
		public static SourceText From(byte[] buffer, int length, [Nullable(2)] Encoding encoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, bool throwIfBinaryDetected = false, bool canBeEmbedded = false)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length < 0 || length > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			SourceText.ValidateChecksumAlgorithm(checksumAlgorithm);
			string text = SourceText.Decode(buffer, length, encoding ?? SourceText.s_utf8EncodingWithNoBOM, out encoding);
			if (throwIfBinaryDetected && SourceText.IsBinary(text))
			{
				throw new InvalidDataException();
			}
			ImmutableArray<byte> immutableArray = SourceText.CalculateChecksum(buffer, 0, length, checksumAlgorithm);
			ImmutableArray<byte> immutableArray2 = (canBeEmbedded ? EmbeddedText.CreateBlob(new ArraySegment<byte>(buffer, 0, length)) : default(ImmutableArray<byte>));
			return new StringText(text, encoding, immutableArray, checksumAlgorithm, immutableArray2);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00062940 File Offset: 0x00060B40
		private static string Decode(Stream stream, Encoding encoding, out Encoding actualEncoding)
		{
			int num = 4096;
			if (stream.CanSeek)
			{
				stream.Seek(0L, 0);
				int num2 = (int)stream.Length;
				if (num2 == 0)
				{
					actualEncoding = encoding;
					return string.Empty;
				}
				num = Math.Min(4096, num2);
			}
			string text2;
			using (StreamReader streamReader = new StreamReader(stream, encoding, true, num, true))
			{
				string text = streamReader.ReadToEnd();
				actualEncoding = streamReader.CurrentEncoding;
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000629BC File Offset: 0x00060BBC
		private static string Decode(byte[] buffer, int length, Encoding encoding, out Encoding actualEncoding)
		{
			int num;
			actualEncoding = SourceText.TryReadByteOrderMark(buffer, length, out num) ?? encoding;
			return actualEncoding.GetString(buffer, num, length - num);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000629E8 File Offset: 0x00060BE8
		internal static bool IsBinary(string text)
		{
			int i = 1;
			while (i < text.Length)
			{
				if (text.get_Chars(i) == '\0')
				{
					if (text.get_Chars(i - 1) == '\0')
					{
						return true;
					}
					i++;
				}
				else
				{
					i += 2;
				}
			}
			return false;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00062A23 File Offset: 0x00060C23
		public SourceHashAlgorithm ChecksumAlgorithm
		{
			get
			{
				return this._checksumAlgorithm;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001FED RID: 8173
		[Nullable(2)]
		public abstract Encoding Encoding
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001FEE RID: 8174
		public abstract int Length { get; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00062A2B File Offset: 0x00060C2B
		internal virtual int StorageSize
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00062A33 File Offset: 0x00060C33
		[Nullable(new byte[] { 0, 1 })]
		internal virtual ImmutableArray<SourceText> Segments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SourceText>.Empty;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x00062A3A File Offset: 0x00060C3A
		internal virtual SourceText StorageKey
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00062A3D File Offset: 0x00060C3D
		public bool CanBeEmbedded
		{
			get
			{
				if (this._precomputedEmbeddedTextBlob.IsDefault)
				{
					return this.Encoding != null;
				}
				return !this._precomputedEmbeddedTextBlob.IsEmpty;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00062A64 File Offset: 0x00060C64
		[Nullable(0)]
		internal ImmutableArray<byte> PrecomputedEmbeddedTextBlob
		{
			[NullableContext(0)]
			get
			{
				return this._precomputedEmbeddedTextBlob;
			}
		}

		// Token: 0x1700082C RID: 2092
		public abstract char this[int position] { get; }

		// Token: 0x06001FF5 RID: 8181
		public abstract void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00062A6C File Offset: 0x00060C6C
		public virtual SourceTextContainer Container
		{
			get
			{
				if (this._lazyContainer == null)
				{
					Interlocked.CompareExchange<SourceTextContainer>(ref this._lazyContainer, new SourceText.StaticContainer(this), null);
				}
				return this._lazyContainer;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00062A8F File Offset: 0x00060C8F
		internal void CheckSubSpan(TextSpan span)
		{
			if (span.End > this.Length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00062AAC File Offset: 0x00060CAC
		public virtual SourceText GetSubText(TextSpan span)
		{
			this.CheckSubSpan(span);
			int length = span.Length;
			if (length == 0)
			{
				return SourceText.From(string.Empty, this.Encoding, this.ChecksumAlgorithm);
			}
			if (length == this.Length && span.Start == 0)
			{
				return this;
			}
			return new SubText(this, span);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00062AFD File Offset: 0x00060CFD
		public SourceText GetSubText(int start)
		{
			if (start < 0 || start > this.Length)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (start == 0)
			{
				return this;
			}
			return this.GetSubText(new TextSpan(start, this.Length - start));
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00062B30 File Offset: 0x00060D30
		public void Write(TextWriter textWriter, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.Write(textWriter, new TextSpan(0, this.Length), cancellationToken);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00062B48 File Offset: 0x00060D48
		public virtual void Write(TextWriter writer, TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSubSpan(span);
			char[] array = SourceText.s_charArrayPool.Allocate();
			try
			{
				int i = span.Start;
				int end = span.End;
				while (i < end)
				{
					cancellationToken.ThrowIfCancellationRequested();
					int num = Math.Min(array.Length, end - i);
					this.CopyTo(i, array, 0, num);
					writer.Write(array, 0, num);
					i += num;
				}
			}
			finally
			{
				SourceText.s_charArrayPool.Free(array);
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00062BC8 File Offset: 0x00060DC8
		[NullableContext(0)]
		public ImmutableArray<byte> GetChecksum()
		{
			if (this._lazyChecksum.IsDefault)
			{
				using (SourceTextStream sourceTextStream = new SourceTextStream(this, 2048, true))
				{
					ImmutableInterlocked.InterlockedInitialize<byte>(ref this._lazyChecksum, SourceText.CalculateChecksum(sourceTextStream, this._checksumAlgorithm));
				}
			}
			return this._lazyChecksum;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00062C2C File Offset: 0x00060E2C
		[NullableContext(0)]
		internal static ImmutableArray<byte> CalculateChecksum([Nullable(1)] byte[] buffer, int offset, int count, SourceHashAlgorithm algorithmId)
		{
			ImmutableArray<byte> immutableArray;
			using (HashAlgorithm hashAlgorithm = CryptographicHashProvider.TryGetAlgorithm(algorithmId))
			{
				immutableArray = ImmutableArray.Create<byte>(hashAlgorithm.ComputeHash(buffer, offset, count));
			}
			return immutableArray;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00062C6C File Offset: 0x00060E6C
		[NullableContext(0)]
		internal static ImmutableArray<byte> CalculateChecksum([Nullable(1)] Stream stream, SourceHashAlgorithm algorithmId)
		{
			ImmutableArray<byte> immutableArray;
			using (HashAlgorithm hashAlgorithm = CryptographicHashProvider.TryGetAlgorithm(algorithmId))
			{
				if (stream.CanSeek)
				{
					stream.Seek(0L, 0);
				}
				immutableArray = ImmutableArray.Create<byte>(hashAlgorithm.ComputeHash(stream));
			}
			return immutableArray;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00062CBC File Offset: 0x00060EBC
		public override string ToString()
		{
			return this.ToString(new TextSpan(0, this.Length));
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00062CD0 File Offset: 0x00060ED0
		public virtual string ToString(TextSpan span)
		{
			this.CheckSubSpan(span);
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = new char[Math.Min(span.Length, 1024)];
			int num = Math.Max(Math.Min(span.Start, this.Length), 0);
			int num2 = Math.Min(span.End, this.Length) - num;
			while (num < this.Length && num2 > 0)
			{
				int num3 = Math.Min(array.Length, num2);
				this.CopyTo(num, array, 0, num3);
				stringBuilder.Append(array, 0, num3);
				num2 -= num3;
				num += num3;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00062D70 File Offset: 0x00060F70
		public virtual SourceText WithChanges(IEnumerable<TextChange> changes)
		{
			if (changes == null)
			{
				throw new ArgumentNullException("changes");
			}
			if (!Enumerable.Any<TextChange>(changes))
			{
				return this;
			}
			ArrayBuilder<SourceText> instance = ArrayBuilder<SourceText>.GetInstance();
			ArrayBuilder<TextChangeRange> instance2 = ArrayBuilder<TextChangeRange>.GetInstance();
			SourceText sourceText2;
			try
			{
				int num = 0;
				foreach (TextChange textChange in changes)
				{
					if (textChange.Span.End > this.Length)
					{
						throw new ArgumentException(CodeAnalysisResources.ChangesMustBeWithinBoundsOfSourceText, "changes");
					}
					if (textChange.Span.Start < num)
					{
						if (textChange.Span.End <= instance2.Last().Span.Start)
						{
							changes = Enumerable.ToList<TextChange>(Enumerable.OrderBy<TextChange, TextSpan>(Enumerable.Where<TextChange>(changes, delegate(TextChange c)
							{
								TextChange textChange2 = c;
								if (textChange2.Span.IsEmpty)
								{
									textChange2 = c;
									string newText2 = textChange2.NewText;
									return newText2 != null && newText2.Length > 0;
								}
								return true;
							}), delegate(TextChange c)
							{
								TextChange textChange3 = c;
								return textChange3.Span;
							}));
							return this.WithChanges(changes);
						}
						throw new ArgumentException(CodeAnalysisResources.ChangesMustNotOverlap, "changes");
					}
					else
					{
						string newText = textChange.NewText;
						int num2 = ((newText != null) ? newText.Length : 0);
						if (textChange.Span.Length != 0 || num2 != 0)
						{
							if (textChange.Span.Start > num)
							{
								SourceText subText = this.GetSubText(new TextSpan(num, textChange.Span.Start - num));
								CompositeText.AddSegments(instance, subText);
							}
							if (num2 > 0)
							{
								SourceText sourceText = SourceText.From(textChange.NewText, this.Encoding, this.ChecksumAlgorithm);
								CompositeText.AddSegments(instance, sourceText);
							}
							num = textChange.Span.End;
							instance2.Add(new TextChangeRange(textChange.Span, num2));
						}
					}
				}
				if (num == 0 && instance.Count == 0)
				{
					sourceText2 = this;
				}
				else
				{
					if (num < this.Length)
					{
						SourceText subText2 = this.GetSubText(new TextSpan(num, this.Length - num));
						CompositeText.AddSegments(instance, subText2);
					}
					SourceText sourceText3 = CompositeText.ToSourceText(instance, this, true);
					if (sourceText3 != this)
					{
						sourceText2 = new ChangedText(this, sourceText3, instance2.ToImmutable());
					}
					else
					{
						sourceText2 = this;
					}
				}
			}
			finally
			{
				instance.Free();
				instance2.Free();
			}
			return sourceText2;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00062FFC File Offset: 0x000611FC
		public SourceText WithChanges(params TextChange[] changes)
		{
			return this.WithChanges(changes);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00063005 File Offset: 0x00061205
		public SourceText Replace(TextSpan span, string newText)
		{
			return this.WithChanges(new TextChange[]
			{
				new TextChange(span, newText)
			});
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00063021 File Offset: 0x00061221
		public SourceText Replace(int start, int length, string newText)
		{
			return this.Replace(new TextSpan(start, length), newText);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00063031 File Offset: 0x00061231
		public virtual IReadOnlyList<TextChangeRange> GetChangeRanges(SourceText oldText)
		{
			if (oldText == null)
			{
				throw new ArgumentNullException("oldText");
			}
			if (oldText == this)
			{
				return TextChangeRange.NoChanges;
			}
			return ImmutableArray.Create<TextChangeRange>(new TextChangeRange(new TextSpan(0, oldText.Length), this.Length));
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0006306C File Offset: 0x0006126C
		public virtual IReadOnlyList<TextChange> GetTextChanges(SourceText oldText)
		{
			int num = 0;
			List<TextChangeRange> list = Enumerable.ToList<TextChangeRange>(this.GetChangeRanges(oldText));
			List<TextChange> list2 = new List<TextChange>(list.Count);
			foreach (TextChangeRange textChangeRange in list)
			{
				int num2 = textChangeRange.Span.Start + num;
				string text;
				if (textChangeRange.NewLength > 0)
				{
					TextSpan textSpan = new TextSpan(num2, textChangeRange.NewLength);
					text = this.ToString(textSpan);
				}
				else
				{
					text = string.Empty;
				}
				list2.Add(new TextChange(textChangeRange.Span, text));
				num += textChangeRange.NewLength - textChangeRange.Span.Length;
			}
			return list2.ToImmutableArrayOrEmpty<TextChange>();
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0006314C File Offset: 0x0006134C
		public TextLineCollection Lines
		{
			get
			{
				TextLineCollection textLineCollection = this._lazyLineInfo;
				TextLineCollection textLineCollection2;
				if ((textLineCollection2 = textLineCollection) == null)
				{
					textLineCollection2 = Interlocked.CompareExchange<TextLineCollection>(ref this._lazyLineInfo, textLineCollection = this.GetLinesCore(), null) ?? textLineCollection;
				}
				return textLineCollection2;
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0006317E File Offset: 0x0006137E
		[NullableContext(2)]
		internal bool TryGetLines([NotNullWhen(true)] out TextLineCollection lines)
		{
			lines = this._lazyLineInfo;
			return lines != null;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0006318D File Offset: 0x0006138D
		protected virtual TextLineCollection GetLinesCore()
		{
			return new SourceText.LineInfo(this, this.ParseLineStarts());
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0006319C File Offset: 0x0006139C
		private void EnumerateChars(Action<int, char[], int> action)
		{
			int i = 0;
			char[] array = SourceText.s_charArrayPool.Allocate();
			int length = this.Length;
			while (i < length)
			{
				int num = Math.Min(length - i, array.Length);
				this.CopyTo(i, array, 0, num);
				action.Invoke(i, array, num);
				i += num;
			}
			action.Invoke(i, array, 0);
			SourceText.s_charArrayPool.Free(array);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000631FC File Offset: 0x000613FC
		private int[] ParseLineStarts()
		{
			if (this.Length == 0)
			{
				return new int[1];
			}
			ArrayBuilder<int> lineStarts = ArrayBuilder<int>.GetInstance();
			lineStarts.Add(0);
			bool lastWasCR = false;
			this.EnumerateChars(delegate(int position, char[] buffer, int length)
			{
				int i = 0;
				if (lastWasCR)
				{
					if (length > 0 && buffer[0] == '\n')
					{
						i++;
					}
					lineStarts.Add(position + i);
					lastWasCR = false;
				}
				while (i < length)
				{
					char c = buffer[i];
					i++;
					if (c - '\u000e' > 'q')
					{
						if (c == '\r')
						{
							if (i < length && buffer[i] == '\n')
							{
								i++;
							}
							else if (i >= length)
							{
								lastWasCR = true;
								continue;
							}
						}
						else if (!TextUtilities.IsAnyLineBreakCharacter(c))
						{
							continue;
						}
						lineStarts.Add(position + i);
					}
				}
			});
			return lineStarts.ToArrayAndFree();
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0006325C File Offset: 0x0006145C
		public bool ContentEquals(SourceText other)
		{
			if (this == other)
			{
				return true;
			}
			ImmutableArray<byte> lazyChecksum = this._lazyChecksum;
			ImmutableArray<byte> lazyChecksum2 = other._lazyChecksum;
			if (!lazyChecksum.IsDefault && !lazyChecksum2.IsDefault && this.Encoding == other.Encoding && this.ChecksumAlgorithm == other.ChecksumAlgorithm)
			{
				return lazyChecksum.SequenceEqual<byte, byte>(lazyChecksum2, null);
			}
			return this.ContentEqualsImpl(other);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000632BC File Offset: 0x000614BC
		protected virtual bool ContentEqualsImpl(SourceText other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Length != other.Length)
			{
				return false;
			}
			char[] array = SourceText.s_charArrayPool.Allocate();
			char[] array2 = SourceText.s_charArrayPool.Allocate();
			bool flag;
			try
			{
				int num;
				for (int i = 0; i < this.Length; i += num)
				{
					num = Math.Min(this.Length - i, array.Length);
					this.CopyTo(i, array, 0, num);
					other.CopyTo(i, array2, 0, num);
					for (int j = 0; j < num; j++)
					{
						if (array[j] != array2[j])
						{
							return false;
						}
					}
				}
				flag = true;
			}
			finally
			{
				SourceText.s_charArrayPool.Free(array2);
				SourceText.s_charArrayPool.Free(array);
			}
			return flag;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00063380 File Offset: 0x00061580
		[return: Nullable(2)]
		internal static Encoding TryReadByteOrderMark(byte[] source, int length, out int preambleLength)
		{
			if (length >= 2)
			{
				byte b = source[0];
				if (b != 239)
				{
					if (b != 254)
					{
						if (b == 255)
						{
							if (source[1] == 254)
							{
								preambleLength = 2;
								return Encoding.Unicode;
							}
						}
					}
					else if (source[1] == 255)
					{
						preambleLength = 2;
						return Encoding.BigEndianUnicode;
					}
				}
				else if (source[1] == 187 && length >= 3 && source[2] == 191)
				{
					preambleLength = 3;
					return Encoding.UTF8;
				}
			}
			preambleLength = 0;
			return null;
		}

		// Token: 0x04000E03 RID: 3587
		private const int CharBufferSize = 32768;

		// Token: 0x04000E04 RID: 3588
		private const int CharBufferCount = 5;

		// Token: 0x04000E05 RID: 3589
		internal const int LargeObjectHeapLimitInChars = 40960;

		// Token: 0x04000E06 RID: 3590
		private static readonly ObjectPool<char[]> s_charArrayPool = new ObjectPool<char[]>(() => new char[32768], 5);

		// Token: 0x04000E07 RID: 3591
		private readonly SourceHashAlgorithm _checksumAlgorithm;

		// Token: 0x04000E08 RID: 3592
		[Nullable(2)]
		private SourceTextContainer _lazyContainer;

		// Token: 0x04000E09 RID: 3593
		[Nullable(2)]
		private TextLineCollection _lazyLineInfo;

		// Token: 0x04000E0A RID: 3594
		[Nullable(0)]
		private ImmutableArray<byte> _lazyChecksum;

		// Token: 0x04000E0B RID: 3595
		[Nullable(0)]
		private ImmutableArray<byte> _precomputedEmbeddedTextBlob;

		// Token: 0x04000E0C RID: 3596
		private static readonly Encoding s_utf8EncodingWithNoBOM = new UTF8Encoding(false, false);

		// Token: 0x0200067C RID: 1660
		[Nullable(0)]
		internal sealed class LineInfo : TextLineCollection
		{
			// Token: 0x06003934 RID: 14644 RVA: 0x0009EF17 File Offset: 0x0009D117
			public LineInfo(SourceText text, int[] lineStarts)
			{
				this._text = text;
				this._lineStarts = lineStarts;
			}

			// Token: 0x17000FD8 RID: 4056
			// (get) Token: 0x06003935 RID: 14645 RVA: 0x0009EF2D File Offset: 0x0009D12D
			public override int Count
			{
				get
				{
					return this._lineStarts.Length;
				}
			}

			// Token: 0x17000FD9 RID: 4057
			public override TextLine this[int index]
			{
				get
				{
					if (index < 0 || index >= this._lineStarts.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					int num = this._lineStarts[index];
					if (index == this._lineStarts.Length - 1)
					{
						return TextLine.FromSpan(this._text, TextSpan.FromBounds(num, this._text.Length));
					}
					int num2 = this._lineStarts[index + 1];
					return TextLine.FromSpan(this._text, TextSpan.FromBounds(num, num2));
				}
			}

			// Token: 0x06003937 RID: 14647 RVA: 0x0009EFB0 File Offset: 0x0009D1B0
			public override int IndexOf(int position)
			{
				if (position < 0 || position > this._text.Length)
				{
					throw new ArgumentOutOfRangeException("position");
				}
				int lastLineNumber = this._lastLineNumber;
				int num2;
				if (position >= this._lineStarts[lastLineNumber])
				{
					int num = Math.Min(this._lineStarts.Length, lastLineNumber + 4);
					for (int i = lastLineNumber; i < num; i++)
					{
						if (position < this._lineStarts[i])
						{
							num2 = i - 1;
							this._lastLineNumber = num2;
							return num2;
						}
					}
				}
				num2 = this._lineStarts.BinarySearch(position);
				if (num2 < 0)
				{
					num2 = ~num2 - 1;
				}
				this._lastLineNumber = num2;
				return num2;
			}

			// Token: 0x06003938 RID: 14648 RVA: 0x0009F03F File Offset: 0x0009D23F
			public override TextLine GetLineFromPosition(int position)
			{
				return this[this.IndexOf(position)];
			}

			// Token: 0x040018E4 RID: 6372
			private readonly SourceText _text;

			// Token: 0x040018E5 RID: 6373
			private readonly int[] _lineStarts;

			// Token: 0x040018E6 RID: 6374
			private int _lastLineNumber;
		}

		// Token: 0x0200067D RID: 1661
		[Nullable(0)]
		private class StaticContainer : SourceTextContainer
		{
			// Token: 0x06003939 RID: 14649 RVA: 0x0009F04E File Offset: 0x0009D24E
			public StaticContainer(SourceText text)
			{
				this._text = text;
			}

			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x0600393A RID: 14650 RVA: 0x0009F05D File Offset: 0x0009D25D
			public override SourceText CurrentText
			{
				get
				{
					return this._text;
				}
			}

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x0600393B RID: 14651 RVA: 0x0009F065 File Offset: 0x0009D265
			// (remove) Token: 0x0600393C RID: 14652 RVA: 0x0009F067 File Offset: 0x0009D267
			public override event EventHandler<TextChangeEventArgs> TextChanged
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x040018E7 RID: 6375
			private readonly SourceText _text;
		}
	}
}
