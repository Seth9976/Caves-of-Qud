using System;
using System.IO;
using System.Text;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000003 RID: 3
	internal class CustomMetadataWriter : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CustomMetadataWriter(SymWriter sym_writer)
		{
			this.sym_writer = sym_writer;
			this.stream = new MemoryStream();
			this.writer = new BinaryStreamWriter(this.stream);
			this.writer.WriteByte(4);
			this.writer.WriteByte(0);
			this.writer.Align(4);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		public void WriteUsingInfo(ImportDebugInformation import_info)
		{
			this.Write(CustomMetadataType.UsingInfo, delegate
			{
				this.writer.WriteUInt16(1);
				this.writer.WriteUInt16((ushort)import_info.Targets.Count);
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public void WriteForwardInfo(MetadataToken import_parent)
		{
			this.Write(CustomMetadataType.ForwardInfo, delegate
			{
				this.writer.WriteUInt32(import_parent.ToUInt32());
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		public void WriteIteratorScopes(StateMachineScopeDebugInformation[] scopes, MethodDebugInformation debug_info)
		{
			this.Write(CustomMetadataType.IteratorScopes, delegate
			{
				this.writer.WriteInt32(scopes.Length);
				StateMachineScopeDebugInformation[] scopes2 = scopes;
				foreach (StateMachineScopeDebugInformation stateMachineScopeDebugInformation in scopes2)
				{
					int offset = stateMachineScopeDebugInformation.Start.Offset;
					int num = (stateMachineScopeDebugInformation.End.IsEndOfMethod ? debug_info.code_size : stateMachineScopeDebugInformation.End.Offset);
					this.writer.WriteInt32(offset);
					this.writer.WriteInt32(num - 1);
				}
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
		public void WriteForwardIterator(TypeReference type)
		{
			this.Write(CustomMetadataType.ForwardIterator, delegate
			{
				this.writer.WriteBytes(Encoding.Unicode.GetBytes(type.Name));
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002184 File Offset: 0x00000384
		private void Write(CustomMetadataType type, Action write)
		{
			this.count++;
			this.writer.WriteByte(4);
			this.writer.WriteByte((byte)type);
			this.writer.Align(4);
			int position = this.writer.Position;
			this.writer.WriteUInt32(0U);
			write.Invoke();
			this.writer.Align(4);
			int position2 = this.writer.Position;
			int num = position2 - position + 4;
			this.writer.Position = position;
			this.writer.WriteInt32(num);
			this.writer.Position = position2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002224 File Offset: 0x00000424
		public void WriteCustomMetadata()
		{
			if (this.count != 0)
			{
				this.writer.BaseStream.Position = 1L;
				this.writer.WriteByte((byte)this.count);
				this.writer.Flush();
				this.sym_writer.DefineCustomMetadata("MD2", this.stream.ToArray());
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002283 File Offset: 0x00000483
		public void Dispose()
		{
			this.stream.Dispose();
		}

		// Token: 0x04000006 RID: 6
		private readonly SymWriter sym_writer;

		// Token: 0x04000007 RID: 7
		private readonly MemoryStream stream;

		// Token: 0x04000008 RID: 8
		private readonly BinaryStreamWriter writer;

		// Token: 0x04000009 RID: 9
		private int count;

		// Token: 0x0400000A RID: 10
		private const byte version = 4;
	}
}
