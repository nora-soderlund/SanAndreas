using System.Linq;
using System.Collections.Generic;

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
        public static GameObject PlayerPed;
        public static GameObject PlayerVehicle;

        public static List<GameObject> Vehicles = new List<GameObject>();

        public void Start() {
            gameObject.AddComponent<GameDevelopment>();

            WorldMap.Initialize();
            AnimationManager.Initialize();
            GameVehicleData.Initialize();

            PlayerCamera = Camera.main.gameObject;
            PlayerCamera.AddComponent<GamePlayerCameraController>();

            PlayerPed = InstantiatePed("gangrl3", new Vector3(0, 3f, 0));
            PlayerPed.AddComponent<GamePlayerPedController>();
            PlayerCamera.transform.SetParent(PlayerPed.transform);

            Cursor.lockState = CursorLockMode.Locked;

            GameObject tractor = InstantiateVehicle("tractor", new Vector3(5, 5, 0));
            GameObject premier = InstantiateVehicle("premier", new Vector3(10, 5, 0));
            GameObject bus = InstantiateVehicle("bus", new Vector3(15, 5, 0));
            GameObject rhino = InstantiateVehicle("rhino", new Vector3(20, 5, 0));
            GameObject linerun = InstantiateVehicle("linerun", new Vector3(25, 5, 0));
            GameObject clover = InstantiateVehicle("clover", new Vector3(30, 5, 0));
            GameObject turismo = InstantiateVehicle("turismo", new Vector3(35, 8, 0));
            GameObject fbitruck = InstantiateVehicle("fbitruck", new Vector3(40, 8, 0));
            GameObject bullet = InstantiateVehicle("bullet", new Vector3(45, 8, 0));
            GameObject copcarla = InstantiateVehicle("copcarla", new Vector3(50, 8, 0));
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

            Vehicles.Add(vehicle);

            return vehicle;
        }

        public void Update() {
            WorldMap.Update();
        }
    }
}
