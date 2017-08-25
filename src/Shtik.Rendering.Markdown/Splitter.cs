using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shtik.Rendering.Markdown
{
    public class Splitter : IDisposable
    {
        private readonly StringReader _reader;
        private readonly StringBuilder _builder = new StringBuilder();
        private bool _first = true;

        public Splitter(string markdown)
        {
            _reader = new StringReader(markdown);
        }

        public string ReadNextBlock()
        {
            CheckDisposed();
            if (_reader.Peek() == -1) return null;
            _builder.Clear();
            while (_reader.Peek() >= 0)
            {
                var line = _reader.ReadLine();
                if (line.StartsWith("---"))
                {
                    if (_first)
                    {
                        _first = false;
                        continue;
                    }
                    return _builder.ToString().Trim();
                }
                _builder.AppendLine(line);
                _first = false;
            }
            return _builder.ToString().Trim();
        }

        private void CheckDisposed()
        {
            if (_disposed) throw new ObjectDisposedException("Splitter");
        }

        #region IDisposable Support
        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _reader.Dispose();
            }

            _disposed = true;
        }

        ~Splitter()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}