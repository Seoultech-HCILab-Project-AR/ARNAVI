using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;

    public GameObject iceFirstGameObject;
    public Transform iceSecondTransform;
    public ParticleSystem[] iceSecondGameObject;
    public float multi = 1f;
    private bool _isIceParticleMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isIceParticleMoving)
        {
            iceFirstGameObject.transform.position += transform.forward * multi * Time.deltaTime;
        }
    }

    public void FireBreath()
    {
        fireParticleSystem.Play();
    }

    public void IceBeam()
    {
        _isIceParticleMoving = true;
        iceFirstGameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Ice());
    }

    IEnumerator Ice()
    {
        yield return new WaitForSeconds(2f);
        _isIceParticleMoving = false;
        iceSecondTransform.localPosition = iceFirstGameObject.transform.localPosition;
        iceFirstGameObject.SetActive(false);
        foreach (var particle in iceSecondGameObject)
        {
            particle.Play();
        }
    }
}
