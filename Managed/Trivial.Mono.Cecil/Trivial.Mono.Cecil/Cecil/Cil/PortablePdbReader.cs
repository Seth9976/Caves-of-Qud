using System;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000125 RID: 293
	public sealed class PortablePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00025084 File Offset: 0x00023284
		private bool IsEmbedded
		{
			get
			{
				return this.reader.image == this.debug_reader.image;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002509E File Offset: 0x0002329E
		internal PortablePdbReader(Image image, ModuleDefinition module)
		{
			this.image = image;
			this.module = module;
			this.reader = module.reader;
			this.debug_reader = new MetadataReader(image, module, this.reader);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000250D3 File Offset: 0x000232D3
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new PortablePdbWriterProvider();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000250DC File Offset: 0x000232DC
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (this.image == this.module.Image)
			{
				return true;
			}
			ImageDebugHeaderEntry codeViewEntry = header.GetCodeViewEntry();
			if (codeViewEntry == null)
			{
				return false;
			}
			byte[] data = codeViewEntry.Data;
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
			Guid guid;
			guid..ctor(array);
			Buffer.BlockCopy(this.image.PdbHeap.Id, 0, array, 0, 16);
			Guid guid2;
			guid2..ctor(array);
			if (guid != guid2)
			{
				return false;
			}
			this.ReadModule();
			return true;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002517A File Offset: 0x0002337A
		private static int ReadInt32(byte[] bytes, int start)
		{
			return (int)bytes[start] | ((int)bytes[start + 1] << 8) | ((int)bytes[start + 2] << 16) | ((int)bytes[start + 3] << 24);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00025199 File Offset: 0x00023399
		private void ReadModule()
		{
			this.module.custom_infos = this.debug_reader.GetCustomDebugInformation(this.module);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000251B8 File Offset: 0x000233B8
		public MethodDebugInformation Read(MethodDefinition method)
		{
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			this.ReadSequencePoints(methodDebugInformation);
			this.ReadScope(methodDebugInformation);
			this.ReadStateMachineKickOffMethod(methodDebugInformation);
			this.ReadCustomDebugInformations(methodDebugInformation);
			return methodDebugInformation;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000251E9 File Offset: 0x000233E9
		private void ReadSequencePoints(MethodDebugInformation method_info)
		{
			method_info.sequence_points = this.debug_reader.ReadSequencePoints(method_info.method);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00025202 File Offset: 0x00023402
		private void ReadScope(MethodDebugInformation method_info)
		{
			method_info.scope = this.debug_reader.ReadScope(method_info.method);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002521B File Offset: 0x0002341B
		private void ReadStateMachineKickOffMethod(MethodDebugInformation method_info)
		{
			method_info.kickoff_method = this.debug_reader.ReadStateMachineKickoffMethod(method_info.method);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00025234 File Offset: 0x00023434
		private void ReadCustomDebugInformations(MethodDebugInformation info)
		{
			info.method.custom_infos = this.debug_reader.GetCustomDebugInformation(info.method);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00025252 File Offset: 0x00023452
		public void Dispose()
		{
			if (!this.IsEmbedded)
			{
				this.image.Dispose();
			}
		}

		// Token: 0x04000697 RID: 1687
		private readonly Image image;

		// Token: 0x04000698 RID: 1688
		private readonly ModuleDefinition module;

		// Token: 0x04000699 RID: 1689
		private readonly MetadataReader reader;

		// Token: 0x0400069A RID: 1690
		private readonly MetadataReader debug_reader;
	}
}
