using Assets.Sourses.Tag;
using Cinemachine;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CaslePainter), typeof(CheckCountPainted), typeof(P3dColor))]
[RequireComponent(typeof(Monetization))]
public class Finish : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _nextCamera;
    [SerializeField] private CameraTransiter _cameraTransiter;
    [SerializeField] private Transform _casle;
    [SerializeField] private FixedJoystick _jostick;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private FovCamera _fovCamera;

    private CaseMaterial _caseMaterial;
    private BossFighter _bossFighter;
    private Player _player;
    private TagNewStickmanPosition _newSticmanPosition;
    private CaslePainter _caslePainter;
    private CheckCountPainted _checkCountPainted;
    private P3dColor _p3DColor;
    private Menu _menu;
    private ChekPaint _chekPaint;
    private Monetization _monetization;
    private float _timeLife;
    private int _circle = 0;

    private void Start()
    {
        _chekPaint = FindObjectOfType<ChekPaint>();
        _caslePainter = GetComponent<CaslePainter>();
        _checkCountPainted = GetComponent<CheckCountPainted>();
        _p3DColor = GetComponent<P3dColor>();
        _menu = FindObjectOfType<Menu>();
        _monetization = GetComponent<Monetization>();
        _newSticmanPosition = FindObjectOfType<TagNewStickmanPosition>();
        if (PlayerPrefs.HasKey("Circle"))
            _circle = PlayerPrefs.GetInt("Circle");
    }

    private void OnEnable()
    {
        _bossFighter = FindObjectOfType<BossFighter>();
        _bossFighter.FightEnd += PaintStarter;
        Player.MaterialChanged += ChangeMarerial;
    }
    private void OnDisable()
    {
        _bossFighter.FightEnd -= PaintStarter;
        _checkCountPainted.FullPaited -= Enable;
        Player.MaterialChanged -= ChangeMarerial;
    }

    private void ChangeMarerial(CaseMaterial value)
    {
        _caseMaterial = value;
    }

    private void Update()
    {
        _timeLife += Time.deltaTime;
    }

    private void Enable()
    {
        _caslePainter.MoveBrushToStartPosition(0.5f);
        _caslePainter.enabled = false;
        _checkCountPainted.enabled = false;
        _p3DColor.enabled = false;
        FindObjectOfType<LearnPaintOpener>().ClosePanelAll();
        _cameraTransiter.Transit(3);
        if (_chekPaint.SideCount - 1 == _chekPaint.CurrentPaintSide)
            FindObjectOfType<LinesAnimator>().DisableLine();
        if (PlayerPrefs.HasKey("Circle"))
            _circle = PlayerPrefs.GetInt("Circle");

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            _circle++;
            PlayerPrefs.SetInt("Circle", _circle);
        }

        SceneLoader.Instance.SaveLevel(SceneManager.GetActiveScene().buildIndex + 1);
        SceneLoader.Instance.SaveColor((CasleSide)_chekPaint.CurrentPaintSide, _caseMaterial.Color);
        StartCoroutine(AwitAndOpen(_winPanel, 4));
        StartCoroutine(_monetization.ShowInterstitialAfterWhile(4.5f));
    }

    private IEnumerator AwitAndOpen(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        _menu.OpenPanel(_winPanel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _cameraTransiter.CurrentCamera.Follow = null;
            _cameraTransiter.Transit(_nextCamera);
            _player = player;
            _fovCamera.OnFinishEnter();
        }
    }

    private void PaintStarter(List<Friend> friends)
    {
        _player.Move(_newSticmanPosition.transform.position);

        for (int i = 0; i < friends.Count; i++)
        {
            friends[i].NavMeshAgent.speed = 6;
            StartCoroutine(FriendPursue(friends[i]));
        }

        _caslePainter.enabled = true;
        _checkCountPainted.enabled = true;
        _p3DColor.enabled = true;
        _checkCountPainted.FullPaited += Enable;
    }

    private IEnumerator FriendPursue(Stickman friends)
    {
        yield return new WaitForEndOfFrame();
        if (friends != null)
        {
            friends.Init(_newSticmanPosition.transform);
            friends.LookAt();
            friends.StopGoToAttack();
            var newPosition = _newSticmanPosition.GetPosition();
            friends.Move(newPosition);
            yield return new WaitWhile(() => friends.Engen.GetDistant(newPosition) >= 0.7f);
            friends.Init(_casle);
            friends.Stop();
            friends.Joi();
            friends.LookAt();
            friends.NavMeshAgent.enabled = false;
        }
    }
}
