using System;
using System.Runtime.InteropServices;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace Mono.Cecil.Pdb
{
	// Token: 0x02000336 RID: 822
	internal class SymWriter
	{
		// Token: 0x0600156A RID: 5482
		[DllImport("ole32.dll")]
		private static extern int CoCreateInstance([In] ref Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkOuter, [In] uint dwClsContext, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		// Token: 0x0600156B RID: 5483 RVA: 0x000433A8 File Offset: 0x000415A8
		public SymWriter()
		{
			object obj;
			SymWriter.CoCreateInstance(ref SymWriter.s_CorSymWriter_SxS_ClassID, null, 1U, ref SymWriter.s_symUnmangedWriterIID, out obj);
			this.writer = (ISymUnmanagedWriter2)obj;
			this.documents = new Collection<ISymUnmanagedDocumentWriter>();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000433E8 File Offset: 0x000415E8
		public byte[] GetDebugInfo(out ImageDebugDirectory idd)
		{
			int num;
			this.writer.GetDebugInfo(out idd, 0, out num, null);
			byte[] array = new byte[num];
			this.writer.GetDebugInfo(out idd, num, out num, array);
			return array;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00043420 File Offset: 0x00041620
		public void DefineLocalVariable2(string name, VariableAttributes attributes, int sigToken, int addr1, int addr2, int addr3, int startOffset, int endOffset)
		{
			this.writer.DefineLocalVariable2(name, (int)attributes, sigToken, 1, addr1, addr2, addr3, startOffset, endOffset);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00043446 File Offset: 0x00041646
		public void DefineConstant2(string name, object value, int sigToken)
		{
			if (value == null)
			{
				this.writer.DefineConstant2(name, 0, sigToken);
				return;
			}
			this.writer.DefineConstant2(name, value, sigToken);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00043470 File Offset: 0x00041670
		public void Close()
		{
			this.writer.Close();
			Marshal.ReleaseComObject(this.writer);
			foreach (ISymUnmanagedDocumentWriter symUnmanagedDocumentWriter in this.documents)
			{
				Marshal.ReleaseComObject(symUnmanagedDocumentWriter);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000434D8 File Offset: 0x000416D8
		public void CloseMethod()
		{
			this.writer.CloseMethod();
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000434E5 File Offset: 0x000416E5
		public void CloseNamespace()
		{
			this.writer.CloseNamespace();
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000434F2 File Offset: 0x000416F2
		public void CloseScope(int endOffset)
		{
			this.writer.CloseScope(endOffset);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00043500 File Offset: 0x00041700
		public SymDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			ISymUnmanagedDocumentWriter symUnmanagedDocumentWriter;
			this.writer.DefineDocument(url, ref language, ref languageVendor, ref documentType, out symUnmanagedDocumentWriter);
			this.documents.Add(symUnmanagedDocumentWriter);
			return new SymDocumentWriter(symUnmanagedDocumentWriter);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00043533 File Offset: 0x00041733
		public void DefineSequencePoints(SymDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
		{
			this.writer.DefineSequencePoints(document.Writer, offsets.Length, offsets, lines, columns, endLines, endColumns);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00043551 File Offset: 0x00041751
		public void Initialize(object emitter, string filename, bool fFullBuild)
		{
			this.writer.Initialize(emitter, filename, null, fFullBuild);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00043562 File Offset: 0x00041762
		public void SetUserEntryPoint(int methodToken)
		{
			this.writer.SetUserEntryPoint(methodToken);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00043570 File Offset: 0x00041770
		public void OpenMethod(int methodToken)
		{
			this.writer.OpenMethod(methodToken);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004357E File Offset: 0x0004177E
		public void OpenNamespace(string name)
		{
			this.writer.OpenNamespace(name);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004358C File Offset: 0x0004178C
		public int OpenScope(int startOffset)
		{
			int num;
			this.writer.OpenScope(startOffset, out num);
			return num;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000435A8 File Offset: 0x000417A8
		public void UsingNamespace(string fullName)
		{
			this.writer.UsingNamespace(fullName);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000435B8 File Offset: 0x000417B8
		public void DefineCustomMetadata(string name, byte[] metadata)
		{
			GCHandle gchandle = GCHandle.Alloc(metadata, GCHandleType.Pinned);
			this.writer.SetSymAttribute(0U, name, (uint)metadata.Length, gchandle.AddrOfPinnedObject());
			gchandle.Free();
		}

		// Token: 0x04000A98 RID: 2712
		private static Guid s_symUnmangedWriterIID = new Guid("0b97726e-9e6d-4f05-9a26-424022093caa");

		// Token: 0x04000A99 RID: 2713
		private static Guid s_CorSymWriter_SxS_ClassID = new Guid("108296c1-281e-11d3-bd22-0000f80849bd");

		// Token: 0x04000A9A RID: 2714
		private readonly ISymUnmanagedWriter2 writer;

		// Token: 0x04000A9B RID: 2715
		private readonly Collection<ISymUnmanagedDocumentWriter> documents;
	}
}
