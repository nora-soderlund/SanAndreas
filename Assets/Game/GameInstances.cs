using UnityEngine;

using Game.Player;
using Game.Ped;
using Game.World;
using Game.Animations;
using Game.Vehicles;

namespace Game {
    internal class GameInstances : MonoBehaviour {
        // technically we could use Camera.main instead of GameInstances.PlayerCamera but
        // this keeps all our instances in a better maintained collection.
        public static GameObject PlayerCamera;

        public void Start() {
            gameObject.AddComponent<GameDevelopment>();

            WorldMap.Initialize();
            AnimationManager.Initialize();
            GameVehicleData.Initialize();

            PlayerCamera = Camera.main.gameObject;
            PlayerCamera.AddComponent<GamePlayerCameraController>();

            GameObject ped = InstantiatePed("gangrl3", new Vector3(0, 3f, 0));
            //ped.AddComponent<GamePlayerPedController>();
            //PlayerCamera.transform.SetParent(ped.transform);

            Cursor.lockState = CursorLockMode.Locked;

            GameObject tractor = InstantiateVehicle("tractor", new Vector3(5, 5, 0));
            GameObject premier = InstantiateVehicle("premier", new Vector3(10, 5, 0));
            GameObject bus = InstantiateVehicle("bus", new Vector3(15, 5, 0));
            GameObject rhino = InstantiateVehicle("rhino", new Vector3(20, 5, 0));
            GameObject linerun = InstantiateVehicle("linerun", new Vector3(25, 5, 0));

            GameObject clover = InstantiateVehicle("clover", new Vector3(30, 5, 0));
            clover.AddComponent<GamePlayerVehicleController>();
            PlayerCamera.transform.SetParent(clover.transform);

            //player.transform.SetParent(clover.transform);
            //player.transform.localPosition = Vector3.zero;
        }

        public static GameObject InstantiatePed(string model, Vector3 position) {
            WorldMapData.GetGameObject(model, out GameObject skin);
            GameObject ped = Instantiate(skin, position, new Quaternion());
            ped.AddComponent<GamePedBonesComponent>();
            ped.AddComponent<GamePedAnimationComponent>();
            ped.AddComponent<GamePedMovementComponent>();

            GameObject pedRoot = GameObjectHelper.FindChild(ped, "Root");
            pedRoot.transform.rotation = Quaternion.Euler(-90, 0, 90);

            return ped;
        }

        public static GameObject InstantiateVehicle(string model, Vector3 position) {
            WorldMapData.GetGameObject(model, out GameObject skin);
            GameObject vehicle = Instantiate(skin, position, new Quaternion());
            vehicle.AddComponent<GameVehicleComponent>();

            return vehicle;
        }

        public void Update() {
            WorldMap.Update();
        }
    }
}
