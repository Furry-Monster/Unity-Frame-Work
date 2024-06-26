using System.Collections.Generic;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要用于设定游戏内的定时器，
//          定时模块我采用的是Update函数，每帧检查是否有定时器需要执行，
//          没有采用携程，主要是因为:
//          Unity的携程仍然是单线程的，如果有大量的定时器需要执行，就会导致游戏的卡顿
//==========================
public class TimerSystem : Singleton<TimerSystem>,ISystem
{
    private class TimerNode
    {
        public TimerHandler callback;
        public float interval;
        public float passedTime;
        public int repeat;
        public bool isRemoved;
        public object param;

        public int ID;
    }

    public delegate void TimerHandler(object param);

    private Dictionary<int, TimerNode> timerDict;

    private Queue<TimerNode> timerToAddQueue;
    private Queue<TimerNode> timerToRemoveQueue;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Update()
    {
        IterateTimerToAddQueue();

        foreach (var node in timerDict.Values)
        {
            UpdateTimer(node);
        }

        IterateTimerToRemoveQueue();
    }

    #region internal
    public void Init()
    {
        timerDict = new Dictionary<int, TimerNode>();
        timerToAddQueue = new Queue<TimerNode>();
        timerToRemoveQueue = new Queue<TimerNode>();

        Debug.Log("Timer System initialized successfully");
    }


    #endregion

    #region reusable

    private int GetNextID()
    {
        int id = 0;
        while (timerDict.ContainsKey(id))
        {
            id++;
        }
        return id;
    }

    private void IterateTimerToAddQueue()
    {
        while (timerToAddQueue.Count > 0)
        {
            if (!timerDict.ContainsKey(timerToAddQueue.Peek().ID))
            {
                TimerNode node = timerToAddQueue.Dequeue();
                timerDict.Add(node.ID, node);
            }
        }
    }

    private void IterateTimerToRemoveQueue()
    {
        while (timerToRemoveQueue.Count > 0)
        {
            if (timerDict.ContainsKey(timerToRemoveQueue.Peek().ID))
            {
                TimerNode node = timerToRemoveQueue.Dequeue();
                timerDict.Remove(node.ID);
            }
        }
    }

    private void UpdateTimer(TimerNode node)
    {
        if (node.isRemoved)
        {
            return;
        }

        node.passedTime += Time.deltaTime;

        if (node.passedTime >= node.interval)
        {
            if (node.repeat > 0)
            {
                node.repeat--;
                node.passedTime = 0;
                node.callback(node.param);
            }
            else //if (node.repeat == 0)
            {
                node.callback(node.param);
                node.isRemoved = true;
                timerToRemoveQueue.Enqueue(node);
            }
        }
    }


    #endregion


    #region Schedule

    public void ScheduleWithSenderRepeatly(TimerHandler callback, float interval, int repeat, object param)
    {
        TimerNode node = new TimerNode();

        node.callback = callback;
        node.interval = interval;
        node.repeat = repeat;
        node.param = param;

        //default settings
        node.isRemoved = false;
        node.passedTime = 0;
        node.ID = GetNextID();

        timerToAddQueue.Enqueue(node);
    }

    public void ScheduleWithSender(TimerHandler callback, float interval, object param)
    {
        ScheduleWithSenderRepeatly(callback, interval, 0, param);
    }

    public void Schedule(TimerHandler callback, float interval)
    {
        ScheduleWithSenderRepeatly(callback, interval, 0, null);
    }

    public void ScheduleRepeatly(TimerHandler callback, float interval, int repeat)
    {
        ScheduleWithSenderRepeatly(callback, interval, repeat, null);
    }
    #endregion


    #region UnSchedule
    public void Unschedule(int id)
    {
        if (timerDict.ContainsKey(id))
        {
            timerDict[id].isRemoved = true;
            timerToRemoveQueue.Enqueue(timerDict[id]);
        }
    }

    public void UnscheduleAll()
    {
        foreach (var node in timerDict.Values)
        {
            node.isRemoved = true;
            timerToRemoveQueue.Enqueue(node);
        }
    }

    public void UnscheduleAll(TimerHandler callback)
    {
        foreach (var node in timerDict.Values)
        {
            if (node.callback == callback)
            {
                node.isRemoved = true;
                timerToRemoveQueue.Enqueue(node);
            }
        }
    }

    #endregion
}
