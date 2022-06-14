using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public GameObject quitPanel;

        public MapConfig config;
        public MapView view;

        public Map CurrentMap { get; private set; }

        private void Start()
        {
            // PlayerPrefs를 이용하여 맵 상태 저장
            if (PlayerPrefs.HasKey("Map"))
            {
                var mapJson = PlayerPrefs.GetString("Map");
                var map = JsonConvert.DeserializeObject<Map>(mapJson);
                // using this instead of .Contains()
                // .Contains() 대신 이것을 사용
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
                {
                    // player has already reached the boss, generate a new map
                    // 플레이어가 이미 보스에게 도달했을시 새 맵을 생성함
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // player has not reached the boss yet, load the current map
                    // 플레이어가 아직 보스에게 도착하지 않을 시 현재 맵을 불러옴
                    view.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        // 맵 생성 함수
        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            //Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        // 맵 저장 함수
        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(CurrentMap);
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        // 게임을 종료했을 때 실행
        private void OnApplicationQuit()
        { 
            // 맵을 새로 생성하고 저장한 뒤 종료
            // 나중에 현재 맵 저장할 것인지 묻는 창을 띄울 예정
            GenerateNewMap();
            SaveMap();
        }
        
    }
}
