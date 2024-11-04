using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowABeanBag : MonoBehaviour
{

    public Transform BeanBagSource;
    public Rigidbody[] LooseTooth = null;

    public float ExplosionForce = 10.0f;
    public float BeanBagVelocity = 2f;

    public GameObject BeanBagPrefab = null;

    public AudioSource impactAudio = null;

    private void ReleaseTeeth()
    {
        foreach (Rigidbody tooth in LooseTooth)
        {
            tooth.constraints = RigidbodyConstraints.None;
            tooth.AddExplosionForce(ExplosionForce, BeanBagSource.transform.position, 10.0f);
        }

    }
    public void ThrowBeanBag()
    {
        GameObject beanBag = GameObject.Instantiate(BeanBagPrefab, BeanBagSource.transform.position + BeanBagSource.transform.forward * 1.0f, BeanBagSource.transform.rotation);
        beanBag.GetComponent<Rigidbody>().AddForce(BeanBagSource.transform.forward * BeanBagVelocity, ForceMode.VelocityChange);

        GetComponent<Collider>().enabled = false;

        StartCoroutine(ThrowBeanBagCoroutine());

    }

    private IEnumerator ThrowBeanBagCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        impactAudio?.Play();

        ReleaseTeeth();
        gameObject.SetActive(false);

    }

}
