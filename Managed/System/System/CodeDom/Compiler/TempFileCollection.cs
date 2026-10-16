using System;
using System.Collections;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of temporary files.</summary>
	// Token: 0x02000344 RID: 836
	[Serializable]
	public class TempFileCollection : ICollection, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class with default values.</summary>
		// Token: 0x06001A6F RID: 6767 RVA: 0x000618FD File Offset: 0x0005FAFD
		public TempFileCollection()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory that is set to delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		// Token: 0x06001A70 RID: 6768 RVA: 0x00061907 File Offset: 0x0005FB07
		public TempFileCollection(string tempDir)
			: this(tempDir, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory and specified value indicating whether to keep or delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		/// <param name="keepFiles">true if the temporary files should be kept after use; false if the temporary files should be deleted. </param>
		// Token: 0x06001A71 RID: 6769 RVA: 0x00061911 File Offset: 0x0005FB11
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.KeepFiles = keepFiles;
			this._tempDir = tempDir;
			this._files = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. </summary>
		// Token: 0x06001A72 RID: 6770 RVA: 0x00061937 File Offset: 0x0005FB37
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001A73 RID: 6771 RVA: 0x00061946 File Offset: 0x0005FB46
		protected virtual void Dispose(bool disposing)
		{
			this.SafeDelete();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00061950 File Offset: 0x0005FB50
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		// Token: 0x06001A75 RID: 6773 RVA: 0x00061980 File Offset: 0x0005FB80
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.KeepFiles);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection, using the specified value indicating whether the file should be deleted or retained.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileExtension" /> is null or an empty string.</exception>
		// Token: 0x06001A76 RID: 6774 RVA: 0x00061990 File Offset: 0x0005FB90
		public string AddExtension(string fileExtension, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileExtension))
			{
				throw new ArgumentException(SR.Format("Argument {0} cannot be null or zero-length.", "fileExtension"), "fileExtension");
			}
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		/// <summary>Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called.</summary>
		/// <param name="fileName">The name of the file to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is null or an empty string.-or-<paramref name="fileName" /> is a duplicate.</exception>
		// Token: 0x06001A77 RID: 6775 RVA: 0x000619DC File Offset: 0x0005FBDC
		public void AddFile(string fileName, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(SR.Format("Argument {0} cannot be null or zero-length.", "fileName"), "fileName");
			}
			if (this._files[fileName] != null)
			{
				throw new ArgumentException(SR.Format("The file name '{0}' was already in the collection.", fileName), "fileName");
			}
			this._files.Add(fileName, keepFile);
		}

		/// <summary>Gets an enumerator that can enumerate the members of the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x06001A78 RID: 6776 RVA: 0x00061A41 File Offset: 0x0005FC41
		public IEnumerator GetEnumerator()
		{
			return this._files.Keys.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001A79 RID: 6777 RVA: 0x00061A41 File Offset: 0x0005FC41
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._files.Keys.GetEnumerator();
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="start">The zero-based index in array at which copying begins.</param>
		// Token: 0x06001A7A RID: 6778 RVA: 0x00061A53 File Offset: 0x0005FC53
		void ICollection.CopyTo(Array array, int start)
		{
			this._files.Keys.CopyTo(array, start);
		}

		/// <summary>Copies the members of the collection to the specified string, beginning at the specified index.</summary>
		/// <param name="fileNames">The array of strings to copy to. </param>
		/// <param name="start">The index of the array to begin copying to. </param>
		// Token: 0x06001A7B RID: 6779 RVA: 0x00061A53 File Offset: 0x0005FC53
		public void CopyTo(string[] fileNames, int start)
		{
			this._files.Keys.CopyTo(fileNames, start);
		}

		/// <summary>Gets the number of files in the collection.</summary>
		/// <returns>The number of files in the collection.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00061A67 File Offset: 0x0005FC67
		public int Count
		{
			get
			{
				return this._files.Count;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00061A67 File Offset: 0x0005FC67
		int ICollection.Count
		{
			get
			{
				return this._files.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the temporary directory to store the temporary files in.</summary>
		/// <returns>The temporary directory to store the temporary files in.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00061A74 File Offset: 0x0005FC74
		public string TempDir
		{
			get
			{
				return this._tempDir ?? string.Empty;
			}
		}

		/// <summary>Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</summary>
		/// <returns>The full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</returns>
		/// <exception cref="T:System.Security.SecurityException">If the <see cref="P:System.CodeDom.Compiler.TempFileCollection.BasePath" /> property has not been set or is set to null, and <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" /> is not granted for the temporary directory indicated by the <see cref="P:System.CodeDom.Compiler.TempFileCollection.TempDir" /> property. </exception>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00061A85 File Offset: 0x0005FC85
		public string BasePath
		{
			get
			{
				this.EnsureTempNameCreated();
				return this._basePath;
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00061A94 File Offset: 0x0005FC94
		private void EnsureTempNameCreated()
		{
			if (this._basePath == null)
			{
				string text = null;
				bool flag = false;
				int num = 5000;
				do
				{
					this._basePath = Path.Combine(string.IsNullOrEmpty(this.TempDir) ? Path.GetTempPath() : this.TempDir, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
					text = this._basePath + ".tmp";
					try
					{
						new FileStream(text, FileMode.CreateNew, FileAccess.Write).Dispose();
						flag = true;
					}
					catch (IOException ex)
					{
						num--;
						if (num == 0 || ex is DirectoryNotFoundException)
						{
							throw;
						}
						flag = false;
					}
				}
				while (!flag);
				this._files.Add(text, this.KeepFiles);
			}
		}

		/// <summary>Gets or sets a value indicating whether to keep the files, by default, when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called or the collection is disposed.</summary>
		/// <returns>true if the files should be kept; otherwise, false.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00061B48 File Offset: 0x0005FD48
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00061B50 File Offset: 0x0005FD50
		public bool KeepFiles { get; set; }

		// Token: 0x06001A85 RID: 6789 RVA: 0x00061B5C File Offset: 0x0005FD5C
		private bool KeepFile(string fileName)
		{
			object obj = this._files[fileName];
			return obj != null && (bool)obj;
		}

		/// <summary>Deletes the temporary files within this collection that were not marked to be kept.</summary>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00061946 File Offset: 0x0005FB46
		public void Delete()
		{
			this.SafeDelete();
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00061B84 File Offset: 0x0005FD84
		internal void Delete(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00061BAC File Offset: 0x0005FDAC
		internal void SafeDelete()
		{
			if (this._files != null && this._files.Count > 0)
			{
				string[] array = new string[this._files.Count];
				this._files.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!this.KeepFile(text))
					{
						this.Delete(text);
						this._files.Remove(text);
					}
				}
			}
		}

		// Token: 0x04000E3B RID: 3643
		private string _basePath;

		// Token: 0x04000E3C RID: 3644
		private readonly string _tempDir;

		// Token: 0x04000E3D RID: 3645
		private readonly Hashtable _files;
	}
}
