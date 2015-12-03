﻿#region Copyright (c) 2015 KEngine / Kelly <http://github.com/mr-kelly>, All rights reserved.

// KEngine - Toolset and framework for Unity3D
// ===================================
// 
// Filename: KDepBuild_AudioSourceAndClip.cs
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

using KEngine;
using KEngine.Editor;
using UnityEditor;
using UnityEngine;

public partial class KDependencyBuild
{
    [DepBuild(typeof (AudioSource))]
    private static void ProcessAudioSource(AudioSource com)
    {
        var audioSource = com;
        if (audioSource.clip != null)
        {
            string audioPath = BuildAudioClip(audioSource.clip);
            KAssetDep.Create<KAudioSourceDep>(audioSource, audioPath);
            audioSource.clip = null;
        }
        else
        {
            Logger.LogWarning("找不到AudioClip在AudioSource... {0}", audioSource.name);
        }
    }

    private static string BuildAudioClip(AudioClip audioClip)
    {
        string assetPath = AssetDatabase.GetAssetPath(audioClip);
        bool needBuild = KAssetVersionControl.TryCheckNeedBuildWithMeta(assetPath);
        if (needBuild)
            KAssetVersionControl.TryMarkBuildVersion(assetPath);

        var result = DoBuildAssetBundle("Audio/Audio_" + audioClip.name, audioClip, needBuild);

        return result.Path;
    }
}