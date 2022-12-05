using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        const string CUE_SHEET_NAME = "MaterialFolder";

        // サウンド１つのデータ
        [System.Serializable]
        public class SoundDic
        {
            public string key = "";
            public string name = "";
            [Range(0.0f, 1.0f)] public float volume = 1;
        }

        [SerializeField] List<SoundDic> _BGMList;
        [SerializeField] List<SoundDic> _SEList;
        [SerializeField] List<SoundDic> _3DSEList;

        Dictionary<string, CriAtomSource> _poolBGM = new Dictionary<string, CriAtomSource>();
        Dictionary<string, CriAtomSource> _poolSE = new Dictionary<string, CriAtomSource>();

        string _playingBgmKey = "";

        Dictionary<string, bool> _poolIsSEPause = new Dictionary<string, bool>();

        GameObject _bgmParent;
        GameObject _seParent;
        GameObject _3dSeParent;

        // シングルトン
        static SoundManager _instanse = null;
        public static SoundManager Instanse { get => _instanse; private set { Instanse = _instanse; } }

        private void Awake()
        {
            if (_instanse != null)
            {
                Debug.Log("こんにちは");
                Destroy(gameObject);
                return;
            }
            _instanse = this;

            _bgmParent = new GameObject("BGM");
            _bgmParent.transform.parent = transform;
            foreach (var bgm in _BGMList) LoadBGM(bgm);

            _seParent = new GameObject("SE");
            _seParent.transform.parent = transform;
            foreach (var se in _SEList) LoadSE(se);

            _3dSeParent = new GameObject("3DSE");
            _3dSeParent.transform.parent = transform;
            foreach (var se in _3DSEList) Load3DSE(se);
        }

        /// <summary>
        /// BGMの読み込み
        /// </summary>
        /// <param name="dic">サウンドデータ</param>
        /// <returns>keyが同じで上書きしたか</returns>
        public bool LoadBGM(SoundDic dic)
        {
            var soundObj = new GameObject(dic.key);
            soundObj.transform.parent = _bgmParent.transform;
            soundObj.tag = "Sound";

            var source = soundObj.AddComponent<CriAtomSource>();
            source.cueSheet = CUE_SHEET_NAME;
            source.cueName = dic.name;
            source.volume = dic.volume;
            source.loop = true;
            source.use3dPositioning = false;

            Instanse._poolBGM.Add(dic.key, source);

            return true;
        }

        /// <summary>
        /// SEの読み込み
        /// </summary>
        /// <param name="dic">サウンドデータ</param>
        /// <returns>keyが同じで上書きしたか</returns>
        public bool LoadSE(SoundDic dic)
        {
            var soundObj = new GameObject(dic.key);
            soundObj.transform.parent = _seParent.transform;

            var source = soundObj.AddComponent<CriAtomSource>();
            source.cueSheet = CUE_SHEET_NAME;
            source.cueName = dic.name;
            source.volume = dic.volume;
            source.use3dPositioning = false;

            Instanse._poolSE.Add(dic.key, source);
            Instanse._poolIsSEPause.Add(dic.key, false);

            return true;
        }

        /// <summary>
        /// 3DSEの読み込み
        /// </summary>
        /// <param name="dic">サウンドデータ</param>
        /// <returns>keyが同じで上書きしたか</returns>
        public bool Load3DSE(SoundDic dic)
        {
            var soundObj = new GameObject(dic.key);
            soundObj.transform.parent = _3dSeParent.transform;

            var source = soundObj.AddComponent<CriAtomSource>();
            source.cueSheet = CUE_SHEET_NAME;
            source.cueName = dic.name;
            source.volume = dic.volume;

            Instanse._poolSE.Add(dic.key, source);
            Instanse._poolIsSEPause.Add(dic.key, false);

            return true;
        }

        /// <summary>
        /// BGMの再生
        /// </summary>
        /// <param name="key">BGMに付けた別名</param>
        /// <returns>再生できたか</returns>
        public static bool PlayBGM(string key)
        {
            if (!Instanse._poolBGM.ContainsKey(key)) return false;

            StopBGM();

            Instanse._playingBgmKey = key;

            UnPauseBGM();

            Instanse._poolBGM[key].Play();

            return true;
        }

        /// <summary>
        /// SEの再生
        /// </summary>
        /// <param name="key">SEの名前(別名)</param>
        /// <param name="callObj">関数を呼び出したGameObject</param>
        /// <returns>再生できたか</returns>
        public static bool PlaySE(string key, GameObject callObj)
        {
            if (!Instanse._poolSE.ContainsKey(key)) return false;

            // 一時停止中はSEを再生しない
            if (Instanse._poolIsSEPause[key]) return false;

            // callObjの子に作る
            var obj = new GameObject(key);
            obj.tag = "Sound";
            obj.transform.parent = callObj.transform;
            var source = obj.AddComponent<CriAtomSource>();
            source.cueSheet = Instanse._poolSE[key].cueSheet;
            source.cueName = Instanse._poolSE[key].cueName;
            source.volume = Instanse._poolSE[key].volume;
            source.Play();
            obj.AddComponent<CriDestroy>();

            //Instanse._poolSE[key].Play();

            return true;
        }

        /// <summary>
        /// BGMの一時停止
        /// </summary>
        public static void PauseBGM()
        {
            if (Instanse._playingBgmKey == "") return;

            Instanse._poolBGM[Instanse._playingBgmKey].Pause(true);
        }

        /// <summary>
        /// SEの一時停止
        /// </summary>
        /// <param name="key">SEの名前(別名)</param>
        public static void PauseSE(string key)
        {
            Instanse._poolSE[key].Pause(true);

            Instanse._poolIsSEPause[key] = true;
        }

        /// <summary>
        /// BGMの再開
        /// </summary>
        public static void UnPauseBGM()
        {
            if (Instanse._playingBgmKey == "") return;

            Instanse._poolBGM[Instanse._playingBgmKey].Pause(false);
        }

        /// <summary>
        /// SEの再開
        /// </summary>
        /// <param name="key">SEの名前(別名)</param>
        public static void UnPauseSE(string key)
        {
            Instanse._poolSE[key].Pause(false);

            Instanse._poolIsSEPause[key] = false;
        }

        /// <summary>
        /// BGMの停止
        /// </summary>
        public static void StopBGM()
        {
            if (Instanse._playingBgmKey == "") return;

            Instanse._poolBGM[Instanse._playingBgmKey].Stop();

            Instanse._playingBgmKey = "";
        }

        /// <summary>
        /// SEの停止
        /// </summary>
        /// <param name="key">SEの名前(別名)</param>
        public static void StopSE(string key)
        {
            Instanse._poolSE[key].Stop();
        }

        /// <summary>
        /// 全てのSEの停止
        /// </summary>
        public static void StopAllSE()
        {
            foreach (var se in Instanse._poolSE)
            {
                se.Value.Stop();
            }
        }

        /// <summary>
        /// BGMとSE一括停止
        /// </summary>
        public static void StopBGMandAllSE()
        {
            StopBGM();
            StopAllSE();
        }

        /// <summary>
        /// BGMの音量調節
        /// </summary>
        /// <param name="vol">音量(0～1)</param>
        public static void SetBGMVolume(float vol)
        {
            if (Instanse._playingBgmKey == "") return;

            Instanse._poolBGM[Instanse._playingBgmKey].volume = Mathf.Clamp01(vol);
        }

        /// <summary>
        /// SEの音量調節
        /// </summary>
        /// <param name="key">SEの名前(別名)</param>
        /// <param name="vol">音量(0～1)</param>
        public static void SetSEVolume(string key, float vol)
        {
            Instanse._poolSE[key].volume = Mathf.Clamp01(vol);
        }

        /// <summary>
        /// 全てのSEの音量調節
        /// </summary>
        /// <param name="vol">音量(0～1)</param>
        public static void SetAllSEVolume(float vol)
        {
            foreach (var se in Instanse._poolSE)
            {
                se.Value.volume = Mathf.Clamp01(vol);
            }
        }

        /// <summary>
        /// BGMとSE一括音量調節
        /// </summary>
        /// <param name="vol">音量(0～1)</param>
        public static void SetBGMandAllSEVolume(float vol)
        {
            SetBGMVolume(vol);
            SetAllSEVolume(vol);
        }
    }
}