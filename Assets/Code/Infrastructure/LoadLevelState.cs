using System;
using Code.CameraLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
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
            GameObject player = Instantiate("Prefabs/PlayerShip");
            Instantiate("Prefabs/HUD");
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
    }
}