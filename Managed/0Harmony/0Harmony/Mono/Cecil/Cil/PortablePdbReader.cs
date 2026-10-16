using System;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002C9 RID: 713
	internal sealed class PortablePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0003BA8C File Offset: 0x00039C8C
		private bool IsEmbedded
		{
			get
			{
				return this.reader.image == this.debug_reader.image;
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003BAA6 File Offset: 0x00039CA6
		internal PortablePdbReader(Image image, ModuleDefinition module)
		{
			this.image = image;
			this.module = module;
			this.reader = module.reader;
			this.debug_reader = new MetadataReader(image, module, this.reader);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0003BADB File Offset: 0x00039CDB
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new PortablePdbWriterProvider();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003BAE4 File Offset: 0x00039CE4
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (this.image == this.module.Image)
			{
				return true;
			}
			foreach (ImageDebugHeaderEntry imageDebugHeaderEntry in header.Entries)
			{
				if (PortablePdbReader.IsMatchingEntry(this.image.PdbHeap, imageDebugHeaderEntry))
				{
					this.ReadModule();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003BB3C File Offset: 0x00039D3C
		private static bool IsMatchingEntry(PdbHeap heap, ImageDebugHeaderEntry entry)
		{
			if (entry.Directory.Type != ImageDebugType.CodeView)
			{
				return false;
			}
			byte[] data = entry.Data;
			if (data.Length < 24)
			{
				return false;
			}
			if (PortablePdbReader.ReadInt32(data, 0) != 1396986706)
			{
				return false;
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, 4, array, 0, 16);
			Guid guid = new Guid(array);
			Buffer.BlockCopy(heap.Id, 0, array, 0, 16);
			Guid guid2 = new Guid(array);
			return guid == guid2;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		private static int ReadInt32(byte[] bytes, int start)
		{
			return (int)bytes[start] | ((int)bytes[start + 1] << 8) | ((int)bytes[start + 2] << 16) | ((int)bytes[start + 3] << 24);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0003BBCF File Offset: 0x00039DCF
		private void ReadModule()
		{
			this.module.custom_infos = this.debug_reader.GetCustomDebugInformation(this.module);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		public MethodDebugInformation Read(MethodDefinition method)
		{
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			this.ReadSequencePoints(methodDebugInformation);
			this.ReadScope(methodDebugInformation);
			this.ReadStateMachineKickOffMethod(methodDebugInformation);
			this.ReadCustomDebugInformations(methodDebugInformation);
			return methodDebugInformation;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003BC21 File Offset: 0x00039E21
		private void ReadSequencePoints(MethodDebugInformation method_info)
		{
			method_info.sequence_points = this.debug_reader.ReadSequencePoints(method_info.method);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0003BC3A File Offset: 0x00039E3A
		private void ReadScope(MethodDebugInformation method_info)
		{
			method_info.scope = this.debug_reader.ReadScope(method_info.method);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0003BC53 File Offset: 0x00039E53
		private void ReadStateMachineKickOffMethod(MethodDebugInformation method_info)
		{
			method_info.kickoff_method = this.debug_reader.ReadStateMachineKickoffMethod(method_info.method);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003BC6C File Offset: 0x00039E6C
		private void ReadCustomDebugInformations(MethodDebugInformation info)
		{
			info.method.custom_infos = this.debug_reader.GetCustomDebugInformation(info.method);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003BC8A File Offset: 0x00039E8A
		public void Dispose()
		{
			if (this.IsEmbedded)
			{
				return;
			}
			this.image.Dispose();
		}

		// Token: 0x04000934 RID: 2356
		private readonly Image image;

		// Token: 0x04000935 RID: 2357
		private readonly ModuleDefinition module;

		// Token: 0x04000936 RID: 2358
		private readonly MetadataReader reader;

		// Token: 0x04000937 RID: 2359
		private readonly MetadataReader debug_reader;
	}
}
