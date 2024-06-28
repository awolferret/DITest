using System.Linq;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants = GameInfrastructure.Constants;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x =>
                        new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterType, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                levelData.InitialPoint = GameObject.FindWithTag(Constants.Initialpoint).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}