using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonGettingStabbed : MonoBehaviour
{
    public GameObjectVariable HolePrefab;
    public IntVariable MaxHoles;
    public FloatReference Heat;
    public FloatReference HoleHeatLoss;
    public FloatReference TimeForHoleFix;

    public GameEventBalloonInfo TauntEvent;

    protected List<GameObject> stabs;
    protected List<GameObject> fixingStabs;
    protected float stabbedCooldown = 0f;

    private AudioSource deflateSoundSource;

    protected void Awake()
    {
        stabs = new List<GameObject>();
        fixingStabs = new List<GameObject>();
    }

    private void Start() {
        AudioMixer mixer = Resources.Load("Main") as AudioMixer;
        AudioMixerGroup amx = mixer.FindMatchingGroups("Balloon")[0];

        deflateSoundSource = gameObject.AddComponent<AudioSource>();
        deflateSoundSource.clip = AudioManager.GetSound(Sound.Deflate);
        deflateSoundSource.loop = true;
        deflateSoundSource.outputAudioMixerGroup = amx;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Heat.Value > 0f) {
            foreach (GameObject go in stabs) {
                Heat.Value -= HoleHeatLoss.Value * Time.deltaTime;
            }
            foreach (GameObject go in fixingStabs) { 
                Heat.Value -= HoleHeatLoss.Value * Time.deltaTime;
            }
        }

        if (stabbedCooldown > 0f)
            stabbedCooldown -= Time.deltaTime;
    }

    public void Stabbed(Vector3 pos, Vector3 normal) {
        if (stabs.Count >= MaxHoles.Value)
            return;

        stabbedCooldown = 0.5f;

        GameObject go = Instantiate(HolePrefab.Value);
        go.transform.position = new Vector3(pos.x, pos.y, transform.position.z - 0.1f);
        go.transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
        go.transform.parent = transform;
        stabs.Add(go);

        Invoke("CallTaunt", 1f);

        deflateSoundSource.Play();
    }

    protected IEnumerator FixHole(GameObject hole) {
        AudioManager.PlaySound(Sound.Repair);

        stabs.Remove(hole);
        fixingStabs.Add(hole);
        hole.transform.DOScale(0f, TimeForHoleFix.Value);

        yield return new WaitForSeconds(TimeForHoleFix.Value);

        fixingStabs.Remove(hole);
        Destroy(hole);

        if(fixingStabs.Count == 0 && stabs.Count == 0) {
            deflateSoundSource.Stop();
        }
    }

    private void CallTaunt() {
        TauntEvent.Raise(GetComponent<BalloonInfo>());
    }
}
