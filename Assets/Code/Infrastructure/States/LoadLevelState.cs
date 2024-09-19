using Code.CameraLogic;
using Code.Infrastructure.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayershipPath = "Prefabs/PlayerShip";
        private const string HUDPath = "Hud/HUD";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }
        

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject player = Instantiate(PlayershipPath, at: initialPoint.transform.position);
            
            Instantiate(HUDPath);
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject target)
        {
            Camera.main?
                .GetComponent<CameraFollow>()
                .Follow(target);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at , Quaternion.identity);
        }
    }
}