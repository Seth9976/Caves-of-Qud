using System;
using System.Collections.Immutable;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000227 RID: 551
	internal sealed class CustomAttributesBag<T> where T : AttributeData
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x000443FD File Offset: 0x000425FD
		private CustomAttributesBag(CustomAttributesBag<T>.CustomAttributeBagCompletionPart part, ImmutableArray<T> customAttributes)
		{
			this._customAttributes = customAttributes;
			this.NotePartComplete(part);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00044414 File Offset: 0x00042614
		public CustomAttributesBag()
			: this(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.None, default(ImmutableArray<T>))
		{
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00044434 File Offset: 0x00042634
		public static CustomAttributesBag<T> WithEmptyData()
		{
			return new CustomAttributesBag<T>(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.EarlyDecodedWellKnownAttributeData | CustomAttributesBag<T>.CustomAttributeBagCompletionPart.DecodedWellKnownAttributeData, default(ImmutableArray<T>));
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00044450 File Offset: 0x00042650
		public bool IsEmpty
		{
			get
			{
				return this.IsSealed && this._customAttributes.IsEmpty && this._decodedWellKnownAttributeData == null && this._earlyDecodedWellKnownAttributeData == null;
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0004447A File Offset: 0x0004267A
		public bool SetEarlyDecodedWellKnownAttributeData(EarlyWellKnownAttributeData data)
		{
			bool flag = Interlocked.CompareExchange<EarlyWellKnownAttributeData>(ref this._earlyDecodedWellKnownAttributeData, data, null) == null;
			this.NotePartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.EarlyDecodedWellKnownAttributeData);
			return flag;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00044493 File Offset: 0x00042693
		public bool SetDecodedWellKnownAttributeData(WellKnownAttributeData data)
		{
			bool flag = Interlocked.CompareExchange<WellKnownAttributeData>(ref this._decodedWellKnownAttributeData, data, null) == null;
			this.NotePartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.DecodedWellKnownAttributeData);
			return flag;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000444AC File Offset: 0x000426AC
		public bool SetAttributes(ImmutableArray<T> newCustomAttributes)
		{
			bool flag = ImmutableInterlocked.InterlockedCompareExchange<T>(ref this._customAttributes, newCustomAttributes, default(ImmutableArray<T>)) == default(ImmutableArray<T>);
			this.NotePartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.Attributes);
			return flag;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000444E3 File Offset: 0x000426E3
		public ImmutableArray<T> Attributes
		{
			get
			{
				return this._customAttributes;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000444EB File Offset: 0x000426EB
		public WellKnownAttributeData DecodedWellKnownAttributeData
		{
			get
			{
				return this._decodedWellKnownAttributeData;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000444F3 File Offset: 0x000426F3
		public EarlyWellKnownAttributeData EarlyDecodedWellKnownAttributeData
		{
			get
			{
				return this._earlyDecodedWellKnownAttributeData;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000444FB File Offset: 0x000426FB
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x00044504 File Offset: 0x00042704
		private CustomAttributesBag<T>.CustomAttributeBagCompletionPart State
		{
			get
			{
				return (CustomAttributesBag<T>.CustomAttributeBagCompletionPart)this._state;
			}
			set
			{
				this._state = (int)value;
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004450D File Offset: 0x0004270D
		private void NotePartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart part)
		{
			ThreadSafeFlagOperations.Set(ref this._state, (int)(this.State | part));
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00044523 File Offset: 0x00042723
		internal bool IsPartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart part)
		{
			return (this.State & part) == part;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00044530 File Offset: 0x00042730
		internal bool IsSealed
		{
			get
			{
				return this.IsPartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.All);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00044539 File Offset: 0x00042739
		internal bool IsEarlyDecodedWellKnownAttributeDataComputed
		{
			get
			{
				return this.IsPartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.EarlyDecodedWellKnownAttributeData);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00044542 File Offset: 0x00042742
		internal bool IsDecodedWellKnownAttributeDataComputed
		{
			get
			{
				return this.IsPartComplete(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.DecodedWellKnownAttributeData);
			}
		}

		// Token: 0x040008E9 RID: 2281
		private ImmutableArray<T> _customAttributes;

		// Token: 0x040008EA RID: 2282
		private WellKnownAttributeData _decodedWellKnownAttributeData;

		// Token: 0x040008EB RID: 2283
		private EarlyWellKnownAttributeData _earlyDecodedWellKnownAttributeData;

		// Token: 0x040008EC RID: 2284
		private int _state;

		// Token: 0x040008ED RID: 2285
		public static readonly CustomAttributesBag<T> Empty = new CustomAttributesBag<T>(CustomAttributesBag<T>.CustomAttributeBagCompletionPart.All, ImmutableArray<T>.Empty);

		// Token: 0x020005EE RID: 1518
		[Flags]
		internal enum CustomAttributeBagCompletionPart : byte
		{
			// Token: 0x04001726 RID: 5926
			None = 0,
			// Token: 0x04001727 RID: 5927
			EarlyDecodedWellKnownAttributeData = 1,
			// Token: 0x04001728 RID: 5928
			DecodedWellKnownAttributeData = 2,
			// Token: 0x04001729 RID: 5929
			Attributes = 4,
			// Token: 0x0400172A RID: 5930
			All = 7
		}
	}
}
