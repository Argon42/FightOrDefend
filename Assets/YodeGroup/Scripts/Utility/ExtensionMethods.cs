using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumer, Action<T> action)
    {
        foreach (T elem in enumer)
        {
            action(elem);
        }
        return enumer;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumer, Action<T, int> action)
    {
        int index = 0;
        foreach (T elem in enumer)
        {
            action(elem, index);
            index++;
        }
        return enumer;
    }

    public static Coroutine ExecuteCicle(this MonoBehaviour behaviour, float time, Action action)
    {
        return behaviour.StartCoroutine(ExecuteCicle(time, action));
    }

    private static IEnumerator ExecuteCicle(float time, Action action)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }

    public static Coroutine WaitAndExecute(this MonoBehaviour behaviour, float time, Action action)
    {
        return behaviour.StartCoroutine(WaitAndExecute(time, action));
    }

    private static IEnumerator WaitAndExecute(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public static void WaitEndOfFrameAndExecute(this MonoBehaviour behaviour, Action action)
    {
        behaviour.StartCoroutine(WaitEndOfFrameAndExecute(action));
    }

    private static IEnumerator WaitEndOfFrameAndExecute(Action action)
    {
        yield return new WaitForEndOfFrame();
        action();
    }

    public static void WaitWhileAndExecute(this MonoBehaviour behaviour,
        Func<bool> condition, float timeWait, Action action)
    {
        behaviour.StartCoroutine(WaitWhileAndExecute(condition, timeWait, action));

    }

    public static void WaitWhileAndExecute(this MonoBehaviour behaviour,
        Func<bool> condition, Action action)
    {
        const float timeWait = 0.01f;
        behaviour.StartCoroutine(WaitWhileAndExecute(condition, timeWait, action));

    }

    private static IEnumerator WaitWhileAndExecute(Func<bool> condition, float timeWait, Action action)
    {
        while (condition())
        {
            yield return new WaitForSeconds(timeWait);
        }
        action();
    }

    public static Action PartialApply<T>(this Action<T> action, T t)
    {
        return () => action(t);
    }

    /// <summary>
    /// Преобразование текстуры в спрайт
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public static Sprite ToSprite(this Texture2D texture)
    {
        var rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, Vector2.one / 2);
    }

    /// <summary>
    /// Перемешивает коллекцию
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = new System.Random().Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    /// <summary>
    /// Возвращает случайное значение с границами вектора
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static float Random(this Vector2 vector)
    {
        return UnityEngine.Random.Range(vector.x, vector.y);
    }

    /// <summary>
    /// Возвращает случайный элемент коллекции
    /// </summary>
    /// <param name="enumer"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T RandomElement<T>(this IEnumerable<T> enumer)
    {
        T randomElement = enumer.ElementAt(UnityEngine.Random.Range(0, enumer.Count()));
        return randomElement;
    }

    public static bool Exist(this object obj)
    {
        return obj != null;
    }

    static public T GetOrAddComponent<T>(this Component component)where T : Component
    {
        return component.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Является ли строка числом
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNumeric(this string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c) && c != '.')
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsNullOrEmpty(this string s) =>
        string.IsNullOrEmpty(s);

}