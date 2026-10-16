using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x0200035B RID: 859
	internal struct CustomDebugInfoEncoder
	{
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x00072367 File Offset: 0x00070567
		public readonly BlobBuilder Builder { get; }

		// Token: 0x060024CD RID: 9421 RVA: 0x0007236F File Offset: 0x0007056F
		public CustomDebugInfoEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
			this._recordCount = 0;
			builder.WriteByte(4);
			this._recordCountFixup = builder.ReserveBytes(1);
			builder.WriteInt16(0);
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0007239A File Offset: 0x0007059A
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000723A4 File Offset: 0x000705A4
		public byte[] ToArray()
		{
			if (this._recordCount == 0)
			{
				return null;
			}
			new BlobWriter(this._recordCountFixup).WriteByte((byte)this._recordCount);
			return this.Builder.ToArray();
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000723E0 File Offset: 0x000705E0
		public void AddStateMachineTypeName(string typeName)
		{
			this.AddRecord<string>(CustomDebugInfoKind.StateMachineTypeName, typeName, delegate(string name, BlobBuilder builder)
			{
				builder.WriteUTF16(name);
				builder.WriteInt16(0);
			});
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00072409 File Offset: 0x00070609
		public void AddForwardMethodInfo(MethodDefinitionHandle methodHandle)
		{
			this.AddRecord<MethodDefinitionHandle>(CustomDebugInfoKind.ForwardMethodInfo, methodHandle, delegate(MethodDefinitionHandle mh, BlobBuilder builder)
			{
				builder.WriteInt32(MetadataTokens.GetToken(mh));
			});
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00072432 File Offset: 0x00070632
		public void AddForwardModuleInfo(MethodDefinitionHandle methodHandle)
		{
			this.AddRecord<MethodDefinitionHandle>(CustomDebugInfoKind.ForwardModuleInfo, methodHandle, delegate(MethodDefinitionHandle mh, BlobBuilder builder)
			{
				builder.WriteInt32(MetadataTokens.GetToken(mh));
			});
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0007245B File Offset: 0x0007065B
		public void AddUsingGroups(IReadOnlyCollection<int> groupSizes)
		{
			if (groupSizes.Count == 0)
			{
				return;
			}
			this.AddRecord<IReadOnlyCollection<int>>(CustomDebugInfoKind.UsingGroups, groupSizes, delegate(IReadOnlyCollection<int> uc, BlobBuilder builder)
			{
				builder.WriteUInt16((ushort)uc.Count);
				foreach (int num in uc)
				{
					builder.WriteUInt16((ushort)num);
				}
			});
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0007248D File Offset: 0x0007068D
		public void AddStateMachineHoistedLocalScopes(ImmutableArray<StateMachineHoistedLocalScope> scopes)
		{
			if (scopes.IsDefaultOrEmpty)
			{
				return;
			}
			this.AddRecord<ImmutableArray<StateMachineHoistedLocalScope>>(CustomDebugInfoKind.StateMachineHoistedLocalScopes, scopes, delegate(ImmutableArray<StateMachineHoistedLocalScope> s, BlobBuilder builder)
			{
				builder.WriteInt32(s.Length);
				foreach (StateMachineHoistedLocalScope stateMachineHoistedLocalScope in s)
				{
					if (stateMachineHoistedLocalScope.IsDefault)
					{
						builder.WriteInt32(0);
						builder.WriteInt32(0);
					}
					else
					{
						builder.WriteInt32(stateMachineHoistedLocalScope.StartOffset);
						builder.WriteInt32(stateMachineHoistedLocalScope.EndOffset - 1);
					}
				}
			});
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000724C0 File Offset: 0x000706C0
		public void AddDynamicLocals([TupleElementNames(new string[] { "LocalName", "Flags", "Count", "SlotIndex" })] IReadOnlyCollection<ValueTuple<string, byte[], int, int>> dynamicLocals)
		{
			this.AddRecord<IReadOnlyCollection<ValueTuple<string, byte[], int, int>>>(CustomDebugInfoKind.DynamicLocals, dynamicLocals, delegate([TupleElementNames(new string[] { "LocalName", "Flags", "Count", "SlotIndex" })] IReadOnlyCollection<ValueTuple<string, byte[], int, int>> infos, BlobBuilder builder)
			{
				builder.WriteInt32(infos.Count);
				foreach (ValueTuple<string, byte[], int, int> valueTuple in infos)
				{
					builder.WriteBytes(valueTuple.Item2);
					builder.WriteBytes(0, 64 - valueTuple.Item2.Length);
					builder.WriteInt32(valueTuple.Item3);
					builder.WriteInt32(valueTuple.Item4);
					builder.WriteUTF16(valueTuple.Item1);
					builder.WriteBytes(0, 2 * (64 - valueTuple.Item1.Length));
				}
			});
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000724E9 File Offset: 0x000706E9
		public void AddTupleElementNames([TupleElementNames(new string[] { "LocalName", "SlotIndex", "ScopeStart", "ScopeEnd", "Names" })] IReadOnlyCollection<ValueTuple<string, int, int, int, ImmutableArray<string>>> tupleLocals)
		{
			this.AddRecord<IReadOnlyCollection<ValueTuple<string, int, int, int, ImmutableArray<string>>>>(CustomDebugInfoKind.TupleElementNames, tupleLocals, delegate([TupleElementNames(new string[] { "LocalName", "SlotIndex", "ScopeStart", "ScopeEnd", "Names" })] IReadOnlyCollection<ValueTuple<string, int, int, int, ImmutableArray<string>>> infos, BlobBuilder builder)
			{
				builder.WriteInt32(infos.Count);
				foreach (ValueTuple<string, int, int, int, ImmutableArray<string>> valueTuple in infos)
				{
					ImmutableArray<string> immutableArray = valueTuple.Item5;
					builder.WriteInt32(immutableArray.Length);
					immutableArray = valueTuple.Item5;
					foreach (string text in immutableArray)
					{
						if (text != null)
						{
							builder.WriteUTF8(text, true);
						}
						builder.WriteByte(0);
					}
					builder.WriteInt32(valueTuple.Item2);
					builder.WriteInt32(valueTuple.Item3);
					builder.WriteInt32(valueTuple.Item4);
					if (valueTuple.Item1 != null)
					{
						builder.WriteUTF8(valueTuple.Item1, true);
					}
					builder.WriteByte(0);
				}
			});
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00072514 File Offset: 0x00070714
		public void AddRecord<T>(CustomDebugInfoKind kind, T debugInfo, Action<T, BlobBuilder> recordSerializer)
		{
			int count = this.Builder.Count;
			this.Builder.WriteByte(4);
			this.Builder.WriteByte((byte)kind);
			this.Builder.WriteByte(0);
			BlobWriter blobWriter = new BlobWriter(this.Builder.ReserveBytes(5));
			recordSerializer.Invoke(debugInfo, this.Builder);
			int num = this.Builder.Count - count;
			int num2 = 4 * ((num + 3) / 4);
			byte b = (byte)(num2 - num);
			this.Builder.WriteBytes(0, (int)b);
			blobWriter.WriteByte((kind > CustomDebugInfoKind.DynamicLocals) ? b : 0);
			blobWriter.WriteUInt32((uint)num2);
			this._recordCount++;
		}

		// Token: 0x04000FC7 RID: 4039
		private readonly Blob _recordCountFixup;

		// Token: 0x04000FC8 RID: 4040
		private int _recordCount;

		// Token: 0x04000FC9 RID: 4041
		internal const int DynamicAttributeSize = 64;

		// Token: 0x04000FCA RID: 4042
		internal const int IdentifierSize = 64;
	}
}
