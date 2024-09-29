// <auto-generated />
#pragma warning disable
#if !NET6_0_OR_GREATER && FeatureMemory

namespace Polyfills;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
    static FieldInfo chunkCharsField = GetStringBuilderField("m_ChunkChars");
    static FieldInfo chunkPreviousField = GetStringBuilderField("m_ChunkPrevious");
    static FieldInfo chunkLengthField = GetStringBuilderField("m_ChunkLength");

    static FieldInfo GetStringBuilderField(string name)
    {
        var field = typeof(StringBuilder).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field != null)
        {
            return field;
        }

        throw new($"Expected to find field '{name}' on StringBuilder");
    }

    static int GetChunkLength(StringBuilder stringBuilder) =>
        (int) chunkLengthField.GetValue(stringBuilder)!;

    static char[] GetChunkChars(StringBuilder stringBuilder) =>
        (char[]) chunkCharsField.GetValue(stringBuilder)!;

    static StringBuilder? GetChunkPrevious(StringBuilder stringBuilder) =>
        (StringBuilder?) chunkPreviousField.GetValue(stringBuilder);

    /// <summary>
    /// GetChunks returns ChunkEnumerator that follows the IEnumerable pattern and
    /// thus can be used in a C# 'foreach' statements to retrieve the data in the StringBuilder
    /// as chunks (ReadOnlyMemory) of characters.  An example use is:
    ///
    ///      foreach (ReadOnlyMemory&lt;char&gt; chunk in sb.GetChunks())
    ///         foreach (char c in chunk.Span)
    ///             { /* operation on c }
    ///
    /// It is undefined what happens if the StringBuilder is modified while the chunk
    /// enumeration is incomplete.  StringBuilder is also not thread-safe, so operating
    /// on it with concurrent threads is illegal.  Finally the ReadOnlyMemory chunks returned
    /// are NOT guaranteed to remain unchanged if the StringBuilder is modified, so do
    /// not cache them for later use either.  This API's purpose is efficiently extracting
    /// the data of a CONSTANT StringBuilder.
    ///
    /// Creating a ReadOnlySpan from a ReadOnlyMemory  (the .Span property) is expensive
    /// compared to the fetching of the character, so create a local variable for the SPAN
    /// if you need to use it in a nested for statement.  For example
    ///
    ///    foreach (ReadOnlyMemory&lt;char&gt; chunk in sb.GetChunks())
    ///    {
    ///         var span = chunk.Span;
    ///         for (int i = 0; i &lt; span.Length; i++)
    ///             { /* operation on span[i] */ }
    ///    }
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks")]
    public static ChunkEnumerator GetChunks(this StringBuilder target) =>
        new(target);

    /// <summary>
    /// ChunkEnumerator supports both the IEnumerable and IEnumerator pattern so foreach
    /// works (see GetChunks).  It needs to be public (so the compiler can use it
    /// when building a foreach statement) but users typically don't use it explicitly.
    /// (which is why it is a nested type).
    /// </summary>
    public struct ChunkEnumerator
    {
        // The first Stringbuilder chunk (which is the end of the logical string)
        StringBuilder _firstChunk;

        // The chunk that this enumerator is currently returning (Current).
        StringBuilder? _currentChunk;

        // Only used for long string builders with many chunks (see constructor)
        ManyChunkInfo? _manyChunks;

        // Only here to make foreach work
        /// <summary>
        /// Implement IEnumerable.GetEnumerator() to return  'this' as the IEnumerator
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ChunkEnumerator GetEnumerator() =>
            this;

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public bool MoveNext()
        {
            if (_currentChunk == _firstChunk)
            {
                return false;
            }

            if (_manyChunks != null)
            {
                return _manyChunks.MoveNext(ref _currentChunk);
            }

            var next = _firstChunk;
            while (true)
            {
                var chunkPrevious = GetChunkPrevious(next);
                if (chunkPrevious == _currentChunk)
                {
                    break;
                }

                next = chunkPrevious;
            }

            _currentChunk = next;
            return true;
        }

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public ReadOnlyMemory<char> Current
        {
            get
            {
                if (_currentChunk == null)
                {
                    throw new InvalidOperationException("Enumeration operation cant happen");
                }

                return new ReadOnlyMemory<char>(GetChunkChars(_currentChunk), 0, GetChunkLength(_currentChunk));
            }
        }

        internal ChunkEnumerator(StringBuilder builder)
        {
            _firstChunk = builder;
            // MoveNext will find the last chunk if we do this.
            _currentChunk = null;
            _manyChunks = null;

            // There is a performance-vs-allocation tradeoff.   Because the chunks
            // are a linked list with each chunk pointing to its PREDECESSOR, walking
            // the list FORWARD is not efficient.   If there are few chunks (< 8) we
            // simply scan from the start each time, and tolerate the N*N behavior.
            // However above this size, we allocate an array to hold reference to all
            // the chunks and we can be efficient for large N.
            var chunkCount = ChunkCount(builder);
            if (8 < chunkCount)
            {
                _manyChunks = new ManyChunkInfo(builder, chunkCount);
            }
        }

        static int ChunkCount(StringBuilder? builder)
        {
            var ret = 0;
            while (builder != null)
            {
                ret++;
                builder = GetChunkPrevious(builder);
            }

            return ret;
        }

        /// <summary>
        /// Used to hold all the chunks indexes when you have many chunks.
        /// </summary>
        class ManyChunkInfo
        {
            // These are in normal order (first chunk first)
            StringBuilder[] _chunks;
            int _chunkPos;

            public bool MoveNext(ref StringBuilder? current)
            {
                int pos = ++_chunkPos;
                if (_chunks.Length <= pos)
                {
                    return false;
                }

                current = _chunks[pos];
                return true;
            }

            public ManyChunkInfo(StringBuilder? builder, int chunkCount)
            {
                _chunks = new StringBuilder[chunkCount];
                while (0 <= --chunkCount)
                {
                    _chunks[chunkCount] = builder;
                    builder = GetChunkPrevious(builder);
                }

                _chunkPos = -1;
            }
        }
    }
}
#endif