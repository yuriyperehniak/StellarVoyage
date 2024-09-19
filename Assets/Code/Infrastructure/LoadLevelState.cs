using System;
using Code.CameraLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayershipPath = "Prefabs/PlayerShip";
        private const string HUDPath = "Prefabs/HUD";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject player = Instantiate(PlayershipPath, at: initialPoint.transform.position);
            
            Instantiate(HUDPath);
            CameraFollow(player);
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