//MIT License

//Copyright(c) 2016-2018 Peter Kirmeier

//Permission Is hereby granted, free Of charge, to any person obtaining a copy
//of this software And associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
//copies of the Software, And to permit persons to whom the Software Is
//furnished to do so, subject to the following conditions:

//The above copyright notice And this permission notice shall be included In all
//copies Or substantial portions of the Software.

//THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
//IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
//LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
//OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HitCounterManager
{
    //[Serializable]
    //public class TestType
    //{
    //    public string test;
    //}

    /// <summary>
    /// Loads and Saves data from/to XML files
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class SaveModule<T> where T : class
    {
        private string _Filename;
        private XmlSerializer xml;

        /// <summary>
        /// Creates a Savemodule object
        /// </summary>
        /// <param name="Filename">Filename that is bound for storing the data. Also used for reading if default file is used.</param>
        public SaveModule(string Filename)
        {
            _Filename = Filename;
            xml = new XmlSerializer(typeof(T));
        }

        /// <summary>
        /// Reads the data from the XML file
        /// </summary>
        /// <param name="Filename">File being read</param>
        /// <returns>Data of data type</returns>
        public T ReadXML(string Filename = null)
        {
            StreamReader file = null;
            if (null == Filename) Filename = _Filename;
            try
            {
                file = new StreamReader(Filename);
                T result = (T)xml.Deserialize(file);
                file.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (null != file) file.Close();
                if (ex.HResult != unchecked((int)0x80070002))
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "==> Using defaults", "Error loading settings!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Writes data to the bound XML file
        /// </summary>
        /// <param name="data">Data to be written</param>
        /// <returns>Success state</returns>
        public bool WriteXML(T data)
        {
            StreamWriter file = null;
            try
            {
                file = new StreamWriter(_Filename);
                xml.Serialize(file, data);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (null != file) file.Close();
                MessageBox.Show(ex.Message, "Error writing settings!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
