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
using Cube.Log;
using NUnit.Framework;
using System.Reflection;

namespace Cube.Pdf.Tests.Pinstaller
{
    /* --------------------------------------------------------------------- */
    ///
    /// GlobalSetup
    ///
    /// <summary>
    /// Represents the initialization of tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [SetUpFixture]
    public class GlobalSetup
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// OneTimeSetup
        ///
        /// <summary>
        /// Executes only once.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger.Configure();
            Logger.ObserveTaskException();
            Logger.Info(typeof(GlobalSetup), Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}