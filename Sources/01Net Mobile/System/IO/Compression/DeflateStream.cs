﻿//*************************************************************************************************
// DEBUT DU FICHIER
//*************************************************************************************************

//*************************************************************************************************
// Nom           : DeflateStream.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet DeflateStream
// Environnement : Visual Studio 2012
// Créé le       : 19/04/2015
// Modifié le    : 19/04/2015
//*************************************************************************************************

//-------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Runtime.InteropServices;
//-------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------

//*************************************************************************************************
// Début du bloc "System.IO.Compression"
//*************************************************************************************************
namespace System.IO.Compression
	{

	//*********************************************************************************************
	// Classe DeflateStream
	//*********************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------
    public class DeflateStream : Stream
    {
        private readonly ZlibBaseStream _baseStream;
        private bool _disposed;

        public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level = CompressionLevel.Level6, bool leaveOpen = false)
        {
            this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this._disposed)
                {
                    if (disposing && (this._baseStream != null))
                    {
                        this._baseStream.Dispose();
                    }
                    this._disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override void Flush()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException("DeflateStream");
            }
            this._baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException("DeflateStream");
            }
            return this._baseStream.Read(buffer, offset, count);
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
            if (this._disposed)
            {
                throw new ObjectDisposedException("DeflateStream");
            }
            this._baseStream.Write(buffer, offset, count);
        }

        public int BufferSize
        {
            get
            {
                return this._baseStream._bufferSize;
            }
            set
            {
                if (this._disposed)
                {
                    throw new ObjectDisposedException("DeflateStream");
                }
                if (this._baseStream._workingBuffer != null)
                {
                    throw new ZlibException("The working buffer is already set.");
                }
                if (value < 0x400)
                {
                    throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", new object[] { value, 0x400 }));
                }
                this._baseStream._bufferSize = value;
            }
        }

        public override bool CanRead
        {
            get
            {
                if (this._disposed)
                {
                    throw new ObjectDisposedException("DeflateStream");
                }
                return this._baseStream._stream.CanRead;
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
                if (this._disposed)
                {
                    throw new ObjectDisposedException("DeflateStream");
                }
                return this._baseStream._stream.CanWrite;
            }
        }

        public virtual FlushType FlushMode
        {
            get
            {
                return this._baseStream._flushMode;
            }
            set
            {
                if (this._disposed)
                {
                    throw new ObjectDisposedException("DeflateStream");
                }
                this._baseStream._flushMode = value;
            }
        }

        public MemoryStream InputBuffer
        {
            get
            {
                return new MemoryStream(this._baseStream._z.InputBuffer, this._baseStream._z.NextIn, this._baseStream._z.AvailableBytesIn);
            }
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
                if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
                {
                    return this._baseStream._z.TotalBytesOut;
                }
                if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
                {
                    return this._baseStream._z.TotalBytesIn;
                }
                return 0L;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CompressionStrategy Strategy
        {
            get
            {
                return this._baseStream.Strategy;
            }
            set
            {
                if (this._disposed)
                {
                    throw new ObjectDisposedException("DeflateStream");
                }
                this._baseStream.Strategy = value;
            }
        }

        public virtual long TotalIn
        {
            get
            {
                return this._baseStream._z.TotalBytesIn;
            }
        }

        public virtual long TotalOut
        {
            get
            {
                return this._baseStream._z.TotalBytesOut;
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

