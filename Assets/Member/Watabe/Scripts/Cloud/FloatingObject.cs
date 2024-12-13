using UnityEngine;
using DG.Tweening;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject1;
    [SerializeField] private Transform targetObject2;
    [SerializeField] private float floatDistance = 1f;
    [SerializeField] private float floatDuration = 2f;
    [SerializeField] private Ease floatEase = Ease.InOutSine;

    private Tween floatingTween1;
    private Tween floatingTween2;

    private void Start()
    {
        StartFloating(targetObject1, ref floatingTween1);
        StartFloating(targetObject2, ref floatingTween2);
    }

    private void StartFloating(Transform target, ref Tween tween)
    {
        if (target == null) return;

        Vector3 startPosition = target.position;

        tween = target.DOMoveY(startPosition.y + floatDistance, floatDuration)
            .SetEase(floatEase)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false);
    }

    private void OnDestroy()
    {
        if (floatingTween1 != null && floatingTween1.IsActive())
        {
            floatingTween1.Kill();
        }

        if (floatingTween2 != null && floatingTween2.IsActive())
        {
            floatingTween2.Kill();
        }
    }
}
