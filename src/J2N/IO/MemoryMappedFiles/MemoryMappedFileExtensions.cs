﻿using System;
using System.IO.MemoryMappedFiles;

namespace J2N.IO.MemoryMappedFiles
{
    /// <summary>
    /// Extensions to the <see cref="MemoryMappedFile"/> class.
    /// </summary>
    public static class MemoryMappedFileExtensions
    {
        /// <summary>
        /// Creates a <see cref="MemoryMappedViewByteBuffer"/> that maps to a view of the 
        /// memory-mapped file.
        /// </summary>
        /// <param name="memoryMappedFile">This <see cref="MemoryMappedFile"/>.</param>
        /// <returns>A randomly accessible block of memory, as a <see cref="MemoryMappedViewByteBuffer"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="memoryMappedFile"/> is <c>null</c>.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        public static MemoryMappedViewByteBuffer CreateViewByteBuffer(this MemoryMappedFile memoryMappedFile)
        {
            return CreateViewByteBuffer(memoryMappedFile, 0, 0, MemoryMappedFileAccess.ReadWrite, 0, 0);
        }

        /// <summary>
        /// Creates a <see cref="MemoryMappedViewByteBuffer"/> that maps to a view of the 
        /// memory-mapped file, and that has the specified <paramref name="offset"/> and <paramref name="size"/>.
        /// </summary>
        /// <param name="memoryMappedFile">This <see cref="MemoryMappedFile"/>.</param>
        /// <param name="offset">The byte at which to start the view.</param>
        /// <param name="size">The size of the view. Specify <c>0</c> (zero) to create a view that
        /// starts at <paramref name="offset"/> and ends approximately at the end of the memory-mapped file.</param>
        /// <returns>A randomly accessible block of memory, as a <see cref="MemoryMappedViewByteBuffer"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="memoryMappedFile"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="size"/> is a negative value.
        /// <para/>
        /// -or-
        /// <para/>
        /// <paramref name="size"/> is greater than the logical address space.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        public static MemoryMappedViewByteBuffer CreateViewByteBuffer(this MemoryMappedFile memoryMappedFile, long offset, long size)
        {
            return CreateViewByteBuffer(memoryMappedFile, offset, size, MemoryMappedFileAccess.ReadWrite, 0, (int)size); // TODO: Make ByteBuffer use long?
        }

        /// <summary>
        /// Creates a <see cref="MemoryMappedViewByteBuffer"/> that maps to a view of the 
        /// memory-mapped file, and that has the specified <paramref name="offset"/> and <paramref name="size"/>.
        /// </summary>
        /// <param name="memoryMappedFile">This <see cref="MemoryMappedFile"/>.</param>
        /// <param name="offset">The byte at which to start the view.</param>
        /// <param name="size">The size of the view. Specify <c>0</c> (zero) to create a view that
        /// starts at <paramref name="offset"/> and ends approximately at the end of the memory-mapped file.</param>
        /// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="MemoryMappedFileAccess.ReadWrite"/>.</param>
        /// <returns>A randomly accessible block of memory, as a <see cref="MemoryMappedViewByteBuffer"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="memoryMappedFile"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="size"/> is a negative value.
        /// <para/>
        /// -or-
        /// <para/>
        /// <paramref name="size"/> is greater than the logical address space.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException"><paramref name="access"/> is invalid for the memory-mapped file.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        public static MemoryMappedViewByteBuffer CreateViewByteBuffer(this MemoryMappedFile memoryMappedFile, long offset, long size, MemoryMappedFileAccess access)
        {
            return CreateViewByteBuffer(memoryMappedFile, offset, size, access, 0, (int)size);
        }

        internal static MemoryMappedViewByteBuffer CreateViewByteBuffer(this MemoryMappedFile memoryMappedFile, long offset, long size, MemoryMappedFileAccess access, int bufferOffset, int bufferSize)
        {
            if (memoryMappedFile == null)
                throw new ArgumentNullException(nameof(memoryMappedFile));

            switch (access)
            {
                case MemoryMappedFileAccess.Read:
                case MemoryMappedFileAccess.ReadExecute:
                    return new ReadOnlyMemoryMappedViewByteBuffer(memoryMappedFile.CreateViewAccessor(offset, size, access), bufferSize, bufferOffset);
                case MemoryMappedFileAccess.ReadWrite:
                case MemoryMappedFileAccess.ReadWriteExecute:
                case MemoryMappedFileAccess.Write:
                case MemoryMappedFileAccess.CopyOnWrite:
                    return new ReadWriteMemoryMappedViewByteBuffer(memoryMappedFile.CreateViewAccessor(offset, size, access), bufferSize, bufferOffset);
                default:
                    throw new ArgumentOutOfRangeException(nameof(access));
            }
        }
    }
}
