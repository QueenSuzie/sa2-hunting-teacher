using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace sa2_hunting_teacher.DropdownControls
{
    /// <summary>
    /// Attaches to a System.Windows.Forms.Control and provides buffered 
    /// painting functionality.
    /// <para>
    /// Uses TState to represent the visual state of the control. Animations 
    /// are attached to transitions between states.
    /// </para>
    /// </summary>
    /// <typeparam name="TState">Any type representing the visual state of the control.</typeparam>
    public class BufferedPainter<TState>
    {

        bool _animationsNeedCleanup;
        bool _enabled;
        TState _currentState;
        TState _newState;
        TState _defaultState;
        Size _oldSize;

        /// <summary>
        /// Fired when the control must be painted in a particular state.
        /// </summary>
        public event EventHandler<BufferedPaintEventArgs<TState>> PaintVisualState;

        /// <summary>
        /// Raises the PaintVisualState event.
        /// </summary>
        /// <param name="e">BufferedPaintEventArgs instance.</param>
        protected virtual void OnPaintVisualState(BufferedPaintEventArgs<TState> e)
        {
            if (PaintVisualState != null) PaintVisualState(this, e);
        }

        /// <summary>
        /// Gets whether buffered painting is supported for the current OS/configuration.
        /// </summary>
        public bool BufferedPaintSupported { get; private set; }
        /// <summary>
        /// Gets the control this instance is attached to.
        /// </summary>
        public Control Control { get; private set; }
        /// <summary>
        /// Gets or sets the default animation duration (in milliseconds) for state transitions. The default is zero (not animated).
        /// </summary>
        public int DefaultDuration { get; set; }
        /// <summary>
        /// Gets or sets whether animation is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }
        /// <summary>
        /// Gets or sets the default visual state. The default value is 'default(TState)'.
        /// </summary>
        public TState DefaultState
        {
            get
            {
                return _defaultState;
            }
            set
            {
                bool usingOldDefault = Equals(_currentState, _defaultState);
                _defaultState = value;
                if (usingOldDefault) _currentState = _newState = _defaultState;
            }
        }
        /// <summary>
        /// Gets the collection of state transitions and their animation durations. 
        /// Only one item for each unique state transition is permitted.
        /// </summary>
        public ICollection<BufferedPaintTransition<TState>> Transitions { get; private set; }
        /// <summary>
        /// Gets the collection of state change triggers. 
        /// Only one item for each unique combination of type and visual state is permitted.
        /// </summary>
        public ICollection<VisualStateTrigger<TState>> Triggers { get; private set; }
        /// <summary>
        /// Gets or sets the current visual state.
        /// </summary>
        public TState State
        {
            get
            {
                return _currentState;
            }
            set
            {
                bool diff = !Equals(_currentState, value);
                _newState = value;
                if (diff)
                {
                    if (_animationsNeedCleanup && Control.IsHandleCreated) Interop.BufferedPaintStopAllAnimations(Control.Handle);
                    Control.Invalidate();
                }
            }
        }

        /// <summary>
        /// Initialises a new instance of the BufferedPainter class.
        /// </summary>
        /// <param name="control">
        /// Control this instance is attached to.
        /// <para>
        /// For best results, use a control which does not paint its background.
        /// </para>
        /// <para>
        /// Note: Buffered painting does not work if the OptimizedDoubleBuffer flag is set for the control.
        /// </para> 
        /// </param>
        public BufferedPainter(Control control)
        {
            Transitions = new HashSet<BufferedPaintTransition<TState>>();
            Triggers = new HashSet<VisualStateTrigger<TState>>();

            _enabled = true;
            _defaultState = _currentState = _newState = default;

            Control = control;
            _oldSize = Control.Size;

            // buffered painting requires Windows Vista and above with themes supported and enabled (i.e. Basic/Aero theme, not Classic)
            BufferedPaintSupported = IsSupported();

            Control.Resize += new EventHandler(Control_Resize);
            Control.Disposed += new EventHandler(Control_Disposed);
            Control.Paint += new PaintEventHandler(Control_Paint);
            Control.HandleCreated += new EventHandler(Control_HandleCreated);

            Control.MouseEnter += (o, e) => EvalTriggers();
            Control.MouseLeave += (o, e) => EvalTriggers();
            Control.MouseMove += (o, e) => EvalTriggers();
            Control.GotFocus += (o, e) => EvalTriggers();
            Control.LostFocus += (o, e) => EvalTriggers();
            Control.MouseDown += (o, e) => EvalTriggers();
            Control.MouseUp += (o, e) => EvalTriggers();
        }

        /// <summary>
        /// Returns a value indicating whether buffered painting is supported under the current OS and configuration.
        /// </summary>
        /// <returns></returns>
        internal static bool IsSupported()
        {
            return Environment.OSVersion.Version.Major >= 6 && VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles;
        }

        /// <summary>
        /// Short-hand method for adding a state transition.
        /// </summary>
        /// <param name="fromState">The previous visual state.</param>
        /// <param name="toState">The new visual state.</param>
        /// <param name="duration">Duration of the animation (in milliseconds).</param>
        public void AddTransition(TState fromState, TState toState, int duration)
        {
            Transitions.Add(new BufferedPaintTransition<TState>(fromState, toState, duration));
        }

        /// <summary>
        /// Short-hand method for adding a state change trigger.
        /// </summary>
        /// <param name="type">Type of trigger.</param>
        /// <param name="state">Visual state applied when the trigger occurs.</param>
        /// <param name="bounds">Bounds within which the trigger applies.</param>
        /// <param name="anchor">How the bounds are anchored to the control.</param>
        public void AddTrigger(VisualStateTriggerTypes type, TState state, Rectangle bounds = default, AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left)
        {
            Triggers.Add(new VisualStateTrigger<TState>(type, state, bounds, anchor));
        }

        /// <summary>
        /// Evaluates all state change triggers.
        /// </summary>
        private void EvalTriggers()
        {
            if (!Triggers.Any()) return;

            TState newState = DefaultState;

            ApplyCondition(VisualStateTriggerTypes.Focused, ref newState);
            ApplyCondition(VisualStateTriggerTypes.Hot, ref newState);
            ApplyCondition(VisualStateTriggerTypes.Pushed, ref newState);

            State = newState;
        }

        /// <summary>
        /// Helper method for EvalTriggers().
        /// </summary>
        /// <param name="type">Type of trigger to search for.</param>
        /// <param name="stateIfTrue">Reference to the visual state variable to update (if the trigger occurs).</param>
        private void ApplyCondition(VisualStateTriggerTypes type, ref TState stateIfTrue)
        {
            foreach (VisualStateTrigger<TState> trigger in Triggers.Where(x => x.Type == type))
            {
                if (trigger != null)
                {
                    Rectangle bounds = trigger.Bounds != Rectangle.Empty ? trigger.Bounds : Control.ClientRectangle;

                    bool inRect = bounds.Contains(Control.PointToClient(Cursor.Position));
                    bool other = true;

                    switch (type)
                    {
                        case VisualStateTriggerTypes.Focused:
                            other = Control.Focused;
                            inRect = true;
                            break;
                        case VisualStateTriggerTypes.Pushed:
                            other = (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;
                            break;
                    }
                    if (other && inRect) stateIfTrue = trigger.State;
                }
            }
        }

        /// <summary>
        /// Deactivates buffered painting.
        /// </summary>
        private void CleanupAnimations()
        {
            if (Control.InvokeRequired)
            {
                Control.Invoke(new MethodInvoker(CleanupAnimations));
            }
            else if (_animationsNeedCleanup)
            {
                if (Control.IsHandleCreated) Interop.BufferedPaintStopAllAnimations(Control.Handle);
                Interop.BufferedPaintUnInit();
                _animationsNeedCleanup = false;
            }
        }

        void Control_HandleCreated(object sender, EventArgs e)
        {
            if (BufferedPaintSupported)
            {
                Interop.BufferedPaintInit();
                _animationsNeedCleanup = true;
            }
        }

        void Control_Disposed(object sender, EventArgs e)
        {
            if (_animationsNeedCleanup)
            {
                Interop.BufferedPaintUnInit();
                _animationsNeedCleanup = false;
            }
        }

        void Control_Resize(object sender, EventArgs e)
        {
            // resizing stops all playing animations
            if (_animationsNeedCleanup && Control.IsHandleCreated) Interop.BufferedPaintStopAllAnimations(Control.Handle);

            // update trigger bounds according to anchor styles
            foreach (VisualStateTrigger<TState> trigger in Triggers)
            {
                if (trigger.Bounds != Rectangle.Empty)
                {
                    Rectangle newBounds = trigger.Bounds;

                    if ((trigger.Anchor & AnchorStyles.Left) != AnchorStyles.Left)
                    {
                        newBounds.X += Control.Width - _oldSize.Width;
                    }

                    if ((trigger.Anchor & AnchorStyles.Top) != AnchorStyles.Top)
                    {
                        newBounds.Y += Control.Height - _oldSize.Height;
                    }

                    if ((trigger.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
                    {
                        newBounds.Width += Control.Width - _oldSize.Width;
                    }

                    if ((trigger.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                    {
                        newBounds.Height += Control.Height - _oldSize.Height;
                    }

                    trigger.Bounds = newBounds;
                }
            }

            // save old size for next resize
            _oldSize = Control.Size;
        }

        void Control_Paint(object sender, PaintEventArgs e)
        {
            if (BufferedPaintSupported && Enabled)
            {
                bool stateChanged = !Equals(_currentState, _newState);

                nint hdc = e.Graphics.GetHdc();
                if (hdc != nint.Zero)
                {
                    // see if this paint was generated by a soft-fade animation
                    if (!Interop.BufferedPaintRenderAnimation(Control.Handle, hdc))
                    {
                        Interop.BP_ANIMATIONPARAMS animParams = new Interop.BP_ANIMATIONPARAMS();
                        animParams.cbSize = Marshal.SizeOf(animParams);
                        animParams.style = Interop.BP_ANIMATIONSTYLE.BPAS_LINEAR;

                        // get appropriate animation time depending on state transition (or 0 if unchanged)
                        animParams.dwDuration = 0;
                        if (stateChanged)
                        {
                            BufferedPaintTransition<TState> transition = Transitions.Where(x => Equals(x.FromState, _currentState) && Equals(x.ToState, _newState)).SingleOrDefault();
                            animParams.dwDuration = transition != null ? transition.Duration : DefaultDuration;
                        }

                        Rectangle rc = Control.ClientRectangle;
                        nint hdcFrom, hdcTo;
                        nint hbpAnimation = Interop.BeginBufferedAnimation(Control.Handle, hdc, ref rc, Interop.BP_BUFFERFORMAT.BPBF_COMPATIBLEBITMAP, nint.Zero, ref animParams, out hdcFrom, out hdcTo);
                        if (hbpAnimation != nint.Zero)
                        {
                            if (hdcFrom != nint.Zero)
                            {
                                using (Graphics g = Graphics.FromHdc(hdcFrom))
                                {
                                    OnPaintVisualState(new BufferedPaintEventArgs<TState>(_currentState, g));
                                }
                            }
                            if (hdcTo != nint.Zero)
                            {
                                using (Graphics g = Graphics.FromHdc(hdcTo))
                                {
                                    OnPaintVisualState(new BufferedPaintEventArgs<TState>(_newState, g));
                                }
                            }

                            _currentState = _newState;
                            Interop.EndBufferedAnimation(hbpAnimation, true);
                        }
                        else
                        {
                            OnPaintVisualState(new BufferedPaintEventArgs<TState>(_currentState, e.Graphics));
                        }
                    }

                    e.Graphics.ReleaseHdc(hdc);
                }
            }
            else
            {
                // buffered painting not supported, just paint using the current state
                _currentState = _newState;
                OnPaintVisualState(new BufferedPaintEventArgs<TState>(_currentState, e.Graphics));
            }
        }
    }

    /// <summary>
    /// Represents a transition between two visual states. Describes the duration of the animation. 
    /// Two transitions are considered equal if they represent the same change in visual state.
    /// </summary>
    /// <typeparam name="TState">Any type representing the visual state of the control.</typeparam>
    public class BufferedPaintTransition<TState> : IEquatable<BufferedPaintTransition<TState>>
    {

        /// <summary>
        /// Gets the previous visual state.
        /// </summary>
        public TState FromState { get; private set; }
        /// <summary>
        /// Gets the new visual state.
        /// </summary>
        public TState ToState { get; private set; }
        /// <summary>
        /// Gets or sets the duration (in milliseconds) of the animation.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Initialises a new instance of the BufferedPaintTransition class.
        /// </summary>
        /// <param name="fromState">The previous visual state.</param>
        /// <param name="toState">The new visual state.</param>
        /// <param name="duration">Duration of the animation (in milliseconds).</param>
        public BufferedPaintTransition(TState fromState, TState toState, int duration)
        {
            FromState = fromState;
            ToState = toState;
            Duration = duration;
        }

        /// <summary>
        /// Determines if two instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is BufferedPaintTransition<TState>)
                return ((IEquatable<BufferedPaintTransition<TState>>)this).Equals((BufferedPaintTransition<TState>)obj);
            else
                return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((object)FromState ?? 0).GetHashCode() ^ ((object)ToState ?? 0).GetHashCode();
        }

        #region IEquatable<BufferedPaintAnimation<TState>> Members

        bool IEquatable<BufferedPaintTransition<TState>>.Equals(BufferedPaintTransition<TState> other)
        {
            return Equals(FromState, other.FromState) && Equals(ToState, other.ToState);
        }

        #endregion
    }

    /// <summary>
    /// Represents a trigger for a particular visual state. 
    /// Two triggers are considered equal if they are of the same type and visual state.
    /// </summary>
    /// <typeparam name="TState">Any type representing the visual state of the control.</typeparam>
    public class VisualStateTrigger<TState> : IEquatable<VisualStateTrigger<TState>>
    {

        /// <summary>
        /// Gets the type of trigger.
        /// </summary>
        public VisualStateTriggerTypes Type { get; private set; }
        /// <summary>
        /// Gets the visual state applied when the trigger occurs.
        /// </summary>
        public TState State { get; private set; }
        /// <summary>
        /// Gets or sets the bounds within which the trigger applies.
        /// </summary>
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// Gets or sets how the bounds are anchored to the edges of the control.
        /// </summary>
        public AnchorStyles Anchor { get; set; }

        /// <summary>
        /// Initialises a new instance of the VisualStateTrigger class.
        /// </summary>
        /// <param name="type">Type of trigger.</param>
        /// <param name="state">Visual state applied when the trigger occurs.</param>
        /// <param name="bounds">Bounds within which the trigger applies.</param>
        /// <param name="anchor">How the bounds are anchored to the edges of the control.</param>
        public VisualStateTrigger(VisualStateTriggerTypes type, TState state, Rectangle bounds = default, AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left)
        {
            Type = type;
            State = state;
            Bounds = bounds;
            Anchor = anchor;
        }

        /// <summary>
        /// Determines if two instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is BufferedPaintTransition<TState>)
                return ((IEquatable<VisualStateTrigger<TState>>)this).Equals((VisualStateTrigger<TState>)obj);
            else
                return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ ((object)State ?? 0).GetHashCode();
        }

        #region IEquatable<VisualStateTrigger<TState>> Members

        bool IEquatable<VisualStateTrigger<TState>>.Equals(VisualStateTrigger<TState> other)
        {
            return Type == other.Type && Equals(State, other.State);
        }

        #endregion
    }

    /// <summary>
    /// Represents the types of trigger which can change the visual state of a control.
    /// </summary>
    public enum VisualStateTriggerTypes
    {
        /// <summary>
        /// The control receives input focus.
        /// </summary>
        Focused,
        /// <summary>
        /// The mouse is over the control.
        /// </summary>
        Hot,
        /// <summary>
        /// The left mouse button is pressed on the control.
        /// </summary>
        Pushed
    }

    /// <summary>
    /// EventArgs class for the BufferedPainter.PaintVisualState event.
    /// </summary>
    /// <typeparam name="TState">Any type representing the visual state of the control.</typeparam>
    public class BufferedPaintEventArgs<TState> : EventArgs
    {

        /// <summary>
        /// Gets the visual state to paint.
        /// </summary>
        public TState State { get; private set; }
        /// <summary>
        /// Gets the Graphics object on which to paint.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Initialises a new instance of the BufferedPaintEventArgs class.
        /// </summary>
        /// <param name="state">Visual state to paint.</param>
        /// <param name="graphics">Graphics object on which to paint.</param>
        public BufferedPaintEventArgs(TState state, Graphics graphics)
        {
            State = state;
            Graphics = graphics;
        }
    }

    internal static class Interop
    {

        [DllImport("uxtheme")]
        public static extern nint BufferedPaintInit();

        [DllImport("uxtheme")]
        public static extern nint BufferedPaintUnInit();

        [DllImport("uxtheme")]
        public static extern nint BeginBufferedAnimation(
            nint hwnd,
            nint hdcTarget,
            ref Rectangle rcTarget,
            BP_BUFFERFORMAT dwFormat,
            nint pPaintParams,
            ref BP_ANIMATIONPARAMS pAnimationParams,
            out nint phdcFrom,
            out nint phdcTo
        );

        [DllImport("uxtheme")]
        public static extern nint EndBufferedAnimation(nint hbpAnimation, bool fUpdateTarget);

        [DllImport("uxtheme")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BufferedPaintRenderAnimation(nint hwnd, nint hdcTarget);

        [DllImport("uxtheme")]
        public static extern nint BufferedPaintStopAllAnimations(nint hwnd);

        [StructLayout(LayoutKind.Sequential)]
        internal struct BP_ANIMATIONPARAMS
        {
            public int cbSize, dwFlags;
            public BP_ANIMATIONSTYLE style;
            public int dwDuration;
        }

        internal enum BP_BUFFERFORMAT
        {
            BPBF_COMPATIBLEBITMAP,
            BPBF_DIB,
            BPBF_TOPDOWNDIB,
            BPBF_TOPDOWNMONODIB
        }

        [Flags]
        internal enum BP_ANIMATIONSTYLE
        {
            BPAS_NONE = 0,
            BPAS_LINEAR = 1,
            BPAS_CUBIC = 2,
            BPAS_SINE = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        const int MM_TEXT = 1;

        [DllImport("gdi32")]
        static extern int SetMapMode(nint hdc, int fnMapMode);

        [DllImport("user32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern bool DrawFocusRect(HandleRef hDc, ref RECT lpRect);

        public static void DrawFocusRect(Graphics graphics, Rectangle r)
        {
            if (r.IsEmpty) return;

            nint hdc = graphics.GetHdc();
            try
            {
                int iMode = SetMapMode(hdc, MM_TEXT);
                RECT rect = new RECT() { left = r.Left, top = r.Top, right = r.Right, bottom = r.Bottom };
                DrawFocusRect(new HandleRef(graphics, hdc), ref rect);
                if (iMode != MM_TEXT) SetMapMode(hdc, iMode);
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
        }
    }
}