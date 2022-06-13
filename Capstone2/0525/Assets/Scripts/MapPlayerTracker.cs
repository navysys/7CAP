using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; } // 

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                // 플레이어가 아직 노드를 선택하지 않았으면 y = 0인 노드 중 하나를 선택한다.
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            // 여기서도 Blueprint 이름에 액세스할 수 있음.
            //Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            /*
            nodeType을 기준으로 context와 함께 적절한 장면 로드
            또는 지도에 적절한 GUI를 표시한다.
            이러한 경우에 GUI를 표시하도록 선택한 경우 MapPlayerTracker에서 
            "Locked"을 다시 false로 설정하는 것을 잊지 마십시오.

            해석 : 어떤 노드를 눌렀는가에 따라 각기 다른 씬 또는 GUI(팝업)을 띄움.
            GUI를 띄울 때 Locked를 다시 false로 설정할 것.
            */
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    SceneDirector.instance.SceneChange("MinorEnemy");
                    break;
                case NodeType.EliteEnemy:
                    SceneDirector.instance.SceneChange("EliteEnemy");
                    break;
                case NodeType.RestSite:
                    MapPlayerTracker.Instance.Locked = true;
                    GameObject.Find("Canvas").transform.Find("RestSite Panel").gameObject.SetActive(true);
                    GameManager.instance.RecoveryHP();
                    break;
                case NodeType.Treasure:
                    MapPlayerTracker.Instance.Locked = true;
                    GameObject.Find("Canvas").transform.Find("Treasure Panel").gameObject.SetActive(true);
                    CardManager.instance.SetTreasure();
                    break;
                case NodeType.Store:
                    SceneDirector.instance.SceneChange("Store");
                    break;
                case NodeType.Boss:
                    SceneDirector.instance.SceneChange("Boss");
                    break;
                case NodeType.Mystery:
                    SceneDirector.instance.SceneChange("Mystery");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}

