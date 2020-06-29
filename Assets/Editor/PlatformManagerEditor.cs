﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Runtime.Platforms;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;

[CustomEditor(typeof(PlatformManager))]
public class PlatformManagerEditor : Editor
{
    private string[] _options = new string[0];
    private string[] _bottomOptions = new string[0];
    private string[] _halvesPaths;
    private int _firstPopupIndex = 0;
    private int _secondPopupIndex = 0;
    private bool _canPlatformTurn = false;

    private void OnEnable()
    {
        _canPlatformTurn = ((PlatformManager) target).canTurn;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        PopulatePlatformsList();
        
        _firstPopupIndex = EditorGUILayout.Popup("Top Half", _firstPopupIndex, _options);

        if (ShouldShowSecondHalfDropDown(_firstPopupIndex)) UpdateBottomOptions(_options[_firstPopupIndex]);

        var generateButton = GUILayout.Button("Generate");
        if (generateButton)
        {
            var platformManager = (PlatformManager)target;
            var platformTransform = platformManager.transform;

            var spawnedHalves = new GameObject[platformTransform.childCount];
            for (int i = 0; i < platformTransform.childCount; i++)
                spawnedHalves[i] = platformTransform.GetChild(i).gameObject;

            foreach (var spawnedHalve in spawnedHalves.Where((half) => !half.name.Contains("Blocker")))
                DestroyImmediate(spawnedHalve);

            for (int i = 0; i < platformManager.turned.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(platformManager.turned, i);
            }

            var topAssetPath = _halvesPaths[_firstPopupIndex];
            var firstObject = InstantiateHalf(topAssetPath, platformTransform);
            
            if (firstObject.name.Contains("Wall"))
            {
                var wallPlatform = firstObject.GetComponent<WallPlatform>();
                UnityEventTools.AddPersistentListener(platformManager.turned, wallPlatform.OnTurned);
            }

            if (ShouldShowSecondHalfDropDown(_firstPopupIndex))
            {
                var bottomAssetPath = _halvesPaths
                    .First((path) => path.Contains(_bottomOptions[_secondPopupIndex]));
                var secondObject = InstantiateHalf(bottomAssetPath, platformTransform);

                secondObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);

                if (secondObject.name.Contains("Wall"))
                {
                    var wallPlatform = secondObject.GetComponent<WallPlatform>();
                    UnityEventTools.AddPersistentListener(platformManager.turned, wallPlatform.OnTurned);
                }
            }
            
            EditorUtility.SetDirty(target);
        }
    }

    private GameObject InstantiateHalf(string assetPath, Transform platformTransform)
    {
        var halfAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
        var halfObject = PrefabUtility.InstantiatePrefab(halfAsset) as GameObject;
        
        if (halfObject == null) return halfObject;
        halfObject.transform.SetParent(platformTransform);
        halfObject.transform.localPosition = Vector3.zero;
        halfObject.transform.localRotation = Quaternion.identity;

        return halfObject;
    }

    private bool ShouldShowSecondHalfDropDown(int firstIndex)
    {
        var firstOption = _options[firstIndex];
        return !firstOption.Contains("Road") && !firstOption.Contains("Move");
    }

    private void UpdateBottomOptions(string firstOption)
    {
        var options = _options;
        
        if(firstOption.Contains("Glass"))
            options = _options
                .Where((option) => ValidateConditions(option, "Wall", "FixedShock"))
                .ToArray();
        else if(firstOption.Contains("FixedShock"))
            options = _options
                .Where((option) => ValidateConditions(option, "FixedShock", "Glass"))
                .ToArray();
        else if(firstOption.Contains("Wall"))
            options = _options
                .Where((option) => ValidateConditions(option, "Glass"))
                .ToArray();
        
        _bottomOptions = options;
        _secondPopupIndex = EditorGUILayout.Popup("Bottom Half", _secondPopupIndex, options.ToArray());
    }

    private bool ValidateConditions(string value, params string[] acceptConditions)
    {
        foreach (var condition in acceptConditions)
        {
            if (value.Contains(condition)) return true;
        }

        return false;
    }

    private void PopulatePlatformsList()
    {
        var guidList = AssetDatabase.FindAssets("", new [] {"Assets/Prefabs/HalfPlatforms"});
        if (_options != null && _options.Length == guidList.Length) return;
        
        _options = new string[guidList.Length];
        _halvesPaths = new string[guidList.Length];

        for (var i = 0; i < guidList.Length; i++)
        {
            var guid = guidList[i];

            var path = AssetDatabase.GUIDToAssetPath(guid);
            _halvesPaths[i] = path;
            _options[i] = Path.GetFileNameWithoutExtension(path);
        }
    }
}
