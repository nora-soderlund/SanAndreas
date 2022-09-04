using System.Collections.Generic;

using Game.Data;
using Game.Data.Types;

namespace Game.Vehicles {
    class GameVehicleData {
        private static readonly string path = "Assets/Data/vehicles.ide";
        public static List<IdeCar> Cars = new List<IdeCar>();
        public static List<CfgHandling> Handling = new List<CfgHandling>();

        public static void Initialize() {
            using(IdeReader reader = new IdeReader(path)) {
                while(reader.ReadCars(out IdeCar ideCar))
                    Cars.Add(ideCar);
            }

            using(CfgReader reader = new CfgReader("Assets/Data/handling.cfg")) {
                while(reader.TryReadHandling(out CfgHandling cfgHandling))
                    Handling.Add(cfgHandling);
            }
        }
    }
}