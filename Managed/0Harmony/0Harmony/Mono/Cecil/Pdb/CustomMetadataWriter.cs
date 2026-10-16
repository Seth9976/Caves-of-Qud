using System;
using System.IO;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil.Pdb
{
	// Token: 0x0200032C RID: 812
	internal class CustomMetadataWriter : IDisposable
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x00042E4C File Offset: 0x0004104C
		public CustomMetadataWriter(SymWriter sym_writer)
		{
			this.sym_writer = sym_writer;
			this.stream = new MemoryStream();
			this.writer = new BinaryStreamWriter(this.stream);
			this.writer.WriteByte(4);
			this.writer.WriteByte(0);
			this.writer.Align(4);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00042EA8 File Offset: 0x000410A8
		public void WriteUsingInfo(ImportDebugInformation import_info)
		{
			this.Write(CustomMetadataType.UsingInfo, delegate
			{
				this.writer.WriteUInt16(1);
				this.writer.WriteUInt16((ushort)import_info.Targets.Count);
			});
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00042EDC File Offset: 0x000410DC
		public void WriteForwardInfo(MetadataToken import_parent)
		{
			this.Write(CustomMetadataType.ForwardInfo, delegate
			{
				this.writer.WriteUInt32(import_parent.ToUInt32());
			});
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00042F10 File Offset: 0x00041110
		public void WriteIteratorScopes(StateMachineScopeDebugInformation state_machine, MethodDebugInformation debug_info)
		{
			this.Write(CustomMetadataType.IteratorScopes, delegate
			{
				Collection<StateMachineScope> scopes = state_machine.Scopes;
				this.writer.WriteInt32(scopes.Count);
				foreach (StateMachineScope stateMachineScope in scopes)
				{
					int offset = stateMachineScope.Start.Offset;
					int num = (stateMachineScope.End.IsEndOfMethod ? debug_info.code_size : stateMachineScope.End.Offset);
					this.writer.WriteInt32(offset);
					this.writer.WriteInt32(num - 1);
				}
			});
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00042F4C File Offset: 0x0004114C
		public void WriteForwardIterator(TypeReference type)
		{
			this.Write(CustomMetadataType.ForwardIterator, delegate
			{
				this.writer.WriteBytes(Encoding.Unicode.GetBytes(type.Name));
			});
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00042F80 File Offset: 0x00041180
		private void Write(CustomMetadataType type, Action write)
		{
			this.count++;
			this.writer.WriteByte(4);
			this.writer.WriteByte((byte)type);
			this.writer.Align(4);
			int position = this.writer.Position;
			this.writer.WriteUInt32(0U);
			write();
			this.writer.Align(4);
			int position2 = this.writer.Position;
			int num = position2 - position + 4;
			this.writer.Position = position;
			this.writer.WriteInt32(num);
			this.writer.Position = position2;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00043020 File Offset: 0x00041220
		public void WriteCustomMetadata()
		{
			if (this.count == 0)
			{
				return;
			}
			this.writer.BaseStream.Position = 1L;
			this.writer.WriteByte((byte)this.count);
			this.writer.Flush();
			this.sym_writer.DefineCustomMetadata("MD2", this.stream.ToArray());
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00043080 File Offset: 0x00041280
		public void Dispose()
		{
			this.stream.Dispose();
		}

		// Token: 0x04000A89 RID: 2697
		private readonly SymWriter sym_writer;

		// Token: 0x04000A8A RID: 2698
		private readonly MemoryStream stream;

		// Token: 0x04000A8B RID: 2699
		private readonly BinaryStreamWriter writer;

		// Token: 0x04000A8C RID: 2700
		private int count;

		// Token: 0x04000A8D RID: 2701
		private const byte version = 4;
	}
}
