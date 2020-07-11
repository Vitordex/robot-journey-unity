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
    private static Vector3 _gridSize;
    private static bool _snapInPlayingMode;
    private static bool _snapX;
    private static bool _snapY;
    private static bool _snapZ;

    private bool _typeSet = false;

    static PlatformManagerEditor()
    {
        _gridSize = new Vector3(1.6f, 1f, 1.6f);
        _snapX = _snapZ = true;
    }

    private void OnEnable()
    {
        _canPlatformTurn = ((PlatformManager) target).canTurn;
    }

    private void OnSceneGUI() 
    {
        SnapPlatform();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        PopulatePlatformsList();
        var platformManager = (PlatformManager)target;
        FillIndexes(platformManager);

        _firstPopupIndex = EditorGUILayout.Popup("Top Half", _firstPopupIndex, _options);

        if (ShouldShowSecondHalfDropDown(_firstPopupIndex)) UpdateBottomOptions(_options[_firstPopupIndex]);

        var generateButton = GUILayout.Button("Generate");
        if (generateButton)
        {
            platformManager = (PlatformManager)target;
            var platformPivotTransform = platformManager.transform.GetChild(0);
            if (!platformPivotTransform.name.Contains("Pivot"))
            {
                var platformTransform = platformManager.transform;
                for (int i = 0; i < platformTransform.childCount; i++)
                {
                    var child = platformTransform.GetChild(i);
                    if (child.name.Contains("Pivot")) platformPivotTransform = child;
                }
            }

            var spawnedHalves = new GameObject[platformPivotTransform.childCount];
            for (int i = 0; i < platformPivotTransform.childCount; i++)
                spawnedHalves[i] = platformPivotTransform.GetChild(i).gameObject;

            foreach (var spawnedHalve in spawnedHalves.Where((half) => !half.name.Contains("Blocker")))
                DestroyImmediate(spawnedHalve);

            for (int i = 0; i < platformManager.turned.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(platformManager.turned, i);
            }

            var topAssetPath = _halvesPaths[_firstPopupIndex];
            var platformType = $"{_firstPopupIndex}.";
            var firstObject = InstantiateHalf(topAssetPath, platformPivotTransform);
            firstObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            
            if (firstObject.name.Contains("Wall"))
            {
                var wallPlatform = firstObject.GetComponent<WallPlatform>();
                UnityEventTools.AddPersistentListener(platformManager.turned, wallPlatform.OnTurned);
            }

            if (ShouldShowSecondHalfDropDown(_firstPopupIndex))
            {
                var bottomAssetPath = _halvesPaths
                    .First((path) => path.Contains(_bottomOptions[_secondPopupIndex]));
                platformType += _secondPopupIndex;
                var secondObject = InstantiateHalf(bottomAssetPath, platformPivotTransform);

                secondObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);

                if (secondObject.name.Contains("Wall"))
                {
                    var wallPlatform = secondObject.GetComponent<WallPlatform>();
                    UnityEventTools.AddPersistentListener(platformManager.turned, wallPlatform.OnTurned);
                }
            }

            platformManager.platformType = platformType;
            
            EditorUtility.SetDirty(target);
        }

        GridToolInspectorGUI();
    }

    private void FillIndexes(PlatformManager platformManager)
    {
        var platformType = platformManager.platformType;
        if (string.IsNullOrEmpty(platformType) || _typeSet) return;
        
        var divided = platformType.Split('.');
        _firstPopupIndex = int.Parse(divided[0]);
        if (!string.IsNullOrEmpty(divided[1]))
            _secondPopupIndex = int.Parse(divided[1]);

        _typeSet = true;
    }

    private GameObject InstantiateHalf(string assetPath, Transform platformPivotTransform)
    {
        var halfAsset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        var halfObject = PrefabUtility.InstantiatePrefab(halfAsset) as GameObject;
        
        if (halfObject == null) return halfObject;
        halfObject.transform.SetParent(platformPivotTransform);
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

    private void GridToolInspectorGUI()
    {
        EditorGUILayout.Space(25);
        if(Application.isPlaying)
            _snapInPlayingMode = EditorGUILayout.ToggleLeft("Snap In Playing Mode", _snapInPlayingMode);
        
        var isDisable = !_snapInPlayingMode && EditorApplication.isPlaying;
        EditorGUI.BeginDisabledGroup(isDisable);
        EditorGUILayout.Space(5);
        var layout = EditorStyles.boldLabel;
        EditorGUILayout.LabelField("Grid Config", layout);
        _gridSize= EditorGUILayout.Vector3Field("Grid Size", _gridSize);
        
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Snap Option", layout);
        _snapX = EditorGUILayout.ToggleLeft("Snap X", _snapX);
        _snapY = EditorGUILayout.ToggleLeft("Snap Y", _snapY);
        _snapZ = EditorGUILayout.ToggleLeft("Snap Z", _snapZ);
        EditorGUI.EndDisabledGroup();
    }

    private void SnapPlatform()
    {
        if (!_snapInPlayingMode && EditorApplication.isPlaying) return;

        var platformManager = target as PlatformManager;
        var transform = platformManager.transform;
        var position = transform.position;
        
        if(_snapX)
            position.x = Mathf.Round(position.x / _gridSize.x) * _gridSize.x;
        if(_snapY)
            position.y = Mathf.Round(position.y / _gridSize.y) * _gridSize.y;
        if(_snapZ)
            position.z = Mathf.Round(position.z / _gridSize.z) * _gridSize.z;

        transform.position = position;
    }
}
