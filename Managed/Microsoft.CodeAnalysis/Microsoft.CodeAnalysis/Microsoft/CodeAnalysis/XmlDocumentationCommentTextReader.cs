using System;
using System.IO;
using System.Xml;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000188 RID: 392
	internal class XmlDocumentationCommentTextReader
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0002F238 File Offset: 0x0002D438
		public static XmlException ParseAndGetException(string text)
		{
			XmlDocumentationCommentTextReader xmlDocumentationCommentTextReader = XmlDocumentationCommentTextReader.s_pool.Allocate();
			XmlException ex = xmlDocumentationCommentTextReader.ParseInternal(text);
			XmlDocumentationCommentTextReader.s_pool.Free(xmlDocumentationCommentTextReader);
			return ex;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002F264 File Offset: 0x0002D464
		internal XmlException ParseInternal(string text)
		{
			this._textReader.SetText(text);
			if (this._reader == null)
			{
				this._reader = XmlReader.Create(this._textReader, XmlDocumentationCommentTextReader.s_xmlSettings);
			}
			XmlException ex;
			try
			{
				do
				{
					this._reader.Read();
				}
				while (!XmlDocumentationCommentTextReader.Reader.ReachedEnd(this._reader));
				if (this._textReader.Eof)
				{
					this._reader.Dispose();
					this._reader = null;
					this._textReader.Reset();
				}
				ex = null;
			}
			catch (XmlException ex2)
			{
				this._reader.Dispose();
				this._reader = null;
				this._textReader.Reset();
				ex = ex2;
			}
			return ex;
		}

		// Token: 0x04000479 RID: 1145
		private XmlReader _reader;

		// Token: 0x0400047A RID: 1146
		private readonly XmlDocumentationCommentTextReader.Reader _textReader = new XmlDocumentationCommentTextReader.Reader();

		// Token: 0x0400047B RID: 1147
		private static readonly ObjectPool<XmlDocumentationCommentTextReader> s_pool = new ObjectPool<XmlDocumentationCommentTextReader>(() => new XmlDocumentationCommentTextReader(), 2);

		// Token: 0x0400047C RID: 1148
		private static readonly XmlReaderSettings s_xmlSettings = new XmlReaderSettings
		{
			DtdProcessing = 0
		};

		// Token: 0x020005B0 RID: 1456
		internal sealed class Reader : TextReader
		{
			// Token: 0x060035EE RID: 13806 RVA: 0x00098159 File Offset: 0x00096359
			public void Reset()
			{
				this._text = null;
				this._position = 0;
				this._readsPastTheEnd = 0;
			}

			// Token: 0x060035EF RID: 13807 RVA: 0x00098170 File Offset: 0x00096370
			public void SetText(string text)
			{
				this._text = text;
				this._readsPastTheEnd = 0;
				if (this._position > 0)
				{
					this._position = XmlDocumentationCommentTextReader.Reader.RootStart.Length;
				}
			}

			// Token: 0x17000F46 RID: 3910
			// (get) Token: 0x060035F0 RID: 13808 RVA: 0x00098199 File Offset: 0x00096399
			internal int Position
			{
				get
				{
					return this._position;
				}
			}

			// Token: 0x060035F1 RID: 13809 RVA: 0x000981A1 File Offset: 0x000963A1
			public static bool ReachedEnd(XmlReader reader)
			{
				return reader.Depth == 1 && reader.NodeType == 15 && reader.Name == XmlDocumentationCommentTextReader.Reader.s_currentElementName;
			}

			// Token: 0x17000F47 RID: 3911
			// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000981C8 File Offset: 0x000963C8
			public bool Eof
			{
				get
				{
					return this._readsPastTheEnd >= 100;
				}
			}

			// Token: 0x060035F3 RID: 13811 RVA: 0x000981D8 File Offset: 0x000963D8
			public override int Read(char[] buffer, int index, int count)
			{
				if (count == 0 || this.Eof)
				{
					return 0;
				}
				int num = count;
				this._position += XmlDocumentationCommentTextReader.Reader.EncodeAndAdvance(XmlDocumentationCommentTextReader.Reader.RootStart, this._position, buffer, ref index, ref count);
				this._position += XmlDocumentationCommentTextReader.Reader.EncodeAndAdvance(XmlDocumentationCommentTextReader.Reader.CurrentStart, this._position - XmlDocumentationCommentTextReader.Reader.RootStart.Length, buffer, ref index, ref count);
				this._position += XmlDocumentationCommentTextReader.Reader.EncodeAndAdvance(this._text, this._position - XmlDocumentationCommentTextReader.Reader.RootStart.Length - XmlDocumentationCommentTextReader.Reader.CurrentStart.Length, buffer, ref index, ref count);
				this._position += XmlDocumentationCommentTextReader.Reader.EncodeAndAdvance(XmlDocumentationCommentTextReader.Reader.CurrentEnd, this._position - XmlDocumentationCommentTextReader.Reader.RootStart.Length - XmlDocumentationCommentTextReader.Reader.CurrentStart.Length - this._text.Length, buffer, ref index, ref count);
				if (num == count)
				{
					this._readsPastTheEnd++;
					buffer[index] = ' ';
					count--;
				}
				return num - count;
			}

			// Token: 0x060035F4 RID: 13812 RVA: 0x000982E0 File Offset: 0x000964E0
			private static int EncodeAndAdvance(string src, int srcIndex, char[] dest, ref int destIndex, ref int destCount)
			{
				if (destCount == 0 || srcIndex < 0 || srcIndex >= src.Length)
				{
					return 0;
				}
				int num = Math.Min(src.Length - srcIndex, destCount);
				src.CopyTo(srcIndex, dest, destIndex, num);
				destIndex += num;
				destCount -= num;
				return num;
			}

			// Token: 0x060035F5 RID: 13813 RVA: 0x0009832C File Offset: 0x0009652C
			public override int Read()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060035F6 RID: 13814 RVA: 0x00098333 File Offset: 0x00096533
			public override int Peek()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x0400165D RID: 5725
			private string _text;

			// Token: 0x0400165E RID: 5726
			private int _position;

			// Token: 0x0400165F RID: 5727
			private const int maxReadsPastTheEnd = 100;

			// Token: 0x04001660 RID: 5728
			private int _readsPastTheEnd;

			// Token: 0x04001661 RID: 5729
			private static readonly string s_rootElementName = "_" + Guid.NewGuid().ToString("N");

			// Token: 0x04001662 RID: 5730
			private static readonly string s_currentElementName = "_" + Guid.NewGuid().ToString("N");

			// Token: 0x04001663 RID: 5731
			internal static readonly string RootStart = "<" + XmlDocumentationCommentTextReader.Reader.s_rootElementName + ">";

			// Token: 0x04001664 RID: 5732
			internal static readonly string CurrentStart = "<" + XmlDocumentationCommentTextReader.Reader.s_currentElementName + ">";

			// Token: 0x04001665 RID: 5733
			internal static readonly string CurrentEnd = "</" + XmlDocumentationCommentTextReader.Reader.s_currentElementName + ">";
		}
	}
}
