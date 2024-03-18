using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Managers;
using Model;
using Net;
using Request;
using UnityEngine;


public class GameFacade : MonoBehaviour
{
    private static GameFacade _instance;
    public static GameFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        }
    }
    private UIManager _uiManager;
    private AudioManager _audioManager;
    private PlayerManager _playerManager;
    private CameraManager _cameraManager;
    private RequestManager _requestManager;
    private Client _client;
    private bool isEnterPlaying = false;
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        OnUpdate();
        if (isEnterPlaying)
        {
            EnterPlaying();
            isEnterPlaying = false;
        }
    }
    private void Init()
    {
        _uiManager = new UIManager(this);
        _audioManager = new AudioManager(this);
        _playerManager = new PlayerManager(this);
        _cameraManager = new CameraManager(this);
        _requestManager = new RequestManager(this);
        _client = new Client(this);
        _uiManager.Init();
        _audioManager.Init();
        _playerManager.Init();
        _cameraManager.Init();
        _requestManager.Init();
        _client.Init();
    }

    private void Destroy()
    {

        OnDestroy();
    }

    private void OnDestroy()
    {
        _uiManager.Destroy();
        _audioManager.Destroy();
        _playerManager.Destroy();
        _cameraManager.Destroy();
        _requestManager.Destroy();
        _client.Destroy();
    }

    private void OnUpdate()
    {
        _uiManager.Update();
        _audioManager.Update();
        _playerManager.Update();
        _cameraManager.Update();
        _requestManager.Update();
        _client.Update();
    }
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        _requestManager.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        _requestManager.RemoveRequest(actionCode);
    }
    public void HandleReponse(ActionCode actionCode, string data)
    {
        _requestManager.HandleReponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        _client.SendRequest(requestCode, actionCode, data);
    }

    public void ShowMessage(string msg)
    {
        _uiManager.ShowMessage(msg);
    }
    public void PlayBgAudio(string audioName)
    {
        _audioManager.PlayBgAudio(audioName);
    }
    public void PlayNormalAudio(string audioName)
    {
        _audioManager.PlayNormalAudio(audioName);
    }

    public void SetUserData(UserData user)
    {
        _playerManager.UserData = user;
    }
    public UserData GetUserData() => _playerManager.UserData;

    public void SetCurrentRoleType(RoleType rt)
    {
        _playerManager.SetCurrentRoleType(rt);
    }

    public GameObject GetCurrentGameObject()
    {
        return _playerManager.CurrentRoleGO;
    }

    public void EnterPlayingSync()
    {
        isEnterPlaying = true;
    }
    private void EnterPlaying()
    {
        _playerManager.SpawnRoles();
    }
    public void StartPlaying()
    {
        _playerManager.AddControlScript();
        _playerManager.CreateSyncRequest();
        _cameraManager.FollowTarget();
    }
    public void SendAttack(int damage)
    {
        _playerManager.SendAttack(damage);
    }
}


