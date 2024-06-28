using System.Collections;
using UnityEngine;

namespace GameInfrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}