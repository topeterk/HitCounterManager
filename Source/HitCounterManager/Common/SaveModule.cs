﻿// SPDX-FileCopyrightText: © 2016-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

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
    /// <remarks>
    /// Creates a Savemodule object
    /// </remarks>
    /// <param name="Filename">Filename that is bound for storing the data. Also used for reading if default file is used.</param>
    public class SaveModule<T>(string Filename) where T : class
    {
        private readonly string _Filename = Filename;
        private readonly XmlSerializer xml = new (typeof(T));

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
        /// Reads the data from the XML file (given as string stream)
        /// </summary>
        /// <param name="xmlStringStream">Stream with file contents being read</param>
        /// <returns>Data of data type</returns>
        public T? ReadXML(Stream xmlStringStream)
        {
            XmlReader? file = null;
            try
            {
                file = XmlReader.Create(xmlStringStream);
                T? result = xml.Deserialize(file) as T;
                file.Close();
                return result;
            }
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
                    Encoding = Encoding.UTF8, // UTF-8 BOM
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
