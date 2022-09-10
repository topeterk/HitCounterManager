//MIT License

//Copyright (c) 2016-2022 Peter Kirmeier

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HitCounterManager.Models
{
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
        /// <param name="EnableBackup">Ture: A backup will be created when read was successful</param>
        /// <param name="Filename">File being read</param>
        /// <returns>Data of data type</returns>
        public T? ReadXML(bool EnableBackup, string? Filename = null)
        {
            XmlReader? file = null;
            if (null == Filename) Filename = _Filename;
            try
            {
                file = XmlReader.Create(Filename);
                T? result = xml.Deserialize(file) as T;
                file.Close();

                if (EnableBackup) File.Copy(Filename, Filename + ".bak", true); // Create backup on successful read only

                return result;
            }
            catch (FileNotFoundException) { } // Exception.HResult == COR_E_FILENOTFOUND only be available since .Net 4.5, use overloading for older frameworks
            catch (Exception ex)
            {
                App.CurrentApp.DisplayAlert("Error loading settings!", ex.Message + Environment.NewLine + "==> Using defaults");
            }
            finally
            {
                file?.Close();
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
            XmlWriter? file = null;
            try
            {
                file = XmlWriter.Create(_Filename, new XmlWriterSettings()
                {
                    Encoding = Encoding.Unicode, // UTF16LE
                    Indent = true
                });
                xml.Serialize(file, data);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                file?.Close();
                App.CurrentApp.DisplayAlert("Error writing settings!", ex.Message);
            }
            return false;
        }
    }
}
