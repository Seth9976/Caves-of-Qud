using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zlib;
using Microsoft.CSharp;

namespace Ionic.Zip
{
	// Token: 0x02000033 RID: 51
	[ComVisible(true)]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
	[ClassInterface(1)]
	public class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000B053 File Offset: 0x00009253
		public ZipEntry AddItem(string fileOrDirectoryName)
		{
			return this.AddItem(fileOrDirectoryName, null);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B05D File Offset: 0x0000925D
		public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
		{
			if (File.Exists(fileOrDirectoryName))
			{
				return this.AddFile(fileOrDirectoryName, directoryPathInArchive);
			}
			if (Directory.Exists(fileOrDirectoryName))
			{
				return this.AddDirectory(fileOrDirectoryName, directoryPathInArchive);
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", fileOrDirectoryName));
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B091 File Offset: 0x00009291
		public ZipEntry AddFile(string fileName)
		{
			return this.AddFile(fileName, null);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B09C File Offset: 0x0000929C
		public ZipEntry AddFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			ZipEntry zipEntry = ZipEntry.CreateFromFile(fileName, text);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B0DC File Offset: 0x000092DC
		public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (ZipEntry zipEntry in entriesToRemove)
			{
				this.RemoveEntry(zipEntry);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B134 File Offset: 0x00009334
		public void RemoveEntries(ICollection<string> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (string text in entriesToRemove)
			{
				this.RemoveEntry(text);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B18C File Offset: 0x0000938C
		public void AddFiles(IEnumerable<string> fileNames)
		{
			this.AddFiles(fileNames, null);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B196 File Offset: 0x00009396
		public void UpdateFiles(IEnumerable<string> fileNames)
		{
			this.UpdateFiles(fileNames, null);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			this.AddFiles(fileNames, false, directoryPathInArchive);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000B1AC File Offset: 0x000093AC
		public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this._addOperationCanceled = false;
			this.OnAddStarted();
			if (preserveDirHierarchy)
			{
				using (IEnumerator<string> enumerator = fileNames.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (this._addOperationCanceled)
						{
							break;
						}
						if (directoryPathInArchive != null)
						{
							string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(text)));
							this.AddFile(text, fullPath);
						}
						else
						{
							this.AddFile(text, null);
						}
					}
					goto IL_00AD;
				}
			}
			foreach (string text2 in fileNames)
			{
				if (this._addOperationCanceled)
				{
					break;
				}
				this.AddFile(text2, directoryPathInArchive);
			}
			IL_00AD:
			if (!this._addOperationCanceled)
			{
				this.OnAddCompleted();
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B290 File Offset: 0x00009490
		public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this.OnAddStarted();
			foreach (string text in fileNames)
			{
				this.UpdateFile(text, directoryPathInArchive);
			}
			this.OnAddCompleted();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public ZipEntry UpdateFile(string fileName)
		{
			return this.UpdateFile(fileName, null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B300 File Offset: 0x00009500
		public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			if (this[text] != null)
			{
				this.RemoveEntry(text);
			}
			return this.AddFile(fileName, directoryPathInArchive);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000B32D File Offset: 0x0000952D
		public ZipEntry UpdateDirectory(string directoryName)
		{
			return this.UpdateDirectory(directoryName, null);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B337 File Offset: 0x00009537
		public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOrUpdate);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B342 File Offset: 0x00009542
		public void UpdateItem(string itemName)
		{
			this.UpdateItem(itemName, null);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B34C File Offset: 0x0000954C
		public void UpdateItem(string itemName, string directoryPathInArchive)
		{
			if (File.Exists(itemName))
			{
				this.UpdateFile(itemName, directoryPathInArchive);
				return;
			}
			if (Directory.Exists(itemName))
			{
				this.UpdateDirectory(itemName, directoryPathInArchive);
				return;
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", itemName));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B382 File Offset: 0x00009582
		public ZipEntry AddEntry(string entryName, string content)
		{
			return this.AddEntry(entryName, content, Encoding.UTF8);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B394 File Offset: 0x00009594
		public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Seek(0L, 0);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public ZipEntry AddEntry(string entryName, Stream stream)
		{
			ZipEntry zipEntry = ZipEntry.CreateForStream(entryName, stream);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B41C File Offset: 0x0000961C
		public ZipEntry AddEntry(string entryName, WriteDelegate writer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForWriter(entryName, writer);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B454 File Offset: 0x00009654
		public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForJitStreamProvider(entryName, opener, closer);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B4A0 File Offset: 0x000096A0
		private ZipEntry _InternalAddEntry(ZipEntry ze)
		{
			ze._container = new ZipContainer(this);
			ze.CompressionMethod = this.CompressionMethod;
			ze.CompressionLevel = this.CompressionLevel;
			ze.ExtractExistingFile = this.ExtractExistingFile;
			ze.ZipErrorAction = this.ZipErrorAction;
			ze.SetCompression = this.SetCompression;
			ze.AlternateEncoding = this.AlternateEncoding;
			ze.AlternateEncodingUsage = this.AlternateEncodingUsage;
			ze.Password = this._Password;
			ze.Encryption = this.Encryption;
			ze.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			ze.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			this.InternalAddEntry(ze.FileName, ze);
			this.AfterAddEntry(ze);
			return ze;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B552 File Offset: 0x00009752
		public ZipEntry UpdateEntry(string entryName, string content)
		{
			return this.UpdateEntry(entryName, content, Encoding.UTF8);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B561 File Offset: 0x00009761
		public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, content, encoding);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B573 File Offset: 0x00009773
		public ZipEntry UpdateEntry(string entryName, WriteDelegate writer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, writer);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B584 File Offset: 0x00009784
		public ZipEntry UpdateEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, opener, closer);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B596 File Offset: 0x00009796
		public ZipEntry UpdateEntry(string entryName, Stream stream)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, stream);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B5A8 File Offset: 0x000097A8
		private void RemoveEntryForUpdate(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			string text = null;
			if (entryName.IndexOf('\\') != -1)
			{
				text = Path.GetDirectoryName(entryName);
				entryName = Path.GetFileName(entryName);
			}
			string text2 = ZipEntry.NameInArchive(entryName, text);
			if (this[text2] != null)
			{
				this.RemoveEntry(text2);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B5FC File Offset: 0x000097FC
		public ZipEntry AddEntry(string entryName, byte[] byteContent)
		{
			if (byteContent == null)
			{
				throw new ArgumentException("bad argument", "byteContent");
			}
			MemoryStream memoryStream = new MemoryStream(byteContent);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B62B File Offset: 0x0000982B
		public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, byteContent);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B63C File Offset: 0x0000983C
		public ZipEntry AddDirectory(string directoryName)
		{
			return this.AddDirectory(directoryName, null);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000B646 File Offset: 0x00009846
		public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOnly);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B654 File Offset: 0x00009854
		public ZipEntry AddDirectoryByName(string directoryNameInArchive)
		{
			ZipEntry zipEntry = ZipEntry.CreateFromNothing(directoryNameInArchive);
			zipEntry._container = new ZipContainer(this);
			zipEntry.MarkAsDirectory();
			zipEntry.AlternateEncoding = this.AlternateEncoding;
			zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			zipEntry._Source = ZipEntrySource.Stream;
			this.InternalAddEntry(zipEntry.FileName, zipEntry);
			this.AfterAddEntry(zipEntry);
			return zipEntry;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B6DB File Offset: 0x000098DB
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action)
		{
			if (rootDirectoryPathInArchive == null)
			{
				rootDirectoryPathInArchive = "";
			}
			return this.AddOrUpdateDirectoryImpl(directoryName, rootDirectoryPathInArchive, action, true, 0);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B6F2 File Offset: 0x000098F2
		internal void InternalAddEntry(string name, ZipEntry entry)
		{
			this._entries.Add(name, entry);
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B710 File Offset: 0x00009910
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action, bool recurse, int level)
		{
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("{0} {1}...", (action == AddOrUpdateAction.AddOnly) ? "adding" : "Adding or updating", directoryName);
			}
			if (level == 0)
			{
				this._addOperationCanceled = false;
				this.OnAddStarted();
			}
			if (this._addOperationCanceled)
			{
				return null;
			}
			string text = rootDirectoryPathInArchive;
			ZipEntry zipEntry = null;
			if (level > 0)
			{
				int num = directoryName.Length;
				for (int i = level; i > 0; i--)
				{
					num = directoryName.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
				}
				text = directoryName.Substring(num + 1);
				text = Path.Combine(rootDirectoryPathInArchive, text);
			}
			if (level > 0 || rootDirectoryPathInArchive != "")
			{
				zipEntry = ZipEntry.CreateFromFile(directoryName, text);
				zipEntry._container = new ZipContainer(this);
				zipEntry.AlternateEncoding = this.AlternateEncoding;
				zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
				zipEntry.MarkAsDirectory();
				zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
				zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
				if (!this._entries.ContainsKey(zipEntry.FileName))
				{
					this.InternalAddEntry(zipEntry.FileName, zipEntry);
					this.AfterAddEntry(zipEntry);
				}
				text = zipEntry.FileName;
			}
			if (!this._addOperationCanceled)
			{
				string[] files = Directory.GetFiles(directoryName);
				if (recurse)
				{
					foreach (string text2 in files)
					{
						if (this._addOperationCanceled)
						{
							break;
						}
						if (action == AddOrUpdateAction.AddOnly)
						{
							this.AddFile(text2, text);
						}
						else
						{
							this.UpdateFile(text2, text);
						}
					}
					if (!this._addOperationCanceled)
					{
						string[] directories = Directory.GetDirectories(directoryName);
						foreach (string text3 in directories)
						{
							if (this.AddDirectoryWillTraverseReparsePoints)
							{
								this.AddOrUpdateDirectoryImpl(text3, rootDirectoryPathInArchive, action, recurse, level + 1);
							}
						}
					}
				}
			}
			if (level == 0)
			{
				this.OnAddCompleted();
			}
			return zipEntry;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B8DB File Offset: 0x00009ADB
		public static bool CheckZip(string zipFileName)
		{
			return ZipFile.CheckZip(zipFileName, false, null);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public static bool CheckZip(string zipFileName, bool fixIfNecessary, TextWriter writer)
		{
			ZipFile zipFile = null;
			ZipFile zipFile2 = null;
			bool flag = true;
			try
			{
				zipFile = new ZipFile();
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile2 = ZipFile.Read(zipFileName);
				foreach (ZipEntry zipEntry in zipFile)
				{
					foreach (ZipEntry zipEntry2 in zipFile2)
					{
						if (zipEntry.FileName == zipEntry2.FileName)
						{
							if (zipEntry._RelativeOffsetOfLocalHeader != zipEntry2._RelativeOffsetOfLocalHeader)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in RelativeOffsetOfLocalHeader  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._RelativeOffsetOfLocalHeader, zipEntry2._RelativeOffsetOfLocalHeader);
								}
							}
							if (zipEntry._CompressedSize != zipEntry2._CompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._CompressedSize, zipEntry2._CompressedSize);
								}
							}
							if (zipEntry._UncompressedSize != zipEntry2._UncompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in UncompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._UncompressedSize, zipEntry2._UncompressedSize);
								}
							}
							if (zipEntry.CompressionMethod != zipEntry2.CompressionMethod)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressionMethod  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.CompressionMethod, zipEntry2.CompressionMethod);
								}
							}
							if (zipEntry.Crc == zipEntry2.Crc)
							{
								break;
							}
							flag = false;
							if (writer != null)
							{
								writer.WriteLine("{0}: mismatch in Crc32  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.Crc, zipEntry2.Crc);
								break;
							}
							break;
						}
					}
				}
				zipFile2.Dispose();
				zipFile2 = null;
				if (!flag && fixIfNecessary)
				{
					string text = Path.GetFileNameWithoutExtension(zipFileName);
					text = string.Format("{0}_fixed.zip", text);
					zipFile.Save(text);
				}
			}
			finally
			{
				if (zipFile != null)
				{
					zipFile.Dispose();
				}
				if (zipFile2 != null)
				{
					zipFile2.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000BB48 File Offset: 0x00009D48
		public static void FixZipDirectory(string zipFileName)
		{
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile.Save(zipFileName);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000BB8C File Offset: 0x00009D8C
		public static bool CheckZipPassword(string zipFileName, string password)
		{
			bool flag = false;
			try
			{
				using (ZipFile zipFile = ZipFile.Read(zipFileName))
				{
					foreach (ZipEntry zipEntry in zipFile)
					{
						if (!zipEntry.IsDirectory && zipEntry.UsesEncryption)
						{
							zipEntry.ExtractWithPassword(Stream.Null, password);
						}
					}
				}
				flag = true;
			}
			catch (BadPasswordException)
			{
			}
			return flag;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000BC20 File Offset: 0x00009E20
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipFile: {0}\n", this.Name));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("          Comment: {0}\n", this._Comment));
				}
				if (this._versionMadeBy != 0)
				{
					stringBuilder.Append(string.Format("  version made by: 0x{0:X4}\n", this._versionMadeBy));
				}
				if (this._versionNeededToExtract != 0)
				{
					stringBuilder.Append(string.Format("needed to extract: 0x{0:X4}\n", this._versionNeededToExtract));
				}
				stringBuilder.Append(string.Format("       uses ZIP64: {0}\n", this.InputUsesZip64));
				stringBuilder.Append(string.Format("     disk with CD: {0}\n", this._diskNumberWithCd));
				if (this._OffsetOfCentralDirectory == 4294967295U)
				{
					stringBuilder.Append(string.Format("      CD64 offset: 0x{0:X16}\n", this._OffsetOfCentralDirectory64));
				}
				else
				{
					stringBuilder.Append(string.Format("        CD offset: 0x{0:X8}\n", this._OffsetOfCentralDirectory));
				}
				stringBuilder.Append("\n");
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					stringBuilder.Append(zipEntry.Info);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000BD94 File Offset: 0x00009F94
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public bool FullScan { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000BDA5 File Offset: 0x00009FA5
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000BDAD File Offset: 0x00009FAD
		public bool SortEntriesBeforeSaving { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000BDB6 File Offset: 0x00009FB6
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000BDBE File Offset: 0x00009FBE
		public bool AddDirectoryWillTraverseReparsePoints { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000BDC7 File Offset: 0x00009FC7
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000BDCF File Offset: 0x00009FCF
		public int BufferSize
		{
			get
			{
				return this._BufferSize;
			}
			set
			{
				this._BufferSize = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		public int CodecBufferSize { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000BDE9 File Offset: 0x00009FE9
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000BDF1 File Offset: 0x00009FF1
		public bool FlattenFoldersOnExtract { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000BDFA File Offset: 0x00009FFA
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000BE02 File Offset: 0x0000A002
		public CompressionStrategy Strategy
		{
			get
			{
				return this._Strategy;
			}
			set
			{
				this._Strategy = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BE0B File Offset: 0x0000A00B
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000BE13 File Offset: 0x0000A013
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000BE1C File Offset: 0x0000A01C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000BE24 File Offset: 0x0000A024
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000BE2D File Offset: 0x0000A02D
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000BE35 File Offset: 0x0000A035
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this._compressionMethod;
			}
			set
			{
				this._compressionMethod = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000BE3E File Offset: 0x0000A03E
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000BE46 File Offset: 0x0000A046
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._contentsChanged = true;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000BE56 File Offset: 0x0000A056
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000BE5E File Offset: 0x0000A05E
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000BE67 File Offset: 0x0000A067
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000BE6F File Offset: 0x0000A06F
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000BE78 File Offset: 0x0000A078
		internal bool Verbose
		{
			get
			{
				return this._StatusMessageTextWriter != null;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BE86 File Offset: 0x0000A086
		public bool ContainsEntry(string name)
		{
			return this._entries.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000BE99 File Offset: 0x0000A099
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		public bool CaseSensitiveRetrieval
		{
			get
			{
				return this._CaseSensitiveRetrieval;
			}
			set
			{
				if (value != this._CaseSensitiveRetrieval)
				{
					this._CaseSensitiveRetrieval = value;
					this._initEntriesDictionary();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.GetEncoding("UTF-8") && this._alternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.GetEncoding("UTF-8");
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipFile.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000BF07 File Offset: 0x0000A107
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000BF0F File Offset: 0x0000A10F
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				return this._zip64;
			}
			set
			{
				this._zip64 = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000BF18 File Offset: 0x0000A118
		public bool? RequiresZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				if (!this._hasBeenSaved || this._contentsChanged)
				{
					return default(bool?);
				}
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (zipEntry.RequiresZip64.Value)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public bool? InputUsesZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				foreach (ZipEntry zipEntry in this)
				{
					if (zipEntry.Source != ZipEntrySource.ZipFile)
					{
						return default(bool?);
					}
					if (zipEntry._InputUsesZip64)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000C050 File Offset: 0x0000A250
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000C063 File Offset: 0x0000A263
		[Obsolete("use AlternateEncoding instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000C073 File Offset: 0x0000A273
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000C07B File Offset: 0x0000A27B
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000C084 File Offset: 0x0000A284
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000C08C File Offset: 0x0000A28C
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000C095 File Offset: 0x0000A295
		public static Encoding DefaultEncoding
		{
			get
			{
				return ZipFile._defaultEncoding;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000C09C File Offset: 0x0000A29C
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public TextWriter StatusMessageTextWriter
		{
			get
			{
				return this._StatusMessageTextWriter;
			}
			set
			{
				this._StatusMessageTextWriter = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		public string TempFileFolder
		{
			get
			{
				return this._TempFileFolder;
			}
			set
			{
				this._TempFileFolder = value;
				if (value == null)
				{
					return;
				}
				if (!Directory.Exists(value))
				{
					throw new FileNotFoundException(string.Format("That directory ({0}) does not exist.", value));
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000C103 File Offset: 0x0000A303
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this.Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this.Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C10B File Offset: 0x0000A30B
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C113 File Offset: 0x0000A313
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000C11C File Offset: 0x0000A31C
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000C133 File Offset: 0x0000A333
		public ZipErrorAction ZipErrorAction
		{
			get
			{
				if (this.ZipError != null)
				{
					this._zipErrorAction = ZipErrorAction.InvokeErrorEvent;
				}
				return this._zipErrorAction;
			}
			set
			{
				this._zipErrorAction = value;
				if (this._zipErrorAction != ZipErrorAction.InvokeErrorEvent && this.ZipError != null)
				{
					this.ZipError = null;
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C154 File Offset: 0x0000A354
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000C15C File Offset: 0x0000A35C
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000C17C File Offset: 0x0000A37C
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C185 File Offset: 0x0000A385
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000C18D File Offset: 0x0000A38D
		public int MaxOutputSegmentSize
		{
			get
			{
				return this._maxOutputSegmentSize;
			}
			set
			{
				if (value < 65536 && value != 0)
				{
					throw new ZipException("The minimum acceptable segment size is 65536.");
				}
				this._maxOutputSegmentSize = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public int NumberOfSegmentsForMostRecentSave
		{
			get
			{
				return (int)(this._numberOfSegmentsForMostRecentSave + 1U);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000C1B6 File Offset: 0x0000A3B6
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0L && value != -1L && value < 65536L)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateThreshold should be -1, 0, or > 65536");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C1ED File Offset: 0x0000A3ED
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C20A File Offset: 0x0000A40A
		public override string ToString()
		{
			return string.Format("ZipFile::{0}", this.Name);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000C21C File Offset: 0x0000A41C
		public static Version LibraryVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C22D File Offset: 0x0000A42D
		internal void NotifyEntryChanged()
		{
			this._contentsChanged = true;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C236 File Offset: 0x0000A436
		internal Stream StreamForDiskNumber(uint diskNumber)
		{
			if (diskNumber + 1U == this._diskNumberWithCd || (diskNumber == 0U && this._diskNumberWithCd == 0U))
			{
				return this.ReadStream;
			}
			return ZipSegmentedStream.ForReading(this._readName ?? this._name, diskNumber, this._diskNumberWithCd);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C274 File Offset: 0x0000A474
		internal void Reset(bool whileSaving)
		{
			if (this._JustSaved)
			{
				using (ZipFile zipFile = new ZipFile())
				{
					zipFile._readName = (zipFile._name = (whileSaving ? (this._readName ?? this._name) : this._name));
					zipFile.AlternateEncoding = this.AlternateEncoding;
					zipFile.AlternateEncodingUsage = this.AlternateEncodingUsage;
					ZipFile.ReadIntoInstance(zipFile);
					foreach (ZipEntry zipEntry in zipFile)
					{
						foreach (ZipEntry zipEntry2 in this)
						{
							if (zipEntry.FileName == zipEntry2.FileName)
							{
								zipEntry2.CopyMetaData(zipEntry);
								break;
							}
						}
					}
				}
				this._JustSaved = false;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C384 File Offset: 0x0000A584
		public ZipFile(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("Could not read {0} as a zip file", fileName), ex);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C414 File Offset: 0x0000A614
		public ZipFile(string fileName, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		public ZipFile()
		{
			this._InitInstance(null, null);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C520 File Offset: 0x0000A720
		public ZipFile(Encoding encoding)
		{
			this.AlternateEncoding = encoding;
			this.AlternateEncodingUsage = ZipOption.Always;
			this._InitInstance(null, null);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C598 File Offset: 0x0000A798
		public ZipFile(string fileName, TextWriter statusMessageWriter)
		{
			try
			{
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C628 File Offset: 0x0000A828
		public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public void Initialize(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C704 File Offset: 0x0000A904
		private void _initEntriesDictionary()
		{
			StringComparer stringComparer = (this.CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			this._entries = ((this._entries == null) ? new Dictionary<string, ZipEntry>(stringComparer) : new Dictionary<string, ZipEntry>(this._entries, stringComparer));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C748 File Offset: 0x0000A948
		private void _InitInstance(string zipFileName, TextWriter statusMessageWriter)
		{
			this._name = zipFileName;
			this._StatusMessageTextWriter = statusMessageWriter;
			this._contentsChanged = true;
			this.AddDirectoryWillTraverseReparsePoints = true;
			this.CompressionLevel = CompressionLevel.Default;
			this.ParallelDeflateThreshold = 524288L;
			this._initEntriesDictionary();
			if (File.Exists(this._name))
			{
				if (this.FullScan)
				{
					ZipFile.ReadIntoInstance_Orig(this);
				}
				else
				{
					ZipFile.ReadIntoInstance(this);
				}
				this._fileAlreadyExists = true;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		private List<ZipEntry> ZipEntriesAsList
		{
			get
			{
				if (this._zipEntriesAsList == null)
				{
					this._zipEntriesAsList = new List<ZipEntry>(this._entries.Values);
				}
				return this._zipEntriesAsList;
			}
		}

		// Token: 0x17000088 RID: 136
		public ZipEntry this[int ix]
		{
			get
			{
				return this.ZipEntriesAsList[ix];
			}
		}

		// Token: 0x17000089 RID: 137
		public ZipEntry this[string fileName]
		{
			get
			{
				string text = SharedUtilities.NormalizePathForUseInZipFile(fileName);
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				text = text.Replace("/", "\\");
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				return null;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000C844 File Offset: 0x0000AA44
		public ICollection<string> EntryFileNames
		{
			get
			{
				return this._entries.Keys;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000C851 File Offset: 0x0000AA51
		public ICollection<ZipEntry> Entries
		{
			get
			{
				return this._entries.Values;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C860 File Offset: 0x0000AA60
		public ICollection<ZipEntry> EntriesSorted
		{
			get
			{
				List<ZipEntry> list = new List<ZipEntry>();
				foreach (ZipEntry zipEntry in this.Entries)
				{
					list.Add(zipEntry);
				}
				bool caseSensitiveRetrieval = this.CaseSensitiveRetrieval;
				return list.AsReadOnly();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C8CD File Offset: 0x0000AACD
		public void RemoveEntry(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this._entries.Remove(SharedUtilities.NormalizePathForUseInZipFile(entry.FileName));
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C904 File Offset: 0x0000AB04
		public void RemoveEntry(string fileName)
		{
			string text = ZipEntry.NameInArchive(fileName, null);
			ZipEntry zipEntry = this[text];
			if (zipEntry == null)
			{
				throw new ArgumentException("The entry you specified was not found in the zip archive.");
			}
			this.RemoveEntry(zipEntry);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C936 File Offset: 0x0000AB36
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C948 File Offset: 0x0000AB48
		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (!this._disposed)
			{
				if (disposeManagedResources)
				{
					if (this._ReadStreamIsOurs && this._readstream != null)
					{
						this._readstream.Dispose();
						this._readstream = null;
					}
					if (this._temporaryFileName != null && this._name != null && this._writestream != null)
					{
						this._writestream.Dispose();
						this._writestream = null;
					}
					if (this.ParallelDeflater != null)
					{
						this.ParallelDeflater.Dispose();
						this.ParallelDeflater = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		internal Stream ReadStream
		{
			get
			{
				if (this._readstream == null && (this._readName != null || this._name != null))
				{
					this._readstream = File.Open(this._readName ?? this._name, 3, 1, 3);
					this._ReadStreamIsOurs = true;
				}
				return this._readstream;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000CA20 File Offset: 0x0000AC20
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		private Stream WriteStream
		{
			get
			{
				if (this._writestream != null)
				{
					return this._writestream;
				}
				if (this._name == null)
				{
					return this._writestream;
				}
				if (this._maxOutputSegmentSize != 0)
				{
					this._writestream = ZipSegmentedStream.ForWriting(this._name, this._maxOutputSegmentSize);
					return this._writestream;
				}
				SharedUtilities.CreateAndOpenUniqueTempFile(this.TempFileFolder ?? Path.GetDirectoryName(this._name), out this._writestream, out this._temporaryFileName);
				return this._writestream;
			}
			set
			{
				if (value != null)
				{
					throw new ZipException("Cannot set the stream to a non-null value.");
				}
				this._writestream = null;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		private string ArchiveNameForEvent
		{
			get
			{
				if (this._name == null)
				{
					return "(stream)";
				}
				return this._name;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600022F RID: 559 RVA: 0x0000CACC File Offset: 0x0000ACCC
		// (remove) Token: 0x06000230 RID: 560 RVA: 0x0000CB04 File Offset: 0x0000AD04
		public event EventHandler<SaveProgressEventArgs> SaveProgress;

		// Token: 0x06000231 RID: 561 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		internal bool OnSaveBlock(ZipEntry entry, long bytesXferred, long totalBytesToXfer)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesXferred, totalBytesToXfer);
				saveProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private void OnSaveEntry(int current, ZipEntry entry, bool before)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = new SaveProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, entry);
				saveProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		private void OnSaveEvent(ZipProgressEventType eventFlavor)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = new SaveProgressEventArgs(this.ArchiveNameForEvent, eventFlavor);
				saveProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000CC04 File Offset: 0x0000AE04
		private void OnSaveStarted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.Started(this.ArchiveNameForEvent);
				saveProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CC40 File Offset: 0x0000AE40
		private void OnSaveCompleted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.Completed(this.ArchiveNameForEvent);
				saveProgress.Invoke(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000236 RID: 566 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		// (remove) Token: 0x06000237 RID: 567 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public event EventHandler<ReadProgressEventArgs> ReadProgress;

		// Token: 0x06000238 RID: 568 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		private void OnReadStarted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Started(this.ArchiveNameForEvent);
				readProgress.Invoke(this, e);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CD08 File Offset: 0x0000AF08
		private void OnReadCompleted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Completed(this.ArchiveNameForEvent);
				readProgress.Invoke(this, e);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000CD34 File Offset: 0x0000AF34
		internal void OnReadBytes(ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, this.ReadStream.Position, this.LengthOfReadStream);
				readProgress.Invoke(this, e);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CD74 File Offset: 0x0000AF74
		internal void OnReadEntry(bool before, ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = (before ? ReadProgressEventArgs.Before(this.ArchiveNameForEvent, this._entries.Count) : ReadProgressEventArgs.After(this.ArchiveNameForEvent, entry, this._entries.Count));
				readProgress.Invoke(this, e);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
		private long LengthOfReadStream
		{
			get
			{
				if (this._lengthOfReadStream == -99L)
				{
					this._lengthOfReadStream = (this._ReadStreamIsOurs ? SharedUtilities.GetFileLength(this._name) : (-1L));
				}
				return this._lengthOfReadStream;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600023D RID: 573 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		// (remove) Token: 0x0600023E RID: 574 RVA: 0x0000CE30 File Offset: 0x0000B030
		public event EventHandler<ExtractProgressEventArgs> ExtractProgress;

		// Token: 0x0600023F RID: 575 RVA: 0x0000CE68 File Offset: 0x0000B068
		private void OnExtractEntry(int current, bool before, ZipEntry currentEntry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = new ExtractProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, currentEntry, path);
				extractProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		internal bool OnExtractBlock(ZipEntry entry, long bytesWritten, long totalBytesToWrite)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesWritten, totalBytesToWrite);
				extractProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		internal bool OnSingleEntryExtract(ZipEntry entry, string path, bool before)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = (before ? ExtractProgressEventArgs.BeforeExtractEntry(this.ArchiveNameForEvent, entry, path) : ExtractProgressEventArgs.AfterExtractEntry(this.ArchiveNameForEvent, entry, path));
				extractProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000CF4C File Offset: 0x0000B14C
		internal bool OnExtractExisting(ZipEntry entry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractExisting(this.ArchiveNameForEvent, entry, path);
				extractProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000CF90 File Offset: 0x0000B190
		private void OnExtractAllCompleted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllCompleted(this.ArchiveNameForEvent, path);
				extractProgress.Invoke(this, e);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		private void OnExtractAllStarted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllStarted(this.ArchiveNameForEvent, path);
				extractProgress.Invoke(this, e);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000245 RID: 581 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		// (remove) Token: 0x06000246 RID: 582 RVA: 0x0000D020 File Offset: 0x0000B220
		public event EventHandler<AddProgressEventArgs> AddProgress;

		// Token: 0x06000247 RID: 583 RVA: 0x0000D058 File Offset: 0x0000B258
		private void OnAddStarted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.Started(this.ArchiveNameForEvent);
				addProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D094 File Offset: 0x0000B294
		private void OnAddCompleted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.Completed(this.ArchiveNameForEvent);
				addProgress.Invoke(this, e);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		internal void AfterAddEntry(ZipEntry entry)
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.AfterEntry(this.ArchiveNameForEvent, entry, this._entries.Count);
				addProgress.Invoke(this, e);
				if (e.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600024A RID: 586 RVA: 0x0000D108 File Offset: 0x0000B308
		// (remove) Token: 0x0600024B RID: 587 RVA: 0x0000D140 File Offset: 0x0000B340
		public event EventHandler<ZipErrorEventArgs> ZipError;

		// Token: 0x0600024C RID: 588 RVA: 0x0000D178 File Offset: 0x0000B378
		internal bool OnZipErrorSaving(ZipEntry entry, Exception exc)
		{
			if (this.ZipError != null)
			{
				lock (this.LOCK)
				{
					ZipErrorEventArgs e = ZipErrorEventArgs.Saving(this.Name, entry, exc);
					this.ZipError.Invoke(this, e);
					if (e.Cancel)
					{
						this._saveOperationCanceled = true;
					}
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public void ExtractAll(string path)
		{
			this._InternalExtractAll(path, true);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this._InternalExtractAll(path, true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D200 File Offset: 0x0000B400
		private void _InternalExtractAll(string path, bool overrideExtractExistingProperty)
		{
			bool flag = this.Verbose;
			this._inExtractAll = true;
			try
			{
				this.OnExtractAllStarted(path);
				int num = 0;
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (flag)
					{
						this.StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", new object[] { "Name", "Modified", "Size", "Ratio", "Packed" });
						this.StatusMessageTextWriter.WriteLine(new string('-', 72));
						flag = false;
					}
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", new object[]
						{
							zipEntry.FileName,
							zipEntry.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
							zipEntry.UncompressedSize,
							zipEntry.CompressionRatio,
							zipEntry.CompressedSize
						});
						if (!string.IsNullOrEmpty(zipEntry.Comment))
						{
							this.StatusMessageTextWriter.WriteLine("  Comment: {0}", zipEntry.Comment);
						}
					}
					zipEntry.Password = this._Password;
					this.OnExtractEntry(num, true, zipEntry, path);
					if (overrideExtractExistingProperty)
					{
						zipEntry.ExtractExistingFile = this.ExtractExistingFile;
					}
					zipEntry.Extract(path);
					num++;
					this.OnExtractEntry(num, false, zipEntry, path);
					if (this._extractOperationCanceled)
					{
						break;
					}
				}
				if (!this._extractOperationCanceled)
				{
					foreach (ZipEntry zipEntry2 in this._entries.Values)
					{
						if (zipEntry2.IsDirectory || zipEntry2.FileName.EndsWith("/"))
						{
							string text = (zipEntry2.FileName.StartsWith("/") ? Path.Combine(path, zipEntry2.FileName.Substring(1)) : Path.Combine(path, zipEntry2.FileName));
							zipEntry2._SetTimes(text, false);
						}
					}
					this.OnExtractAllCompleted(path);
				}
			}
			finally
			{
				this._inExtractAll = false;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D48C File Offset: 0x0000B68C
		public static ZipFile Read(string fileName)
		{
			return ZipFile.Read(fileName, null, null, null);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D497 File Offset: 0x0000B697
		public static ZipFile Read(string fileName, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(fileName, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		private static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			ZipFile zipFile = new ZipFile();
			zipFile.AlternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile.AlternateEncodingUsage = ZipOption.Always;
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._name = fileName;
			if (readProgress != null)
			{
				zipFile.ReadProgress = readProgress;
			}
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from {0}...", fileName);
			}
			ZipFile.ReadIntoInstance(zipFile);
			zipFile._fileAlreadyExists = true;
			return zipFile;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000D529 File Offset: 0x0000B729
		public static ZipFile Read(Stream zipStream)
		{
			return ZipFile.Read(zipStream, null, null, null);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D534 File Offset: 0x0000B734
		public static ZipFile Read(Stream zipStream, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(zipStream, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D55C File Offset: 0x0000B75C
		private static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			if (zipStream == null)
			{
				throw new ArgumentNullException("zipStream");
			}
			ZipFile zipFile = new ZipFile();
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._alternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile._alternateEncodingUsage = ZipOption.Always;
			if (readProgress != null)
			{
				zipFile.ReadProgress += readProgress;
			}
			zipFile._readstream = ((zipStream.Position == 0L) ? zipStream : new OffsetStream(zipStream));
			zipFile._ReadStreamIsOurs = false;
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from stream...");
			}
			ZipFile.ReadIntoInstance(zipFile);
			return zipFile;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		private static void ReadIntoInstance(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			try
			{
				zf._readName = zf._name;
				if (!readStream.CanSeek)
				{
					ZipFile.ReadIntoInstance_Orig(zf);
					return;
				}
				zf.OnReadStarted();
				uint num = ZipFile.ReadFirstFourBytes(readStream);
				if (num == 101010256U)
				{
					return;
				}
				int num2 = 0;
				bool flag = false;
				long num3 = readStream.Length - 64L;
				long num4 = Math.Max(readStream.Length - 16384L, 10L);
				do
				{
					if (num3 < 0L)
					{
						num3 = 0L;
					}
					readStream.Seek(num3, 0);
					long num5 = SharedUtilities.FindSignature(readStream, 101010256);
					if (num5 != -1L)
					{
						flag = true;
					}
					else
					{
						if (num3 == 0L)
						{
							break;
						}
						num2++;
						num3 -= (long)(32 * (num2 + 1) * num2);
					}
				}
				while (!flag && num3 > num4);
				if (flag)
				{
					zf._locEndOfCDS = readStream.Position - 4L;
					byte[] array = new byte[16];
					readStream.Read(array, 0, array.Length);
					zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
					if (zf._diskNumberWithCd == 65535U)
					{
						throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
					}
					zf._diskNumberWithCd += 1U;
					int num6 = 12;
					uint num7 = BitConverter.ToUInt32(array, num6);
					if (num7 == 4294967295U)
					{
						ZipFile.Zip64SeekToCentralDirectory(zf);
					}
					else
					{
						zf._OffsetOfCentralDirectory = num7;
						readStream.Seek((long)((ulong)num7), 0);
					}
					ZipFile.ReadCentralDirectory(zf);
				}
				else
				{
					readStream.Seek(0L, 0);
					ZipFile.ReadIntoInstance_Orig(zf);
				}
			}
			catch (Exception ex)
			{
				if (zf._ReadStreamIsOurs && zf._readstream != null)
				{
					zf._readstream.Dispose();
					zf._readstream = null;
				}
				throw new ZipException("Cannot read that as a ZipFile", ex);
			}
			zf._contentsChanged = false;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		private static void Zip64SeekToCentralDirectory(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			byte[] array = new byte[16];
			readStream.Seek(-40L, 1);
			readStream.Read(array, 0, 16);
			long num = BitConverter.ToInt64(array, 8);
			zf._OffsetOfCentralDirectory = uint.MaxValue;
			zf._OffsetOfCentralDirectory64 = num;
			readStream.Seek(num, 0);
			uint num2 = (uint)SharedUtilities.ReadInt(readStream);
			if (num2 != 101075792U)
			{
				throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) looking for ZIP64 EoCD Record at position 0x{1:X8}", num2, readStream.Position));
			}
			readStream.Read(array, 0, 8);
			long num3 = BitConverter.ToInt64(array, 0);
			array = new byte[num3];
			readStream.Read(array, 0, array.Length);
			num = BitConverter.ToInt64(array, 36);
			readStream.Seek(num, 0);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D860 File Offset: 0x0000BA60
		private static uint ReadFirstFourBytes(Stream s)
		{
			return (uint)SharedUtilities.ReadInt(s);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D878 File Offset: 0x0000BA78
		private static void ReadCentralDirectory(ZipFile zf)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
			{
				zipEntry.ResetDirEntry();
				zf.OnReadEntry(true, null);
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				if (zipEntry._InputUsesZip64)
				{
					flag = true;
				}
				dictionary.Add(zipEntry.FileName, null);
			}
			if (flag)
			{
				zf.UseZip64WhenSaving = Zip64Option.Always;
			}
			if (zf._locEndOfCDS > 0L)
			{
				zf.ReadStream.Seek(zf._locEndOfCDS, 0);
			}
			ZipFile.ReadCentralDirectoryFooter(zf);
			if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
			{
				zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
			}
			if (zf.Verbose)
			{
				zf.StatusMessageTextWriter.WriteLine("read in {0} entries.", zf._entries.Count);
			}
			zf.OnReadCompleted();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D978 File Offset: 0x0000BB78
		private static void ReadIntoInstance_Orig(ZipFile zf)
		{
			zf.OnReadStarted();
			zf._entries = new Dictionary<string, ZipEntry>();
			if (zf.Verbose)
			{
				if (zf.Name == null)
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
				}
				else
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zf.Name);
				}
			}
			bool flag = true;
			ZipContainer zipContainer = new ZipContainer(zf);
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadEntry(zipContainer, flag)) != null)
			{
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				flag = false;
			}
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ZipEntry zipEntry2;
				while ((zipEntry2 = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
				{
					ZipEntry zipEntry3 = zf._entries[zipEntry2.FileName];
					if (zipEntry3 != null)
					{
						zipEntry3._Comment = zipEntry2.Comment;
						if (zipEntry2.IsDirectory)
						{
							zipEntry3.MarkAsDirectory();
						}
					}
					dictionary.Add(zipEntry2.FileName, null);
				}
				if (zf._locEndOfCDS > 0L)
				{
					zf.ReadStream.Seek(zf._locEndOfCDS, 0);
				}
				ZipFile.ReadCentralDirectoryFooter(zf);
				if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
				{
					zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
				}
			}
			catch (ZipException)
			{
			}
			catch (IOException)
			{
			}
			zf.OnReadCompleted();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		private static void ReadCentralDirectoryFooter(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			int num = SharedUtilities.ReadSignature(readStream);
			int num2 = 0;
			byte[] array;
			if ((long)num == 101075792L)
			{
				array = new byte[52];
				readStream.Read(array, 0, array.Length);
				long num3 = BitConverter.ToInt64(array, 0);
				if (num3 < 44L)
				{
					throw new ZipException("Bad size in the ZIP64 Central Directory.");
				}
				zf._versionMadeBy = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._versionNeededToExtract = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._diskNumberWithCd = BitConverter.ToUInt32(array, num2);
				num2 += 2;
				array = new byte[num3 - 44L];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
				if ((long)num != 117853008L)
				{
					throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
				}
				array = new byte[16];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
			}
			if ((long)num != 101010256L)
			{
				readStream.Seek(-4L, 1);
				throw new BadReadException(string.Format("Bad signature ({0:X8}) at position 0x{1:X8}", num, readStream.Position));
			}
			array = new byte[16];
			zf.ReadStream.Read(array, 0, array.Length);
			if (zf._diskNumberWithCd == 0U)
			{
				zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
			}
			ZipFile.ReadZipFileComment(zf);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		private static void ReadZipFileComment(ZipFile zf)
		{
			byte[] array = new byte[2];
			zf.ReadStream.Read(array, 0, array.Length);
			short num = (short)((int)array[0] + (int)array[1] * 256);
			if (num > 0)
			{
				array = new byte[(int)num];
				zf.ReadStream.Read(array, 0, array.Length);
				string @string = zf.AlternateEncoding.GetString(array, 0, array.Length);
				zf.Comment = @string;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public static bool IsZipFile(string fileName)
		{
			return ZipFile.IsZipFile(fileName, false);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		public static bool IsZipFile(string fileName, bool testExtract)
		{
			bool flag = false;
			try
			{
				if (!File.Exists(fileName))
				{
					return false;
				}
				using (FileStream fileStream = File.Open(fileName, 3, 1, 3))
				{
					flag = ZipFile.IsZipFile(fileStream, testExtract);
				}
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public static bool IsZipFile(Stream stream, bool testExtract)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag = false;
			try
			{
				if (!stream.CanRead)
				{
					return false;
				}
				Stream @null = Stream.Null;
				using (ZipFile zipFile = ZipFile.Read(stream, null, null, null))
				{
					if (testExtract)
					{
						foreach (ZipEntry zipEntry in zipFile)
						{
							if (!zipEntry.IsDirectory)
							{
								zipEntry.Extract(@null);
							}
						}
					}
				}
				flag = true;
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		private void DeleteFileWithRetry(string filename)
		{
			bool flag = false;
			int num = 3;
			int num2 = 0;
			while (num2 < num && !flag)
			{
				try
				{
					File.Delete(filename);
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("************************************************** Retry delete.");
					Thread.Sleep(200 + num2 * 200);
				}
				num2++;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public void Save()
		{
			try
			{
				bool flag = false;
				this._saveOperationCanceled = false;
				this._numberOfSegmentsForMostRecentSave = 0U;
				this.OnSaveStarted();
				if (this.WriteStream == null)
				{
					throw new BadStateException("You haven't specified where to save the zip.");
				}
				if (this._name != null && this._name.EndsWith(".exe") && !this._SavingSfx)
				{
					throw new BadStateException("You specified an EXE for a plain zip file.");
				}
				if (!this._contentsChanged)
				{
					this.OnSaveCompleted();
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("No save is necessary....");
					}
				}
				else
				{
					this.Reset(true);
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("saving....");
					}
					if (this._entries.Count >= 65535 && this._zip64 == Zip64Option.Default)
					{
						throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
					}
					int num = 0;
					ICollection<ZipEntry> collection = (this.SortEntriesBeforeSaving ? this.EntriesSorted : this.Entries);
					foreach (ZipEntry zipEntry in collection)
					{
						this.OnSaveEntry(num, zipEntry, true);
						zipEntry.Write(this.WriteStream);
						if (this._saveOperationCanceled)
						{
							break;
						}
						num++;
						this.OnSaveEntry(num, zipEntry, false);
						if (this._saveOperationCanceled)
						{
							break;
						}
						if (zipEntry.IncludedInMostRecentSave)
						{
							flag |= zipEntry.OutputUsedZip64.Value;
						}
					}
					if (!this._saveOperationCanceled)
					{
						ZipSegmentedStream zipSegmentedStream = this.WriteStream as ZipSegmentedStream;
						this._numberOfSegmentsForMostRecentSave = ((zipSegmentedStream != null) ? zipSegmentedStream.CurrentSegment : 1U);
						bool flag2 = ZipOutput.WriteCentralDirectoryStructure(this.WriteStream, collection, this._numberOfSegmentsForMostRecentSave, this._zip64, this.Comment, new ZipContainer(this));
						this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
						this._hasBeenSaved = true;
						this._contentsChanged = false;
						flag = flag || flag2;
						this._OutputUsesZip64 = new bool?(flag);
						if (this._name != null && (this._temporaryFileName != null || zipSegmentedStream != null))
						{
							this.WriteStream.Dispose();
							if (this._saveOperationCanceled)
							{
								return;
							}
							if (this._fileAlreadyExists && this._readstream != null)
							{
								this._readstream.Close();
								this._readstream = null;
								foreach (ZipEntry zipEntry2 in collection)
								{
									ZipSegmentedStream zipSegmentedStream2 = zipEntry2._archiveStream as ZipSegmentedStream;
									if (zipSegmentedStream2 != null)
									{
										zipSegmentedStream2.Dispose();
									}
									zipEntry2._archiveStream = null;
								}
							}
							string text = null;
							if (File.Exists(this._name))
							{
								text = this._name + "." + SharedUtilities.GenerateRandomStringImpl(8, 0) + ".tmp";
								if (File.Exists(text))
								{
									this.DeleteFileWithRetry(text);
								}
								File.Move(this._name, text);
							}
							this.OnSaveEvent(ZipProgressEventType.Saving_BeforeRenameTempArchive);
							File.Move((zipSegmentedStream != null) ? zipSegmentedStream.CurrentTempName : this._temporaryFileName, this._name);
							this.OnSaveEvent(ZipProgressEventType.Saving_AfterRenameTempArchive);
							if (text != null)
							{
								try
								{
									if (File.Exists(text))
									{
										File.Delete(text);
									}
								}
								catch
								{
								}
							}
							this._fileAlreadyExists = true;
						}
						ZipFile.NotifyEntriesSaveComplete(collection);
						this.OnSaveCompleted();
						this._JustSaved = true;
					}
				}
			}
			finally
			{
				this.CleanupAfterSaveOperation();
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		private static void NotifyEntriesSaveComplete(ICollection<ZipEntry> c)
		{
			foreach (ZipEntry zipEntry in c)
			{
				zipEntry.NotifySaveComplete();
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E208 File Offset: 0x0000C408
		private void RemoveTempFile()
		{
			try
			{
				if (File.Exists(this._temporaryFileName))
				{
					File.Delete(this._temporaryFileName);
				}
			}
			catch (IOException ex)
			{
				if (this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E260 File Offset: 0x0000C460
		private void CleanupAfterSaveOperation()
		{
			if (this._name != null)
			{
				if (this._writestream != null)
				{
					try
					{
						this._writestream.Dispose();
					}
					catch (IOException)
					{
					}
				}
				this._writestream = null;
				if (this._temporaryFileName != null)
				{
					this.RemoveTempFile();
					this._temporaryFileName = null;
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public void Save(string fileName)
		{
			if (this._name == null)
			{
				this._writestream = null;
			}
			else
			{
				this._readName = this._name;
			}
			this._name = fileName;
			if (Directory.Exists(this._name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this.Save();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E334 File Offset: 0x0000C534
		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (!outputStream.CanWrite)
			{
				throw new ArgumentException("Must be a writable stream.", "outputStream");
			}
			this._name = null;
			this._writestream = new CountingStream(outputStream);
			this._contentsChanged = true;
			this._fileAlreadyExists = false;
			this.Save();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E390 File Offset: 0x0000C590
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
		{
			this.SaveSelfExtractor(exeToGenerate, new SelfExtractorSaveOptions
			{
				Flavor = flavor
			});
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			if (this._name == null)
			{
				this._writestream = null;
			}
			this._SavingSfx = true;
			this._name = exeToGenerate;
			if (Directory.Exists(this._name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this._SaveSfxStub(exeToGenerate, options);
			this.Save();
			this._SavingSfx = false;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E434 File Offset: 0x0000C634
		private static void ExtractResourceToFile(Assembly a, string resourceName, string filename)
		{
			byte[] array = new byte[1024];
			using (Stream manifestResourceStream = a.GetManifestResourceStream(resourceName))
			{
				if (manifestResourceStream == null)
				{
					throw new ZipException(string.Format("missing resource '{0}'", resourceName));
				}
				using (FileStream fileStream = File.OpenWrite(filename))
				{
					int num;
					do
					{
						num = manifestResourceStream.Read(array, 0, array.Length);
						fileStream.Write(array, 0, num);
					}
					while (num > 0);
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		private void _SaveSfxStub(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				if (File.Exists(exeToGenerate) && this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", exeToGenerate);
				}
				if (!exeToGenerate.EndsWith(".exe") && this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
				}
				text3 = this.TempFileFolder ?? Path.GetDirectoryName(exeToGenerate);
				text = ZipFile.GenerateTempPathname(text3, "exe");
				Assembly assembly = typeof(ZipFile).Assembly;
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("CompilerVersion", "v2.0");
				using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(dictionary))
				{
					ZipFile.ExtractorSettings extractorSettings = null;
					foreach (ZipFile.ExtractorSettings extractorSettings2 in ZipFile.SettingsList)
					{
						if (extractorSettings2.Flavor == options.Flavor)
						{
							extractorSettings = extractorSettings2;
							break;
						}
					}
					if (extractorSettings == null)
					{
						throw new BadStateException(string.Format("While saving a Self-Extracting Zip, Cannot find that flavor ({0})?", options.Flavor));
					}
					CompilerParameters compilerParameters = new CompilerParameters();
					compilerParameters.ReferencedAssemblies.Add(assembly.Location);
					if (extractorSettings.ReferencedAssemblies != null)
					{
						foreach (string text4 in extractorSettings.ReferencedAssemblies)
						{
							compilerParameters.ReferencedAssemblies.Add(text4);
						}
					}
					compilerParameters.GenerateInMemory = false;
					compilerParameters.GenerateExecutable = true;
					compilerParameters.IncludeDebugInformation = false;
					compilerParameters.CompilerOptions = "";
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					StringBuilder stringBuilder = new StringBuilder();
					string text5 = ZipFile.GenerateTempPathname(text3, "cs");
					using (ZipFile zipFile = ZipFile.Read(executingAssembly.GetManifestResourceStream("Ionic.Zip.Resources.ZippedResources.zip")))
					{
						text2 = ZipFile.GenerateTempPathname(text3, "tmp");
						if (string.IsNullOrEmpty(options.IconFile))
						{
							Directory.CreateDirectory(text2);
							ZipEntry zipEntry = zipFile["zippedFile.ico"];
							if ((zipEntry.Attributes & 1) == 1)
							{
								zipEntry.Attributes ^= 1;
							}
							zipEntry.Extract(text2);
							string text6 = Path.Combine(text2, "zippedFile.ico");
							CompilerParameters compilerParameters2 = compilerParameters;
							compilerParameters2.CompilerOptions += string.Format("/win32icon:\"{0}\"", text6);
						}
						else
						{
							CompilerParameters compilerParameters3 = compilerParameters;
							compilerParameters3.CompilerOptions += string.Format("/win32icon:\"{0}\"", options.IconFile);
						}
						compilerParameters.OutputAssembly = text;
						if (options.Flavor == SelfExtractorFlavor.WinFormsApplication)
						{
							CompilerParameters compilerParameters4 = compilerParameters;
							compilerParameters4.CompilerOptions += " /target:winexe";
						}
						if (!string.IsNullOrEmpty(options.AdditionalCompilerSwitches))
						{
							CompilerParameters compilerParameters5 = compilerParameters;
							compilerParameters5.CompilerOptions = compilerParameters5.CompilerOptions + " " + options.AdditionalCompilerSwitches;
						}
						if (string.IsNullOrEmpty(compilerParameters.CompilerOptions))
						{
							compilerParameters.CompilerOptions = null;
						}
						if (extractorSettings.CopyThroughResources != null && extractorSettings.CopyThroughResources.Count != 0)
						{
							if (!Directory.Exists(text2))
							{
								Directory.CreateDirectory(text2);
							}
							foreach (string text7 in extractorSettings.CopyThroughResources)
							{
								string text8 = Path.Combine(text2, text7);
								ZipFile.ExtractResourceToFile(executingAssembly, text7, text8);
								compilerParameters.EmbeddedResources.Add(text8);
							}
						}
						compilerParameters.EmbeddedResources.Add(assembly.Location);
						stringBuilder.Append("// " + Path.GetFileName(text5) + "\n").Append("// --------------------------------------------\n//\n").Append("// This SFX source file was generated by DotNetZip ")
							.Append(ZipFile.LibraryVersion.ToString())
							.Append("\n//         at ")
							.Append(DateTime.Now.ToString("yyyy MMMM dd  HH:mm:ss"))
							.Append("\n//\n// --------------------------------------------\n\n\n");
						if (!string.IsNullOrEmpty(options.Description))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"" + options.Description.Replace("\"", "") + "\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
						}
						if (!string.IsNullOrEmpty(options.ProductVersion))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyInformationalVersion(\"" + options.ProductVersion.Replace("\"", "") + "\")]\n");
						}
						string text9 = (string.IsNullOrEmpty(options.Copyright) ? "Extractor: Copyright © Dino Chiesa 2008-2011" : options.Copyright.Replace("\"", ""));
						if (!string.IsNullOrEmpty(options.ProductName))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"").Append(options.ProductName.Replace("\"", "")).Append("\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"DotNetZip\")]\n");
						}
						stringBuilder.Append("[assembly: System.Reflection.AssemblyCopyright(\"" + text9 + "\")]\n").Append(string.Format("[assembly: System.Reflection.AssemblyVersion(\"{0}\")]\n", ZipFile.LibraryVersion.ToString()));
						if (options.FileVersion != null)
						{
							stringBuilder.Append(string.Format("[assembly: System.Reflection.AssemblyFileVersion(\"{0}\")]\n", options.FileVersion.ToString()));
						}
						stringBuilder.Append("\n\n\n");
						string text10 = options.DefaultExtractDirectory;
						if (text10 != null)
						{
							text10 = text10.Replace("\"", "").Replace("\\", "\\\\");
						}
						string text11 = options.PostExtractCommandLine;
						if (text11 != null)
						{
							text11 = text11.Replace("\\", "\\\\");
							text11 = text11.Replace("\"", "\\\"");
						}
						foreach (string text12 in extractorSettings.ResourcesToCompile)
						{
							using (Stream stream = zipFile[text12].OpenReader())
							{
								if (stream == null)
								{
									throw new ZipException(string.Format("missing resource '{0}'", text12));
								}
								using (StreamReader streamReader = new StreamReader(stream))
								{
									while (streamReader.Peek() >= 0)
									{
										string text13 = streamReader.ReadLine();
										if (text10 != null)
										{
											text13 = text13.Replace("@@EXTRACTLOCATION", text10);
										}
										text13 = text13.Replace("@@REMOVE_AFTER_EXECUTE", options.RemoveUnpackedFilesAfterExecute.ToString());
										text13 = text13.Replace("@@QUIET", options.Quiet.ToString());
										if (!string.IsNullOrEmpty(options.SfxExeWindowTitle))
										{
											text13 = text13.Replace("@@SFX_EXE_WINDOW_TITLE", options.SfxExeWindowTitle);
										}
										text13 = text13.Replace("@@EXTRACT_EXISTING_FILE", ((int)options.ExtractExistingFile).ToString());
										if (text11 != null)
										{
											text13 = text13.Replace("@@POST_UNPACK_CMD_LINE", text11);
										}
										stringBuilder.Append(text13).Append("\n");
									}
								}
								stringBuilder.Append("\n\n");
							}
						}
					}
					string text14 = stringBuilder.ToString();
					CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[] { text14 });
					if (compilerResults == null)
					{
						throw new SfxGenerationException("Cannot compile the extraction logic!");
					}
					if (this.Verbose)
					{
						foreach (string text15 in compilerResults.Output)
						{
							this.StatusMessageTextWriter.WriteLine(text15);
						}
					}
					if (compilerResults.Errors.Count != 0)
					{
						using (TextWriter textWriter = new StreamWriter(text5))
						{
							textWriter.Write(text14);
							textWriter.Write("\n\n\n// ------------------------------------------------------------------\n");
							textWriter.Write("// Errors during compilation: \n//\n");
							string fileName = Path.GetFileName(text5);
							foreach (object obj in compilerResults.Errors)
							{
								CompilerError compilerError = (CompilerError)obj;
								textWriter.Write(string.Format("//   {0}({1},{2}): {3} {4}: {5}\n//\n", new object[]
								{
									fileName,
									compilerError.Line,
									compilerError.Column,
									compilerError.IsWarning ? "Warning" : "error",
									compilerError.ErrorNumber,
									compilerError.ErrorText
								}));
							}
						}
						throw new SfxGenerationException(string.Format("Errors compiling the extraction logic!  {0}", text5));
					}
					this.OnSaveEvent(ZipProgressEventType.Saving_AfterCompileSelfExtractor);
					using (Stream stream2 = File.OpenRead(text))
					{
						byte[] array = new byte[4000];
						int num = 1;
						while (num != 0)
						{
							num = stream2.Read(array, 0, array.Length);
							if (num != 0)
							{
								this.WriteStream.Write(array, 0, num);
							}
						}
					}
				}
				this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
			}
			finally
			{
				try
				{
					if (Directory.Exists(text2))
					{
						try
						{
							Directory.Delete(text2, true);
						}
						catch (IOException ex)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", ex);
						}
					}
					if (File.Exists(text))
					{
						try
						{
							File.Delete(text);
						}
						catch (IOException ex2)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", ex2);
						}
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000EF70 File Offset: 0x0000D170
		internal static string GenerateTempPathname(string dir, string extension)
		{
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			string text3;
			do
			{
				string text = Guid.NewGuid().ToString();
				string text2 = string.Format("{0}-{1}-{2}.{3}", new object[]
				{
					name,
					DateTime.Now.ToString("yyyyMMMdd-HHmmss"),
					text,
					extension
				});
				text3 = Path.Combine(dir, text2);
			}
			while (File.Exists(text3) || Directory.Exists(text3));
			return text3;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public void AddSelectedFiles(string selectionCriteria)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, false);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000F008 File Offset: 0x0000D208
		public void AddSelectedFiles(string selectionCriteria, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000F018 File Offset: 0x0000D218
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, false);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000F024 File Offset: 0x0000D224
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000F030 File Offset: 0x0000D230
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, false);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000F03C File Offset: 0x0000D23C
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, false);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000F04A File Offset: 0x0000D24A
		public void UpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, true);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000F058 File Offset: 0x0000D258
		private string EnsureendInSlash(string s)
		{
			if (s.EndsWith("\\"))
			{
				return s;
			}
			return s + "\\";
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000F074 File Offset: 0x0000D274
		private void _AddOrUpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories, bool wantUpdate)
		{
			if (directoryOnDisk == null && Directory.Exists(selectionCriteria))
			{
				directoryOnDisk = selectionCriteria;
				selectionCriteria = "*.*";
			}
			else if (string.IsNullOrEmpty(directoryOnDisk))
			{
				directoryOnDisk = ".";
			}
			while (directoryOnDisk.EndsWith("\\"))
			{
				directoryOnDisk = directoryOnDisk.Substring(0, directoryOnDisk.Length - 1);
			}
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding selection '{0}' from dir '{1}'...", selectionCriteria, directoryOnDisk);
			}
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			ReadOnlyCollection<string> readOnlyCollection = fileSelector.SelectFiles(directoryOnDisk, recurseDirectories);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("found {0} files...", readOnlyCollection.Count);
			}
			this.OnAddStarted();
			AddOrUpdateAction addOrUpdateAction = (wantUpdate ? AddOrUpdateAction.AddOrUpdate : AddOrUpdateAction.AddOnly);
			foreach (string text in readOnlyCollection)
			{
				string text2 = ((directoryPathInArchive == null) ? null : ZipFile.ReplaceLeadingDirectory(Path.GetDirectoryName(text), directoryOnDisk, directoryPathInArchive));
				if (File.Exists(text))
				{
					if (wantUpdate)
					{
						this.UpdateFile(text, text2);
					}
					else
					{
						this.AddFile(text, text2);
					}
				}
				else
				{
					this.AddOrUpdateDirectoryImpl(text, text2, addOrUpdateAction, false, 0);
				}
			}
			this.OnAddCompleted();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		private static string ReplaceLeadingDirectory(string original, string pattern, string replacement)
		{
			string text = original.ToUpper();
			string text2 = pattern.ToUpper();
			int num = text.IndexOf(text2);
			if (num != 0)
			{
				return original;
			}
			return replacement + original.Substring(text2.Length);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000F210 File Offset: 0x0000D410
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria, string directoryPathInArchive)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this, directoryPathInArchive);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F234 File Offset: 0x0000D434
		public int RemoveSelectedEntries(string selectionCriteria)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000F258 File Offset: 0x0000D458
		public int RemoveSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria, directoryPathInArchive);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000F27C File Offset: 0x0000D47C
		public void ExtractSelectedEntries(string selectionCriteria)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		public void ExtractSelectedEntries(string selectionCriteria, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractExistingFile);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F334 File Offset: 0x0000D534
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F390 File Offset: 0x0000D590
		public void ExtractSelectedEntries(string selectionCriteria, string directoryInArchive, string extractDirectory)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive, string extractDirectory, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory, extractExistingFile);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F584 File Offset: 0x0000D784
		public IEnumerator<ZipEntry> GetEnumerator()
		{
			foreach (ZipEntry e in this._entries.Values)
			{
				yield return e;
			}
			yield break;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		[DispId(-4)]
		public IEnumerator GetNewEnum()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		// Note: this type is marked as 'beforefieldinit'.
		static ZipFile()
		{
			ZipFile.ExtractorSettings[] array = new ZipFile.ExtractorSettings[2];
			ZipFile.ExtractorSettings[] array2 = array;
			int num = 0;
			ZipFile.ExtractorSettings extractorSettings = new ZipFile.ExtractorSettings();
			extractorSettings.Flavor = SelfExtractorFlavor.WinFormsApplication;
			ZipFile.ExtractorSettings extractorSettings2 = extractorSettings;
			List<string> list = new List<string>();
			list.Add("System.dll");
			list.Add("System.Windows.Forms.dll");
			list.Add("System.Drawing.dll");
			extractorSettings2.ReferencedAssemblies = list;
			ZipFile.ExtractorSettings extractorSettings3 = extractorSettings;
			List<string> list2 = new List<string>();
			list2.Add("Ionic.Zip.WinFormsSelfExtractorStub.resources");
			list2.Add("Ionic.Zip.Forms.PasswordDialog.resources");
			list2.Add("Ionic.Zip.Forms.ZipContentsDialog.resources");
			extractorSettings3.CopyThroughResources = list2;
			ZipFile.ExtractorSettings extractorSettings4 = extractorSettings;
			List<string> list3 = new List<string>();
			list3.Add("WinFormsSelfExtractorStub.cs");
			list3.Add("WinFormsSelfExtractorStub.Designer.cs");
			list3.Add("PasswordDialog.cs");
			list3.Add("PasswordDialog.Designer.cs");
			list3.Add("ZipContentsDialog.cs");
			list3.Add("ZipContentsDialog.Designer.cs");
			list3.Add("FolderBrowserDialogEx.cs");
			extractorSettings4.ResourcesToCompile = list3;
			array2[num] = extractorSettings;
			ZipFile.ExtractorSettings[] array3 = array;
			int num2 = 1;
			ZipFile.ExtractorSettings extractorSettings5 = new ZipFile.ExtractorSettings();
			extractorSettings5.Flavor = SelfExtractorFlavor.ConsoleApplication;
			ZipFile.ExtractorSettings extractorSettings6 = extractorSettings5;
			List<string> list4 = new List<string>();
			list4.Add("System.dll");
			extractorSettings6.ReferencedAssemblies = list4;
			extractorSettings5.CopyThroughResources = null;
			ZipFile.ExtractorSettings extractorSettings7 = extractorSettings5;
			List<string> list5 = new List<string>();
			list5.Add("CommandLineSelfExtractorStub.cs");
			extractorSettings7.ResourcesToCompile = list5;
			array3[num2] = extractorSettings5;
			ZipFile.SettingsList = array;
		}

		// Token: 0x04000103 RID: 259
		private TextWriter _StatusMessageTextWriter;

		// Token: 0x04000104 RID: 260
		private bool _CaseSensitiveRetrieval;

		// Token: 0x04000105 RID: 261
		private Stream _readstream;

		// Token: 0x04000106 RID: 262
		private Stream _writestream;

		// Token: 0x04000107 RID: 263
		private ushort _versionMadeBy;

		// Token: 0x04000108 RID: 264
		private ushort _versionNeededToExtract;

		// Token: 0x04000109 RID: 265
		private uint _diskNumberWithCd;

		// Token: 0x0400010A RID: 266
		private int _maxOutputSegmentSize;

		// Token: 0x0400010B RID: 267
		private uint _numberOfSegmentsForMostRecentSave;

		// Token: 0x0400010C RID: 268
		private ZipErrorAction _zipErrorAction;

		// Token: 0x0400010D RID: 269
		private bool _disposed;

		// Token: 0x0400010E RID: 270
		private Dictionary<string, ZipEntry> _entries;

		// Token: 0x0400010F RID: 271
		private List<ZipEntry> _zipEntriesAsList;

		// Token: 0x04000110 RID: 272
		private string _name;

		// Token: 0x04000111 RID: 273
		private string _readName;

		// Token: 0x04000112 RID: 274
		private string _Comment;

		// Token: 0x04000113 RID: 275
		internal string _Password;

		// Token: 0x04000114 RID: 276
		private bool _emitNtfsTimes = true;

		// Token: 0x04000115 RID: 277
		private bool _emitUnixTimes;

		// Token: 0x04000116 RID: 278
		private CompressionStrategy _Strategy;

		// Token: 0x04000117 RID: 279
		private CompressionMethod _compressionMethod = CompressionMethod.Deflate;

		// Token: 0x04000118 RID: 280
		private bool _fileAlreadyExists;

		// Token: 0x04000119 RID: 281
		private string _temporaryFileName;

		// Token: 0x0400011A RID: 282
		private bool _contentsChanged;

		// Token: 0x0400011B RID: 283
		private bool _hasBeenSaved;

		// Token: 0x0400011C RID: 284
		private string _TempFileFolder;

		// Token: 0x0400011D RID: 285
		private bool _ReadStreamIsOurs = true;

		// Token: 0x0400011E RID: 286
		private object LOCK = new object();

		// Token: 0x0400011F RID: 287
		private bool _saveOperationCanceled;

		// Token: 0x04000120 RID: 288
		private bool _extractOperationCanceled;

		// Token: 0x04000121 RID: 289
		private bool _addOperationCanceled;

		// Token: 0x04000122 RID: 290
		private EncryptionAlgorithm _Encryption;

		// Token: 0x04000123 RID: 291
		private bool _JustSaved;

		// Token: 0x04000124 RID: 292
		private long _locEndOfCDS = -1L;

		// Token: 0x04000125 RID: 293
		private uint _OffsetOfCentralDirectory;

		// Token: 0x04000126 RID: 294
		private long _OffsetOfCentralDirectory64;

		// Token: 0x04000127 RID: 295
		private bool? _OutputUsesZip64;

		// Token: 0x04000128 RID: 296
		internal bool _inExtractAll;

		// Token: 0x04000129 RID: 297
		private ZipOption _alternateEncodingUsage;

		// Token: 0x0400012A RID: 298
		private static Encoding _defaultEncoding = Encoding.UTF8;

		// Token: 0x0400012B RID: 299
		private Encoding _alternateEncoding = Encoding.UTF8;

		// Token: 0x0400012C RID: 300
		private int _BufferSize = ZipFile.BufferSizeDefault;

		// Token: 0x0400012D RID: 301
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x0400012E RID: 302
		private long _ParallelDeflateThreshold;

		// Token: 0x0400012F RID: 303
		private int _maxBufferPairs = 16;

		// Token: 0x04000130 RID: 304
		internal Zip64Option _zip64;

		// Token: 0x04000131 RID: 305
		private bool _SavingSfx;

		// Token: 0x04000132 RID: 306
		public static readonly int BufferSizeDefault = 32768;

		// Token: 0x04000135 RID: 309
		private long _lengthOfReadStream = -99L;

		// Token: 0x04000139 RID: 313
		private static ZipFile.ExtractorSettings[] SettingsList;

		// Token: 0x02000034 RID: 52
		private class ExtractorSettings
		{
			// Token: 0x04000142 RID: 322
			public SelfExtractorFlavor Flavor;

			// Token: 0x04000143 RID: 323
			public List<string> ReferencedAssemblies;

			// Token: 0x04000144 RID: 324
			public List<string> CopyThroughResources;

			// Token: 0x04000145 RID: 325
			public List<string> ResourcesToCompile;
		}
	}
}
