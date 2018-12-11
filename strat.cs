using UnityEngine;

namespace Customcommand
{
    public class strat : MonoBehaviour
    {
        public static GameObject Nelrcoma;
        public static void Awake()
        {
            try
            {
                Destroy(Nelrcoma);
                Nelrcoma = new GameObject("Nelrcoma");
                GameObject.DontDestroyOnLoad(Nelrcoma);
                Nelrcoma.AddComponent<Com>();
            }
            catch
            {
                Nelrcoma = new GameObject("Nelrcoma");
                GameObject.DontDestroyOnLoad(Nelrcoma);
                Nelrcoma.AddComponent<Com>();
            }
        }
    }
}
