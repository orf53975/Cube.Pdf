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
using Cube.FileSystem.TestService;
using Cube.Log;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace Cube.Pdf.Tests.Pinstaller
{
    /* --------------------------------------------------------------------- */
    ///
    /// DeviceFixture
    ///
    /// <summary>
    /// Provides functionality to test device installing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class DeviceFixture : FileFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified test. When the RPC server is not
        /// available, the test result is ignored.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected T Invoke<T>(Func<T> test)
        {
            try { return test(); }
            catch (Win32Exception e)
            {
                var message = $"{e.Message} ({e.ErrorCode})";
                this.LogWarn(message, e);
                Assert.Ignore(message);
            }
            return default(T);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified test. When the RPC server is not
        /// available, the test result is ignored.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Invoke(Action test) => Invoke(() =>
        {
            test();
            return true;
        });

        #endregion
    }
}
