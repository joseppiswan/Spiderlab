using System;
using System.Linq;
using MelonLoader;
using UnityEngine;
using System.Reflection;
using System.IO;
using AudioImportLib;
using UnhollowerRuntimeLib;
using SLZ.Marrow.Pool;
using SLZ.Rig;
using SLZ.Props;
using Boneworks;

namespace SpiderlabV2
{
    public static class EmebeddedAssetBundle
    {
        public static AssetBundle LoadFromAssembly(Assembly assembly, string name)
        {
            if (assembly.GetManifestResourceNames().Contains(name))
            {
                MelonLogger.Msg(ConsoleColor.DarkCyan, "Loading embedded bundle " + name + "...");
                byte[] arr;
                using (Stream manifestResourceStream = assembly.GetManifestResourceStream(name))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        manifestResourceStream.CopyTo(memoryStream);
                        arr = memoryStream.ToArray();
                    }
                }
                MelonLogger.Msg(ConsoleColor.DarkCyan, "Done!");
                return AssetBundle.LoadFromMemory(arr);
            }
            return null;
        }
    }
    public class Main : MelonMod
    {
        float _LowestLimit = float.PositiveInfinity;
        public LineRenderer rrend;
        public LineRenderer lrend;
        public GameObject light;
        private ConfigurableJoint rjoint;
        private ConfigurableJoint ljoint;
        private Vector3 rtarget;
        private Vector3 ltarget;
        private bool rgrappled = false;
        private bool lgrappled = false;
        private Material webMat;
        private MelonPreferences_Category categ;
        private MelonPreferences_Entry<float> pull;
        public static AssetBundle assets;
        public AudioSource source;
        public GameObject webobj;

        public override void OnInitializeMelon()
        {
            categ = MelonPreferences.CreateCategory("Webs");
            pull = categ.CreateEntry<float>("PullStrength", 1500f);
            //pull = categ.CreateEntry<float>("WebThickness", 0.1f);
        }
        public LineRenderer setupRender()
        {
            if (!light)
            {
                //light = 
            }
            PhysicsRig rig = GameObject.FindObjectsOfType<PhysicsRig>().First();
            //string assetsPath = Path.Combine(MelonUtils.UserDataDirectory, "WebAssets");
            //var bundle = AssetBundle.LoadFromFile(assetsPath + "/werbthing.webobj");
            //GameObject webobj = (GameObject)bundle.mainAsset;
            //UnityEngine.Object asset = 

            //webobj = bundle.LoadAsset("webObj", Il2CppType.Of<GameObject>()).Cast<GameObject>();
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.name = "_SPIDERMAN-MOD-RENDERER";
            temp.transform.localScale = Vector3.one / 10;
            temp.transform.position = rig.rightHand.transform.position;
                 LineRenderer r = temp.AddComponent<LineRenderer>();
            //r.realtimeLightmapIndex = 1;
            //GameObject h = UnityEngine.Object.Instantiate(webobj);
            //Material mat = UnityEngine.Object.Instantiate(webobj.GetComponent<Renderer>().material);//webMaterial();//new Material(Resources.FindObjectsOfTypeAll<Shader>().FirstOrDefault(m => m.name == "Standard"));
            //    Shader original = Resources.FindObjectsOfTypeAll<Material>().First().shader;

            //  Material mat = new Material(original);//Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard").shader != null ? Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard").shader : Shader.Find(""));
            //    mat.mainTexture = null;
            //    mat.color = Color.white;
            //   r.material = mat;//webMaterial();
            //            LoggerInstance.Msg(mat.shader.name);

            //LoggerInstance.Msg(Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.name == "ZPEFM_lighting") == null);
            //if(Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.name == "ZPEFM_lighting") == null)
            //{
            //    r.material = constMat();

            //}
            //else
            //{
            //    r.material = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.name == "ZPEFM_lighting");
            //}
            Material m = webMaterial(); // new Material(Shader.Find("Universal Render Pipeline/Lit (PBR Workflow)")); //new Material(webMaterial().shader);//Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(mm => mm.shader.name == "Universal Render Pipeline/Lit (PBR Workflow)").shader);//UnityEngine.Object.Instantiate(webobj.GetComponent<Renderer>().material).shader);
            //m.color = Color.white;
            r.material = m;
            //m.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
            //m.SetInt("_SmoothnessTextureChannel", 0);
            //m.SetFloat("_Metallic", 0f);
            //m.SetFloat("_GlossMapScale", 0f);
            temp.GetComponent<Renderer>().material = m;
            r.SetWidth(0.22f, 0.22f); //0.02f
            //LineRenderer clone = UnityEngine.Object.Instantiate(webobj.GetComponent<LineRenderer>());
            return r; //temp.GetComponent<LineRenderer>();
        }
        public Material constMat()
        {
            Material zpefm = null;
            AssetSpawner spawner = Resources.FindObjectsOfTypeAll<AssetSpawner>().First();
            Constrainer con = new Constrainer();
            //GameObject temp1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //GameObject temp2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //temp1.AddComponent<Rigidbody>();
            //temp1.isStatic = true;
            //temp2.AddComponent<Rigidbody>();
            //temp2.isStatic = true;
                //foreach (var a in spawner._poolList)
                //    foreach (var b in a.objects)
                //    {
                //        if (b.name.Contains("Constrainer"))
                //        {
                //            a.Spawn(rig.rightHand.transform.position);
                //            UnityEngine.Object.Instantiate(b.gameObject, rig.rightHand.transform.position, Quaternion.Euler(0, 0, 0));//new Vector3(0, 25000, 0)
                //            con = Resources.FindObjectsOfTypeAll<Constrainer>().First();
                //            zpefm = new Material(con.LineMaterial);
                //            LoggerInstance.Warning($"ZPEFM NULL:{zpefm == null}");
                //        }
                //        LoggerInstance.Warning(b.name);
                //    }
            //if (con!=null)
            //{
            //    con.JointTether(temp1.GetComponent<Rigidbody>(), temp2.GetComponent<Rigidbody>(), new Vector3(0, 25000, 0), new Vector3(0, 25000, 0));
            //    zpefm = con.LineMaterial;
            //}
            return con.LineMaterial;
        }
        public Material webMaterial()
        {
            string assetsPath = Path.Combine(MelonUtils.UserDataDirectory, "WebAssets");
            var bundle = AssetBundle.LoadFromFile(assetsPath + "/spiderfx.sres");
            //GameObject webobj = (GameObject)bundle.mainAsset;
            //UnityEngine.Object asset = 
            GameObject webobj = bundle.LoadAsset("SpiderWeb_Renderer", Il2CppType.Of<GameObject>()).Cast<GameObject>();
            GameObject h = UnityEngine.Object.Instantiate(webobj);
            //GameObject webobj = (GameObject) UnityEngine.Object.Instantiate<UnityEngine.Object>(bundle.LoadAsset("webObj"));
            DrawLine2Points p = h.GetComponent<DrawLine2Points>();
            Material wm = p.Liner.material;
            wm.hideFlags = HideFlags.DontUnloadUnusedAsset;
            //wm.color = Color.white;
            LoggerInstance.Msg(wm.shader.name);
            bundle.Unload(false);
            return wm;
        }


        public override void OnLateUpdate()
        {
            line();
        }

        private void line()
        {
            PhysicsRig rig = GameObject.FindObjectsOfType<PhysicsRig>().First();
            if (rjoint)
            {
                rrend.SetPositions(new Vector3[] { rig.rightHand.transform.position, rtarget });
            }
            if (ljoint)
            {
                lrend.SetPositions(new Vector3[] { rig.leftHand.transform.position, ltarget });
            }
        }

        public override void OnUpdate()
        {
            if (GameObject.FindObjectsOfType<PhysicsRig>().First() == null) return;
            PhysicsRig rig = GameObject.FindObjectsOfType<PhysicsRig>().First();
            bool hands = rig.rightHand != null && rig.leftHand != null;
            if (hands)
            {
                BaseController l = GameObject.FindObjectOfType<RigManager>().ControllerRig.leftController;
                BaseController r = GameObject.FindObjectOfType<RigManager>().ControllerRig.rightController;
                LoggerInstance.Msg(r.GetGripForce());
                if (rjoint != null && rtarget != null)
                {
                }
                if (ljoint && ltarget != null)
                {

                }
                if (r.GetThumbStickDown() && r.GetGripForce() > 0.5f)
                {
                    //LoggerInstance.Msg(AssetDatabase Resources.FindObjectsOfTypeAll<Material>().First().shader.);
                    if (!rrend)
                    {
                        rrend = setupRender();
                    }
                    rgrappled = !rgrappled;
                    if (rgrappled)
                    {
                        RaycastHit h;
                        if (Physics.Raycast(rig.rightHand.gameObject.transform.position + rig.rightHand.gameObject.transform.forward / 6, rig.rightHand.gameObject.transform.forward, out h, 1000f) && !rjoint)
                        {
                            try 
                            {
                                try
                                {
                                    //rrend.material = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard") != null ? Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard") : rrend.material;
                                }
                                catch (Exception) { }
                                rjoint = rig.rightHand.gameObject.AddComponent<ConfigurableJoint>();
                                LoggerInstance.Msg(LayerMask.LayerToName(rig.rightHand.gameObject.layer));
                                GameObject c = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                c.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                c.transform.position = h.point;
                                c.GetComponent<MeshRenderer>().material.color = Color.magenta;
                                Rigidbody rigidbody = (!h.rigidbody || h.rigidbody.isKinematic) ? null : h.rigidbody;
                                if (h.rigidbody)
                                {
                                    rjoint.connectedBody = h.rigidbody;
                                }
                                float num = rigidbody ? (Mathf.Max(1f, rigidbody.mass) * 10f) : 10f;
                                SetWorldAnchor(rjoint, rig.rightHand.transform.position);
                                SetWorldConnected(rjoint, h.point);
                                SetLinearMotion(rjoint, ConfigurableJointMotion.Limited);
                                SetLinearLimit(rjoint, h.distance + 0.1f, 0f);
                                SetSpringLimit(rjoint, 500000000f, 500000f * num * 1);
                                SetSpringDrive(rjoint, pull.Value, pull.Value, pull.Value);
                                rjoint.enableCollision = true;
                                //rrend.gameObject.SetActive(true);
                                rtarget = h.point;
                                rrend.positionCount = 2;
                                string customAudioPath = Path.Combine(MelonUtils.UserDataDirectory, "WebSounds");
                                AudioClip sound = API.LoadAudioClip(customAudioPath + "/webSound.wav",true);
                                source = rig.rightHand.gameObject.GetComponent<AudioSource>() ? rig.rightHand.gameObject.GetComponent<AudioSource>() : rig.rightHand.gameObject.AddComponent<AudioSource>();
                                source.pitch = UnityEngine.Random.Range(0.87f, 1.1f);
                                source.PlayOneShot(sound, 1f);
                            }
                            catch (Exception ex)
                            {
                                UnityEngine.Object.Destroy(rjoint);
                                LoggerInstance.Warning(ex.Data);
                            }
                            LoggerInstance.Msg(rjoint == null);
                            LoggerInstance.Msg(rrend == null);
                            LoggerInstance.Msg(rtarget.ToString());
                        }
                    }
                    else if (!rgrappled)
                    {
                        rrend.positionCount = 0;
                        UnityEngine.Object.Destroy(rjoint);

                    }
                }
                if (l.GetThumbStickDown() && l.GetGripForce() > 0.5f)
                {
                    if (!lrend)
                    {
                        lrend = setupRender();
                    }
                    lgrappled = !lgrappled;
                    if (lgrappled)
                    {
                        RaycastHit h;
                        if (Physics.Raycast(rig.leftHand.gameObject.transform.position + rig.leftHand.gameObject.transform.forward / 6, rig.leftHand.gameObject.transform.forward, out h, 1000f) && !ljoint)
                        {
                            try
                            {
                                try
                                {
                                    //lrend.material = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard") != null ? Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.shader.name == "Standard") : lrend.material;
                                }
                                catch (Exception) { }
                                ljoint = rig.leftHand.gameObject.AddComponent<ConfigurableJoint>();
                                LoggerInstance.Msg(LayerMask.LayerToName(rig.leftHand.gameObject.layer));
                                GameObject c = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                c.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                c.transform.position = h.point;
                                c.GetComponent<MeshRenderer>().material.color = Color.magenta;
                                Rigidbody rigidbody = (!h.rigidbody || h.rigidbody.isKinematic) ? null : h.rigidbody;
                                if (h.rigidbody)
                                {
                                    ljoint.connectedBody = h.rigidbody;
                                }
                                float num = rigidbody ? (Mathf.Max(1f, rigidbody.mass) * 10f) : 10f;
                                SetWorldAnchor(ljoint, rig.leftHand.transform.position);
                                SetWorldConnected(ljoint, h.point);
                                SetLinearMotion(ljoint, ConfigurableJointMotion.Limited);
                                SetLinearLimit(ljoint, h.distance + 0.1f, 0f);
                                SetSpringLimit(ljoint, 500000000f, 500000f * num * 1);
                                SetSpringDrive(ljoint, pull.Value, pull.Value, pull.Value);
                                ljoint.enableCollision = true;
                                //lrend.gameObject.SetActive(true);
                                ltarget = h.point;
                                lrend.positionCount = 2;
                                string customAudioPath = Path.Combine(MelonUtils.UserDataDirectory, "WebSounds");
                                AudioClip sound = API.LoadAudioClip(customAudioPath + "\\webSound.wav", true);
                                source = rig.leftHand.gameObject.GetComponent<AudioSource>() ? rig.leftHand.gameObject.GetComponent<AudioSource>() : rig.leftHand.gameObject.AddComponent<AudioSource>();
                                source.pitch = UnityEngine.Random.Range(0.87f, 1.1f);
                                source.PlayOneShot(sound, 1f);
                            }
                            catch (Exception ex)
                            {
                                UnityEngine.Object.Destroy(ljoint);
                                LoggerInstance.Warning(ex.Data);
                            }
                            LoggerInstance.Msg(ljoint == null);
                            LoggerInstance.Msg(rrend == null);
                            LoggerInstance.Msg(ltarget.ToString());
                        }
                    }
                    else if (!lgrappled)
                    {
                        lrend.positionCount = 0;
                        UnityEngine.Object.Destroy(ljoint);

                    }
                }
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            UnityEngine.Object.Destroy(ljoint);
            UnityEngine.Object.Destroy(rjoint);
            UnityEngine.Object.Destroy(rrend);
            UnityEngine.Object.Destroy(lrend);
            rtarget = Vector3.zero;
            ltarget = Vector3.zero;
            rgrappled = false;
            lgrappled = false;
        }
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            UnityEngine.Object.Destroy(ljoint);
            UnityEngine.Object.Destroy(rjoint);
            UnityEngine.Object.Destroy(rrend);
            UnityEngine.Object.Destroy(lrend);
            rtarget = Vector3.zero;
            ltarget = Vector3.zero;
            rgrappled = false;
            lgrappled = false;
        }
        public void SetSpringDrive(ConfigurableJoint joint, float spring, float damper, float max)
        {
            JointDrive jointDrive = new JointDrive
            {
                positionSpring = spring,
                positionDamper = damper,
                maximumForce = max
            };
            joint.xDrive = jointDrive;  
            joint.yDrive = jointDrive;
            joint.zDrive = jointDrive;
        }
        public void SetSpringLimit(ConfigurableJoint joint, float spring, float damper)
        {
            SoftJointLimitSpring linearLimitSpring = joint.linearLimitSpring;
            linearLimitSpring.spring = spring;
            linearLimitSpring.damper = damper;
            joint.linearLimitSpring = linearLimitSpring;
        }
        public void SetLinearLimit(ConfigurableJoint joint, float limit, float bounce)
        {
            SoftJointLimit linearLimit = joint.linearLimit;
            linearLimit.limit = limit;
            linearLimit.bounciness = bounce;
            joint.linearLimit = linearLimit;
            if (limit < _LowestLimit)
            {
                _LowestLimit = limit;
            }
        }
        public void SetLinearMotion(ConfigurableJoint joint, ConfigurableJointMotion motion)
        {
            joint.xMotion = motion;
            joint.yMotion = motion;
            joint.zMotion = motion;
        }
        public void SetWorldAnchor(ConfigurableJoint joint, Vector3 anchor)
        {
            joint.anchor = joint.transform.InverseTransformPoint(anchor);
        }
        public void SetWorldConnected(ConfigurableJoint joint, Vector3 anchor)
        {
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = (joint.connectedBody ? joint.connectedBody.transform.InverseTransformPoint(anchor) : anchor);
        }
    }
}
