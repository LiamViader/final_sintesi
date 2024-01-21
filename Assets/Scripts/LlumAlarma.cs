using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlumAlarma : MonoBehaviour
{
    public float velocitatParpelleig = 1.0f;
    public float intensitatMinima = 0.0f;
    public float intensitatMaxima = 1.0f;

    private Light llum;

    // Start is called before the first frame update
    void Start()
    {
        llum = GetComponent<Light>();
        StartCoroutine(Parpellejar());
    }

    IEnumerator Parpellejar()
    {
        while (true)
        {
            float targetIntensity = (llum.intensity == intensitatMinima) ? intensitatMaxima : intensitatMinima;
            float tempsEspera = 1.0f / velocitatParpelleig;

            while (Mathf.Abs(llum.intensity - targetIntensity) > 0.01f)
            {
                llum.intensity = Mathf.Lerp(llum.intensity, targetIntensity, Time.deltaTime * velocitatParpelleig);
                yield return null;
            }

            llum.intensity = targetIntensity;

            yield return new WaitForSeconds(tempsEspera);
        }
    }
}
