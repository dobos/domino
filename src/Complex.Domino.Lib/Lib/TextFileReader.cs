using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex.Domino.Lib
{
    public class TextFileReader : IEnumerable<string[]>, IEnumerator<string[]>
    {
        private TextReader reader;
        private string[] current;

        public string[] Current
        {
            get { return current; }
        }

        public TextFileReader(TextReader reader)
        {
            this.reader = reader;
        }

        #region IEnumerable implementation

        public IEnumerator<string[]> GetEnumerator()
        {
            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
        #region IEnumerator implementation

        public void Dispose()
        {
            reader.Close();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return current; }
        }

        public bool MoveNext()
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("#"))
                {
                    current = line.Split(new char[] { '\t' });
                    return true;
                }
            }

            reader.Close();
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
