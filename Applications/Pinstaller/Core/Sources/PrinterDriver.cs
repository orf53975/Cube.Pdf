﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Cube.Pdf.App.Pinstaller
{
    /* --------------------------------------------------------------------- */
    ///
    /// PrinterDriver
    ///
    /// <summary>
    /// Provides functionality to install or uninstall a printer driver.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PrinterDriver : IInstallable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PrinterDriver
        ///
        /// <summary>
        /// Initializes a new instance of the PrinterDriver class with
        /// the specified name.
        /// </summary>
        ///
        /// <param name="name">Name of the printer driver.</param>
        ///
        /* ----------------------------------------------------------------- */
        public PrinterDriver(string name) : this(name, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PrinterDriver
        ///
        /// <summary>
        /// Initializes a new instance of the PrinterDriver class with
        /// the specified name.
        /// </summary>
        ///
        /// <param name="name">Name of the printer driver.</param>
        /// <param name="force">
        /// Value indicating whether to forcibly create an object
        /// ignoring any exceptions.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public PrinterDriver(string name, bool force) :
            this(new DriverInfo3 { cVersion = 3, pDefaultDataType = "RAW" })
        {
            var obj = this.GetOrDefault(GetElements, name, force);
            Exists = (obj != null);
            if (Exists) _core = obj._core;
            else
            {
                Name = name;
                Environment = this.GetEnvironment();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PrinterDriver
        ///
        /// <summary>
        /// Initializes a new instance of the PrinterDriver class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private PrinterDriver(DriverInfo3 core) { _core = core; }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the name of the printer drvier.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name
        {
            get => _core.pName;
            private set => _core.pName = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileName
        ///
        /// <summary>
        /// Gets or sets the name of the drvier file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FileName
        {
            get => _core.pDriverPath;
            set => _core.pDriverPath = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MonitorName
        ///
        /// <summary>
        /// Gets or set the name of the port monitor.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string MonitorName
        {
            get => _core.pMonitorName;
            set => _core.pMonitorName = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Environment
        ///
        /// <summary>
        /// Gets the name of architecture (Windows NT x86 or Windows x64).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Environment
        {
            get => _core.pEnvironment;
            private set => _core.pEnvironment = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Config
        ///
        /// <summary>
        /// Gets the name of config file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Config
        {
            get => _core.pConfigFile;
            set => _core.pConfigFile = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        ///
        /// <summary>
        /// Gets the name of data file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Data
        {
            get => _core.pDataFile;
            set => _core.pDataFile = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Help
        ///
        /// <summary>
        /// Gets the name of help file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Help
        {
            get => _core.pHelpFile;
            set => _core.pHelpFile = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dependencies
        ///
        /// <summary>
        /// Gets the name of dependency files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Dependencies
        {
            get => _core.pDependentFiles;
            set => _core.pDependentFiles = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// Gets the value indicating whether the printer driver has been
        /// already installed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets the default path that driver resources are installed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName => _directory ?? (_directory = GetDirectory());

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements
        ///
        /// <summary>
        /// Gets the collection of currently installed printer drivers.
        /// </summary>
        ///
        /// <returns>Collection of printer drivers.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<PrinterDriver> GetElements()
        {
            if (GetEnumApi(IntPtr.Zero, 0, out var bytes, out var _)) return null;
            if (Marshal.GetLastWin32Error() != 122) throw new Win32Exception();

            var ptr = Marshal.AllocHGlobal((int)bytes);
            try
            {
                if (GetEnumApi(ptr, bytes, out var __, out var n)) return Convert(ptr, n);
                else throw new Win32Exception();
            }
            finally { Marshal.FreeHGlobal(ptr); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Install
        ///
        /// <summary>
        /// Installs the printer driver.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Install()
        {
            if (Exists) return;
            if (!NativeMethods.AddPrinterDriver(Name, 3, ref _core)) throw new Win32Exception();
            Exists = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Uninstall
        ///
        /// <summary>
        /// Uninstalls the printer driver.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Uninstall()
        {
            if (!Exists) return;
            if (!NativeMethods.DeletePrinterDriver("", Environment, Name)) throw new Win32Exception();
            Exists = false;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectory
        ///
        /// <summary>
        /// Gets the default path that driver resources are installed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetDirectory()
        {
            if (GetDirectoryApi(null, 0, out var bytes)) return string.Empty;
            if (Marshal.GetLastWin32Error() != 122) throw new Win32Exception();

            var sb = new StringBuilder((int)bytes);
            if (GetDirectoryApi(sb, bytes, out var _)) return sb.ToString();
            else throw new Win32Exception();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoryApi
        ///
        /// <summary>
        /// Executes the API of getting the driver directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static bool GetDirectoryApi(StringBuilder src, uint n, out uint bytes) =>
            NativeMethods.GetPrinterDriverDirectory("", "", 1, src, n, out bytes);

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnumApi
        ///
        /// <summary>
        /// Executes the API of getting currently installed printer
        /// drivers.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static bool GetEnumApi(IntPtr src, uint n, out uint bytes, out uint count) =>
            NativeMethods.EnumPrinterDrivers("", "", 3, src, n, out bytes, out count);

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts unmanaged resources to the PrinterDriver collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<PrinterDriver> Convert(IntPtr src, uint n)
        {
            var dest = new List<PrinterDriver>();
            var ptr = src;

            for (var i = 0; i < n; ++i)
            {
                var e = (DriverInfo3)Marshal.PtrToStructure(ptr, typeof(DriverInfo3));
                dest.Add(new PrinterDriver(e) { Exists = true });
                ptr = IntPtr.Add(ptr, Marshal.SizeOf(typeof(DriverInfo3)));
            }

            return dest;
        }

        #endregion

        #region Fields
        private DriverInfo3 _core;
        private string _directory;
        #endregion
    }
}
