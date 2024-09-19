using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}