using System.Collections;
using UnityEngine;

namespace GameInfasrtucture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}