﻿#region Copyright (c) 2015 KEngine / Kelly <http://github.com/mr-kelly>, All rights reserved.

// KEngine - Toolset and framework for Unity3D
// ===================================
// 
// Filename: AppVersion.cs
// Date:     2015/12/03
// Author:  Kelly
// Email: 23110388@qq.com
// Github: https://github.com/mr-kelly/KEngine
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3.0 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library.

#endregion

using System;
using System.Text;

namespace KEngine
{
    public enum AppVersionType
    {
        Alpha,
        Beta,
        Preview,
        Release,
    }

    /// <summary>
    /// For App Version, version string can with description
    /// 1.2.3.10.4567.release.mi
    /// </summary>
    public class AppVersion
    {
        public Version Version { get; private set; }
        public AppVersionType VersionType { get; private set; }
        public string VersionDesc { get; private set; }

        public AppVersion(string versionStr)
        {
            var versionArr = versionStr.Split('.');
            if (versionArr.Length < 4)
                throw new Exception("Version String's length must larger than 4!");
            Version =
                new Version(string.Format("{0}.{1}.{2}.{3}", versionArr[0], versionArr[1], versionArr[2], versionArr[3]));

            if (versionArr.Length >= 5)
            {
                var strVerType = versionArr[4];
                var titleCaseStrVerType =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strVerType); // 首字母大写
                VersionType = (AppVersionType) Enum.Parse(typeof (AppVersionType), titleCaseStrVerType);
            }

            if (versionArr.Length >= 6)
            {
                VersionDesc = versionArr[6];
            }
        }

        /// <summary>
        /// To Version String
        /// eg. 1.2.1.0.alpha.xxx
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}.{1}", Version.ToString(4), VersionType.ToString().ToLower());
            if (!string.IsNullOrEmpty(VersionDesc))
            {
                sb.AppendFormat(".{0}", VersionDesc);
            }
            return sb.ToString();
        }

        /// <summary>
        /// eg.  1.2
        /// </summary>
        /// <returns></returns>
        public string ToVersion2()
        {
            return Version.ToString(2);
        }

        /// <summary>
        /// eg. 1.2.1
        /// </summary>
        /// <returns></returns>
        public string ToVersion3()
        {
            return Version.ToString(3);
        }

        /// <summary>
        /// eg. 1.2.1.0
        /// </summary>
        /// <returns></returns>
        public string ToVersion4()
        {
            return Version.ToString(4);
        }
    }
}