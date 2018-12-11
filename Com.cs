using UnityEngine;
using UnityEngine.SceneManagement;

namespace Customcommand
{
    public class Com : MonoBehaviour
    {
        private Quaternion spawnRotation;
        private Vector3 spawnPosition;

        float x; 
        float y;
        float z;
        public bool friend = false;
        public bool pickup = false;
        public void Awake()
        {
            SceneManager.sceneLoaded += Command;
        }
        public void OnDestroy()
        {
            strat.Awake();
            SceneManager.sceneLoaded -= Command;
        }
        public void Command(Scene off, LoadSceneMode on)
        {
            DevConsole.RegisterConsoleCommand(this, "spse", false);
            DevConsole.RegisterConsoleCommand(this, "pickup", false);
            DevConsole.RegisterConsoleCommand(this, "friend", false);
            DevConsole.RegisterConsoleCommand(this, "playse", false);
            DevConsole.RegisterConsoleCommand(this, "size", false);
            DevConsole.RegisterConsoleCommand(this, "suse", false);
        }
        bool Gettarget(out GameObject targeta)
        {
            Targeting.GetTarget(Player.mainObject, 100f, out GameObject target, out float dist);
            if(target != null)
            {
                targeta = UWE.Utils.GetEntityRoot(target);
                return true;
            }
            targeta = null;
            return false;
        }
        public void OnConsoleCommand_pickup()
        {
            if (Gettarget(out GameObject target))
            {
                if (target.GetComponent<Pickupable>() == null)
                {
                    target.AddComponent<Pickupable>();
                    ErrorMessage.AddDebug(target.GetFullName() + " add pick up");
                }
                else
                {
                    Destroy(target.GetComponent<Pickupable>());
                    ErrorMessage.AddDebug(target.GetFullName() + " remove pick up");
                }
            }
            else
            {
                ErrorMessage.AddDebug("Target not found");
            }

        }
        public void OnConsoleCommand_size(NotificationCenter.Notification n)
        {
            x = 1f;
            y = 1f;
            z = 1f;
            if (n.data.Count == 1)
            {
                x = float.Parse((string)n.data[0]);
                y = float.Parse((string)n.data[0]);
                z = float.Parse((string)n.data[0]);
            }
            if (n.data.Count == 2)
            {
                x = float.Parse((string)n.data[0]);
                y = float.Parse((string)n.data[1]);
                z = float.Parse((string)n.data[1]);
            }
            if (n.data.Count == 3)
            {
                x = float.Parse((string)n.data[0]);
                y = float.Parse((string)n.data[1]);
                z = float.Parse((string)n.data[2]);
            }
            if (Gettarget(out GameObject target))
            {
                string name = target.GetFullName();
                target.transform.localScale = new Vector3(x, y, z);
                ErrorMessage.AddDebug(name + " Size changed complete");
            }
            else
            {
                ErrorMessage.AddDebug("Target not found");
            }
        }
        public void OnConsoleCommand_playse(NotificationCenter.Notification n)
        {
            float si = float.Parse((string)n.data[0]);
            Player.mainObject.transform.localScale = new Vector3(si, si, si);
            ErrorMessage.AddDebug("Warning!!!! This feature may cause you to be unable to move and other issues");
        }
        public void OnConsoleCommand_friend()
        {
            if (Gettarget(out GameObject target) && target.GetComponent<Creature>())
            {
                if (target.GetComponent<Creature>().friend != Player.main.gameObject)
                {
                    target.GetComponent<Creature>().Friendliness.Add(1f);
                    target.GetComponent<Creature>().friend = Player.main.gameObject;
                    ErrorMessage.AddDebug(target.GetFullName() + " set friend");
                }
                else
                {
                    target.GetComponent<Creature>().Friendliness.Value = 0f;
                    target.GetComponent<Creature>().friend = null;
                    ErrorMessage.AddDebug(target.GetFullName() + " remove friend");
                }
            }
            else
            {
                ErrorMessage.AddDebug("Target not found or Not a creature");
            }
        }
        public void OnConsoleCommand_spse(NotificationCenter.Notification n)
        {
            if (n != null && n.data != null && n.data.Count > 0)
            {
                string text = (string)n.data[0];
                TechType techType;
                if (UWE.Utils.TryParseEnum<TechType>(text, out techType))
                {
                    if (CraftData.IsAllowed(techType))
                    {
                        GameObject prefabForTechType = CraftData.GetPrefabForTechType(techType, true);
                        if (prefabForTechType != null)
                        {
                            x = 1f;
                            y = 1f;
                            z = 1f;
                            int dist = 6;
                            int count = 1;
                            if (n.data.Count > 1)
                            {
                                count = int.Parse((string)n.data[1]);
                            }
                            if (n.data.Count > 2)
                            {
                                x = float.Parse((string)n.data[2]);
                            }
                            if (n.data.Count > 3)
                            {
                                y = float.Parse((string)n.data[3]);
                            }
                            if (n.data.Count > 4)
                            {
                                z = float.Parse((string)n.data[4]);
                            }
                            if (n.data.Count > 5)
                            {
                                dist = int.Parse((string)n.data[5]);
                            }
                            if (n.data.Count == 3)
                            {
                                y = float.Parse((string)n.data[2]);
                                z = float.Parse((string)n.data[2]);
                            }
                            if (n.data.Count == 4)
                            {
                                z = float.Parse((string)n.data[3]);
                            }
                            for (int i = 0; i < count; i++)
                            {
                                GameObject gameObject = Utils.CreatePrefab(prefabForTechType, dist, i > 0);
                                UWE.Utils.GetEntityRoot(gameObject).transform.localScale = new Vector3(x, y, z);
                                if (gameObject.GetComponent<Creature>() != null)
                                {
                                    gameObject.AddComponent<mono>().Setsize(x, y, z);
                                }
                                LargeWorldEntity.Register(gameObject);
                                CrafterLogic.NotifyCraftEnd(gameObject, techType);
                                gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                            }
                            return;
                        }
                        ErrorMessage.AddDebug("无法找到 = " + techType);
                        return;
                    }
                }
                else
                {
                    ErrorMessage.AddDebug("无法解析 " + text + " as TechType");
                }
            }
        }
        public void OnConsoleCommand_suse(NotificationCenter.Notification n)
        {
            string text = (string)n.data[0];
            if (text != null && text != string.Empty)
            {
                x = 1f;
                y = 1f;
                z = 1f;
                int dist = 6;
                if (n.data.Count > 1)
                {
                    x = float.Parse((string)n.data[1]);
                }
                if (n.data.Count > 2)
                {
                    y = float.Parse((string)n.data[2]);
                }
                if (n.data.Count > 3)
                {
                    z = float.Parse((string)n.data[3]);
                }
                if (n.data.Count > 4)
                {
                    dist = int.Parse((string)n.data[4]);
                }
                if (n.data.Count == 2)
                {
                    y = float.Parse((string)n.data[1]);
                    z = float.Parse((string)n.data[1]);
                }
                if (n.data.Count == 3)
                {
                    z = float.Parse((string)n.data[2]);
                }
                Transform transform = MainCamera.camera.transform;
                this.spawnPosition = transform.position + dist * transform.forward;
                this.spawnRotation = Quaternion.LookRotation(MainCamera.camera.transform.right);
                LightmappedPrefabs.main.RequestScenePrefab(text, new LightmappedPrefabs.OnPrefabLoaded(this.OnSubPrefabLoaded));
                return;
            }
            ErrorMessage.AddDebug("必须指定名称(cyclops beetle aurora)");
        }
        private void OnSubPrefabLoaded(GameObject prefab)
        {
            GameObject gameObject = Utils.SpawnPrefabAt(prefab, null, this.spawnPosition);
            gameObject.transform.localScale = (new Vector3(x, y, z));
            gameObject.transform.rotation = this.spawnRotation;
            gameObject.SetActive(true);
            gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
            LargeWorldEntity.Register(gameObject);
            CrafterLogic.NotifyCraftEnd(gameObject, CraftData.GetTechType(gameObject));
        }
    }
}
