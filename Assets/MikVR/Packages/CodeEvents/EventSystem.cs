using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEvents
{

    public class AbstractEventSystem {}

    internal static class EventSystemController
    {
        internal static List<EventSystem> VarOut_EventSystems { get; private set; } = new List<EventSystem>();

        internal static void AddEventSystem(EventSystem eventSystem)
        {
            if (!VarOut_EventSystems.Contains(eventSystem))
            {
                VarOut_EventSystems.Add(eventSystem);
            }
        }
    }

    internal static class EventSystemController<T1>
    {
        internal static List<EventSystem<T1>> VarOut_EventSystemsT1 { get; private set; } = new List<EventSystem<T1>>();

        internal static void AddEventSystem(EventSystem<T1> eventSystem)
        {
            if (!VarOut_EventSystemsT1.Contains(eventSystem))
            {
                VarOut_EventSystemsT1.Add(eventSystem);
            }
        }
    }

    internal static class EventSystemController<T1, T2>
    {
        internal static List<EventSystem<T1, T2>> VarOut_EventSystemsT2 { get; private set; } = new List<EventSystem<T1, T2>>();

        internal static void AddEventSystem(EventSystem<T1, T2> eventSystem)
        {
            if (!VarOut_EventSystemsT2.Contains(eventSystem))
            {
                VarOut_EventSystemsT2.Add(eventSystem);
            }
        }
    }

    internal static class EventSystemController<T1, T2, T3>
    {
        internal static List<EventSystem<T1, T2, T3>> VarOut_EventSystemsT3 { get; private set; } = new List<EventSystem<T1, T2, T3>>();

        internal static void AddEventSystem(EventSystem<T1, T2, T3> eventSystem)
        {
            if (!VarOut_EventSystemsT3.Contains(eventSystem))
            {
                VarOut_EventSystemsT3.Add(eventSystem);
            }
        }
    }

    internal static class EventSystemController<T1, T2, T3, T4>
    {
        internal static List<EventSystem<T1, T2, T3, T4>> VarOut_EventSystemsT4 { get; private set; } = new List<EventSystem<T1, T2, T3, T4>>();

        internal static void AddEventSystem(EventSystem<T1, T2, T3, T4> eventSystem)
        {
            if (!VarOut_EventSystemsT4.Contains(eventSystem))
            {
                VarOut_EventSystemsT4.Add(eventSystem);
            }
        }
    }

    internal static class EventSystemController<T1, T2, T3, T4, T5>
    {
        internal static List<EventSystem<T1, T2, T3, T4, T5>> VarOut_EventSystemsT5 { get; private set; } = new List<EventSystem<T1, T2, T3, T4, T5>>();

        internal static void AddEventSystem(EventSystem<T1, T2, T3, T4, T5> eventSystem)
        {
            if (!VarOut_EventSystemsT5.Contains(eventSystem))
            {
                VarOut_EventSystemsT5.Add(eventSystem);
            }
        }
    }

    public class EventSystem : AbstractEventSystem
    {
        public EventSystem()
        {
            EventSystemController.AddEventSystem(this);
        }

        private List<Action> actions = new List<Action>();
        public void AddListener(Action action)
        {
            actions.Add(action);
        }

        // TODO: test if that works on scene change too!
        public void AddListenerSingle(Action action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke();
            }
        }

        public void InvokeSafe()
        {
            foreach (Action a in actions.ToArray())
            {
                a.Invoke();
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }

        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class EventSystem<T1> : AbstractEventSystem
    {
        private List<Action<T1>> actions = new List<Action<T1>>();

        public EventSystem()
        {
            EventSystemController<T1>.AddEventSystem(this);
        }

        public void AddListener(Action<T1> action)
        {
            actions.Add(action);
        }

        public void AddListenerSingle(Action<T1> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1> action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0);
            }
        }
        public void InvokeSafe(T1 param0)
        {
            foreach (Action<T1> a in actions.ToArray())
            {
                a.Invoke(param0);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class EventSystem<T1, T2> : AbstractEventSystem
    {
        private List<Action<T1, T2>> actions = new List<Action<T1, T2>>();

        public EventSystem()
        {
            EventSystemController<T1, T2>.AddEventSystem(this);
        }

        public void AddListener(Action<T1, T2> action)
        {
            actions.Add(action);
        }

        public void AddListenerSingle(Action<T1, T2> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2)
        {
            foreach (Action<T1, T2> a in actions.ToArray())
            {
                a.Invoke(param1, param2);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class EventSystem<T1, T2, T3> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3>> actions = new List<Action<T1, T2, T3>>();

        public EventSystem()
        {
            EventSystemController<T1, T2, T3>.AddEventSystem(this);
        }

        public void AddListener(Action<T1, T2, T3> action)
        {
            actions.Add(action);
        }

        public void AddListenerSingle(Action<T1, T2, T3> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3> action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3)
        {
            foreach (Action<T1, T2, T3> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class EventSystem<T1, T2, T3, T4> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3, T4>> actions = new List<Action<T1, T2, T3, T4>>();

        public EventSystem()
        {
            EventSystemController<T1, T2, T3, T4>.AddEventSystem(this);
        }

        public void AddListener(Action<T1, T2, T3, T4> action)
        {
            actions.Add(action);
        }

        public void AddListenerSingle(Action<T1, T2, T3, T4> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3, T4> action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2, T4 param3)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2, param3);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            foreach (Action<T1, T2, T3, T4> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3, param4);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class EventSystem<T1, T2, T3, T4, T5> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3, T4, T5>> actions = new List<Action<T1, T2, T3, T4, T5>>();

        public EventSystem()
        {
            EventSystemController<T1, T2, T3, T4, T5>.AddEventSystem(this);
        }

        public void AddListener(Action<T1, T2, T3, T4, T5> action)
        {
            actions.Add(action);
        }

        public void AddListenerSingle(Action<T1, T2, T3, T4, T5> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3, T4, T5> action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2, T4 param3, T5 param4)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2, param3, param4);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            foreach (Action<T1, T2, T3, T4, T5> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3, param4, param5);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}