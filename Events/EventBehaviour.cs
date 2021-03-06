namespace HouraiTeahouse {

    /// <summary> An abstract class for MonoBehaviours that handle events published by Mediators. </summary>
    /// <typeparam name="T"> the event type to subscribe to </typeparam>
    public abstract class EventBehaviour<T> : BaseBehaviour {

        Mediator _eventManager;

        /// <summary> Gets or sets the event manager the event handler is subscribed to. </summary>
        public Mediator EventManager {
            get { return _eventManager; }
            set {
                if (_eventManager != null)
                    _eventManager.Unsubscribe<T>(OnEvent);
                _eventManager = value;
                if (_eventManager != null)
                    _eventManager.Subscribe<T>(OnEvent);
            }
        }

        /// <summary> Unity callback. Called on object instantiation. </summary>
        protected override void Awake() {
            base.Awake();
            _eventManager = Mediator.Global;
            _eventManager.Subscribe<T>(OnEvent);
        }

        /// <summary> Unity callback. Called on object destruction. </summary>
        /// >
        protected virtual void OnDestroy() {
            if (_eventManager != null)
                _eventManager.Unsubscribe<T>(OnEvent);
        }

        /// <summary> Events callback. </summary>
        /// <param name="eventArgs"> event arguments </param>
        protected abstract void OnEvent(T eventArgs);

    }

}
