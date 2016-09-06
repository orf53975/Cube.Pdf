﻿/* ------------------------------------------------------------------------- */
///
/// EventAggregator.cs
///
/// Copyright (c) 2010 CubeSoft, Inc.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
namespace Cube.Pdf.App.Picker
{
    /* --------------------------------------------------------------------- */
    ///
    /// EventAggregator
    /// 
    /// <summary>
    /// CubePDF ImagePicker で発生するイベントを集約するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class EventAggregator
    {
        #region EventArgs

        /* ----------------------------------------------------------------- */
        ///
        /// All
        ///
        /// <summary>
        /// 全ての画像を表します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueEventArgs<int[]> All = new ValueEventArgs<int[]>(null);

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 画像を保存するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<ValueEventArgs<int[]>> Save { get; }
            = new RelayEvent<ValueEventArgs<int[]>>();

        /* ----------------------------------------------------------------- */
        ///
        /// SaveComplete
        ///
        /// <summary>
        /// 画像の保存が完了した事を表すイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent SaveComplete { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// Preview
        ///
        /// <summary>
        /// プレビュー画面を表示するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent Preview { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// PreviewImage
        ///
        /// <summary>
        /// 画像のプレビューを表示するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent PreviewImage { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// Remove
        ///
        /// <summary>
        /// 画像を一覧から削除するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent Remove { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を表示するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent Version { get; } = new RelayEvent();

        #endregion
    }
}