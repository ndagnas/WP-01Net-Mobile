//*************************************************************************************************
// DEBUT DU FICHIER
//*************************************************************************************************

//*************************************************************************************************
// Nom           : GZipStream.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet GZipStream
// Environnement : Visual Studio 2012
// Créé le       : 19/04/2015
// Modifié le    : 19/04/2015
//*************************************************************************************************

//-------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;
//-------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------

//*************************************************************************************************
// Début du bloc "System.IO.Compression"
//*************************************************************************************************
namespace System.IO.Compression
	{

	//*********************************************************************************************
	// Classe GZipStream
	//*********************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------
    public class GZipStream : Stream
    {
        internal ZlibBaseStream BaseStream;
        private string comment;
        private int crc32;
        private bool disposed;
        private string fileName;
        private bool firstReadDone;
        internal static readonly DateTime UnixEpoch = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
        {
        }

        public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
        {
        }

        public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
        {
        }

        public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
        {
            this.BaseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing && (this.BaseStream != null))
                    {
                        this.BaseStream.Dispose();
                        this.crc32 = this.BaseStream.Crc32;
                    }
                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private int EmitHeader()
        {
            byte[] sourceArray = (this.Comment == null) ? null : ArchiveEncoding.Default.GetBytes(this.Comment);
            byte[] buffer2 = (this.FileName == null) ? null : ArchiveEncoding.Default.GetBytes(this.FileName);
            int num = (this.Comment == null) ? 0 : (sourceArray.Length + 1);
            int num2 = (this.FileName == null) ? 0 : (buffer2.Length + 1);
            int num3 = (10 + num) + num2;
            byte[] destinationArray = new byte[num3];
            int destinationIndex = 0;
            destinationArray[destinationIndex++] = 0x1f;
            destinationArray[destinationIndex++] = 0x8b;
            destinationArray[destinationIndex++] = 8;
            byte num5 = 0;
            if (this.Comment != null)
            {
                num5 = (byte) (num5 ^ 0x10);
            }
            if (this.FileName != null)
            {
                num5 = (byte) (num5 ^ 8);
            }
            destinationArray[destinationIndex++] = num5;
            if (!this.LastModified.HasValue)
            {
                this.LastModified = new DateTime?(DateTime.Now);
            }
            TimeSpan span = this.LastModified.Value - UnixEpoch;
            int totalSeconds = (int) span.TotalSeconds;
            Array.Copy(BitConverter.GetBytes(totalSeconds), 0, destinationArray, destinationIndex, 4);
            destinationIndex += 4;
            destinationArray[destinationIndex++] = 0;
            destinationArray[destinationIndex++] = 0xff;
            if (num2 != 0)
            {
                Array.Copy(buffer2, 0, destinationArray, destinationIndex, num2 - 1);
                destinationIndex += num2 - 1;
                destinationArray[destinationIndex++] = 0;
            }
            if (num != 0)
            {
                Array.Copy(sourceArray, 0, destinationArray, destinationIndex, num - 1);
                destinationIndex += num - 1;
                destinationArray[destinationIndex++] = 0;
            }
            this.BaseStream._stream.Write(destinationArray, 0, destinationArray.Length);
            return destinationArray.Length;
        }

        public override void Flush()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("GZipStream");
            }
            this.BaseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("GZipStream");
            }
            int num = this.BaseStream.Read(buffer, offset, count);
            if (!this.firstReadDone)
            {
                this.firstReadDone = true;
                this.FileName = this.BaseStream._GzipFileName;
                this.Comment = this.BaseStream._GzipComment;
            }
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("GZipStream");
            }
            if (this.BaseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
            {
                    throw new InvalidOperationException();
            }
            this.BaseStream.Write(buffer, offset, count);
        }

        public int BufferSize
        {
            get
            {
                return this.BaseStream._bufferSize;
            }
            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                if (this.BaseStream._workingBuffer != null)
                {
                    throw new ZlibException("The working buffer is already set.");
                }
                if (value < 0x400)
                {
                    throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", new object[] { value, 0x400 }));
                }
                this.BaseStream._bufferSize = value;
            }
        }

        public override bool CanRead
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                return this.BaseStream._stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                return this.BaseStream._stream.CanWrite;
            }
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                this.comment = value;
            }
        }

        public int Crc32
        {
            get
            {
                return this.crc32;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                this.fileName = value;
                if (this.fileName != null)
                {
                    if (this.fileName.IndexOf("/") != -1)
                    {
                        this.fileName = this.fileName.Replace("/", @"\");
                    }
                    if (this.fileName.EndsWith(@"\"))
                    {
                        throw new InvalidOperationException("Illegal filename");
                    }
                }
            }
        }

        public virtual FlushType FlushMode
        {
            get
            {
                return this.BaseStream._flushMode;
            }
            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("GZipStream");
                }
                this.BaseStream._flushMode = value;
            }
        }

        public DateTime? LastModified
        {
            get;
            set;
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                if (this.BaseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
                {
                    return (this.BaseStream._z.TotalBytesOut);
                }
                if (this.BaseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
                {
                    return (this.BaseStream._z.TotalBytesIn + this.BaseStream._gzipHeaderByteCount);
                }
                return 0L;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        internal virtual long TotalIn
        {
            get
            {
                return this.BaseStream._z.TotalBytesIn;
            }
        }

        internal virtual long TotalOut
        {
            get
            {
                return this.BaseStream._z.TotalBytesOut;
            }
        }
    }
	//---------------------------------------------------------------------------------------------
	#endregion
	//*********************************************************************************************

	} // Fin du namespace "System.IO.Compression"
//*************************************************************************************************

//*************************************************************************************************
// FIN DU FICHIER
//*************************************************************************************************

