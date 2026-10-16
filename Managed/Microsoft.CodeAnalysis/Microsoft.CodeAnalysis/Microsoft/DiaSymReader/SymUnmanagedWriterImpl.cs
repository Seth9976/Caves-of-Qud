using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000105 RID: 261
	internal sealed class SymUnmanagedWriterImpl : SymUnmanagedWriter
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x000191F8 File Offset: 0x000173F8
		internal SymUnmanagedWriterImpl(ComMemoryStream pdbStream, ISymUnmanagedWriter5 symWriter, string symWriterModuleName)
		{
			this._pdbStream = pdbStream;
			this._symWriter = symWriter;
			this._documentWriters = new List<ISymUnmanagedDocumentWriter>();
			this._symWriterModuleName = symWriterModuleName;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00019220 File Offset: 0x00017420
		private ISymUnmanagedWriter5 GetSymWriter()
		{
			ISymUnmanagedWriter5 symWriter = this._symWriter;
			if (symWriter == null)
			{
				throw this._disposed ? new ObjectDisposedException("SymUnmanagedWriterImpl") : new InvalidOperationException();
			}
			return symWriter;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00019248 File Offset: 0x00017448
		private ISymUnmanagedWriter8 GetSymWriter8()
		{
			ISymUnmanagedWriter8 symUnmanagedWriter = this.GetSymWriter() as ISymUnmanagedWriter8;
			if (symUnmanagedWriter == null)
			{
				throw this.PdbWritingException(new NotSupportedException());
			}
			return symUnmanagedWriter;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00019271 File Offset: 0x00017471
		private Exception PdbWritingException(Exception inner)
		{
			return new SymUnmanagedWriterException(inner, this._symWriterModuleName);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00019280 File Offset: 0x00017480
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.CloseSymWriter();
			try
			{
				this._pdbStream.CopyTo(stream);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000192C8 File Offset: 0x000174C8
		public override void Dispose()
		{
			this.DisposeImpl();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000192D8 File Offset: 0x000174D8
		~SymUnmanagedWriterImpl()
		{
			this.DisposeImpl();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00019304 File Offset: 0x00017504
		private void DisposeImpl()
		{
			try
			{
				this.CloseSymWriter();
			}
			catch
			{
			}
			this._disposed = true;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00019334 File Offset: 0x00017534
		private void CloseSymWriter()
		{
			ISymUnmanagedWriter5 symUnmanagedWriter = Interlocked.Exchange<ISymUnmanagedWriter5>(ref this._symWriter, null);
			if (symUnmanagedWriter == null)
			{
				return;
			}
			try
			{
				symUnmanagedWriter.Close();
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
			finally
			{
				this._documentWriters.Clear();
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001938C File Offset: 0x0001758C
		public override IEnumerable<ArraySegment<byte>> GetUnderlyingData()
		{
			this.GetSymWriter().Commit();
			return this._pdbStream.GetChunks();
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000193A4 File Offset: 0x000175A4
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x000193B1 File Offset: 0x000175B1
		public override int DocumentTableCapacity
		{
			get
			{
				return this._documentWriters.Capacity;
			}
			set
			{
				if (value > this._documentWriters.Count)
				{
					this._documentWriters.Capacity = value;
				}
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000193D0 File Offset: 0x000175D0
		public unsafe override int DefineDocument(string name, Guid language, Guid vendor, Guid type, Guid algorithmId, ReadOnlySpan<byte> checksum, ReadOnlySpan<byte> source)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			int count = this._documentWriters.Count;
			ISymUnmanagedDocumentWriter symUnmanagedDocumentWriter;
			try
			{
				symUnmanagedDocumentWriter = symWriter.DefineDocument(name, ref language, ref vendor, ref type);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
			this._documentWriters.Add(symUnmanagedDocumentWriter);
			if (algorithmId != default(Guid) && checksum.Length > 0)
			{
				try
				{
					try
					{
						fixed (byte* ptr = checksum.GetPinnableReference())
						{
							byte* ptr2 = ptr;
							symUnmanagedDocumentWriter.SetCheckSum(algorithmId, (uint)checksum.Length, ptr2);
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				catch (Exception ex2)
				{
					throw this.PdbWritingException(ex2);
				}
			}
			if (source != null)
			{
				try
				{
					try
					{
						fixed (byte* ptr = source.GetPinnableReference())
						{
							byte* ptr3 = ptr;
							symUnmanagedDocumentWriter.SetSource((uint)source.Length, ptr3);
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				catch (Exception ex3)
				{
					throw this.PdbWritingException(ex3);
				}
			}
			return count;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000194F4 File Offset: 0x000176F4
		public override void DefineSequencePoints(int documentIndex, int count, int[] offsets, int[] startLines, int[] startColumns, int[] endLines, int[] endColumns)
		{
			if (documentIndex < 0 || documentIndex >= this._documentWriters.Count)
			{
				throw new ArgumentOutOfRangeException("documentIndex");
			}
			if (offsets == null)
			{
				throw new ArgumentNullException("offsets");
			}
			if (startLines == null)
			{
				throw new ArgumentNullException("startLines");
			}
			if (startColumns == null)
			{
				throw new ArgumentNullException("startColumns");
			}
			if (endLines == null)
			{
				throw new ArgumentNullException("endLines");
			}
			if (endColumns == null)
			{
				throw new ArgumentNullException("endColumns");
			}
			if (count < 0 || count > startLines.Length || count > startColumns.Length || count > endLines.Length || count > endColumns.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.DefineSequencePoints(this._documentWriters[documentIndex], count, offsets, startLines, startColumns, endLines, endColumns);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000195D4 File Offset: 0x000177D4
		public override void OpenMethod(int methodToken)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.OpenMethod((uint)methodToken);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001960C File Offset: 0x0001780C
		public override void CloseMethod()
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.CloseMethod();
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00019644 File Offset: 0x00017844
		public override void OpenScope(int startOffset)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.OpenScope(startOffset);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001967C File Offset: 0x0001787C
		public override void CloseScope(int endOffset)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.CloseScope(endOffset);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000196B4 File Offset: 0x000178B4
		public override void DefineLocalVariable(int index, string name, int attributes, int localSignatureToken)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.DefineLocalVariable2(name, attributes, localSignatureToken, 1U, index, 0U, 0U, 0U, 0U);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000196F4 File Offset: 0x000178F4
		public override bool DefineLocalConstant(string name, object value, int constantSignatureToken)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			string text = value as string;
			if (text != null)
			{
				return this.DefineLocalStringConstant(symWriter, name, text, constantSignatureToken);
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				try
				{
					symWriter.DefineConstant2(name, new VariantStructure(dateTime), constantSignatureToken);
				}
				catch (Exception ex)
				{
					throw this.PdbWritingException(ex);
				}
				return true;
			}
			try
			{
				this.DefineLocalConstantImpl(symWriter, name, value ?? SymUnmanagedWriterImpl.s_zeroInt32, constantSignatureToken);
			}
			catch (Exception ex2)
			{
				throw this.PdbWritingException(ex2);
			}
			return true;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00019788 File Offset: 0x00017988
		private unsafe void DefineLocalConstantImpl(ISymUnmanagedWriter5 symWriter, string name, object value, int constantSignatureToken)
		{
			VariantStructure variantStructure = default(VariantStructure);
			Marshal.GetNativeVariantForObject(value, new IntPtr((void*)(&variantStructure)));
			symWriter.DefineConstant2(name, variantStructure, constantSignatureToken);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000197B8 File Offset: 0x000179B8
		private bool DefineLocalStringConstant(ISymUnmanagedWriter5 symWriter, string name, string value, int constantSignatureToken)
		{
			int num;
			if (!SymUnmanagedWriterImpl.IsValidUnicodeString(value))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				num = bytes.Length;
				value = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			}
			else
			{
				num = Encoding.UTF8.GetByteCount(value);
			}
			num++;
			if (num > 2032)
			{
				return false;
			}
			try
			{
				this.DefineLocalConstantImpl(symWriter, name, value, constantSignatureToken);
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
			return true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00019848 File Offset: 0x00017A48
		private static bool IsValidUnicodeString(string str)
		{
			int i = 0;
			while (i < str.Length)
			{
				char c = str.get_Chars(i++);
				if (char.IsHighSurrogate(c))
				{
					if (i >= str.Length || !char.IsLowSurrogate(str.get_Chars(i)))
					{
						return false;
					}
					i++;
				}
				else if (char.IsLowSurrogate(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000198A0 File Offset: 0x00017AA0
		public override void UsingNamespace(string importString)
		{
			if (importString == null)
			{
				throw new ArgumentNullException("importString");
			}
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.UsingNamespace(importString);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000198E4 File Offset: 0x00017AE4
		public unsafe override void SetAsyncInfo(int moveNextMethodToken, int kickoffMethodToken, int catchHandlerOffset, ReadOnlySpan<int> yieldOffsets, ReadOnlySpan<int> resumeOffsets)
		{
			if (yieldOffsets == null)
			{
				throw new ArgumentNullException("yieldOffsets");
			}
			if (resumeOffsets == null)
			{
				throw new ArgumentNullException("resumeOffsets");
			}
			if (yieldOffsets.Length != resumeOffsets.Length)
			{
				throw new ArgumentOutOfRangeException("yieldOffsets");
			}
			ISymUnmanagedAsyncMethodPropertiesWriter symUnmanagedAsyncMethodPropertiesWriter = this.GetSymWriter() as ISymUnmanagedAsyncMethodPropertiesWriter;
			if (symUnmanagedAsyncMethodPropertiesWriter != null)
			{
				int length = yieldOffsets.Length;
				if (length > 0)
				{
					int[] array = new int[length];
					for (int i = 0; i < length; i++)
					{
						array[i] = moveNextMethodToken;
					}
					try
					{
						try
						{
							fixed (int* ptr = yieldOffsets.GetPinnableReference())
							{
								int* ptr2 = ptr;
								try
								{
									fixed (int* ptr3 = resumeOffsets.GetPinnableReference())
									{
										int* ptr4 = ptr3;
										try
										{
											int[] array2;
											int* ptr5;
											if ((array2 = array) == null || array2.Length == 0)
											{
												ptr5 = null;
											}
											else
											{
												ptr5 = &array2[0];
											}
											symUnmanagedAsyncMethodPropertiesWriter.DefineAsyncStepInfo(length, ptr2, ptr4, ptr5);
										}
										finally
										{
											int[] array2 = null;
										}
									}
								}
								finally
								{
									int* ptr3 = null;
								}
							}
						}
						finally
						{
							int* ptr = null;
						}
					}
					catch (Exception ex)
					{
						throw this.PdbWritingException(ex);
					}
				}
				try
				{
					if (catchHandlerOffset >= 0)
					{
						symUnmanagedAsyncMethodPropertiesWriter.DefineCatchHandlerILOffset(catchHandlerOffset);
					}
					symUnmanagedAsyncMethodPropertiesWriter.DefineKickoffMethod(kickoffMethodToken);
				}
				catch (Exception ex2)
				{
					throw this.PdbWritingException(ex2);
				}
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00019A40 File Offset: 0x00017C40
		public unsafe override void DefineCustomMetadata(byte[] metadata)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (metadata.Length == 0)
			{
				return;
			}
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				try
				{
					fixed (byte[] array = metadata)
					{
						byte* ptr;
						if (metadata == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						symWriter.SetSymAttribute(0U, "MD2", metadata.Length, ptr);
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00019ABC File Offset: 0x00017CBC
		public override void SetEntryPoint(int entryMethodToken)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.SetUserEntryPoint(entryMethodToken);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00019AF4 File Offset: 0x00017CF4
		public override void UpdateSignature(Guid guid, uint stamp, int age)
		{
			ISymUnmanagedWriter8 symWriter = this.GetSymWriter8();
			try
			{
				symWriter.UpdateSignature(guid, stamp, age);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00019B2C File Offset: 0x00017D2C
		public unsafe override void SetSourceServerData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 0)
			{
				return;
			}
			ISymUnmanagedWriter8 symWriter = this.GetSymWriter8();
			try
			{
				try
				{
					fixed (byte[] array = data)
					{
						byte* ptr;
						if (data == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						symWriter.SetSourceServerData(ptr, data.Length);
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00019BA4 File Offset: 0x00017DA4
		public unsafe override void SetSourceLinkData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 0)
			{
				return;
			}
			ISymUnmanagedWriter8 symWriter = this.GetSymWriter8();
			try
			{
				try
				{
					fixed (byte[] array = data)
					{
						byte* ptr;
						if (data == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						symWriter.SetSourceLinkData(ptr, data.Length);
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00019C1C File Offset: 0x00017E1C
		public override void OpenTokensToSourceSpansMap()
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.OpenMapTokensToSourceSpans();
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00019C54 File Offset: 0x00017E54
		public override void MapTokenToSourceSpan(int token, int documentIndex, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (documentIndex < 0 || documentIndex >= this._documentWriters.Count)
			{
				throw new ArgumentOutOfRangeException("documentIndex");
			}
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.MapTokenToSourceSpan(token, this._documentWriters[documentIndex], startLine, startColumn, endLine, endColumn);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00019CBC File Offset: 0x00017EBC
		public override void CloseTokensToSourceSpansMap()
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			try
			{
				symWriter.CloseMapTokensToSourceSpans();
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00019CF4 File Offset: 0x00017EF4
		public unsafe override void GetSignature(out Guid guid, out uint stamp, out int age)
		{
			ISymUnmanagedWriter5 symWriter = this.GetSymWriter();
			ImageDebugDirectory imageDebugDirectory = default(ImageDebugDirectory);
			uint num;
			try
			{
				symWriter.GetDebugInfo(ref imageDebugDirectory, 0U, out num, null);
			}
			catch (Exception ex)
			{
				throw this.PdbWritingException(ex);
			}
			byte[] array = new byte[num];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			try
			{
				symWriter.GetDebugInfo(ref imageDebugDirectory, num, out num, ptr);
			}
			catch (Exception ex2)
			{
				throw this.PdbWritingException(ex2);
			}
			array2 = null;
			byte[] array3 = new byte[16];
			Buffer.BlockCopy(array, 4, array3, 0, array3.Length);
			guid = new Guid(array3);
			((IPdbWriter)symWriter).GetSignatureAge(out stamp, out age);
		}

		// Token: 0x04000286 RID: 646
		private static readonly object s_zeroInt32 = 0;

		// Token: 0x04000287 RID: 647
		private ISymUnmanagedWriter5 _symWriter;

		// Token: 0x04000288 RID: 648
		private readonly ComMemoryStream _pdbStream;

		// Token: 0x04000289 RID: 649
		private readonly List<ISymUnmanagedDocumentWriter> _documentWriters;

		// Token: 0x0400028A RID: 650
		private readonly string _symWriterModuleName;

		// Token: 0x0400028B RID: 651
		private bool _disposed;
	}
}
