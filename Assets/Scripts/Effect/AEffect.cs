using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEffect : MonoBehaviour
{
    float duration;
    float name;
    //Animation animation;
    public float currentDuration;
    GameObject particle;

    public float Duration { get => duration; set => duration = value; }
    public float Name { get => name; set => name = value; }
    //public Animation Animation { get => animation; set => animation = value; }
    public float CurrentDuration { get => currentDuration; set => currentDuration = value; }
    public GameObject Particle { get => particle; set => particle = value; }

    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();
    public abstract void OnDisable();
    public abstract void OnEnable();
    public static void Active(GameObject target, List<Type> ls, float? duration = null)
    {
        var methodInfo = typeof(AEffect).GetMethod("ActiveE");
        foreach (Type effect in ls)
        {
            var genericMethod = methodInfo.MakeGenericMethod(effect);
            genericMethod.Invoke(null, new object[] { target, duration });
        }
        //var component = target.GetComponent<typeof(t)>();
    }
    public static void ActiveE<T>(GameObject target, float? duration = null) where T : AEffect
    {
        if (!target.TryGetComponent<T>(out T st))
        {
            T go = target.AddComponent<T>();//add effect duration
            if (duration.HasValue)
                go.duration = duration ?? 0;
        }
        else
        {
            if (duration == null)
            {
                st.enabled = false;
                st.enabled = true;
                return;
            }
            else if (st.currentDuration > duration)
            {
                return;
            }
            else
            {
                //st.enabled = false;
                st.duration = duration ?? 0;
                st.ResetDuration();
                st.enabled = true;//reset effect duration
            }
        }
        return;
    }
    public void ActiveAnimation(string type)
    {
        Transform location = gameObject.transform;
        Transform ts = location.Find(type + "(Clone)");
        if (ts != null)
        {
            ts.gameObject.SetActive(true);
        }
        else
        {
            Instantiate(Particle, location);
            Destroy(gameObject.GetComponent<ParticleSystem>());
        }
    }

    public void DeactiveAnimation(string type)
    {
        Transform location = gameObject.transform.Find(type + "(Clone)");
        if (location != null)
        {
            location.gameObject.SetActive(false);
        }
    }
    public void ResetDuration()
    {
        CurrentDuration = Duration;
    }
}
