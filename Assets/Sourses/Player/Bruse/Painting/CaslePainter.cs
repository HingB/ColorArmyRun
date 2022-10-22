using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaslePainter : MonoBehaviour
{
    [SerializeField] private BrushCase _brushCase;
    [SerializeField] private List<Transform> _startPosition;
    [SerializeField] private List<CinemachineVirtualCamera> _camer;
    [SerializeField] private Brush _brushsPaint;
    [SerializeField] private FixedJoystick _jostick;
    [SerializeField] private float _speedBrush = 1;
    [SerializeField] private float _speedBrushPc = 7;
    [SerializeField] private float _sensitivity = 0.1f;
    private Transform _pointPlane;
    private List<PaintingSide> _paintingSides;
    private CameraTransiter _cameraTransiter;
    private float _timeToFlyUp = 1;
    private float _duration = 2;
    private Coroutine _paitn;
    private Sequence _sequence;
    private Camera _cameraMain;

    private void Start()
    {
        _paintingSides = new List<PaintingSide>();
        _cameraTransiter = FindObjectOfType<CameraTransiter>();
        _paitn = StartCoroutine(StartPaint());
        _cameraMain = Camera.main;
        _pointPlane = FindObjectOfType<ChekPaint>().transform;

        if (_paintingSides != null)
        {
            _paintingSides[0].Speed = GetPaintSpeed();
        }
    }

    private float GetPaintSpeed()
    {
#if YANDEX_GAMES && !UNITY_EDITOR
        switch (Agava.YandexGames.Device.Type)
        {
            case Agava.YandexGames.DeviceType.Desktop:
                return _speedBrushPc;
            default:
                return _speedBrush;
        }
#elif VK_GAMES && !UNITY_EDITOR
        if (Application.isMobilePlatform)
            return _speedBrush;
        else
            return _speedBrushPc;
#elif UNITY_EDITOR
        return _speedBrushPc;
#endif
    }

    private void OnDisable()
    {
        StopCoroutine(_paitn);
        _sequence.Kill();
    }

    public IEnumerator StartPaint()
    {
        EnableTrailRender(_brushsPaint);
        EnableTrailRender(_brushCase.Brushes);
        InitPaintinSides(_brushsPaint);
        BrushToSpace();
        _cameraTransiter.Transit(_camer[0]);
        _paintingSides[0].MoveToStartPosition(_duration);
        yield return new WaitForSeconds(_duration);
        FindObjectOfType<LearnPaintOpener>().OpenPanel();
        StartCoroutine(ClouseUntilClick());
        var wait = new WaitForFixedUpdate();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_cameraMain);
        var pointDownYPosition = _pointPlane.position.y;
        while (true)
        {
            ReadDirection(planes, pointDownYPosition);
            yield return wait;
        }
    }

    private IEnumerator ClouseUntilClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        FindObjectOfType<LearnPaintOpener>().ClosePanelText();
    }

    public void MoveBrushToStartPosition(float duration) => _paintingSides[0].MoveToStartPosition(duration);

    private void BrushToSpace()
    {
        _sequence = DOTween.Sequence();
        foreach (var brush in _brushCase.Brushes)
        {
            _sequence.Append(brush.transform.DOMoveY(20, _timeToFlyUp)
                .OnComplete(() => brush.gameObject.SetActive(false)))
                .SetRelative();
        }
    }

    private void ReadDirection(Plane[] planes, float downDirectionYPosition)
    {
        if (Input.GetMouseButton(0) == false)
            return;

        var inputX = Input.GetAxis("Mouse X");
        var inputY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(inputY) < _sensitivity)
            inputY = 0;
        if (Mathf.Abs(inputX) < _sensitivity)
            inputX = 0;
        Vector3 direction = SetLimitatMove(planes, downDirectionYPosition, inputX, inputY);
        _paintingSides[0].SetDirection(direction);
    }

    private Vector3 SetLimitatMove(Plane[] planes, float downDirectionYPosition, float inputX, float inputY)
    {
        // 0 - Left 1 - Right 2 - Down 3 - Up (Plane)
        var direction = new Vector3(inputX, inputY, 0);
        var directionVector = _brushsPaint.transform.position - _startPosition[0].transform.position;
        Ray ray = new Ray(_startPosition[0].transform.position, directionVector);
        var minDistant = Mathf.Infinity;
        var isIntersects = false;
        int planeIndex = int.MaxValue;
        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distant))
            {
                if (minDistant > distant)
                {
                    minDistant = distant;
                    isIntersects = true;
                    planeIndex = i;
                }
            }
        }

        if (isIntersects)
        {
            if (directionVector.magnitude > minDistant)
            {
                if (planeIndex == 3 && inputY > 0)
                    direction = new Vector3(direction.x, 0, 0);
                if (planeIndex == 1 && inputX > 0)
                    direction = new Vector3(0, direction.y, 0);
                if (planeIndex == 0 && inputX < 0)
                    direction = new Vector3(0, direction.y, 0);
            }
        }

        if (downDirectionYPosition > _brushsPaint.transform.position.y && inputY < 0)
            direction = new Vector3(direction.x, 0, 0);
        return direction;
    }

    private void InitPaintinSides(params Brush[] brushes)
    {
        _paintingSides.Add(new PaintingSide());
        _paintingSides[0].Init(brushes[0], _startPosition[0].transform);
    }

    private void EnableTrailRender(params Brush[] brushes) => EnableTrailRender((IEnumerable<Brush>)brushes);
    private void EnableTrailRender(IEnumerable<Brush> brushes)
    {
        foreach (Brush brush in brushes)
            brush.BrushColorChanger.DisconnectTrailRender();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_paintingSides != null)
        {
            _paintingSides[0].Speed = _speedBrush;
        }
    }
#endif
}