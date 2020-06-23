using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demo : MonoBehaviour
{
    public static Demo instance;

    public Transform playerTr;
    public Rigidbody2D playerRb;
    public Player player;
    public List<ActiveElement> trackedElements = new List<ActiveElement>();

    private List<Player.Shape> shapes = new List<Player.Shape>();
    private List<Vector2> playerPos = new List<Vector2>();
    private Dictionary<ActiveElement, List<bool>> elementsStatus = new Dictionary<ActiveElement, List<bool>>();

    private int currentFrame = 0;

    private bool recording = false;
    private bool playMode = false;


    public UnityEvent OnEnd;

    void Awake()
    {
        Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        foreach(ActiveElement e in trackedElements)
        {
            elementsStatus.Add(e, new List<bool>());
        }
        StartRecording();
    }

    public void StartRecording()
    {
        recording = true;
    }

    public void StopRecording()
    {
        recording = false;
    }

    public void Play()
    {
        StopRecording();
        playerRb.simulated = false;
        playMode = true;
    }

    public void Pause()
    {
        playMode = !playMode;
    }

    public void Stop()
    {
        playerRb.simulated = true;
        playMode = false;
    }

    void FixedUpdate()
    {
        if (playMode)
        {
            playerTr.position = playerPos[currentFrame];
            player.ChangeState(shapes[currentFrame]);

            foreach(ActiveElement e in trackedElements)
            {
                if(elementsStatus[e][currentFrame])
                {
                    e.Activate();
                } else
                {
                    e.Deactivate();
                }
            }
            currentFrame++;
            if (currentFrame >= playerPos.Count)
            {
                currentFrame = 0;
                Stop();
                OnEnd.Invoke();
            }
        }
        else
        {
            if (recording)
            {
                shapes.Add(player.state);
                playerPos.Add(playerTr.position);
                foreach (ActiveElement e in trackedElements)
                {
                    elementsStatus[e].Add(e.active);
                }
            }
        }
    }
}
