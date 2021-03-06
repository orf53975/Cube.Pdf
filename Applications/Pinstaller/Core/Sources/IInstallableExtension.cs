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
using Cube.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Pdf.App.Pinstaller
{
    /* --------------------------------------------------------------------- */
    ///
    /// IInstallableExtension
    ///
    /// <summary>
    /// Provides extended methods for IInstallable implemented classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class IInstallableExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnvironment
        ///
        /// <summary>
        /// Gets the name of current architecture.
        /// </summary>
        ///
        /// <param name="src">IInstaller implementation.</param>
        ///
        /// <returns>Name of architecture.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetEnvironment(this IInstallable src) =>
            (IntPtr.Size == 4) ? "Windows NT x86" : "Windows x64";

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault
        ///
        /// <summary>
        /// Gets the first matched element from the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source object.</param>
        /// <param name="collection">Collection of elements.</param>
        /// <param name="name">Target name.</param>
        /// <param name="ignoreError">Ignore exceptions or not.</param>
        ///
        /// <returns>First matched element.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T GetOrDefault<T>(this IInstallable src, Func<IEnumerable<T>> collection,
            string name, bool ignoreError) where T : IInstallable
        {
            try
            {
                var opt = StringComparison.InvariantCultureIgnoreCase;
                return collection().FirstOrDefault(e => e.Name.Equals(name, opt));
            }
            catch
            {
                if (ignoreError) return default(T);
                else throw;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// Copies the specified file.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="filename">Filename to be copied.</param>
        /// <param name="from">Source directory.</param>
        /// <param name="to">Destination directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Copy(this IO io, string filename, string from, string to) =>
            io.Copy(io.Combine(from, filename), io.Combine(to, filename), true);

        #endregion
    }
}
