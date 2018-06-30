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
using System.Runtime.InteropServices;

namespace Cube.Pdf.Pdfium.PdfiumApi
{
    /* --------------------------------------------------------------------- */
    ///
    /// PdfiumApi.NativeMethods
    ///
    /// <summary>
    /// PDFium の API を定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class NativeMethods
    {
        #region Methods

        #region Common

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_AddRef
        ///
        /// <summary>
        /// Increment the reference counter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern void FPDF_AddRef();

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_Release
        ///
        /// <summary>
        /// Decrement the reference counter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern void FPDF_Release();

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetLastError
        ///
        /// <summary>
        /// Get the latest error code.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern uint FPDF_GetLastError();

        #endregion

        #region Document

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_LoadCustomDocument
        ///
        /// <summary>
        /// Load PDF document from a custom access descriptor.
        /// </summary>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, CharSet = CharSet.Ansi)]
        public static extern IntPtr FPDF_LoadCustomDocument(
            [MarshalAs(UnmanagedType.LPStruct)] FileAccess access, string password);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_CloseDocument
        ///
        /// <summary>
        /// Close a loaded PDF document.
        /// </summary>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern void FPDF_CloseDocument(IntPtr document);

        #endregion

        #region Page

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetPageCount
        ///
        /// <summary>
        /// Get total number of pages in the document.
        /// </summary>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern int FPDF_GetPageCount(IntPtr document);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_LoadPage
        ///
        /// <summary>
        /// Load a page inside the document.
        /// </summary>
        ///
        /// <remarks>
        /// page_index parameter inicates the first page as ZERO.
        /// </remarks>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern IntPtr FPDF_LoadPage(IntPtr document, int page_index);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetPageWidth
        ///
        /// <summary>
        /// Get page width.
        /// </summary>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern double FPDF_GetPageWidth(IntPtr page);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetPageHeight
        ///
        /// <summary>
        /// Get page height.
        /// </summary>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern double FPDF_GetPageHeight(IntPtr page);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDFPage_GetRotation
        ///
        /// <summary>
        /// Get the rotation of the page.
        /// </summary>
        ///
        /// <returns>
        /// Returns one of the following indicating the page rotation:
        /// 0 - No rotation.
        /// 1 - Rotated 90 degrees clockwise.
        /// 2 - Rotated 180 degrees clockwise.
        /// 3 - Rotated 270 degrees clockwise.
        /// </returns>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdf_edit.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern int FPDFPage_GetRotation(IntPtr page);

        #endregion

        #region Metadata

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetFileVersion
        ///
        /// <summary>
        /// Get the file version of the given PDF document.
        /// </summary>
        ///
        /// <remarks>
        /// File version: 14 for 1.4, 15 for 1.5, ...
        /// </remarks>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdfview.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern bool FPDF_GetFileVersion(IntPtr document, out int version);

        /* ----------------------------------------------------------------- */
        ///
        /// FPDF_GetMetaText
        ///
        /// <summary>
        /// Get meta-data tag content from document.
        /// </summary>
        ///
        /// <remarks>
        /// The tag can be one of: Title, Author, Subject, Keywords,
        /// Creator, Producer, CreationDate, or ModDate.
        ///
        /// For linearized files, FPDFAvail_IsFormAvail must be called
        /// before this, and it must have returned PDF_FORM_AVAIL or
        /// PDF_FORM_NOTEXIST. Before that, there is no guarantee the
        /// metadata has been loaded.
        /// </remarks>
        ///
        /// <see hcref="https://pdfium.googlesource.com/pdfium/+/master/public/fpdf_doc.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern uint FPDF_GetMetaText(IntPtr document, string tag, byte[] buffer, uint buflen);

        #endregion

        #endregion

        #region Fields
        private const string LibName = "pdfium.dll";
        #endregion
    }
}
