/*

  SlickInterface
  Copyright (c) 2010, Christoph Heindl
  All rights reserved.

  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  - Redistributions of source code must retain the above copyright notice, this list 
    of conditions and the following disclaimer.
  - Redistributions in binary form must reproduce the above copyright notice, this list 
    of conditions and the following disclaimer in the documentation and/or other materials 
    provided with the distribution.
  - Neither the name of the Christoph Heindl nor the names of its contributors may be 
    used to endorse or promote products derived from this software without specific prior 
    written permission.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
  OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING 
  IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SlickInterface {

  /// <summary>
  /// Used to control slides.
  /// </summary>
  public partial class SlideControl : UserControl {
    private Stack<Slide> _undo;
    private Slide _selected;
    private int _slide_speed;

    /// <summary>
    /// Triggered when a slide change occurred.
    /// </summary>
    public event EventHandler<SlideChangedArgs> SlideChanged;

    /// <summary>
    /// Construct new SlideControl
    /// </summary>
    public SlideControl() {
      InitializeComponent();
      _undo = new Stack<Slide>();
      _selected = null;
      _slide_speed = 250;
    }

    /// <summary>
    /// Get and set the speed of the slide-animation in milliseconds.
    /// </summary>
    [Description("Animation slide speed of the in milliseconds.")]
    public int SlideSpeed {
      get { return _slide_speed; }
      set { _slide_speed = value; }
    }

    /// <summary>
    /// Add a new slide to this SlideControl.
    /// </summary>
    /// <param name="sc">Slide to add.</param>
    public void AddSlide(Slide sc) {
      this.SuspendLayout();
      sc.Dock = DockStyle.Fill;
      sc.Visible = false;
      this.Controls.Add(sc);
      sc.SlideControl = this;
      this.ResumeLayout();
    }

    /// <summary>
    /// Get and set the selected slide.
    /// </summary>
    /// <remarks>
    /// Setting a selected Slide does not modify the state of the undo-stack and
    /// is considered a forward movement.
    /// </remarks>
    public Slide Selected {
      get { return _selected; }
      set {
        if (this.RequestTransition(_selected, value)) {
          SlidingEventArgs e = new SlidingEventArgs(SlideDirection.Forward, null);
          if (_selected != null) _selected.OnSlidingOutInternal(e);
          _selected = value;
          _selected.BringToFront();
          _selected.OnSlidingInInternal(e);
          _selected.Visible = true;
        }
      }
    }

    /// <summary>
    /// Test if a transition between two slides is allowed
    /// </summary>
    /// <param name="from">Slide out</param>
    /// <param name="to">Slide in</param>
    /// <returns>True if transition is ok to perform, false otherwise</returns>
    private bool RequestTransition(Slide from, Slide to) {
      return
        (from == null || from.SlideOutRequestedInternal()) &&
        (to != null && to.SlideInRequestedInternal()) &&
        (from != to);
    }

    /// <summary>
    /// Forward to slide given by name.
    /// </summary>
    /// <remarks>When forwarding to a slide the state of undo-stack will be modified to
    /// contain the last Selected Slide.</remarks>
    /// <param name="name">Name of slide to forward to.</param>
    public void ForwardTo(string name) {
      this.ForwardTo(this.FindByName(name), null);
    }

    /// <summary>
    /// Forward to slide given by name.
    /// </summary>
    /// <remarks>When forwarding to a slide the state of undo-stack will be modified to
    /// contain the last Selected Slide.</remarks>
    /// <param name="name">Name of slide to forward to.</param>
    /// <param name="user_data">User data</param>
    public void ForwardTo(string name, object user_data) {
      this.ForwardTo(this.FindByName(name), user_data);
    }

    /// <summary>
    /// Forward to slide given by type.
    /// </summary>
    /// <remarks>When forwarding to a slide the state of undo-stack will be modified to
    /// contain the last Selected Slide. Forwards to the first slide of given type.
    /// </remarks>
    /// <typeparam name="T">Type of Slide to forward to.</typeparam>
    public void ForwardTo<T>() where T : Slide {
      this.ForwardTo(this.FindByType<T>(), null);
    }

    /// <summary>
    /// Forward to slide given by type.
    /// </summary>
    /// <remarks>When forwarding to a slide the state of undo-stack will be modified to
    /// contain the last Selected Slide. Forwards to the first slide of given type.
    /// </remarks>
    /// <typeparam name="T">Type of Slide to forward to.</typeparam>
    public void ForwardTo<T>(object user_data) where T : Slide {
      this.ForwardTo(this.FindByType<T>(), user_data);
    }

    /// <summary>
    /// Forward to Slide
    /// </summary>
    /// <remarks>When forwarding to a slide the state of undo-stack will be modified to
    /// contain the last Selected Slide. Forwards to the first slide of given type.
    /// </remarks>
    /// <param name="s"></param>
    /// <param name="user_data">User data</param>
    /// <returns>true if slide change occurred, false otherwise</returns>
    public bool ForwardTo(Slide s, object user_data) {
      if (!RequestTransition(_selected, s))
        return false;

      SlidingEventArgs e = new SlidingEventArgs(SlideDirection.Forward, user_data);
      if (_selected != null) _selected.OnSlidingOutInternal(e);
      AnimateControl(new AnimationProperties(
         _selected,
         AnimationProperties.Direction.Left,
         AnimationProperties.Action.Hide,
         _slide_speed
       ));
      _undo.Push(_selected);
      this.ChangeSlide(s, e);
      return true;
    }

    /// <summary>
    /// Retrieve the top Slide on the undo-stack.
    /// </summary>
    public Slide Previous {
      get { return _undo.Peek(); }
    }

    /// <summary>
    /// True if the undo-stack is not empty; false otherwise.
    /// </summary>
    public bool HasPrevious {
      get { return _undo.Count > 0; }
    }

    /// <summary>
    /// Move backward to last Slide on undo-stack.
    /// </summary>
    /// <remarks>When moving backward the state of the undo-stack is modified:
    /// the top Slide is pulled of the stack.
    /// </remarks>
    public void Backward() {
      if (_undo.Count > 0) {
        Slide dest = _undo.Peek();
        if (this.BackwardTo(dest, null)) {
          _undo.Pop();
        }
      }
    }

    /// <summary>
    /// Move backward to last Slide on undo-stack.
    /// </summary>
    /// <remarks>When moving backward the state of the undo-stack is modified:
    /// the top Slide is pulled of the stack.
    /// </remarks>
    public void Backward(object user_data) {
      if (_undo.Count > 0) {
        Slide dest = _undo.Peek();
        if (this.BackwardTo(dest, user_data)) {
          _undo.Pop();
        }
      }
    }

    /// <summary>
    /// Move backward to Slide by name.
    /// </summary>
    /// <remarks>Does not modify the undo-stack.</remarks>
    /// <param name="name">Name of Slide.</param>
    public void BackwardTo(string name) {
      this.BackwardTo(this.FindByName(name), null);
    }

    /// <summary>
    /// Move backward to Slide by name.
    /// </summary>
    /// <remarks>Does not modify the undo-stack.</remarks>
    /// <param name="name">Name of Slide.</param>
    /// <param name="user_data">User data</param>
    public void BackwardTo(string name, object user_data) {
      this.BackwardTo(this.FindByName(name), user_data);
    }

    /// <summary>
    /// Move backward to Slide by name.
    /// </summary>
    /// <remarks>Does not modify the undo-stack.</remarks>
    /// <typeparam name="T">Type of Slide to forward to.</typeparam>
    public void BackwardTo<T>() where T : Slide {
      this.BackwardTo(this.FindByType<T>(), null);
    }

    /// <summary>
    /// Move backward to Slide by name.
    /// </summary>
    /// <remarks>Does not modify the undo-stack.</remarks>
    /// <typeparam name="T">Type of Slide to forward to.</typeparam>
    public void BackwardTo<T>(object user_data) where T : Slide {
      this.BackwardTo(this.FindByType<T>(), user_data);
    }

    /// <summary>
    /// Move backward to Slide.
    /// </summary>
    /// <remarks>Does not modify the undo-stack.</remarks>
    /// <param name="s"></param>
    /// <param name="user_data">User data</param>
    /// <returns>true if slide change occurred, false otherwise</returns>
    public bool BackwardTo(Slide s, object user_data) {
      if (!RequestTransition(_selected, s))
        return false;

      SlidingEventArgs e = new SlidingEventArgs(SlideDirection.Backward, user_data);
      if (_selected != null) _selected.OnSlidingOutInternal(e);
      AnimateControl(new AnimationProperties(
        _selected,
        AnimationProperties.Direction.Right,
        AnimationProperties.Action.Hide,
        _slide_speed
      ));
      this.ChangeSlide(s, e);
      return true;
    }

    /// <summary>
    /// Change to Slide
    /// </summary>
    /// <param name="next">Slide to change to</param>
    /// <param name="e">Event parameters</param>
    void ChangeSlide(Slide next, SlidingEventArgs e) {
      _selected.Visible = false;
      SlideChangedArgs sch = new SlideChangedArgs(_selected, next);
      _selected = next;
      _selected.BringToFront();
      _selected.OnSlidingInInternal(e);
      _selected.Visible = true;
      if (SlideChanged != null) {
        SlideChanged(this, sch);
      }
    }

    /// <summary>
    /// Find Slide by name.
    /// </summary>
    /// <param name="name">Name of slide.</param>
    /// <returns>Slide if found, null otherwise</returns>
    public Slide FindByName(string name) {
      Control[] dest = this.Controls.Find(name, false);
      if (dest.Length == 0) {
        return null;
      } else {
        return dest[0] as Slide;
      }
    }

    /// <summary>
    /// Find Slide by type.
    /// </summary>
    /// <typeparam name="T">Type of Slide.</typeparam>
    /// <returns>Returns the first Slide of given Type, or null if no slide was found.</returns>
    public T FindByType<T>() where T : Slide {
      IEnumerable<T> dest = this.Controls.OfType<T>();
      try {
        return dest.ElementAt(0);
      } catch (ArgumentOutOfRangeException) {
        return null;
      }
    }

    /// <summary>
    /// Invoke control animation.
    /// </summary>
    /// <param name="props">Animation properties.</param>
    private static void AnimateControl(AnimationProperties props) {
      if (props.duration > 0) {
        const int slide = 0x00040000;
        int ret = AnimateWindow(
          props.control.Handle,
          props.duration,
          slide | (int)props.direction | (int)props.action
        );
      }
    }

    /// <summary>
    /// Describes the type of slide effect.
    /// </summary>
    struct AnimationProperties {

      /// <summary>
      /// Slide direction
      /// </summary>
      public enum Direction {
        Left = 0x00000002,
        Right = 0x00000001
      };

      /// <summary>
      /// Slide action
      /// </summary>
      public enum Action {
        Activate = 0x20000,
        Hide = 0x10000
      };

      /// <summary>
      /// Initialize properties
      /// </summary>
      /// <param name="c">Control to animate.</param>
      /// <param name="d">Direction of animation.</param>
      /// <param name="a">Animation action.</param>
      /// <param name="msecs">Duration of animation</param>
      public AnimationProperties(Control c, Direction d, Action a, int msecs) {
        control = c;
        direction = d;
        action = a;
        duration = msecs;
      }

      public int duration;
      public Control control;
      public Direction direction;
      public Action action;
    };

    [DllImport("User32.dll")]
    private static extern int AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
  }
}
